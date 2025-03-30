using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public Vector3 checkpoint;
    public void Awake()
    {
        checkpoint = transform.position;
    }
    public void Respawn()
    {
        transform.position = checkpoint;
        gameObject.SetActive(true);
    }
    
}
