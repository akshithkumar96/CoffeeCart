using UnityEngine;

public class OrderController : IOrder
{
    public float Price { get; private set; }

    public void OrderItem(OrderData item)
    {
        Price = item.Price;
    }
}

public struct OrderData
{
    public FoodType FoodType;   
    public int Price; 
}
