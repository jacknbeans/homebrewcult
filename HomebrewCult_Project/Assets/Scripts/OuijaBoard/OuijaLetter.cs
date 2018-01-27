using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuijaLetter : MonoBehaviour {

    [HideInInspector]
    public char Letter;

    void Awake()
    {
        Letter = transform.name.ToCharArray()[0];
    }
}
