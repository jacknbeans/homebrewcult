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
    public bool helpingClient;
    public bool dialogRead;
    private bool playerSpeaking = false;

    public GameObject clientPrefab;
    public GameObject photoPrefab;
    public GameObject photoInsertPrefab;
    public bool photoGiven;
    public bool photoInsertSpawned;
    public Photograph photoInUse;
    public Vector3 clientPos;
    private Client_Core currentClient;

    private float soundTimer;
    private AudioSource blaSound;
    private bool playSound;
    public GameObject victoryObject;
    private AudioSource victorySound;

    private OuijaManager ouijaBoard;

	// Use this for initialization
	void Start () {
        SpawnClient();
	    dialogRead = true;
        dialogTextBox.text = " ";
        blaSound = gameObject.GetComponent<AudioSource>();
        victorySound = victoryObject.GetComponent<AudioSource>();
        ouijaBoard = FindObjectOfType<OuijaManager>();
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
    }

    public void SpawnClient()
    {
        currentClient = Instantiate(clientPrefab, clientPos, Quaternion.identity, null).GetComponent<Client_Core>();
        currentClient.rootPos = clientPos;
        ghostSummoned = false;
        helpingClient = true;
        chosenText = requestString[Random.Range(0, requestString.Length)];
        dialogRead = false;
        currentCharacterInt = 0;
    }

    public void StartFirstDialog()
    {
        dialogRead = false;
    }

    public void DialogRead()
    {
        if (!ghostSummoned)
        {
            dialogTextBox.text = " ";
            dialogRead = true;
            ouijaBoard.channeling = true;
        }
        if (ghostSummoned && !helpingClient)
        {
            dialogTextBox.text = " ";
            dialogRead = true;
            NextClient();
        }
    }

    public void FinishedSummoning()
    {
        photoInUse.spiritSummoned = true;
        ghostSummoned = true;
        ouijaBoard.channeling = false;
        victorySound.Play();
        EndDialog();
    }

    public void EndDialog()
    {
        playerSpeaking = false;
        chosenText = responseString[Random.Range(0, responseString.Length)];
        dialogRead = false;
        currentCharacterInt = 0;
    }

    public void NextClient()
    {
        playerSpeaking = true;
        currentClient.completed = true;
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
            //photospawning
            if (currentCharacterInt == chosenText.Length && !ghostSummoned && !photoGiven)
            {
                photoInUse = Instantiate(photoPrefab, new Vector3(0, 1, -1), Quaternion.identity, null).GetComponent<Photograph>();
                photoGiven = true;
            }
            //photo insert spawning
            if (currentCharacterInt == chosenText.Length && ghostSummoned && !photoInsertSpawned)
            {
                Instantiate(photoInsertPrefab, new Vector3(0, 1, -1), photoInsertPrefab.transform.rotation, null);
                photoInsertSpawned = true;
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

    public bool IsSummoning()
    {
        return helpingClient;
    }
}
