using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientManager : MonoBehaviour
{
    public string[] requestString;
    public string[] responseString;

    public Text dialogTextBox;
    private int currentCharacterInt;
    public float nextCharacterDelay;
    private float nextCharacterTimer;
    private string chosenText;

    private bool ghostSummoned;
    private bool dialogRead;
    private bool playerSpeaking = false;

    public GameObject clientPrefab;
    public Vector3 clientPos;
    private Client_Core currentClient;

    private float soundTimer;
    private AudioSource blaSound;
    private bool playSound;

    private bool summoning;

	// Use this for initialization
	void Start () {
        SpawnClient();
	    dialogRead = true;
        dialogTextBox.text = " ";
        blaSound = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!dialogRead)
        {
            UpdateText();
            soundTimer += Time.deltaTime;
            if (soundTimer >= 0.15f && playSound == true)
            {
                BlaSound();
            soundTimer = 0.0f;
        }
        }
        /*if(Input.GetMouseButtonDown(0))
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
        }*/
    }

    public void StartDialogue()
    {
        dialogRead = false;
    }

    public bool IsSummoning()
    {
        return summoning;
    }

    public void DialogRead()
    {
        if (!ghostSummoned)
        {
            dialogTextBox.text = " ";
            dialogRead = true;
            summoning = true;
        }
        else
        {
            if (!summoning)
            {
                dialogTextBox.text = " ";
                NextClient();
            }
        }
    }

    public void FinishedSummoning()
    {
        summoning = false;
    }

    public void SpawnClient()
    {
        currentClient = Instantiate(clientPrefab, clientPos, Quaternion.identity, null).GetComponent<Client_Core>();
        currentClient.rootPos = clientPos;
        ghostSummoned = false;
        chosenText = requestString[Random.Range(0, requestString.Length)];
        dialogRead = false;
        currentCharacterInt = 0;
    }

    public void SummonGhost()
    {
        playerSpeaking = false;
        ghostSummoned = true;
        chosenText = responseString[Random.Range(0, responseString.Length)];
        dialogRead = false;
        currentCharacterInt = 0;
    }

    public void NextClient()
    {
        currentClient.completed = true;
        playerSpeaking = true;
        SpawnClient();
    }

    public void UpdateText()
    {
        dialogTextBox.text = chosenText.Substring(0, currentCharacterInt);
        nextCharacterTimer -= Time.deltaTime;
        if(nextCharacterTimer <= 0)
        {
            nextCharacterTimer = nextCharacterDelay;
            if (currentCharacterInt < chosenText.Length)
            {
                currentCharacterInt++;
                playSound = true;
            }
            else
            {
                playSound = false;
            }
        }
        

    }
    void BlaSound()
    {
        if (playerSpeaking)
        {
            blaSound.pitch = Random.Range(1.0f, 1.2f);
        }
        else
        {
            blaSound.pitch = Random.Range(1.5f, 1.7f);
        }
        blaSound.Play();
    }
}
