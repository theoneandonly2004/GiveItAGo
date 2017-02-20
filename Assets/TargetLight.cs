using UnityEngine;
using System.Collections;

public class TargetLight : MonoBehaviour {

    public bool wasHit = false;

	// Use this for initialization
	void Start () {
	
	}

    public void setWasHit(bool wasHit)
    {
        this.wasHit = wasHit;
    }

    public bool getWasHit()
    {
        return wasHit;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
