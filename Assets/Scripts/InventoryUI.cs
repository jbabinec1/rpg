using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public bool[] isFull;

   // public GameObject[] slots;

    public GameObject slotImage;

    public List<GameObject> slots;

    public GameObject go;

    public GameObject spawnHereItem;

    // public GameObject another;

   // public GameObject gameItem;


    public InventoryUI inventory;

   public GameObject canvas;

    public GameObject findUIslot;

    public GameObject itemimage;


    void Start()

    {
       //The old way of adding slots to the canvas .. couldn't find a way to instantiate at the slot index


        slots = new List<GameObject>();
        slots.Add(slotImage);
        slots.Add(slotImage);
        Debug.Log("slots " + slots.Count);

       GameObject canvas = GameObject.Find("Canvas");
        GameObject holder = GameObject.Find("spawn-here-item");

        findUIslot = GameObject.Find("InventoryUIslot");
    
        //Spawns however many slot gameobjects there are according to the slots count ..
  /*     for(int i = 0; i < slots.Count; i++)
        {

            // GameObject go = Instantiate(slotImage, slots[i].transform, false) as GameObject;

            //spawnHereItem.transform.position

             go = Instantiate(slotImage, canvas.transform, false) as GameObject;

            go.transform.SetParent(canvas.transform, false);

            slots[i] = go;

           // slots[i].transform.position = go.transform.position;


        }  */


     


    }




    public void bam(GameObject gameItem)
    {

        GameObject ahh = Instantiate(gameItem, canvas.transform, false) as GameObject;

        ahh.transform.SetParent(canvas.transform, false);

        //Instantiate(gameItem, findUIslot.transform);
        
    }












}
