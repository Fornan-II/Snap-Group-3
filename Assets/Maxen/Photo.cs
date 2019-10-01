using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

[System.Serializable]
public class Photo
{
    //File path to Photo. Important for database.
    public string Path;
    //Image loaded in from file path. Set in LoadImage()
    public Texture2D Image;
    //A list of all celebrities in this photo.
    public List<CharacterInformation.Character> CelebritiesInPhoto = new List<CharacterInformation.Character>();
    //Which game number this picture was taken from. Useful for organazing pictures by which round they were taken in.
    public int RoundTakenDuring = -1;

    public Photo(string filePath, int roundNumber)
    {
        Path = filePath;
        CelebritiesInPhoto = new List<CharacterInformation.Character>();
        RoundTakenDuring = roundNumber;
    }

    public bool LoadImage()
    {
        // load image from file path
        //AssetDatabase.Refresh();
        Image = Resources.Load(Path) as Texture2D;

        if (Image != null)
            return true;

        // Return false if this fails for some reason (ex: file does not exist)
        Debug.Log("image not loaded: " + Path);
        return false;
    }

    public void Discard()
    {
        //Deletes image file at path
        Image = null;
        CelebritiesInPhoto = new List<CharacterInformation.Character>();
        RoundTakenDuring = -1;
    }
}
