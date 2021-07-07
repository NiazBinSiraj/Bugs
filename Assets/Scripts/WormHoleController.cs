using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormHoleController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private float hue;
    private Color spriteColor;
    public float colorChangeDelay;
    public float increment;
    public float saturation;
    public float value;
    private float currentTime;
    private float lastColorChangeTime;
    public float rotationSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastColorChangeTime = Time.time;

        hue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f,0f,rotationSpeed);
        currentTime = Time.time;

        if(currentTime - lastColorChangeTime >= colorChangeDelay)
        {
            hue += increment;
            hue = hue%360;
            float tHue = hue/360;
            float tSaturation = saturation/100;
            float tValue = value/100;
            spriteColor = Color.HSVToRGB(tHue,tSaturation,tValue);
            spriteRenderer.color = spriteColor;
            lastColorChangeTime = currentTime;
        }
    }
}
