using System.Collections;
using UnityEngine;

[System.Serializable]
public abstract class Consumibles : MonoBehaviour
{
    [SerializeField] private protected Transform _SpawnConsumible;
    [SerializeField] private float _despawnTime;
    public Transform SpawnConsumible { get => _SpawnConsumible; }
    private void OnEnable()
    {
        StartCoroutine(nameof(Desaparecer));
    }
    public virtual IEnumerator Desaparecer()
    {
        yield return new WaitForSeconds(_despawnTime);
        Destroy(gameObject);
    }
}