﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Activator : MonoBehaviour {

    public PlayerStats playerStats;
    public ControladorRescompensa sc;
    public BarraVidaEnemigo vidaEnemigo;

    public PuntosDeJuego PuntosDeJuegoScript;
    public BarraVida BarraVidaScript;

    public GameObject hitPlayer;
    public GameObject hitEnemy;

    public AudioSource song;

    public Text InfoText;
    public KeyCode key;
    bool active = false;
    GameObject Note;

    private void Start()
    {
        sc = GameObject.Find("ControlDeEscenaRecompensa").GetComponent<ControladorRescompensa>();
    }


    // Update is called once per frame
    void Update () {
	    if(Input.GetKeyDown(key) && active)
        {

            if (Mathf.Abs(Note.transform.position.y) - 3.54 <= 0.5)
            {
                if (Mathf.Abs(Note.transform.position.y) - 3.54 <= 0.25)
                {
                    if (Mathf.Abs(Note.transform.position.y) - 3.54 <= 0.1)
                    {
                        PuntosDeJuegoScript.PuntosTotales += sPerfect();
                        InfoText.text = "Super Perfect!";
                    }
                    else
                    {
                        PuntosDeJuegoScript.PuntosTotales += perfect();
                        InfoText.text = "Perfect!";
                    }
                }
                else
                {
                    PuntosDeJuegoScript.PuntosTotales += good();
                    InfoText.text = "Good!";
                }
                GameObject clone2 = Instantiate<GameObject>(hitPlayer, this.gameObject.transform.position, new Quaternion());

                Destroy(clone2, 2);
            }
            else
            {
                BarraVidaScript.Damage();
                PuntosDeJuegoScript.MultiplicadorDeCombo = 1;
            }
            Destroy(Note);
            
            if (PuntosDeJuegoScript.MultiplicadorDeCombo % 5 == 00) //cada combo de 5...
            {
                BarraVidaScript.Curar();
            }
            PuntosDeJuegoScript.MultiplicadorDeCombo++;
            playerStats.Atack();
            vidaEnemigo.Damage();

        }
        else if  (Input.GetKeyDown(key) && active==false)
            {

            PuntosDeJuegoScript.MultiplicadorDeCombo = 1;
            BarraVidaScript.Damage();

            AnimaciónHitEnemy();
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (song != null) {
            if (!song.isPlaying)
            {
                sc.HasStarted = true;
                song.Play();

            }
        }

        active = true;
        if (col.gameObject.tag == "Note")
        {

            Note = col.gameObject;
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        active = false;
    }

    int sPerfect()
    {
        return  2 * (PuntosDeJuegoScript.MultiplicadorDeCombo * PuntosDeJuegoScript.PuntosPorNota);
    }

    int perfect()
    {
        return (int)(1.5 * (PuntosDeJuegoScript.MultiplicadorDeCombo * PuntosDeJuegoScript.PuntosPorNota));
    }

    int good()
    {
        return PuntosDeJuegoScript.MultiplicadorDeCombo * PuntosDeJuegoScript.PuntosPorNota;
    }
    public void AnimaciónHitEnemy()
    {
        GameObject clone2 = Instantiate<GameObject>(hitEnemy, new Vector2(3.72f + Random.Range(-2, 1), 0), new Quaternion());

        Destroy(clone2, 0.25f);
    }

}
