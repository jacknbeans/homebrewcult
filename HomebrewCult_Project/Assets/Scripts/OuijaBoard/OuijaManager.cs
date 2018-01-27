using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuijaManager : MonoBehaviour {

    public OuijaLetter[] OuijaLetters;
    public TextMesh OuijaSpellingText;
    
    public string[] words;
    private int charIndex;
    private char[] characters;

    private Dictionary<char, int> LetterIndices;
    private int letterIndex;

    public OuijaPlanchetteMovement planchette;

    public float timeBetweenLetters;
    public float timeStayingOnLetters;
    public float timeBetweenWords;
    public float timeBetweenNewGhosts;
    public float threshold;

    private OuijaLetter letter;

    private void Awake()
    {
    }

    private void GetNewWord()
    {
        // Do this each time a new word is needed for the planchette
        var word = words[Random.Range(0, words.Length)];
        characters = word.ToCharArray();
        OuijaSpellingText.text = null;
    }

    private OuijaLetter GetLetter()
    {
        // Do this each time a new letter is needed for the planchette
        if (charIndex >= characters.Length)
        {
            charIndex = 0;
            return null;
        }
        letterIndex = LetterIndices[characters[charIndex++]];
        return OuijaLetters[letterIndex];
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
        LetterIndices = new Dictionary<char, int>(OuijaLetters.Length);
        // Creating the dictionary only needs to happen at the start
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
