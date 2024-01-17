using UnityEngine;
//using Cinemachine;
//using DG.Tweening;

public class GameplayInstaller : MonoBehaviour
{
    [Tooltip("En este apartado ira el padre de los spawns del mapa")]
    [SerializeField] private SpawnerZombies _spawnerZombies;

    [SerializeField] private PersonajesConfiguracion _personajesConfiguracion;
    [SerializeField] private ArmasConfiguracion _armasConfiguracion;
    [SerializeField] private EnemyConfiguration _configuracionEnemigo;
    //[SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private InteractuableVendedor _vendedor;
    private FactoriaMainGameplay _abstractFactory;
    private bool slowMotion;
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
        //_virtualCamera.Follow = consumer.transform;
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
