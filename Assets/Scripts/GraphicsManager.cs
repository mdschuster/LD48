using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsManager : MonoBehaviour
{

    public int squareSize;
    private int xCells;
    private int yCells;

    public GameObject AirPrefab;
    public GameObject RockPrefab;
    public GameObject DirtPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // xCells = GameManager.instance().XCells;
        // yCells = GameManager.instance().YCells;
    }

    private void setupInitialGrid()
    {
        for (int j = 0; j < yCells; j++)
        {
            for (int i = 0; i < xCells; i++)
            {

            }
        }
    }

    // Update is called once per frame
    public void UpdateGraphics()
    {
        for (int j = 0; j < yCells; j++)
        {
            for (int i = 0; i < xCells; i++)
            {

            }
        }
    }
}
