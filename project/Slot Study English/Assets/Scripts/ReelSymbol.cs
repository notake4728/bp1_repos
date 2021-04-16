using UnityEngine;
public class ReelSymbol : MonoBehaviour
{
    public int symbolType;
    int symbolIdx = 0;

    public void SetIndexOnReel(int _idx)
    {
        symbolIdx = _idx;
    }

    public int GetIndexOnReel()
    {
        return symbolIdx;
    }
}
