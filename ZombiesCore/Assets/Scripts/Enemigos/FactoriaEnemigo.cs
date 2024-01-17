
using System.Collections.Generic;
using UnityEngine;

public class FactoriaEnemigo
{
    private readonly EnemyConfiguration enemyConfiguration;
    private Dictionary<string, ObjectPool> _pools;

    public FactoriaEnemigo(EnemyConfiguration enemyConfiguracion)
    {
        _pools = new Dictionary<string, ObjectPool>();
        this.enemyConfiguration = enemyConfiguracion;
        var enemigos = enemyConfiguracion.Enemigo;
        foreach (var enemigo in enemigos)
        {
            var objectPool = new ObjectPool(enemigo);
            objectPool.Init(0);
            _pools.Add(enemigo.IdEnemigo, objectPool);
        }
    }

    public Enemy Crear(string idPersonaje,Transform lugarSpawn)
    {
        var enemy = _pools[idPersonaje];
        return enemy.SpawnWithPosition<Enemy>(lugarSpawn);
    }
}
