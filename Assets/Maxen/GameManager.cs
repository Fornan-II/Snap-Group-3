using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton structure
    //
    public static GameManager Instance;
    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    //

    protected int _gameNumber = -1;
    public int GameNumber
    {
        get
        {
            //If game number not set...
            if (_gameNumber <= 0)
            {
                //Find highest game number in database...
                foreach (Photo p in Database.GetAllPhotos())
                {
                    _gameNumber = Mathf.Max(p.RoundTakenDuring, _gameNumber);
                }
                //And go one higher for this current round
                _gameNumber++;
            }

            return _gameNumber;
        }
    }

    public List<Photo> PhotosThisRound = new List<Photo>();

    [SerializeField] protected PlayerController _player;
    [SerializeField] protected int NumberOfCelebsPerRound = 3;
    [SerializeField] protected int CurrentTargetIndex = 0;
    public List<CharacterInformation.Character> CelebTargets = new List<CharacterInformation.Character>();

    public void RegisterPhoto(string path, List<CharacterInformation.Character> celebritiesInPhoto)
    {
        Photo newPhoto = new Photo(path, GameNumber);
        newPhoto.CelebritiesInPhoto = celebritiesInPhoto;
        PhotosThisRound.Add(newPhoto);

        //Need some way to track which waypoint each photo was taken at.
        //For the purposes of only one photo per celeb per waypoint
    }

    public void InitGameRound()
    {
        List<CharacterInformation.Character> allCelebs = new List<CharacterInformation.Character>(Database.GetAllCharacters());
        CelebTargets.Clear();
        for(int counter = 0; counter < NumberOfCelebsPerRound; counter++)
        {
            int selectedIndex = Random.Range(0, allCelebs.Count);
            CelebTargets.Add(allCelebs[selectedIndex]);
            allCelebs.RemoveAt(selectedIndex);
        }

        //Get player waypoints, and get their associated characters.
        //Pick characters at that waypoint to assign celeb to
    }
}
