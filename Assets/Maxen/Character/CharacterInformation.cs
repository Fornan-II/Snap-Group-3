using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInformation : MonoBehaviour
{
    [System.Serializable]
    public struct Character
    {
        public int CharID;
        public string Name;

        public static Character Empty = new Character { CharID = -1, Name = "Random" };
    }

    protected Character _info = Character.Empty;

    public Character GetInfo()
    {
        //return Character.Empty;
        return _info;
    }
    
    public void AssignCharacter(Character newChar)
    {
        _info = newChar;
    }

    public void GenerateFeatures()
    {
        //If CharID >= 0
        int ranIndex = Random.Range(0, GameManager.Instance.AvailableCharFeatures.Count);
        CharFeature selectedFeature = GameManager.Instance.AvailableCharFeatures[ranIndex];

        //If this character is a celeb, remove that feature from the available features list
        if (_info.CharID >= 0)
        {
            GameManager.Instance.AvailableCharFeatures.RemoveAt(ranIndex);
        }

        //Useful for later:
        //Random.InitState(_info.CharID);
        //I'll use this for picking char color instead of having it be a CharFeature. CharFeature is supposed to be stuff like clothes.
    }
}
