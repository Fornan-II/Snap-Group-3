using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MSGDisplay : MonoBehaviour
{
    [SerializeField] private Text _output;

    static MSGDisplay instance;

    private void Start()
    {
        if(instance)
        {
            Destroy(instance);
        }
        instance = this;
        ClearMsg();
    }

    public static void AppendMsg(string msg)
    {
        if(!instance)
        {
            return;
        }

        instance._output.text += msg + "\n";
    }

    public static void SetMsg(string msg)
    {
        if (!instance)
        {
            return;
        }

        instance._output.text = msg;
    }

    public static void ClearMsg()
    {
        if (!instance)
        {
            return;
        }

        instance._output.text = "";
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if(!_output)
        {
            _output = GetComponent<Text>();
        }
    }
#endif
}
