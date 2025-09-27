using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Order Database", menuName = "Coffee Cart/Order Database")]
public class OrderDatabase : ScriptableObject
{
    [Header("Order Database")]
    public List<OrderDataScriptableObject> orderDataList = new List<OrderDataScriptableObject>();
    
    public OrderDataScriptableObject GetOrderData(FoodType foodType)
    {
        foreach (var orderData in orderDataList)
        {
            if (orderData.foodType == foodType)
            {
                return orderData;
            }
        }
        return null;
    }
    
    public OrderDataScriptableObject GetRandomOrder()
    {
        if (orderDataList.Count == 0) return null;
        
        int randomIndex = Random.Range(0, orderDataList.Count);
        return orderDataList[randomIndex];
    }
    
    public List<OrderDataScriptableObject> GetOrdersByPriceRange(float minPrice, float maxPrice)
    {
        List<OrderDataScriptableObject> filteredOrders = new List<OrderDataScriptableObject>();
        
        foreach (var orderData in orderDataList)
        {
            if (orderData.price >= minPrice && orderData.price <= maxPrice)
            {
                filteredOrders.Add(orderData);
            }
        }
        
        return filteredOrders;
    }
}

