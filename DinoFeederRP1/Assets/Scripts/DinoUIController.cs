using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DinoUIController : MonoBehaviour
{
    public GameObject titleText;
    public GameObject playButton;
    public GameObject howButton;
    public GameObject quitButton;
    public GameObject howPanel;
    // Start is called before the first frame update
    void Start()
    {
        howPanel.SetActive(false);
        titleText.transform.localScale = new Vector3(0, 0, 0);
        playButton.transform.localScale = new Vector3(0, 0, 0);
        howButton.transform.localScale = new Vector3(0, 0, 0);
        quitButton.transform.localScale = new Vector3(0, 0, 0);

        LeanTween.scale(titleText, new Vector3(1, 1, 1), 0.2f).setDelay(0.2f);
        LeanTween.scale(playButton, new Vector3(1, 1, 1), 0.3f).setDelay(0.4f);
        LeanTween.scale(howButton, new Vector3(1, 1, 1), 0.3f).setDelay(0.6f);
        LeanTween.scale(quitButton, new Vector3(1, 1, 1), 0.3f).setDelay(0.8f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonSelect(GameObject button)
    {
        LeanTween.scale(button, new Vector3(1.2f, 1.2f, 1.2f), 0.1f);
    }

    public void ButtonDeselect(GameObject button)
    {
        LeanTween.scale(button, new Vector3(1, 1, 1), 0.1f);
    }

    public void PlayGame()
    {
        LeanTween.scale(titleText, new Vector3(0, 0, 0), 0.3f).setDelay(0.2f);
        LeanTween.scale(playButton, new Vector3(0, 0, 0), 0.3f);
        LeanTween.scale(howButton, new Vector3(0, 0, 0), 0.3f).setDelay(0.2f);
        LeanTween.scale(quitButton, new Vector3(0, 0, 0), 0.3f).setDelay(0.2f).setOnComplete(ChangeTileScene);
    }

    public void OpenHow()
    {
        howPanel.SetActive(true);
        LeanTween.scale(howPanel, new Vector3(1, 1, 1), 0.3f);
    }

    public void CloseHow()
    {
        LeanTween.scale(howPanel, new Vector3(0, 0, 0), 0.3f).setOnComplete(()=> howPanel.SetActive(false));
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeTileScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
