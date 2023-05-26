using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public Animator _animator;
    public string nextscene;
    private void Start()
    {
        _animator = GetComponentInChildren<Animator>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.tag == "Player")
        {
           _animator.SetBool("Playeropen", true);
            Invoke("Loadnextscene", 1.5f); 
        }
      

    }

    private void Loadnextscene()
    {
        SceneManager.LoadScene(nextscene);
    }

}
