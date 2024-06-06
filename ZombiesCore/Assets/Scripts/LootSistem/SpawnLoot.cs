using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLoot : MonoBehaviour
{
    private float probabilidadDeEspawnear = 1;
    private float suerte = 1;
    [SerializeField] private LootTable _loot;

    public void SpawnLootProbability()
    {
        float probabilidad = Random.Range(0.0f, 100.0f);
        if( probabilidad <= probabilidadDeEspawnear + suerte)
        {
            Instantiate(_loot.GetRandom(), transform.position, Quaternion.identity);
        }
    }
}