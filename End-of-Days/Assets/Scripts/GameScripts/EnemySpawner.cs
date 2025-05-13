using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace DenisAlipov.GameTime
{
    public class EnemySpawner : MonoBehaviour
    {
        private int lastActionHour;
        private int spawnHour = 10;
        private int stopSpawnHour = 11;

        [SerializeField]private GameObject[] spawnLocations = new GameObject[5]; 

        [SerializeField]private GameObject enemyPrefab;
        
        // Start is called before the first frame update
        void Start()
        {
            GameTime.AddOnTimeChanged(SpawnEnemies);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void SpawnEnemies(GameDayTime newTime)
        {
            int newTimeHours = newTime.Hours;
            if (lastActionHour == newTimeHours)
            {
                return;
            }

            if (newTimeHours == spawnHour)
            {
                // Debug.Log("Spawning Enemies");
                float numOfEnemies = Mathf.Round(newTime.Days * 2);
                StartCoroutine(SpawnDelay(numOfEnemies));
                lastActionHour = newTimeHours;
                return;
            }
            if (newTimeHours == stopSpawnHour)
            {
                lastActionHour = newTimeHours;
                return;
            }
        }

        private IEnumerator SpawnDelay(float numEnemies)
        {
        for(int i = 0; i < numEnemies; i++)
        {
            Spawn();
            yield return new WaitForSeconds(1);
        }
        }

        private void Spawn() 
        {
            int randomInt = Random.Range(0, spawnLocations.Length);
            Instantiate(enemyPrefab, spawnLocations[randomInt].transform.position, Quaternion.identity);
        }
        }
}
