﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Photo
{
    //File path to Photo. Important for database.
    public string Path;
    //Image loaded in from file path. Set in LoadImage()
    public Texture2D Image;
    //A list of all celebrities in this photo.
    public List<CharacterInformation.Character> CelebritiesInPhoto;
    //Which game number this picture was taken from. Useful for organazing pictures by which round they were taken in.
    public int RoundTakenDuring;

    public Photo(string filePath, int roundNumber)
    {
        Path = filePath;
        CelebritiesInPhoto = new List<CharacterInformation.Character>();
        RoundTakenDuring = roundNumber;
    }

    public bool LoadImage()
    {
        // Load screenshot file at path.
        // Return false if this fails for some reason (ex: file does not exist)
        return false;
    }
}