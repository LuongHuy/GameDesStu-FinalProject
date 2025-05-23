using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public LevelStatsManager statsManager;
    public Image[] winStars;
    public Sprite filledStar;
    public Sprite emptyStar;

    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI boxMessageText;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Audio")]
    public AudioClip jumpSound;
    public AudioClip shootSound;
    public AudioClip jetpackPickupSound;
    public AudioClip invulnerablePickupSound;
    public AudioClip coinPickupSound;
    public AudioClip winSound;
    public AudioClip backgroundMusic;

    private AudioSource audioSource;
    private Rigidbody2D rb;

    private bool hasKey = false;
    public bool bossDefeated = false;

    public GameObject endGameWinUI;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        if (backgroundMusic != null)
        {
            audioSource.loop = true;
            audioSource.clip = backgroundMusic;
            audioSource.Play();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Shoot();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(collision.gameObject);
            Debug.Log("Key collected!");
        }

        if (collision.CompareTag("WinBox"))
        {
            if (hasKey && bossDefeated)
            {
                Debug.Log("Box unlocked and boss defeated!");
                ShowWinUI();
            }
            else if (!hasKey)
            {
                Debug.Log("Need a key to open this box.");
                StartCoroutine(ShowBoxMessage("You need a key", 2f));
            }
            else if (!bossDefeated)
            {
                Debug.Log("Defeat the boss first.");
                StartCoroutine(ShowBoxMessage("You must defeat the boss", 2f));
            }
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            if (bullet != null)
            {
                PlayerBullet bulletScript = bullet.GetComponent<PlayerBullet>();
                if (bulletScript != null)
                {
                    float yRotation = transform.eulerAngles.y;
                    bulletScript.direction = (yRotation == 0f) ? Vector2.right : Vector2.left;
                }
            }

            if (shootSound != null) audioSource.PlayOneShot(shootSound);
        }
    }

    void ShowWinUI()
    {
        if (endGameWinUI != null)
            endGameWinUI.SetActive(true);

        if (winSound != null)
            AudioSource.PlayClipAtPoint(winSound, transform.position);

        Time.timeScale = 0f;

        if (finalScoreText != null)
            finalScoreText.text = "Score: " + GlobalGameManager.instance.score;

        int starsEarned = statsManager.CalculateStars(GlobalGameManager.instance.score);
        for (int i = 0; i < winStars.Length; i++)
        {
            winStars[i].sprite = i < starsEarned ? filledStar : emptyStar;
        }

        statsManager.SaveStars(GlobalGameManager.instance.score);

        // Let ScoreTracker handle high score saving
        GetComponent<ScoreTracker>()?.SaveCurrentScore();
    }

    IEnumerator ShowBoxMessage(string message, float duration)
    {
        if (boxMessageText != null)
        {
            boxMessageText.text = message;
            boxMessageText.gameObject.SetActive(true);
            yield return new WaitForSeconds(duration);
            boxMessageText.gameObject.SetActive(false);
        }
    }
}