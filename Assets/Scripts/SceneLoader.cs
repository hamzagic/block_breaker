using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadSelectedScene(string sceneName)
    {
        if (sceneName == "Start Menu" || sceneName == "Level 1")
        {
            GameStatus gameStatus = FindObjectOfType<GameStatus>();
            if (gameStatus != null) gameStatus.ResetGame();
        }
       SceneManager.LoadScene(sceneName);
    }

}
