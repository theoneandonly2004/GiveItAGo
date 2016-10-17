using UnityEngine;
using System.Collections;

public class DistanceCollider : MonoBehaviour {
    bool isCollided = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public bool getIsCollided()
    {
        return isCollided;
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isCollided = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isCollided = false;
        }
    }

   
}
