using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private PlayerData playerData;

    private bool moveLeft = false;
    private bool moveRight = false;
    private bool moveUp = false;
    private bool moveDown = false;
    private Vector3Int up = new Vector3Int(0, 1, 0);
    private Vector3Int down = new Vector3Int(0, -1, 0);
    private Vector3Int right = new Vector3Int(1, 0, 0);
    private Vector3Int left = new Vector3Int(-1, 0, 0);

    private Vector3 rayOrigin;
    private RaycastHit2D rayHitDown;
    private RaycastHit2D rayHitUp;
    private RaycastHit2D rayHitLeft;
    private RaycastHit2D rayHitRight;

    public LayerMask mask;
    public GameObject score;

    public Tilemap dirtTileMap;



    // Start is called before the first frame update
    void Start()
    {
        playerData = GetComponent<PlayerData>();
        dirtTileMap = GameManager.instance().getDirtTilemap();

    }

    // Update is called once per frame
    void Update()
    {
        rayOrigin = this.transform.position;




        Debug.DrawRay(rayOrigin, Vector3.down * 0.75f, Color.red, 0.01f);
        Debug.DrawRay(rayOrigin, Vector3.up * 0.75f, Color.red, 0.01f);
        Debug.DrawRay(rayOrigin, Vector3.left * 0.75f, Color.red, 0.01f);
        Debug.DrawRay(rayOrigin, Vector3.right * 0.75f, Color.red, 0.01f);

        //get input
        if (Input.GetKeyUp(KeyCode.UpArrow) && rayHitUp.collider == null)
        {
            rayHitUp = Physics2D.Raycast(rayOrigin, Vector2.up, 0.75f, mask);
            moveUp = true;
            moveDown = false;
            moveRight = false;
            moveLeft = false;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            rayHitDown = Physics2D.Raycast(rayOrigin, Vector2.down, 0.75f, mask);
            if (rayHitDown.collider != null && rayHitDown.collider.tag == "Door")
            {
                GameManager.instance().win();
            }
            else if (rayHitDown.collider == null)
            {
                moveUp = false;
                moveDown = true;
                moveRight = false;
                moveLeft = false;
            }

        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            rayHitLeft = Physics2D.Raycast(rayOrigin, Vector2.left, 0.75f, mask);
            if (rayHitLeft.collider != null)
            {
                if (rayHitLeft.collider.GetComponent<Rock>() != null)
                {
                    Rock r = rayHitLeft.collider.GetComponent<Rock>();
                    bool pushed = r.pushLeft();
                    if (pushed)
                    {
                        moveLeft = true;
                        return;
                    }
                }
                return;
            }
            moveUp = false;
            moveDown = false;
            moveRight = false;
            moveLeft = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            rayHitRight = Physics2D.Raycast(rayOrigin, Vector2.right, 0.75f, mask);
            if (rayHitRight.collider != null)
            {
                if (rayHitRight.collider.GetComponent<Rock>() != null)
                {
                    Rock r = rayHitRight.collider.GetComponent<Rock>();
                    bool pushed = r.pushRight();
                    if (pushed)
                    {
                        moveRight = true;
                        return;
                    }
                }
                if (rayHitRight.collider.tag == "Door")
                {
                    GameManager.instance().win();
                }
                return;
            }

            moveUp = false;
            moveDown = false;
            moveRight = true;
            moveLeft = false;
        }


    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        bool hasTile = false;
        Vector3Int pos = playerData.getGridPosition();
        if (moveUp)
        {
            pos = playerData.getGridPosition() + up;
            hasTile = dirtTileMap.HasTile(pos);

            playerData.setGridPosition(pos);
            this.transform.position = dirtTileMap.GetCellCenterWorld(pos);

            moveUp = false;
        }
        if (moveDown)
        {
            pos = playerData.getGridPosition() + down;
            hasTile = dirtTileMap.HasTile(pos);

            playerData.setGridPosition(pos);
            this.transform.position = dirtTileMap.GetCellCenterWorld(pos);

            moveDown = false;
        }
        if (moveRight)
        {
            pos = playerData.getGridPosition() + right;
            hasTile = dirtTileMap.HasTile(pos);

            playerData.setGridPosition(pos);
            this.transform.position = dirtTileMap.GetCellCenterWorld(pos);

            moveRight = false;
        }
        if (moveLeft)
        {
            pos = playerData.getGridPosition() + left;
            hasTile = dirtTileMap.HasTile(pos);


            playerData.setGridPosition(pos);
            this.transform.position = dirtTileMap.GetCellCenterWorld(pos);

            moveLeft = false;
        }
        if (hasTile)
        {
            TileBase tile = dirtTileMap.GetTile(pos);
            if (tile == GameManager.instance().getGreenRuleTile())
            {
                int value = GameManager.instance().greenTileMoney;
                GameObject go = Instantiate(score, this.transform.position, Quaternion.identity);
                go.GetComponent<Score>().setText(value.ToString());
                playerData.addMoney(value);
            }
            moveSound(pos);

            dirtTileMap.SetTile(pos, null); //WARNING, this is quite slow when using a composite collider and what is causing the framerate problem
        }

    }

    public void moveSound(Vector3 pos)
    {
        Instantiate(playerData.digSound, pos, Quaternion.identity);
    }
}
