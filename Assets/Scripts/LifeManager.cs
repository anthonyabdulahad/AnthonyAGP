using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public string currentscene;

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
        PlayerHealth health = FindObjectOfType<PlayerHealth>();
        health.Respawn();

        lives--;
        if (lives == 0)
        {
            SceneManager.LoadScene(currentscene);

        }
    }

    // Update is called once per frame
    void Update()
    {
        text.text = lives.ToString();
    }
}
