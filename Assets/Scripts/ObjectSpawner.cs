using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {
    int score = 0;
    float delayTime = 3.0f;
    public GameObject balloonObject;
    float maxXDiff = 1.5f , minXDiff = -0.5f;
    float maxZDiff = 1.5f, minZDiff = -0.5f;
    int level = 1;
    int maxSpawns = 10;
    GameObject[] spawnPoints;
    PauseMenu manager;

	// Use this for initialization
	void Start () {        
         manager = GameObject.Find("GameManager").GetComponent<PauseMenu>();
        
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawner");
        Invoke("Spawn", delayTime);
        Spawn();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Spawn()
    {
        GameObject[] targets;
        targets = GameObject.FindGameObjectsWithTag("Target");
        if (!manager.getIsPaused())
        {
            if (targets != null)
            {
                if (targets.Length < maxSpawns)
                {
                    GameObject spawnedObject = Instantiate(balloonObject);
                    spawnedObject.GetComponent<BalloonControl>().setBalloonType(manager.getScore());
                    Vector3 spawnPosition = GetSpawnPoint();
                    //spawnedObject.transform.LookAt(GameObject.Find("Controller (right)").transform);
                    spawnedObject.transform.Rotate(Vector3.up, 90);
                    //sGameObject.Find("Camera (head)")
                    spawnedObject.transform.position = spawnPosition;
                    spawnedObject.GetComponent<BalloonControl>().setPosition(spawnPosition);
                }
            }
        }
        Invoke("Spawn", delayTime);
    }

    Vector3 GetSpawnPoint()
    {
        int xDiff = (int)Random.Range(minXDiff, maxXDiff);
        int zDiff = (int)Random.Range(minZDiff, maxZDiff);

        int spawnPoint = (int)Random.Range(0, spawnPoints.Length - 1);

        float posX = spawnPoints[spawnPoint].transform.position.x + xDiff;
        float posY = spawnPoints[spawnPoint].transform.position.y - 0.3f;
        float posZ = spawnPoints[spawnPoint].transform.position.z + zDiff;

        

        return new Vector3(posX, posY, posZ);
    }
}
