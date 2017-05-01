using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoviePlayer : MonoBehaviour {

    public MovieTexture engarde;
    public MovieTexture lunge;
    public MovieTexture move;

    MovieTexture currentMovie;
    int movieNumber = 0;
    const int MAXMOVIENUMBER = 3;
    AudioSource audio;
    Renderer renderer;

    RawImage ri;


	// Use this for initialization
	void Start () {

        //renderer = this.GetComponent<Renderer>();
        //renderer.material.mainTexture = engarde;

        ri = this.GetComponent<RawImage>();
        ri.texture = engarde;
        
        currentMovie = engarde;
        audio = this.GetComponent<AudioSource>();
        audio.clip = currentMovie.audioClip;
        //currentMovie.Play();
        //engarde.Play();
        //audio.Play();   



    }

    public void skipForward()
    {
        currentMovie.Stop();
        audio.Stop();
        if (movieNumber + 1 > MAXMOVIENUMBER)
        {
            movieNumber = 1;
            Achievement.achievements["watchAllVideos"].areRequirementsMet(0, true);
        }
        else
        {
            movieNumber++;
        }

        switch (movieNumber)
        {
            case 1:
                currentMovie = engarde;
                break;
            case 2:
                currentMovie = move;
                break;
            case 3:
                currentMovie = lunge;
                break;
        }

        setupVideo();

    }

    public void skipBack()
    {
        currentMovie.Stop();
        audio.Stop();
        if (movieNumber - 1 <= 0)
        {
            movieNumber = MAXMOVIENUMBER;
        }
        else
        {
            movieNumber--;
        }

        switch (movieNumber)
        {
            case 1:
                currentMovie = engarde;
                break;
            case 2:
                currentMovie = move;
                break;
            case 3:
                currentMovie = lunge;
                break;
        }

        setupVideo();

    }

    void setupVideo()
    {
        //renderer.material.mainTexture = currentMovie;
        ri.texture = currentMovie;
        audio.clip = currentMovie.audioClip;
        currentMovie.Play();
        audio.Play();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.P))
        {
            currentMovie.Stop();
            audio.Stop();
            if(movieNumber + 1 > MAXMOVIENUMBER)
            {
                movieNumber = 1;
            }
            else
            {
                movieNumber++;
            }

            switch (movieNumber)
            {
                case 1:
                    currentMovie = engarde;
                    break;
                case 2:
                    currentMovie = move;
                    break;
                case 3:
                    currentMovie = lunge;
                    break;
            }

            setupVideo();

        }
		
	}
}
