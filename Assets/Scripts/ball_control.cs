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
    private GameObject swordButton;
    private SwordTipCollision buttonInfo;
    private PauseMenu manager;
    private Vector3 startSize;

    public enum LevelType
    {
        beginner = 4 , intermediate = 3 , hard = 2
    }

	// Use this for initialization
	void Start () {
        startSize = targets[0].transform.localScale;
        swordButton = GameObject.Find("sword_button");
        manager = GameObject.Find("GameManager").GetComponent<PauseMenu>();
        buttonInfo = swordButton.GetComponent<SwordTipCollision>();
        timeTilSpawn = (int)LevelType.intermediate;
        timerText = buttonInfo.getTimerTextObject();
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

    void swapParts(GameObject spawnedPieces) //untested code, the intention of picking 2 parts of target and swapping
    {
        int numberOfSwapablePieces = spawnedPieces.transform.childCount;
        int randomNumberOne = Random.Range(0, numberOfSwapablePieces);
        int randomNumberTwo = Random.Range(0, numberOfSwapablePieces);
        GameObject firstPart, secPart;
        Color firstColor, secondColor;
        string initialName;
       

        while(randomNumberOne == randomNumberTwo)
        {
            randomNumberTwo = Random.Range(0, numberOfSwapablePieces);
        }

        firstPart = spawnedPieces.transform.GetChild(randomNumberOne).gameObject;
        secPart = spawnedPieces.transform.GetChild(randomNumberTwo).gameObject;

        firstColor = firstPart.GetComponent<Renderer>().material.color;
        secondColor = secPart.GetComponent<Renderer>().material.color;

        initialName = firstPart.name;
        firstPart.name = secPart.name;
        firstPart.GetComponent<Renderer>().material.color = secondColor;

        secPart.name = initialName;
        secPart.GetComponent<Renderer>().material.color = firstColor;


    }

    void scaleObject(GameObject spawned , int scaleAmount)
    {
        float initialX = startSize.x;
        float initialY = startSize.y;
        float initialZ = startSize.z;

        float newX = initialX / scaleAmount;
        float newY = initialY / scaleAmount;
        float newZ = initialZ / scaleAmount;

        spawned.transform.localScale = new Vector3(newX, newY, newZ);

    }

    void SpawnTarget()
    {
        CancelInvoke(invokeSpawnName);
        int rand = Random.Range(0, targets.Length - 1);
        int shouldTargetChange = Random.Range(0,manager.getScore());
        //int shouldTargetChange = 86;

        int minColorChange = 40;
        int minSmallSizeChange = 60;
        int midSizeChange = 80;
        int bigSizeChange = 100;
       
      

        int randomDelayTime = (int)Random.Range(4.0f, 6.0f);
        Debug.Log(rand);
        GameObject spawned = Instantiate(targets[rand]);

        if (shouldTargetChange > minColorChange)
        {
            swapParts(spawned);
        }
        if(shouldTargetChange > minSmallSizeChange && shouldTargetChange < midSizeChange)
        {
            scaleObject(spawned,2);  
        }
        else if(shouldTargetChange > midSizeChange && shouldTargetChange < bigSizeChange)
        {
            scaleObject(spawned,3);
        }
        else if(shouldTargetChange > bigSizeChange)
        {
            scaleObject(spawned,4);
        }



        swapParts(spawned); //untested code
        spawned.transform.position = spawnerPosition;
        //spawned.transform.rotation.Set(0, -90, 0,0);
        timerText.text = "Get Ready!!!";
        Invoke(startInvokeName, randomDelayTime);

    }

    void Countdown()
    {
        timeTilSpawn -= 1;
        //timerText.text = "" + timeTilSpawn;
       

        if(timeTilSpawn == 0)
        {
            SpawnTarget();           
            timeTilSpawn = (int)LevelType.intermediate;
            timerText.text = "get ready";

        }

        
    }

    void RestartInvoke()
    {
        //timerText.text = "" + timeTilSpawn;
        InvokeRepeating(invokeSpawnName, 1.0f, 1.0f);
    }

}
