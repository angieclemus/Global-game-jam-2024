using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class intro : MonoBehaviour
{
    public TMP_Text texto;
    public GameObject panel;
    private bool isActive = false;
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isActive)
        {
            panel.SetActive(true);
            isActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isActive)
        {
            button.SetActive(true);
            texto.text = "La urgencia se apodera de mí mientras navego por pasillos claustrofóbicos, sintiendo el aliento del monstruo en mi nuca. Cada risa despierta es un respiro temporal, pero el tiempo avanza inexorablemente. Mi única misión: reunir los objetos antes de que el monstruo me alcance o el reloj marque mi destino. La lucha contra la oscuridad se intensifica, y el laberinto del terror se convierte en un desafío desesperado por escapar de las garras de esta pesadilla";
        }
    }
    public void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
}
