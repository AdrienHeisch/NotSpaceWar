///-----------------------------------------------------------------
/// Author : Adrien HEISCH
/// Date : 25/01/2019 21:42
///-----------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Com.AdrienHeisch.NotSpaceWar.UI
{
    public class GameOverScreen : MonoBehaviour
    {

        [SerializeField] private WinnerDisplay winnerDisplay;

        [SerializeField] private UnityEvent onRestart = new UnityEvent();
        [SerializeField] private UnityEvent onReturn = new UnityEvent();

        public void SetWinner(Ship winner)
        {
            if (winner != null) winnerDisplay.SetWinner(winner.name);
            else winnerDisplay.SetDraw();
        }

        private void Update()
        {
            if (Input.GetButton("Submit")) onRestart.Invoke();
            if (Input.GetButton("Cancel")) onReturn.Invoke();
        }

    }
}