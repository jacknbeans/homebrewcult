using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneStone : MonoBehaviour {
    public Sprite[] images;


	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().sprite = images[Random.Range(0, images.Length)];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
