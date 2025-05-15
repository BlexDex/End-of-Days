using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour, IDamageable
{

    public enum UnitType
    {
        Human,
        Dog
    }
    [SerializeField] public UnitType type;

    private float unitHealth;
    public float unitMaxHealth;
    [SerializeField] GameObject deathParticles;

    Animator animator;
    UnityEngine.AI.NavMeshAgent navMeshAgent;

    public HealthTracker healthTracker;
    // Start is called before the first frame update
    void Start()
    {
        UnitSelectionManager.Instance.allUnitsList.Add(this.gameObject);
        
        unitHealth = unitMaxHealth;
        UpdateHealthUI();

        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private void OnDestroy()
    {
        UnitSelectionManager.Instance.allUnitsList.Remove(gameObject);
        UnitSelectionManager.Instance.unitsSelected.Remove(gameObject);
    }

    private void UpdateHealthUI()
    {
        healthTracker.UpdateSliderValue(unitHealth, unitMaxHealth);

        if (unitHealth <= 0){
            GameObject tempParticles = Instantiate(deathParticles.gameObject, this.gameObject.transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(tempParticles, 5.0f);

            switch (type)
            {
                case UnitType.Human:
                    SoundManager.Instance.PlayUnitDeathSound();
                    break;
                case UnitType.Dog:
                    SoundManager.Instance.PlayDogDeathSound();
                    break;
                default:
                    break;
            }

            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damageToInflict) 
    {
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
