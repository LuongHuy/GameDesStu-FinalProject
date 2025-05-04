using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class OnboardingPoint : MonoBehaviour
{
    public GameObject onboardInfo;
    public BoxCollider2D collider;

    private void Awake()
    {
        onboardInfo.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onboardInfo.SetActive(true);
            collider.enabled = false;
        }
    }
}
