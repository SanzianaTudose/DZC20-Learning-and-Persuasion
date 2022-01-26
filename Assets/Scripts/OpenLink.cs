using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLink : MonoBehaviour {

    public Texture2D handCursor;
    public void OpenIDLink() {
        Application.OpenURL("https://www.tue.nl/en/education/bachelor-college/bachelor-industrial-design/");
    }

    public void OnMouseOver() {
        Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.Auto);
    }
    public void OnMouseExit() {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
