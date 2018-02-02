using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuijaManager : GameplayBehaviour {

    public OuijaLetter[] OuijaLetters;
    public TextMesh OuijaSpellingText;
    [HideInInspector]
    public bool channeling;

    public string[] goodWords;
    public string[] badWords;
    private int charIndex;
    private char[] characters;

    private Dictionary<char, int> LetterIndices;
    private int letterIndex;

    public OuijaPlanchetteMovement planchette;

    public float timeBetweenLetters;
    public float timeStayingOnLetters;
    public float timeBetweenWords;
    public float timeStranglingPlanchetteRequired;
    [Range(10, 50)]
    public float channelPoints;
    private float timer;
    public float threshold;

    private AudioSource badWordSound;
    private AudioSource goodWordSound;

    private OuijaLetter letter;

    private ClientManager _clientManager;

    private void GetNewWord()
    {
        if (channeling)
        {
            // Reset text and the word
            charIndex = 0;
            OuijaSpellingText.text = null;

            // Do this each time a new word is needed for the planchette
            float randomWord = Random.Range(0.0f, 1.0f);
            if (randomWord > 0.5f)
            {
                // Good words
                var word = goodWords[Random.Range(0, goodWords.Length)];
                characters = word.ToCharArray();
                channelPoints = Mathf.Abs(channelPoints);
            }
            else
            {
                // Bad words
                var word = badWords[Random.Range(0, badWords.Length)];
                characters = word.ToCharArray();
                channelPoints = -Mathf.Abs(channelPoints);
            }
        }
    }

    private OuijaLetter GetLetter()
    {
        if (channeling)
        {
            // Do this each time a new letter is needed for the planchette
            if (charIndex >= characters.Length)
            {
                if (_clientManager.IsSummoning())
                {
                    Channel(channelPoints);
                    if (channelPoints < 0.0f)
                    {
                        badWordSound.Play();
                    }
                    else
                    {
                        goodWordSound.Play();
                    }
                }

                charIndex = 0;
                return null;
            }
            timer = 0;
            letterIndex = LetterIndices[characters[charIndex++]];
            return OuijaLetters[letterIndex];
        }
        return null;
    }

    private void UpdateSpellingBoard()
    {
        // Spells out the text that was formed on the ouija board
        System.Text.StringBuilder sB = new System.Text.StringBuilder();
        sB.Append(OuijaSpellingText.text).Append(OuijaLetters[letterIndex].Letter.ToString());
        OuijaSpellingText.text = sB.ToString();
    }

    void Start()
    {
        badWordSound = GetComponent<AudioSource>();
        _clientManager = FindObjectOfType<ClientManager>();
        if (_clientManager == null)
        {
            Debug.LogError("There should be a client manager in the scene!");
        }

        goodWordSound = GameObject.Find("GoodWordSound").GetComponent<AudioSource>();

        // Creating the dictionary only needs to happen at the start
        LetterIndices = new Dictionary<char, int>(OuijaLetters.Length);
        for (var i = 0; i < OuijaLetters.Length; ++i)
        {
            LetterIndices.Add(OuijaLetters[i].Letter, i);
        }

        GetNewWord();
        letter = GetLetter();
    }

    void Update()
    {
        if (letter == null)
        {
            // If the word is completed spelling, wait a while on the last letter, then continue with a new word
            bool pauseCompleted = planchette.Pause(timeBetweenWords);

            if (pauseCompleted)
            {
                GetNewWord();
                letter = GetLetter();
            }
        }
        else
        {
            planchette.Move(letter.transform, timeBetweenLetters);

            timer += Time.deltaTime;
            if (timer >= timeStranglingPlanchetteRequired)
            {
                GetNewWord();
                letter = GetLetter();
                timer = 0;
            }
            // Do this when the planchette is near the letter it moves to
            if ((letter.transform.position - planchette.transform.position).magnitude <= threshold)
            {
                bool effectCompleted = planchette.Effect();

                if (effectCompleted)
                {
                    UpdateSpellingBoard();
                    letter = GetLetter();
                }
            }
        }
    }
}
