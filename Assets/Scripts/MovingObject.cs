using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    
    [SerializeField]
    private Vector3 distanceToMove;
    [SerializeField]
    private float speed = 1f;
    
    private Vector3 startPosition;
    private Vector3 endPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform.position;
        endPosition = startPosition + distanceToMove;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.PingPong(Time.time * speed, 1));
    }
}
