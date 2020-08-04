using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public bool Need_To_Move;

    public Transform Carriage1;
    public Transform Carriage2;

    public float Speed;
    public bool Direction;

    public float Z_End;

    public Spawner spawner;

    private void FixedUpdate()
    {
        if (Need_To_Move)
        {
            if (transform.position.z > Z_End)
            {
                Carriage1.position += Carriage1.forward * 0.01f * Speed * (Direction ? 1 : -1);
                Carriage2.position += Carriage2.forward * 0.01f * Speed * (Direction ? 1 : -1);
                transform.position = new Vector3(transform.position.x, transform.position.y, Carriage1.localPosition.z);
            } else
            {
                spawner.WallPassed();
                Destroy(gameObject);
            }
        }
    }
}
