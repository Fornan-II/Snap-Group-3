using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicateCelebColors : MonoBehaviour
{
    public Image[] CelebColorIndicator;
    public Image[] CelebConfirmationIndicator;

    private static IndicateCelebColors Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public static void SetCelebColor(int CelebNumber, Color CelebColor)
    {
        Instance.CelebColorIndicator[CelebNumber].color = CelebColor;
    }

    public static void SetCelebFound(int CelebNumber)
    {
        Instance.CelebConfirmationIndicator[CelebNumber].gameObject.SetActive(true);
    }
}
