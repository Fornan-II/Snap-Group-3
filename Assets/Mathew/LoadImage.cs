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
    Transform trans;

    [SerializeField]
    List<RawImage> rawImages;

    bool done = false;    

    private void Start()
    {
        if (done == false)
        {
            Vector3 newPos = image.rectTransform.localPosition;

            newPos.x += 100;

            for (int i = 1; i < rawImages.Count; ++i)
            {
                Debug.Log(i);

                screenShot = Resources.Load<Texture>("screen " + i);

                Debug.Log(screenShot);

                image.GetComponent<RawImage>().texture = screenShot;

                Debug.Log(image.GetComponent<RawImage>().texture = screenShot);
            }
            done = true;
        }
    }
}