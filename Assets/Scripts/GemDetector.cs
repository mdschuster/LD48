using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemDetector : MonoBehaviour
{

    private PlayerData playerData;
    private Vector3 rayOrigin;
    private RaycastHit2D rayHit;
    public LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        playerData = GetComponent<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        rayOrigin = this.transform.position;
        rayHit = Physics2D.Raycast(rayOrigin, Vector2.down, 0.1f, mask);
        if (rayHit.collider != null)
        {
            Gem g = rayHit.collider.gameObject.GetComponent<Gem>();
            int gemValue = g.collect();
            playerData.addMoney(gemValue);
            g.death();
        }
    }
}
