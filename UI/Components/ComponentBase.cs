using UnityEngine;

namespace Util.UiComponents
{
    public class ComponentBase : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}