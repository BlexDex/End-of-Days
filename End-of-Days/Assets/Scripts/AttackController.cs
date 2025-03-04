using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Transform Target;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") && Target == null)
        {
            Target = other.transform;
        }
    }

     private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemy") && Target != null)
        {
            Target = null;
        }
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
