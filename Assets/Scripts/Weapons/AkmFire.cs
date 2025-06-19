using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkmFire : MonoBehaviour
{
    public GameObject theGun;
    public AudioSource gunShot;
    public bool isFiring = false;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (isFiring == false)
            {
                StartCoroutine(FireHandGun());
            }
        }
    }

    IEnumerator FireHandGun()
    {
        isFiring = true;
        theGun.GetComponent<Animator>().Play("AK_fire");
        gunShot.Play();
        yield return new WaitForSeconds(0.5f);
        theGun.GetComponent<Animator>().Play("New State");
        isFiring = false;
    }
}
