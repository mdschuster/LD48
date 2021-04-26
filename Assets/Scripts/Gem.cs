using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public GameObject score;

    public int value;
    // Start is called before the first frame update
    void Start()
    {

    }

    public int getValue()
    {
        return value;
    }

    public int collect()
    {
        return value;
    }

    public void death()
    {
        GameObject go = Instantiate(score, this.transform.position, Quaternion.identity);
        go.GetComponent<Score>().setText(getValue().ToString());
        Destroy(this.gameObject);
    }
}
