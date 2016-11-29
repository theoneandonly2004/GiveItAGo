using UnityEngine;
using System.Collections;

public class BalloonControl : MonoBehaviour {

    public GameObject sword;
     GameObject spawner;

    ObjectSpawner spawn;
    bool isSwordAlive;
    int balloonNumber;
    int secondLevelSpawn = 1000;
    int thirdLevelSpawn = 5000;
    int balloonType;
    GameObject player;
    GameObject spawnedSword;
    Vector3 position;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Foil sword");
        sword = this.transform.GetChild(0).gameObject;
        balloonNumber = 0;
        this.name = "balloonTarget";
        isSwordAlive = sword.activeInHierarchy;
        position = this.transform.position;
        spawner = GameObject.Find("ObjectSpawner");
        spawn = spawner.GetComponent<ObjectSpawner>();

	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(player.transform);
    }

    public void setBalloonType(int number)
    {
        
        //balloonNumber = number;
        balloonNumber = Random.Range(0, number);
        
        if (balloonNumber < secondLevelSpawn)
        {
            sword.SetActive(false);
            balloonType = 0;         

        }
        else if (balloonNumber >= secondLevelSpawn && balloonNumber < thirdLevelSpawn)
        {
            sword.SetActive(true);
            this.gameObject.GetComponent<Renderer>().material.color = Color.black;
            balloonType = 1;
           //spawnedSword =Instantiate(sword);
           // spawnedSword.transform.position = position;
        }
        else
        {
            sword.SetActive(true);            
            balloonType = 2;
            this.gameObject.GetComponent<Renderer>().material.color = Color.cyan;       
        }
    }

   /* void OnTriggerEnter(Collider collision)
    {
        if(collision.name == this.name)
        {
            Destroy(collision.gameObject);
            spawn.Spawn();
        }
    }*/

    public int getBalloonType()
    {
        //0 for default , 1 for cyan , 2 for black
        return balloonType;
    }

    public int getBalloonNumber()
    {
        return balloonNumber;
    }

   public void setIsSwordAlive(bool isActive)
    {
        isSwordAlive = isActive;
    }

    public bool getIsSwordAlive()
    {
        return isSwordAlive;
    }

    public void setPosition(Vector3 position)
    {
        this.position = position;
    }

}
