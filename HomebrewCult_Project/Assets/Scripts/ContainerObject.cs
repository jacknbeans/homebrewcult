using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerObject : GameplayBehaviour
{
    public GameObject spawnObj;
    public Vector3 spawnPosOffset;
    public Vector2 spawnRotOffset;
    public float shakeBump;
    public float timerBump;

    private Vector3 lastPosition;
    private float distance;
    private float timer;
    private Transform spawnTransform;

    private bool soundON = false;
    public bool enableSound = true;
    private AudioSource shakeSound;
    private float soundTimer;
    
	// Use this for initialization
	void Start ()
    {
        distance = 0f;
        lastPosition = gameObject.transform.position;
        if (gameObject.GetComponent<AudioSource>() != null)
        {
            shakeSound = gameObject.GetComponent<AudioSource>();
        }
        else
        {
            enableSound = false;
        }
	}
	
	// Update is called once per frame
	void Update ()
	{
        distance = Vector3.Distance(lastPosition, gameObject.transform.position);
        timer += Time.deltaTime;
        soundTimer += 1.0f * Time.deltaTime;
       if (distance >= shakeBump && enableSound && !shakeSound.isPlaying)
        {
            shakeSound.Play();
            soundON = true;
        }
       else if (distance < 0.1f && soundTimer < 0.5f && enableSound)
        { 
            shakeSound.Stop();
            soundON = false;
        }
       else if (soundTimer > 0.5f)
        {
            soundTimer = 0.0f;
        }

	    if (distance >= shakeBump && timer >= timerBump)
        {
            var newGameobject = Instantiate(spawnObj);
            newGameobject.transform.position = gameObject.transform.position + spawnPosOffset;
            newGameobject.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(spawnRotOffset.x, spawnRotOffset.y), 0));
            timer = 0f;
        }


        distance = 0f;
        lastPosition = gameObject.transform.position;
	}
}
