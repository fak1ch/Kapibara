using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyShopSettings : MonoBehaviour
{
    [SerializeField] private LotsController _lotsController1;
    [SerializeField] private LotsController _lotsController2;

    [SerializeField] private CustomizeLots _customizeLots1;
    [SerializeField] private CustomizeLots _customizeLots2;

    private void Start()
    {
        _lotsController1.RefreshLots();
        _lotsController2.RefreshLots();
    }

    public void ApplySettings()
    {
        _customizeLots1.ApplyMassives();
        _customizeLots2.ApplyMassives();
    }
}
