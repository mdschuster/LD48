using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSimulation
{

    private CellData[,] grid { get; }
    private int height;
    private int width;

    private int updateRadius;


    public GameSimulation(int height, int width, int updateRadius)
    {
        this.height = height;
        this.width = width;
        this.updateRadius = updateRadius;
        grid = new CellData[width, height];
        generate();
    }


    public void generate()
    {
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                grid[i, j] = new CellData(i, j);
            }
        }
    }


    public void Update(int playerY)
    {
        for (int j = playerY - updateRadius; j <= playerY + updateRadius; j++)
        {
            if (j < 0 || j > width - 1) continue;
            for (int i = 1; i < width - 1; i++)
            {
                applyRule(grid[i, j]);
            }
        }
    }


    private void applyRule(CellData cell)
    {
        int x = cell.xPos;
        int y = cell.yPos;
        CellData leftCell = getNeighbor(x - 1, y);
        CellData upCell = getNeighbor(x, y - 1);
        CellData rightCell = getNeighbor(x + 1, y);

        CellData downCell = getNeighbor(x, y + 1);
        CellData downRightCell = getNeighbor(x + 1, y + 1);
        CellData downLeftCell = getNeighbor(x - 1, y + 1);

        if (cell.state == CellData.CellState.ROCK && downCell != null && downLeftCell != null && downRightCell != null)
        {

            //prefer straight down
            if (downCell.state == CellData.CellState.AIR)
            {
                downCell.state = CellData.CellState.ROCK;
                cell.state = CellData.CellState.AIR;
            }
            else if (downLeftCell.state == CellData.CellState.AIR && downRightCell.state != CellData.CellState.AIR)
            {
                downLeftCell.state = CellData.CellState.ROCK;
                cell.state = CellData.CellState.AIR;
            }
            else if (downRightCell.state == CellData.CellState.AIR && downLeftCell.state != CellData.CellState.AIR)
            {
                downRightCell.state = CellData.CellState.ROCK;
                cell.state = CellData.CellState.AIR;
            }
            //if both left and right are air, but down is not
            else if (downRightCell.state == CellData.CellState.AIR && downLeftCell.state == CellData.CellState.AIR)
            {
                int rand = Random.Range(0, 2);
                switch (rand)
                {
                    case (0):
                        downRightCell.state = CellData.CellState.ROCK;
                        cell.state = CellData.CellState.AIR;
                        break;
                    case (1):
                        downLeftCell.state = CellData.CellState.ROCK;
                        cell.state = CellData.CellState.AIR;
                        break;
                    default:
                        Debug.LogError("Something went wrong with moving downleft or downright");
                        break;

                }
            }

        }

    }

    private CellData getNeighbor(int x, int y)
    {
        if (x < 0 || y < 0) return null;
        return grid[x, y];
    }
}
