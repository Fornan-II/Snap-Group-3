using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    private void OnDestroy()
    {
        Instance = null;
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
    
    [SerializeField] protected int NumberOfCelebsPerRound = 3;

    //Temporarily predefining features in script while CharFeatures is simple
    [SerializeField]
    protected List<CharFeature> _AllCharFeatures = new List<CharFeature>()
    {
        new CharFeature() {BodyColor = Color.black},
        new CharFeature() {BodyColor = Color.red},
        new CharFeature() {BodyColor = Color.blue},
        new CharFeature() {BodyColor = Color.green},
        new CharFeature() {BodyColor = Color.gray},
        new CharFeature() {BodyColor = Color.magenta},
        new CharFeature() {BodyColor = Color.yellow}
    };

    public List<CharFeature> AvailableCharFeatures;

    //Character Key represents the actual celebrity
    //int Value represents the index in PhotosThisRound. Index is -1 if no photo has been taken
    public Dictionary<CharacterInformation, int> CelebTargets = new Dictionary<CharacterInformation, int>();

    public PhotoArrayEvent OnFinalPhotosSelected;

    public CharacterInformation[] GetCelebCharacters()
    {
        CharacterInformation[] celebChars = new CharacterInformation[CelebTargets.Values.Count];
        CelebTargets.Keys.CopyTo(celebChars, 0);
        return celebChars;
    }

    private void Start()
    {
        InitGameRound();
    }

    public void RegisterPhoto(string path, List<CharacterInformation.Character> celebritiesInPhoto)
    {
        Photo newPhoto = new Photo(path, GameNumber);
        newPhoto.CelebritiesInPhoto = celebritiesInPhoto;

        //Record index of PhotosThisRound with their associated celebrities
        CharacterInformation[] celebsInScene = new CharacterInformation[CelebTargets.Keys.Count];
        CelebTargets.Keys.CopyTo(celebsInScene, 0);
        for(int i = 0; i < celebsInScene.Length; i++)
        {
            CharacterInformation.Character celebChar = celebsInScene[i].GetInfo();
            if (newPhoto.CelebritiesInPhoto.Contains(celebChar))
            {
                CelebTargets[celebsInScene[i]] = newPhoto.CelebritiesInPhoto.IndexOf(celebChar);
                //TEMP tell player that they took a picture of that celeb
                IndicateCelebColors.SetCelebFound(i);
            }
        }
        PhotosThisRound.Add(newPhoto);
    }

    public void InitGameRound()
    {
        //Mouse stuffs
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Reset important things.
        AvailableCharFeatures = _AllCharFeatures;
        CelebTargets.Clear();
        PhotosThisRound.Clear();

        //Get CharacterInformations in scene so they can be assigned a random few celebrities.
        List<CharacterInformation> allCharacters = new List<CharacterInformation>(FindObjectsOfType<CharacterInformation>());
        //Grab a celebrities from the database
        List<CharacterInformation.Character> allCelebs = new List<CharacterInformation.Character>(Database.GetAllCharacters());
        
        //Pick 3 random celebrities from the database and assign them
        for(int counter = 0; counter < NumberOfCelebsPerRound; counter++)
        {
            //Select random celeb
            int selectedCelebIndex = Random.Range(0, allCelebs.Count);
            
            //Select random character from scene to make become this celeb
            int selectedCharacterIndex = Random.Range(0, allCharacters.Count);
            allCharacters[selectedCharacterIndex].AssignCharacter(allCelebs[selectedCelebIndex]);
            //Debug.Log(allCharacters[selectedCelebIndex])
            //Let selected char pick some identifying features
            allCharacters[selectedCharacterIndex].GenerateFeatures();
            //TEMP assign char to UI
            SpriteRenderer sr = allCharacters[selectedCharacterIndex].GetComponent<SpriteRenderer>();
            if(sr)
            {
                IndicateCelebColors.SetCelebColor(counter, sr.color);
            }
            
            //record that this celeb is a target this round, then remove them from the pools of potentially picked celebs and characters.
            CelebTargets.Add(allCharacters[selectedCharacterIndex], -1);
            allCelebs.RemoveAt(selectedCelebIndex);
            allCharacters.RemoveAt(selectedCharacterIndex);
        }

        //Generate outfits for the rest of the characters
        foreach(CharacterInformation character in allCharacters)
        {
            character.GenerateFeatures();
        }

        //Tell player to start moving? Maybe? If that's necessary?
    }

    public void EndGameRound()
    {
        //Mouse stuffs
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //Get final photos
        Photo[] finalPhotos = GetFinalCelebPhotos();
        //Show 'em to the player
        OnFinalPhotosSelected?.Invoke(finalPhotos);
        //Push photos to database
        foreach(Photo p in finalPhotos)
        {
            Database.AddPhoto(p);
        }
        //Discard the rest... implement this later
        //Go to main menu
        StartCoroutine(WaitToReturnToMenu(1.0f));
    }

    protected Photo[] GetFinalCelebPhotos()
    {
        List<Photo> photos = new List<Photo>();
        foreach(int photoIndex in CelebTargets.Values)
        {
            if (0 <= photoIndex && photoIndex < PhotosThisRound.Count)
            {
                if (!photos.Contains(PhotosThisRound[photoIndex]))
                {
                    photos.Add(PhotosThisRound[photoIndex]);
                }
            }
        }

        return photos.ToArray();
    }

    protected IEnumerator WaitToReturnToMenu(float acceptInputDelay = 0.0f)
    {
        //Wait for a short time before accepting input for this. Prevents accidental triggering right when game ends. If acceptInputDelay is 0, there is no delay.
        for(float timer = 0.0f; timer < acceptInputDelay; timer += Time.deltaTime)
        {
            yield return null;
        }

        while (!Input.anyKey)
        {
            yield return null;
        }

        MenuManagement.GoToMainMenu();
    }
}

[System.Serializable]
public class PhotoArrayEvent : UnityEvent<Photo[]> { }
