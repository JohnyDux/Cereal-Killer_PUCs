using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WantedLevel : MonoBehaviour
{
    [SerializeField] RectTransform WantedMask;
    [Range(0, 297.9f)]
    public float maskValue;

    private void Update()
    {
        // Check if the target RectTransform is assigned
        if (WantedMask != null)
        {
            // Change the width while keeping the current height
            Vector2 newSize = new Vector2(maskValue, WantedMask.sizeDelta.y);
            WantedMask.sizeDelta = newSize;
        }
        else
        {
            Debug.LogError("Target RectTransform is not assigned.");
        }
    }
}
