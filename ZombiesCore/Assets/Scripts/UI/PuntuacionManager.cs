using UnityEngine;
using System;
using TMPro;

public class PuntuacionManager : Singleton<PuntuacionManager>
{
    [SerializeField] private GameObject panelPuntuacion;
    private int usuarioID;

    //Datos totales
    private int partidasJugadas;
    private int rondaMaximaAlcanzada;
    private int rondaMediaAlcanzada;
    private TimeSpan tiempoTotalJugado;
    private TimeSpan tiempoMedioJugado;

    //partida Actual
    private DateTime inicioPartida;
    private DateTime finalPartida;
    private TimeSpan tiempoJugado;

    //puntuacion
    private int _enemigosDerrotados;
    

    [SerializeField] private GuardarPartida guardar;
    [SerializeField] private DatosTotalesManager datosTotales;
    [SerializeField] private EnviarPuntuacion puntuacion;
    [SerializeField] private ObtenerPuntuacionesMaximas puntuacionMaxima;

    //PartidaActual
    [SerializeField] TextMeshProUGUI usuariotext;
    [SerializeField] TextMeshProUGUI rondamaximatext;
    [SerializeField] TextMeshProUGUI enemigosDerrotadostext;
    [SerializeField] TextMeshProUGUI tiempotext;

    public void Awake()
    {
        guardar = GetComponent<GuardarPartida>();
        datosTotales = GetComponent<DatosTotalesManager>();
    }
    public void SetEnemigosDerrotados( )
    {
        _enemigosDerrotados++;
    }

    public void SetUsuarioID(int id)
    {
        usuarioID = id;
    }

    public void SetPartidasJugadas(int numPartidas)
    {
        partidasJugadas = numPartidas;
    }

    public void SetRondaMaximaAlcanzada(int rondaMaxima)
    {
        rondaMaximaAlcanzada = rondaMaxima;
    }

    public void SetRondaMediaAlcanzada(int rondaMedia)
    {
        rondaMediaAlcanzada = rondaMedia;
    }

    public void SetTiempoTotalJugado(TimeSpan tiempoTotal)
    {
        tiempoTotalJugado = tiempoTotal;
    }

    public void SetTiempoMedioJugado(TimeSpan tiempoMedio)
    {
        tiempoMedioJugado = tiempoMedio;
    }

    public void SetInicioPartida(DateTime inicio)
    {
        inicioPartida = inicio;
    }

    public void SetFinalPartida(DateTime final)
    {
        finalPartida = final;
        SetTiempoJugado();
    }

    public void SetTiempoJugado()
    {
        tiempoJugado = finalPartida - inicioPartida;
    }

    // Método para guardar la partida
    public void GuardarPartida()
    {
        guardar.FinalizarPartida(usuarioID, UIManager.Instance.CurrentRound + 1 , inicioPartida.ToString(), finalPartida.ToString(), tiempoJugado.ToString());
    }

    // Método para actualizar los datos totales
    public void ActualizarDatosTotales()
    {
        datosTotales.EnviarDatosTotales(usuarioID, 1, UIManager.Instance.CurrentRound + 1, UIManager.Instance.CurrentRound + 1, UIManager.Instance.CurrentRound + 1, tiempoJugado, tiempoJugado);
    }

    public void SetPartidaActual()
    {
        usuariotext.text = PlayerPrefs.GetString("NombreUsuario");
        rondamaximatext.text = UIManager.Instance.CurrentRound.ToString();
        enemigosDerrotadostext.text = _enemigosDerrotados.ToString();
        tiempotext.text = string.Format("{0:D2}:{1:D2}:{2:D2}", tiempoJugado.Hours, tiempoJugado.Minutes, tiempoJugado.Seconds);
        puntuacion.GuardarPuntuacion(PlayerPrefs.GetInt("IDUsuario"), _enemigosDerrotados, tiempoJugado.ToString(), UIManager.Instance.CurrentRound);
        GuardarPartida();
        ActualizarDatosTotales();
    }
    public void SetTop3()
    {
        puntuacionMaxima.ObtenerPuntuaciones();
    }
}
