using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneStone : MonoBehaviour {
    public Material[] images;
    public bool goodStone;
    public float chanceBump = 0.5f;
    private ParticleSystem.MainModule myGlow;
	// Use this for initialization
	void Start () {
        myGlow = gameObject.GetComponent<ParticleSystem>().main;
        GetComponent<MeshRenderer>().material = images[Random.Range(0, images.Length)];
        goodStone = (Random.value < chanceBump);
        if (goodStone != true)
        {
            myGlow.startColor = Color.red;
        }
        Destroy(gameObject, 10.0f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
