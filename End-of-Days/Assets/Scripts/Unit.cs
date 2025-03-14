using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{

    private float unitHealth;
    public float unitMaxHealth;

    public HealthTracker healthTracker;
    // Start is called before the first frame update
    void Start()
    {
        UnitSelectionManager.Instance.allUnitsList.Add(this.gameObject);
        
        unitHealth = unitMaxHealth;
        UpdateHealthUI();
    }

    private void OnDestroy()
    {
        UnitSelectionManager.Instance.allUnitsList.Remove(gameObject);
    }

    private void UpdateHealthUI()
    {
        healthTracker.UpdateSliderValue(unitHealth, unitMaxHealth);

        if (unitHealth <= 0){
            Destroy(gameObject);
        }
    }

    internal void TakeDamage(int damageToInflict) 
    {
        unitHealth -= damageToInflict;
        UpdateHealthUI();
    }
}
