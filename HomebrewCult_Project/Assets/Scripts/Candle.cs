using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour {

    public Vector2 timeDurationBetween;
    private float remainingLitTime;
    private bool isLit;

    public Vector2 flickerDelay;
    private float flickerTimer;

    public Vector2 minMaxIntensity;
    public Color[] flamePalette;

    public Light flameLight;
    public ParticleSystem flameFx;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isLit)
        {
            //change color/intensity timer
            flickerTimer -= Time.deltaTime;
            if (flickerTimer <= 0)
            {
                flameLight.intensity = Random.Range(minMaxIntensity.x, minMaxIntensity.y);
                flameLight.color = flamePalette[Random.Range(0, flamePalette.Length)];
                flickerTimer = Random.Range(flickerDelay.x, flickerDelay.y);
            }
            //lit remaining timer
            remainingLitTime -= Time.deltaTime;
            if (remainingLitTime <= 0)
            {
                flameLight.intensity = 0;
                flameFx.Stop();
                isLit = false;
            }
        }
	}

    public void GetLit()
    {
        if (!isLit)
        {
            remainingLitTime = Random.Range(timeDurationBetween.x, timeDurationBetween.y);
            flameLight.intensity = Random.Range(minMaxIntensity.x, minMaxIntensity.y);
            flameFx.Play();
            isLit = true;
        }
    }
}
