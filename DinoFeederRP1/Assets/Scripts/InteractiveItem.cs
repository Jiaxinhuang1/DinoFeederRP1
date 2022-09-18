using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractiveItem : MonoBehaviour
{
    AudioManager aM;
    GameManager gM;
    private GameObject actionIndicator;
    private GameObject player;
    private bool isExist;
    private bool isInside;
    [SerializeField] private bool isGrabbed;
    public string actionText;
    public string actionTwoText;
    // Start is called before the first frame update
    void Start()
    {
        aM = AudioManager.instance;
        gM = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(gM.player.GetComponent<PlayerBehaviour>().contains == this.transform.gameObject.GetComponentInParent<ItemBehaviour>())
        {
            isGrabbed = true;
        }
        else
        {
            isGrabbed = false;
        }
        if (Input.GetKeyDown(KeyCode.F) && (isInside || isGrabbed))
        {
            Grab();
        }
        if (Input.GetKeyDown(KeyCode.G) && (isInside && !isGrabbed))
        {
            Debug.Log("Grow");
            Instantiate(Resources.Load("Grow"), this.gameObject.transform);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isInside = true;
            if (!isExist)
            {
                if (this.transform.gameObject.GetComponentInParent<ItemBehaviour>().type == GameManager.ItemType.wateringCan)
                {
                    actionIndicator = Instantiate(Resources.Load("ActionIndicator")) as GameObject;
                    actionIndicator.GetComponentInChildren<TextMeshProUGUI>().text = actionText;
                }
                else if (this.transform.gameObject.GetComponentInParent<ItemBehaviour>().type == GameManager.ItemType.bone)
                {
                    actionIndicator = Instantiate(Resources.Load("TwoIndicator")) as GameObject;
                    actionIndicator.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = actionText;
                    actionIndicator.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = actionTwoText;
                }
                actionIndicator.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
                LeanTween.moveY(actionIndicator, this.gameObject.transform.position.y + 15, 0.2f);
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
            LeanTween.moveY(actionIndicator, this.gameObject.transform.position.y, 0.2f);
            LeanTween.scaleY(actionIndicator, 0, 0.2f).setDestroyOnComplete(true);
            isExist = false;
        }
    }

    public void Grab()
    {
        if (this.transform.GetComponentInParent<ItemBehaviour>() != null)
        {
            if (gM.player.GetComponent<PlayerBehaviour>().contains != this.transform.gameObject.GetComponentInParent<ItemBehaviour>())
            {
                if (!isGrabbed)
                {
                    if (gM.player.GetComponent<PlayerBehaviour>().contains != null)
                    {
                        gM.player.GetComponent<PlayerBehaviour>().contains.transform.parent = gM.player.transform.parent;
                        gM.player.GetComponent<PlayerBehaviour>().contains.transform.position = this.transform.parent.position;
                    }
                    gM.player.GetComponent<PlayerBehaviour>().contains = this.transform.gameObject.GetComponentInParent<ItemBehaviour>();
                    this.transform.parent.parent = gM.player.transform;
                    this.transform.parent.localPosition = Vector3.zero;
                    //isGrabbed = true;
                    aM.pickUpSound.Play();
                }
            }
            else
            {
                gM.player.GetComponent<PlayerBehaviour>().contains.transform.parent = gM.player.transform.parent;
                gM.player.GetComponent<PlayerBehaviour>().contains = null;
                //isGrabbed = false;
                aM.plantSound.Play();
            }
        }
    }
}
