using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMasterW3 : MonoBehaviour
{
    // Singleton set up
    static GameMasterW3 _instance;
    public static GameMasterW3 Instance { get { return _instance; } }

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

    [SerializeField] TextMeshProUGUI point;

    float coinCollected;
    float secondaryObjective;

    Transform currentCheckpoint = null;
    List<GameObject> unsaveCollectible = new List<GameObject>();
    List<GameObject> unsavePlatform = new List<GameObject>();
    List<GameObject> unsaveEnemy = new List<GameObject>();
    float currentPoint;
    float savedPoint;

    private void Start()
    {
        ResetAll();
    }
    public void SaveStage(Transform checkpoint)
    {
        // save current point and checkpoint location
        if (checkpoint != currentCheckpoint)
        {
            savedPoint = currentPoint;
            currentCheckpoint = checkpoint;
            Debug.Log("New checkpoint registered");
        }

        // reset information
        unsaveCollectible.Clear();
        unsavePlatform.Clear();
        unsaveEnemy.Clear();
    }

    public void ResetState()
    {
        currentPoint = savedPoint;

        // re initiate each collectible, platform and enemy


    }

    public void ResetAll()
    {
        currentCheckpoint = null;
        currentPoint = 0;
        savedPoint=0;
        point.SetText(currentPoint.ToString());

        unsaveCollectible = new List<GameObject>();
        unsavePlatform = new List<GameObject>();
        unsaveEnemy = new List<GameObject>();


    }

    public Transform GetCheckpoint()
    {
        return currentCheckpoint;
    }

    public void AddUnsaveCollectible()
    {

    }
    public void AddUnsaveEnemy()
    {

    }
    public void AddUnsavePlatform()
    {

    }

    public void CollectToken()
    {
        Debug.Log("Collect Token");
        currentPoint += 1;
        point.SetText(currentPoint.ToString());
    }
    public void CollectSecondaryObjective()
    {
        Debug.Log("Collect Secondary Objective");
        secondaryObjective++;
        currentPoint += 10;
        point.SetText(currentPoint.ToString());
    }
    public void CollectMainObjective()
    {
        Debug.Log("Collect Main Objective");
        Win();
    }

    public void Win()
    {
        Debug.Log("You Win");
    }

    public void Lose()
    {
        Debug.Log("You lose");
    }
}
