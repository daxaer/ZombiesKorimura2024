using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncontrarEnemigoMasCercano : MonoBehaviour
{
    private readonly KdTree<RecyclableObject> _enemigos = new KdTree<RecyclableObject>();
    private readonly KdTree<Personaje> _personajes = new KdTree<Personaje>();

    public KdTree<RecyclableObject> Enemigos => _enemigos;

    public static EncontrarEnemigoMasCercano Instance { get; private set; }

    private RecyclableObject _enemigoMasCercano = null;

    //private RecyclableObject _enemigoMasCercano;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    public RecyclableObject GetEnemigoMasCercano()
    {
        _enemigos.UpdatePositions();
        if (_enemigos.Count == 0) return _enemigoMasCercano;
        foreach (var personaje in _personajes)
        {

            _enemigoMasCercano = _enemigos.FindClosest(personaje.CurrentPosition);
            Debug.DrawLine(personaje.CurrentPosition,_enemigoMasCercano.transform.position, Color.red);

        }
        return _enemigoMasCercano;
    }
    public void AddEnemigo(RecyclableObject itemToAdd)
    {
       _enemigos.Add(itemToAdd);
    }
    public void RemoverEnemigo(RecyclableObject enemy)
    {
        _enemigos.Remove(enemy);
        Debug.Log(_enemigos.Count);
    }
    public void AddPersonaje(Personaje personaje)
    {
        _personajes.Add(personaje);
    }
    public void DamageComponent()
    {

    }
}