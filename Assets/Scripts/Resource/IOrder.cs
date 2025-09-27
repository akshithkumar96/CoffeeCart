using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOrder
{
    float Price { get; }
    void OrderItem(OrderData item);
}
