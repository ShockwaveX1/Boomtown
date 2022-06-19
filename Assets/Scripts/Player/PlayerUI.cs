using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;

    [SerializeField] private TextMeshProUGUI WeaponNameText;
    [SerializeField] private TextMeshProUGUI CurrentAmmoText;
    [SerializeField] private TextMeshProUGUI MaxAmmoText;

    public void UpdateText(string promptMessage)
    {
        promptText.text = promptMessage;
    }

    public void UpdateWeaponText(string name, int current, int max)
    {
        WeaponNameText.text = name;
        CurrentAmmoText.text = current.ToString();
        MaxAmmoText.text = max.ToString();
    }

    public void UpdateAmmo(int current)
    {
        CurrentAmmoText.text = current.ToString();
    }

    public void UpdateAmmo(int current, int max)
    {
        CurrentAmmoText.text = current.ToString();
        MaxAmmoText.text = max.ToString();
    }
}
