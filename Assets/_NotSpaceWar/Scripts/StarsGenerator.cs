///-----------------------------------------------------------------
/// Author : Adrien HEISCH
/// Date : 25/01/2019 16:15
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.AdrienHeisch.NotSpaceWar
{
	public class StarsGenerator : MonoBehaviour
	{

        [SerializeField] private GameObject starPrefab;
        [SerializeField] private uint nStars = 20;
        [SerializeField] private Vector2 scaleRange = new Vector2(0.1f, 1f);
	    
		private void Start ()
		{
            float lHalfScreenWidth = Camera.main.orthographicSize * Camera.main.aspect;
            
            for (uint i = 0; i < nStars; i++)
            {
                Transform star = Instantiate(starPrefab).transform;
                star.position = new Vector2(lHalfScreenWidth * (Random.value * 2 - 1), Camera.main.orthographicSize * (Random.value * 2 - 1));
                star.Rotate(Vector3.forward * Random.Range(0, 360));
                
                float scale = Random.Range(scaleRange.x, scaleRange.y);
                star.localScale = new Vector2(scale, scale);
                star.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, Mathf.InverseLerp(scaleRange.x, scaleRange.y, scale));
            }
		}
		
	}
}