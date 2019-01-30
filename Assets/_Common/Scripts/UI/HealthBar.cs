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

        [Range(0, 1)] [SerializeField] private float _value = 1;
        [SerializeField] private Image fill;
        [SerializeField] private Slider slider;

        public Gradient gradient;
        public float value
        {
            get { return _value; }
            set
            {
                slider.value = value;
                fill.color = gradient.Evaluate(value);
                _value = value;
            }
        }
        
    }
}