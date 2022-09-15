using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int aliveCount;
    private Slider progressSlider;

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
    }
}
