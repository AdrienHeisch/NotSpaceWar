///-----------------------------------------------------------------
/// Author : Adrien HEISCH
/// Date : 25/01/2019 22:32
///-----------------------------------------------------------------

using System;
using UnityEngine;

namespace Com.AdrienHeisch.NotSpaceWar.UI
{
	public class UIManager : MonoBehaviour
	{

        public static UIManager instance { get; private set; }
        
        [SerializeField] private GameObject titleCard;
        [SerializeField] private GameObject gameSettingsScreen;
        [SerializeField] private GameOverScreen gameOverScreen;
        [SerializeField] private GameObject hud;

        private void Awake()
        {
            instance = this;
        }

        private void Start ()
		{
            LevelManager.instance.onGameOver += LevelManager_OnGameOver;
		}

        private void CloseScreens ()
        {
            titleCard.gameObject.SetActive(false);
            gameSettingsScreen.gameObject.SetActive(false);
            hud.gameObject.SetActive(false);
            gameOverScreen.gameObject.SetActive(false);
        }

        public void SetState (AppState state)
        {
            CloseScreens();

            switch (state)
            {
                case AppState.TitleCard:
                    titleCard.gameObject.SetActive(true);
                    break;
                case AppState.GameSettings:
                    gameSettingsScreen.gameObject.SetActive(true);
                    break;
                case AppState.InGame:
                    hud.gameObject.SetActive(true);
                    break;
                case AppState.GameOver:
                    hud.gameObject.SetActive(true);
                    gameOverScreen.gameObject.SetActive(true);
                    break;
            }
        }

        private void LevelManager_OnGameOver(Ship winner)
        {
            gameOverScreen.SetWinner(winner);
        }

        private void OnDestroy()
        {
            if (instance == this) instance = null;
            if (LevelManager.instance != null)
            {
                LevelManager.instance.onGameOver -= LevelManager_OnGameOver;
            }
        }
		
	}
}