///-----------------------------------------------------------------
/// Author : Adrien HEISCH
/// Date : 25/01/2019 16:15
///-----------------------------------------------------------------

using Com.AdrienHeisch.NotSpaceWar.ShipControllers;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.AdrienHeisch.NotSpaceWar
{
    public class LevelManager : MonoBehaviour
	{

        public static LevelManager instance { get; private set; }
        
        public event Action onGameStart;
        public event Action<Ship> onGameOver;
        
        [SerializeField] private bool autoStart = false;
        [SerializeField] private int defaultNShips = 4;
        [SerializeField] private int defaultNPlayers = 1;
        [SerializeField] private List<Ship> initShips = new List<Ship>();
        private List<Ship> ships;

        private bool noInit = true;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            if (autoStart) StartGame();
        }

        public void InitGame (int nShips, int nPlayers)
        {
            noInit = false;
            ships = initShips;

            for (int i = ships.Count - 1; i >= 0; i--)
            {
                if (i >= nShips)
                {
                    ships[i].Kill();
                    ships.RemoveAt(i);
                    continue;
                }

                if (i < nPlayers)
                {
                    Component aiController = ships[i].GetComponent<AIShipController>();
                    if (aiController != null) Destroy(aiController);

                    Component playerController = ships[i].GetComponent<KeyboardShipController>();
                    if (playerController == null) ships[i].gameObject.AddComponent<KeyboardShipController>();
                }

                Vector2 position = new Vector2();

                if (nShips % 2 == 0 || i < 2) position.x = 5f * (i % 2 == 0 ? -1 : 1);
                else if (i % 2 != 0) position.x = 0;

                if (nShips < 2) position.y = 0;
                else position.y = 2.5f * (i < 2 ? 1 : -1);

                ships[i].spawnPosition = position;
            }
        }

        public void StartGame ()
        {
            ClearShips();
            if (noInit) InitGame(defaultNShips, defaultNPlayers);
            Ship.onDeath += Ship_OnDeath;
            foreach (Ship ship in ships) ship.Init();
            if (onGameStart != null) onGameStart();
        }

        public void EndGame()
        {
            Ship.onDeath -= Ship_OnDeath;
            if (onGameOver != null)
            {
                if (Ship.list.Count == 1) onGameOver(Ship.list[0]);
                else onGameOver(null);
            }
        }

        public void ClearShips ()
        {
            for (int i = Ship.list.Count - 1; i >= 0; i--) Ship.list[i].Kill();
        }

        private void Ship_OnDeath(Ship killerShip, Ship killedShip)
        {
            if (Ship.list.Count <= 1) EndGame();
        }

        private void OnDestroy ()
        {
            if (instance == this) instance = null;
            onGameStart = null;
            onGameOver = null;
        }

    }
}