using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This class sets the text and image of the popup message
/// </summary>
public class PopupMessage : MonoBehaviour
{
    public TextMeshProUGUI popupMessage;
    private Image image;

    /// <summary>
    /// Gets the X mark image in the child object
    /// </summary>
    private void Awake()
    {
        image = GetComponentsInChildren<Image>()[1];
    }


    /// <summary>
    /// Sets the new test and shows or hides the X mark image.
    /// Hides the whole gameObject after 2 seconds
    /// </summary>
    /// <param name="newText"></param>
    /// <param name="enabled"></param>
    /// <returns></returns>
    public IEnumerator Reset(string newText, bool enabled = false)
    {

        popupMessage.text = newText;
        image.gameObject.SetActive(enabled);
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
