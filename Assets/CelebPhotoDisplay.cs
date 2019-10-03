using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CelebPhotoDisplay : MonoBehaviour
{
    [SerializeField] protected Image img;
    [SerializeField] protected TextMeshProUGUI txt;

    public void SetPhoto(Photo p)
    {
        gameObject.SetActive(true);

        if(txt)
        {
            string msg = "";
            for(int i = 0; i < p.CelebritiesInPhoto.Count; i++)
            {
                msg += p.CelebritiesInPhoto[i].Name;
                if(i == p.CelebritiesInPhoto.Count - 2)
                {
                    msg += "\n";
                }
                txt.text = msg;
            }
        }

        if(img)
        {
            Rect photoRect = new Rect(0, 0, p.Image.width, p.Image.height);
            img.sprite = Sprite.Create(p.Image, photoRect, photoRect.center);
        }
    }
}
