using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void PrevScene()
    {
        Scene active = SceneManager.GetActiveScene();

        if (active.buildIndex > 0)
        {
            SceneManager.LoadScene(active.buildIndex - 1);
        }
        else if(active.buildIndex == 0)
        {
            SceneManager.LoadScene(5);
        }
    }

    public void NextScene()
    {
        Scene active = SceneManager.GetActiveScene();

        if (active.buildIndex < 5)
        {
            SceneManager.LoadScene(active.buildIndex + 1);
        }
        else if (active.buildIndex == 5)
        {
            SceneManager.LoadScene(0);
        }
    }
}
