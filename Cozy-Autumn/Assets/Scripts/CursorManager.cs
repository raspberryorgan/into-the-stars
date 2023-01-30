using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CursorManager : MonoBehaviour
{
    public Texture2D normalCursor;
    public Texture2D crossHair;

    void Start()
    {
        SetCrossHair();
    }


    public void SetCrossHair()
    {
        Cursor.SetCursor(crossHair, Vector2.one * 18, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void SetNormalCursor()
    {
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
    }

}
