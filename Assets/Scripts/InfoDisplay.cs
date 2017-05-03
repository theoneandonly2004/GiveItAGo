using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoDisplay : MonoBehaviour {

    public bool canDisplay = false;
    Text displayText;
    Canvas achievementCanvas;
    public LayerMask achievementsLayer;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
       
      
        if (canDisplay)
        {
            
            Transform cam = this.transform;             
            Ray finderRay = new Ray(cam.position, cam.forward);
            RaycastHit hit;
            Debug.DrawRay(cam.position,cam.forward,Color.black);
            if (Physics.Raycast(finderRay, out hit, achievementsLayer))
            {

                if (hit.collider.tag == "Achievement")
                {
                    achievementCanvas.gameObject.SetActive(true);
                    Debug.Log(hit.collider.name + " was hit");
                    UnlockableAchievement achievement = Achievement.achievements[hit.collider.name];
                    if (achievement.getIsUnlocked())
                    {
                        displayText.text = achievement.getUnlockedText();
                    }
                    else
                    {
                        displayText.text ="LOCKED:" + achievement.getLockedText();
                    }
                }

                Debug.Log("raycast found " + hit.collider.name);
            }
            else
            {
                achievementCanvas.gameObject.SetActive(false);
            }
        }

    }

    public void SetupToDisplay(Text textDisplay , Canvas achievementsCanvas)
    {
        this.displayText = textDisplay;
        this.achievementCanvas = achievementsCanvas;

        this.achievementCanvas.worldCamera = this.GetComponent<Camera>();
        canDisplay = true;   
    }

    void FixedUpdate()
    {
       
    }

    public void setCanDisplay(bool display)
    {
        canDisplay = display;
    }
}
