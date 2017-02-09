using UnityEngine;
using System.Collections;

public class InfoDisplay : MonoBehaviour {

    bool canDisplay = true;

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
            if (Physics.Raycast(finderRay, out hit, 150))
            {

                /*if (hit.collider.tag == "Achievement")
                {
                    Debug.Log(hit.collider.name + " was hit");
                }*/

                Debug.Log("raycast found " + hit.collider.name);
            }
        }

    }

    void FixedUpdate()
    {
       
    }

    public void setCanDisplay(bool display)
    {
        canDisplay = display;
    }
}
