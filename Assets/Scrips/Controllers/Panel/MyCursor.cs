using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyCursor : MonoBehaviour
{
    public Texture2D normal;
    public Texture2D red;
    public Texture2D blue;

     void Start()
    {
        MySetCursor(blue);
    }

    public void MySetCursor(Texture2D status)
    {
        Cursor.SetCursor(status, new Vector2(16, 16), CursorMode.Auto);
    }
}
