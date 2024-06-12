using UnityEngine;

public class MaxAmmo : Consumibles
{
    [SerializeField] private AudioConfig audioConfig;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponentInChildren<DetallesArma>().RecoverMaxAmmo();
            AudioManager.Instance.PlayAudio2D(audioConfig);
            Destroy(gameObject);
        }
    }
}