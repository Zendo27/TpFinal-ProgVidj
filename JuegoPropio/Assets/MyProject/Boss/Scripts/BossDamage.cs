using UnityEngine;

public class BossDamage : MonoBehaviour
{
    public int contactDamage = 1;
    public float damageCooldown = 1f;
    private float timer = 0f;

    private void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        TryDamagePlayer(other);
    }

    private void OnTriggerStay(Collider other)
    {
        TryDamagePlayer(other);
    }

    void TryDamagePlayer(Collider other)
    {
        if (timer > 0) return;

        if (other.CompareTag("Player"))
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();

            if (ph != null)
            {
                ph.TakeDamage(contactDamage);
                timer = damageCooldown;
            }
        }
    }
}
