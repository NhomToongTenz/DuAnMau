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


    public void Demo3()
    {
        // return sum int a, b ,c 
        Func<int, int, int, int> sum = (a, b, c) => a + b + c;
    
        // return n is chan le 
        Func<int, string> check = (n) => n % 2 == 0 ? "chan" : "le";
        
        Action<int> _isEvenOrOdd = (n) => Debug.Log(n % 2 == 0 ? "chan" : "le");
        
        
        // Use the sum function
        int result = sum(1, 2, 3);
            System.Console.WriteLine(result);  // Outputs: 6

        // Use the check function
        string isEvenOrOdd = check(5);
            System.Console.WriteLine(isEvenOrOdd);  // Outputs: le
    }
    
    
}
