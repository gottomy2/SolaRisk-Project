using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIResourceBar : MonoBehaviour
{
    public static UIResourceBar Instance { get; private set; }

    public Image mask;
    public TextMeshProUGUI textMesh;
    float originalSize;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }
    
    public void SetValue(float value)
    {
        originalSize = mask.rectTransform.rect.width;
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
    public void SetText(string text)
    {
        textMesh.text = text;
    }
}
