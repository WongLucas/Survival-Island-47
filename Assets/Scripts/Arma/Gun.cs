using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Tiro")]
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 1f;

    [Header("Efeitos")]
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    [Header("Referências")]
    public Camera fpsCam;

    [Header("Munição")]
    public int maxAmmo = 30;
    private int currentAmmo;
    public float reloadTime = 1.5f;
    private bool isReloading = false;

    [Header("Áudio")]
    public AudioSource gunAudio;
    public AudioClip shootClip;
    public AudioClip reloadClip;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void Update()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (currentAmmo <= 0)
            return;

        currentAmmo--;

        if (muzzleFlash != null)
            muzzleFlash.Play();

        if (gunAudio && shootClip)
            gunAudio.PlayOneShot(shootClip);

        Ray ray = fpsCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.Log("Acertou: " + hit.transform.name);

            Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (impactEffect != null)
            {
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 1f);
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Recarregando...");

        if (gunAudio && reloadClip)
            gunAudio.PlayOneShot(reloadClip);

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
    }
}
