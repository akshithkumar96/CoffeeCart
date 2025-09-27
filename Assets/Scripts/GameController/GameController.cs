using System.Collections.Generic;
using UnityEngine;
using System.Collections;   

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private Transform shopPos;
    [SerializeField] private Transform coffeMachinePos;
    [SerializeField] private Transform coffeBeanPos;

    [SerializeField] private Transform customerSpawnPosition;
    [SerializeField] private Transform customerCoffeShop;
    [SerializeField] private Transform customerExitPosition;

    [SerializeField] private GameObject Coffee;
    [SerializeField] private Transform coffeeInitalPos;
    [SerializeField] private Transform coffeeTargetPos;

    [SerializeField]private UIController uiController;

    [SerializeField]private OrderDataScriptableObject orderDataScriptableObject;

    private IMoveable player;
    private GameObject coffeeInstance;
    private IPool customerPool;
    private Queue<ICustomer> _customers;
    private ResourceManager _resourceManager;
    private OrderData _orderData;

    public void Start()
    {
        Initialize();
        CreatePlayer();
        CreateCoffee();
        //Spawn after 2 sec
        //if needed change the value...
        Invoke(nameof(SpawnCustomer), 2f);
    }

    private void CreatePlayer()
    {
        GameObject playerObject = Instantiate(playerPrefab, coffeMachinePos.position, Quaternion.identity);

        player = playerObject.GetComponent<IMoveable>();
    }

    private void Initialize()
    {
        customerPool = new ObjectPool(customerPrefab, 5);
        _resourceManager = ResourceManager.Instance;
        _customers = new Queue<ICustomer>();

        //This need to update based on requirement
        _orderData.Price = orderDataScriptableObject.price;
        _orderData.FoodType = orderDataScriptableObject.foodType;
    }
    
    private void CreateCoffee()
    {
        coffeeInstance = Instantiate(Coffee, coffeeInitalPos.position, Quaternion.identity);
        coffeeInstance.SetActive(false);
    }

    private void SpawnCustomer()
    {
        GameObject customerObject = customerPool.GetItem();
        customerObject.transform.position = customerSpawnPosition.position;
        var customerController = customerObject.GetComponent<CustomerController>();
        if (customerController != null)
        {
            float moveDuration = 3.0f;
            customerController.Move(customerCoffeShop.position, moveDuration);
            _customers.Enqueue(customerController);
            StartCoroutine(MovePlayerWhenCustomerReachesShop(moveDuration));
        }
        else
        {   Debug.LogError("CustomerController component not found on the customer prefab.");
            customerPool.ReleaseItem(customerObject);
        }
    }
    
    private IEnumerator MovePlayerWhenCustomerReachesShop(float customerMoveDuration)
    {
        yield return new WaitForSeconds(customerMoveDuration);
        StartCoroutine(PlayerServeCustomerSequence());
    }
    
    private IEnumerator PlayerServeCustomerSequence()
    {
        if (player == null) yield break;
        
        // Move to shop position
        yield return StartCoroutine(MovePlayerToShop());
        
        // Wait 1 second
        yield return new WaitForSeconds(1f);
        
        // Move to coffee bean position
        yield return StartCoroutine(MovePlayerToCoffeeBean());
        
        // Wait 1 second
        yield return new WaitForSeconds(1f);
        
        // Move to coffee machine position
        yield return StartCoroutine(MovePlayerToCoffeeMachine());
        
        // Wait 1 second
        yield return new WaitForSeconds(1f);
        
        // Show coffee at initial position
        coffeeInstance.transform.position = coffeeInitalPos.position;
        coffeeInstance.SetActive(true);

        // Wait 0.5 seconds
        // can show UI slider animation here for better UX
        yield return new WaitForSeconds(0.5f);
        
        // Disable coffee
        coffeeInstance.SetActive(false);
        
        // Move back to shop position
        yield return StartCoroutine(MovePlayerToShop());
        
        // Make coffee reappear and set to target position
        coffeeInstance.transform.position = coffeeTargetPos.position;
        coffeeInstance.SetActive(true);
        
        // Wait 0.5 seconds
        yield return new WaitForSeconds(0.5f);
        
        // Disable coffee
        coffeeInstance.SetActive(false);
        
        // Customer served and spawn new customer
        OnCustomerServed();
        SpawnCustomer();
    }
    
    private IEnumerator MovePlayerToShop()
    {
        if (player != null)
        {
            float moveDuration = 2.0f;
            player.Move(shopPos.position, moveDuration);
            yield return new WaitForSeconds(moveDuration);
        }
    }
    
    private IEnumerator MovePlayerToCoffeeBean()
    {
        if (player != null)
        {
            float moveDuration = 2.0f;
            player.Move(coffeBeanPos.position, moveDuration);
            yield return new WaitForSeconds(moveDuration);
        }
    }
    
    private IEnumerator MovePlayerToCoffeeMachine()
    {
        if (player != null)
        {
            float moveDuration = 2.0f;
            player.Move(coffeMachinePos.position, moveDuration);
            yield return new WaitForSeconds(moveDuration);
        }
    }

    public void OnCustomerServed()
    {
        var customer = _customers.Dequeue();
        var customerController = customer as CustomerController;
        if (customerController != null)
        {
            float moveDuration = 5.0f;
            customerController.Move(customerExitPosition.position, moveDuration);
            StartCoroutine(ReleaseCustomerAfterMovement(customerController.gameObject, moveDuration));
        }
        _resourceManager.AddCoins(_orderData.Price);
    }

    private IEnumerator ReleaseCustomerAfterMovement(GameObject customerObject, float moveDuration)
    {
        yield return new WaitForSeconds(moveDuration);
        customerPool.ReleaseItem(customerObject);
    }
}
