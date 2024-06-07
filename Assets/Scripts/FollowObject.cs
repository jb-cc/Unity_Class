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
    private float _fixYOffset;
    private float _distance;
    // Start is called before the first frame update
    void Start()
    {
        _otherObjectTransform = objectToFollow.GetComponent<Transform>();
        _thisObjectTransform = gameObject.GetComponent<Transform>();
        _fixYOffset = _thisObjectTransform.position.y - _otherObjectTransform.position.y;
        _distance = Vector3.Distance(_thisObjectTransform.position, _otherObjectTransform.position);
        SetOffset();
    }

    public void SetOffset()
    {
        _offsetOfThisObjectToOtherObject = _thisObjectTransform.position - _otherObjectTransform.position;
        _offsetOfThisObjectToOtherObject.Normalize();
        _offsetOfThisObjectToOtherObject *= _distance;
        _offsetOfThisObjectToOtherObject.y = _fixYOffset;
    }
    // Update is called once per frame
    void Update()
    {
        _thisObjectTransform.position = _otherObjectTransform.position + _offsetOfThisObjectToOtherObject;
    }
}
