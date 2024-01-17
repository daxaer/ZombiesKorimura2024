
using UnityEngine;

public abstract class RecyclableObject : MonoBehaviour
{
    private ObjectPool _objectPool;
    // Start is called before the first frame update
    internal void Configure(ObjectPool objectPool)
    {
        _objectPool = objectPool;
    }

    // Update is called once per frame
    public void Reciclar()
    {
        _objectPool.RecycleGameObject(this);
    }

    /// <summary>
    /// Es como un constructor de cuando se iniciliace el objeto
    /// </summary>
    internal abstract void Init();

    /// <summary>
    /// Se llama antes despúes de que se desactiva el objeto
    /// </summary>
    internal abstract void Release();
}
