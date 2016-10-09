using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {

    float delayTime = 5.0f;
    public GameObject balloonObject;
    float maxXDiff = 2.5f , minXDiff = -0.5f;
    float maxZDiff = 2.5f, minZDiff = -0.5f;
    int level = 1;
    GameObject[] spawnPoints;

	// Use this for initialization
	void Start () {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawner");
        Invoke("Spawn", delayTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Spawn()
    {      
       GameObject spawnedObject = Instantiate(balloonObject);
        Vector3 spawnPosition = GetSpawnPoint();
        spawnedObject.transform.position = spawnPosition;
        Invoke("Spawn", delayTime);
    }

    Vector3 GetSpawnPoint()
    {
        int xDiff = (int)Random.Range(minXDiff, maxXDiff);
        int zDiff = (int)Random.Range(minZDiff, maxZDiff);

        int spawnPoint = (int)Random.Range(0, spawnPoints.Length - 1);

        float posX = spawnPoints[spawnPoint].transform.position.x + xDiff;
        float posY = spawnPoints[spawnPoint].transform.position.y;
        float posZ = spawnPoints[spawnPoint].transform.position.z + zDiff;

        

        return new Vector3(posX, posY, posZ);
    }
}
