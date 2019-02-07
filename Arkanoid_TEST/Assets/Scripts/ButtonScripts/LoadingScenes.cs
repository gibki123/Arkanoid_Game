using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScenes : MonoBehaviour
{ 
    public void LoadStartSceneScript()
    {
        SceneManager.LoadScene(0);
    } 

    public void LoadActualGameSceneScript()
    {
        SceneManager.LoadScene(1);
        BlockSpawning.endlessLevelling = true;
    }

    public void LoadSpecificLevel()
    {
        SceneManager.LoadScene(1);
        BlockSpawning.endlessLevelling = false;
    }

    public void LoadGameOverSceneScript()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
