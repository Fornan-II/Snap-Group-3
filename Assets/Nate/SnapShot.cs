using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapShot : MonoBehaviour
{
    public int resWidth = 1080;
    public int resHeight = 1920;

    private bool takeHiResShot = false;

    GameObject celeb;

    public RawImage image;

    bool m_Started;
    public LayerMask celebLayer;
    public LayerMask playerLayer;

    public new Vector3 collider;

    List<CharacterInformation.Character> characters = new List<CharacterInformation.Character>();

    void Start()
    {
        //Use this to ensure that the Gizmos are being drawn when in Play Mode.
        m_Started = true;
    }

    public static string ScreenShotPath(int width, int height)
    {
        return string.Format("{0}/Resources/screen_{1}x{2}_{3}.png",
                             Application.dataPath,
                             width, height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }
    public static string ScreenShotName(int width, int height)
    {
        return string.Format("screen_{1}x{2}_{3}",
                             Application.dataPath,
                             width, height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public void TakeHiResShot()
    {
        takeHiResShot = true;
    }

    public bool CelebPictureCollisions()
    {
        bool value = false; // The bool value to be returned from the function

        //Collider[] hitColliders = Physics.OverlapBox(new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + (collider.z / 2f)), collider, Quaternion.identity, celebLayer);
        Collider[] hitColliders = Physics.OverlapBox(Vector3.zero, collider, Quaternion.identity, celebLayer);

        // Calculate the planes from the main camera's view frustum
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        // Check each collider detected to see if its a celeb 
        foreach (Collider c in hitColliders)
        {
            bool onScreen = false; // is the celeb visible on Screen 

            // we see someone
            // check from characterinfo
            CharacterInformation instance = c.GetComponent<CharacterInformation>();

            if (instance.GetInfo().CharID >= 0) // it is a celeb 
            {
                // Check if the c collider is within bounds of the camera frustrum 
                if (GeometryUtility.TestPlanesAABB(planes, c.bounds))
                {
                    // Linecast to see if anything is in the way or the celeb is on screen 
                    if (Physics.Linecast(Camera.main.transform.position, c.transform.position, out RaycastHit hitInfo, (1 << celebLayer)))
                    {
                        Debug.Log(c.name + " blocked by " + hitInfo.collider.name);
                    }
                    else
                    {
                        Debug.Log(c.name + " has been detected!");
                        onScreen = true;
                    }
                }
                else
                {
                    Debug.Log(c.name + " Not within Camera frustrum");
                    onScreen = false;
                }

                // If the celeb is on screen get their info 
                if (onScreen == true)
                {
                    // get celeb name 
                    string celebName = instance.GetInfo().Name;
                    Debug.Log("You photographed " + celebName);

                    // add the character to the list of celebs in picture
                    characters.Add(instance.GetInfo());

                    // a celeb was found return true
                    value = true;
                }
            }
            //Debug.Log(c.name + " is not a celebrity");
        }

        Debug.Log("value returned " + value);
        return value;
    }

    void OnDrawGizmos()
    {
        //Gizmos.matrix = this.transform.localToWorldMatrix;
        Gizmos.color = Color.green;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (m_Started)
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(Vector3.zero, collider);
        //(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + (collider.z / 2f))
    }

    void LateUpdate()
    {
        takeHiResShot |= Input.GetKeyDown(KeyCode.Space);
        if (takeHiResShot)
        {
            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            GetComponent<Camera>().targetTexture = rt;

            Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            GetComponent<Camera>().Render();

            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            GetComponent<Camera>().targetTexture = null;
            //image.texture = rt;
            RenderTexture.active = null; // added to avoid errors

            //Destroy(rt);

            byte[] bytes = screenShot.EncodeToPNG();
            string filename = ScreenShotPath(resWidth, resHeight);
            System.IO.File.WriteAllBytes(filename, bytes);

            string screenshotName = ScreenShotName(resWidth, resHeight);

            Debug.Log(string.Format("Took screenshot to: {0}", filename));
            takeHiResShot = false;

            // is the celeb in the shot
            if (CelebPictureCollisions() == true)
            {
                // register photo 
                GameManager.Instance.RegisterPhoto(screenshotName, characters);
            }
        }
    }

    public static Quaternion QuaternionFromMatrix(Matrix4x4 m)
    {
        // Adapted from: http://www.euclideanspace.com/maths/geometry/rotations/conversions/matrixToQuaternion/index.htm
        Quaternion q = new Quaternion();
        q.w = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] + m[1, 1] + m[2, 2])) / 2;
        q.x = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] - m[1, 1] - m[2, 2])) / 2;
        q.y = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] + m[1, 1] - m[2, 2])) / 2;
        q.z = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] - m[1, 1] + m[2, 2])) / 2;
        q.x *= Mathf.Sign(q.x * (m[2, 1] - m[1, 2]));
        q.y *= Mathf.Sign(q.y * (m[0, 2] - m[2, 0]));
        q.z *= Mathf.Sign(q.z * (m[1, 0] - m[0, 1]));
        return q;
    }
}
