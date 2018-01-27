using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneStone : MonoBehaviour {
    public Sprite[] images;
    public bool goodStone;
    public float chanceBump = 0.5f;
	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().sprite = images[Random.Range(0, images.Length)];
        goodStone = (Random.value < chanceBump);
        if (goodStone != true)
        {
            gameObject.GetComponent<ParticleSystem>().Stop();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
