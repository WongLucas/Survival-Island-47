using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abririnventario : MonoBehaviour
{
    
    public GameObject arbriinventario;
    bool ativar = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ativar = !ativar;
             arbriinventario.SetActive(ativar);
        }   
        if (ativar)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        
    }
}


