using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    private TextMeshPro text; // Reference to the TextMeshProUGUI that displays Cost.

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshPro>();
        if (text == null)
        {
            ThrowError("TextMeshProUGUI component not found in children.");
        }
    }

    public void UpdateText(int newText)
    {
        if (text == null) return;
        text.text = newText.ToString();
    }

    public void UpdateText(string newText)
    {
        if (text == null) return;
        text.text = newText;
    }

    private void ThrowError(string message)
    {
        Debug.LogError(message);
    }
}