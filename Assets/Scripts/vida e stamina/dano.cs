  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dano : MonoBehaviour
{
    public int Dano =10; 
    private  void OnTriggerEnter(Collider other)
    {
        other.GetComponent<vida>().receberDano(  Dano)   ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
