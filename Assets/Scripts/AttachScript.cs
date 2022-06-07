using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "areaAttach")
        {
            Debug.Log("enter hit");
            GameScript.instance.trailerIsNear = true;
            Debug.Log("gameObject");
            GameScript.instance.trailerNear = transform.parent.gameObject;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "areaAttach")
        {
            Debug.Log("enter hit");
            GameScript.instance.trailerIsNear = true;
            GameScript.instance.trailerNear = transform.parent.gameObject;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "areaAttach")
        {
            Debug.Log("exit hit");
            GameScript.instance.trailerIsNear = false;
            GameScript.instance.trailerNear = null;
        }
    }
}
