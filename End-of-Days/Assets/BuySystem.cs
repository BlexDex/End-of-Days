using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySystem : MonoBehaviour
{
    public GameObject buildingPanel;
    public GameObject unitPanel;

    public Button buildingBtn;
    public Button unitBtn;

    public PlacementSystem placementSystem;
    // Start is called before the first frame update
    void Start()
    {
        unitBtn.onClick.AddListener(UnitCategorySelected);
        buildingBtn.onClick.AddListener(BuildingCategorySelected);

        unitPanel.SetActive(false);
        buildingPanel.SetActive(true);
    }

    private void BuildingCategorySelected()
    {
        unitPanel.SetActive(false);
        buildingPanel.SetActive(true);
    }

    private void UnitCategorySelected()
    {
        unitPanel.SetActive(true);
        buildingPanel.SetActive(false);
    }
}
