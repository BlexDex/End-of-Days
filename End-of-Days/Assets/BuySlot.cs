using System;
using UnityEngine;
using UnityEngine.UI;

public class BuySlot : MonoBehaviour
{
    public Sprite availableSprite;
    public Sprite unavailabeSprite;
    private bool isAvailable;

    public BuySystem buySystem;

    public int databaseItemID;

    private void Start()
    {
        ResourceManager.Instance.OnResourceChanged += HandleResourceChange;
        HandleResourceChange();

        ResourceManager.Instance.OnBuildingChanged += HandleBuildingChange;
        HandleBuildingChange();
    }

    public void ClickedOnSlot()
    {
        if (isAvailable)
        {
            buySystem.placementSystem.StartPlacement(databaseItemID);
        }
    }

    private void UpdateSprite()
    {
        if (isAvailable)
        {
            GetComponent<Image>().sprite = availableSprite;
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Image>().sprite = unavailabeSprite;
            GetComponent<Button>().interactable = false;
        }
    }

    private void HandleResourceChange()
    {
        ObjectData objectData = DatabaseManager.Instance.databaseOS.objectsData[databaseItemID];

        bool requirementMet = true;

        foreach (BuildRequirement req in objectData.resourceRequirements)
        {
            if (ResourceManager.Instance.GetResourceAmount(req.resource) < req.amount)
            {
                requirementMet = false;
                break;
            }
        }

        isAvailable = requirementMet;

        UpdateSprite();
    }

    private void HandleBuildingChange()
    {
        ObjectData objectData = DatabaseManager.Instance.databaseOS.objectsData[databaseItemID];

        foreach (BuildingType depedency in objectData.buildDependecies)
        {
            if (depedency == BuildingType.None)
            {
                gameObject.SetActive(true);
                return;
            }

            if (ResourceManager.Instance.allExistingBuildings.Contains(depedency) == false)
            {
                gameObject.SetActive(false);
                return;
            }
        }

        gameObject.SetActive(true);
    }

}