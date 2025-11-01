using TMPro;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private ResourceUIElement[] _resourceUIElements = new ResourceUIElement[5];

    public void UpdateUIElement(ResourceType resourceType, string value)
    {
        for (int i = 0; i < _resourceUIElements.Length; i++)
        {
            if (_resourceUIElements[i].ResourceType == resourceType)
            {
                _resourceUIElements[i].Text.text = value;
                break;
            }
        }
    }

    [System.Serializable]
    private struct ResourceUIElement
    {
        [field: SerializeField] public ResourceType ResourceType { get; private set; }
        [field: SerializeField] public TextMeshProUGUI Text { get; private set; }
    }
}