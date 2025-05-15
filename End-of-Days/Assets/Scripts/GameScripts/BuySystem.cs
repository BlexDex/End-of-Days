using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySystem : MonoBehaviour
{
    public GameObject buildingPanel;

    public PlacementSystem placementSystem;
    // Start is called before the first frame update
    void Start()
    {
        buildingPanel.SetActive(true);
    }

    private void BuildingCategorySelected()
    {
        buildingPanel.SetActive(true);
    }

    private void UnitCategorySelected()
    {
        buildingPanel.SetActive(false);
    }
}
