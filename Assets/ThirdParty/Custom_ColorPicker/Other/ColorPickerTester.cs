using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPickerTester : MonoBehaviour 
{

    public Renderer partRenderer;
    public ColorPicker picker;

    public Color Color = Color.red;
    List<Color> originalMaterials = new List<Color>();

    // Use this for initialization
    void Awake () 
    {
        partRenderer = GetComponent<Renderer>();

        foreach (Material mat in partRenderer.materials)
        {
            var newCreatedMaterialInstance = mat.color;
            originalMaterials.Add(newCreatedMaterialInstance);
        }

        picker.onValueChanged.AddListener(color =>
        {
            foreach (Material mat in partRenderer.materials)
            {
                mat.color = color;
            }

            Color = color;
        });
    }
}
