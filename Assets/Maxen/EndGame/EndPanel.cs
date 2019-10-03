using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPanel : MonoBehaviour
{
    [SerializeField] protected CelebPhotoDisplay[] displays;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void DisplayPhotos(Photo[] photos)
    {
        gameObject.SetActive(true);
        MoneyManager.instance.AddMoney(photos);

        for(int i = 0; i < photos.Length && i < displays.Length; i++)
        {
            displays[i].SetPhoto(photos[i]);
        }
    }
}
