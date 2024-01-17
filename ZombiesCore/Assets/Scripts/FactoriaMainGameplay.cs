using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoriaMainGameplay
{
    private readonly FactoriaPersonajes factoriaPersonajes;
    private readonly FactoriaArmas factoriaArmas;
    private readonly FactoriaEnemigo factoriaEnemigo;

    public FactoriaMainGameplay(FactoriaPersonajes factoriaPersonajes, FactoriaArmas factoriaArmas, FactoriaEnemigo factoriaEnemigo)
    {
        this.factoriaPersonajes = factoriaPersonajes;
        this.factoriaArmas = factoriaArmas;
        this.factoriaEnemigo = factoriaEnemigo;
    }

    public Personaje CrearPersonaje(string idPersonaje)
    {
        return factoriaPersonajes.Crear(idPersonaje);
    }

    public DetallesArma CrearArma(int idArma, Transform transform)
    {
        return factoriaArmas.Crear(idArma, transform);
    }
    public DetallesArma CrearYAsignarAPersonaje(int idArma, Personaje personaje)
    {
        return factoriaArmas.CrearYAsignarAPersonaje(idArma, personaje);
    }
    public Enemy CrearZombie(string idZombie,Transform lugarSpawn)
    {
        return factoriaEnemigo.Crear(idZombie,lugarSpawn);
    }
}
