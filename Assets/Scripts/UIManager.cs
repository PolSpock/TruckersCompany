using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	public static UIManager instance { get; private set; }

	GameObject[] pauseObjects;
	GameObject[] jobsObjects;
    GameObject attachTrailerObjects;

    bool showTextNotificiation;
    float textNotificationTime = 4f;

    public GameObject cameraPause;
	public GameObject cameraJobs;
	public InputField inputFieldCompanyName;

	public string companyName;
	public string[] cities;
	public string[] typeMissions;
	public string[] distanceMissions;

	public Text companyNameText;
	public Text fromCity;
	public Text toCity;
	public Text typeMission;
	public Text distanceMission;

	public Text onJobsDestination;
	public Text onJobsDistance;
	public Text onJobsType;

    public Text trailerAttach;

	void Start () {
		UIManager.instance = this;
        showTextNotificiation = false;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		jobsObjects = GameObject.FindGameObjectsWithTag("ShowOnJobs");

        attachTrailerObjects = GameObject.FindGameObjectWithTag("ShowOnAttachTrailer");
        attachTrailerObjects.GetComponent<Canvas>().enabled = false;

        hidePaused();
		hideJobs();
		cameraPause.SetActive(false);
		cameraJobs.SetActive (false);
		Scene currentScene = SceneManager.GetActiveScene ();
		if (currentScene.name == "mainScene") {
			getCompanyName ();
			fillArraysJobs ();
		}
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			TestIfPause ();
		}

        if (showTextNotificiation)
        {
            textNotificationTime -= Time.deltaTime;
            if (textNotificationTime < 0)
            {
                showTextNotificiation = false;
                attachTrailerObjects.GetComponent<Canvas>().enabled = false;
                textNotificationTime = 5f;
            }
        }

	}

	public void TestIfPause() {
		if(Time.timeScale == 1)
		{
			Time.timeScale = 0;
			showPaused();
			cameraPause.SetActive(true);
		}
		else if (Time.timeScale == 0){
			Time.timeScale = 1;
			hidePaused();
			cameraPause.SetActive(false);
			cameraJobs.SetActive(false);
		}
	}

	//shows objects with ShowOnPause tag
	public void showPaused(){
		foreach(GameObject pause in pauseObjects){
			pause.SetActive(true);
		}
	}

	//hides objects with ShowOnPause tag
	public void hidePaused(){
		foreach(GameObject pause in pauseObjects){
			pause.SetActive(false);
		}
	}

	public void hideJobs(){
		foreach(GameObject jobs in jobsObjects){
			jobs.SetActive (false);
		}
	}

	public void showJobs(){
		foreach(GameObject jobs in jobsObjects){
			jobs.SetActive (true);
		}
	}

	//loads inputted level
	public void LoadLevel(string level) {
		SceneManager.LoadScene(level);
	}

	public void ChangePauseToJobs() {
		cameraPause.SetActive(false);
		cameraJobs.SetActive(true);
	}

	public void QuitGame() {
		Application.Quit();
	}

	public void setCompanyName(){
		companyName = inputFieldCompanyName.text;
		PlayerPrefs.SetString("CompanyName", companyName);
	}

	public void getCompanyName() {
		companyNameText.text = PlayerPrefs.GetString ("CompanyName");
	}

	public void selectMission() {
		TestIfPause();
		onJobsDestination.text = cities [2];
		onJobsDistance.text = distanceMissions [0];
		onJobsType.text = typeMissions [1];
		showJobs();
		GameScript.instance.plyOnJob = true;
	}

	public void fillArraysJobs(){
		cities = new string[3];
		typeMissions = new string[3];
		distanceMissions = new string[3];

		cities [0] = "Paris";
		cities [1] = "Marseille";
		cities [2] = "Lyon";
		typeMissions [0] = "Electronique";
		typeMissions [1] = "Pommes";
		typeMissions [2] = "Fournitures";
		distanceMissions [0] = "340Km";
		distanceMissions [1] = "480Km";
		distanceMissions [2] = "800Km";

		fromCity.text = cities [2];
		toCity.text = cities [1];
		typeMission.text = typeMissions [1];
		distanceMission.text = distanceMissions [0];
	}

    public void TrailerAttachText()
    {
        if (GameScript.instance.plyHasTrailer && GameScript.instance.plyTrailer)
        {
            attachTrailerObjects.GetComponent<Canvas>().enabled = true;
            trailerAttach.text = "Vous avez attaché la remorque";
            showTextNotificiation = true;
        }
        else
        {
            attachTrailerObjects.GetComponent<Canvas>().enabled = true;
            trailerAttach.text = "Vous avez détaché la remorque";
            showTextNotificiation = true;
        }
    }

    public void FinishMissionText()
    {
        if (!GameScript.instance.plyOnJob)
        {
            attachTrailerObjects.GetComponent<Canvas>().enabled = true;
            trailerAttach.text = "Vous avez terminé votre livraison";
            showTextNotificiation = true;
        }
    }
}
