using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DineroX2 : Consumibles
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var Enemigos = EncontrarEnemigoMasCercano.Instance.Enemigos;

            foreach (var enemy in Enemigos)
            {
                if(enemy.isActiveAndEnabled)
                {
                    enemy.GetComponent<Damageable>().DoDamage(9999);
                }
            }
            Destroy(gameObject);
        }
    }
}