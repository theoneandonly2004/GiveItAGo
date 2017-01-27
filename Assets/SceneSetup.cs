using UnityEngine;
using System.Collections;

public class SceneSetup : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Find("sword_button").GetComponent<SwordTipCollision>().Setup();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
