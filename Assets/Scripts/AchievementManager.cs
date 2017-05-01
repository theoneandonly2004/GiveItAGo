using UnityEngine;
using System.Collections;

public class AchievementManager : MonoBehaviour {
        
	// Use this for initialization
	void Start () {
        //Achievement.achievements["popABalloon"].setUnlocked(true);
        loadAchievementScene();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void loadAchievementScene()
    {
        GameObject currentAchievementObject;
        UnlockableAchievement[] unlockable = new UnlockableAchievement[Achievement.achievements.Count];
        Achievement.achievements.Values.CopyTo(unlockable, 0);

        for(int count = 0; count < unlockable.Length; count++)
        {
           currentAchievementObject= GameObject.Find(unlockable[count].name);
            if (!unlockable[count].getIsUnlocked())
            {
                currentAchievementObject.SetActive(false);
                Debug.Log("the achievement " + unlockable[count].name + " was not completed");
            }
        }
    }
}
