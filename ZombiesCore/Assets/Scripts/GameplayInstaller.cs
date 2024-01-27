using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class GameplayInstaller : MonoBehaviour
{
    [Tooltip("En este apartado ira el padre de los spawns del mapa")]
    [SerializeField] private SpawnerZombies _spawnerZombies;

    [SerializeField] private PersonajesConfiguracion _personajesConfiguracion;
    [SerializeField] private ArmasConfiguracion _armasConfiguracion;
    [SerializeField] private EnemyConfiguration _configuracionEnemigo;
    [SerializeField] private CinemachineVirtualCamera camera3eraPersona;
    [SerializeField] private CinemachineVirtualCamera cameraPrimeraPersona;
    [SerializeField] private SeguirMouse MouseApuntarGameObject;
    //[SerializeField] private ControladorCamara _controladorCamara;
    [SerializeField] private InteractuableVendedor _vendedor;
    private FactoriaMainGameplay _abstractFactory;
    private bool slowMotion;


    
    private void OnEnable()
    {
        CambiadorCamaras.TerceraPersonaCamara = camera3eraPersona;
        CambiadorCamaras.PrimeraPersonaCamara = cameraPrimeraPersona;
        CambiadorCamaras.Register(camera3eraPersona);
        CambiadorCamaras.Register(cameraPrimeraPersona);
        CambiadorCamaras.CambiarCamara(camera3eraPersona);
    }
    private void OnDisable()
    {
        CambiadorCamaras.Unregister(camera3eraPersona);
        CambiadorCamaras.Unregister(cameraPrimeraPersona);
    }
    private void Start()
    {
        var factoriaPersonajes = new FactoriaPersonajes(Instantiate(_personajesConfiguracion));
        var factoriaArmas = new FactoriaArmas(Instantiate(_armasConfiguracion));
        var factoriaEnemigo = new FactoriaEnemigo(Instantiate(_configuracionEnemigo));   

        _abstractFactory = new FactoriaMainGameplay(factoriaPersonajes, factoriaArmas, factoriaEnemigo);
        var consumer = _abstractFactory.CrearPersonaje("Humano"); //TODO: CAMBIAR LA LOGICA DE CUANDO SE INICIE LA PARTIDA INSTANCIAR LOS RESPECTIVOS PERSONAJES
        var pistola = _abstractFactory.CrearYAsignarAPersonaje(1000,consumer);
        pistola.ConfigurarFactoriaArmas(factoriaArmas); //EL ASIGNAR LA FACTORIA ESTA BIEN LO QUE SE TIENE QUE MEJORAR ES QUE FUNCIONE DINAMICAMENTE ESE ID QUE SE LE ESTA PASANDO
        _spawnerZombies.ConfigurarFactoriaEnemigos(factoriaEnemigo);
        MouseApuntarGameObject.player = consumer.transform;
        //_controladorCamara.player = consumer.transform;
        camera3eraPersona.Follow = MouseApuntarGameObject.transform;
        cameraPrimeraPersona.Follow = consumer.transform;
        consumer.ConfigurarFactoriaGameplay(_abstractFactory);
        consumer.SetArma(pistola);
        _vendedor.ConfigurarFactoriaArma(factoriaArmas);
        //var consumer2 = _abstractFactory.CrearPersonaje("Gato"); //TODO: CAMBIAR LA LOGICA DE CUANDO SE INICIE LA PARTIDA INSTANCIAR LOS RESPECTIVOS PERSONAJES
    }

    public void EfectoSlowMotion()
    {
        slowMotion = !slowMotion;

        Time.timeScale = slowMotion ? 0.33f : 1.0f;
    }
}
