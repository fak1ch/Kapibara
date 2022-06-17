using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
public class GooglePlayPurchasing : MonoBehaviour
{
    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == "com.fak1ch.removeads")
        {
            GoogleServices.Instance.SaveDataRemoveAdsBool(true);
        }
    }

    public void PurchaseComplete()
    {
        GoogleServices.Instance.SaveDataRemoveAdsBool(true);
    }
}
