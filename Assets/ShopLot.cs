using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopLot : MonoBehaviour
{
    [SerializeField] private GameObject _buyButton;
    [SerializeField] private GameObject _checkmarkPurchase;
    [SerializeField] private Text _priceText;
    [SerializeField] private CoinsWallet _wallet;

    [SerializeField] private int _productId;
    [SerializeField] private int _price;

    [SerializeField] private LotsController _LotsController;

    private string _key;
    private bool _productPurchased = false;

    public int ProductId => _productId;
    public bool PurchaseFlag => _productPurchased;

    public void BuyThisProduct()
    {
        if (_wallet.TakeBucks(_price) == true)
        {
            _productPurchased = true;
            PlayerPrefs.SetInt(_key, 1);
            _LotsController.ProductPurchased(_productId);
            RefreshLot();
        }
        else
        {

        }

    }

    public void RefreshLot()
    {
        _key = $"ProductNumber:{_productId}";
        //PlayerPrefs.SetInt(_key, 0);
        _priceText.text = _price.ToString();

        if (PlayerPrefs.HasKey(_key))
        {
            int value = PlayerPrefs.GetInt(_key);
            if (value == 1)
                _productPurchased = true;
            else
                _productPurchased = false;
        }
        else
        {
            PlayerPrefs.SetInt(_key, 0);
        }

        if (_productPurchased == true)
        {
            _buyButton.SetActive(false);
            _checkmarkPurchase.SetActive(true);
        }
        else
        {
            _buyButton.SetActive(true);
            _checkmarkPurchase.SetActive(false);
        }
    }
}
