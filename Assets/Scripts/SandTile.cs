using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandTile
{
    public int sandCount = 0;
    public int overflowThreshold = 4;

    public void AddSand(int count)
    {
        sandCount += count;

        if (sandCount >= overflowThreshold)
        {
            Overflow();
        }
    }

    private void Overflow()
    {
        // Code to distribute sand to neighbouring tiles
        sandCount -= overflowThreshold;
    }
}
