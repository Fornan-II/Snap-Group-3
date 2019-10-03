using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicateCelebColors : MonoBehaviour
{
    public CharUI[] CelebIndicator;
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

    public static void SetCelebAppearance(int CelebNumber, CharacterGen.Appearance CelebAppearance)
    {
        Instance.CelebIndicator[CelebNumber].SetAppearance(CelebAppearance);
    }

    public static void SetCelebFound(int CelebNumber)
    {
        Instance.CelebConfirmationIndicator[CelebNumber].gameObject.SetActive(true);
    }
}
