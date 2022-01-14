using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;  

public class CursorImage : MonoBehaviour
{
    private static Vector3 mousePos;

    private Image cursorImage;

    [SerializeField]
    private Sprite[] cursorSprite;

    private void Start()
    {
        cursorImage = GetComponent<Image>();
        Cursor.visible = false;
        SetCursorSprite(0);
    }
    private void Update()
    {
        GetCursorPosition();
        SetCursorPosition();
    }

    private void GetCursorPosition()
    {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Input.mousePosition.z));

        mousePos = RectTransformUtility.WorldToScreenPoint(Camera.main, mousePos);
    }

    private void SetCursorPosition()
    {
        transform.position = mousePos;
    }

    public void SetCursorSprite(int key)
    {
        cursorImage.sprite = cursorSprite[key];
    }
}
