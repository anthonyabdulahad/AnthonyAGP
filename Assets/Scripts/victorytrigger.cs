using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class victorytrigger : MonoBehaviour
{
    private Scene VictoryScreen;

    

    public void Start()
    {
        VictoryScreen = SceneManager.GetActiveScene();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("VictoryScreen");
        }
        
    }

}
