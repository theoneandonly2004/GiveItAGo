using UnityEngine;
using System.Collections;

public class botSword : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collision)
    {
        if (collision.name == "blade")
        {
            Debug.Log(collision.tag);
            GameObject parent = this.transform.parent.gameObject;
            BalloonControl control = parent.transform.parent.GetComponent<BalloonControl>();
            control.setIsSwordAlive(false);
            parent.SetActive(false);
            //triggerPulse();
        }
    }

  
}
