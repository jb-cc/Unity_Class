using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    void Awake()  {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
       // gameObject.SetActive(false);
       // Destroy(gameObject);
        Debug.Log(other.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
