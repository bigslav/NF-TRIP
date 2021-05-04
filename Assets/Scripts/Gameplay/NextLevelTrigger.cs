using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    public string Level;

    private void OnTriggerEnter(Collider other)
    {
        GlobalVariables.spawnToCheckointId = 0;
        SceneManager.LoadScene(Level);
    }
}
