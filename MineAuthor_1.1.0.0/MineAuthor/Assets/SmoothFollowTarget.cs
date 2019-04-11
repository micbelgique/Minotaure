using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ajc.HIMineSweeper
{
    /// <summary>
    /// This Script allows the object to always stay in front of the user
    /// </summary>
    public class SmoothFollowTarget : MonoBehaviour
    {
        public Transform m_target;
        public Vector3 m_offset;
        public bool m_lerpPosition;
        public float m_distance = 2;
        public float m_speed = 1;
        // Use this for initialization
        void Start()
        {
            m_transform = transform;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 targetedPosition = m_target.position + m_target.forward * m_distance;

            if (m_lerpPosition)
            {
                
                m_transform.position = Vector3.Lerp(m_transform.position, targetedPosition, m_speed * Time.deltaTime);
            }
            else
            {
                m_transform.position = targetedPosition;
            }
            
        }
        private Transform m_transform;
    }

}
