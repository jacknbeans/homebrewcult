using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneStone : MonoBehaviour {

    public Material[] images;
    public bool goodStone;
    public float chanceBump = 0.5f;
    public Light runeLight;
    public MeshRenderer runeRend;

	// Use this for initialization
	void Start () {
        runeRend.material = images[Random.Range(0, images.Length)];
        goodStone = (Random.value < chanceBump);
        if (goodStone != true)
            runeLight.color = Color.red;
        else
            runeLight.color = Color.green;


        Destroy(gameObject, 10.0f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
