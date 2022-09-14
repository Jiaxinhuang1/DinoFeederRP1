using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    Vector3 mousePos;
    GameManager gM;
    LineRenderer lineRenderer;
    Rigidbody2D rb;
    public float moveSpeed;
    public float reach;
    public SpriteRenderer avatarSR;
    public Animator avatarAnimator;
    public LayerMask interactMask;
    public event System.Action interactEvent;
    public ItemBehaviour contains;
    // Start is called before the first frame update
    void Start()
    {
        gM = GameManager.instance;
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Interact();
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0){
            if(Input.GetAxisRaw("Horizontal") != 0){
                avatarSR.flipX = Input.GetAxisRaw("Horizontal") < 0 ? true : false;
            }
            if(avatarSR.flipX){
                avatarAnimator.Play("Lean Right",1);
            }else{
                avatarAnimator.Play("Lean Left",1);
            }
        }else{
            avatarAnimator.Play("IdleRotation",1);
        }
    }

    void Movement(){
        rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * moveSpeed, 0.5f);
    }

    void Interact(){
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, 200, interactMask)){
            lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, transform.position.z+1));
            lineRenderer.SetPosition(1, hit.transform.position);
            lineRenderer.endColor = Color.red;
            if(Vector3.Distance(transform.position, hit.transform.position) < reach){
                lineRenderer.endColor = Color.green;
                if(Input.GetButtonDown("Interact")){
                    if(hit.transform.GetComponent<ItemBehaviour>() != null){
                        if(contains != hit.transform.gameObject.GetComponent<ItemBehaviour>()){
                            if(contains != null){
                                contains.transform.parent = this.transform.parent;
                            }
                            contains = hit.transform.gameObject.GetComponent<ItemBehaviour>();
                            hit.transform.parent = this.transform;
                            hit.transform.localPosition = Vector3.zero;
                        }
                        else{ 
                            contains.transform.parent = this.transform.parent;
                            contains = null;
                        }
                    }
                    else if(hit.transform.GetComponent<TileBehaviour>() != null && contains != null && contains.type == GameManager.ItemType.wateringCan){    
                        print("name: " + hit.collider.name + ", distance: " + ", " + transform.position + ", " + hit.transform.position + ", " + Vector3.Distance(transform.position, hit.transform.position) + ", reach: " + reach);
                        hit.transform.gameObject.GetComponent<TileBehaviour>().currentState = GameManager.State.live;
                    }
                } 
            }    
        }
    }

    void OnDrawGizmos(){
        Gizmos.DrawLine(mousePos, new Vector3(mousePos.x, mousePos.y, 20));
        Gizmos.DrawLine(transform.position, mousePos);
    }
}
