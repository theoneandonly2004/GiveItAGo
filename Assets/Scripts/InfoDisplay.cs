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
            RaycastHit hit;
            Ray finderRay = new Ray(transform.position, Vector3.forward);

            if (Physics.Raycast(finderRay, out hit, 50))
            {
                if (hit.collider.tag == "Achievement")
                {
                    Debug.Log(hit.collider.name + " was hit");
                }
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
