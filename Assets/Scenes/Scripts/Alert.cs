using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Alert : MonoBehaviour
{
    private static Alert instance;

    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private CanvasGroup canvasGroup;

    public static Alert Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    public void ShowMessage(string message)
    { 
        messageText.text = message;
        canvasGroup.alpha = 1.0f;
        StartCoroutine(HideMessage());
    }

    IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(2);
        canvasGroup.alpha = 0f;
    }
}
