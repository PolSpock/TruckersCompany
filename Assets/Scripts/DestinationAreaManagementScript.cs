using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationAreaManagementScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "plyTruck" && GameScript.instance.plyOnJob)
        {
            Debug.Log("destination enter");
            GameScript.instance.plyHasArrived = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "plyTruck" && GameScript.instance.plyOnJob)
        {
            GameScript.instance.plyHasArrived = false;
            Debug.Log("destination exit");
        }
    }
}
