using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDrop : MonoBehaviour
{
    public int xPos;
    public int zPos;

    void Start()
    {
        xPos = Random.Range(445, 425);
        zPos = Random.Range(600, 560);
        transform.position = new Vector3(xPos, 70, zPos);
    }
}
