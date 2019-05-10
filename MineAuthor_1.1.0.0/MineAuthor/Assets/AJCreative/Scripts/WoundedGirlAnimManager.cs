using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ajc.HIMineSweeper
{
    public class WoundedGirlAnimManager : MonoBehaviour
    {
        public float m_standardIdleDuration = 5;
        public Animator m_animator;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(Time.time>m_standardIdleDuration + m_startTimer)
            {
                m_startTimer = Time.time;
                int decision = Mathf.RoundToInt(Random.value);
                print(decision);
                if (decision == 0)
                {
                    m_animator.SetTrigger("MoaningTrigger");
                }
                else
                {
                    m_animator.SetTrigger("BreathlessTrigger");
                }
                
                
            }
        }

        private float m_startTimer;
    }
}

