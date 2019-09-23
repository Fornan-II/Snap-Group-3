using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseInserter : MonoBehaviour
{
    public CharacterInformation.Character[] CelebsToAdd;
    public bool AddCelebs = false;

    public Photo[] PhotosToAdd;
    public bool AddPhotos = false;

    private void Update()
    {
        if(AddCelebs)
        {
            foreach(CharacterInformation.Character celeb in CelebsToAdd)
            {
                Database.AddCharacter(celeb);
            }

            CelebsToAdd = new CharacterInformation.Character[0];

            AddCelebs = false;
        }

        if(AddPhotos)
        {
            foreach(Photo p in PhotosToAdd)
            {
                Database.AddPhoto(p);
            }

            PhotosToAdd = new Photo[0];

            AddPhotos = false;
        }
    }
}
