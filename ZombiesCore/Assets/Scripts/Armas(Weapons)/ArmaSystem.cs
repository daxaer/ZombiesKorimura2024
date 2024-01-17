using UnityEngine;


[System.Serializable]
public class ArmaSystem 
{

    [Tooltip("Son las armas que puede llevar el personaje")]
    [SerializeField] private int _armasTotales;
    public InterfaceArma[] _armas;

    //ESTE AUN NO SIRVE PARA NADA

}
