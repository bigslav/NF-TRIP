using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoPlayerScript : MonoBehaviour
{
    UnityEngine.Video.VideoPlayer videoPlayer;
    private bool triggered = false;
    private GameObject golem;
    private GameObject mushroom;

    // Start is called before the first frame update
    void Start()
    {
        golem = GameObject.Find("Golem");
        mushroom = GameObject.Find("Mushroom");
        GameObject camera = GameObject.Find("Camera");
        videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        videoPlayer.url = "Assets/Outroduction.mp4";
        videoPlayer.Prepare();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            if (golem != null)
            {
                golem.SetActive(false);
            }
            if (mushroom != null)
            {
                mushroom.SetActive(false);
            }
            videoPlayer.Play();
            videoPlayer.loopPointReached += CheckOver;
            triggered = true;
        }
    }
    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        GlobalVariables.spawnToCheckointId = 0;
        SceneManager.LoadScene("MainMenu"); //the scene that you want to load after the video has ended.
    }
}
