using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPanel : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void DisplayPhotos(Photo[] photos)
    {
        gameObject.SetActive(true);
        MoneyManager.instance.AddMoney(photos);
    }
}
