using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public GameObject bulletPrefab;
    public Transform shootPoint;

    [Header("Stats")]
    public float maxHealth = 100;
    private float currentHealth;

    [Header("Movement")]
    public float moveSpeed = 3f;
    public float dashSpeed = 15f;
    public float dashCooldown = 4f;
    private float dashTimer = 0f;

    private bool isPhase2 = false;
    private bool isDashing = false;
    private float dashDuration = 0.25f;
    private float dashTime = 0f;

    private enum Phase2Attack { Directed, Trident, MachineGun }
    private Phase2Attack currentAttack = Phase2Attack.Directed;

    private float attackCooldown = 2f;
    private float attackTimer = 0f;

    void Start()
    {
        currentHealth = maxHealth;
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (currentHealth > maxHealth * 0.5f)
        {
            Phase1();
        }
        else
        {
            if (!isPhase2)
            {
                isPhase2 = true;
            }
            Phase2();
        }
    }

    // ------------------------------------------------------------
    // --------------------  FASE 1 ------------------------------
    // ------------------------------------------------------------
    void Phase1()
    {
        if (isDashing)
        {
            dashTime += Time.deltaTime;

            // --- Movimiento solo en X y Z ---
            transform.position += transform.forward * dashSpeed * Time.deltaTime;

            if (dashTime >= dashDuration)
            {
                isDashing = false;
            }
            return;
        }

        // Seguir al jugador
        Vector3 dir = (player.position - transform.position).normalized;

        // --- Evitar moverse en Y ---
        dir.y = 0;

        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.forward = dir; // Ya corregido el dir
        // -----------------------------------

        // Dash ocasional
        dashTimer += Time.deltaTime;
        if (dashTimer >= dashCooldown)
        {
            dashTimer = 0f;
            StartDash();
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTime = 0f;

        Vector3 dir = (player.position - transform.position).normalized;

        // --- Evitar inclinación ---
        dir.y = 0;

        transform.forward = dir;
    }

    // ------------------------------------------------------------
    // --------------------  FASE 2 ------------------------------
    // ------------------------------------------------------------
    void Phase2()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer < attackCooldown) return;

        attackTimer = 0f;

        switch (currentAttack)
        {
            case Phase2Attack.Directed:
                ShootDirected();
                currentAttack = Phase2Attack.Trident;
                break;

            case Phase2Attack.Trident:
                ShootTrident();
                currentAttack = Phase2Attack.MachineGun;
                break;

            case Phase2Attack.MachineGun:
                StartCoroutine(ShootMachineGun());
                currentAttack = Phase2Attack.Directed;
                break;
        }
    }

    // Ataque 1 - bala dirigida
    void ShootDirected()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        dir.y = 0; // --- CORRECCIÓN ---
        Quaternion rot = Quaternion.LookRotation(dir);

        Instantiate(bulletPrefab, shootPoint.position, rot);
    }

    // Ataque 2 - tridente
    void ShootTrident()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        dir.y = 0;  // --- CORRECCIÓN ---

        Quaternion center = Quaternion.LookRotation(dir);

        // Centro
        Instantiate(bulletPrefab, shootPoint.position, center);

        // Lados
        Instantiate(bulletPrefab, shootPoint.position, center * Quaternion.Euler(0, 25, 0));
        Instantiate(bulletPrefab, shootPoint.position, center * Quaternion.Euler(0, -25, 0));
    }

    // Ataque 3 - ráfaga rápida
    System.Collections.IEnumerator ShootMachineGun()
    {
        float duration = 1f;
        float fireRate = 0.1f;
        float timer = 0f;

        while (timer < duration)
        {
            timer += fireRate;

            Vector3 dir = (player.position - transform.position).normalized;
            dir.y = 0;  // --- CORRECCIÓN ---

            Quaternion rot = Quaternion.LookRotation(dir);
            Instantiate(bulletPrefab, shootPoint.position, rot);

            yield return new WaitForSeconds(fireRate);
        }
    }

    // ------------------------------------------------------------
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
