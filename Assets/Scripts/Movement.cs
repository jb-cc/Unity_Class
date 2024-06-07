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

    [SerializeField]
    private float jumpForce = 10;
    [SerializeField]
    private float jumpingMaxHeight = 0.5f;
    [SerializeField]
    private float fallFactor = 0.9f;
    
    private bool _isOnGround = false;
    private bool _isJumping = false;
    
    private Animator _animator;
    
    private Vector3 movementDirection3d;
    private Rigidbody _rigidbody;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _animator = playerFigure.GetComponent<Animator>();
        
        if (_rigidbody == null)
        {
            Debug.LogError("Rigidbody not found");
        }

        StartCoroutine(FallControlFlow());
    }

    // Update is called once per frame
    void Update()
    {
       /* if (Input.GetKey(KeyCode.W))
            gameObject.transform.position += new Vector3(0, 0, -1f) * _velocity;*/ 
       PerformMovement();

       
    }

    void PerformMovement()
    {
        if (movementDirection3d != Vector3.zero)
        {
            transform.forward = -Camera.main.transform.forward;
            Quaternion cameraAlignedForwardRotation = transform.rotation;
            transform.forward = movementDirection3d;
            transform.rotation *= cameraAlignedForwardRotation;
        }
        _animator.SetBool("isWalking", movementDirection3d.magnitude > 0);
        _animator.SetBool("isJumping", _isJumping);

        if (movementType == MovementType.TransformBased)
        { 
            float movementStrength = Vector3.Magnitude(movementDirection3d);
            gameObject.transform.Translate(new Vector3(0,0,1) * _velocity * movementStrength);
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
        if (_isJumping || !_isOnGround)
        {
            return;
        }
        StartCoroutine(JumpControlFlow());
    }
    
    private IEnumerator JumpControlFlow()
    {
        _isJumping = true;
        float jumpHeight = transform.position.y + jumpingMaxHeight;
        Debug.Log("jump started" + Time.time);
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Force);
        while (transform.position.y < jumpHeight)
        {
            Debug.Log("jump ongoing" + Time.time);
            yield return null;
        }
        _rigidbody.AddForce(Vector3.up * jumpForce * -1 * fallFactor, ForceMode.Force );
        _isJumping = false;
    }
    
    private IEnumerator FallControlFlow()
    {
        RaycastHit hit;
        float previousY;
        float currentY = transform.position.y;
        // StopCoroutine(JumpControlFlow());
        while (true)
        {
           bool raycastSuccess = Physics.Raycast(transform.position, Vector3.down, out hit);
           if (raycastSuccess && hit.collider.gameObject.CompareTag("Ground") && hit.distance <= 0.50001f)
           {
               _isJumping = false;
               StopCoroutine(JumpControlFlow());
               _isOnGround = true;
           }
           else
           {
               _isOnGround = false;
           }
           yield return null;
        }
    }
}
