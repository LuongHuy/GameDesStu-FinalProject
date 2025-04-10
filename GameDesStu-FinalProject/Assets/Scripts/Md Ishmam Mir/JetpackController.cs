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
    public Slider fuelSlider;

    [Header("Unlock")]
    public bool jetpackUnlocked = false;

    private Rigidbody2D rb;
    private float currentFuel;
    private bool isJetpacking;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentFuel = maxFuel;

        if (fuelSlider != null)
        {
            fuelSlider.value = 1f;
        }
    }

    void Update()
    {
        if (!jetpackUnlocked) return;

        if (Input.GetKey(jetpackKey) && currentFuel > 0)
        {
            isJetpacking = true;
            currentFuel -= fuelConsumptionRate * Time.deltaTime;
        }
        else
        {
            isJetpacking = false;

            if (currentFuel < maxFuel)
            {
                currentFuel += fuelRechargeRate * Time.deltaTime;
            }
        }

        if (fuelSlider != null)
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

    public void UnlockJetpack()
    {
        jetpackUnlocked = true;
        currentFuel = maxFuel;
        Debug.Log("Jetpack unlocked!");
    }
}
