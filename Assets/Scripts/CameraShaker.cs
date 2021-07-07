using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    #region singleton
    public static CameraShaker instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion


    public float shakingDuration;
    private float currentTime;
    private float shakingStartedTime;
    private bool shake;

    void Start()
    {
        shake = false;
    }

    void Update()
    {
        currentTime = Time.time;
        if(shake == true)
        {
            if(currentTime-shakingStartedTime <= shakingDuration)
            {
                gameObject.transform.position = new Vector3(0.0f, -1.0f*gameObject.transform.position.y,-10.0f);
            }

            else
            {
                shake = false;
                gameObject.transform.position = new Vector3(0.0f, 0.0f,-10.0f);
            }
        }
    }

    public void StartShakingCamera()
    {
        shake = true;
        shakingStartedTime = Time.time;
        gameObject.transform.position = new Vector3(0.0f, 0.2f,-10.0f);
    }

    public void StopShakingCamera()
    {
        shake = false;
        gameObject.transform.position = new Vector3(0.0f, 0.0f,-10.0f);
    }
}
