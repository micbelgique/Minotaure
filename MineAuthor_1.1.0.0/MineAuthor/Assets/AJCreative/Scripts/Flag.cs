using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ajc.HIMineSweeper
{
    
    public class Flag : MonoBehaviour
    {

        public AudioClip m_removeClip;
            // Use this for initialization
            void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Remove()
        {

            AudioSource.PlayClipAtPoint(m_removeClip, transform.position);           
        }
    }

}