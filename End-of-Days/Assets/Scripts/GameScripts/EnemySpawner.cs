using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
namespace DenisAlipov.GameTime
{
    public class EnemySpawner : MonoBehaviour
    {
        private int lastActionHour;
        private int spawnHour = 21;
        private int stopSpawnHour = 22;

        public List<GameObject> Spawners;

        [SerializeField] private GameObject[] enemies;
        [SerializeField]private GameObject brutePrefab;

        // Start is called before the first frame update
        void Start()
        {
            GameTime.AddOnTimeChanged(SpawnEnemies);
            Debug.Log(Spawners.Find(x => x.name == "Spawner"));
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void SpawnEnemies(GameDayTime newTime)
        {
            int newTimeHours = newTime.Hours;
            int newTimeDays = newTime.Days;
            if (lastActionHour == newTimeHours)
            {
                return;
            }

            if (newTimeHours == spawnHour)
            {
                // Debug.Log("Spawning Enemies");
                SoundManager.Instance.PlaySpawnSound();
                if (newTimeDays % 5 == 0)
                {
                    int randomInt = UnityEngine.Random.Range(0, Spawners.Count);
                    Instantiate(brutePrefab, Spawners[randomInt].transform.position, Quaternion.identity);
                }
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

        public void DestroySpawner(String spawnerNum)
        {
            GameObject Num = Spawners.Find(x => x.name == spawnerNum);
            Spawners.Remove(Num);

            if (Spawners.Count == 0)
            {
                Debug.Log("You Win");
                InGameMenu.Instance.WonGame();
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
            int randomInt = UnityEngine.Random.Range(0, Spawners.Count);
            int randomEnemy = UnityEngine.Random.Range(0, enemies.Length);
            Instantiate(enemies[randomEnemy], Spawners[randomInt].transform.position, Quaternion.identity);
        }
        }
}
