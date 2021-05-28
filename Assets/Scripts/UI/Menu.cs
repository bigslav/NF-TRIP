using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine.UI;
using UnityEngine.Video;
using System.IO;

public class Menu : MonoBehaviour {
	public GameObject mainPanel;

    public GameObject loadPanel;

    public GameObject levelSelectPanel;

    public GameObject optionsPanel;

    public GameObject savePanel;

    // Because we need someone to read our saves
    public SaveSystem saveSystem;


    public string[] levels;

    private string buttonName; 
    
    UnityEngine.Video.VideoPlayer videoPlayer;


    //Settings
    //public Slider volumeSlider;
    //public Text volumeValueText;

    // Start is called before the first frame update
    void Start()
    {
        /*
        GameObject camera = GameObject.Find("Main Camera");
        videoPlayer = camera.AddComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
        videoPlayer.url = "Assets/Introduction.mp4";
        videoPlayer.Prepare();
        */
        //if (!PlayerPrefs.HasKey("volume")) {
        //	PlayerPrefs.SetFloat("volume", 1);
        //}
        //SoundManager.instance.PlaySound(SoundManager.Sounds.MENU_THEME);
    }

	// Update is called once per frame
	void Update() {

	}

    public void StartLevelOne()
    {
        /*
        GameObject.Find("Menu").SetActive(false);
        videoPlayer.Play();
        videoPlayer.loopPointReached += CheckOver;
        */
        PlaySound("event:/ui/menu/level");
        DestroyGameObject(GameObject.Find("Music"));
        SceneManager.LoadScene(levels[0]);
        //GlobalVariables.spawnToCheckointId = 0;
        //SceneManager.LoadScene(levels[0]);
    }

    void DestroyGameObject(UnityEngine.GameObject gameObject)
    {
        Destroy(gameObject);
    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        GlobalVariables.spawnToCheckointId = 0;
        SceneManager.LoadScene(levels[0]);//the scene that you want to load after the video has ended.
    }

    public void StartLevelTwo()
    {
        PlaySound("event:/ui/menu/level");
        GlobalVariables.spawnToCheckointId = 0;
        SceneManager.LoadScene(levels[1]);
    }

    public void StartLevelThree()
    {
        PlaySound("event:/ui/menu/level");
        GlobalVariables.spawnToCheckointId = 0;
        SceneManager.LoadScene(levels[2]);
    }

    public void StartLevelFour()
    {
        PlaySound("event:/ui/menu/level");
        GlobalVariables.spawnToCheckointId = 0;
        SceneManager.LoadScene(levels[3]);
    }

    public void Exit() {
		Application.Quit();
	}

	public void BackToMenu() {
        PlaySound("event:/ui/menu/back");
        mainPanel.SetActive(true);

        loadPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
        optionsPanel.SetActive(false);
        //savePanel.SetActive(false);
	}

    public void OpenLoad()
    {
        PlaySound("event:/ui/menu/open");
        mainPanel.SetActive(false);
        loadPanel.SetActive(true);
    }
    public void OpenEpisodeSelect()
    {
        PlaySound("event:/ui/menu/open");
        mainPanel.SetActive(false);
        levelSelectPanel.SetActive(true);
    }
    public void OpenOptions()
    {
        PlaySound("event:/ui/menu/open");
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
    public void OpenSave()
    {
        PlaySound("event:/ui/menu/open");
        mainPanel.SetActive(false);
        savePanel.SetActive(true);
    }

    public void LoadLatestCheckpoint()
    {
        PlaySound("event:/ui/menu/open");
        GlobalVariables.spawnToCheckointId = saveSystem.savedCheckpontNum;
        SceneManager.LoadScene(levels[saveSystem.savedLevelNum]);
    }
    public void LoadGame(int num)
    {
        // 1
        if (File.Exists(num + ".save"))
        {
            // 2
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(num + ".save", FileMode.Open);
            SaveData save = (SaveData)bf.Deserialize(file);
            file.Close();

            // 3
            LoaderWatchDog.wasLoaded = 2;
            LoaderWatchDog.saveNum = num;
            SceneManager.LoadScene(save.sceneName);
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }
    void PlaySound(string buttonName)
    {
        FMOD.Studio.EventInstance Footstep = FMODUnity.RuntimeManager.CreateInstance(buttonName);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Footstep, transform, GetComponent<Rigidbody>());
        Footstep.start();
        Footstep.release();
    }
}
