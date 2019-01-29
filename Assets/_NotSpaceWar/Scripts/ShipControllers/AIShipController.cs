///-----------------------------------------------------------------
/// Author : Adrien HEISCH
/// Date : 11/01/2019 14:33
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.AdrienHeisch.NotSpaceWar.ShipControllers
{
    [RequireComponent(typeof(Ship))]
    public class AIShipController : ShipController
	{

        [SerializeField] private float DISTANCE_BEFORE_ACCELERATION = 5f;
        [SerializeField] private float AIM_ANGLE = 5f;
        [SerializeField] private float ANTI_CIRCLE_MAGIC_FACTOR = 100f;

        private Ship controlledShip;
        private Ship target;

        private void Start()
        {
            controlledShip = GetComponent<Ship>();
        }

        public void Update ()
        {
            if (Ship.list.Count > 1)
            {
                Ship lShip;
                float lDistanceToTarget = -1;
                Vector2 lFutureTargetPosition = new Vector2();

                for (int i = 0; i < Ship.list.Count; i++)
                {
                    lShip = Ship.list[i];
                    if (lShip == controlledShip) continue;

                    float lTimeBeforeImpact = Time.deltaTime * Vector2.Distance(transform.position, lShip.transform.position) / Bullet.SPEED;

                    //This line is used to keep AI controlled ships to turn around each other indefinitely when there are only two of them left.
                    if (Ship.list.Count == 2 && Vector2.Dot(target.velocity, target.acceleration) < 0.1) lTimeBeforeImpact *= ANTI_CIRCLE_MAGIC_FACTOR;

                    Vector2 lFutureShipPosition = (Vector2)lShip.transform.position + lShip.velocity * lTimeBeforeImpact + lShip.acceleration * (lTimeBeforeImpact * lTimeBeforeImpact);
                    float lDistance = Vector2.Distance(transform.position, lFutureShipPosition);
                    if (lDistance < lDistanceToTarget || lDistanceToTarget < 0)
                    {
                        lDistanceToTarget = lDistance;
                        lFutureTargetPosition = lFutureShipPosition;
                        target = lShip;
                    }
                }
                
                float lAngleDelta = Mathf.DeltaAngle(Mathf.Atan2(lFutureTargetPosition.y - transform.position.y, lFutureTargetPosition.x - transform.position.x) * Mathf.Rad2Deg, transform.eulerAngles.z);
                left = !(right = lAngleDelta > 0);

                if (lAngleDelta > -AIM_ANGLE && lAngleDelta < AIM_ANGLE)
                {
                    boost = lDistanceToTarget > DISTANCE_BEFORE_ACCELERATION;
                    shoot = true;
                }
                else shoot = false;

                up = true;
            }
            else ResetValues();
        }

    }
}