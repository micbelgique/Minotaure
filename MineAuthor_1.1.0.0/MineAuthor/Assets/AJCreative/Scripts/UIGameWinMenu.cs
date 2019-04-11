using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ajc.HIMineSweeper
{
    public class UIGameWinMenu : MonoBehaviour
    {
        private void Awake()
        {
            m_follow = GetComponent<SmoothFollowTarget>();
        }

        // Use this for initialization
        void Start()
        {
            gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Show(float _delayInSeconds = 0)
        {
            gameObject.SetActive(true);
            m_follow.TeleportToPosition();
    

    }
        public void Close()
        {
            gameObject.SetActive(false);
        }
        private Animator m_animator;
        private SmoothFollowTarget m_follow;
    }

}
