using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public int aliveCount;
    private Slider progressSlider;
    public GameObject pausePanel;

    public static UIManager instance;

    void Awake(){
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        progressSlider = GameObject.FindGameObjectWithTag("ProgressSlider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        progressSlider.value = aliveCount;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel.activeInHierarchy)
            {
                pausePanel.SetActive(false);
            }
            else
            {
                pausePanel.SetActive(true);
            }
        }
    }

    public void ButtonSelect(GameObject button)
    {
        LeanTween.scale(button, new Vector3(1.2f, 1.2f, 1.2f), 0.1f);
    }

    public void ButtonDeselect(GameObject button)
    {
        LeanTween.scale(button, new Vector3(1, 1, 1), 0.1f);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("StartScreen");
    }


}
