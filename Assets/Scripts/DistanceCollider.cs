using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DistanceCollider : MonoBehaviour {
    public bool isCollided = false;
    int score = 0;
    float waitTime = 5;
    Text scoreDisplay;
    PauseMenu manager;
    public GameObject objectHolder;
    int numberOfChildren;
    public GameObject childOne, childTwo;
	// Use this for initialization
	void Start () {
        numberOfChildren = objectHolder.transform.childCount;
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

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //this.GetComponent<Renderer>().material.color = Color.green;
            /*childOne.GetComponent<Renderer>().material.color = Color.green;
            childTwo.GetComponent<Renderer>().material.color = Color.green;*/
            Debug.Log(numberOfChildren);
            for(int count = 0; count < numberOfChildren; count++)
            {
                Debug.Log(objectHolder.transform.GetChild(count).name);
                objectHolder.transform.GetChild(count).GetComponent<Renderer>().material.color = Color.green;
            }

            Debug.Log("i'm in range");
            isCollided = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           // this.GetComponent<Renderer>().material.color = Color.red;
            /*childOne.GetComponent<Renderer>().material.color = Color.red;
            childTwo.GetComponent<Renderer>().material.color = Color.red;*/

            for (int count = 0; count < numberOfChildren; count++)
            {
               
                objectHolder.transform.GetChild(count).GetComponent<Renderer>().material.color = Color.red;
            }
            isCollided = false;
        }
    }

   
}
