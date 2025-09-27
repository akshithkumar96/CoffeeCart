using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReward
{
    void Reward(int PriceData, Action OnCOmplete);
}
