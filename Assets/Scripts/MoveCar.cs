using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveCar : MonoBehaviour
{
    [SerializeField] private float originalVelocity = .5f;

    private float currentVelocity;
    // Start is called before the first frame update
    void Start()
    {
        currentVelocity = originalVelocity;
        StartCoroutine(CheckForVelocity());
    }

    // Update is called once per frame
    void Update()
    {
        //use transform.Translate to move Car into its forward direction by currentVelocity
        transform.Translate(Vector3.forward * currentVelocity);
    }

    IEnumerator CheckForVelocity()
    {
        RaycastHit hit;
        
        //DONT FORGET TO EXECUTE the Coroutine "CheckForVelocity" once
        while (true)
        {
            //Raycast?
            //if player is hit currentVelocity is half of original velocity.
            //if not, currentVelocity is original velocity
            //use Physics.Raycast to check if player is in front of the car
            bool raycastSuccess = Physics.Raycast(transform.position, transform.forward, out hit, 10f);
            if (raycastSuccess && hit.collider.tag == "CarHitzone")
            {
                currentVelocity = originalVelocity / 5;
            }
            else
            {
                currentVelocity = originalVelocity;
            }
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision other) /*or make OnTriggerEnter(Collider other) - depending on how you configure your colliders.. I went with physical collisions here, since my car should act as a physical object.. e.g. for moving the player if it hits it.*/
    {
        Debug.Log("Collision with: " + other.gameObject.name);
        if (other.gameObject.tag == "TurnCar")
        {
            //use transform.Rotate to rotate car by 180 degrees around its y Axis
            transform.Rotate(Vector3.up * 180);
        }
    }
   
}
