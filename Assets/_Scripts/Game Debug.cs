using NUnit.Framework;
using TMPro;
using UnityEngine;

// ⚠️ Debug | Can be removed
public class GameDebug : MonoBehaviour
{
    public TMP_Text _velocityText;

    public static GameDebug Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SetVelocityText(string text)
    {
        if (_velocityText)
            _velocityText.SetText(text);
    }
}