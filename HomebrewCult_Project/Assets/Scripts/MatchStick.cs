using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchStick : MonoBehaviour {

    public float lightBump;
    public float flameDuration;
    public Material burntMat;
    public Renderer matchStickMat;
    public ParticleSystem flameFx;

    private float distance;
    private Vector3 lastPosition;
    private bool stroking;
    private bool isLit;

	// Use this for initialization
	void Start () {
        lastPosition = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //check speed and collision with strokey bit
        distance = Vector3.Distance(lastPosition, gameObject.transform.position);
        lastPosition = gameObject.transform.position;
        if (distance > lightBump && stroking)
        {
            isLit = true;
        }

        //flame particle
        if (isLit)
        {
            if (!flameFx.isPlaying)
                flameFx.Play();
        }
        else
        {
            if (flameFx.isPlaying)
                flameFx.Stop();
        }

        stroking = false;
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "StrokeBit")
        {
            stroking = true;
            Debug.Log("touch");
        }
    }
}



