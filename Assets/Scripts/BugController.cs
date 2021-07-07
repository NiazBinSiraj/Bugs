using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugController : MonoBehaviour
{
    private Animator bugAnim;
    private Move moveScript;

    private bool isClicked = false;
    private Spwan_Bugs spwan_Bugs;
    private GameController gameController;

    void Start()
    {
        if(spwan_Bugs == null) spwan_Bugs = GameObject.FindGameObjectWithTag("GameController").GetComponent<Spwan_Bugs>();
        if(gameController == null) gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        isClicked = false;
        
        if(bugAnim == null) bugAnim = GetComponent<Animator>();
        if(moveScript == null) moveScript = GetComponent<Move>();

        bugAnim.SetBool("isDestroyed", false);
        moveScript.enabled = true;
        moveScript.speed = GameData.bugSpeed;
        moveScript.Start();
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void Initialize()
    {
        Start();
    }

    void Update()
    {
        if(moveScript != null) moveScript.speed = GameData.bugSpeed;
    }

    void OnMouseDown()
    {
        if(isClicked) return;
        isClicked = true;

        AudioManager.instance.PlaySquashSound();
        
        gameController.DecreaseLife();
        moveScript.enabled = false;
        
        bugAnim.SetBool("isDestroyed", true);
        Invoke("Destroy", 1.1f);
    }

    void Destroy()
    {
        transform.position = new Vector2(1000f, 1000f);
        GameObject thiis = this.gameObject;
        spwan_Bugs.InsertIntoBugPool(ref thiis);
    }
}
