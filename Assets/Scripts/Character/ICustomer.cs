using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICustomer 
{
    int CustomerID { get; set; }

    void Initialize(IOrder order);
}
