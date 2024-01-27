using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class player : MonoBehaviour
{
    //1. Declaración de variables
    [Range(1, 100)] public float velocidad; //Velocidad del jugador
    public int cargas;
    public TMP_Text vistaCargas;
    Rigidbody2D rb2d;
    bool isPressed;

    void Start () {
        //2. Capturo y asocio los componentes Rigidbody2D y Sprite Renderer del Jugador
        rb2d = GetComponent<Rigidbody2D>();
        vistaCargas.text = cargas.ToString();
    }
   
    void Update () {

        //3. Movimiento horizontal
        float movimientoH = Input.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2(movimientoH * velocidad, rb2d.velocity.y);

        //4. Movimiento vertical
        float movimientoV = Input.GetAxisRaw("Vertical");
        rb2d.velocity = new Vector2(rb2d.velocity.x, movimientoV * velocidad);

        //5. Accionar caja (space)
        if(Input.GetKeyDown(KeyCode.Space) && !isPressed){
            Debug.Log("f");
            activarCaja();
            isPressed = true;
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            isPressed = false;
        }
   }

   void activarCaja(){
        //Activas una animacion
        if(cargas==0){
            return;
        }
        cargas --;
        vistaCargas.text = cargas.ToString();
   }

   void recogerObj (){
        cargas ++;
        vistaCargas.text = cargas.ToString();
   }
}
