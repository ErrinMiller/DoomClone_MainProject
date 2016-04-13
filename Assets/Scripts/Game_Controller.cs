using UnityEngine;
using System.Collections;

public class Game_Controller : MonoBehaviour
{
    [Header("Mouse Variables")]
    public bool mouse_HideMouse;


	void Start()
    {
        mouse_HideMouse = true;
    }
	
	
	void Update()
    {
        MouseHide();
	}


    void MouseHide()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mouse_HideMouse = !mouse_HideMouse;
        }

        if (mouse_HideMouse)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
