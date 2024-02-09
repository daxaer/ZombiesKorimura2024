using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerSpawn : MonoBehaviour
{
    [SerializeField] private GROUPSPAWN groupSpawnType;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            WaveSpawnerManager.Instance.SetSpawnTipe(groupSpawnType);
        }
    }
}
