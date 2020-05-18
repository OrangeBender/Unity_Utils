using TMPro;
using UnityEngine;

namespace Util.UiComponents
{
    public class TextComponent : ComponentBase
    {
        [SerializeField] private TMP_Text text;

        public void SetText(string newText)
        {
            text.text = newText;
        }
    }
}