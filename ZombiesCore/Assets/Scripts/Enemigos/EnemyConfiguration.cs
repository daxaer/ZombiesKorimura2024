using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "CustomSO/ Configuracion enemigos")]
public class EnemyConfiguration : ScriptableObject
{
    [SerializeField] private Enemy[] _enemigo;
    public Enemy[] Enemigo => _enemigo;
    private Dictionary<string, Enemy> _enemigosDiccionario;

    private void Awake()
    {
        _enemigosDiccionario = new Dictionary<string, Enemy>();
        foreach (var enemy in _enemigo)
        {
            _enemigosDiccionario.Add(enemy.IdEnemigo, enemy);
        }
    }

    public Enemy GetEnemyPrefabById(string id)
    {
        if (!_enemigosDiccionario.TryGetValue(id, out var enemigo))
        {
            throw new Exception($"Arma con el id {id} no existe! revisar");
        }
        return enemigo;
    }
}
