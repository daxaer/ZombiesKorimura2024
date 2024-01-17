using UnityEngine;
using TMPro;


public class ArmasEspecialesTrigger : MonoBehaviour
{
    [HideInInspector] public int IdArma;
    private int _nivelRequerido;
    private GameObject _mostrador;
    private void OnEnable()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        _nivelRequerido = GetComponentInChildren<DetallesArma>().NivelRequerido;
        var parent = transform.parent;
        parent.Find("Canvas/Descripcion").GetComponent<TextMeshProUGUI>().text = GetComponentInChildren<DetallesArma>().Descripcion;
       
    }
  
    private void OnTriggerEnter(Collider other)
    {
        
        var personajeStats = other.GetComponent<Personaje>()?._statsPersonaje;
        if (!RequerimientoArmaEspecial(other, personajeStats)) return;
        personajeStats.ArmaEspecialObtenida = true;
        transform.parent.Find("Canvas/Descripcion").GetComponent<TextMeshProUGUI>().text = "";
        var posArma = other.GetComponent<Personaje>().PosicionArma;
        transform.SetParent(posArma, false);
        var boxCollider = GetComponent<BoxCollider>();
        Destroy(boxCollider);
        Destroy(this);
    }

    private bool RequerimientoArmaEspecial(Collider other, StatsPersonaje personajeStats)
    {
        return other.CompareTag("Player") && !personajeStats.ArmaEspecialObtenida && personajeStats.Nivel >= _nivelRequerido;
    }
}
