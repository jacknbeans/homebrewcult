using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour {

    public Sprite[] logoAnim;
    public float animFrameDelay;
    public float soundPlayDelay;
    private float animTimer;
    private float soundTimer;

    private int animFrameUsed = 0;
    private bool soundPlayed = false;

    private AudioSource logoAudioSource;
    private SpriteRenderer logoSpriteRend;

	// Use this for initialization
	void Start () {
        animTimer = animFrameDelay;
        soundTimer = soundPlayDelay;
        logoAudioSource = GetComponent<AudioSource>();
        logoSpriteRend = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        //change sprite
        logoSpriteRend.sprite = logoAnim[animFrameUsed];

        //animation
        animTimer -= Time.deltaTime;
        if (animTimer <= 0 && animFrameUsed < logoAnim.Length)
        {
            animTimer = animFrameDelay;
            animFrameUsed++;
        }
        else
        {
            NextScene();
        }

        //sound
        soundTimer -= Time.deltaTime;
        if (soundTimer <= 0 && !soundPlayed)
        {
            logoAudioSource.Play();
            soundPlayed = true;
        }
	}

    void NextScene()
    {
        Debug.Log("Next Scene");
    }
}
