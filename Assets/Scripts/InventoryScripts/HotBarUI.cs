using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarUI : MonoBehaviour
{
    [Header("Slots da Hotbar")]
    public Image[] slotBackgrounds;
    public Color selectedColor = Color.white;
    public Color normalColor = new Color(1, 1, 1, 0.5f); // cinza claro com transparência

    private WeaponSwitcher weaponSwitcher;

    void Start()
    {
        weaponSwitcher = FindObjectOfType<WeaponSwitcher>();
        UpdateHotbarUI(weaponSwitcher != null ? weaponSwitcher.GetCurrentWeaponIndex() : 0);
    }

    void Update()
    {
        if (weaponSwitcher == null) return;

        int current = weaponSwitcher.GetCurrentWeaponIndex();
        UpdateHotbarUI(current);
    }

    void UpdateHotbarUI(int selectedIndex)
    {
        for (int i = 0; i < slotBackgrounds.Length; i++)
        {
            slotBackgrounds[i].color = (i == selectedIndex) ? selectedColor : normalColor;
        }
    }
}
//