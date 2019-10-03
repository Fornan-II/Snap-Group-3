using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBillboard : MonoBehaviour
{
    private static Transform CameraTransform;

    public bool LockXZRotation = true;

    private SpriteRenderer[] childRenderers;
    private static int sortOrderSize = 1;
    private static bool sortedThisFrame = false;
    private static List<SimpleBillboard> Instances = new List<SimpleBillboard>();

    private void Awake()
    {
        Instances.Add(this);
    }

    private void OnDestroy()
    {
        Instances.Remove(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!CameraTransform)
        {
            CameraTransform = Camera.main.transform;
        }

        childRenderers = GetComponentsInChildren<SpriteRenderer>();
        sortOrderSize = Mathf.Max(sortOrderSize, childRenderers.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (LockXZRotation)
        {
            Vector3 oldRotation = transform.rotation.eulerAngles;
            transform.forward = -CameraTransform.forward;
            transform.rotation = Quaternion.Euler(oldRotation.x, transform.rotation.eulerAngles.y, oldRotation.z);
        }
        else
        {
            transform.forward = -CameraTransform.forward;
        }

        if(!sortedThisFrame)
        {
            UpdateAllSpriteOrdering();
            sortedThisFrame = true;
        }
    }

    private void LateUpdate()
    {
        sortedThisFrame = false;
    }

    private static void UpdateAllSpriteOrdering()
    {
        Instances.Sort((x, y) =>
            {
                float xDistFromCamera = (CameraTransform.position - x.transform.position).sqrMagnitude;
                float yDistFromCamera = (CameraTransform.position - y.transform.position).sqrMagnitude;

                return Comparer<float>.Default.Compare(xDistFromCamera, yDistFromCamera);
            });

        for(int i = 0; i < Instances.Count; i++)
        {
            Instances[i].UpdateSpriteOrdering(Instances.Count - i);
        }
    }

    private void UpdateSpriteOrdering(int depthPosition)
    {
        for (int i = 0; i < childRenderers.Length; i++)
        {
            int localSort = Mathf.CeilToInt(childRenderers[i].transform.localPosition.z * 100000000);
            childRenderers[i].sortingOrder = depthPosition * sortOrderSize + localSort;
        }
    }
}
