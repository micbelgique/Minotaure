using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace com.ajc.HIMineSweeper
{
    public class UIGameOverMenu : MonoBehaviour
    {
        public TMP_Text m_gameOverText;

        public string m_outOfTimeGameOverText;
        public string m_explosionGameOver;

        private void Awake()
        {
            m_follow = GetComponent<SmoothFollowTarget>();
        }
        // Use this for initialization
        void Start()
        {
            m_animator = GetComponent<Animator>();
            gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Show(GameManager.GAMEOVERSTATE _context, float _delayInSeconds)
        {
            gameObject.SetActive(true);
            m_follow.TeleportToPosition();
            switch (_context)
            {
                case GameManager.GAMEOVERSTATE.OUTOFTIME:
                    m_gameOverText.SetText(m_outOfTimeGameOverText);
                    break;
                case GameManager.GAMEOVERSTATE.EXPLOSION:
                    m_gameOverText.SetText(m_explosionGameOver);
                    break;
                default:
                    break;
            }
        }
        public void Close()
        {
            gameObject.SetActive(false);
        }
        private Animator m_animator;

        private SmoothFollowTarget m_follow;
    }

}
