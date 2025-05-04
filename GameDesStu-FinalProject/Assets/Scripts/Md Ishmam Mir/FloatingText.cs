using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float lifetime = 1f;

    private TextMeshProUGUI textComponent;

    void Awake()
    {
        // Look for TextMeshProUGUI in self or children
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        if (textComponent == null)
        {
            Debug.LogError("FloatingText: No TextMeshProUGUI found!");
        }
    }

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
        if (textComponent != null)
        {
            textComponent.text = message;
        }
    }
}
