using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
public class camera : MonoBehaviour
{

    public Transform characterBody;
    public Transform  characterHead;

    float rotationX = 0;
    float rotationY = 0;
 

    void Start()
    {
      
    }

    private void LateUpdate()
    {
        transform.position = characterHead.position;
    }

    // Update is called once per frame
    void Update()
    {
        float verticalDelta = Input.GetAxisRaw("Mouse Y");
        float horizontalDelta = Input.GetAxisRaw("Mouse X");

        rotationX += horizontalDelta;
        rotationY += verticalDelta;

        characterBody.localEulerAngles = new Vector3(0, rotationX, 0);

        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        
    }
}

