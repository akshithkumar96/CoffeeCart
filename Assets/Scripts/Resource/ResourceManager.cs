public delegate void ResourceUpdateDelegate(int currentCoins, int totalCoins);

public class ResourceManager 
{
    private static ResourceManager _instance;
    public static ResourceManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ResourceManager();
            }
            return _instance;
        }
    }
    public UserResource userResource;
    
    public event ResourceUpdateDelegate OnResourceUpdated;

    public int GetCoins()
    {
        return userResource.Coins;
    }   
    public int GetCoffeeBeans()
    {
        return userResource.CoffeeBeans;
    }

    private ResourceManager()
    {
        // load from persistent storage
        // since this is a demo, we will initialize with default values
        userResource = new UserResource
        {
            Coins = 0,
            CoffeeBeans = int.MaxValue // Infinite coffee beans
        };
    }

    public void AddCoins(int amount)
    {
        userResource.Coins += amount;
        OnResourceUpdated?.Invoke(amount, userResource.Coins);
    }
    

    //for upgrade 
    //I have not written logic due to time constraint
    public bool SpendCoins(int amount)
    {
        if (userResource.Coins >= amount)
        {
            userResource.Coins -= amount;
            return true;
        }
        return false;
    }
    public void AddCoffeeBeans(int amount)
    {
        // Coffee beans are infinite, no need to add
    }

}

public struct UserResource
{
    public int Coins;
    public int CoffeeBeans;
}
