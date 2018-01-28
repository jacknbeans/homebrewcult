using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerObject : GameplayBehaviour {
    public GameObject spawnObj;
    public Vector3 spawnVector;
    public float shakeBump;
    public float timerBump;
    private Vector3 lastPosition;
    private float distance;
    private float timer;
    private Transform spawnTransform;

    private float _timer;

	// Use this for initialization
	void Start () {
        distance = 0f;
        lastPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _timer += Time.deltaTime;
	    if (_timer >= 0.3f)
	    {
	        Stress(Random.Range(-50.0f, 50.0f));
	        _timer = 0.0f;
	    }

        distance = Vector3.Distance(lastPosition, gameObject.transform.position);
        timer += 1f;
       
	if (distance >= shakeBump && timer >= timerBump)
        {
            var newGameobject = Instantiate(spawnObj);
            newGameobject.transform.position = gameObject.transform.position + new Vector3(0,0.5f,0.1f);
            newGameobject.transform.rotation = Quaternion.Euler(spawnVector + new Vector3(0, Random.Range(0, 180), 0));
            timer = 0f;
        }
        distance = 0f;
        lastPosition = gameObject.transform.position;
	}
}
