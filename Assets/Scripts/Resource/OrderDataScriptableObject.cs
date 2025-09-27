using UnityEngine;

[CreateAssetMenu(fileName = "New Order Data", menuName = "Coffee Cart/Order Data")]
public class OrderDataScriptableObject : ScriptableObject
{
    public FoodType foodType;
    public int price;
    public string orderName;
    public string description;
    
    public Sprite orderIcon;
    public Color orderColor = Color.white;
    
    public float preparationTime = 2f;
    public int experiencePoints = 10;
}

