using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ajc.HIMineSweeper
{
    public class SmoothLookAtTarget : MonoBehaviour
    {
        public Transform m_target;
        // Use this for initialization
        void Start()
        {
            m_transform = transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (m_target)
            {
                Vector3 direction =  m_transform.position - m_target.position;
                Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
                m_transform.rotation = rotation;
            }
        }

        private Transform m_transform;
    }

}

