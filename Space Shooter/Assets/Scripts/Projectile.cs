using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float lifetime = 3f;
    private float speed = 10f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * moveDistance);
    }
}
