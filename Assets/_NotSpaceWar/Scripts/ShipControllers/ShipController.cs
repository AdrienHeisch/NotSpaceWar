///-----------------------------------------------------------------
/// Author : Adrien HEISCH
/// Date : 11/01/2019 11:02
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.AdrienHeisch.NotSpaceWar.ShipControllers
{
    public abstract class ShipController : MonoBehaviour
	{

        public bool left { get; protected set; }
        public bool right { get; protected set; }
        public bool up { get; protected set; }
        public bool boost { get; protected set; }
        public bool fire { get; protected set; }

        private void Awake ()
        {
            ResetValues();
        }

        protected void ResetValues ()
        {
            left = false;
            right = false;
            up = false;
            boost = false;
            fire = false;
        }

    }
}