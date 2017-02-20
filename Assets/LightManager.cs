using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LightManager : MonoBehaviour {

    public GameObject[] targets;
    Light[] lights;
    int maxNumberOfActiveTargets = 1;
    List<GameObject> targetsToHit;
    int score = 0;
    int scoreAmount = 5;
    bool isFirstRound = true;
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

        resetTargets();

        Invoke("ChangeTarget", 5.0f);
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeTarget();
        }
	}

    List<int> SelectTarget()
    {
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
            if (numberHit < maxNumberOfActiveTargets) // change this once number is more steady
            {
                diff = maxNumberOfActiveTargets - numberHit;
                score -= (diff * scoreAmount);
                manager.setScore(score);
                scoreDisplay.text = "Score:" + score;
            }
            else if (numberHit >= maxNumberOfActiveTargets)
            {
                score += 30;
                manager.setScore(score);
                scoreDisplay.text = "Score:" + score;
            }
        }
        else
        {
            isFirstRound = false;
        }
    }

    void ChangeTarget()
    {
        CheckTargets();
        resetTargets();
        List<int> selectedTargets = SelectTarget();

        foreach(int selected in selectedTargets)
        {
            ActivateObject(lights[selected],targets[selected]);
        }
        Invoke("ChangeTarget", 5.0f);
        Debug.Log("score:" + score);
    }
}
