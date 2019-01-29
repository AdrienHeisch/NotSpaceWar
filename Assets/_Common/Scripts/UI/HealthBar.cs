///-----------------------------------------------------------------
/// Author : Adrien HEISCH
/// Date : 25/01/2019 01:56
///-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace Com.AdrienHeisch.UI
{
    [ExecuteInEditMode]
	public class HealthBar : MonoBehaviour
	{

        [Range(0, 1)] public float value = 1;
        [SerializeField] private Image fill;
        [SerializeField] private Slider slider;

        public Gradient gradient;

        private void Update()
        {
            slider.value = value;
            fill.color = gradient.Evaluate(value);
        }

    }
}