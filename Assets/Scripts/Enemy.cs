using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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
        if (other.gameObject.name == "Player")
        {
            GameManager.Instance.PlayerHealth -= 10;
            Debug.Log("Player Health: " + GameManager.Instance.PlayerHealth);
        }
    }
}
