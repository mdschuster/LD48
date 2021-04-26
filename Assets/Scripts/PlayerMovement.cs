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



    // Start is called before the first frame update
    void Start()
    {
        playerData = GetComponent<PlayerData>();

    }

    // Update is called once per frame
    void Update()
    {
        rayOrigin = this.transform.position;
        rayHitDown = Physics2D.Raycast(rayOrigin, Vector2.down, 0.75f, mask);
        rayHitUp = Physics2D.Raycast(rayOrigin, Vector2.up, 0.75f, mask);
        rayHitLeft = Physics2D.Raycast(rayOrigin, Vector2.left, 0.75f, mask);
        rayHitRight = Physics2D.Raycast(rayOrigin, Vector2.right, 0.75f, mask);
        Debug.DrawRay(rayOrigin, Vector3.down * 0.75f, Color.red, 0.01f);
        Debug.DrawRay(rayOrigin, Vector3.up * 0.75f, Color.red, 0.01f);
        Debug.DrawRay(rayOrigin, Vector3.left * 0.75f, Color.red, 0.01f);
        Debug.DrawRay(rayOrigin, Vector3.right * 0.75f, Color.red, 0.01f);

        //get input
        if (Input.GetKeyUp(KeyCode.UpArrow) && rayHitUp.collider == null)
        {
            moveUp = true;
            moveDown = false;
            moveRight = false;
            moveLeft = false;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
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
            hasTile = GameManager.instance().getDirtTilemap().HasTile(pos);

            playerData.setGridPosition(pos);
            this.transform.position = GameManager.instance().getDirtTilemap().GetCellCenterWorld(pos);

            moveUp = false;
        }
        if (moveDown)
        {
            pos = playerData.getGridPosition() + down;
            hasTile = GameManager.instance().getDirtTilemap().HasTile(pos);

            playerData.setGridPosition(pos);
            this.transform.position = GameManager.instance().getDirtTilemap().GetCellCenterWorld(pos);

            moveDown = false;
        }
        if (moveRight)
        {
            pos = playerData.getGridPosition() + right;
            hasTile = GameManager.instance().getDirtTilemap().HasTile(pos);

            playerData.setGridPosition(pos);
            this.transform.position = GameManager.instance().getDirtTilemap().GetCellCenterWorld(pos);

            moveRight = false;
        }
        if (moveLeft)
        {
            pos = playerData.getGridPosition() + left;
            hasTile = GameManager.instance().getDirtTilemap().HasTile(pos);


            playerData.setGridPosition(pos);
            this.transform.position = GameManager.instance().getDirtTilemap().GetCellCenterWorld(pos);

            moveLeft = false;
        }
        if (hasTile)
        {
            TileBase tile = GameManager.instance().getDirtTilemap().GetTile(pos);
            if (tile == GameManager.instance().getGreenRuleTile())
            {
                int value = GameManager.instance().greenTileMoney;
                GameObject go = Instantiate(score, this.transform.position, Quaternion.identity);
                go.GetComponent<Score>().setText(value.ToString());
                playerData.addMoney(value);
            }
            moveSound(pos);

            GameManager.instance().getDirtTilemap().SetTile(pos, null);
        }

    }

    public void moveSound(Vector3 pos)
    {
        Instantiate(playerData.digSound, pos, Quaternion.identity);
    }
}
