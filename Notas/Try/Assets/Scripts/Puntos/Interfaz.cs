﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interfaz : MonoBehaviour {

    public Text interfazDePuntos;
    public PuntosDeJuego PuntosDeJuegoScript;
    public Text interfazCombo;
	
	// Update is called once per frame
	void Update () {
        interfazDePuntos.text = PuntosDeJuegoScript.PuntosTotales.ToString();
        interfazCombo.text = PuntosDeJuegoScript.MultiplicadorDeCombo.ToString();
    }
}
