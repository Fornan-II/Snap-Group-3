using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;


public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;

    int money;
    int celebsPhotographed;
    public TextMeshProUGUI text;
    //public TextMeshProUGUI celeb0;
    //public TextMeshProUGUI celeb1;
    //public TextMeshProUGUI celeb2;
    public TextMeshProUGUI winState;
    public TextMeshProUGUI bonusPhotos;

    public GameObject check1;
    public GameObject check2;
    public GameObject check3;

    List<CharacterInformation.Character> celebritiesInPreviousPhoto;
    //[SerializeField]
    //TextMeshProUGUI[] celebNames;
    string[] tempNames;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        tempNames = new string[3];

        //celebNames = new TextMeshProUGUI[3];
        //celebNames[0] = celeb0;
        //celebNames[1] = celeb1;
        //celebNames[2] = celeb2;

        celebsPhotographed = 0;
    }

    public void AddMoney(Photo[] photos)
    {
        //for (int i = 0; i < photos.Length; i++)
        //{
        //    if (photos[i].CelebritiesInPhoto.Count > 2)
        //    {
        //        for (int j = 0; j < photos[i].CelebritiesInPhoto.Count; j++)
        //        {
        //            tempNames[j] = photos[i].CelebritiesInPhoto[j].Name;
        //        }
        //        celebNames[i].text = tempNames[0] + " & " + tempNames[1] + " & " + tempNames[2];
        //    }
        //    else if (photos[i].CelebritiesInPhoto.Count > 1)
        //    {
        //        for (int j = 0; j < photos[i].CelebritiesInPhoto.Count; j++)
        //        {
        //            tempNames[j] = photos[i].CelebritiesInPhoto[j].Name;
        //        }
        //        celebNames[i].text = tempNames[0] + " & " + tempNames[1];
        //    }
        //    else
        //    {
        //        celebNames[i].text = photos[i].CelebritiesInPhoto[0].Name;
        //    }
        //}
        // number of photos
        int numPicsToSell = GameManager.Instance.PhotosThisRound.Count;

        bool allFound = false;

        if (check1.activeSelf)
        {
            celebsPhotographed++;
        }
        if (check2.activeSelf)
        {
            celebsPhotographed++;
        }
        if (check3.activeSelf)
        {
            celebsPhotographed++;
        }


        // did we get all celebrities?
        if (celebsPhotographed >= 3)
        {
            winState.text = "You Photographed All The Celebrities!";
            allFound = true;
        }
        else
        {
            winState.text = "You Didn't Photograph All The Celebrities";
            winState.color = Color.red;
        }

        // set bonus photos
        if (!allFound)
        {
            if (numPicsToSell > celebsPhotographed)
                bonusPhotos.text = "+ " + (numPicsToSell - celebsPhotographed) + " Bonus Photos";
            else
                bonusPhotos.gameObject.SetActive(false);
        }
        else
        {
            bonusPhotos.text = "+ " + (numPicsToSell - 3) + " Bonus Photos";
        }
        // animate money adding
        StartCoroutine(AddMoneyAnim(numPicsToSell));
    }

    void Add()
    {
        text.GetComponent<Animator>().SetTrigger("Collected");
        money += 100;
        text.text = "$" + money;
    }

    IEnumerator AddMoneyAnim(int numPicsToSell)
    {
        for (int i = 0; i < numPicsToSell; i++)
        {
            // add the amount of money you earned
            Add();
            yield return new WaitForSeconds(0.5f);
        }
    }
}