using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CollectibleBox : MonoBehaviour , ICollectible
{
    public void Collected()
    {
        gameObject.SetActive(false);
    }

    public void IncrementValue(Collector script)
    {
        script._boxCollected++;
    }

    private void OnTriggerEnter(Collider other)
    {
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
