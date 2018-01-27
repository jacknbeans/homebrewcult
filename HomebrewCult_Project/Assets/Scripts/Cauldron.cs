using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour {

    private GameObject hitObject;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision collision)
    {
        hitObject = collision.gameObject;
        if (hitObject.tag == "Interactable")
        {
            Destroy(hitObject);
        }
    }
}
