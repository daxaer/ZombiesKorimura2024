using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerZombies : MonoBehaviour
{
    [SerializeField] private float _tiempoSpawn;
    [SerializeField] private float _spawnDelay;
    private FactoriaEnemigo _factoriaEnemigos;
   
    private Transform[] _transformSpawns;
    public bool DetenerSpawns = false;

    public void ConfigurarFactoriaEnemigos(FactoriaEnemigo factoriaEnemigo)
    {
        _factoriaEnemigos = factoriaEnemigo;
    }

    private void Awake()
    {
        _transformSpawns = GetComponentsInChildren<Transform>();
    }
   

    public void SpawnZombiesRandom()
    {
        int numRandom = Random.Range(0,_transformSpawns.Length);
        _factoriaEnemigos.Crear("Zombie",_transformSpawns[numRandom].transform);

        
    }

}
