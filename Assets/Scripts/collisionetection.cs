using UnityEngine;
using System.Collections;

public class collisionetection : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collision)
    {   
        this.gameObject.transform.parent= collision.gameObject.transform.parent;
        Debug.Log("mommy?");    
       
    }
}
