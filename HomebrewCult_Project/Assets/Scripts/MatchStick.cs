using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchStick : MonoBehaviour {

    public Rigidbody matchBody;
    public float lightBump;
    private float distance;
    public Vector3 lastPosition;
    private bool stroking;

	// Use this for initialization
	void Start () {
        lastPosition = transform.position;	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        distance = Vector3.Distance(lastPosition, gameObject.transform.position);

        if (distance >= lightBump)
        {
            Debug.Log("fire!!!");
        }

        lastPosition = gameObject.transform.position;
        stroking = false;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "StrokeBit")
        {
            //stroking = true;
        }
    }
}



