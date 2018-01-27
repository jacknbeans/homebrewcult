using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client_BodyMovement : MonoBehaviour {

    public Material[] spriteArray;
    public AnimationCurve moveCurve;
    public Vector3 moveOffsetAmount;
    public float moveSpeed;
    private float moveTimer;
    private Vector3 moveStartPos;

    public bool isEye;
    public float blinkChance;
    private float blinkDuration;

    private Renderer thisSpriteRend;

	// Use this for initialization
	void Start () {
        thisSpriteRend = GetComponent<Renderer>();
        thisSpriteRend.material = spriteArray[Random.Range(0, spriteArray.Length-1)];

        moveStartPos = transform.position;
        
	}
	
	// Update is called once per frame
	void Update () {
        //eye functions
        if (isEye)
            EyeSprite();

        //wavey movement timer
        moveTimer += Time.deltaTime * moveSpeed;
        if (moveTimer > 1)
            moveTimer = 0;
        //wavey movement change position
        transform.position = moveStartPos + (moveOffsetAmount * moveCurve.Evaluate(moveTimer));
	}

    void EyeSprite()
    {
        //blink
        if (Random.Range(0, blinkChance) < 1)
        {
            thisSpriteRend.material = spriteArray[0];
            blinkDuration = 0.2f;
        }
        if (blinkDuration > 0)
            blinkDuration -= Time.deltaTime;
        else
            thisSpriteRend.material = spriteArray[1];
    }
}
