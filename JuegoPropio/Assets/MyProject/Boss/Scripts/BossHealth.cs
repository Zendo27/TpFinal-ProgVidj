using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    private BossController boss;

    void Start()
    {
        currentHealth = maxHealth;
        boss = GetComponent<BossController>();
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        // Le avisa al BossController del daño (cambia de fase, etc.)
        if (boss != null)
            boss.TakeDamage(dmg);

        if (currentHealth <= 0)
        {
            // Acá podés poner animaciones, efectos, etc.
            Destroy(gameObject);
        }
    }
}
