using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoBehaviour : MonoBehaviour
{
    AudioManager aM;
    public GameManager.FoodType diet;
    public float moveDelay;
    public float moveSpeed;
    public float moveDistanceMax;
    public Vector3 moveTarget;
    public float searchSize;
    public LayerMask searchMask;
    Rigidbody2D rb;
    Bounds mapBounds;

    IEnumerator TakeMove()
    {
        while (true){
            yield return new WaitForSeconds(moveDelay);
            Vector3 proposedMoveTarget = (Vector2)transform.position + Random.insideUnitCircle * moveDistanceMax;
            Collider2D searchForFood = Physics2D.OverlapCircle(transform.position, searchSize, searchMask);
            if(searchForFood != null && searchForFood.TryGetComponent(out PlantBehaviour plantBehaviour) && plantBehaviour.foodType == diet){
                proposedMoveTarget = searchForFood.transform.position;
            }
            if(mapBounds.Contains(proposedMoveTarget)){
                moveTarget = proposedMoveTarget;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        aM = AudioManager.instance;
        mapBounds = new Bounds(new Vector3(0, 0), new Vector3(GameManager.instance.mapSizeX * GameManager.instance.tileSize, GameManager.instance.mapSizeY * GameManager.instance.tileSize));
        mapBounds.center += new Vector3(50, 50, 0);
        StartCoroutine(TakeMove());
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D consumeFood = Physics2D.OverlapCircle(transform.position, 0.5f, searchMask);
        if(consumeFood != null && consumeFood.TryGetComponent(out PlantBehaviour plantBehaviour) && plantBehaviour.foodType == diet){
            aM.burpSound.Play();
        }
        if(transform.position != moveTarget){
            transform.position = Vector3.MoveTowards(transform.position, moveTarget, moveSpeed);
            //transform.position = Vector3.Lerp(transform.position, newPos, 0.5f);
        }
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(mapBounds.center, mapBounds.size);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, searchSize);
    }
    /*
    void Produce(){
        Instantiate();
    }
    */
}