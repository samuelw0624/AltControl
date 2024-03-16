using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyFix : MonoBehaviour
{

    private void Start()
    {
        MaterialSettings(this.gameObject);
    }
    private void MaterialSettings(GameObject go)
    {
        Renderer[] renderers = go.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            foreach (Material material in renderer.sharedMaterials)
            {
                ApplyTransparency(material);
            }
        }
    }

    private static void ApplyTransparency(Material material)
    {

        material.EnableKeyword("_EMISSION");
        material.EnableKeyword("_ALPHABLEND_ON");

    }

}
