using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private string _levelName;

    public void LoadLevel()
    {
        SceneManager.LoadScene(_levelName);
    }
}
