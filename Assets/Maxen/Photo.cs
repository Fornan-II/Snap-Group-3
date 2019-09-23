using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Photo
{
    //File path to Photo. Important for database.
    public string path;
    //Image loaded in from file path. Set in LoadImage()
    public Texture2D image;
    //A list of all celebrities in this photo.
    public CharacterInformation.Character[] celebritiesInPhoto;
    //Which game number this picture was taken from. Useful for organazing pictures by which round they were taken in.
    public int roundTakenDuring;

    public bool LoadImage()
    {
        // Load screenshot file at path.
        // Return false if this fails for some reason (ex: file does not exist)
        return false;
    }
}
