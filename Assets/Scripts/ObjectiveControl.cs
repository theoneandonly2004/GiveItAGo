using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveControl : MonoBehaviour {

    public GameObject spawnPointHolder;
    GameObject[] spawnPoints;
    int numberOfSpawnPoints;
    const int MAXWAITTIME = 20;
    const int MINWAITTIME = 5;
    int lastNumber = -1;

	// Use this for initialization
	void Start () {
       
        numberOfSpawnPoints = spawnPointHolder.transform.childCount;
        spawnPoints = new GameObject[numberOfSpawnPoints];
        int randomWaitTime = 0;
        randomWaitTime = Random.Range(MINWAITTIME, MAXWAITTIME);

        for(int count = 0; count< numberOfSpawnPoints; count++)
        {
            spawnPoints[count] = spawnPointHolder.transform.GetChild(count).gameObject;
            spawnPoints[count].SetActive(false);
        }

        Invoke("spawnTargets", randomWaitTime);
		
	}

    void spawnTargets()
    {
        int selectedTarget = Random.Range(0, numberOfSpawnPoints);
        int randomWaitTime = 0;
        randomWaitTime = Random.Range(MINWAITTIME, MAXWAITTIME);

        while (selectedTarget == lastNumber)
        {
            selectedTarget = Random.Range(0, numberOfSpawnPoints);
        }

        lastNumber = selectedTarget;

        for (int count = 0; count < numberOfSpawnPoints; count++)
        {          
            spawnPoints[count].SetActive(false);
        }

        spawnPoints[selectedTarget].SetActive(true);
        Invoke("spawnTargets", randomWaitTime);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
