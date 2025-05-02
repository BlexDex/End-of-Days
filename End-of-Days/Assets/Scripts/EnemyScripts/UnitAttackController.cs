using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttackController : MonoBehaviour
{
    public Transform targetToAttack;

    //public Material idleStateMat;
    //public Material followStateMat;
    //public Material attackStateMat;

    public bool isPlayer;

    public int unitDamage;
    private void OnTriggerEnter(Collider other)
    {
        if(isPlayer && other.CompareTag("Enemy") && targetToAttack == null)
        {
            targetToAttack = other.transform;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(isPlayer && other.CompareTag("Enemy") && targetToAttack == null)
        {
            targetToAttack = other.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(isPlayer && other.CompareTag("Enemy") && targetToAttack != null)
        {
            targetToAttack = null;
        }
    }

    public void SetIdleMaterial () 
    {
        //GetComponent<Renderer>().material = idleStateMat;
    }
    public void SetFollowMaterial () 
    {
        //GetComponent<Renderer>().material = followStateMat;
    }
    public void SetAttackMaterial () 
    {
        //GetComponent<Renderer>().material = attackStateMat;
    }
    
    private void OnDrawGizmos()
    {
        // Follow Distance
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 10f * 0.5f);
        // Attack Distance
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);
        // Stop Attacking Distance
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 1.2f);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
