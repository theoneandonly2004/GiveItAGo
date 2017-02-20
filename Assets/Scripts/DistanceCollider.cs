using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DistanceCollider : MonoBehaviour {
    public bool isCollided = false;
    int score = 0;
    float waitTime = 5;
    Text scoreDisplay;
    PauseMenu manager;
    public GameObject childOne, childTwo;
	// Use this for initialization
	void Start () {
        scoreDisplay = GameObject.Find("Score_Display").GetComponent<Text>();
        InvokeRepeating("CheckCollisionForPoints",waitTime,waitTime);
        manager = GameObject.Find("GameManager").GetComponent<PauseMenu>();

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

        manager.setScore(score);
        scoreDisplay.text = "Score:" + manager.getScore();
    }

    public bool getIsCollided()
    {
        return isCollided;
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //this.GetComponent<Renderer>().material.color = Color.green;
            childOne.GetComponent<Renderer>().material.color = Color.green;
            childTwo.GetComponent<Renderer>().material.color = Color.green;

            isCollided = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           // this.GetComponent<Renderer>().material.color = Color.red;
            childOne.GetComponent<Renderer>().material.color = Color.red;
            childTwo.GetComponent<Renderer>().material.color = Color.red;
            isCollided = false;
        }
    }

   
}
