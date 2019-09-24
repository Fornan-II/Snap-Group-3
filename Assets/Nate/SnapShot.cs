using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapShot : MonoBehaviour
{
    public int resWidth = 1080;
    public int resHeight = 1920;

    private bool takeHiResShot = false;

    GameObject celeb;

    bool m_Started;
    public LayerMask m_LayerMask;

    public Vector3 collider;

    void Start()
    {
        //Use this to ensure that the Gizmos are being drawn when in Play Mode.
        m_Started = true;
    }

    public static string ScreenShotName(int width, int height)
    {
        return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png",
                             Application.dataPath,
                             width, height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public void TakeHiResShot()
    {
        takeHiResShot = true;
    }

    private void Update()
    {

    }

    public bool MyCollisions()
    {
        Collider[] hitColliders = Physics.OverlapBox(new Vector3(transform.position.x, transform.position.y, transform.position.z + (collider.z / 2)), collider, Quaternion.identity, m_LayerMask);
        int i = 0;
        //Check when there is a new collider coming into contact with the box
        while (i < hitColliders.Length)
        {
            //Output all of the collider names
            Debug.Log("Hit : " + hitColliders[i].name + i);
            //Increase the number of Colliders in the array
            i++;
        }

        foreach(Collider c in hitColliders)
        {
            if (c.GetComponent<CelebVisible>() != null)
            {
                if (c.GetComponent<CelebVisible>().isVisibile == true)
                {
                    return true;
                }
            }
        }

        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (m_Started)
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y, transform.position.z + (collider.z / 2)), collider);
    }

    void LateUpdate()
    {
        takeHiResShot |= Input.GetKeyDown("k");
        if (takeHiResShot)
        {
            // is the celeb in the shot
            if (MyCollisions() == true)
            {
                Debug.Log("Celeb Captured");
            }
            // else if () add another function to look for past celebs 

            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            GetComponent<Camera>().targetTexture = rt;

            Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            GetComponent<Camera>().Render();

            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            GetComponent<Camera>().targetTexture = null;

            RenderTexture.active = null; // JC: added to avoid errors

            Destroy(rt);

            byte[] bytes = screenShot.EncodeToPNG();
            string filename = ScreenShotName(resWidth, resHeight);
            System.IO.File.WriteAllBytes(filename, bytes);

            Debug.Log(string.Format("Took screenshot to: {0}", filename));
            takeHiResShot = false;
        }
    }
}
