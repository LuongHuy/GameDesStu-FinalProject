using UnityEngine;
using UnityEngine.UI;

public class JetpackController : MonoBehaviour
{
    [Header("Jetpack Settings")]
    public float jetpackForce = 10f;
    public float maxFuel = 100f;
    public float fuelConsumptionRate = 10f;
    public float fuelRechargeRate = 5f;
    public KeyCode jetpackKey = KeyCode.E;

    [Header("Fuel UI")]
    public Image fuelBarFill;           // replaces slider
    public GameObject fuelBarCover;     // optional background or border

    public SpriteRenderer playerRenderer; // assign in Inspector
    public Color normalColor = Color.white;
    public Color jetpackColor = Color.cyan; // or any color you like

    [Header("Unlock")]
    public bool jetpackUnlocked = false;

    private Rigidbody2D rb;
    private float currentFuel;
    private bool isJetpacking;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentFuel = maxFuel;

        if (fuelBarCover != null)
            fuelBarCover.SetActive(false); // hide initially
    }

    void Update()
    {
        if (!jetpackUnlocked) return;

        if (Input.GetKey(jetpackKey) && currentFuel > 0)
        {
            isJetpacking = true;
            currentFuel -= fuelConsumptionRate * Time.deltaTime;

            if (fuelBarCover != null)
                fuelBarCover.SetActive(true);
        }
        else
        {
            isJetpacking = false;

            if (currentFuel < maxFuel)
                currentFuel += fuelRechargeRate * Time.deltaTime;
        }

        UpdateFuelUI();
    }

    void FixedUpdate()
    {
        if (isJetpacking)
        {
            rb.AddForce(Vector2.up * jetpackForce, ForceMode2D.Force);
        }
    }

    void UpdateFuelUI()
    {
        if (fuelBarFill != null)
        {
            fuelBarFill.fillAmount = currentFuel / maxFuel;
        }
    }

    public void UnlockJetpack()
    {
        jetpackUnlocked = true;
        currentFuel = maxFuel;

        if (fuelBarCover != null)
            fuelBarCover.SetActive(true);

        Debug.Log("Jetpack unlocked!");

        playerRenderer.color = jetpackColor;
    }
}
