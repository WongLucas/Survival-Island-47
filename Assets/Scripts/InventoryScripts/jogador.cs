using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class jogador : MonoBehaviour
{
    CharacterController Controller;
    Vector3 forward;
    Vector3 strafe;
    Vector3 vertical;

    float forwardSpeed = 5f;
    float strafeSpeed = 5f;

    float gravidade;
    float velocidadedopulo;
    float alturamaximadopulo = 2f;
    float tempodaalturamaxima = 0.5f;


    void Start()
    {
        Controller = GetComponent<CharacterController>();
        gravidade = (-2 * alturamaximadopulo) / (tempodaalturamaxima * tempodaalturamaxima);
        velocidadedopulo=(2* alturamaximadopulo / tempodaalturamaxima);
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxisRaw("Vertical");
        float strafeInput = Input.GetAxisRaw("Horizontal");

        forward = forwardInput * forwardSpeed * transform.forward;
        strafe = strafeInput * strafeSpeed * transform.right;

        vertical += gravidade * Time.deltaTime * Vector3.up;

        if (Controller.isGrounded)
        {
            vertical = Vector3.down;
        }

        if(Input.GetKeyDown(KeyCode.Space) &&Controller.isGrounded)
        {
            vertical = velocidadedopulo * Vector3.up;
        }
        

        Vector3 finalVelocity = forward + strafe + vertical;



        Controller.Move(finalVelocity * Time.deltaTime);


    }
}

