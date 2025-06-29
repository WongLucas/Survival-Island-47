using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollection : MonoBehaviour
{
    public GameObject ammoCrate;
    public AudioSource pickUp;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pickUp.Play();
            GlobalAmmo.ammoCount += 10;
            ammoCrate.SetActive(false);
        }
    }
}
