using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomSO/ Configuracion Personaje")]

public class PersonajesConfiguracion : ScriptableObject
{
    [SerializeField] private Personaje[] personajes;
    private Dictionary<string, Personaje> personajeDictionary;

    private void Awake()
    {
        personajeDictionary = new Dictionary<string,Personaje>();

        foreach(var personaje in personajes)
        {
            personajeDictionary.Add(personaje.IdPersonaje,personaje);
        }
    }

    public Personaje GetPersonajePrefabById(string id)
    {
        if(!personajeDictionary.TryGetValue(id,out var personaje))
        {
            throw new Exception($"No se encontro el personaje con el siguiente id{id}");
        }
        return personaje;
    }
}

