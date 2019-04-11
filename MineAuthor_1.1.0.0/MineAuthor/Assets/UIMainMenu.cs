using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ajc.HIMineSweeper
{
    public class UIMainMenu : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            m_animator = GetComponent<Animator>();
            Show();
        }

        // Update is called once per frame
        void Update()
        {

        }
        /// <summary>
        /// Close the menu when :
        ///  - the Start Button is clicked
        /// </summary>
        public void Close()
        {
            m_animator.SetBool("HideBool", true);
        }
        /// <summary>
        /// Show the menu when :
        /// - The Scene starts
        /// </summary>
        public void Show()
        {
            m_animator.SetBool("HideBool", false);
        }

        private Animator m_animator;
    }

}
