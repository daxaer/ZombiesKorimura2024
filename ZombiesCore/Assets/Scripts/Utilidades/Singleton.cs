using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject nuevoGameObject = new GameObject();
                    _instance = nuevoGameObject.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this as T;
    }
}