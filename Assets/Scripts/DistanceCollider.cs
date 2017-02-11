using UnityEngine;
using System.Collections;

public class DistanceCollider : MonoBehaviour {
    public bool isCollided = false;
    int score = 0;
    float waitTime = 5;
	// Use this for initialization
	void Start () {
        InvokeRepeating("CheckCollisionForPoints",waitTime,waitTime);
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void CheckCollisionForPoints()
    {
        Debug.Log("your score is now " + score);
        if (isCollided)
        {
            score += 5;
        }
        else
        {
            score -= 5;
        }
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
