///-----------------------------------------------------------------
/// Author : Adrien HEISCH
/// Date : 11/01/2019 11:03
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.AdrienHeisch.NotSpaceWar.ShipControllers {
	public class PlayerShipController : ShipController
	{

        public void Update () //TODO Wrap strings in something cleaner (that allows multiple players)
        {
            left = Input.GetAxis("P1_Horizontal") < 0;
            right = Input.GetAxis("P1_Horizontal") > 0;
            up = Input.GetButton("P1_Thrust");
            boost = Input.GetButton("P1_Boost");
            fire = Input.GetButton("P1_Fire");
        }

	}
}