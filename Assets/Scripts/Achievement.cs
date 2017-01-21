using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnlockableAchievement
{
   public string name;
    string unlockedText = "";
    string tag = "";
    int requiredScore = 0;
    bool isUnlocked = false;
    Texture locked;
    Texture unlocked;
    GameObject attached;
    AudioClip clip;
    AudioSource source;
    public UnlockableAchievement(string name , string unlockedText,string tag ,int requiredScore)
    {
        this.tag = tag;
        this.unlockedText = unlockedText;
        this.name = name;
        this.requiredScore = requiredScore;
        //attached = GameObject.Find(name);
        //attached.GetComponent<AchievementMouseOver>().setDisplaytext(unlockedText);
        clip = Resources.Load("AchievementSound") as AudioClip;


        //source = attached.GetComponent<AudioSource>();
        //source.clip = clip;
        
        


  

        // locked = Resources.Load(name+"-locked") as Texture;
        // unlocked = Resources.Load(name + "-unlocked") as Texture;

        locked = Resources.Load("locked2") as Texture;
        unlocked = Resources.Load("unlocked2") as Texture;

        

        Achievement.achievements.Add(name, this);

        //Debug.Log(this.name);
        managetexture();


    }

    public string getTag()
    {
        return tag;
    }

    public GameObject getAttached()
    {
        return attached;
    }

    public bool getIsUnlocked()
    {
        return isUnlocked;
    }

    public bool areRequirementsMet(int score , bool displayAchievement)
    {
        Vector3 playPos = GameObject.Find("Player").transform.position;
        
        if (score >= requiredScore)
            {
            //AchievementList.saveAchievements();
            if (!isUnlocked && displayAchievement)
                {
                Achievement.achievementUnlocked.SetActive(true);
                Achievement.achievementUnlocked.GetComponentInChildren<Text>().text = unlockedText;
                //source.clip = clip;
                //source.Play();
                setUnlocked(true);

                //AchievementList.saveAchievements();
                AudioSource.PlayClipAtPoint(clip, playPos);
                return true;
               // AchievementList.saveAchievements();
                }
              else
                {

                setUnlocked(true);

                return true;
                }

           
            
                }
           else
            {
            //AchievementList.saveAchievements();
            setUnlocked(false);
                //managetexture();
                //return false;
            }
        
        return false;
    }

    public IEnumerator hideAchievementUnlockedPane()
    {
        yield return new WaitForSeconds(1.0f);
        Achievement.achievementUnlocked.SetActive(false);
    }

    public void setUnlockedText(string unlockedText)
    {
        this.unlockedText = unlockedText;
    }

    public void setUnlocked(bool isUnlocked)
    {
        this.isUnlocked = isUnlocked;
        managetexture();
    }

    void managetexture()
    {
        if (isUnlocked)
        {
            //attached.GetComponent<RawImage>().texture = unlocked;
            //attached.GetComponent<RawImage>().color = Color.blue;
        }
        else
        {
            //attached.GetComponent<RawImage>().texture = locked;
            //attached.GetComponent<RawImage>().color = Color.red;
        }
    }


}



public class Achievement : MonoBehaviour {

    public static Dictionary<string, UnlockableAchievement> achievements;
    bool islocked;
    int score = 0;
    public GameObject tempachievement;
    public static GameObject achievementUnlocked;

    void Awake()
    {
        achievements = new Dictionary<string, UnlockableAchievement>();
        AchievementList.registerAchievements();
        AchievementList.readAchievements();
    }

	// Use this for initialization
	void Start () {
      //  achievementUnlocked = tempachievement;
      //  achievementUnlocked.SetActive(false);
        //AchievementList.saveAchievements();
        
    }
	
	// Update is called once per frame
	void Update () {

	}



    public Dictionary<string , UnlockableAchievement> getAchievementDictionary()
    {
        return achievements;
    }
}
