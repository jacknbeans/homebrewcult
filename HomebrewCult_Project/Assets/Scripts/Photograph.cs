using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photograph : MonoBehaviour {

    private bool handedOver;
    public bool spiritSummoned;
    private Core.Interactable iScript;
    private ClientManager clientScript;
    private Rigidbody rBody;

    //effects
    public ParticleSystem[] ghostFx;
    public Light lightSource;
    public Renderer photographRenderer;
    public Material summonedPhotographMat;
    private bool FxPlayed;

    public AnimationCurve lightCurve;
    public float lightCurveSpeed;
    public float lightIntensity;
    private float lightTimer;

	// Use this for initialization
	void Start () {
        iScript = GetComponent<Core.Interactable>();
        clientScript = FindObjectOfType<ClientManager>();
        rBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (iScript.IsHeld() && handedOver == false)
        {        
            clientScript.DialogRead();
            handedOver = true;
        }
        if (!iScript.IsHeld() && handedOver == true)
        {
            StartPhysics();
        }

        if (spiritSummoned)
            PlaySummonEffects();
	}

    void StartPhysics()
    {
        rBody.isKinematic = false;
        rBody.useGravity = true;
        handedOver = true;
        clientScript.DialogRead();
    }

    void PlaySummonEffects()
    {
        if (!FxPlayed)
        {
            for (int i = 0; i < ghostFx.Length; i++)
                ghostFx[i].Play();
        }

        if (lightTimer < 1.25)
        {
            lightTimer += Time.deltaTime * lightCurveSpeed;
            if (lightTimer > 1)
                photographRenderer.material = summonedPhotographMat;
        }
        lightSource.intensity = lightIntensity * lightCurve.Evaluate(lightTimer);
        FxPlayed = true;
    }
}
