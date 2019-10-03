using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;


public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;

    public GameObject moneyText;
    TextMeshProUGUI text;
    int money;
    public Texture2D celebOnePic;
    public Texture2D celebTwoPic;
    public Texture2D celebthreePic;

    List<CharacterInformation.Character> celebritiesInPreviousPhoto;

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
        text = moneyText.GetComponent<TextMeshProUGUI>();
    }

    public void AddMoney()
    {
        int numPicsToSell = GameManager.Instance.PhotosThisRound.Count;
        StartCoroutine(AddMoneyAnim(numPicsToSell));
    }

    void Add()
    {
        moneyText.GetComponent<Animator>().SetTrigger("Collected");
        money += 100;
        text.text = "$" + money;
    }

    IEnumerator AddMoneyAnim(int numPicsToSell)
    {
        for (int i = 0; i < numPicsToSell; i++)
        {
            // add the amount of money you earned
            Add();
            yield return new WaitForSeconds(0.25f);
        }
    }
}