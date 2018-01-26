using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour {

    public Sprite[] logoAnim;
    public float animFrameDelay;
    public float soundPlayDelay;
    private float animTimer;
    private float soundTimer;

    private AudioSource logoAudioSource;

	// Use this for initialization
	void Start () {
        animTimer = animFrameDelay;
        soundTimer = soundPlayDelay;
        logoAudioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        animTimer -= Time.deltaTime;
        if (animTimer <= 0)
            animTimer = animFrameDelay;
        soundPlayDelay -= Time.deltaTime;
        if (soundPlayDelay <= 0)
            logoAudioSource.Play();
	}
}
