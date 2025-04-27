using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackMaster : MonoBehaviour
{
    // Singleton set up
    static FeedbackMaster _instance;
    public static FeedbackMaster Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }
}
