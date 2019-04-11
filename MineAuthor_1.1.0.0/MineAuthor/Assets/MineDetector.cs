using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script manages the mine detector prefab. 
/// It draws the line renderer 
/// </summary>

[RequireComponent(typeof(LineRenderer))]

public class MineDetector : MonoBehaviour {

    public Transform m_laserStartPoint;
	// Use this for initialization
	void Start () {
        m_lineRenderer = GetComponent<LineRenderer>();
        m_controller = FindObjectOfType<LaserController>();
        m_transform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameEngine.ISGAMEON)
        {
            DrawLine();
        }
	}

    private void DrawLine()
    {
        m_lineRenderer.enabled = m_controller.AllowLaserDraw;
        m_lineRenderer.positionCount = 2;
        m_lineRenderer.SetPosition(0, m_laserStartPoint.position);
        m_lineRenderer.SetPosition(1, m_controller.GetCursorPosition());
    }
    private Transform m_transform;
    private LaserController m_controller;
    private LineRenderer m_lineRenderer;
}
