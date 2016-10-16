using UnityEngine;
using System.Collections;

public class DistanceController : MonoBehaviour {

    float movementDistance = 0.4f;
    float maxDistanceDiff = 2.0f;
    float minScoreDiff = 0.5f;
    GameObject head;
    Vector3 myPosition, playerPosition;

	// Use this for initialization
	void Start () {
        // head = GameObject.Find("Camera (head)");
        head = GameObject.Find("FakePlayer");
        playerPosition = head.transform.position;
        myPosition = this.gameObject.transform.position;
        InvokeRepeating("DecideMovement", 2.0f, 5.0f);
        Debug.Log(head.name);
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void DecideMovement()
    {
        Debug.Log("deciding");
        float playerPositionX = playerPosition.x;
        float trainerPositionX = myPosition.x;
        float difference = playerPositionX - trainerPositionX;

        Debug.Log("the difference is " + difference);

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
           // }
        }        
        else if(difference - movementDistance < minScoreDiff)
        {
            Debug.Log("I am within range now");
            MoveAwayFromPlayer();
        }
        else
        {
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
                    Debug.Log("rolled a zero but must move tpwards");
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
}
