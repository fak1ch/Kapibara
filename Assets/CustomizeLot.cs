using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeLot : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] public Sprite _sprite;
    [SerializeField] public Material _groundMaterial;
    [SerializeField] public Material _wallMaterial;
    [SerializeField] private Toggle _toggle;

    public int Id => _id;
    public Toggle Toggle => _toggle;
}
