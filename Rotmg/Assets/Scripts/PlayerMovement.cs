using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
    private Vector2 direction;
    private Vector3 moveTowards;
    private Rigidbody2D rb;
    
    public float speed = 1f;
    private float rotationSide = 0f;
    public float rotationSpeed = 1f;

    public float camOffset;
    private bool isOffset;
    Camera mainCam;

    private void Start()
    {
        mainCam = gameObject.GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();  
    }
    public void Rotate(InputAction.CallbackContext context)
    {
        rotationSide = context.ReadValue<float>();
    }
    public void OffsetCam(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isOffset)
            {
                mainCam.transform.position -= camOffset*transform.up;
                
            }
            else
            {
                mainCam.transform.position += camOffset * transform.up;
            }
            isOffset = !isOffset;
            
        }
    }

    public void FixedUpdate()
    {

        if (direction != Vector2.zero)
        {
            
            
            moveTowards = direction.x * gameObject.transform.right + direction.y * gameObject.transform.up;
            
            gameObject.transform.position += moveTowards * Time.deltaTime * speed;

            
        }

        if (rotationSide != 0)
        {
            gameObject.transform.Rotate(new Vector3(0,0,rotationSide) * rotationSpeed * Time.deltaTime);
        }
    }

    
    
}
