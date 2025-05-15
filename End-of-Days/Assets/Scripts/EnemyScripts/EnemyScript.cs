using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyScript : MonoBehaviour, IDamageable
{

    private float unitHealth;
    public float unitMaxHealth;
    [SerializeField] GameObject deathParticles;
    Animator animator;
    UnityEngine.AI.NavMeshAgent navMeshAgent;

    public HealthTracker healthTracker;
    // Start is called before the first frame update
    void Awake()
    {
        unitHealth = unitMaxHealth;
        UpdateHealthUI();

        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.SetDestination(new Vector3(0, 0, 0));


    }

    private void OnDestroy()
    {

    }

    private void UpdateHealthUI()
    {
        healthTracker.UpdateSliderValue(unitHealth, unitMaxHealth);

        if (unitHealth <= 0){

            GameObject tempParticles = Instantiate(deathParticles.gameObject, this.gameObject.transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(tempParticles, 5.0f);
            ResourceManager.Instance.IncreaseResource(ResourceManager.ResourceType.Credits, 50);
            SoundManager.Instance.PlayZombieDeathSound();

            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damageToInflict) 
    {  
        healthTracker.gameObject.SetActive(true);
        unitHealth -= damageToInflict;
        UpdateHealthUI();
    }

    private void Update()
    {
         if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
