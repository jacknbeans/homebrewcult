using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client_Core : MonoBehaviour {

    //movement
    public Vector3 rootPos;
    public AnimationCurve moveCurve;
    public float moveTimer;
    public float moveSpeed;
    public Vector3 moveOffset;

    public bool started;
    //[HideInInspector]
    public bool completed;

	// Use this for initialization
	void Start () {
        moveTimer = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(!started)
        {
            moveTimer -= Time.deltaTime * moveSpeed;
            transform.position = rootPos + new Vector3(moveOffset.x, moveOffset.y, moveOffset.z) * moveCurve.Evaluate(moveTimer);
            if(moveTimer < 0)
            {
                started = true;
            }
        }
        if(started && completed)
        {
            moveTimer += Time.deltaTime * moveSpeed;
            transform.position = rootPos + new Vector3(-moveOffset.x, moveOffset.y, moveOffset.z) * moveCurve.Evaluate(moveTimer);
            if (moveTimer > 1)
            {
                Destroy(gameObject);
            }
        }
	}
}
