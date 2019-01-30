///-----------------------------------------------------------------
/// Author : Adrien HEISCH
/// Date : 11/01/2019 10:33
///-----------------------------------------------------------------

using Com.AdrienHeisch.NotSpaceWar.ShipControllers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.AdrienHeisch.NotSpaceWar
{
    [RequireComponent(typeof(ShipController), typeof(SpriteRenderer))]
    public class Ship : MonoBehaviour
    {

        public static List<Ship> list = new List<Ship>();
        public static event DeathEvent onDeath;

        public Color color;
        [SerializeField] private int MAX_HEALTH = 5;
        [SerializeField] private float MAX_TURNING_SPEED = 180f;
        [SerializeField] private float TURNING_ACCELERATION_VALUE = 900f;
        [SerializeField] private float BASE_MAX_SPEED = 4.166667f;
        [SerializeField] private float BASE_ACCELERATION = 10f;
        [SerializeField] private float SELF_BRAKE = 2.5f;
        [SerializeField] private float BOOST_MULT = 2f;
        [SerializeField] private float SHOOT_COOLDOWN = 0.3f;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject explosionPrefab;

        [NonSerialized] public Vector2 velocity = new Vector2();
        [NonSerialized] public Vector2 acceleration = new Vector2();
        [NonSerialized] public Vector2 spawnPosition;
        private int health;
        private float turningSpeed = 0;
        private float shootTimer = 0;
        private ShipController controller;
        private SpriteRenderer spriteRenderer;

        private void OnValidate ()
        {
            GetComponent<SpriteRenderer>().color = color;
        }
        
        private void Awake ()
        {
            health = MAX_HEALTH;
        }

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            controller = GetComponent<PlayerShipController>();
            if (controller == null) controller = GetComponent<AIShipController>();

            spawnPosition = transform.position;
        }

        public void Init ()
        {
            list.Add(this);
            health = MAX_HEALTH;
            shootTimer = 0;
            transform.position = spawnPosition;
            transform.right = -transform.position;
            gameObject.SetActive(true);
            Bullet.onHit += Bullet_OnHit;
        }

        private void Update ()
        {
            float lAccelerationValue = BASE_ACCELERATION * (controller.boost ? BOOST_MULT : 1);
            float lMaxSpeed = BASE_MAX_SPEED * (controller.boost ? BOOST_MULT : 1);
            float lTurningAcceleration = 0;

            if (controller.fire && shootTimer <= 0) Shoot();
            else if (shootTimer > 0) shootTimer -= Time.deltaTime;
            
            lTurningAcceleration = ((controller.left ? 1 : 0) - (controller.right ? 1 : 0)) * TURNING_ACCELERATION_VALUE;

            if (lTurningAcceleration == 0)
            {
                if (turningSpeed > 0)
                {
                    lTurningAcceleration = -TURNING_ACCELERATION_VALUE;
                }
                else if (turningSpeed < 0)
                {
                    lTurningAcceleration = TURNING_ACCELERATION_VALUE;
                }
            }
            
            turningSpeed += lTurningAcceleration * Time.deltaTime;

            if (Mathf.Abs(turningSpeed) < 1) turningSpeed = 0;
            else turningSpeed = Mathf.Clamp(turningSpeed, -MAX_TURNING_SPEED, MAX_TURNING_SPEED);

            transform.Rotate(Vector3.forward * (turningSpeed * Time.deltaTime));

            if (controller.up)
            {
                acceleration = transform.right * lAccelerationValue;
            }
            else if (velocity.magnitude > SELF_BRAKE * Time.deltaTime)
            {
                acceleration = -velocity.normalized * SELF_BRAKE;
            }
            else acceleration.Set(0, 0);

            velocity += acceleration * Time.deltaTime;
            
            if (velocity.magnitude >= lMaxSpeed)
            {
                velocity -= velocity.normalized * ((SELF_BRAKE + lAccelerationValue) * Time.deltaTime);
            }
            
            Vector2 lFrameVelocity = velocity * Time.deltaTime;
            Vector2 lBounds = new Vector2(spriteRenderer.bounds.size.x, spriteRenderer.bounds.size.y) / 2;
            float lHalfScreenWidth = Camera.main.orthographicSize * Camera.main.aspect;
            
            if ((transform.position.x - lFrameVelocity.x <= -lHalfScreenWidth + lBounds.x / 2 && velocity.x < 0) || (transform.position.x + lFrameVelocity.x >= lHalfScreenWidth - lBounds.x / 2 && velocity.x > 0)) velocity.x *= -1;
            if ((transform.position.y - lFrameVelocity.y <= -Camera.main.orthographicSize + lBounds.y / 2 && velocity.y < 0) || (transform.position.y + lFrameVelocity.y >= Camera.main.orthographicSize - lBounds.y / 2 && velocity.y > 0)) velocity.y *= -1;

            transform.Translate(velocity * Time.deltaTime, Space.World);
        }

        private void Shoot()
        {
            Bullet lBullet = Instantiate(bulletPrefab, transform.position, transform.rotation).GetComponent<Bullet>();
            lBullet.parentShip = this;
            lBullet.color = color;
            shootTimer = SHOOT_COOLDOWN;
        }

        private void Bullet_OnHit(Ship damagingShip, Ship damagedShip, int damage)
        {
            if (damagedShip == this)
            {
                health -= damage;
                if (health <= 0)
                {
                    Kill();
                    onDeath(damagingShip, this);
                }
            }
        }

        public float GetHealthFraction () { return (float)health / MAX_HEALTH; }

        public void Kill ()
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            list.Remove(this);
            velocity.Set(0, 0);
            acceleration.Set(0, 0);
            Bullet.onHit -= Bullet_OnHit;
            gameObject.SetActive(false);
        }

    }

    public delegate void DeathEvent(Ship killerShip, Ship killedShip);
}