using UnityEngine;
using System.Collections;

public class TargetLight : MonoBehaviour {

    public bool wasHit = false;
    LightManager manager;
    bool isActiveTarget = false;

	// Use this for initialization
	void Start () {
        manager = this.transform.parent.gameObject.transform.parent.gameObject.GetComponent<LightManager>();
	}

    public bool getIsActivetarget()
    {
        return isActiveTarget;
    }

    public void setIsActiveTarget(bool isActive)
    {
        this.isActiveTarget = isActive;
    }

    public void setWasHit(bool wasHit)
    {
        this.wasHit = wasHit;

        if(wasHit == true && isActiveTarget == true)
        {
            this.GetComponent<Renderer>().material.color = Color.blue;
            manager.UpdateNumberHit();
        }
    }

    public bool getWasHit()
    {
        return wasHit;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
