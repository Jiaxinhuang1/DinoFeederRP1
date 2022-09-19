using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemBehaviour : MonoBehaviour
{
    public GameManager.ItemType type;

    AudioManager aM;
    GameManager gM;
    private GameObject[] actionIndicator;
    private GameObject player;
    private bool isExist;
    private bool isInside;
    [SerializeField] private bool isGrabbed;
    public string[] actionText;
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
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!isExist)
            {
                actionIndicator = new GameObject[actionText.Length];
                for(int i = 0; i < actionText.Length; i++){
                    actionIndicator[i] = Instantiate(Resources.Load("ActionIndicator")) as GameObject;
                    actionIndicator[i].transform.position += new Vector3(transform.position.x, transform.position.y + (i * 5f), transform.position.z);
                    actionIndicator[i].GetComponentInChildren<TextMeshProUGUI>().text = actionText[i];
                    LeanTween.moveY(actionIndicator[i], this.gameObject.transform.position.y + 15 + (i * 5), 0.2f);
                    LeanTween.scaleY(actionIndicator[i], 1, 0.2f);
                }
                isExist = true;
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collision){
        if (collision.tag == "Player" && collision.TryGetComponent(out PlayerBehaviour playerBehaviour)){
            playerBehaviour.grabTarget = this;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerBehaviour>().grabTarget = null;
            foreach(GameObject i in actionIndicator){
                LeanTween.moveY(i, this.gameObject.transform.position.y, 0.2f);
                LeanTween.scaleY(i, 0, 0.2f).setDestroyOnComplete(true);   
            }
            isExist = false;
        }
    }
}
