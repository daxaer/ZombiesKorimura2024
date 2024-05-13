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
    [SerializeField] private Enemy enemi;

    public bool DetenerSpawns = false;

    public void ConfigurarFactoriaEnemigos(FactoriaEnemigo factoriaEnemigo)
    {
        _factoriaEnemigos = factoriaEnemigo;
    }

    public void SpawnZombiesRandom()
    {
        enemi = _factoriaEnemigos.Crear("Zombie", WaveSpawnerManager.Instance.GetTransform(WaveSpawnerManager.Instance.GetSpawnTipe()));
        enemi.SetEnemyLife(WaveSpawnerManager.Instance.GetLifeMultiplier());
    }

    public void SpawnerActive(int active)
    {
        _spawnActive = active;
    }

   
}
