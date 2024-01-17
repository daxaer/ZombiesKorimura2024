using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ObjectPool 
{
    private readonly RecyclableObject _prefabAreutilizar;
    private readonly HashSet<RecyclableObject> _instantiateObjects;
    private Queue<RecyclableObject> _objetosReciclados;


    public ObjectPool(RecyclableObject prefab)
    {
        _prefabAreutilizar = prefab;
        _instantiateObjects = new HashSet<RecyclableObject>();
    }

    public void Init(int numberOfInitialObjects)
    {
        _objetosReciclados = new Queue<RecyclableObject>(numberOfInitialObjects);

        for(var i=0; i<numberOfInitialObjects; i++)
        {
            var instance = InstantiateNewInstance();
            instance.gameObject.SetActive(false);
            _objetosReciclados.Enqueue(instance);
        }
    }

    private RecyclableObject InstantiateNewInstance()
    {
        var instance = Object.Instantiate(_prefabAreutilizar);
        instance.Configure(this);
        return instance;
    }
    private RecyclableObject InstantiateNewInstance(Transform transform)
    {
        var instance = Object.Instantiate(_prefabAreutilizar,transform);
        instance.Configure(this);
        return instance;
    }

    public T Spawn<T>()
    {
        var recyclableObject = GetInstance();
        _instantiateObjects.Add(recyclableObject);
        recyclableObject.gameObject.SetActive(true);
        recyclableObject.Init();
        return recyclableObject.GetComponent<T>();
    }
    public T SpawnWithPosition<T>(Transform transform)
    {
        var recyclableObject = GetInstance(transform);
        _instantiateObjects.Add(recyclableObject);
        recyclableObject.gameObject.transform.position = transform.position;
        recyclableObject.gameObject.SetActive(true);
        recyclableObject.Init();
        return recyclableObject.GetComponent<T>();
    }
    //public T SpawnWithPosition<T>(Transform transform,RecyclableObject target)
    //{
    //    var recyclableObject = GetInstance(transform);
    //    _instantiateObjects.Add(recyclableObject);
    //    recyclableObject.gameObject.transform.position = transform.position;
    //    //recyclableObject.GetComponent<Bala>().ConfigureTarget(target);
    //    recyclableObject.GetComponent<Bala>().ConfigureTarget();
    //    recyclableObject.gameObject.SetActive(true);
    //    recyclableObject.Init();
    //    return recyclableObject.GetComponent<T>();
    //}
    //public T SpawnWithPosition<T>(Transform transform,Transform playerTransform, RecyclableObject target)
    //{
    //    var recyclableObject = GetInstance(playerTransform);
    //    _instantiateObjects.Add(recyclableObject);
    //    recyclableObject.gameObject.transform.position = transform.position;
    //    //recyclableObject.GetComponent<Bala>().ConfigureTarget(target);
    //    recyclableObject.GetComponent<Bala>().ConfigureTarget();
    //    recyclableObject.gameObject.SetActive(true);
    //    recyclableObject.Init();
    //    return recyclableObject.GetComponent<T>();
    //}
    public T SpawnWithPositionBala<T>(Transform transform)
    {
        var recyclableObject = GetInstance(transform);
        _instantiateObjects.Add(recyclableObject);
        recyclableObject.gameObject.transform.position = transform.position;
        recyclableObject.GetComponent<Bala>().ConfigureTarget();
        recyclableObject.gameObject.SetActive(true);
        recyclableObject.Init();
        return recyclableObject.GetComponent<T>();
    }

    private RecyclableObject GetInstance()
    {
        if(_objetosReciclados.Count > 0)
        {
            return _objetosReciclados.Dequeue();
        }

        var instance = InstantiateNewInstance();
        return instance;
    }
    private RecyclableObject GetInstance(Transform transform)
    {
        if (_objetosReciclados.Count > 0)
        {
            return _objetosReciclados.Dequeue();
        }

        var instance = InstantiateNewInstance(transform);
        return instance;
    }
    public void RecycleGameObject(RecyclableObject gameObjectToRecycle)
    {
        var wasInstantiated = _instantiateObjects.Remove(gameObjectToRecycle);
        Assert.IsTrue(wasInstantiated, $"{gameObjectToRecycle.name} no fue instanciado");

        gameObjectToRecycle.gameObject.SetActive(false);
        gameObjectToRecycle.Release();
        _objetosReciclados.Enqueue(gameObjectToRecycle);
    }
}
