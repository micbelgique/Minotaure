using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using UnityEngine.SceneManagement;
using HoloToolkit.Unity.InputModule;
using com.ajc.HIMineSweeper;

public class LaserController : MonoBehaviour {
    public float m_laserDistance = 5;
    public LayerMask m_layerMask;
    public GameManager m_gameManager;
    public Collider terrainCollider;
    public Transform ParentTransform;
    public GameObject IndicatorPrefab;
    public GameObject ExplosionPrefab;
    public GameObject TracePrefab;
    public GameObject m_flagPrefab;
    public bool SelectWasPressed = false;
    public bool ThumbStickWasPressed = false;
    public MixedRealityTeleport teleport;
    AudioSource clicsound;
    private float timer;
    private bool timerOn;
    public GameEngine gameEngine;
    private List<GameObject> instList;
    public Vector3 StartPosition { get { return new Vector3(-2, 5, 10); } }
    private bool m_allowLaserDraw;

    public bool AllowLaserDraw { get { return m_allowLaserDraw; } }

    
    //public Projector projector;

    // Use this for initialization
    void Start () {
        //InteractionManager.InteractionSourcePressed += InteractionManager_InteractionSourcePressed;

        clicsound = GetComponent<AudioSource>();
        //teleport.SetWorldPosition(StartPosition);
        instList = new List<GameObject>();
        timerOn = false;
        //projector = GetComponent<Projector>();
    }

    IEnumerator waitEndOfAnimation()
    {
        yield return new WaitForSeconds(7);
    }

    private bool ValidRotation(Quaternion newRotation)
    {
        return !float.IsNaN(newRotation.x) && !float.IsNaN(newRotation.y) && !float.IsNaN(newRotation.z) && !float.IsNaN(newRotation.w) &&
            !float.IsInfinity(newRotation.x) && !float.IsInfinity(newRotation.y) && !float.IsInfinity(newRotation.z) && !float.IsInfinity(newRotation.w);
    }

    private bool ValidPosition(Vector3 newPosition)
    {
        return !float.IsNaN(newPosition.x) && !float.IsNaN(newPosition.y) && !float.IsNaN(newPosition.z) &&
            !float.IsInfinity(newPosition.x) && !float.IsInfinity(newPosition.y) && !float.IsInfinity(newPosition.z);
    }


    void Update()
    {
        var interactionSourceStates = InteractionManager.GetCurrentReading();

        bool allowInteraction = false;

        Vector3 newPosition = Vector3.zero ;
        Quaternion newRotation = Quaternion.identity;

        // Draw Laser Pointer
        foreach (var interactionSourceState in interactionSourceStates)
        {
            if (interactionSourceState.sourcePose.TryGetPosition(out newPosition, InteractionSourceNode.Pointer) && ValidPosition(newPosition)
                && interactionSourceState.sourcePose.TryGetRotation(out newRotation, InteractionSourceNode.Pointer) && ValidRotation(newRotation))
            {
                var ray = new Ray(
                    ParentTransform.TransformPoint(newPosition),
                    ParentTransform.TransformDirection(newRotation * Vector3.forward)
                    );

                RaycastHit raycastHit;
                
                if (Physics.Raycast(ray, out raycastHit, m_laserDistance, m_layerMask))
                {
                    m_allowLaserDraw = true;
                    m_cursorPosition = raycastHit.point;
                    allowInteraction = true;
                }
                else
                {
                    m_cursorPosition = ray.GetPoint(m_laserDistance);
                    //m_allowLaserDraw = false;
                    allowInteraction = false;
                }
            }
        }

        if (GameEngine.ISGAMEON)
        {
            Vector3 Position = getCellCenterPosition(Camera.main.transform.position);
            int minev = getCellMineValue(Position);
            if (minev == -1)            {

                m_gameManager.GameOver(GameManager.GAMEOVERSTATE.EXPLOSION);

                GameObject explosion = Instantiate(ExplosionPrefab, Vector3.zero, Quaternion.identity);
                explosion.transform.position = Position;
                instList.Add(explosion);
            }

            if (allowInteraction)
            {
                foreach (var interactionSourceState in interactionSourceStates)
                {
                    Vector3 IndicatorPosition = getCellCenterPosition(m_cursorPosition);
                    //Debug.Log("Centres = " + IndicatorPosition);

                    bool visible = getCellIndicatorVisibility(IndicatorPosition);
                    
                    bool flagged = GetCellFlagStatus(IndicatorPosition);
                    

                    //Select Button Interaction
                    if (!SelectWasPressed && interactionSourceState.selectPressed)
                    {
                        //print("visible:"+visible + " - "+ "Flagged: "+ flagged);
                        if (visible == false && flagged == false)
                        {
                            int minevalue = RevealCell(IndicatorPosition);
                            //print("pressed");
                            if (minevalue == -1)
                            {

                                // GameOver
                                m_gameManager.GameOver(GameManager.GAMEOVERSTATE.EXPLOSION);

                                //Instantiate Mine Prefab + Sound of Mine Activation
                                //Show GameOver Menu (can be don evia GameEngine)
                                GameObject explosion = Instantiate(ExplosionPrefab, Vector3.zero, Quaternion.identity);
                                explosion.transform.position = IndicatorPosition;
                                instList.Add(explosion);

                                GameObject trace = Instantiate(TracePrefab, Vector3.zero, Quaternion.identity);
                                trace.transform.Rotate(new Vector3(90.0f, 0.0f, 0.0f));
                                trace.transform.position = new Vector3(IndicatorPosition.x, IndicatorPosition.y + 0.1f, IndicatorPosition.z);
                                instList.Add(trace);
                            }
                            else if (minevalue == -2)
                            {
                                //Application.LoadLevel(Application.loadedLevel);
                                //teleport.SetWorldPosition(new Vector3(8.0f, 8.0f, 9.01f));
                                //Win
                                //gameEngine.ReinitGame();                                
                                m_gameManager.GameWin();

                            }
                            else if (minevalue != -3)
                            {
                                clicsound.Play();

                                GameObject MineTextindicator = Instantiate(IndicatorPrefab, Vector3.zero, Quaternion.identity);
                                MineTextindicator.transform.GetComponent<TextMesh>().transform.Rotate(new Vector3(90.0f, -90.0f, 0.0f));
                                MineTextindicator.transform.GetComponent<TextMesh>().transform.position = IndicatorPosition;
                                MineTextindicator.transform.GetComponent<TextMesh>().text = minevalue.ToString();
                                instList.Add(MineTextindicator);                                
                            }
                        }
                    }
                    if (!ThumbStickWasPressed && interactionSourceState.thumbstickPressed)
                    {
                        if (visible)
                        {
                            //print("visible");
                            break;
                        }
                        
                        if (flagged)
                        {
                            //Unflag
                            //print("Unflagged");
                            gameEngine.UnflagCell(IndicatorPosition.z, -IndicatorPosition.x);
                            foreach (GameObject go in instList)
                            {
                                if (go.name == "flag_" + ((int)IndicatorPosition.z).ToString()+(-1*IndicatorPosition.x).ToString())
                                {
                                    instList.Remove(go);
                                    Destroy(go);
                                    break;
                                }
                            }

                        }
                        else
                        {
                            //Flag
                            GameObject flag = Instantiate(m_flagPrefab, IndicatorPosition, Quaternion.identity);
                            flag.name = "flag_" + ((int)IndicatorPosition.z).ToString() + (-1 * IndicatorPosition.x).ToString();
                            instList.Add(flag);
                            gameEngine.FlagCell(IndicatorPosition.z, -IndicatorPosition.x);
                        }

                    }
                    ThumbStickWasPressed = interactionSourceState.thumbstickPressed;
                    SelectWasPressed = interactionSourceState.selectPressed;
                }                   
                
            }
        }      
                
    }

