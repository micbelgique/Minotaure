using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
/// <summary>
/// This class display the timer info on a UI element
/// AJCreative
/// </summary>
public class UITimerDisplayer : MonoBehaviour {

    public TMP_Text m_timerText;
	// Use this for initialization
	void Start () {
        m_engine = FindObjectOfType<GameEngine>();
	}
	
	// Update is called once per frame
	void Update () {

        int seconds = (int)m_engine.GetCurrentTimer();
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
        string timeText = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);

        m_timerText.SetText(timeText);
	}

    private GameEngine m_engine;
}
