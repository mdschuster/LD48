using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public TMP_Text text;
    public float aliveTime;
    public float minFontSize;
    public float maxFontSize;
    public float speed;

    private float time;
    private Vector3 pos;
    private float currentFontSize;

    // Start is called before the first frame update
    void Start()
    {
        text.fontSize = minFontSize;
        pos = this.transform.position;
        time = 0;
        currentFontSize = text.fontSize;
    }

    // Update is called once per frame
    void Update()
    {

        pos += speed * Vector3.up * Time.deltaTime;
        this.transform.position = pos;

        currentFontSize = Mathf.Lerp(currentFontSize, maxFontSize, 3 * Time.deltaTime);
        text.fontSize = currentFontSize;


        if (time >= aliveTime)
        {
            Destroy(this.gameObject);
        }
        time += Time.deltaTime;
    }

    public void setText(string s)
    {
        text.SetText("$" + s);
    }
}
