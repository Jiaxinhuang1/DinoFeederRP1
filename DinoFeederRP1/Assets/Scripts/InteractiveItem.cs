using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractiveItem : MonoBehaviour
{
    AudioManager aM;
    private GameObject actionIndicator;
    private GameObject player;
    private bool isExist;
    private bool isInside;
    private bool isGrabbed;
    public string actionText;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        aM = AudioManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && (isInside || isGrabbed))
        {
            Grab();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isInside = true;
            if (!isExist)
            {
                actionIndicator = Instantiate(Resources.Load("ActionIndicator")) as GameObject;
                actionIndicator.GetComponentInChildren<TextMeshProUGUI>().text = actionText;
                actionIndicator.transform.position = new Vector3(this.gameObject.transform.position.x + 10, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
                LeanTween.moveZ(actionIndicator, this.gameObject.transform.position.z - 20, 0.2f);
                LeanTween.scaleY(actionIndicator, 1, 0.2f);
                isExist = true;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isInside = false;
            LeanTween.moveZ(actionIndicator, this.gameObject.transform.position.z, 0.2f);
            LeanTween.scaleY(actionIndicator, 0, 0.2f).setDestroyOnComplete(true);
            isExist = false;
        }
    }

    public void Grab()
    {
        if (this.transform.GetComponentInParent<ItemBehaviour>() != null)
        {
            if (player.GetComponent<PlayerBehaviour>().contains != this.transform.gameObject.GetComponentInParent<ItemBehaviour>())
            {
                if (player.GetComponent<PlayerBehaviour>().contains != null)
                {
                    player.GetComponent<PlayerBehaviour>().contains.transform.parent = player.transform.parent;
                    player.GetComponent<PlayerBehaviour>().contains.transform.position = this.transform.parent.position;
                }
                player.GetComponent<PlayerBehaviour>().contains = this.transform.gameObject.GetComponentInParent<ItemBehaviour>();
                this.transform.parent.parent = player.transform;
                this.transform.parent.localPosition = Vector3.zero;
                isGrabbed = true;
                aM.pickUpSound.Play();
            }
            else
            {
                player.GetComponent<PlayerBehaviour>().contains.transform.parent = player.transform.parent;
                player.GetComponent<PlayerBehaviour>().contains = null;
                isGrabbed = false;
                aM.plantSound.Play();
            }
        }
    }
}
