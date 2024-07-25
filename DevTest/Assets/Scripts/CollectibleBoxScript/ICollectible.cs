using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for all collectible items.
/// </summary>
public interface ICollectible 
{
    void Collected();

    void IncrementValue(Collector script);
}
