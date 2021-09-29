using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthPlayer : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image demageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    //PlayerShooting playerShooting;
    bool isDead;
    bool damaged;
    
     void Awake() 
    {
        //Mendapatkan referendce komponen
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource>();
        playerMovement = GetComponent <PlayerMovement> ();

        //playerShooting = GetComponent <PlayerShooting> ();
        currentHealth = startingHealth;
    }
    
     void Update() 
    {
      //Jika terkena damage
      if(damaged)
      {
          //Merubah warna gambar menjadi value dari flashColour
          demageImage.color = flashColour;
      }  
      else
      {
          //Fade out damage image
          demageImage.color = Color.Lerp (demageImage.color, Color.clear, flashSpeed * Time.deltaTime);
      }

      //Set damage to false
      damaged = false;
    }
    //fungsi untuk mendapatkan damage
    public void TakeDamage (int amount)
    {
        damaged = true;

        //mengurangi health
        currentHealth -= amount;

        //Merubah tampilan dari health slider
        healthSlider.value = currentHealth;

        //Memainkan suara ketika terkena damage
        playerAudio.Play ();

        //Memanggil method Death() jika darahnya kurang dari sama dengan 10 dan belu mati
        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }

    void Death ()
    {
        isDead = true;
        //playerShooting.DisableEffects ();
 
        //mentrigger animasi Die
        anim.SetTrigger ("Die");

        //Memainkan suara ketika mati
        playerAudio.clip = deathClip;
        playerAudio.Play ();

        //mematikan script player movement
        playerMovement.enabled = false;
 
        //playerShooting.enabled = false;
    }
    public void RestartLevel1 ()
    {
        //meload ulang scene dengan index 0 pada build setting
        SceneManager.LoadScene (0);
    }
}
