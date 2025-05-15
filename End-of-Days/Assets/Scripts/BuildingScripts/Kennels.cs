using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Kennels : MonoBehaviour
{
    private Camera cam;
    public LayerMask building;
    public bool isAvailable;
    public Sprite availableSprite;
    public Sprite unavailabeSprite;
    public GameObject unitBtn;
    public GameObject unit;
    private GameObject buildingSpawnLoc;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        buildingSpawnLoc = gameObject.transform.Find("SmallBarn").Find("spawnLocation").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (ResourceManager.Instance.GetResourceAmount(ResourceManager.ResourceType.Food) >= 200)
        {
            isAvailable = true;
        }
        else
        {
            isAvailable = false;
        }

        if (isAvailable)
        {
            unitBtn.GetComponent<Image>().sprite = availableSprite;
            unitBtn.GetComponent<Button>().interactable = true;
        }
        else
        {
            unitBtn.GetComponent<Image>().sprite = unavailabeSprite;
            unitBtn.GetComponent<Button>().interactable = false;
        }

        GameObject UI;
        GameObject thisUI;
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //if a clickable object is being hit
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, building))
        {

            if (Input.GetMouseButtonDown(0))
            {
                UI = gameObject.transform.Find("BuildingUI").transform.Find("BuildingOptions").gameObject;
                UI.SetActive(false);

                thisUI = hit.collider.gameObject.transform.Find("BuildingUI").transform.Find("BuildingOptions").gameObject;
                thisUI.SetActive(true);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(1))
            {
                UI = gameObject.transform.Find("BuildingUI").transform.Find("BuildingOptions").gameObject;
                UI.SetActive(false);
            }
        }

    }
    public void SpawnUnit()
    {
        ResourceManager.Instance.DecreaseResource(ResourceManager.ResourceType.Food, 200);
        Instantiate(unit, buildingSpawnLoc.transform.position, Quaternion.identity);
    }

}
