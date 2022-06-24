using System;
using UnityEngine;
using Random = UnityEngine.Random;

public static class StaticClass
{
    public static Material _groundMaterial;
    public static Material _wallMaterial;

    public static Sprite[] _sprites;

    public static Sprite GetRandomSprite()
    {
        if (_sprites == null || _sprites.Length == 0)
            return null;

        int value = Random.Range(-1, _sprites.Length);

        if (value == -1)
            return null;

        return _sprites[Random.Range(0, _sprites.Length)];
    }
}
