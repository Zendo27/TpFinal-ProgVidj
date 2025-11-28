using UnityEngine;

public class BossHealth : MonoBehaviour
{

    [Header("Death Sound")]
    [SerializeField] private AudioSource deathAudioSource;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private GameObject deathEffect;

    public float maxHealth = 100f;
    public float currentHealth;

    private BossController boss;

    void Start()
    {
        currentHealth = maxHealth;
        boss = GetComponent<BossController>();

        BossManager bm = FindFirstObjectByType<BossManager>();
        if (bm != null)
        {
            bm.AddBoss();
            Debug.Log("BossHealth: Registrado en BossManager");
        }
        else
        {
            Debug.LogError("❌ No se encontró BossManager en la escena.");
        }
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        Debug.Log("Boss daño recibido. HP actual = " + currentHealth);

        if (boss != null)
            boss.TakeDamage(dmg);

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log("Boss murió.");

        // Avisar al BossManager primero
        BossManager bm = FindFirstObjectByType<BossManager>();
        if (bm != null)
            bm.BossDied();

        // Instanciar efecto de muerte
        if (deathEffect != null)
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        // Si hay audio → reproducirlo y destruir luego
        if (deathAudioSource != null && deathClip != null)
        {
            deathAudioSource.PlayOneShot(deathClip);

            // Destruir al boss *después* de que termine el sonido
            Destroy(gameObject, deathClip.length);
        }
        else
        {
            // Si no hay audio → destruir inmediatamente
            Destroy(gameObject);
        }
    }
}
