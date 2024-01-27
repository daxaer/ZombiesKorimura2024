
using UnityEngine;


//PARA REPARAR LA TORRE SE DEBE COMPRAR AL NPC QUE MEJORA LA TORRE
public class TorreVida : MonoBehaviour
{
    private int _vidaEstructuraActual;
    private int _vidaEstructuraMax;


    public int VidaEstructuraActual
    {
        get { return _vidaEstructuraActual; }
        set
        {
            _vidaEstructuraActual = value;
            if (_vidaEstructuraActual<=0)
                DestruirTorreYReproducirAnimacion();
        }
    }

    private void DestruirTorreYReproducirAnimacion()
    {
        Debug.Log("Destruyendo torre y reproduciendo Animacion");
    }

    private void Awake()
    {
        _vidaEstructuraActual = _vidaEstructuraMax;
    }

    public void RecibirDaño(int daño)
    {
        VidaEstructuraActual -= daño;
    }

}
