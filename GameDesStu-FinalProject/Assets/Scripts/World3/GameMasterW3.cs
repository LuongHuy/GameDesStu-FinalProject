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
    // GUI
    [Header("GUI")]
    [SerializeField] TextMeshProUGUI point;
    [SerializeField] GameObject endGameUI;

    // Save information
    Transform currentCheckpoint = null;
    List<Collectable> unsaveCollectible = new List<Collectable>();
    List<dropPlatformW3> unsavePlatform = new List<dropPlatformW3>();
    List<ElementStatus> unsaveEnemy = new List<ElementStatus>();
    float currentPoint;
    float savedPoint;

    float coinCollected;
    float secondaryObjective;

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
        foreach (var con in unsaveCollectible)
        {
            con.ResetCollectible();
        }

        foreach (var plat in unsavePlatform)
        {
            plat.ResetPlatform();
        }

        foreach(var enemy in unsaveEnemy)
        {
            enemy.ResetElement();
        }

        // reset information
        unsaveCollectible.Clear();
        unsavePlatform.Clear();
        unsaveEnemy.Clear();
    }

    public void ResetAll()
    {
        currentCheckpoint = null;
        currentPoint = 0;
        savedPoint=0;
        point.SetText(currentPoint.ToString());

        unsaveCollectible = new List<Collectable>();
        unsavePlatform = new List<dropPlatformW3>();
        unsaveEnemy = new List<ElementStatus>();
    }

    public Transform GetCheckpoint()
    {
        return currentCheckpoint;
    }

    public void AddUnsaveCollectible(Collectable collectible)
    {
        unsaveCollectible.Add(collectible);
    }
    public void AddUnsaveEnemy(ElementStatus enemy)
    {
        unsaveEnemy.Add(enemy);
    }
    public void AddUnsavePlatform(dropPlatformW3 platform)
    {
        unsavePlatform.Add(platform);
    }

    public void CollectToken()
    {
        currentPoint += 1;
        point.SetText(currentPoint.ToString());
    }
    public void CollectSecondaryObjective()
    {
        secondaryObjective++;
        currentPoint += 10;
        point.SetText(currentPoint.ToString());
    }
    public void CollectMainObjective()
    {
        Win();
    }

    public void Win()
    {
        Debug.Log("You Win");
    }

    public void Lose()
    {
        Debug.Log("You lose");
        Time.timeScale = 0;
        if (endGameUI != null)
        {
            endGameUI.SetActive(true);
        }
    }
}
