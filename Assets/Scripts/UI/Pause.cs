using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour {
    public GameObject PauseGameObject;

    public GameObject mainPanel;

    public GameObject loadPanel;

    public GameObject optionsPanel;

    public GameObject savePanel;

    public string MainMenu;
    //Settings
    //public Slider volumeSlider;
    //public Text volumeValueText;

    // Start is called before the first frame update
    void Start() {
		//if (!PlayerPrefs.HasKey("volume")) {
		//	PlayerPrefs.SetFloat("volume", 1);
		//}
		//SoundManager.instance.PlaySound(SoundManager.Sounds.MENU_THEME);
	}

	// Update is called once per frame
	void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToGame();
        }
    }
        
	public void Exit() {
		Application.Quit();
	}

    public void BackToMenu()
    {
        mainPanel.SetActive(true);

        loadPanel.SetActive(false);
        optionsPanel.SetActive(false);
        savePanel.SetActive(false);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(MainMenu);
    }

    public void BackToGame() {
        Time.timeScale = 1;
        mainPanel.SetActive(true);
        PauseGameObject.SetActive(false);
        loadPanel.SetActive(false);
        optionsPanel.SetActive(false);
        savePanel.SetActive(false);
	}

    public void OpenLoad()
    {
        mainPanel.SetActive(false);
        loadPanel.SetActive(true);
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
}
