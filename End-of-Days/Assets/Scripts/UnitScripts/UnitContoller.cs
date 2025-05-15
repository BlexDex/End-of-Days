using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitContoller : MonoBehaviour
{

    public enum UnitType
    {
        Human,
        Dog
    }
    [SerializeField] public UnitType type;
    Camera cam;
    NavMeshAgent agent;
    public LayerMask ground;

    Animator animator;
    [SerializeField] private GameObject unitSelector;

    public bool isCommandedToMove;

    DirectionIndicator directionIdicator;
    AttackController attackController;
    private void Start()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        unitSelector = GameObject.FindGameObjectWithTag("Unit Selector");
        attackController = GetComponent<AttackController>();
        directionIdicator = GetComponent<DirectionIndicator>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1) && IsMovingPossible())
        {  
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground) && !unitSelector.GetComponent<UnitSelectionManager>().enemyInSight)
            {
                isCommandedToMove = true;
                StartCoroutine(NoCommand());
                agent.SetDestination(hit.point);
                attackController.targetToAttack = null;
                
                switch (type)
                {
                    case UnitType.Human:
                        SoundManager.Instance.PlayCommandedSound();
                        break;
                    case UnitType.Dog:
                        SoundManager.Instance.PlayDogCommandedSound();
                        break;
                    default:
                        break;
                }

                directionIdicator.DrawLine(hit);
            }

        }

        IEnumerator NoCommand()
        {
            yield return new WaitForSeconds(3);
            isCommandedToMove = false;
        }
    }

    private bool IsMovingPossible()
    {
        return CursorManager.Instance.currentCursor != CursorManager.CursorType.UnAvailable;
    }
}
