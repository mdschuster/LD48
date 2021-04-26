using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance = null;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public static GameManager instance()
    {
        return _instance;
    }






    private GameSimulation simulation;

    public Tilemap dirt;
    public Tilemap rocks;
    public Tilemap barrier;

    public TileBase dirtRuleTile;
    public TileBase greenRuleTile;
    public int greenTileMoney;
    public GameObject gameOverCanvas;
    public GameObject winCanvas;
    public Text finalScoreText;
    public Text finalScoreWinText;

    //canvas alpha
    public float fadeDuration = 3f;
    public TMP_Text haulText;



    // [SerializeField] private int xCells;
    // [SerializeField] private int yCells;
    // [SerializeField] private int updateRadius;

    [SerializeField] private PlayerData player;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playerHit()
    {
        gameOver();
        player.playerHit();
    }


    public Vector3Int getTilePosition(Vector3 pos)
    {
        return dirt.WorldToCell(pos);
    }

    public Vector3 getTileTransform(Vector3Int pos)
    {
        return dirt.GetCellCenterWorld(pos);
    }

    public Tilemap getDirtTilemap()
    {
        return dirt;
    }
    public Tilemap getRockTilemap()
    {
        return rocks;
    }
    public TileBase getDirtRuleTile()
    {
        return dirtRuleTile;
    }
    public TileBase getGreenRuleTile()
    {
        return greenRuleTile;
    }
    public bool hasBarrier(Vector3Int pos)
    {
        if (barrier.HasTile(pos))
        {
            return true;
        }
        return false;
    }

    public bool isOpenTile(Vector3Int pos)
    {
        bool barrierTile = barrier.HasTile(pos);
        bool dirtTile = dirt.HasTile(pos);
        if (dirtTile || barrierTile)
        {
            return false;
        }
        return true;
    }

    private void gameOver()
    {
        StartCoroutine(fadeInCanvas(gameOverCanvas.GetComponent<CanvasGroup>()));
        finalScoreText.text = "Total Haul:\n$" + player.getMoney().ToString();
    }


    private IEnumerator fadeInCanvas(CanvasGroup cg)
    {
        for (float t = 0f; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            //right here, you can now use normalizedTime as the third parameter in any Lerp from start to end
            cg.alpha = Mathf.Lerp(0f, 1f, normalizedTime);
            yield return null;
        }
        cg.alpha = 1; //without this, the value will end at something like 0.9992367
    }


    public void onRestartClick()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void onMenuClick()
    {
        SceneManager.LoadScene("Menu");
    }

    public void updateHaul()
    {
        haulText.text = "Haul: $" + player.getMoney().ToString();
    }

    public void win()
    {
        player.gameObject.SetActive(false);
        StartCoroutine(fadeInCanvas(winCanvas.GetComponent<CanvasGroup>()));
        finalScoreWinText.text = "Total Haul:\n$" + player.getMoney().ToString();
    }

}
