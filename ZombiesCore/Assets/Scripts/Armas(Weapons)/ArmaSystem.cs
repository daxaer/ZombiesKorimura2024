using UnityEngine;

[System.Serializable]
public class ArmaSystem : MonoBehaviour
{
    [Tooltip("Son las armas que puede llevar el personaje")]
    [SerializeField] private int _maxWeapons = 2;
    [SerializeField] private int _currentWeapons = 0;
    [SerializeField] private int _currentWeapon = 0;
    [SerializeField] private Personaje Personaje;
    public InterfaceArma[] _weapons;
    private void Awake()
    {
        _weapons = new InterfaceArma[_maxWeapons];
    }

    public void CheckWeapon(int newWeapon, InterfaceArma interfaceArma)
    {
        Debug.Log("nueva arma" + newWeapon + "InterfaceArma" + interfaceArma);
        Debug.Log("maximas arma" + _maxWeapons);
        Debug.Log("actual arma" + _currentWeapons);
        if (_currentWeapons < _maxWeapons)
        {
            _weapons[_currentWeapons] = interfaceArma;
            _currentWeapon = _currentWeapons;
            _currentWeapons++;
        }
        else
        {
            _weapons[_currentWeapon] = interfaceArma;
        }
    }

    public void ChangeWeapon(int changeWeapon)
    {
        _currentWeapon += changeWeapon;

        if (_currentWeapon < 0)
        {
            _currentWeapon = _currentWeapons - 1;
        }
        else if (_currentWeapon >= _currentWeapons)
        {
            _currentWeapon = 0;
        }

        Personaje.SetArma(_weapons[_currentWeapon]);
    }
    public void AsignarArma(int idArma, FactoriaArmas factoriaArmas)
    {
        if (factoriaArmas != null && Personaje != null)
        {
            var arma = factoriaArmas.CrearYAsignarAPersonaje(idArma, Personaje);
            arma.ConfigurarFactoriaArmas(factoriaArmas);
            Personaje.SetArma(arma);
        }
    }
}
