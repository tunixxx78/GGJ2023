using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManageer : MonoBehaviour
{
    [SerializeField] int targetLevelIndex;

    public void ChangeLevel()
    {
        SceneManager.LoadScene(targetLevelIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
