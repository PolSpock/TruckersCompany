using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour {
    public static GameScript instance { get; private set; }

    public bool trailerIsNear { get; set; }
    public GameObject trailerNear { get; set; }

    public bool plyOnJob { get; set; }
    public bool plyHasTrailer { get; set; }
    public bool plyHasArrived { get; set; }
    public GameObject plyTrailer { get; set; }

    // Use this for initialization
    void Start () {
        GameScript.instance = this;
        trailerIsNear = false;
        trailerNear = null;

        plyHasArrived = false;
        plyOnJob = false;
        plyTrailer = null;
    }
	
	// Update is called once per frame
	void Update () {
    }
}
