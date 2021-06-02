using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ResetTrigger : MonoBehaviour
{
    public bool totalReset = false;
    private MagmaScript magma;
    private MagmaScript magmaLogs;
    private TouchActivator dialogue;

    private void Start()
    {
        dialogue = GameObject.Find("Boat1").GetComponent<TouchActivator>();
        magma = GameObject.Find("MagmaBridges").GetComponent<MagmaScript>();
        magmaLogs = GameObject.Find("MagmaLogs").GetComponent<MagmaScript>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
        {
            if (totalReset)
            {
                if (dialogue != null)
                {
                    Debug.Log("stopdialogue");
                    dialogue.StopDialogue();
                }
                if (magma != null)
                {
                    Debug.Log("stopmagma");
                    magma.StopMagma();
                }
                if (magmaLogs != null)
                {
                    Debug.Log("stopmagma2");
                    magmaLogs.StopMagma();
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