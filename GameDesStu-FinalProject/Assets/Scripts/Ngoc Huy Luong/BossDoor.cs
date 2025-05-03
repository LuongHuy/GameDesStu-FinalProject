using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    public BoxCollider2D BoxCollider2D;
    public GameObject doorImage;
    public Transform character;
    private bool isClosed;
    public float distanceCheck;
    public GameObject bossObject;

    public Transform bossCamRoom;


    private void Start()
    {
        BoxCollider2D.enabled = true; 
        BoxCollider2D.isTrigger = true;
        doorImage.SetActive(false);
    }

    private void Update()
    {
        if (character == null || isClosed == true)
        {
            return;
        }
        if (transform.position.x + distanceCheck < character.position.x)
        {
            isClosed = true;
            BoxCollider2D.isTrigger = false;
            CameraControl.Instance.SetFollowTarget(bossCamRoom,11);
            MusicBackgroundControl.Instance.PlayBossMusic();
            doorImage.SetActive(true);
            bossObject.SetActive(true);
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            character = collision.transform;
        }
    }
}
