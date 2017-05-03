using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LightManager : MonoBehaviour {

    public GameObject[] targets;
    Light[] lights;
    const int MAXACTIVE = 4;
    int maxNumberOfActiveTargets = 1;
    List<GameObject> targetsToHit;
    int score = 0;
    int scoreAmount = 5;
    int currentRound = 1;
    int numberHit = 0;
    float timeDelay = 3.0f;
    bool isFirstRound = true;
    bool hasBeenChecked = false;
    PauseMenu manager;
    Text scoreDisplay;

	// Use this for initialization
	void Start () {

        scoreDisplay = GameObject.Find("Score_Display").GetComponent<Text>();
        manager = GameObject.Find("GameManager").GetComponent<PauseMenu>();
        targetsToHit = new List<GameObject>();
        lights = new Light[targets.Length];
        for(int count = 0; count < targets.Length; count++)
        {
            lights[count] = targets[count].GetComponent<Light>(); 
        }

        //resetTargets();
        ChangeTarget();
        //Invoke("ChangeTarget", timeDelay);
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeTarget();
        }
	}

    public void UpdateNumberHit()
    {
        numberHit++;

        if(numberHit == maxNumberOfActiveTargets)
        {
            hasBeenChecked = true;
            currentRound++;
            Debug.Log("you hit all targets");   
            CancelInvoke("ChangeTarget");
            //SelectTarget();
            ChangeTarget();
        }
    }

    void selectNumberOfTargets()
    {
        int selected = Random.Range(0,currentRound/2);

        if(selected <= 0)
        {
            selected = 1;
        }

        if(selected >= MAXACTIVE)
        {
            selected =MAXACTIVE-1;
        }

        maxNumberOfActiveTargets = selected;

        
    }

    List<int> SelectTarget()
    {
        numberHit = 0;
        int selected = 0;
        List<int> numbers = new List<int>();
        for (int count = 0; count < maxNumberOfActiveTargets; count++)
        {
            selected = Random.Range(0, targets.Length);

            if (numbers.Count > 0)
            {
                while (numbers.Contains(selected))
                {
                    selected = Random.Range(0, targets.Length);
                }
            }
            targets[selected].GetComponent<TargetLight>().setIsActiveTarget(true);
            numbers.Add(selected);



        }
        return numbers;
    }

    void ActivateObject(Light lightToActivate , GameObject targetToActivate)
    {
        targetsToHit.Add(targetToActivate);
        targetToActivate.GetComponent<Renderer>().material.color = Color.red;   
        lightToActivate.intensity = 1.5f;
        lightToActivate.color = Color.red;
    }

    void resetTargets()
    {
        targetsToHit.Clear();
        for(int count = 0; count < lights.Length; count++)
        {
            lights[count].intensity = .5f;
            lights[count].color = Color.yellow;
            targets[count].GetComponent<Renderer>().material.color = Color.white;
            targets[count].GetComponent<TargetLight>().setWasHit(false);
            targets[count].GetComponent<TargetLight>().setIsActiveTarget(false);
        }
    }

    void CheckTargets()
    {
        int numberHit = 0;
        int diff = 0;

        foreach(GameObject obj in targetsToHit)
        {
            if (obj.GetComponent<TargetLight>().getWasHit())
            {
                numberHit++;
            }
        }

        if (!isFirstRound)
        {
            if (numberHit < maxNumberOfActiveTargets && !hasBeenChecked)
            {
                diff = maxNumberOfActiveTargets - numberHit;
                score -= (diff * scoreAmount);
                manager.setScore(score);
                scoreDisplay.text = "Score:" + score;
            }
            else if (numberHit >= maxNumberOfActiveTargets)
            {
                currentRound++;
                score += 30;
                manager.setScore(score);
                scoreDisplay.text = "Score:" + score;
            }
        }
        else
        {
            isFirstRound = false;
        }

        //Invoke("ChangeTarget", 5.0f);
    }

    void ChangeTarget()
    {
        CheckTargets();
        resetTargets();
        selectNumberOfTargets();
        List<int> selectedTargets = SelectTarget();

        foreach(int selected in selectedTargets)
        {
            ActivateObject(lights[selected],targets[selected]);
        }

        hasBeenChecked = false;
        Invoke("ChangeTarget",timeDelay);
    }
}
