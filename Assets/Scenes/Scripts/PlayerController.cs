using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem; // Required for InputSystem
public class PlayerController : MonoBehaviour
{   
    
    [SerializeField] float moveSpeed = 5f;
    private Vector2 moveInput;
    
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
        
    }
    
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        
    }
    
    void Walk()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }


    int logarit(int x) => x * x;


   
    
    
}
