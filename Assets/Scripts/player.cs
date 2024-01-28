using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class player : MonoBehaviour
{
    //1. Declaración de variables
    [Range(1, 1000)] public float velocidad; //Velocidad del jugador
    public int cargas;
    bool isActive = true;
    public TMP_Text vistaCargas;
    Rigidbody2D rb2d;
    bool isPressed;
    public GameObject panelVictoria;
    public GameObject panelDerrota;
    public float tiempoTranscurrido = 0f;
    public float enemyTime = 60f;
    public RectTransform personaje;
    private float posicion =0;
    public GameObject puerta;
    public TMP_Text subtitulos;
    private int cont = 0;
    public Animator animacion;
    private bool canWalk = true;
    public List<AudioSource> sonidos;  
    private bool isPlaying = false;

    void Start () {
        subtitulos.text = "Debo encontrar los objetos que me ayudaran a abrir la puerta!";
        Invoke("limpiarSubtitulos",15f);
        //2. Capturo y asocio los componentes Rigidbody2D y Sprite Renderer del Jugador
        rb2d = GetComponent<Rigidbody2D>();
        vistaCargas.text = cargas.ToString();
    }
   
    void Update () {
        if (isActive)
        {
            //3-4 movimiento
            Movement();
            //5. Accionar caja (space)
            CallAlarm();
            VerificarTiempo();
        }
        
    }

    private void CallAlarm()
    {
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            activarAlarma();
        }

        // if (Input.GetKeyUp(KeyCode.Space))
        // {
        //     isPressed = false;
        // }
    }
    
    private IEnumerator resetWalk()
    {
        yield return new WaitForSeconds(1f);
        canWalk = true;
        animacion.SetBool("alarma",false);
        sonidos[0].Play();
    }

    private void Movement()
    {
        if(canWalk == false){
            rb2d.velocity = new Vector2(0,0);
            return;
        }
        //3. Movimiento horizontal
        float movimientoH = Input.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2(movimientoH * velocidad, rb2d.velocity.y);
        //4. Movimiento vertical
        float movimientoV = Input.GetAxisRaw("Vertical");
        rb2d.velocity = new Vector2(rb2d.velocity.x, movimientoV * velocidad);
        if (movimientoH > 0)
        {
            posicion = -90;
            // animacion.Play("caminar");
            animacion.SetBool("caminar", true);
        }
        else if (movimientoH < 0)
        {
            posicion = 90;
            // animacion.Play("caminar");
            animacion.SetBool("caminar", true);
        }
        else if (movimientoV > 0)
        {
            posicion = 0;
            // animacion.Play("caminar");
            animacion.SetBool("caminar", true);
        }
        else if (movimientoV < 0)
        {
            posicion = 180;
            // animacion.Play("caminar");
            animacion.SetBool("caminar", true);
        }
        else
        {
            // animacion.Play("idle");
            animacion.SetBool("caminar", false);
        }
        personaje.rotation = Quaternion.Euler(0,0,posicion);
    }

    void VerificarTiempo()
    {
        tiempoTranscurrido += Time.deltaTime;
        if (tiempoTranscurrido >= enemyTime)
        {
            GameOver();
            
        }
        else if (tiempoTranscurrido >= enemyTime * 0.75f)
        {
            subtitulos.text = "El tiempo se esta acabando! Deberia activar la alarma para ganar mas tiempo";
            CancelInvoke();
            Invoke("limpiarSubtitulos",10f);
        }else if (tiempoTranscurrido >= enemyTime * 0.5f && !isPlaying)
        {
            sonidos[4].Stop();
            sonidos[3].Play();
            isPlaying = true;
        }
    }
    void GameOver(){
        sonidos[4].Stop();
        sonidos[3].Stop();
        subtitulos.text = "El tiempo se ha acabado!";
        CancelInvoke();
        Invoke("limpiarSubtitulos",10f);
        panelDerrota.SetActive(true);
        isActive = false;   
    }

    void activarAlarma(){
        //Activas una animacion
        
        if(cargas==0){
            subtitulos.text = "No tengo cargas! Debo encontrar los objetos que me ayudaran a activar la alarma";
            CancelInvoke();
            Invoke("limpiarSubtitulos",10f);
            return;
        }
        isPlaying = false;
        subtitulos.text = "He activado la alarma de la risa! Tengo 60 segundos mas de tiempo";
        CancelInvoke();
        Invoke("limpiarSubtitulos",10f);
        tiempoTranscurrido = 0;
        cargas --;
        canWalk = false;
        StartCoroutine(resetWalk());
        animacion.SetBool("alarma",true);
        vistaCargas.text = cargas.ToString();
        sonidos[0].Play();
        sonidos[4].Play();
        sonidos[3].Stop();
    }
    void recogerObj (){
        cargas ++;
        vistaCargas.text = cargas.ToString(); 
    }
    public void jugarOtraVez(){
        Application.LoadLevel("SampleScene");
    }
    
    void limpiarSubtitulos(){
         subtitulos.text = "";
    }
    void abrirPuerta (){
        subtitulos.text = "Eh encontrado todos los objetos! La puerta ha sido abierta. Debo encontrar la salida";
        Invoke("limpiarSubtitulos",10f);
        Destroy(puerta);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "objetos")
        {
            recogerObj();
            cont ++;
            //switch case with cont
            if(cont == 1){
                subtitulos.text = "He encontrado mi primer objeto! Eso me da una alarma de la risa extra. Debo encontrar los otros objetos";
                CancelInvoke();
                Invoke("limpiarSubtitulos",10f);
            }else if(cont == 2){
                subtitulos.text = "He encontrado otro objeto! Debo encontrar los dos objetos restantes para abrir la puerta";
                CancelInvoke();
                Invoke("limpiarSubtitulos",10f);
            }else if (cont == 3){
                subtitulos.text = "He encontrado otro objeto! Debo encontrar el ultimo objeto para abrir la puerta";
                CancelInvoke();
                Invoke("limpiarSubtitulos",10f);
            } else if(cont >= 4){
                abrirPuerta();
            }
            Destroy(col.gameObject);
        }else if (col.tag == "Zonafinal")
        {
            sonidos[4].Stop();
            sonidos[3].Stop();
            panelVictoria.SetActive(true);
            isActive = false;
        }
    }
}
