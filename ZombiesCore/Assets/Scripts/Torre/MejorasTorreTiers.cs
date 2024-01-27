using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//EMPEZARA TIER 1 CON UNA SOLA BALLESTA LAS ESCALERAS Y EL PORTAL PARA SALIR LEJOS VIDA PARA AGUANTAR 100 GOLPES


//TIER 2: LA BALLESTA SE REEMPLAZA POR UNA AMETRALLADORA Y UN MORTERO CON 2 MUNICIONES 200 GOLPES

//TIER 3: AMETRALLADORA HACE DOBLE DE DAÑO Y UN MORTERO TIENE 10 MUNICIONES 250 GOLPES

//TIER 4: 350 GOLPES SE CONSERVA LA AMETRALLADORA, EL MORTERO SE MEJORA A UN RAYO SOLAR CON MUCHO DAÑO
//SE AGREGA UN LUGAR NUEVO PARA ACTIVAR UN CAMPO DE ENERGIA QUE PROTEGE LA ESTRUCTURA Y MIENTRAS ESTA ACTIVA CONSUME DINERO DEL JUGADOR
public class MejorasTorreTiers : MonoBehaviour
{
    [SerializeField] private float tier1Precio;
    [SerializeField] private float tier2Precio;
    [SerializeField] private float tier3Precio;
    [SerializeField] private float tier4Precio;

    private int _tier = 1;
    private float _costoPrecioPorMejora;

    private void ActualizarTier()
    {
        //LOGICA DEL SWITCH Y QUÉ HARA CADA TIER
        switch (_tier)
        {
            case 1:
                _tier = 2;
                break;
            case 2:
                _tier = 3;
                break;
            case 3:
                _tier = 4;
                break;
            default: _tier = 1; 
                break;

        }
    }

    public void MejorarTier(int dinero)
    {
        if(TieneDineroParaMejorar(dinero))
        {
            Debug.Log("Se Compro mejora");
            ActualizarTier();
        }
    }
    private bool TieneDineroParaMejorar(int dinero)
    {
        switch (_tier)
        {
            case 1:
                _costoPrecioPorMejora = tier1Precio; break;
            case 2:
                _costoPrecioPorMejora = tier2Precio; break;
            case 3:
                _costoPrecioPorMejora = tier3Precio; break;
            case 4:
                _costoPrecioPorMejora = tier4Precio; break;
            default:
                break;
        }

        return dinero >= _costoPrecioPorMejora;
    }


}
