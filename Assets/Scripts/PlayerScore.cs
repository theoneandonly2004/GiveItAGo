using UnityEngine;
using System.Collections;
using System;
using System.IO;

[Serializable]
public class ScoreStore
{
    
    int length;
    int random = 46;
    string name = "bob";
    public PlayerScoreClass[] exerciseScores;

    public ScoreStore(PlayerScoreClass[] exerciseScores)
    {
        this.exerciseScores = exerciseScores;
        length = exerciseScores.Length;
    }
}

[Serializable]
public class PlayerScoreClass
{
    string exerciseName;
   public int lastSessionScore = 0;
   public int highestScore = 0;
    int currentScore = 0;



    public PlayerScoreClass(string exerciseName)
    {
        this.exerciseName = exerciseName;
    }

    public void setScore(int score)
    {
        currentScore = score;

    }

    public void setFinalScore(int score)
    {
        lastSessionScore = score;
        if (score > highestScore)
        {
            highestScore = score;
        }
    }
}

public class PlayerScore : MonoBehaviour {
    string[] exerciseNames = { "balloon pop", "target practice", "keep distance" , "hit zone" };
    PlayerScoreClass[] exerciseScores;

    // Use this for initialization
    void Start () {
        exerciseScores = new PlayerScoreClass[4];
        
        for(int count = 0; count < exerciseScores.Length; count++)
        {
            exerciseScores[count] = new PlayerScoreClass(exerciseNames[count]);
        }

        readFromFile();
        //outputToFile();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public PlayerScoreClass getExerciseScore(int position)
    {
        try
        {
            PlayerScoreClass found = exerciseScores[position];
            return found;
        }
        catch(IndexOutOfRangeException ex)
        {
            return null;
        }
    }

   public void outputToFile()
    {
        ScoreStore scoreStore = new ScoreStore(exerciseScores);
        StreamWriter writer = new StreamWriter(Application.dataPath + "//Resources//playerScore.json");
        string json = JsonUtility.ToJson(scoreStore,true);
        writer.Write(json);
        writer.Close();



    }


   public void readFromFile()
    {
        
        StreamReader reader = new StreamReader(Application.dataPath + "//Resources//playerScore.json");
        string json = reader.ReadToEnd();
        ScoreStore store = JsonUtility.FromJson<ScoreStore>(json);
        reader.Close();

        for (int count = 0; count < exerciseScores.Length; count++)
        {          
            exerciseScores[count] = store.exerciseScores[count];
        }


    }
}
