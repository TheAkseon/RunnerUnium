using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCoin : MonoBehaviour
{
    public void add()
    {
        CoinManager.Instance.AddMoney(1000);
    }
}
