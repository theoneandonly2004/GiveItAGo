using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwordTipCollision : MonoBehaviour
{

    int score = 0; // this will be moved at a later stage to a more permenant location
    Text canvas , timerText;
    PauseMenu manager;
    GameObject offTargetText;
    PlayerScoreClass currentExerciseScore;
    int level;
    public bool hasScoreCanvas = true;
    bool hasHit = false;
    int maxLevel = 3;
    int lowestLevel = 0;
    int currentExercise = -1;
    int maxTime = 20; //120 for real uses , 20 for testing
    int timeRemaining;
    string[] sceneNames = { "balloon pop", "dropper", "keepDistance" };
    string currentGameName = "";
    bool isExtended = false;
    bool isRunningExercise = false;
    // Use this for initialization
    void Start()
    {
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

    }

    // Update is called once per frame
    void Update()
    {
      
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
                                score += 1000;
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
        else if (objectTag == "UI_Element")
        {
            manageUICollision(objectName);
        }
        else if(objectName == "ExtensionCheck")
        {
            isExtended = true;
            Debug.Log("good job keeping extended");
        }


    }

    void manageUICollision(string objectName)
    {
        string gauntletName = "Gauntlet";
        string dropperName = "Dropper";
        string keepDistanceName = "KeepDistance";
        string balloonPopName = "BalloonPop";

        if (objectName == "Exit(Clone)")
        {
            SteamVR_LoadLevel.Begin("MainMenu");
            currentExercise = -1;
            Setup(true);
            //CancelInvoke();

        }
        else if (objectName == "NextExercise(Clone)")
        {
            timeRemaining = maxTime;
            PlayerScoreClass currentExerciseScore = manager.GetComponent<PlayerScore>().getExerciseScore(currentExercise);
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

                score = 0;
                manager.setScore(score);
                Debug.Log("the current exercise numnber is " + currentExercise);
                //SceneManager.LoadScene(currentExercise);
                SteamVR_LoadLevel.Begin(sceneNames[currentExercise]);
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
            if(objectName == gauntletName)
            {
                currentGameName = "Gauntlet";
                SteamVR_LoadLevel.Begin(sceneNames[0]);
                currentExercise = 0;
                Setup(false);
                //Debug.Log("now starting gauntlet run");
            }
            else if(objectName == balloonPopName)
            {
                currentGameName = "BalloonPop";
                SteamVR_LoadLevel.Begin(sceneNames[0]);
                currentExercise = 0;
                Setup(false);
            }
            else if(objectName == dropperName)
            {
                currentGameName = "Dropper";
                SteamVR_LoadLevel.Begin(sceneNames[1]);
                currentExercise = 1;
                
                Setup(false);
            }
            else if (objectName == keepDistanceName)
            {
                currentGameName = "KeepDistance";
                SteamVR_LoadLevel.Begin(sceneNames[2]);
                currentExercise = 2;
                Setup(false);
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

            score = 0;
            manager.setScore(score);
            Debug.Log("the current exercise numnber is " + currentExercise);
            //SceneManager.LoadScene(currentExercise);
            SteamVR_LoadLevel.Begin(sceneNames[currentExercise]);
            Setup(false);
        }
        else
        {
            SteamVR_LoadLevel.Begin("MainMenu");
            currentExercise = -1;
            Setup(true);
           // CancelInvoke();
        }
    }

    void CountdownGame()
    {      
        if (!KeyComponents.pauseMenu.getIsPaused())
        {
            timeRemaining -= 1;
            timerText.text = "" + timeRemaining + " seconds remaining";

            if (timeRemaining <= 0)
            {
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
        GameObject.Find("Score_Display").GetComponent<Text>().text = "Score " + score;
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
