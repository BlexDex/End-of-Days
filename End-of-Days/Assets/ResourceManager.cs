using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance {get; set;}

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private int credits = 500;

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateBuildingChanged(BuildingType buildingType, bool isNew, Vector3 position)
    {
        if (isNew)
        {
            allExistingBuildings.Add(buildingType);

            SoundManager.Instance.PlayConstructBuildingSound();
        }
        else
        {
            placementSystem.RemovePlacmentData(position);
            allExistingBuildings.Remove(buildingType);
        }

        OnBuildingChanged?.Invoke();
    }
    
    public event Action OnResourceChanged;
    public event Action OnBuildingChanged;

    public TextMeshProUGUI creditsUI;

    public List<BuildingType> allExistingBuildings;
    public PlacementSystem placementSystem;
    public enum ResourceType 
    {
        Credits
    }

    public void IncreaseResource(ResourceType resource, int amountToIncrease) 
    {
        switch (resource)
        {
            case ResourceType.Credits:
                credits += amountToIncrease;
                break;
            default:
                break;
        }
        OnResourceChanged?.Invoke();
    }

    public void DecreaseResource(ResourceType resource, int amountToDecrease) 
    {
        switch (resource)
        {
            case ResourceType.Credits:
                credits -= amountToDecrease;
                break;
            default:
                break;
        }
        OnResourceChanged?.Invoke();
    }

    private void OnEnable()
    {
        OnResourceChanged += UpdateUI;
    }
    private void OnDisable()
    {
        OnResourceChanged -= UpdateUI;
    }

    public void SellBuilding(BuildingType buildingType)
    {
        SoundManager.Instance.PlaySellingBuildinSound();

        var sellingPrice = 0;

        foreach (ObjectData obj in DatabaseManager.Instance.databaseOS.objectsData)
        {
            if (obj.thisBuildingType == buildingType)
            {
                foreach (BuildRequirement req in obj.resourceRequirements)
                {
                    if (req.resource == ResourceType.Credits)
                    {
                        sellingPrice = req.amount;
                    }
                }
            }
        }

        int amountToReturn = (int)(sellingPrice * 0.5f);

        IncreaseResource(ResourceType.Credits, amountToReturn);
    }
    private void UpdateUI()
    {
        creditsUI.text = $"{credits}";
    }

    public int GetCredits()
    {
        return credits;
    }

    internal int GetResourceAmount(ResourceType resource)
    {
        switch (resource)
        {
            case ResourceType.Credits:
                return credits;
            default:
                break;
        }
        return 0;
    }

    internal void DecreaseResourceBaseOnReq(ObjectData objectData)
    {
        foreach (BuildRequirement req in objectData.resourceRequirements)
        {
            DecreaseResource(req.resource, req.amount);
        }
    }
}


