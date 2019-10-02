using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoCanvas : MonoBehaviour
{
    [SerializeField]
    GameObject MainMenu;

    [SerializeField]
    GameObject Photos;

    public void Back()
    {
        MainMenu.SetActive(true);
        Photos.SetActive(false);
    }
}
