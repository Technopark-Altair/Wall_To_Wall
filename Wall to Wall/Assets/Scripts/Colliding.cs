using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colliding : MonoBehaviour
{
    public Spawner spawner;

    private void OnTriggerEnter(Collider other)
    {
        spawner.Touched_Wall = true;
    }
}
