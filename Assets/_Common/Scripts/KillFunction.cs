///-----------------------------------------------------------------
/// Author : Adrien HEISCH
/// Date : 29/01/2019 16:05
///-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.Events;

namespace Com.AdrienHeisch {
	public class KillFunction : MonoBehaviour
	{

        public void Kill() { Destroy(gameObject); }
		
	}
}