using System;
using System.Collections;
using UnityEngine;

public class CoinReward : MonoBehaviour, IReward
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform initialPos;
    [SerializeField] private Transform targetPos;
    [SerializeField] private int poolSize = 5;
    [SerializeField] private float duration;
    
    private ObjectPool coinPool;
    
    void Start()
    {
        coinPool = new ObjectPool(coinPrefab, poolSize);
    }

    public void Reward(int PriceData, Action OnComplete)
    {
        StartCoroutine(SpawnAndMoveCoins(PriceData, OnComplete));
    }
    
    private IEnumerator SpawnAndMoveCoins(int coinCount, Action onComplete)
    {
        int coinsReached = 0;
        
        for (int i = 0; i < coinCount; i++)
        {
            GameObject coin = coinPool.GetItem();
            if (coin != null)
            {
                coin.transform.SetParent(transform); //todo try to remove this
                coin.transform.position = initialPos.position;
                StartCoroutine(MoveCoinToTarget(coin, () => {
                    coinsReached++;
                    if (coinsReached >= coinCount)
                    {
                        onComplete?.Invoke();
                    }
                }));
            }
            
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    private IEnumerator MoveCoinToTarget(GameObject coin, Action onReach)
    {
        Vector3 targPos = targetPos.position;
        
        while (Vector3.Distance(coin.transform.position, targPos) > 0.01f)
        {
            coin.transform.position = Vector3.MoveTowards(coin.transform.position, targPos, duration);
            yield return null;
        }
        
        coin.transform.position = targPos;
        coinPool.ReleaseItem(coin);
        onReach?.Invoke();
    }
}
