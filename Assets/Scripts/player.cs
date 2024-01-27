using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class player : MonoBehaviour
{
    //1. Declaración de variables
    [Range(1, 1000)] public float velocidad; //Velocidad del jugador
    public int cargas;
    public TMP_Text vistaCargas;
    Rigidbody2D rb2d;
    bool isPressed;

    public float tiempoTranscurrido = 0f;
    public float enemyTime = 60f; 

    public float tiempoTranscurrido = 0f;
    public float enemyTime = 60f; 
    public RectTransform personaje;
    private float posicion =0;
    public GameObject puerta;
    public TMP_Text subtitulos;
    private int cont = 0;

    void Start () {
        //2. Capturo y asocio los componentes Rigidbody2D y Sprite Renderer del Jugador
        rb2d = GetComponent<Rigidbody2D>();
        vistaCargas.text = cargas.ToString();
    }
   
    void Update () {

        //3. Movimiento horizontal
        float movimientoH = Input.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2(movimientoH * velocidad, rb2d.velocity.y);

        if(movimientoH>0){
           posicion = -90;
        }else if (movimientoH<0){
            posicion = 90;
        }
        
        //4. Movimiento vertical
        float movimientoV = Input.GetAxisRaw("Vertical");
        rb2d.velocity = new Vector2(rb2d.velocity.x, movimientoV * velocidad);
         
        if(movimientoV>0){
            posicion = 0;
        }else if (movimientoV<0){
            posicion = 180;
        }
        personaje.rotation = Quaternion.Euler(0,0,posicion);
        //5. Accionar caja (space)
        if(Input.GetKeyDown(KeyCode.Space) && !isPressed){
            
            activarCaja();
            isPressed = true;
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            isPressed = false;
        }
        VerificarTiempo();
    }
    
    void VerificarTiempo()
    {
        tiempoTranscurrido += Time.deltaTime;
        if (tiempoTranscurrido >= enemyTime)
        {
            GameOver();
        }
    }
    void GameOver(){
        Debug.Log("Game Over");
    }

    void activarCaja(){
        //Activas una animacion
        if(cargas==0){
            return;
        }
        tiempoTranscurrido = 0;
        cargas --;
        vistaCargas.text = cargas.ToString();

       
    }
        enemyTime = 60;
    }


    void recogerObj (){
        cargas ++;
        vistaCargas.text = cargas.ToString();
   }

    }
    void limpiarSubtitulos(){
         subtitulos.text = "";
    }
    void abrirPuerta (){
        subtitulos.text = "Has encontrado todos los objetos. La puerta ha sido abierta.";
        Invoke("limpiarSubtitulos",15f);
        Destroy(puerta);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "objetos")
        {
            recogerObj();
            cont ++;
            if(cont >= 1){
                abrirPuerta();
            }
            Destroy(col.gameObject);
        }
    }

    }
}
