using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the actal collectible box script to detect collision and update box count.
[RequireComponent(typeof(BoxCollider))]
public class CollectibleBox : MonoBehaviour , ICollectible
{
    //Interface inherited function similar to all collectible
    public void Collected()
    {
        gameObject.SetActive(false);
    }

    // Interface inherited function for incrementing 
    public void IncrementValue(Collector script)
    {
        script._boxCollected++;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Compares to check if player 
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<Collector>())
            {
                IncrementValue(other.GetComponent<Collector>());
                Collected();
            }
        }
    }

}
