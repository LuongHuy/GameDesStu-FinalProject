using UnityEngine;
using UnityEngine.UI;

public class Scene2_PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float jetpackForce = 10f;
    public float maxFuel = 100f;
    public float fuelConsumptionRate = 10f;
    public float fuelRechargeRate = 5f;
    public KeyCode jetpackKey = KeyCode.E;

    public bool jetpackUnlocked = false;
    public Slider fuelSlider;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isJetpacking;
    private float currentFuel;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentFuel = maxFuel;

        if (fuelSlider != null)
            fuelSlider.value = 1f;
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot();
        }

        // Jetpack input
        if (jetpackUnlocked && Input.GetKey(jetpackKey) && currentFuel > 0)
        {
            isJetpacking = true;
            currentFuel -= fuelConsumptionRate * Time.deltaTime;
        }
        else
        {
            isJetpacking = false;
            if (jetpackUnlocked && currentFuel < maxFuel)
            {
                currentFuel += fuelRechargeRate * Time.deltaTime;
            }
        }

        // Update fuel UI
        if (jetpackUnlocked && fuelSlider != null)
        {
            fuelSlider.value = currentFuel / maxFuel;
        }
    }

    void FixedUpdate()
    {
        if (isJetpacking)
        {
            rb.AddForce(Vector2.up * jetpackForce, ForceMode2D.Force);
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void UnlockJetpack()
    {
        jetpackUnlocked = true;
        Debug.Log("Jetpack acquired!");
        currentFuel = maxFuel; // Optional: refill fuel when acquired
    }
}
