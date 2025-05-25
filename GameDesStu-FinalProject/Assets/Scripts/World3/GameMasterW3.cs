using Cinemachine;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

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
    [SerializeField] GameObject winGameUI;
    [SerializeField] Popup popupObj;

    // Save information
    Transform currentCheckpoint = null;
    List<Collectable> unsaveCollectible = new List<Collectable>();
    List<dropPlatformW3> unsavePlatform = new List<dropPlatformW3>();
    List<ElementStatus> unsaveEnemy = new List<ElementStatus>();
    List<BulletStatus> bulletsShot = new List<BulletStatus>();
    float currentPoint;
    float savedPoint;

    float coinCollected;
    float secondaryObjective;

    [Header("Boss mechanic")]
    // for boss fight
    [SerializeField] GameObject bossPrefab;
    //[SerializeField] GameObject boss;
    [SerializeField] Transform bossLocation;
    [SerializeField] GameObject gate;
    [SerializeField] BossGate gateTrigger;
    bool isBossActive;

    [Header("Camera control")]
    // Cam control
    [SerializeField] CinemachineVirtualCamera camMain;
    [SerializeField] CinemachineVirtualCamera camBoss;
    CinemachineVirtualCamera camCurr;


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

            // reset information
            unsaveCollectible.Clear();
            unsavePlatform.Clear();
            unsaveEnemy.Clear();

        }
    }

    public void ResetState()
    {
        currentPoint = savedPoint;
        point.SetText(currentPoint.ToString());

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
        foreach(var bullet in bulletsShot)
        {
            if (bullet != null)
            {
                Destroy(bullet.gameObject);
            }
        }

        // reset information
        unsaveCollectible.Clear();
        unsavePlatform.Clear();
        unsaveEnemy.Clear();
        bulletsShot.Clear();

        // reset Boss
        DeactivateBoss();
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

        // for bullet
        foreach (var bullet in bulletsShot)
        {
            if (bullet != null)
            {
                Destroy(bullet.gameObject);
            }
        }
        bulletsShot = new List<BulletStatus>();

        // for boss fight
        DeactivateBoss();

        // camera
        camCurr = camMain;
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

    GameObject currBoss;
    public void ActivateBoss()
    {
        isBossActive = true;
        // block exit
        gate.gameObject.SetActive(true);

        currBoss = Instantiate(bossPrefab, bossLocation.position, Quaternion.identity);
        currBoss.SetActive(true);
        camCurr = camBoss;
        camBoss.Priority = 20;
        //boss.SetActive(true);
    }
    public void DeactivateBoss()
    {
        if (isBossActive)
        {
            isBossActive = false;
            gate.SetActive(false);
            gateTrigger.Reset();
            Destroy(currBoss);
            camCurr = camMain;
            camBoss.Priority = 0;
        }
    }

    public void CameraControl(Vector2 input, bool facingRight)
    {
        //// Try to change camera
        //if (input.y < 0)
        //{
        //    camMain.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = 0.3f;
        //}
        //else
        //{
        //    camMain.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = 0.6f;
        //}

        //if (facingRight)
        //{
        //    camMain.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.3f;
        //    camMain.GetCinemachineComponent<CinemachineFramingTransposer>().m_BiasX = 0.16f;
        //}
        //else
        //{
        //    camMain.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.6f;
        //    camMain.GetCinemachineComponent<CinemachineFramingTransposer>().m_BiasX = -0.16f;
        //}
    }

    public void AddBullet(BulletStatus bullet)
    {
        bulletsShot.Add(bullet);
    }

    public void PointIncrease(float p, Vector3 pos, Transform parent)
    {
        currentPoint += p;
        point.SetText(currentPoint.ToString());
        SpawnPopup("+" + p, pos, parent);
    }
    public void CollectMainObjective()
    {
        Win();
    }

    // for star at the end game
    [SerializeField] Image[] Stars;
    [SerializeField] Sprite filledStar;
    [SerializeField] Sprite emptyStar;
    [SerializeField] float pointThreshold1 = 25;
    [SerializeField] float pointThreshold2 = 50;
    [SerializeField] float pointThreshold3 = 100;

    public float CalculateStarEarn()
    {
        if (currentPoint >= pointThreshold3)
            return 3f;
        else if (currentPoint >= pointThreshold2)
            return 2f;
        else if (currentPoint >= pointThreshold1)
            return 1f;
        else
            return 0f;
    }

    public void Win()
    {
        Debug.Log("You Win");
        Time.timeScale = 0;
        float starEarn = CalculateStarEarn();
        int previousStars = PlayerPrefs.GetInt("Level3" + "_stars", 0);

        // Only save if the new score is better
        if (starEarn > previousStars)
        {
            PlayerPrefs.SetInt("Level3" + "_stars", Mathf.RoundToInt(starEarn));
            PlayerPrefs.Save();
        }

        for (int i = 0; i < Stars.Length; i++) {
            Stars[i].sprite = i<starEarn?filledStar:emptyStar;
        }

        if (winGameUI != null)
        {
            winGameUI.SetActive(true);
        }

        string key = "HighScore_Level_" + SceneManager.GetActiveScene().buildIndex;
        int previousHigh = PlayerPrefs.GetInt(key, 0);

        if (currentPoint > previousHigh)
        {
            PlayerPrefs.SetInt(key, (int)currentPoint);
            PlayerPrefs.Save();
        }
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

    public void SpawnPopup(string message, Vector3 pos, Transform parent)
    {
        //Debug.Log(pos);
        Popup popup = Instantiate(popupObj, pos, new Quaternion());
        popup.value = message;
        Destroy(popup, 0.5f);
    }
}
