using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour {

    public Sprite[] logoAnim;
    public float animFrameDelay;
    private float animTimer;
    public int soundPlayAtFrame;

    private int animFrameUsed = 0;
    private bool soundPlayed = false;

    private AudioSource logoAudioSource;
    private SpriteRenderer logoSpriteRend;

	// Use this for initialization
	void Start () {
        animTimer = animFrameDelay;
        logoAudioSource = GetComponent<AudioSource>();
        logoSpriteRend = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        //change sprite
        logoSpriteRend.sprite = logoAnim[animFrameUsed];

        //animation
        animTimer -= Time.deltaTime;
        if (animTimer <= 0 && animFrameUsed < logoAnim.Length-1)
        {
            animTimer = animFrameDelay;
            animFrameUsed++;
        }
        if (animTimer <= 0 && animFrameUsed == logoAnim.Length-1)
        {
            NextScene();
        }

        //sound
        if (animFrameUsed == soundPlayAtFrame)
        {
            logoAudioSource.Play();
            soundPlayed = true;
        }
	}

    void NextScene()
    {
        SceneManager.LoadScene(1);
    }
}
