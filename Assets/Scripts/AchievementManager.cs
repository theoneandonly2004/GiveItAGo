using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour {
    Text achievementDisplay;
    public Canvas achievementsCanvas;
    GameObject pointsCanvas;
    GameObject displayCamera;

	// Use this for initialization
	void Start () {
        //Achievement.achievements["popABalloon"].setUnlocked(true);
        achievementDisplay = achievementsCanvas.GetComponentInChildren<Text>();
        loadAchievementScene();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
       displayCamera.GetComponent<InfoDisplay>().setCanDisplay(false);
        pointsCanvas.SetActive(true);
    }

    void loadAchievementScene()
    {
        displayCamera = GameObject.Find("Camera (eye)");
        pointsCanvas = GameObject.Find("PointsCanvas");
        pointsCanvas.SetActive(false);
        displayCamera.GetComponent<InfoDisplay>().SetupToDisplay(achievementDisplay,achievementsCanvas);
        GameObject currentAchievementObject;
        UnlockableAchievement[] unlockable = new UnlockableAchievement[Achievement.achievements.Count];
        Achievement.achievements.Values.CopyTo(unlockable, 0);

        for(int count = 0; count < unlockable.Length; count++)
        {
           currentAchievementObject= GameObject.Find(unlockable[count].name);
            if (!unlockable[count].getIsUnlocked())
            {
                //currentAchievementObject.SetActive(false);
                currentAchievementObject.GetComponent<Renderer>().material.color = Color.red;
                Debug.Log("the achievement " + unlockable[count].name + " was not completed");
            }
            else
            {
                currentAchievementObject.GetComponent<Renderer>().material.color = Color.blue;
            }
        }
    }
}
