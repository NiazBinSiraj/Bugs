using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VBugController : MonoBehaviour
{
    private Animator vBugAnim;
    public GameObject vaccin;
    public GameObject life;
    private Move moveScript;
    public bool isClicked = false;
    private Spwan_Bugs spwan_Bugs;
    private GameController gameController;

    public bool isLimited = false;
    // Start is called before the first frame update
    void Start()
    {
        isClicked = false;
        
        if(vBugAnim == null) vBugAnim = this.gameObject.GetComponent<Animator>();
        if(moveScript == null) moveScript = GetComponent<Move>();
        
        vBugAnim.SetBool("isDestroyed", false);
        if(vaccin != null) vaccin.SetActive(true);
        if(life != null) life.SetActive(true);
        moveScript.enabled = true;
        moveScript.speed = GameData.vBugSpeed;
        moveScript.Start();
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void Load()
    {
        if (!isLimited)
        {
            if (spwan_Bugs == null) spwan_Bugs = GameObject.FindGameObjectWithTag("GameController").GetComponent<Spwan_Bugs>();
            if (gameController == null) gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }
    }

    public void Initialize()
    {
        Load();
        Start();
    }
    
    void Update()
    {
        if(moveScript != null) moveScript.speed = GameData.vBugSpeed;
    }

    void OnMouseDown()
    {
        if(isClicked) return;
        isClicked = true;

        AudioManager.instance.PlaySquashSound();
        
        if(vaccin != null) vaccin.SetActive(false);
        if(life != null) life.SetActive(false);
        moveScript.enabled = false;

        if (vaccin != null)
        {
            if (!isLimited) 
            {
                gameController.IncreaseScore(); 
            }
            else 
            {
                GameControllerLimited.instance.IncreaseScore();
            }
        }
        else if(life != null)
        {
            gameController.IncreaseLife();
        }

        vBugAnim.SetBool("isDestroyed", true);
        Invoke("Destroy", 1.1f);
    } 

    void Destroy()
    {
        if(isLimited)
        {
            Destroy(gameObject);
            return;
        }

        if(life != null)
        {
            Destroy(this.gameObject);
            return;
        }
        
        transform.position = new Vector2(1000f, 1000f);
        GameObject thiis = this.gameObject;
        spwan_Bugs.InsertIntoVBugPool(ref thiis);
    }

    public void DestroyByBomb()
    {
        OnMouseDown();
    }
}
