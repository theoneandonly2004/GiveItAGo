using UnityEngine;
using System.Collections;

public class Destroy_Collision_Object : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
       // Debug.Log("destroyed object " + collider.gameObject.name);
        DestroyObject(collider.gameObject);
    }
}
