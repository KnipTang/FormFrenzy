using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStartGame : MonoBehaviour
{
    static public void StartGame()
    {
        int sceneCount = SceneManager.sceneCount;


        for (int i = 0; i < sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.isLoaded)
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("SampleScene");
    }
}
