using System.Net;
using System.ComponentModel.Design;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    struct item
    {
        public float i_Price;
        public float i_Weight;
        public float i_CarrySize;

        public item(float price, float weight, float carrySize)
        {
            i_Price = price;
            i_Weight = weight;
            i_CarrySize = carrySize;
        }
    }

    [SerializeField]
    List<item> inventory = new List<item>();

    [SerializeField]
    float carryLimit,
        currentSize;

    [SerializeField]
    GameObject camera;

    void Start()
    {
        carryLimit = 10f;
        currentSize = 0f;
        camera = Camera.main.gameObject;
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.transform,Vector3.forward(camera.transform),1f,LayerMask.getMask("item"),out hit))
            if (!(hit.gameObject.getComponent<itemScript>().item.i_carrySize + currentSize>carryLimit))
                inventory.Add(hit.gameObject.getComponent<itemScript>().item);
    }
}
