using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractuableVendedor : Interactuable
{
    private bool _puedesAgarrarArma;
    
    [SerializeField] private Transform mostrador;
    [SerializeField] private DetallesArma _armaEnMostrador;
    [SerializeField] private int _precioArma;
    [SerializeField] private ArmasMostrador _armasMostrador;

    #region ArmaAleatoria

    private FactoriaArmas _factoriaArmas;
    private int _idArma = 0;
    private int _contador = 1000;

    public FactoriaArmas FactoriaArmas => _factoriaArmas;

    private void Start()
    {
        Invoke(nameof(MostrarArmasEspeciales), 1f);
    }

    public void ConfigurarFactoriaArma(FactoriaArmas factoriaArmas)
    {
        _factoriaArmas = factoriaArmas;
    }

    private void Interactuando()
    {
        var dineroJugador = _personaje._statsPersonaje._dineroActual;
        if (dineroJugador >= _precioArma)
        {
            PagarArma();
        }
    }

    private void PagarArma()
    {
        if (_idArma == 0)
        {
            _personaje._statsPersonaje._dineroActual -= _precioArma;
            _idArma = Random.Range(_contador, 1002);

            _armaEnMostrador = _factoriaArmas.Crear(_idArma, mostrador);
            _puedesAgarrarArma = true;
        }
        else if (_puedesAgarrarArma)
        {
            AsignarArma(_idArma, _personaje);
        }
    }

    private void AsignarArma(int idArma, Personaje personaje)
    {
        var arma = _factoriaArmas.CrearYAsignarAPersonaje(idArma, personaje);
        // var balasHijoArma = arma.GetComponentsInChildren<Transform>();
        // QuitarBalasDeArmaADestruir(balasHijoArma, personaje);
        // AsignarBalasANuevaArma(balasHijoArma, arma);
        arma.ConfigurarFactoriaArmas(_factoriaArmas);
        personaje.SetArma(arma);
        Destroy(_armaEnMostrador.gameObject);
        _idArma = 0;
        _puedesAgarrarArma = false;
    }

    // private void QuitarBalasDeArmaADestruir(Transform[] balas, Personaje personaje)
    // {
    //     foreach (var bala in balas)
    //     {
    //         bala.SetParent(personaje.PosicionArma);
    //     }
    // }
    //
    // private void AsignarBalasANuevaArma(Transform[] balas, DetallesArma arma)
    // {
    //     //SI OTRA ARMA TIENE DIFERENTES BALAS CAMBIAR EL SPRITE O PARTICULAS DE CADA BALA
    //     foreach (var bala in balas)
    //     {
    //         bala.SetParent(arma.BalaSpawnReference);
    //     }
    // }

    #endregion


    public override void Interaccion()
    {
        Interactuando();
    }
    

    public void MostrarArmasEspeciales()
    {
        var armas = _armasMostrador.GetArmasMostrador();

        foreach (var arma in armas)
        {
            var especialArma = _factoriaArmas.Crear(1001, arma.transform);
            especialArma.AddYAsignaArmasEspeciales(this);
        }
    }
}

public static class ArmasEspeciales
{
    public static void AddYAsignaArmasEspeciales(this DetallesArma arma, InteractuableVendedor interactuableVendedor)
    {
        arma.gameObject.AddComponent<BoxCollider>();
        arma.gameObject.AddComponent<ArmasEspecialesTrigger>();
        arma.GetComponent<ArmasEspecialesTrigger>().IdArma = arma.IdArma;
    }
}