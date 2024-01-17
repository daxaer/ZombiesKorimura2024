using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootTable: MonoBehaviour
{
    [System.Serializable]
    public struct Loot
    {
        public GameObject _item;
        public float _probabilidad;

        public Loot(GameObject item, float probabilidad)
        {
            this._item = item;
            this._probabilidad = probabilidad;
        }
    }

    public List<Loot> list = new List<Loot>();

    public int Count
    {
        get => list.Count;
    }

    public void Add(GameObject item, float probabilidad)
    {
        list.Add(new Loot(item, probabilidad));
    }

    public GameObject GetRandom()
    {
        float totalProbabilidad = 0;

        foreach (Loot loot in list)
        {
            totalProbabilidad += loot._probabilidad;
        }

        float value = Random.value * totalProbabilidad;

        float sumaProbabilidad = 0;

        foreach (Loot loot in list)
        {
            sumaProbabilidad += loot._probabilidad;

            if (sumaProbabilidad >= value)
            {
                return loot._item;
            }
        }
        return default(GameObject);
    }
}