using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    private Vector3Int gridPosition;

    public GameObject deathEffect;
    public GameObject deathLight;

    public GameObject digSound;
    public GameObject collectSound;
    public GameObject dieSound;


    private int money;

    // Start is called before the first frame update
    void Start()
    {
        money = 0;
        GameManager.instance().updateHaul();
        this.transform.position = new Vector3(-0.5f, 10.5f, 0f);
        gridPosition = GameManager.instance().getTilePosition(new Vector3(-0.5f, 10.5f, 0f));
        this.transform.position = GameManager.instance().getTileTransform(gridPosition);
    }


    public void playerHit()
    {

        Vector3Int temp = GameManager.instance().getTilePosition(this.transform.position);
        Vector3 pos = GameManager.instance().getTileTransform(temp);
        Instantiate(dieSound, pos, Quaternion.identity);
        Instantiate(deathEffect, pos, Quaternion.identity);
        Instantiate(deathLight, pos, Quaternion.identity);
        this.gameObject.SetActive(false);
    }

    public Vector3Int getGridPosition()
    {
        return gridPosition;
    }

    public void setGridPosition(Vector3Int pos)
    {
        gridPosition = pos;
    }

    public void addMoney(int amt)
    {
        Vector3Int temp = GameManager.instance().getTilePosition(this.transform.position);
        Vector3 pos = GameManager.instance().getTileTransform(temp);
        Instantiate(collectSound, pos, Quaternion.identity);
        money += amt;
        GameManager.instance().updateHaul();
    }

    public int getMoney()
    {
        return money;
    }
}
