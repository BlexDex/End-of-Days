using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitContoller : MonoBehaviour
{
    Camera cam;
    NavMeshAgent agent;
    public LayerMask ground;

    Animator animator;

    public bool isCommandedToMove;

    DirectionIndicator directionIdicator;
    private void Start()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();

        directionIdicator = GetComponent<DirectionIndicator>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1) && IsMovingPossible())
        {  
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                isCommandedToMove = true;
                StartCoroutine(NoCommand());
                agent.SetDestination(hit.point);

                SoundManager.Instance.PlayCommandedSound();

                directionIdicator.DrawLine(hit);
            }

        }
        
        // if (agent.hasPath == false || agent.remainingDistance <= agent.stoppingDistance)
        // {
        //     isCommandedToMove = false;
        // }

        IEnumerator NoCommand()
        {
            yield return new WaitForSeconds(1);
            isCommandedToMove = false;
        }
    }

    private bool IsMovingPossible()
    {
        return CursorManager.Instance.currentCursor != CursorManager.CursorType.UnAvailable;
    }
}
