using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyboardmovement : MonoBehaviour
{
    Vector2 movement;
    Vector2 mousePos;
    public Camera cam2;
    public Rigidbody2D rb;
    public float moveSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam2.ScreenToWorldPoint(Input.mousePosition);


    }


    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
