using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoSlot : MonoBehaviour {

    private GameObject currentPhoto;
    private ClientManager clientScript;
    private bool recievedPhoto;
    private bool photoTaken;

	// Use this for initialization
	void Start () {
        currentPhoto = FindObjectOfType<Photograph>().gameObject;
        clientScript = FindObjectOfType<ClientManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if(recievedPhoto)
        {
            TakePhoto();
        }
        if(photoTaken)
        {
            clientScript.helpingClient = false;
            clientScript.photoGiven = false;
            clientScript.photoInsertSpawned = false;
            clientScript.DialogRead();
            Destroy(clientScript.photoInUse.gameObject);
            Destroy(gameObject);  
        }
	}

    void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject == currentPhoto && !recievedPhoto)
        {
            currentPhoto.GetComponent<Photograph>().enabled = false;
            currentPhoto.GetComponent<Core.Interactable>().enabled = false;
            currentPhoto.GetComponent<Rigidbody>().useGravity = false;
            currentPhoto.GetComponent<Rigidbody>().isKinematic = true;
            currentPhoto.layer = 0;
            currentPhoto.tag = "Untagged";
            recievedPhoto = true;
        }
    }

    void TakePhoto()
    {
        currentPhoto.transform.position = Vector3.Lerp(currentPhoto.transform.position, transform.position, 0.1f);
        currentPhoto.transform.rotation = Quaternion.Lerp(currentPhoto.transform.rotation, transform.rotation, 0.1f);
        if(Vector3.Distance(transform.position, currentPhoto.transform.position) < 0.1f && Quaternion.Angle(currentPhoto.transform.rotation, transform.rotation) < 1)
        {
            photoTaken = true;
        }
    }
}
