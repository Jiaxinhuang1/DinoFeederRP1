using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TriggerNotification : MonoBehaviour
{
    private GameObject actionIndicator;
    public bool isTouching;
    public string actionText;
    // Start is called before the first frame update
    void Start()
    {
        isTouching = false;
        actionIndicator = GameObject.FindGameObjectWithTag("ActionIndicator");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Avatar")
        {
            isTouching = true;
            //Debug.Log("Entering watercan");
            actionIndicator.GetComponentInChildren<TextMeshProUGUI>().text = actionText;
            LeanTween.scaleY(actionIndicator, 1, 0.1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Avatar")
        {
            isTouching = false;
            //Debug.Log("Exiting watercan");
            LeanTween.scaleY(actionIndicator, 0, 0.1f);
        }
    }
}
