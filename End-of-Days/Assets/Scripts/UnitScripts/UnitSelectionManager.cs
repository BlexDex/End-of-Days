using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager Instance {get; set;}

    public List<GameObject> allUnitsList = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();

    public LayerMask clickable;
    public LayerMask ground;
    public LayerMask attackable;
    public LayerMask building;
    public bool attackCursorVisible;
    public bool enemyInSight;

    public GameObject groundMarker;
    
    private Camera cam;
    private void Start()
    {
        cam = Camera.main;
    }

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
    private void Update() 
        {
      if(Input.GetMouseButtonDown(0))
        {  
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            
            //if a clickable object is being hit
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    MultiSelect(hit.collider.gameObject);
                }
                else
                {
                    SelectByClick(hit.collider.gameObject);
                }
            }
            else //if not a clickable object
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    DeselectAll();
                }
            }
        }

        if(Input.GetMouseButtonDown(1) && unitsSelected.Count > 0)
        {  
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            
            //if a clickable object is being hit
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                groundMarker.transform.position = hit.point;
                groundMarker.SetActive(false);
                groundMarker.SetActive(true);
            }
        }

        if (unitsSelected.Count > 0 && AtLeastOneOffensiveUnit(unitsSelected)) 
        { 
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            
            //if a clickable object is being hit
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, attackable))
            {
                Debug.Log("Enemy in Sight");
                enemyInSight = true;
                attackCursorVisible = true;

                if (Input.GetMouseButtonDown(1))
                {
                    Transform target = hit.transform;

                    foreach (GameObject unit in unitsSelected)
                    {
                        if(unit.GetComponent<AttackController>())
                        {
                            unit.GetComponent<AttackController>().targetToAttack = target;
                        }
                    }
                }
            }
            else
            {
                enemyInSight = false;
                attackCursorVisible = false;
            }
        }

        CursorSelector();
    }

    private void CursorSelector()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
        {
            CursorManager.Instance.SetMarkerType(CursorManager.CursorType.Selectable);
        }
        else if(ResourceManager.Instance.placementSystem.inSellMode)
        {
            CursorManager.Instance.SetMarkerType(CursorManager.CursorType.Sell);
        }
        else if(Physics.Raycast(ray, out hit, Mathf.Infinity, attackable) && unitsSelected.Count > 0 && AtLeastOneOffensiveUnit(unitsSelected))
        {
            CursorManager.Instance.SetMarkerType(CursorManager.CursorType.Attackable);
        }
        else if(Physics.Raycast(ray, out hit, Mathf.Infinity, building) && unitsSelected.Count > 0)
        {
            CursorManager.Instance.SetMarkerType(CursorManager.CursorType.UnAvailable);
        }
        else if(Physics.Raycast(ray, out hit, Mathf.Infinity, ground) && unitsSelected.Count > 0)
        {
            CursorManager.Instance.SetMarkerType(CursorManager.CursorType.Walkable);
        }
        else
        {
            CursorManager.Instance.SetMarkerType(CursorManager.CursorType.None);
        }

    }

    private bool AtLeastOneOffensiveUnit(List<GameObject> unitsSelected)
    {
        foreach (GameObject unit in unitsSelected)
            {
                if(unit != null && unit.GetComponent<AttackController>())
                {
                    return true;
                }   
            }
            return false;
    }

    private void MultiSelect(GameObject unit)
    {
        if (unitsSelected.Contains(unit) == false)
        {
            unitsSelected.Add(unit);
            SelectUnit(unit, true);;
        }
        else
        {
            SelectUnit(unit, false);;
            unitsSelected.Remove(unit);
        }
    }

    public void DeselectAll()
    {
        foreach (var unit in unitsSelected)
        {
            TriggerUnitMarker(unit, false);
            EnableUnitMovement(unit, false);
        }
        groundMarker.SetActive(false);
        unitsSelected.Clear();
    }

    internal void DragSelect(GameObject unit)
    {
        if (unitsSelected.Contains(unit) == false)
        {
            unitsSelected.Add(unit);
            TriggerUnitMarker(unit, true);
            EnableUnitMovement(unit, true);

        }
    }

    private void SelectByClick(GameObject unit)
    {
        DeselectAll();

        unitsSelected.Add(unit);

        SelectUnit(unit, true);
    }

    private void SelectUnit(GameObject unit, bool isSelected)
    {
        SoundManager.Instance.PlaySelectedSound();
        TriggerUnitMarker(unit, isSelected);
        EnableUnitMovement(unit, isSelected);
    }

    private void EnableUnitMovement(GameObject unit, bool shouldMove)
    {
        unit.GetComponent<UnitContoller>().enabled = shouldMove;
    }

    private void TriggerUnitMarker(GameObject unit, bool isVisible)
    {
        unit.transform.Find("Hexa Select").gameObject.SetActive(isVisible);
    }
}

