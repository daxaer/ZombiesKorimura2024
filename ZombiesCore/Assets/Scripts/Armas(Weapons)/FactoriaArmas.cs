using System.Collections.Generic;
using UnityEngine;

public class FactoriaArmas 
{
    private readonly ArmasConfiguracion armasConfiguracion;
    private Dictionary<int, ObjectPool> _pools;
    public FactoriaArmas(ArmasConfiguracion armasConfiguracion)
    {
        _pools = new Dictionary<int, ObjectPool>();
        this.armasConfiguracion = armasConfiguracion;
        var balas = armasConfiguracion.Armas;
        foreach (var bala in balas)
        {
            var objectPool = new ObjectPool(bala.Bala);
            objectPool.Init(0);
            _pools.Add(bala.IdArma,objectPool);
        }
    }

    public DetallesArma Crear(int idArma, Transform transform)
    {
        var arma = armasConfiguracion.GetArmaPrefabById(idArma);
        return Object.Instantiate(arma, transform);
    }
    
    public DetallesArma CrearYAsignarAPersonaje(int idArma, Personaje personaje)
    {
        var transformPersonaje = personaje.PosicionArma;
        var arma = armasConfiguracion.GetArmaPrefabById(idArma);
        if(transformPersonaje.childCount<1)
        {
            return Object.Instantiate(arma,transformPersonaje);
        }
        else
        {
            Object.Destroy(transformPersonaje.GetChild(0).gameObject);
            return Object.Instantiate(arma, transformPersonaje);
        }
    }
    //public Bala CrearBala(int idArma, Transform balaSpawn, RecyclableObject target)
    //{
    //    var arma = _pools[idArma];
    //    return arma.SpawnWithPosition<Bala>(balaSpawn,target);
    //}
    //public Bala CrearBala(int idArma, Transform balaSpawn,Transform playerTransform, RecyclableObject target)
    //{
    //    var arma = _pools[idArma];
    //    return arma.SpawnWithPosition<Bala>(balaSpawn,playerTransform,target);
    //}

    public Bala CrearBala(int idArma, Transform balaSpawn)
    {
        var arma = _pools[idArma];
        return arma.SpawnWithPositionBala<Bala>(balaSpawn);
    }
}
