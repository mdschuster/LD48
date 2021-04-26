using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellData
{

    public enum CellState
    {
        AIR, DIRT, ROCK, DIAMOND
    }

    public int xPos { get; }
    public int yPos { get; }

    public CellState state { get; set; }


    public CellData(int x, int y)
    {
        xPos = x;
        yPos = y;
        state = CellState.AIR;
    }

}
