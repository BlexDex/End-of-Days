using System.Collections;
using System.Collections.Generic;
using DenisAlipov.GameTime;
using UnityEngine;

public class Cemetary : MonoBehaviour, IDamageable
{

    public enum SpawnerNum
    {
        Spawner,
        Spawner_1,
        Spawner_2,
        Spawner_3
    }

    [SerializeField] public SpawnerNum spawnerNum;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] GameObject destroyParticles;
    private float buildingHealth;
    public float buildingMaxHealth;
    public HealthTracker healthTracker;
    // Start is called before the first frame update
    void Start()
    {
        buildingHealth = buildingMaxHealth;
        UpdateHealthUI();
        enemySpawner = GameObject.FindWithTag("EnemySpawner").GetComponent<EnemySpawner>();
    }

    private void UpdateHealthUI()
    {
    healthTracker.UpdateSliderValue(buildingHealth, buildingMaxHealth);

    if (buildingHealth <= 0){
        GameObject tempParticles = Instantiate(destroyParticles.gameObject, this.gameObject.transform.position, Quaternion.Euler(-90, 0, 0));
        Destroy(tempParticles, 5.0f);
        ResourceManager.Instance.IncreaseResource(ResourceManager.ResourceType.Credits, 200);
        Destroy(gameObject);
    }
    }

    void OnDestroy()
    {
        SoundManager.Instance.PlayDestroyBuildingSound();
        enemySpawner.DestroySpawner(spawnerNum.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageToInflict) 
    {
        buildingHealth -= damageToInflict;
        UpdateHealthUI();
    }
}
