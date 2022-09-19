using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Rigidbody2D rb;
    Vector2 movement;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("StartScreen");
        }
        // returns arrow key inputs and WASD
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Use blend tree from animtor
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetAxis("Horizontal") > 0.5f || Input.GetAxis("Horizontal") < -0.5f || Input.GetAxis("Vertical") > 0.5f || Input.GetAxis("Vertical") < -0.5f)
        {
            anim.SetFloat("LastMoveX", Input.GetAxis("Horizontal"));
            anim.SetFloat("LastMoveY", Input.GetAxis("Vertical"));
        }
        //Debug.Log(anim.GetFloat("LastMoveX") + "   :  " + anim.GetFloat("LastMoveY"));
        /*
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Translate(0f, 1f, 0f);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.Translate(0f, -1f, 0f);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(-1f, 0f, 0f);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(1f, 0f, 0f);
        }
        */
    }

    // executed on a fixed time default = 50 frames per sec (best with physics)
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
