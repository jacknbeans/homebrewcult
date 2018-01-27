using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientManager : MonoBehaviour {

    public string[] requestString;
    public string[] responseString;

    public Text dialogTextBox;
    private int currentCharacterInt;
    public float nextCharacterDelay;
    private float nextCharacterTimer;
    private string chosenText;

    private bool ghostSummoned;
    private bool dialogRead;

    public GameObject clientPrefab;
    public Vector3 clientPos;
    private Client_Core currentClient;

	// Use this for initialization
	void Start () {
        SpawnClient();
        dialogTextBox.text = " ";
	}
	
	// Update is called once per frame
	void Update () {
        if(!dialogRead)
            UpdateText();
        if(Input.GetMouseButtonDown(0))
        {
            if(!ghostSummoned)
            {
                if (!dialogRead)
                {
                    dialogTextBox.text = " ";
                    dialogRead = true;
                }
                else
                    SummonGhost();
            }
            else
            {
                if (!dialogRead)
                {
                    dialogTextBox.text = " ";
                    dialogRead = true;
                }
                else
                    NextClient();
            }
        }
	}

    void SpawnClient()
    {
        currentClient = Instantiate(clientPrefab, clientPos, Quaternion.identity, null).GetComponent<Client_Core>();
        currentClient.rootPos = clientPos;
        ghostSummoned = false;
        chosenText = requestString[Random.Range(0, requestString.Length)];
        dialogRead = false;
        currentCharacterInt = 0;
    }

    void SummonGhost()
    {
        ghostSummoned = true;
        chosenText = responseString[Random.Range(0, responseString.Length)];
        dialogRead = false;
        currentCharacterInt = 0;
    }

    void NextClient()
    {
        currentClient.completed = true;
        SpawnClient();
    }

    void UpdateText()
    {
        dialogTextBox.text = chosenText.Substring(0, currentCharacterInt);
        nextCharacterTimer -= Time.deltaTime;
        if(nextCharacterTimer <= 0)
        {
            nextCharacterTimer = nextCharacterDelay;
            if(currentCharacterInt < chosenText.Length)
                currentCharacterInt++;
        }
    }
}
