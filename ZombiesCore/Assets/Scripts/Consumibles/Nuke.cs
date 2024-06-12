using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuke : Consumibles
{
    [SerializeField] private AudioConfig audioConfig;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var Enemigos = EncontrarEnemigoMasCercano.Instance.Enemigos;
            AudioManager.Instance.PlayAudio2D(audioConfig);
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