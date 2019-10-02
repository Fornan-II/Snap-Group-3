using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;

    public GameObject moneyText;
    TextMeshPro text;
    int money;

    private void Start()
    {
        text = moneyText.GetComponent<TextMeshPro>();
    }

    public void AddMoney(Photo[] photoArray)
    {
        int numPicsToSell = photoArray.Length;
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
