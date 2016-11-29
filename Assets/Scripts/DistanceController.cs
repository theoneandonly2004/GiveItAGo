using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DistanceController : MonoBehaviour {

    Transform targetZone;
    Text canvas;

    int score = 0;
    float movementDistance = 0.4f;
    float maxDistanceDiff = 1.8f;
    float minScoreDiff = 0.0f;
    GameObject head;
    Vector3 myPosition, playerPosition , childIndicatorPosition;
    DistanceCollider wallCollision;

	// Use this for initialization
	void Start () {
        // head = GameObject.Find("Camera (head)");
        canvas = GameObject.Find("PointsCanvas").GetComponent<Text>();
        head = GameObject.Find("Player");
        wallCollision = this.gameObject.GetComponentInChildren<DistanceCollider>();        
       // head = GameObject.Find("FakePlayer");
        playerPosition = head.transform.position;
        myPosition = this.gameObject.transform.position;
       
       
        InvokeRepeating("DecideMovement", 2.0f, 2.0f);
        Debug.Log(head.name);
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void AllocatePoints()
    {
        if (wallCollision.getIsCollided())
        {
            Debug.Log("i am collided");
            score += 5;

        }
        else
        {
            Debug.Log("i'm not collided");
            score -= 5;
        }
        canvas.text = "Score:" + score;
    }

    void DecideMovement()
    {
        movementDistance = Random.Range(movementDistance, 1.5f);
        Debug.Log("deciding");
        float playerPositionX = playerPosition.x;
        float trainerPositionX = myPosition.x;
        float difference = playerPositionX - trainerPositionX;

        Debug.Log("the difference is " + difference);
        Invoke("AllocatePoints", 1.5f);
     

        Debug.Log(score);
       
        if(difference > maxDistanceDiff)
        {
          
           /* if (difference - movementDistance < minScoreDiff)
            {
                Debug.Log("I had to move towards him or i'd be out of range");
                MoveTowardsPlayer();
            }
            else
            {*/
              Debug.Log("it's ok i can move fine");
              MoveTowardsPlayer();
            
        }        
        else if(difference - movementDistance > minScoreDiff)
        {
           // score -= 5;
            Debug.Log("I am within range now");
            MoveAwayFromPlayer();
        }
        else
        {
          // score += 5;
            int randomNumber = Random.Range(0, 2);
            if(randomNumber == 0)
            {
                if (difference + movementDistance < maxDistanceDiff)
                {
                    Debug.Log("rolled a zero and will move normally");
                    MoveAwayFromPlayer();
                }
                else
                {
                    Debug.Log("rolled a zero but must move towards");
                    MoveTowardsPlayer();
                }
            }
            else
            {
                if (difference - movementDistance < minScoreDiff)
                {
                    Debug.Log("rolled a one and will move normally");
                    MoveTowardsPlayer();
                }
                else
                {
                    Debug.Log("rolled a one but must move away");
                    MoveAwayFromPlayer();
                }
            }
        }
       
        Debug.Log("the score currently is " + score);
    }

    void MoveTowardsPlayer()
    {
       
        myPosition.x += movementDistance;
        transform.position = myPosition;        
        Debug.Log("moving towards player");
    }

    void MoveAwayFromPlayer()
    {      
        myPosition.x -= movementDistance;
        transform.position = myPosition;
        
        Debug.Log("moving away from player");
    }

    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.name == "front_blocker")
        {
            movementDistance = 1.2f;
            MoveAwayFromPlayer();
        }
        else if (collision.gameObject.name == "back_blocker")
        {
            movementDistance = 1.2f;
            MoveTowardsPlayer();
        }
    }
}
