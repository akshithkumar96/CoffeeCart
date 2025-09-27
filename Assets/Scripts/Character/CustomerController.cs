using UnityEngine;

public class CustomerController :MovableController, ICustomer
{
    public int CustomerID { get; set; }

    private IOrder Order { get; set; }

    public void Initialize(IOrder order)
    {
        SetRandomID();
        Order = order;
    }
    
    /// <summary>
    /// Setting random Id 
    /// we can update as required...!!!
    /// </summary>
    private void SetRandomID()
    {
        CustomerID = Random.Range(1000, 9999);
    }
    
    public override void Move(Vector3 destinationPosition, float duration)
    {
        base.Move(destinationPosition, duration);
    }
}
