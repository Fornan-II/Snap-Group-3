using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCharGenerator : MonoBehaviour
{
    [SerializeField] private CharacterGen generator;
    [SerializeField] private bool GenerateOnStart = false;

    private static AutoCharGenerator _instance;

    private void Start()
    {
        _instance = this;

        if(generator && GenerateOnStart)
        {
            GenerateCharacters();
        }
    }

    public static void GenerateCharacters()
    {
        CharacterInformation[] allCharacters = FindObjectsOfType<CharacterInformation>();
        for (int i = 0; i < allCharacters.Length; i++)
        {
            allCharacters[i].SetAppearance(_instance.generator.GenerateAppearance(-1));
        }
    }
}
