using UnityEngine;

public class MaxAmmo : Consumibles
{
    private void OnTriggerEnter(Collider other)
    {
        if(CompareTag("Player"))
        {
            other.GetComponentInChildren<DetallesArma>().RecoverMaxAmmo();
            Destroy(gameObject);
        }
    }
}