using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour {

    private GameObject hitObject;
    private float hangryValue;
    public float hangrySpeed = 1.0f;
    public float nomValue = 40.0f;
    public float badnomValue = 50.0f;
    public Color cauldronColor;
    private float maxValue = 255.0f;
    private RuneStone fedStone;
    private MeshRenderer _meshRend;
    public GameObject splashPrefab;
    private GameObject spawnedSplash;

	// Use this for initialization
	void Start () {
        _meshRend = GetComponent<MeshRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        hangryValue += hangrySpeed * Time.deltaTime;
        hangryValue /= maxValue;
        cauldronColor.g = Mathf.Clamp(cauldronColor.g - hangryValue, 0.0f, 1.0f);
        cauldronColor.b = Mathf.Clamp(cauldronColor.b - hangryValue, 0.0f, 1.0f);
        _meshRend.material.color = cauldronColor;
        if (hangryValue >= 1.0f)
        {
            TooHangry();
        }
	}
    void OnCollisionEnter(Collision collision)
    {
        hitObject = collision.gameObject;
        if (hitObject.tag == "Interactable")
        {
            if (hitObject.name == "RuneStone(Clone)")
            {
                fedStone = hitObject.GetComponent<RuneStone>();
                if (fedStone.goodStone == true)
                {
                    hangryValue -= nomValue;
                    spawnedSplash = Instantiate(splashPrefab, hitObject.transform.position + new Vector3(0, 0.3f, 0), Quaternion.Euler(0,0,0));
                    var mainSplash = spawnedSplash.GetComponent<ParticleSystem>().main;
                    mainSplash.startColor = Color.green;
                }
                else
                {
                    hangryValue += badnomValue;
                    spawnedSplash = Instantiate(splashPrefab, hitObject.transform.position + new Vector3(0, 0.3f, 0), Quaternion.Euler(0, 0, 0));
                    var mainSplash = spawnedSplash.GetComponent<ParticleSystem>().main;
                    mainSplash.startColor = Color.red;
                }

            }
            Destroy(hitObject);
        }
    }
    void TooHangry()
    {
        //die lol clap xd
    }
}
