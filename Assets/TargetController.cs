using UnityEngine;
using System.Collections;

public class TargetController : MonoBehaviour {

	// Use this for initialization
	void Start () {
       Rigidbody rigidbody = this.gameObject.GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        // switch to 'non-kinematic'
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
        rigidbody.velocity = Vector3.zero; // or another initial value
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
