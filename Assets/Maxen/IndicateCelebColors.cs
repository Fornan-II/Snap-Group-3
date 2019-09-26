using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicateCelebColors : MonoBehaviour
{
    public Image[] CelebIndicator;

    private static IndicateCelebColors Instance;

    private void Awake()
    {
        Instance = this;
    }

    public static void SetCelebColor(int CelebNumber, Color CelebColor)
    {
        Instance.CelebIndicator[CelebNumber].color = CelebColor;
    }

    public static void SetCelebFound(int CelebNumber)
    {
        //Put a check box on it I guess
    }
}
