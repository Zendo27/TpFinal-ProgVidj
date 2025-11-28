using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject deathEffect;
    [Header("Vida")]
    public int maxHealth = 6;
    private int currentHealth;

    [Header("Invencibilidad")]
    public float invincibleDuration = 1.5f;
    public float blinkInterval = 0.1f;
    private bool isInvincible = false;

    private Renderer playerRenderer;
     

    private void Start()
    {
        currentHealth = maxHealth;

        // Agarra el renderer en hijos por seguridad
        playerRenderer = GetComponentInChildren<Renderer>();

        if (playerRenderer == null)
            Debug.LogError("⚠ No se encontró Renderer en el Player. Añádelo o revisa el modelo.");
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHealth -= damage;
        Debug.Log("Player HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(InvincibilityRoutine());
    }

    private void Die()
    {
        PanelManager pm = GameObject.FindWithTag("PanelManager").GetComponent<PanelManager>();

        if (pm != null)
            pm.TriggerGameOverDelayed(3f); // delay


        gameObject.SetActive(false);

        if (deathEffect != null)
            Instantiate(deathEffect, transform.position, Quaternion.identity);
    }


    private System.Collections.IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;

        float timer = 0f;

        while (timer < invincibleDuration)
        {
            if (playerRenderer != null)
                playerRenderer.enabled = !playerRenderer.enabled;

            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        // Asegurar que queda visible al final
        if (playerRenderer != null)
            playerRenderer.enabled = true;

        isInvincible = false;
    }
}
