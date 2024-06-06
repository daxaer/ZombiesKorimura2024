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
    }

    public void UpdateMoney(int money)
    {
        moneyText.text = money + " $" ;
    }

    public void OpenGameOver()
    {
        gameOver.SetActive(true);
        StartCoroutine("ReturnMenu");
    }

    IEnumerator ReturnMenu()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}
