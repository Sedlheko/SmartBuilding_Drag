using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCheck : MonoBehaviour
{ 
    private Collider col;

    private void Start()
    {
        col = GetComponent<Collider>();
    }

    void Update()
    {
        if(col.enabled == false)
        {
            col.enabled = true;
        }
    }
    void OnTriggerEnter(Collider other)
        {
                col.enabled = false;
        }

        void OnTriggerExit(Collider other)
        {
            col.enabled = true;
        }
}
