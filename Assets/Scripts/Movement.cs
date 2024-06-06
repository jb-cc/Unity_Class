using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


internal enum MovementType
{
    TransformBased,
    PhysicsBased
}

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float _velocity = 1;
    [SerializeField]
    private ForceMode selectedForceMode;
    [SerializeField]
    private MovementType movementType;
    [SerializeField] 
    private GameObject playerFigure;
    
    private Animator _animator;
    
    private Vector3 movementDirection3d;
    private Rigidbody _rigidbody;
    
    private bool _isJumping = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _animator = playerFigure.GetComponent<Animator>();
        
        if (_rigidbody == null)
        {
            Debug.LogError("Rigidbody not found");
        }        
    }

    // Update is called once per frame
    void Update()
    {
       /* if (Input.GetKey(KeyCode.W))
            gameObject.transform.position += new Vector3(0, 0, -1f) * _velocity;*/ 
       PerformMovement();

       if (_isJumping && _rigidbody.velocity.y < 0.05)
       {
           _isJumping = false;
       }
    }

    void PerformMovement()
    {
        _animator.SetBool("isWalking", movementDirection3d.magnitude > 0);
        _animator.SetBool("isJumping", _isJumping);

        if (movementType == MovementType.TransformBased)
        { 
            gameObject.transform.position += movementDirection3d * _velocity;
        } 
        else if (movementType == MovementType.PhysicsBased)
        {
            _rigidbody.AddForce(movementDirection3d, selectedForceMode);
        }
    }
    
    void OnMovement(InputValue inputValue)
    {
        Vector2 movementDirection = inputValue.Get<Vector2>();
        movementDirection3d = new Vector3(movementDirection.x, 0, movementDirection.y);
    }

    void OnJump(InputValue inputValue)
    {
        
        if (!_isJumping)
        {
            _rigidbody.AddForce(Vector3.up * 6, selectedForceMode);
            _isJumping = true;
            Debug.Log("Jump");
        }
        else
        {
            Debug.Log("Pressed Jump but already jumping");
        }
        
        
        
    }
}
