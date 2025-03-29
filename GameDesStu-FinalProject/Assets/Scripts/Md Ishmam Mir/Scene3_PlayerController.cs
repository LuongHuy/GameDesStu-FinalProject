using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene3_PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool isGrounded;

    public int maxLives = 3;
    private int currentLives;

    public Text livesText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentLives = maxLives;

        if (livesText != null)
        {
            livesText.text = "Lives: " + currentLives;
        }
    }


    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("FlyingEnemy"))
        {
            TakeDamage();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void TakeDamage()
    {
        currentLives--;

        if (livesText != null)
        {
            livesText.text = "Lives: " + currentLives;
        }

        if (currentLives <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
