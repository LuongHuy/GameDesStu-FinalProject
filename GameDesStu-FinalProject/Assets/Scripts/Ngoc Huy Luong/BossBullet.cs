using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed;
    public Vector3 direction;


    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
