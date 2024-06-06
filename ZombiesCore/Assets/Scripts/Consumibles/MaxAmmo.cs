using UnityEngine;

public class MaxAmmo : Consumibles
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponentInChildren<DetallesArma>().RecoverMaxAmmo();
            Destroy(gameObject);
        }
    }
}