﻿using UnityEngine;
using System.Collections;


public class Activator : MonoBehaviour {

    public PlayerStats playerStats;

    public BarraVidaEnemigo vidaEnemigo;

    public PuntosDeJuego PuntosDeJuegoScript;
    public BarraVida BarraVidaScript;

    public KeyCode key;
    bool active = false;
    GameObject Note;


	
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
                    }
                    else
                        PuntosDeJuegoScript.PuntosTotales += perfect();
                }
                else
                    PuntosDeJuegoScript.PuntosTotales += good();
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
            }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
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

}
