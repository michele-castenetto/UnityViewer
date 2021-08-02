using UnityEngine;

public class CursorController : MonoBehaviour
{

    public Texture2D cursor;

    private void ChangeCursor(Texture2D cursor) {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        // Cursor.lockState = CursorLockMode.Confined;
    }
    
    private void Awake() {
        ChangeCursor(cursor);
    }
    
}
