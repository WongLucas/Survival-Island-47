using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollection : MonoBehaviour
{
    public GameObject ammoCrate;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GlobalAmmo.ammoCount += 10;
            ammoCrate.SetActive(false);
        }
    }
}
