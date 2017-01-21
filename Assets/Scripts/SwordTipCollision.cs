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
    int maxLevel = 2;
    int lowestLevel = 0;
    int currentExercise = 0;
    int maxTime = 120;
    int timeRemaining;
    string[] sceneNames = { "balloon pop", "dropper", "keepDistance" };
    // Use this for initialization
    void Start()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        timeRemaining = maxTime;        
            canvas = GameObject.Find("PointsCanvas").GetComponent<Text>();
            offTargetText = GameObject.Find("off_target_text");
            offTargetText.SetActive(false);
            timerText = GameObject.Find("Timer").GetComponent<Text>();
        timerText.text = "" + timeRemaining + " seconds remaining";

        InvokeRepeating("CountdownGame", 1.0f, 1.0f);

        manager = GameObject.Find("GameManager").GetComponent<PauseMenu>();
        currentExercise = SceneManager.GetActiveScene().buildIndex;
        currentExerciseScore = manager.GetComponent<PlayerScore>().getExerciseScore(currentExercise);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        hasHit = false;
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
        else if (objectTag == "Box_Target")
        {
            //SteamVR_Controller.Input(0).GetPressDown(SteamVR_Controller.ButtonMask.Trigger);
            Debug.Log(objectName);
            hasHit = true;
            DetermineTargetPoints(objectName);
            attachedColliders = collision.GetComponents<BoxCollider>();
            GameObject parentObject = collision.gameObject.transform.parent.gameObject;
            Destroy(parentObject);
        }
        else if (objectTag == "UI_Element")
        {
            if (objectName == "Exit(Clone)")
            {
                Debug.Log("application ended");

            }
            else if (objectName == "NextExercise(Clone)")
            {
                timeRemaining = maxTime;
                PlayerScoreClass currentExerciseScore = manager.GetComponent<PlayerScore>().getExerciseScore(currentExercise);
                currentExerciseScore.setFinalScore(score);
                KeyComponents.scoreStorage.outputToFile();

                //Application.LoadLevel(1);
                if (currentExercise+1 > maxLevel)
                {                   
                    currentExercise = lowestLevel;
                }
                else
                {
                    score = 0;
                    currentExercise++;
                    //SceneManager.LoadScene(currentExercise);
                    SteamVR_LoadLevel.Begin(sceneNames[currentExercise]);
                }
                score = 0;
                Debug.Log("the current exercise numnber is " +currentExercise);
                //SceneManager.LoadScene(currentExercise);
                SteamVR_LoadLevel.Begin(sceneNames[currentExercise]);
            }
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
                timeRemaining = maxTime;

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
                SceneManager.LoadScene(currentExercise);
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
