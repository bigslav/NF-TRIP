using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ResetTrigger : MonoBehaviour
{
    public bool totalReset = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
        {
            if (totalReset)
            {
                if (GameObject.Find("Dialog4 - Boat").GetComponent<DialogueSnapshot>() != null)
                {
                    GameObject.Find("Dialog4 - Boat").GetComponent<DialogueSnapshot>().StopDialogue();
                }
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                collision.gameObject.GetComponent<InputHandler>().Spawn();
            }
        }
    }
}