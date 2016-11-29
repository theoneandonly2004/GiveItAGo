using UnityEngine;
using System.Collections;

public class KeyComponents : MonoBehaviour {

   public static GameObject gameManager;
    public static PauseMenu pauseMenu;
    public static PlayerScore scoreStorage;
    // public static ControllerManager controlManager;


	// Use this for initialization
	void Start () {
	    gameManager = GameObject.Find("GameManager");
        pauseMenu = gameManager.GetComponent<PauseMenu>();
       // controlManager = GameObject.Find("Controller (right)").GetComponent<ControllerManager>();
        scoreStorage = gameManager.GetComponent<PlayerScore>();
    

    }
	
	// Update is called once per frame
	void Update () {
	
	}

}
