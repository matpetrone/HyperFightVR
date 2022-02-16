using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayButton : MonoBehaviour
{
    TMP_Text text_button;
    public Color32 startColor;
    public Color32 endColor;
    public float speed = 1f;



    private void Start()
    {
        text_button = GetComponent<TMP_Text>();
      
    }

    // Update is called once per frame
    void Update()
    {
        text_button.fontSharedMaterial.SetColor(ShaderUtilities.ID_GlowColor, Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed, 1)));

        
    }
}
