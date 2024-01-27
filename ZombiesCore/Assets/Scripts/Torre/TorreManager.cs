using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof( MejorasTorreTiers),typeof(TorreVida))]
public class TorreManager : Singleton<TorreManager>
{   
    private MejorasTorreTiers _mejorasTorre;
    private TorreVida _torreVida;

    private void Start()
    {
        _mejorasTorre = GetComponent<MejorasTorreTiers>();
        _torreVida = GetComponent<TorreVida>();

    }

    public void MejorarTier(int dinero)
    {
        _mejorasTorre.MejorarTier(dinero);
    }
    public void HacerDañoTorre(int dañoARecibir)
    {
        _torreVida.RecibirDaño(dañoARecibir);
    }
    

    public void ReconstruirTorre()
    {

    }
}
