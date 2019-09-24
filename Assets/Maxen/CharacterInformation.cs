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
    
    //Useful for later:
    //Random.InitState(_info.CharID);

#if UNITY_EDITOR
    [SerializeField] private string _celebrityName;
    private string _namePrevious;

    protected virtual void OnValidate()
    {
        if(_celebrityName != _info.Name && _info.CharID >= 0)
        {
            if(_celebrityName != _namePrevious)
            {
                _info.Name = _celebrityName;
                Database.UpdateCharacter(_info);
            }
            else
            {
                _celebrityName = _info.Name;
            }
        }

        _namePrevious = _celebrityName;
    }
#endif
}
