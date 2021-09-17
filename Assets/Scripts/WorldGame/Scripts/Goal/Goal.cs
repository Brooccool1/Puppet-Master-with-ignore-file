using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<ParticleSystem>().Play();
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        Invoke("_loadStartMenu", 2);
    }

    private void _loadStartMenu()
    {
        SceneManager.LoadScene("Start");
    }
}
