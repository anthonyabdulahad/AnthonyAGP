using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    public void Restart()
    {
        SceneManager.LoadScene("crash camera");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit game");
    
    }
}
