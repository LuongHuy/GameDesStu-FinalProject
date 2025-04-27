using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    public string value;

    // Start is called before the first frame update
    void Start()
    {
        text.text = value;  
        Destroy(gameObject, 0.5f);
    }
}
