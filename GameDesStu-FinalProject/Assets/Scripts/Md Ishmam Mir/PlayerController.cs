using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public LevelStatsManager statsManager;           // Reference to score manager
    public Image[] winStars;                         // 3 star UI images
    public Sprite filledStar;                        // Yellow star
    public Sprite emptyStar;                         // Gray star

    public TextMeshProUGUI finalScoreText;           // Score display on win screen

    public TextMeshProUGUI boxMessageText; // Assign in Inspector

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    [Header("Visual Flip")]
    public Transform playerVisual; // Assign your sprite child here

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
    private bool isGrounded;
    private bool facingRight = true;

    private bool hasKey = false;
    public bool bossDefeated = false;

    public Animator anim;

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
        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Movement
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        // Flip visual only
        if (move > 0 && !facingRight)
        {
            facingRight = true;
            FlipVisual(true);
        }
        else if (move < 0 && facingRight)
        {
            facingRight = false;
            FlipVisual(false);
        }

        // Animation: Set "isMove" to true or false based on movement input
        if (anim != null)
        {
            anim.SetBool("isMove", move != 0);  // true if walking
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            if (jumpSound != null) audioSource.PlayOneShot(jumpSound);
        }

        // Shoot
        if (Input.GetKeyDown(KeyCode.F))
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


    void FlipVisual(bool faceRight)
    {
        if (playerVisual != null)
        {
            Vector3 scale = playerVisual.localScale;
            scale.x = Mathf.Abs(scale.x) * (faceRight ? 1 : -1);
            playerVisual.localScale = scale;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            PlayerBullet bulletScript = bullet.GetComponent<PlayerBullet>();
            if (bulletScript != null)
            {
                bulletScript.direction = facingRight ? Vector2.right : Vector2.left;
            }

            if (shootSound != null) audioSource.PlayOneShot(shootSound);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    public GameObject endGameWinUI;

    void ShowWinUI()
    {
        if (endGameWinUI != null)
        {
            endGameWinUI.SetActive(true);
        }

        // Play win sound BEFORE freezing time
        if (winSound != null)
            AudioSource.PlayClipAtPoint(winSound, transform.position);

        Time.timeScale = 0f;

        if (finalScoreText != null)
        {
            finalScoreText.text = "Score: " + GlobalGameManager.instance.score;
        }

        int starsEarned = statsManager.CalculateStars(GlobalGameManager.instance.score);

        for (int i = 0; i < winStars.Length; i++)
        {
            winStars[i].sprite = i < starsEarned ? filledStar : emptyStar;
        }

        statsManager.SaveStars(GlobalGameManager.instance.score);
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

    //// Show stars earned

    //int starsEarned = statsManager.CalculateStars(GlobalGameManager.instance.score);

    //for (int i = 0; i < winStars.Length; i++)
    //{
    //    winStars[i].sprite = i < starsEarned ? filledStar : emptyStar;
    //}

    //// Save stars
    //statsManager.SaveStars(GlobalGameManager.instance.score);
}
