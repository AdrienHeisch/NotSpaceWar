///-----------------------------------------------------------------
/// Author : Adrien HEISCH
/// Date : 11/01/2019 16:07
///-----------------------------------------------------------------

using System;
using UnityEngine;

namespace Com.AdrienHeisch.NotSpaceWar
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Bullet : MonoBehaviour
    {

        public static float SPEED = 10f;
        public static event DamageEvent onHit;
        
        [SerializeField] private int DAMAGE = 1;
        [NonSerialized] public Ship parentShip;
        private SpriteRenderer spriteRenderer;

        private Color _color;
        public Color color
        {
            get { return _color; }
            set
            {
                _color = value;
                if (spriteRenderer != null) spriteRenderer.color = value;
            }
        }

        private void Start ()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = color;
        }

        private void Update ()
		{
            transform.Translate(transform.right * SPEED * Time.deltaTime, Space.World);
            if (!spriteRenderer.isVisible) Destroy(gameObject);
        }

        private void OnTriggerEnter2D (Collider2D collision)
        {
            Ship lShip = collision.GetComponent<Ship>();
            if (lShip != null && lShip != parentShip)
            {
                if (onHit != null) onHit(parentShip, lShip, DAMAGE);
                Destroy(gameObject);
            }
        }

    }

    public delegate void DamageEvent (Ship damagingShip, Ship damagedShip, int damage);
}