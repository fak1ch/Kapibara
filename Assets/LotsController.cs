using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotsController : MonoBehaviour
{
    [SerializeField] private ShopLot[] _lots;
    [SerializeField] private CustomizeLots _customizeLots;

    private List<int> _productIds = new List<int>();

    public void RefreshLots()
    {
        for (int i = 0; i < _lots.Length; i++)
        {
            _lots[i].RefreshLot();
            if (_lots[i].PurchaseFlag == true)
            {
                _productIds.Add(_lots[i].ProductId);
                _customizeLots.AddLot(_lots[i].ProductId);
            }
        }
    }

    public void ProductPurchased(int id)
    {
        _productIds.Add(id);
        _customizeLots.AddLot(id);
    }
}

