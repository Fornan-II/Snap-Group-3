using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadImage : MonoBehaviour
{
    [SerializeField]
    RawImage image;

    Texture screenShot;

    [SerializeField]
    List<RawImage> rawImages;

    bool done = false;    

    private void Start()
    {
        if (done == false)
        {
            Vector3 newPos = image.rectTransform.localPosition;

            for (int i = 0; i < rawImages.Count; ++i)
            {
                screenShot = Resources.Load<Texture>("screen " + i);

                rawImages[i].GetComponent<RawImage>().texture = screenShot;
            }
            done = true;
        }
    }
}