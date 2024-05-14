using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DineroX2 : Consumibles
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Personaje>().StartMoneyX2();
            Destroy(gameObject);
        }
    }
}