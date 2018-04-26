﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public BaseDeDatos BaseDeDatosScript;
    public GameObject slotPrefab;
    [SerializeField]
    public List<SlotInfo> slotInfoList;
    public int capacity;
    private string saveDataInventario;

    private void Start()
    {
        slotInfoList = new List<SlotInfo>();
        if(PlayerPrefs.HasKey("inventario"))
        {
            CargarInventario();
        }
        else
        {
            CrearInventarioVacío();
        }
    }


    private void CrearInventarioVacío()
    {
        for (int i = 0; i < capacity; i++)
        {
            GameObject slot = Instantiate <GameObject>(slotPrefab, this.transform);
            Slot newSlot = slotPrefab.GetComponent<Slot>();
            newSlot.CreateSlot(i);
            SlotInfo newSlotInfo = newSlot.slotInfo;
            slotInfoList.Add(newSlotInfo);
        }
    }

    private void CargarInventario()
    {
        saveDataInventario = PlayerPrefs.GetString("inventario");
        InventarioGuardado guardarInventario = JsonUtility.FromJson<InventarioGuardado>(saveDataInventario);
        this.slotInfoList = guardarInventario.slotInfoList;
    }

    private SlotInfo EncontrarItemEnInventario(int _identificadorItem)
    {
        foreach(SlotInfo slotInfo in slotInfoList)
        {
            if (slotInfo.identificadorItem == _identificadorItem && !slotInfo.isEmpty)
                return slotInfo;
        }
        return null;
    }

    private SlotInfo SlotAccesible(int _identificadorItem)
    {
        foreach (SlotInfo slotinfo in slotInfoList) //encntrar un slot con el donde se guarden el mismo equipo
        {
            if (slotinfo.identificadorItem == _identificadorItem && slotinfo.cantidad < slotinfo.cantidadMax && !slotinfo.isEmpty)
                return slotinfo;
        }
        foreach (SlotInfo slotinfo in slotInfoList) //si ese slot no existe o esta ocupado con su maxima capacidad encuentra otro vacío
        {
            if (slotinfo.isEmpty)
                return slotinfo;
        }
        return null; //ningun lugar
    }

    public void AñadirItem(int _identificadorItem)
    {
        Item item = BaseDeDatosScript.FindItem(_identificadorItem);//mirar en la base de datos y encontralo
        if (item != null){
            SlotInfo slotInfo = SlotAccesible(_identificadorItem);//encontrar el item y guardarlo
            if (slotInfo != null)
            {
                slotInfo.cantidad++;
                slotInfo.identificadorItem = _identificadorItem;
                slotInfo.isEmpty = false;
            }
        }
    }

    public void EliminarItem(int _identificadorItem)
    {
        SlotInfo slotInfo = EncontrarItemEnInventario(_identificadorItem);
        if (slotInfo != null)
        {
            if (slotInfo.cantidad == 1)
                slotInfo.SetEmptySlot();
            else
                slotInfo.cantidad--;
        }
    }


    private class InventarioGuardado
    {
        public List<SlotInfo> slotInfoList;
    }

    public void GuardarInventario()
    {
        InventarioGuardado guardarInventario = new InventarioGuardado();
        guardarInventario.slotInfoList = this.slotInfoList;
        saveDataInventario = JsonUtility.ToJson(guardarInventario);
        PlayerPrefs.SetString("inventario", saveDataInventario);
    }

    [ContextMenu("Instrucción_1")]
    public void Instrucción_1()
    {
        AñadirItem(1);
    }
    [ContextMenu("Instrucción_2")]
    public void Instrucción_2()
    {
        EliminarItem(1);
    }
    [ContextMenu("Instrucción_3")]
    public void AlmacenarInventario()
    {
        GuardarInventario();
    }

}
