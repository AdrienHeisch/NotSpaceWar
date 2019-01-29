///-----------------------------------------------------------------
/// Author : Adrien HEISCH
/// Date : 11/01/2019 11:03
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.AdrienHeisch.NotSpaceWar.ShipControllers {
	public class KeyboardShipController : ShipController
	{

        public void Update () //TODO Serialize keys
        {
            left = Input.GetKey(KeyCode.LeftArrow);
            right = Input.GetKey(KeyCode.RightArrow);
            up = Input.GetKey(KeyCode.UpArrow);
            down = Input.GetKey(KeyCode.DownArrow);
            boost = Input.GetKey(KeyCode.A);
            shoot = Input.GetKey(KeyCode.Z);
        }

	}
}