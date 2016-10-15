using UnityEngine;
using System.Collections;

public class ControllerControls : SteamVR_TrackedController {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    
    public override void OnTriggerClicked(ClickedEventArgs e)
    {
        Debug.Log("i was clicked now i shall take over the world muhahahaha");
    }

}