    public void ResetGrid()
    {
        for (int i = 0; i < instList.Count; i++)
        {
            GameObject g = instList[i];
            Destroy(g);
        }
        

    }
    public Vector3 getCellCenterPosition(Vector3 initialPosition)
    {
        return new Vector3(Mathf.Floor(initialPosition.x)+0.5f, initialPosition.y, Mathf.Floor(initialPosition.z)+0.5f);

    }

    public int RevealCell(Vector3 initialPosition)
    {
        return gameEngine.checkCell(initialPosition.z, -initialPosition.x);
    }

    public int getCellMineValue(Vector3 initialPosition)
    {
        return gameEngine.checkMine(initialPosition.z, -initialPosition.x);
    }

    public bool getCellIndicatorVisibility(Vector3 initialPosition)
    {
        return gameEngine.isCellRevealed(initialPosition.z, -initialPosition.x);
    }

    public bool GetCellFlagStatus(Vector3 initialPosition)
    {
        return gameEngine.isCellFlagged(initialPosition.z, -initialPosition.x);
    }

    // Update is called once per frame
    void InteractionManager_InteractionSourcePressed(InteractionSourcePressedEventArgs args) {
        var interactionSourceStates = InteractionManager.GetCurrentReading();

        foreach (var interactionSourceState in interactionSourceStates)
        {
            Vector3 newPosition;
            Quaternion newRotation;

            if (interactionSourceState.sourcePose.TryGetPosition(out newPosition, InteractionSourceNode.Pointer) && ValidPosition(newPosition)
                && interactionSourceState.sourcePose.TryGetRotation(out newRotation, InteractionSourceNode.Pointer) && ValidRotation(newRotation))
            {
                var ray = new Ray(
                    ParentTransform.TransformPoint( newPosition ),
                    ParentTransform.TransformDirection(newRotation * Vector3.forward)
                    );

                //Debug.DrawRay(ray.origin, ray.direction, Color.red);

                RaycastHit raycastHit;
                if (terrainCollider.Raycast(ray, out raycastHit, 10))
                {
                    var cursorPos = raycastHit.point;
                    Debug.Log("Collision X = " + Mathf.Floor(-cursorPos.x) + " Z = " + Mathf.Floor(cursorPos.z));

                    GameObject MineTextindicator = Instantiate(IndicatorPrefab, Vector3.zero, Quaternion.identity);
                    MineTextindicator.transform.GetComponent<TextMesh>().text = "0";
                    instList.Add(MineTextindicator);
                }
            }
        }
    }
    public Vector3 GetCursorPosition()
    {
        return m_cursorPosition;
    }

    public void TeleportPlayerAtStartPosition()
    {
        teleport.SetWorldPosition(StartPosition);
    }
    private Vector3 m_cursorPosition;
}
