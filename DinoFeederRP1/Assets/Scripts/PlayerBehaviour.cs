using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    Vector3 mousePos;
    GameManager gM;
    UIManager uM;
    AudioManager aM;
    LineRenderer lineRenderer;
    Rigidbody2D rb;
    public float moveSpeed;
    public float reach;
    public SpriteRenderer avatarSR;
    public Animator avatarAnimator;
    public LayerMask interactMask;
    public event System.Action interactEvent;
    public ItemBehaviour contains;
    public ItemBehaviour grabTarget;
    public int wateringCanPower;

    public GameObject[] plants;

    // Start is called before the first frame update
    void Start()
    {
        gM = GameManager.instance;
        uM = UIManager.instance;
        aM = AudioManager.instance;
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
        if ((rb.velocity.x != 0 || rb.velocity.y != 0) && !aM.footstepsSound.isPlaying)
        {
            aM.footstepsSound.Play();
        }
        else if (rb.velocity.x == 0 && rb.velocity.y == 0 && aM.footstepsSound.isPlaying)
        {
            aM.footstepsSound.Stop();
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
            //grab item underneath player
                if(Input.GetButtonDown("Grab")){
                    if(contains != null){
                        contains.transform.parent = this.transform.parent;
                        contains.transform.position = this.transform.position;
                        contains = null;
                    }
                    if(grabTarget != null){
                        contains = grabTarget;
                        grabTarget.transform.parent = this.transform;
                        grabTarget.transform.localPosition = Vector3.zero;
                    }
                }
            //interact at cursor position
            if(Input.GetButtonDown("Interact") && Vector3.Distance(transform.position, hit.transform.position) < reach){
                if(hit.transform.TryGetComponent(out TileBehaviour tileBehaviour) && contains != null && contains.type == GameManager.ItemType.wateringCan){    
                    print("name: " + hit.collider.name + ", distance: " + ", " + transform.position + ", " + hit.transform.position + ", " + Vector3.Distance(transform.position, hit.transform.position) + ", reach: " + reach);
                    if(tileBehaviour.currentState == GameManager.State.dead){
                        uM.aliveCount++;
                        tileBehaviour.health = Random.Range(wateringCanPower, wateringCanPower + 5);
                    }
                    else
                    {
                        if (tileBehaviour.contains == null)
                        {
                            GameObject spawnedPlant = Instantiate(plants[Random.Range(0, plants.Length)], hit.transform.position, Quaternion.identity, hit.transform);
                            tileBehaviour.contains = spawnedPlant;
                        }
                    }
                    tileBehaviour.currentState = GameManager.State.live;
                    aM.waterSound.Play();
                    Instantiate(Resources.Load("Water"), hit.transform);
                }
            }

            if(Vector3.Distance(transform.position, hit.transform.position) < reach){
                lineRenderer.endColor = Color.green;
            }    
        }
    }

    void OnDrawGizmos(){
        Gizmos.DrawLine(mousePos, new Vector3(mousePos.x, mousePos.y, 20));
        Gizmos.DrawLine(transform.position, mousePos);
    }
}
