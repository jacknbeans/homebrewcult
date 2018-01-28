using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : GameplayBehaviour
{
    private GameObject hitObject;
    public float hangrySpeed = 1.0f;
    public float nomValue = 40.0f;
    public float badnomValue = 50.0f;
    public Color cauldronColor;
    public float stressToAdd = 0.05f;

    private float hangryValue = 255.0f;
    private float maxValue = 255.0f;
    private RuneStone fedStone;
    public MeshRenderer meshRend;
    public GameObject splashPrefab;
    private GameObject spawnedSplash;

	// Use this for initialization
	void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
        hangryValue = Mathf.Clamp(hangryValue - hangrySpeed * Time.deltaTime, 0.0f, maxValue);

        cauldronColor.g = Mathf.Clamp(hangryValue / maxValue, 0.0f, 1.0f);
        cauldronColor.b = Mathf.Clamp(hangryValue / maxValue, 0.0f, 1.0f);
        meshRend.material.color = cauldronColor;

        if (hangryValue <= 0.0f)
        {
            TooHangry();
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        hitObject = collision.gameObject;
        if (hitObject.CompareTag("RuneStone"))
        {
            fedStone = hitObject.GetComponent<RuneStone>();
            if (fedStone == null)
            {
                Debug.LogError("No RuneStone script found on object tagged as being a RuneStone!");
            }

            if (fedStone.goodStone)
            {
                hangryValue += nomValue;
                spawnedSplash = Instantiate(splashPrefab, hitObject.transform.position + new Vector3(0, 0.3f, 0), Quaternion.Euler(0,0,0));
                var mainSplash = spawnedSplash.GetComponent<ParticleSystem>().main;
                mainSplash.startColor = Color.green;
            }
            else
            {
                hangryValue -= badnomValue;
                spawnedSplash = Instantiate(splashPrefab, hitObject.transform.position + new Vector3(0, 0.3f, 0), Quaternion.Euler(0, 0, 0));
                var mainSplash = spawnedSplash.GetComponent<ParticleSystem>().main;
                mainSplash.startColor = Color.red;
            }
            Destroy(hitObject);
        }
    }

    void TooHangry()
    {
        Stress(-stressToAdd);
    }
}
