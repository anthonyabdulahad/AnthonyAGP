using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public int startingLives;
    public TMP_Text text;
    int lives;
    void Start()
    {
        lives = startingLives;
    }

    public void ExtraLife()
    {
        Debug.Log($"life added was {lives} now {lives + 1}");
        lives++;
    }

    public void LoseLife()
    {
        Debug.Log($"lifelose was {lives} now {lives-1}");
        lives--;
        if (lives == 0)
        {
            SceneManager.LoadScene("Crash camera");
        }
    }

    // Update is called once per frame
    void Update()
    {
        text.text = lives.ToString();
    }
}
