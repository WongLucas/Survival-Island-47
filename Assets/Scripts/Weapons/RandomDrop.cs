using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDrop : MonoBehaviour
{
    public int xPos;
    public int zPos;
    public GameObject ammoCrate;
    public bool genCrate = false;
    void Update()
    {
        if (genCrate == false)
        {
            genCrate = true;
            StartCoroutine(RandomCrateFall());
        }
    }

    IEnumerator RandomCrateFall()
    {
        yield return new WaitForSeconds(20);
        xPos = Random.Range(445, 425);
        zPos = Random.Range(600, 560);
        Instantiate(ammoCrate, new Vector3(xPos, 70, zPos), Quaternion.identity);
        genCrate = false;
    }
}
