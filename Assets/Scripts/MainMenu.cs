using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void Play()
    {
        animator.SetTrigger("USBIN");
        Invoke("LoadScene", 2f);
    }

    public void BackToMainMenu()
    {
        animator.SetTrigger("USBOUT");
        //Invoke("LoadSceneMenu", 2f);
        StartCoroutine("InvokeRealtimeCoroutine");
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("Level One Forest");
    }
    private void LoadSceneMenu()
    {
        SceneManager.LoadScene("Menutheme");
    }

    private IEnumerator InvokeRealtimeCoroutine()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("Menutheme");
    }
}
