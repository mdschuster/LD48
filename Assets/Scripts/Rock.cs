using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private RaycastHit2D rayHit;

    private Vector3 rayOrigin;

    public LayerMask mask;

    public GameObject graphicComponent;
    public Vector3 slightlyDown;

    // Start is called before the first frame update
    void Start()
    {
        rayOrigin = graphicComponent.transform.position;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        rayOrigin = graphicComponent.transform.position;
        rayHit = Physics2D.Raycast(rayOrigin, Vector2.down, 0.75f, mask);
        Debug.DrawRay(rayOrigin, Vector3.down * 0.75f, Color.red, 0.1f);
        if (rayHit.collider != null)
        {
            GameManager.instance().playerHit();
        }
    }

    public Vector3Int getGridPostion()
    {
        return GameManager.instance().getTilePosition(graphicComponent.transform.position);
    }

    public bool pushRight()
    {

        Vector3Int pos = GameManager.instance().getTilePosition(this.transform.position);
        pos.x += 1;
        Vector3 origin = GameManager.instance().getTileTransform(pos);
        RaycastHit2D hit = Physics2D.Raycast(origin + slightlyDown, Vector3.right, 0.1f);
        Debug.DrawRay(origin, Vector3.right * 0.1f, Color.red, 1f);

        if (GameManager.instance().isOpenTile(pos) && hit.collider == null)
        {
            //move, need to check for other rocks or gems too
            this.transform.position = GameManager.instance().getTileTransform(pos);
            return true;
        }
        if (GameManager.instance().isOpenTile(pos) && hit.collider != null)
        {
            if (hit.collider.tag == "Gem")
            {
                GameObject g = hit.collider.gameObject;
                Destroy(g);
            }
            this.transform.position = GameManager.instance().getTileTransform(pos);
            return true;
        }
        return false;
    }
    public bool pushLeft()
    {

        Vector3Int pos = GameManager.instance().getTilePosition(this.transform.position);
        pos.x += -1;
        Vector3 origin = GameManager.instance().getTileTransform(pos);
        RaycastHit2D hit = Physics2D.Raycast(origin + slightlyDown, Vector3.left + slightlyDown, 0.1f);
        //Debug.DrawRay(origin, Vector3.left * 0.1f, Color.red, 1f);

        if (GameManager.instance().isOpenTile(pos) && hit.collider == null)
        {
            //move, need to check for other rocks or gems too
            this.transform.position = GameManager.instance().getTileTransform(pos);
            return true;
        }
        if (GameManager.instance().isOpenTile(pos) && hit.collider != null)
        {
            if (hit.collider.tag == "Gem")
            {
                GameObject g = hit.collider.gameObject;
                Destroy(g);
            }
            this.transform.position = GameManager.instance().getTileTransform(pos);
            return true;
        }
        return false;
    }




}
