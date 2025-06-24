
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GlobalAmmo : MonoBehaviour
{
    public static int ammoCount;
    public GameObject ammoText;
    void Update()
    {
        ammoText.GetComponent<Text>().text = "AMMO: " + ammoCount;
    }
}
