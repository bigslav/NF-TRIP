using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    public string Level;

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(Level);
    }
}
