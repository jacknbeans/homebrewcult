using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuijaPlanchetteMovement : MonoBehaviour {

    public Light planchetteLight;
    public float lightIntensity;
    [Range(1.0f, 5.0f)]
    public float effectSpeed;
    public float threshold = 0.00001f;

    private Vector3 velocity = Vector3.zero;

    private float effectTimer;
    private float pauseTimer;
    public AnimationCurve planchetteShakeEffect;

	public void Move(Transform letterTransform, float smoothTime)
    {
        // Planchette moves from current position, to the letter
        transform.position = Vector3.SmoothDamp(transform.position, letterTransform.position, ref velocity, smoothTime);
    }

    public bool Effect()
    {
        // Planchette's spotlight will blink when it stays on a letter
        effectTimer += Time.deltaTime * effectSpeed;
        planchetteLight.intensity = Mathf.PingPong(effectTimer, lightIntensity);

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + planchetteShakeEffect.Evaluate(effectTimer), 0);

        if (planchetteLight.intensity - threshold <= 0.0f)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            planchetteLight.intensity = 0;
            effectTimer = 0.0f;
            return true;
        }

        return false;
    }

    public bool Pause(float pauseTime)
    {
        pauseTimer += Time.deltaTime;
        if (pauseTimer >= pauseTime)
        {
            pauseTimer = 0;
            return true;
        }
        return false;
    }
}
