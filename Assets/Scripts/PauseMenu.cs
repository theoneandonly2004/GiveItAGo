using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject nextGameObject;
    public GameObject exitObject;

    int score = 0;
    int timeMultiplier = 1;

    GameObject[] spawnedButtons = new GameObject[2];
    bool allowNextExerciseButton = true;
    bool allowedMenuButton = true;

    bool isPaused = false;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setAllowNextExerciseButton(bool isAllowed)
    {
        allowNextExerciseButton = isAllowed;
    }

    public void setAllowMenuButton(bool isAllowed)
    {
        allowedMenuButton = isAllowed;
    }

    void LoadNextLevel(bool isNextLevel)
    {
        int currentExercise = SceneManager.GetActiveScene().buildIndex;
        int maxLevel = 2;
        int lowestLevel = 0;

        if (isNextLevel)
        {
            if (currentExercise + 1 > maxLevel)
            {
                currentExercise = lowestLevel;
            }
            else
            {
                currentExercise++;
                SceneManager.LoadScene(currentExercise);
            }
        }
        else
        {
            if (currentExercise - 1 < lowestLevel)
            {
                currentExercise = maxLevel;
            }
            else
            {
                currentExercise--;
                SceneManager.LoadScene(currentExercise);
            }
        }
        Debug.Log(currentExercise);
        SceneManager.LoadScene(currentExercise);

    }

    public int getTimeMultipler()
    {
        return timeMultiplier;
    }

    public bool getIsPaused()
    {
        return isPaused;
    }

    public int getScore()
    {
        return score;
    }

    public void setIsPaused(bool isPaused)
    {
        this.isPaused = isPaused;
    }

    public void setScore(int score)
    {
        this.score = score;
    }

    public void managePause(int axis)
    {
        if (isPaused)
        {
            isPaused = false;
        }
        else
        {
            isPaused = true;
        }

            if (isPaused)
            {
                if (allowNextExerciseButton)
                {
                spawnedButtons[0] = Instantiate(nextGameObject);
                }

                if (allowedMenuButton)
                {
                spawnedButtons[1] = Instantiate(exitObject);
                }
            }
            else
            {
                if (allowNextExerciseButton)
                {
                Destroy(spawnedButtons[0]);
                }
                if (allowedMenuButton)
                {
                Destroy(spawnedButtons[1]);
                }
            }
        

        /*if (isPaused)
        {
           // if (Input.GetKeyDown(KeyCode.RightArrow))
           if(axis > 0)
            {
                LoadNextLevel(true);
            }
           // else if (Input.GetKeyDown(KeyCode.LeftArrow))
           else if(axis < 0)
            {
                LoadNextLevel(false);
            }
        }*/
    }
}
