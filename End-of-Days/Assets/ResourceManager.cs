using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

    private int credits = 10000;
    private int food = 100;

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateBuildingChanged(BuildingType buildingType, bool isNew, Vector3 position)
    {
        if (isNew)
        {
            allExistingBuildings.Add(buildingType);
            buildingLocation = position;
            SoundManager.Instance.PlayConstructBuildingSound();
        }
        else
        {
            placementSystem.RemovePlacmentData(position);
            allExistingBuildings.Remove(buildingType);
        }
        
        
        CheckHQ?.Invoke();

        OnBuildingChanged?.Invoke();
    }
    
    public event Action OnResourceChanged;
    public event Action OnBuildingChanged;
    public event Action CheckHQ;
    public Vector3 buildingLocation;
    public TextMeshProUGUI creditsUI;
    public TextMeshProUGUI foodUI;
    public List<BuildingType> allExistingBuildings;
    public PlacementSystem placementSystem;
    public enum ResourceType 
    {
        Credits,
        Food
    }

    public void IncreaseResource(ResourceType resource, int amountToIncrease) 
    {
        switch (resource)
        {
            case ResourceType.Credits:
                credits += amountToIncrease;
                break;
            case ResourceType.Food:
                food += amountToIncrease;
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
            case ResourceType.Food:
                food -= amountToDecrease;
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

    public void AwardPlayer()
    {
        IncreaseResource(ResourceType.Credits, 50);
    }

    public void BuildMeleeUnit()
    {
        DecreaseResource(ResourceType.Food, 50);
    }
    private void UpdateUI()
    {
        creditsUI.text = $"{credits}";
        foodUI.text = $"{food}";
    }

    public int GetCredits()
    {
        return credits;
    }

    public int GetFood()
    {
        return food;
    }

    internal int GetResourceAmount(ResourceType resource)
    {
        switch (resource)
        {
            case ResourceType.Credits:
                return credits;
            case ResourceType.Food:
                return food;
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


