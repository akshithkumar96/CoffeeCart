using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private IReward rewardSystem;
    [SerializeField] private TMP_Text coinText;
    private ResourceManager _resourceManager;

    private void Start()
    {
        rewardSystem = GetComponent<IReward>();
        _resourceManager = ResourceManager.Instance;
        _resourceManager.OnResourceUpdated += OnResourceUpdated;
        
        UpdateCoinDisplay(_resourceManager.GetCoins());
    }
    
    private void OnDestroy()
    {
        if (_resourceManager != null)
        {
            _resourceManager.OnResourceUpdated -= OnResourceUpdated;
        }
    }
    
    private void OnResourceUpdated(int currentCoins, int totalCoins)
    {
        rewardSystem.Reward(currentCoins, () =>
        {
            coinText.text = totalCoins.ToString();

        });   
    }
    
    private void UpdateCoinDisplay(int coins)
    {
        coinText.text = coins.ToString();
    }
    

}


