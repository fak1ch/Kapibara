using System;

public class PlaygroundData
{
    private int _coinsCount;
    private int _difficult;

    public int CoinsCount => _coinsCount;
    public int Difficult => _difficult;

    public PlaygroundData(int coinsCount, int difficult)
    {
        _coinsCount = coinsCount;
        _difficult = difficult;
    }
}
