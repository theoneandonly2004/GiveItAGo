using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour {

    int numberOfScenes = 0;
    string[] sceneNames;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

   public void triggerPulse()
    {
        GameObject controller;

        controller = GameObject.Find("Controller (right)");
        controller.GetComponent<ControllerManager>().TriggerPulse(0.5f, 1.0f);

    }



}
