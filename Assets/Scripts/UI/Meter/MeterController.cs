using UnityEngine;
using UnityEngine.UI;

public class MeterController : MonoBehaviour
{
    public RectTransform meterBar;
    private int maxValue;
    private int currentValue;

    void Start()
    {
        Debug.Log("MeterController started");
        meterBar = GetComponentInChildren<RectTransform>();
        maxValue = 100;
        currentValue = 100;
    }

    public void OnValueChange(int value)
    {
        Debug.Log($"OnValueChange called with value: {value}");
        if (meterBar != null) // Check if meterBar is not null before accessing its sizeDelta
        {
            // Calculate the percentage of the max value that the current value represents
            float percentage = (float)currentValue / maxValue;

            // Get the current size of the bar
            Vector2 size = meterBar.sizeDelta;

            // Set the width of the bar to the calculated percentage
            size.x = percentage * size.x;

            // Apply the new size to the bar
            meterBar.sizeDelta = size;
        }
        else
        {
            Debug.LogError("meterBar is not assigned in the inspector");
        }
    }
}