///-----------------------------------------------------------------
/// Author : Adrien HEISCH
/// Date : 25/01/2019 22:20
///-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace Com.AdrienHeisch.NotSpaceWar.UI
{
    [RequireComponent(typeof(Text))]
	public class WinnerDisplay : MonoBehaviour
	{

        [Tooltip("[winner] --> P1, P2...")]
        [SerializeField] private string winnerText = "Winner : [winner]";
        [SerializeField] private string drawText = "Draw!";

        private Text textDisplay;
	
		private void Start ()
		{
            textDisplay = GetComponent<Text>();
		}

        public void SetWinner (string winnerName)
        {
            textDisplay.text = winnerText.Replace("[winner]", winnerName);
        }

        public void SetDraw ()
        {
            textDisplay.text = drawText;
        }
		
	}
}