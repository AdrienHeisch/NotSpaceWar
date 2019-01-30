///-----------------------------------------------------------------
/// Author : Adrien HEISCH
/// Date : 25/01/2019 14:13
///-----------------------------------------------------------------

using Com.AdrienHeisch.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Com.AdrienHeisch.NotSpaceWar.UI
{
    public class ShipHUD : MonoBehaviour //TODO This whole system is poorly designed, should use a shared state with ships instead of direct reference
	{

        [SerializeField] private Ship ship;
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private Text textDisplay;
        
        private ShipScore shipInfo; //TODO State shouldn't be stored here

        private void Start ()
        {
            healthBar.gradient.SetKeys(new[] { new GradientColorKey(ship.color, 0) }, new[] { new GradientAlphaKey(1, 0) });

            Bullet.onHit += Bullet_OnHit;
            Ship.onDeath += Ship_OnDeath;
            LevelManager.instance.onGameStart += LevelManager_OnGameStart;
            LevelManager.instance.onGameOver += LevelManager_OnGameOver;
        }

        //TODO Change alpha when a ship goes behind ui
        //private void Update()
        //{
            //foreach (Ship ship in Ship.list)
            //{
            //    Ray ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(ship.transform.position));
            //    GetComponent<CanvasRenderer>().
            //}
        //}

        private void UpdateDisplay ()
        {
            healthBar.value = ship.GetHealthFraction();
            textDisplay.text = shipInfo.wins + " wins, " + shipInfo.kills + " kills, " + shipInfo.hits + " hits"; //TODO Templates
        }

        private void Bullet_OnHit (Ship damagingShip, Ship damagedShip, int damage)
        {
            if (damagingShip == ship) shipInfo.hits += damage;
            UpdateDisplay();
        }

        private void Ship_OnDeath (Ship killerShip, Ship killedShip)
        {
            if (killerShip == ship) shipInfo.kills++;
            UpdateDisplay();
        }

        private void LevelManager_OnGameStart()
        {
            UpdateDisplay();
        }

        private void LevelManager_OnGameOver(Ship winner)
        {
            if (winner == ship) shipInfo.wins++;
            UpdateDisplay();
        }

        private void OnDestroy()
        {
            Bullet.onHit -= Bullet_OnHit;
            Ship.onDeath -= Ship_OnDeath;
            if (LevelManager.instance != null)
            {
                LevelManager.instance.onGameStart -= LevelManager_OnGameStart;
                LevelManager.instance.onGameOver -= LevelManager_OnGameOver;
            }
        }

        private void OnValidate ()
        {
            if (!gameObject.activeInHierarchy) return;
            healthBar.gradient.SetKeys(new[] { new GradientColorKey(ship.color, 0) }, new[] { new GradientAlphaKey(1, 0) });
            healthBar.value = 1;
        }

    }
}