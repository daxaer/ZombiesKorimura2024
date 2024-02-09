using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerZombies : MonoBehaviour
{
    [SerializeField] private float _tiempoSpawn;
    [SerializeField] private float _spawnDelay;
    private FactoriaEnemigo _factoriaEnemigos;
    private int _spawnActive;

    public bool DetenerSpawns = false;

    public void ConfigurarFactoriaEnemigos(FactoriaEnemigo factoriaEnemigo)
    {
        _factoriaEnemigos = factoriaEnemigo;
    }

    public void SpawnZombiesRandom()
    {
        _factoriaEnemigos.Crear("Zombie", WaveSpawnerManager.Instance.GetTransform(WaveSpawnerManager.Instance.GetSpawnTipe()));
    }

    public void SpawnerActive(int active)
    {
        _spawnActive = active;
    }

   
}
