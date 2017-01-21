using UnityEngine;
using System.Collections;
using System.IO;

public class AchievementList : MonoBehaviour {

    public static UnlockableAchievement popABalloon;
    public static UnlockableAchievement score1000;

    private static int repeatCounts = 0;
    private static int maxRepeats = 6;
    private static string levelTag = "Level";
    private static string targetTag = "Target";
    // Use this for initialization

    public static void registerAchievements()
    {
        popABalloon = new UnlockableAchievement("popABalloon", "you popped a balloon good job", targetTag, 1);
        score1000 = new UnlockableAchievement("score1000", "you score 1000 points awesome", targetTag, 1000);

        UnlockableAchievement[] unlockable = new UnlockableAchievement[Achievement.achievements.Count];
        Achievement.achievements.Values.CopyTo(unlockable, 0);

        for(int count = 0; count < unlockable.Length; count++)
        {
            Debug.Log(unlockable[count].name);
        }

    }

    public static void readAchievements()
    {
        StreamReader reader = null;
        string json;
        string fileName = "Achievements";
        string line;
        string achievementName = "";
        string isUnlockedString = "";
        bool isUnlocked = false;       
        string userName= PlayerPrefs.GetString("username");


        if(userName != null)
        {
            if (File.Exists(Application.dataPath + "/Resources/" + userName + fileName + ".json"))
            {
                reader = new StreamReader(Application.dataPath + "/Resources/" + userName + fileName + ".json");
            }
            else
            {
                File.Create(Application.dataPath + "/Resources/" + userName + fileName + ".json");
                saveAchievements();
                //reader = new StreamReader(Application.dataPath + "/Resources/" + userName + fileName + ".json");
            }

        }
        else
        {
            reader = new StreamReader(Application.dataPath + "/Resources/" + fileName + ".json");
        }



        do
        {
            line = reader.ReadLine();

            if (line != null)
            {
                // Do whatever you need to do with the text line, it's a string now
                // In this example, I split it into arguments based on comma
                // deliniators, then send that array to DoStuff()
                string[] entries = line.Split(',');
                if (entries.Length < 0) {

                }
                else
                {
                    achievementName = entries[0];
                    isUnlockedString = entries[1];

                    if(isUnlockedString == "true")
                    {
                        isUnlocked = true;
                    }
                    else
                    {
                        isUnlocked = false;
                    }
                }

                Achievement.achievements[achievementName].setUnlocked(isUnlocked);

                

                   
            }
        }
        while (line != null);

        reader.Close();





    }

    public static void saveAchievements()
    {
        StreamWriter writer;
        string json;
        string fileName = "Achievements";
        string line;
        string userName = PlayerPrefs.GetString("username");
        string totalOutput = "";
      
        string isUnlockedString = "";
        bool isUnlocked = false;

            repeatCounts = 0;

            UnlockableAchievement[] unlockable = new UnlockableAchievement[Achievement.achievements.Count];
            Achievement.achievements.Values.CopyTo(unlockable, 0);

            


            if (userName != null)
            {
                if (File.Exists(Application.dataPath + "/Resources/" + userName + fileName + ".json"))
                {
                    //StreamWriter writer = new StreamWriter(Application.dataPath + "/Resources/" + fileName + ".json");
                    writer = new StreamWriter(Application.dataPath + "/Resources/" + userName + fileName + ".json");
                }
                else
                {
                    File.Create(Application.dataPath + "/Resources/" + userName + fileName + ".json");
                    writer = new StreamWriter(Application.dataPath + "/Resources/" + userName + fileName + ".json");
                }
            }
            else
            {
                writer = new StreamWriter(Application.dataPath + "/Resources/" + fileName + ".json");
            }








            for (int count = 0; count < unlockable.Length; count++)
            {
                if (unlockable[count].getTag() != levelTag)
                {
                    if (unlockable[count].getIsUnlocked() == true)
                    {
                        isUnlockedString = "true";
                    }
                    else
                    {
                        isUnlockedString = "false";
                    }

                
                totalOutput = totalOutput + unlockable[count].name + "," + isUnlockedString + "\n";
                Debug.Log("total output is currently \n" + totalOutput);
                // writer.WriteLine(unlockable[count].name + "," + isUnlockedString);
                }
            }
        
        writer.Write(totalOutput);

            writer.Close();


        }

    public void displayTaggedAchievements(string tag)
    {
        UnlockableAchievement[] unlockable = new UnlockableAchievement[Achievement.achievements.Count];
        Achievement.achievements.Values.CopyTo(unlockable, 0);

        if (tag != "All")
        {
            for (int count = 0; count < unlockable.Length; count++)
            {
                if (unlockable[count].getTag() != tag)
                {
                    unlockable[count].getAttached().SetActive(false);
                }
                else
                {
                    unlockable[count].getAttached().SetActive(true);
                }
            }
        }
        else
        {
            for (int count = 0; count < unlockable.Length; count++)
            {

                unlockable[count].getAttached().SetActive(true);

            }
        }
    }


}

   




	    
	

