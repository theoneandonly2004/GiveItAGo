using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ball_control : MonoBehaviour {

    int level = 1;
    int timeTilSpawn;
    public GameObject[] targets;
    string invokeSpawnName = "Countdown", startInvokeName = "RestartInvoke";

    private Text timerText;
    private Vector3 spawnerPosition;
    private GameObject spawner;

    public enum LevelType
    {
        beginner = 4 , intermediate = 3 , hard = 2
    }

	// Use this for initialization
	void Start () {
        timeTilSpawn = (int)LevelType.intermediate;
        timerText = GameObject.Find("Timer").GetComponent<Text>();
        timerText.text = "" + timeTilSpawn;
        spawner = GameObject.FindWithTag("Spawner");
        spawnerPosition = spawner.transform.position;
        spawnerPosition.y += 1.0f;
        //InvokeRepeating("SpawnTarget", (float)LevelType.intermediate,(float)LevelType.intermediate);
        InvokeRepeating(invokeSpawnName, 1.0f,1.0f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void SpawnTarget()
    {
        CancelInvoke(invokeSpawnName);
        int rand = Random.Range(0, targets.Length - 1);
        int randomDelayTime = (int)Random.Range(4.0f, 6.0f);
        Debug.Log(rand);
        GameObject spawned = Instantiate(targets[rand]);
        spawned.transform.position = spawnerPosition;
        timerText.text = "Get Ready!!!";
        Invoke(startInvokeName, randomDelayTime);

    }

    void Countdown()
    {
        timeTilSpawn -= 1;
        timerText.text = "" + timeTilSpawn;
       

        if(timeTilSpawn == 0)
        {
            SpawnTarget();           
            timeTilSpawn = (int)LevelType.intermediate;
            timerText.text = "get ready";

        }

        
    }

    void RestartInvoke()
    {
        timerText.text = "" + timeTilSpawn;
        InvokeRepeating(invokeSpawnName, 1.0f, 1.0f);
    }

}
