using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SwordTipCollision : MonoBehaviour
{

    int score = 0; // this will be moved at a later stage to a more permenant location
    Text canvas , timerText , scoreDisplay;
    PauseMenu manager;
    GameObject offTargetText;
    PlayerScoreClass currentExerciseScore;
    int level;
    public bool hasScoreCanvas = true;
    bool hasHit = false;
    int maxLevel = 3;
    int lowestLevel = 0;
    int currentExercise = -1;
    int maxTime = 120; //120 for real uses , 20 for testing
    int timeRemaining;
    string[] sceneNames = { "balloon pop", "dropper", "keepDistance" , "hitZone"};
    SwordInformation[] gameSwordInfo;
    string currentGameName = "";
    bool isExtended = false;
    bool isRunningExercise = false;
    Text highScoreDisplay;

    // Use this for initialization
    void Start()
    {
        gameSwordInfo = new SwordInformation[sceneNames.Length];

        for(int count = 0; count < gameSwordInfo.Length; count++)
        {
            gameSwordInfo[count] = new SwordInformation();
        }
        maxLevel = sceneNames.Length;
        scoreDisplay = GameObject.Find("Score_Display").GetComponent<Text>();
        highScoreDisplay = GameObject.Find("High_Score_Display").GetComponent<Text>();
        level = SceneManager.GetActiveScene().buildIndex;
        timeRemaining = maxTime;        
            canvas = GameObject.Find("PointsCanvas").GetComponent<Text>();
            offTargetText = GameObject.Find("off_target_text");
            //offTargetText.SetActive(false);
            timerText = GameObject.Find("Timer").GetComponent<Text>();
        //timerText.text = "" + timeRemaining + " seconds remaining";

        //InvokeRepeating("CountdownGame", 1.0f, 1.0f);

        manager = GameObject.Find("GameManager").GetComponent<PauseMenu>();
        //currentExercise = SceneManager.GetActiveScene().buildIndex;
        currentExerciseScore = manager.GetComponent<PlayerScore>().getExerciseScore(currentExercise);

        SteamVR_LoadLevel.Begin("MainMenu");
        Setup(true);

    }

    public PauseMenu GetManagerObject()
    {
        return manager;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(doMovement());
        }
      
    }

    public Text getTimerTextObject()
    {
        return timerText;
    }

   public void Setup(bool isLoadingMainMenu)
    {
        level = SceneManager.GetActiveScene().buildIndex;
        timeRemaining = maxTime;
        //canvas = GameObject.Find("PointsCanvas").GetComponent<Text>();
        //offTargetText = GameObject.Find("off_target_text");
        //offTargetText.SetActive(false);
        //timerText = GameObject.Find("Timer").GetComponent<Text>();
        CancelInvoke("CountdownGame");
        score = 0;
        scoreDisplay.text = "Score:" + score;
        highScoreDisplay.text = "High Score:0";

        if (!isLoadingMainMenu)
        {
            //offTargetText = GameObject.Find("off_target_text");
            //offTargetText.SetActive(false);

            InvokeRepeating("CountdownGame", 1.0f, 1.0f);
            timerText.text = "" + timeRemaining + " seconds remaining";
            //offTargetText.SetActive(false);
            //timerText.gameObject.SetActive(true);
        }
        else
        {
            CancelInvoke("CountdownGame");
            offTargetText.SetActive(false);
            //timerText.gameObject.SetActive(false);
            timerText.text = "no game in progress";

            
        }

        manager = GameObject.Find("GameManager").GetComponent<PauseMenu>();
        //currentExercise = SceneManager.GetActiveScene().buildIndex;
        currentExerciseScore = manager.GetComponent<PlayerScore>().getExerciseScore(currentExercise);
    }

    void OnDestroy()
    {
        hasHit = false;
    }

    void OnTriggerExit(Collider collision)
    {
        string objectName = collision.gameObject.name;
        if (objectName == "ExtensionCheck")
        {
            isExtended = false;
        }

    }

    void OnTriggerEnter(Collider collision)
    {
        BoxCollider[] attachedColliders;
        string objectName = collision.gameObject.name;
        string objectTag = collision.gameObject.tag;
        int balloonType = 0;
        if (objectTag == "Target")
        {
            Debug.Log("collided with " + objectName);
            if (objectTag == "Target")
            {
                if (isExtended)
                {
                    if (objectName == "balloonTarget")
                    {
                        BalloonControl control = collision.GetComponent<BalloonControl>();
                        balloonType = control.getBalloonType();

                        switch (balloonType)
                        {
                            case 0:
                                DestroyObject(collision.gameObject);
                                score += 10000;
                                manager.setScore(score);
                                break;
                            case 1:
                                if (control.getIsSwordAlive())
                                {
                                    Debug.Log("hitting without priority");
                                    score -= 50;

                                    manager.setScore(score);
                                }
                                else
                                {
                                    DestroyObject(collision.gameObject);
                                    Debug.Log("nice parry reposte");
                                    score += 50;
                                    manager.setScore(score);
                                }
                                break;
                            case 2:
                                DestroyObject(collision.gameObject);
                                break;

                        }

                        Debug.Log("manager score is " + manager.getScore());
                        scoreDisplay.text = "Score " + score;
                    }
                }
            }

        }
        else if (objectTag == "Box_Target")
        {
            //SteamVR_Controller.Input(0).GetPressDown(SteamVR_Controller.ButtonMask.Trigger);
            if (isExtended)
            {
                Debug.Log(objectName);
                hasHit = true;
                DetermineTargetPoints(objectName);
                attachedColliders = collision.GetComponents<BoxCollider>();
                GameObject parentObject = collision.gameObject.transform.parent.gameObject;
                Destroy(parentObject);
            }
        }
        else if(objectTag == "Light_Target")
        {
            Debug.Log("i hit a light y'all");
            collision.gameObject.GetComponent<TargetLight>().setWasHit(true);
        }
        else if (objectTag == "UI_Element")
        {
            manageUICollision(collision.gameObject);
        }
        else if (objectTag == "Movie_Control")
        {
            MoviePlayer player = GameObject.Find("MovieDisplay").GetComponent<MoviePlayer>();
            if (objectName == "Back")
            {
                Debug.Log("going back");
                player.skipBack();
            }
            else
            {
                Debug.Log("going forward");
                player.skipForward();
            }
        }
        else if(objectName == "ExtensionCheck")
        {
            isExtended = true;
            Debug.Log("good job keeping extended");
        }
      


    }

    void manageUICollision(GameObject collidedObject)
    {
        string objectName = collidedObject.name;
        string gauntletName = "Gauntlet";
        string dropperName = "Dropper";
        string keepDistanceName = "KeepDistance";
        string balloonPopName = "BalloonPop";
        string achievementsText = "Achievements";
        string hitZoneName = "HitZone";

        PlayerScoreClass currentExerciseScore;



        Text highScoreDisplay = GameObject.Find("High_Score_Display").GetComponent<Text>();

        if (objectName == "Exit(Clone)")
        {
            KeyComponents.pauseMenu.setIsPaused(false);
            SteamVR_LoadLevel.Begin("MainMenu");
            currentExercise = -1;
            Setup(true);
            //CancelInvoke();

        }
        else if(objectName == "Exit")
        {
            KeyComponents.pauseMenu.setIsPaused(false);
            SteamVR_LoadLevel.Begin("MainMenu");
            currentExercise = -1;
            Setup(true);
            Destroy(collidedObject);
        }
        else if (objectName == "NextExercise(Clone)")
        {
            timeRemaining = maxTime;
            currentExerciseScore = manager.GetComponent<PlayerScore>().getExerciseScore(currentExercise);
            currentExerciseScore.setFinalScore(score);
            KeyComponents.scoreStorage.outputToFile();

            if (currentGameName == "Gauntlet")
            {

                //Application.LoadLevel(1);
                if (currentExercise + 1 > maxLevel)
                {
                    
                    //currentExercise = lowestLevel;
                    SteamVR_LoadLevel.Begin("MainMenu");
                    currentExercise = -1;
                    Setup(true);
                   // CancelInvoke();
                }
                else
                {
                    score = 0;
                    manager.setScore(score);
                    currentExercise++;
                    //SceneManager.LoadScene(currentExercise);
                    SteamVR_LoadLevel.Begin(sceneNames[currentExercise]);
                }

               /* score = 0;
                manager.setScore(score);
                Debug.Log("the current exercise numnber is " + currentExercise);
                //SceneManager.LoadScene(currentExercise);
                SteamVR_LoadLevel.Begin(sceneNames[currentExercise]);*/
            }
            else
            {
                SteamVR_LoadLevel.Begin("MainMenu");
                currentExercise = -1;
                Setup(true);
               // CancelInvoke();
            }
        }
        else
        {

            
            if (objectName == gauntletName)
            {
                
                currentGameName = "Gauntlet";
                SteamVR_LoadLevel.Begin(sceneNames[0]);
                currentExercise = 0;
                Setup(false);
                currentExerciseScore = manager.GetComponent<PlayerScore>().getExerciseScore(0);
                highScoreDisplay.text = "High Score:" + currentExerciseScore.highestScore;
                //Debug.Log("now starting gauntlet run");
            }
            else if(objectName == balloonPopName)
            {
                currentGameName = "BalloonPop";
                SteamVR_LoadLevel.Begin(sceneNames[0]);
                currentExercise = 0;
                Setup(false);
                currentExerciseScore = manager.GetComponent<PlayerScore>().getExerciseScore(0);
                highScoreDisplay.text = "High Score:" + currentExerciseScore.highestScore;
            }
            else if(objectName == dropperName)
            {
                currentGameName = "Dropper";
                SteamVR_LoadLevel.Begin(sceneNames[1]);
                currentExercise = 1;                
                Setup(false);
                currentExerciseScore = manager.GetComponent<PlayerScore>().getExerciseScore(1);
                highScoreDisplay.text = "High Score:" + currentExerciseScore.highestScore;
            }
            else if (objectName == keepDistanceName)
            {
                currentGameName = "KeepDistance";
                SteamVR_LoadLevel.Begin(sceneNames[2]);
                currentExercise = 2;
                Setup(false);
                currentExerciseScore = manager.GetComponent<PlayerScore>().getExerciseScore(2);
                highScoreDisplay.text = "High Score:" + currentExerciseScore.highestScore;
            }
            else if(objectName == hitZoneName)
            {
                currentGameName = "hitZone";
                SteamVR_LoadLevel.Begin(sceneNames[3]);
                currentExercise = 3;
                Setup(false);
                currentExerciseScore = manager.GetComponent<PlayerScore>().getExerciseScore(3);
                highScoreDisplay.text = "High Score:" + currentExerciseScore.highestScore;
            }
            else if (objectName == achievementsText)
            {                
                SteamVR_LoadLevel.Begin("AchievementScene");                
            }
        }

    }

    void LoadLevel()
    {


        if (currentGameName == "Gauntlet")
        {
            timeRemaining = maxTime;
            //Application.LoadLevel(1);
            if (currentExercise + 1 > maxLevel)
            {
                //currentExercise = lowestLevel;
                SteamVR_LoadLevel.Begin("MainMenu");
                currentExercise = -1;
                Setup(true);
                //CancelInvoke();
            }
            else
            {
                score = 0;
                manager.setScore(score);
                currentExercise++;
                timerText.text = "" + timeRemaining + " seconds remaining";
                //SceneManager.LoadScene(currentExercise);
                SteamVR_LoadLevel.Begin(sceneNames[currentExercise]);
                Setup(false);
            }

            /*score = 0;
            manager.setScore(score);
            Debug.Log("the current exercise numnber is " + currentExercise);
            //SceneManager.LoadScene(currentExercise);
            SteamVR_LoadLevel.Begin(sceneNames[currentExercise]);
            Setup(false);*/
        }
        else
        {
            SteamVR_LoadLevel.Begin("MainMenu");
            currentExercise = -1;
            Setup(true);
           // CancelInvoke();
        }
    }

   IEnumerator doMovement()
    {
        bool canEnd = false;
        int currentPos = 0;
        GameObject parent = this.transform.parent.gameObject;
        while (!canEnd)
        {
            parent.transform.position = gameSwordInfo[0].getPositionAtTime(currentPos);
            yield return new WaitForSeconds(1.0f);

            currentPos++;

            if(gameSwordInfo[0].getSwordPositionList().Count <= currentPos)
            {
                canEnd = true;
            }

        }

       

    }

    void CountdownGame()
    {      
        if (!KeyComponents.pauseMenu.getIsPaused())
        {
            gameSwordInfo[currentExercise].addSwordPosition(this.transform.parent.gameObject.transform.position);
            gameSwordInfo[currentExercise].addSwordState(isExtended);

            
       
            timeRemaining -= 1;
            timerText.text = "" + timeRemaining + " seconds remaining";

            if (timeRemaining <= 0)
            {

                for(int count = 0; count < gameSwordInfo[currentExercise].getSwordPositionList().Count; count++)
                {
                    Debug.Log(gameSwordInfo[currentExercise].getSwordPositionList()[count]);
                }

                currentExerciseScore = manager.GetComponent<PlayerScore>().getExerciseScore(currentExercise);
                currentExerciseScore.setFinalScore(manager.getScore());
                KeyComponents.scoreStorage.outputToFile();
                /*timeRemaining = maxTime;

                if (currentExercise + 1 > maxLevel)
                {
                    currentExercise = lowestLevel;
                }
                else
                {
                    score = 0;
                    currentExercise++;
                    SceneManager.LoadScene(currentExercise);
                }
                score = 0;
                Debug.Log(currentExercise);
                SceneManager.LoadScene(currentExercise);*/
                LoadLevel();
            }
        }

        
    }


    void DetermineTargetPoints(string objectName)
    {
        Debug.Log("i met a cool guy called " + objectName);
        //offTargetText.SetActive (true);
        //offTargetText.GetComponent<Text> ().text = objectName;


        switch (objectName)
        {
            case "bullseye":
                score += 10;
                break;
            case "second_target":
                score += 7;
                break;
            case "third_target":
                score += 5;
                break;
            case "fourth_target":
                score += 3;
                break;
            case "off_target":
                StartCoroutine(setObjectStatus(offTargetText, true, 0));
                StartCoroutine(setObjectStatus(offTargetText, false, 2));
                break;
        }
        Debug.Log(score);
        manager.setScore(score);
       scoreDisplay.text = "Score " + score;
    }


    //this will allow gameobjects to be disabled and enabled easier by passing the object
    //isActive is true if to be enabled and false to be disabled
    //delay determines how long to wait before performing the action
    IEnumerator setObjectStatus(GameObject disableObject, bool isActive, float delay)
    {
        yield return new WaitForSeconds(delay);
        disableObject.SetActive(isActive);
    }




}
