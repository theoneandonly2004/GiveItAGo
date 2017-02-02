using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SteamVR_LoadLevel.Begin("MainMenu");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
