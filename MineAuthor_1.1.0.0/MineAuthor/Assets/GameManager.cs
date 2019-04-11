using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ajc.HIMineSweeper
{
    /// <summary>
    /// This script manages the GameEngine and LaserController
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public AudioSource m_heartbeat;
        public GameEngine m_engine;
        public LaserController m_controller;

        [Header("UI Elements")]
        public UIGameOverMenu m_gameOverMenu;
        public UIGameWinMenu m_gameWinMenu;

        public enum GAMEOVERSTATE { OUTOFTIME, EXPLOSION };
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartGame()
        {
            m_engine.StartGame();
            m_heartbeat.Play();
        }
        public void ResetGame()
        {
            m_heartbeat.Stop();
            m_engine.ReinitGame();
            m_controller.ResetGrid();
            m_controller.TeleportPlayerAtStartPosition();

            StartGame();
        }
        public void GameOver(GAMEOVERSTATE _state)
        {
            //Show GameOver Menu in front of the player;
            m_engine.EndGame();
            m_gameOverMenu.Show(_state, 3);
        }

        public void GameWin()
        {
            m_engine.EndGame();
        }
    }

}
