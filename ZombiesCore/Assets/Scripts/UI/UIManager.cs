using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager> 
{
    [SerializeField] private TextMeshProUGUI totalAmmoText;
    [SerializeField] private TextMeshProUGUI chargerAmmoText;
    [SerializeField] private TextMeshProUGUI currentRoundText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private Personaje _player;
    private int _currentRound;
    public void Start()
    {
        UpdateCurrentRound(1);
    }
    public void UpdateTotalAmmo(int maxAmmo, int currentAmmo)
    {
        totalAmmoText.text = currentAmmo + "/" + maxAmmo;
    }

    public void UpdateChargerAmmo(int maxCharger, int currentCharger)
    {
        chargerAmmoText.text = currentCharger + "/" + maxCharger;
    }

    public void UpdateCurrentRound(int currentRound)
    {
        currentRoundText.text = "Round: " + currentRound;
        _currentRound = currentRound;
    }
    public int CurrentRound { get => _currentRound; set => _currentRound = value; }

    public void UpdateMoney(int money)
    {
        moneyText.text = money + " $" ;
    }

    public void OpenGameOver()
    {
        gameOver.SetActive(true);
    }

    public void SetPlayer(Personaje personaje)
    {
        _player = personaje;
    }
    public void UpdatePlayerMoney(int money)
    {
        _player.AddMoney(money);
    }

    
}
