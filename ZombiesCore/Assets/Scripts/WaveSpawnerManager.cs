using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public enum SpawnState
{
    SPAWNING,
    WAITING,
    COUNTING
};


//TODO: MOVER STATS AUMENTAR LA DIFICULTAD, ETC
public class WaveSpawnerManager : Singleton<WaveSpawnerManager>
{
    private SpawnerZombies _spawnerZombies;


    public SpawnState state = SpawnState.COUNTING;
    private SpawnState spawnState;
    [SerializeField] private int _enemigosVivos = 0;
    [SerializeField] private float _tiempoEntreOleada = 5f;
    [SerializeField] private float _countDown = 2f;
    [SerializeField] private float _tiempoSpawnPorEnemigo = 5.0f;
    [SerializeField] private bool _activarSpawnSystem = false;

    [SerializeField] private int _ronda = 0;

    [SerializeField] private int _rondaFinal = 255;
    [SerializeField] private int _jugadores = 1;
    [SerializeField] private float _multiplicadorDeVida = 1.1f;
    [SerializeField] private int[] _zombiesPorRondaUnJugador = { 6, 8, 13, 18, 24, 27, 28, 28, 29 };
    [SerializeField] private int[] _zombiesPorRondaDosJugador = { 7, 9, 15, 21, 27, 31, 32, 33, 34 };
    [SerializeField] private int[] _zombiesPorRondaTresJugador = { 9, 10, 18, 25, 32, 38, 40, 43, 45 };
    [SerializeField] private int[] _zombiesPorRondaCuatroJugador = { 10, 12, 21, 29, 37, 45, 49, 52, 56 };

    [SerializeField] private int _zombies;

    public int EnemigosVivos => _enemigosVivos;
    public int ZombiesMaxPorRonda;
    private bool estaEjecutandoseCorutinaRunSpawner = false;
    private void Start()
    {
        _spawnerZombies = gameObject.GetComponent<SpawnerZombies>();
    }
    public void IniciarOleada()
    {
        StartCoroutine(RunSpawner());

    }
    private void OnEnable()
    {
        Enemy.EnemyDeadEvent += HandleZombieDeadEvent;
    }
    private void OnDisable()
    {
        Enemy.EnemyDeadEvent -= HandleZombieDeadEvent;
    }

    private IEnumerator RunSpawner()
    {
        estaEjecutandoseCorutinaRunSpawner = true;
        // Primera vez espera el tiempo
        yield return new WaitUntil(DiaNocheManager.Instance.HoraIndicadaParaZombies);

        while (_activarSpawnSystem && _ronda < _rondaFinal)
        {
            yield return new WaitUntil(DiaNocheManager.Instance.HoraIndicadaParaZombies);
            state = SpawnState.SPAWNING;

            // hacer el spawn y al mismo tiempo esperar hasta que termine el tiempo


            DiaNocheManager.Instance.enOleada = true;
            yield return SpawnWave();

            state = SpawnState.WAITING;
            // esperar hasta que los enemigos hayan muerto
            yield return new WaitWhile(EnemyisAlive);

            state = SpawnState.COUNTING;
            DiaNocheManager.Instance.enOleada = false;

            //yield return new WaitForSeconds(_tiempoEntreOleada);
            _activarSpawnSystem = false;
            estaEjecutandoseCorutinaRunSpawner = false;
            yield return new WaitWhile(DiaNocheManager.Instance.HoraIndicadaParaZombies);
        }
    }
    public bool EstaActivadoSpawn => _activarSpawnSystem;
    public void ActivarSpawnSystem() { 
        _activarSpawnSystem = true;
    if (_activarSpawnSystem &&!estaEjecutandoseCorutinaRunSpawner) 
        {
            StartCoroutine(RunSpawner());
        }
    }
    //private bool AnimacionCompleta()
    //{

    //}
    public bool EnemyisAlive()
    {
        
        //DiaNocheManager.Instance.enOleada = false;

        return _enemigosVivos > 0; 
        //return DiaNocheManager.Instance.horaDelDia >= 6;
    }

    private IEnumerator SpawnWave()
    {

        _ronda++;
        //AGREGAR LOS ENEMIGOS VIVOS CUANTOS VAN A SER Y APLICAR LA FORMULA DE LOS ZOMBIES DE COD

        if (_jugadores == 1)
        {
            if (_ronda <= 9)
            {
                _zombies += _zombiesPorRondaUnJugador[_ronda];
                _enemigosVivos = _zombies;
                ZombiesMaxPorRonda = _enemigosVivos;
                //cambiar vida de zombies
            }
            else
            {
                _zombies += Mathf.FloorToInt((24 + _ronda / 5 * (0.15f * _ronda) * 3));

            }
            while (_zombies >= 1)
            {

                SpawnEnemy();
                yield return new WaitForSeconds(_tiempoSpawnPorEnemigo);
            }

        }
        else
        {
            if (_ronda <= 9)
            {
                if (_jugadores == 2)
                {
                    _zombies += _zombiesPorRondaDosJugador[_ronda];
                    _enemigosVivos = _zombies;

                }
                else if (_jugadores == 3)
                {
                    _zombies += _zombiesPorRondaTresJugador[_ronda];
                    _enemigosVivos = _zombies;
                }
                else
                {
                    _zombies += _zombiesPorRondaCuatroJugador[_ronda];
                    _enemigosVivos = _zombies;
                }
            }
            while (_zombies >= 1)
            {

                SpawnEnemy();
                yield return new WaitForSeconds(_tiempoSpawnPorEnemigo);
            }
        }


    }
    private void HandleZombieDeadEvent()
    {
        _enemigosVivos--;
    }

    private void SpawnEnemy()
    {

        _spawnerZombies.SpawnZombiesRandom();
        _zombies--;
    }
}
