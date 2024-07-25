using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectible 
{
    void Collected();

    void IncrementValue(Collector script);
}
