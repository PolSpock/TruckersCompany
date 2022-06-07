using UnityEngine;

public class TruckManagementScript : MonoBehaviour {

    public GameObject areaAttach;

    public GameObject cameraTruck;
    public GameObject cameraTruckWithTrailer;
    public GameObject cameraBackTruck;

    // Use this for initialization
    void Start () {
        cameraTruck.SetActive(true);
        cameraTruckWithTrailer.SetActive(false);
        cameraBackTruck.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (GameScript.instance.trailerIsNear && GameScript.instance.plyOnJob && !GameScript.instance.plyHasTrailer && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Collision + Jump");

            AttachTrailer();
            UIManager.instance.TrailerAttachText();
        }
        else if (GameScript.instance.plyHasTrailer && GameScript.instance.plyTrailer && GameScript.instance.plyOnJob && !GameScript.instance.plyHasArrived && Input.GetKeyDown(KeyCode.Space) && GetComponent<Rigidbody>().velocity.magnitude < 1)
        {
            Debug.Log("Détacher trailer");

            DetachTrailer();
            UIManager.instance.TrailerAttachText();
        }
        else if (GameScript.instance.plyHasTrailer && GameScript.instance.plyOnJob && GameScript.instance.plyTrailer && GameScript.instance.plyHasArrived && Input.GetKeyDown(KeyCode.Space) && GetComponent<Rigidbody>().velocity.magnitude < 1)
        {
            Debug.Log("le joueur est arrivé");

            DetachTrailer();
            FinishMission();
        }

        Vector3 velocity = GetComponent<Rigidbody>().velocity;
        Vector3 posBack = transform.InverseTransformDirection(velocity);

        if (!GameScript.instance.plyHasTrailer && !GameScript.instance.plyTrailer) {
            if (posBack.z < -1)
            {
                cameraTruck.SetActive(false);
                cameraBackTruck.SetActive(true);
            }
            else
            {
                cameraTruck.SetActive(true);
                cameraBackTruck.SetActive(false);
            }
        }
    }

    private void DetachTrailer()
    {
        GameObject trailerToDetach = GameScript.instance.plyTrailer;

        //if (trailerToDetach.GetComponent<Collider>())
        //   trailerToDetach.GetComponent<Collider>().enabled = true;

        CharacterJoint cj = trailerToDetach.GetComponent<CharacterJoint>();
        cj.connectedBody = null;
        cj.autoConfigureConnectedAnchor = true;

        // On déplace/attache le trailer dans le plyTruck
        trailerToDetach.transform.parent = gameObject.transform.parent;

        // On modifie la caméra
        cameraTruckWithTrailer.SetActive(false);
        cameraTruck.SetActive(true);

        GameScript.instance.plyHasTrailer = false;
        GameScript.instance.plyTrailer = null;
    }

    private void AttachTrailer()
    {
        // On récupère le trailer à attacher
        GameObject trailerToAttach = GameScript.instance.trailerNear;

        //if (trailerToAttach.GetComponent<Collider>())
        //    trailerToAttach.GetComponent<Collider>().enabled = false;

        CharacterJoint cj = trailerToAttach.GetComponent<CharacterJoint>();
        cj.connectedBody = areaAttach.GetComponent<Rigidbody>();
        cj.autoConfigureConnectedAnchor = false;
        cj.connectedAnchor = new Vector3(0, 1.75f, 0);

        // On modifie le centre de gravité du camion en lot
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, 0, -0.5f);

        // On modifie la caméra
        cameraTruck.SetActive(false);
        cameraTruckWithTrailer.SetActive(true);

        GameScript.instance.plyHasTrailer = true;
        GameScript.instance.plyTrailer = trailerToAttach;
    }

    private void FinishMission()
    {
        GameScript.instance.plyOnJob = false;
        UIManager.instance.FinishMissionText();
        UIManager.instance.hideJobs();
    }
}
