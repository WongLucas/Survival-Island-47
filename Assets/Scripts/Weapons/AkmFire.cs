using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkmFire : MonoBehaviour
{
    public GameObject theGun;
    public AudioSource gunShot;
    public bool isFiring = false;
    public GameObject muzzleFlash;

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
        muzzleFlash.SetActive(true);
        gunShot.Play();
        yield return new WaitForSeconds(0.05f);
        muzzleFlash.SetActive(false);
        yield return new WaitForSeconds(0.45f);
        theGun.GetComponent<Animator>().Play("New State");
        isFiring = false;
    }
}
