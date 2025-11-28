using UnityEngine;

public class FloatingVisual : MonoBehaviour
{
    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float speed = 2.5f;

    [Header("Levitation Sound")]
    [SerializeField] private AudioSource levitationAudio;

    private float startY;

    private void Start()
    {
        startY = transform.localPosition.y;

        // Reproducimos el sonido si está asignado
        if (levitationAudio != null && !levitationAudio.isPlaying)
        {
            levitationAudio.loop = true;
            levitationAudio.Play();
        }
    }

    private void Update()
    {
        float offset = Mathf.Sin(Time.time * speed) * amplitude;

        transform.localPosition = new Vector3(
            transform.localPosition.x,
            startY + offset,
            transform.localPosition.z
        );
    }
}
