using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {
    private GameObject hitObject;
    public GameObject respawnChild;
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collision)
    {
        hitObject = collision.gameObject;
        if (hitObject.tag == "Essential")
        {
            var respawnPosition = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-0.2f, 0.1f));
            hitObject.transform.position = respawnChild.transform.position + respawnPosition;
        }
    }
}
