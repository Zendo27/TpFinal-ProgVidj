using UnityEngine;
using System.Collections;
public class BossManager : MonoBehaviour
{
    public int bossesAlive = 0;

    private PanelManager panelManager;

    void Start()
    {
        panelManager = GameObject.FindWithTag("PanelManager")?.GetComponent<PanelManager>();

        // Inicialmente no contamos bosses, porque se instancian después
        Debug.Log("BossManager iniciado. bossesAlive = " + bossesAlive);
    }

    public void AddBoss()
    {
        bossesAlive++;
        Debug.Log("Boss agregado. Bosses vivos ahora = " + bossesAlive);
    }

    public void BossDied()
    {
        bossesAlive--;
        Debug.Log("Un boss murió. Bosses vivos ahora = " + bossesAlive);

        if (bossesAlive <= 0)
            StartCoroutine(VictoryDelay());
    }

    private IEnumerator VictoryDelay()
    {
        Debug.Log("Esperando 1.5 segundos antes del Victory...");
        yield return new WaitForSeconds(3f);

        if (panelManager != null)
            panelManager.TriggerVictory();
    }

}
