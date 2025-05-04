using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float lifetime = 1f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }

    public void SetText(string message)
    {
        GetComponent<TextMeshProUGUI>().text = message;
    }
}
