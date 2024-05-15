using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    
    [SerializeField]
    private GameObject objectToFollow;
    
    private Transform _otherObjectTransform;
    private Transform _thisObjectTransform;
    
    private Vector3 _offsetOfThisObjectToOtherObject;
    // Start is called before the first frame update
    void Start()
    {
        _otherObjectTransform = objectToFollow.GetComponent<Transform>();
        _thisObjectTransform = gameObject.GetComponent<Transform>();
        _offsetOfThisObjectToOtherObject = _thisObjectTransform.position - _otherObjectTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _thisObjectTransform.position = _otherObjectTransform.position + _offsetOfThisObjectToOtherObject;
    }
}
