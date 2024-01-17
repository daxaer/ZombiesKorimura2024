using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "CustomSO/ Configuracion armas")]
public class ArmasConfiguracion : ScriptableObject
{
    [SerializeField] private DetallesArma[] _armas;
    public DetallesArma[] Armas => _armas;

    private Dictionary<int, DetallesArma> _armasDiccionario;
    public Dictionary<int, DetallesArma> ArmaDiccionario => _armasDiccionario;

    private void Awake()
    {
        _armasDiccionario = new Dictionary<int, DetallesArma>();
        foreach (var arma in _armas)
        {
            _armasDiccionario.Add(arma.IdArma, arma);
        }
    }

    public DetallesArma GetArmaPrefabById(int id)
    {
        if(!_armasDiccionario.TryGetValue(id,out var arma))
        {
            throw new Exception($"Arma con el id {id} no existe! revisar");
        }
        return arma;
    }

}
