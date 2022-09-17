using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoBehaviour : MonoBehaviour
{
    public enum Diet{ carnivore, herbivore }
    public Diet diet;
    public float moveDelay;
    public float moveSpeed;
    public float moveDistanceMax;
    Vector2 moveTarget;
    Rigidbody2D rb;

    IEnumerator TakeMove()
    {
        while (true){
            yield return new WaitForSeconds(moveDelay);
            moveTarget = Random.insideUnitCircle * moveDistanceMax;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position != (Vector3)moveTarget){
            transform.LookAt(moveTarget);
        }
    }
}
