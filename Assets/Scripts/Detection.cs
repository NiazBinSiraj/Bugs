using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Detection : MonoBehaviour
{
    private Spwan_Bugs spwan_Bugs;
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            if (spwan_Bugs == null) spwan_Bugs = GameObject.FindGameObjectWithTag("GameController").GetComponent<Spwan_Bugs>();
            if (gameController == null) gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject thiis = col.gameObject;
        if (col.tag == "vBug" || col.tag == "vBug2" || col.tag == "vBug3")
        {
            if(col.gameObject.GetComponent<VBugController>().isClicked == true) return;
            if (col.gameObject.GetComponent<VBugController>().isLimited == true)
            {
                Destroy(col.gameObject);
                return;
            }

            thiis.transform.position = new Vector2(1000f, 1000f);
            gameController.DecreaseLife();
            spwan_Bugs.InsertIntoVBugPool(ref thiis);
        }
        else if (col.tag == "bug" || col.tag == "bug2" || col.tag == "bug3") {
            thiis.transform.position = new Vector2(1000f, 1000f);
            spwan_Bugs.InsertIntoBugPool(ref thiis);
        }
        else if (col.tag == "LBug")
        {
            if(col.gameObject.GetComponent<VBugController>().isClicked == true) return;
            Destroy(col.gameObject);
        }
    }
}
