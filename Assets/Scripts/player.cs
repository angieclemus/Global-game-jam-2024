using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    //1. Declaración de variables
    [Range(1, 10)] public float velocidad; //Velocidad del jugador
    Rigidbody2D rb2d;
    bool isPressed = true;

    void Start () {
        //2. Capturo y asocio los componentes Rigidbody2D y Sprite Renderer del Jugador
        rb2d = GetComponent<Rigidbody2D>();
    }
   
    void FixedUpdate () {

    //3. Movimiento horizontal
    float movimientoH = Input.GetAxisRaw("Horizontal");
    rb2d.velocity = new Vector2(movimientoH * velocidad, rb2d.velocity.y);

    //4. Movimiento vertical
    float movimientoV = Input.GetAxisRaw("Vertical");
    rb2d.velocity = new Vector2(rb2d.velocity.x, movimientoV * velocidad);

    //5. Accionar caja (space)
   if(Input.GetKeyUp(KeyCode.Space) ){
    activarCaja();
   }
   
   }

   void activarCaja(){

   }
}
