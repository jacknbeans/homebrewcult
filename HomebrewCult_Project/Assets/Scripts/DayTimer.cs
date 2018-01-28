using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTimer : MonoBehaviour {

    public float daySpeed;
    private bool dayEnded;

    public GameObject shortHand;
    public GameObject longHand;

    private float currentDayTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!dayEnded)
        {
            //count down until the end of day
            currentDayTime += Time.deltaTime * daySpeed;
            if (currentDayTime >= 480 && !dayEnded)
                EndOfDay();
        }
        RotateHands();
	}

    void EndOfDay()
    {
        Debug.Log("Time's Up!");
        dayEnded = true;
    }

    void RotateHands()
    {
        shortHand.transform.Rotate(0, ((6f * Time.deltaTime) * daySpeed)/12, 0);
        longHand.transform.Rotate(0, (6f * Time.deltaTime) * daySpeed, 0);
    }
}
