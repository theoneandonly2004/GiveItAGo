using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwordTipCollision : MonoBehaviour {

    int score = 0; // this will be moved at a later stage to a more permenant location
    Text canvas;
    GameObject offTargetText;
	public bool hasScoreCanvas = true;
	bool hasHit = false;

    // Use this for initialization
    void Start () {
		if (hasScoreCanvas) {
			canvas = GameObject.Find ("Canvas").GetComponent<Text> ();
			offTargetText = GameObject.Find ("off_target_text");
			offTargetText.SetActive (false);
		}
    }
	
	// Update is called once per frame
	void Update () {
        	
	}

	void OnDestroy(){
		hasHit = false;
	}

    void OnTriggerEnter(Collider collision)
    {
		BoxCollider[] attachedColliders;
        string objectName = collision.gameObject.name;
        string objectTag = collision.gameObject.tag;
        if(objectTag == "Target")
        {
            Debug.Log("collided with " + objectName);
			if(objectTag == "Target")
            {
                DestroyObject(collision.gameObject);
            }
        
        }
        else if(objectTag == "Box_Target")
        {
            Debug.Log(objectName);
			hasHit = true;
            DetermineTargetPoints(objectName);
			attachedColliders = collision.GetComponents<BoxCollider> ();
			GameObject parentObject = collision.gameObject.transform.parent.gameObject;
			Destroy (parentObject);



        }

       
    }

   

    void DetermineTargetPoints(string objectName)
    {
		Debug.Log ("i met a cool guy called " +objectName);
		//offTargetText.SetActive (true);
		//offTargetText.GetComponent<Text> ().text = objectName;


			switch (objectName) {
			case "bullseye":
				score += 10;
				break;
			case "second_target":
				score += 7;
				break;
			case "third_target":
				score += 5;
				break;
			case "fourth_target":
				score += 3;
				break;
			case "off_target":                
				StartCoroutine (setObjectStatus (offTargetText, true, 0));
				StartCoroutine (setObjectStatus (offTargetText, false, 2));
				break;
			}
		Debug.Log (score);
        GameObject.Find("Score_Display").GetComponent<Text>().text = "Score " + score;
    }
    

    //this will allow gameobjects to be disabled and enabled easier by passing the object
    //isActive is true if to be enabled and false to be disabled
    //delay determines how long to wait before performing the action
    IEnumerator setObjectStatus(GameObject disableObject , bool isActive , float delay)
    {
        yield return new WaitForSeconds(delay);
        disableObject.SetActive(isActive);
    }




}
