//using Cinemachine;
using UnityEngine;

public class FactoriaPersonajes
{
    private readonly PersonajesConfiguracion personajesConfiguracion;

    public FactoriaPersonajes(PersonajesConfiguracion personajesConfiguracion)
    {
        this.personajesConfiguracion = personajesConfiguracion;
    }

    public Personaje Crear(string idPersonaje)
    {
        var personaje = personajesConfiguracion.GetPersonajePrefabById(idPersonaje);
        var personajeInstantiated = Object.Instantiate(personaje);
        EncontrarEnemigoMasCercano.Instance.AddPersonaje(personajeInstantiated);

        return personajeInstantiated;
    }

}
