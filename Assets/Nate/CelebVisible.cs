using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelebVisible : MonoBehaviour
{
    public bool isVisibile = false;

    private void OnBecameVisible()
    {
        isVisibile = true;
    }

    private void OnBecameInvisible()
    {
        isVisibile = false;
    }
}
