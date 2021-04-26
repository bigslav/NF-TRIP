using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
	public GameObject mainPanel;

    public GameObject loadPanel;

    public GameObject levelSelectPanel;

    public GameObject optionsPanel;

    public GameObject savePanel;

    // Because we need someone to read our saves
    public SaveSystem saveSystem;


    public string[] levels;

    //Settings
    //public Slider volumeSlider;
	//public Text volumeValueText;

	// Start is called before the first frame update
	void Start()
    {
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
        GlobalVariables.spawnToCheckointId = 0;
        SceneManager.LoadScene(levels[0]);
    }

    public void StartLevelTwo()
    {
        GlobalVariables.spawnToCheckointId = 0;
        SceneManager.LoadScene(levels[1]);
    }

    public void StartLevelThree()
    {
        GlobalVariables.spawnToCheckointId = 0;
        SceneManager.LoadScene(levels[2]);
    }
    
	public void Exit() {
		Application.Quit();
	}

	public void BackToMenu() {
		mainPanel.SetActive(true);

        loadPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
        optionsPanel.SetActive(false);
        //savePanel.SetActive(false);
	}

    public void OpenLoad()
    {
        mainPanel.SetActive(false);
        loadPanel.SetActive(true);
    }
    public void OpenEpisodeSelect()
    {
        mainPanel.SetActive(false);
        levelSelectPanel.SetActive(true);
    }
    public void OpenOptions()
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
    public void OpenSave()
    {
        mainPanel.SetActive(false);
        savePanel.SetActive(true);
    }

    public void LoadLatestCheckpoint()
    {
        GlobalVariables.spawnToCheckointId = saveSystem.savedCheckpontNum;
        SceneManager.LoadScene(levels[saveSystem.savedLevelNum]);
    }
}
