using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagement : MonoBehaviour
{
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        UpdateManager.Instance.ClearAll();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
