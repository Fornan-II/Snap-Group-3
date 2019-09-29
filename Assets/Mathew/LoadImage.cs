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

    bool done = false;

    public void Start()
    {
        screenShot = Resources.Load<Texture>("screen");

        image.GetComponent<RawImage>().texture = screenShot;

        Debug.Log("Image Position: " + image.rectTransform.localPosition);
    }
        

    private void Update()
    {
        if (done == false)
        {
            Vector3 newPos = image.rectTransform.localPosition;

            newPos.x += 100;

            for (int i = 1; i < 4; ++i)
            {
                Debug.Log(newPos);

                Instantiate(image, trans);

                image.rectTransform.localPosition += newPos;

                Debug.Log(i);
            }
            done = true;
        }
    }
}