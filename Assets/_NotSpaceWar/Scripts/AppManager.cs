///-----------------------------------------------------------------
/// Author : Adrien HEISCH
/// Date : 29/01/2019 02:10
///-----------------------------------------------------------------

using System.Collections;
using Com.AdrienHeisch.NotSpaceWar.UI;
using UnityEngine;

namespace Com.AdrienHeisch.NotSpaceWar {
	public class AppManager : MonoBehaviour
	{

        [SerializeField] private AppState initialState = AppState.TitleCard;

        public AppState state { get; private set; }

        private void Start()
        {
            StartCoroutine(SetInitialStateCoroutine());
            LevelManager.instance.onGameOver += LevelManager_OnGameOver;
        }

        private IEnumerator SetInitialStateCoroutine ()
        {
            yield return new WaitForEndOfFrame();
            SetState(initialState);
        }

        public void SetState (AppState state)
        {
            switch (state)
            {
                case AppState.TitleCard:
                    LevelManager.instance.EndGame();
                    LevelManager.instance.ClearShips();
                    break;
                case AppState.GameSettings: //TODO Game Settings
                    break;
                case AppState.InGame:
                    LevelManager.instance.StartGame();
                    break;
                case AppState.GameOver:
                    break;
            }

            UIManager.instance.SetState(state);

            this.state = state;
        }

        public void SetState(AppStateWrapper wrapper)
        {
            SetState(wrapper.state);
        }

        private void LevelManager_OnGameOver(Ship obj)
        {
            SetState(AppState.GameOver);
        }

        private void OnDestroy()
        {
            if (LevelManager.instance != null) LevelManager.instance.onGameOver += LevelManager_OnGameOver;
        }

    }
}