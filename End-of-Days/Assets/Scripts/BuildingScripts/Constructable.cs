using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Constructable : MonoBehaviour, IDamageable
{   

    private float constHealth;
    public float constMaxHealth;
    public HealthTracker healthTracker;
    [SerializeField] GameObject destroyParticles;
    public bool isEnemy = true;

    NavMeshObstacle obstacle;
    public BuildingType buildingType;
    public Vector3 buildPosition;


    private void Start()
    {
        constHealth = constMaxHealth;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        healthTracker.UpdateSliderValue(constHealth, constMaxHealth);

        if (constHealth <= 0)
        {
            ResourceManager.Instance.UpdateBuildingChanged(buildingType, false, buildPosition);

            SoundManager.Instance.PlayDestroyBuildingSound();

            GameObject tempParticles = Instantiate(destroyParticles.gameObject, this.gameObject.transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(tempParticles, 5.0f);

            Destroy(gameObject);

            if (buildingType.ToString() == "HQ")
            {
                Debug.Log("Game Over");
                InGameMenu.Instance.LoseGame();
            }
        }
    }

    private void OnDestroy()
    {
        if (!isEnemy)
        {
            if (constHealth > 0 && buildPosition != Vector3.zero)
            {
                ResourceManager.Instance.SellBuilding(buildingType);
            }
        }
    }
    public void TakeDamage(int damage)
    {
        constHealth -= damage;
        UpdateHealthUI();
    }

    public void ConstructableWasPlaced()
    {   
        healthTracker.gameObject.SetActive(true);
        
        ActivateObstacle();
    }

    private void ActivateObstacle()
    {
        obstacle = GetComponentInChildren<NavMeshObstacle>();
        obstacle.enabled = true;
    }
}
