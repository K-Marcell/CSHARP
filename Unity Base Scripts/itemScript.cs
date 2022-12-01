using System;
using UnityEngine;
namespace itemScript
{
    public class itemScript : MonoBehaviour
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
        item data = new item(0f, 0f, 0f);
    }
}
