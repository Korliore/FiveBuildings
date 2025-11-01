using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private InventoryView _inventoryView;

    private InventoryModel _inventory;
    public InventoryModel Inventory => _inventory;

    private void Awake()
    {
        _inventory = new InventoryModel();
    }

    private void Start()
    {
        LoadInventoryInfo();
        UpdateUI();
    }

    private void OnEnable()
    {
        GlobalEvents.OnResourceCollected += CollectResource;
    }

    private void OnDisable()
    {
        GlobalEvents.OnResourceCollected -= CollectResource;
    }

    private void LoadInventoryInfo()
    {
        InventoryDTO inventoryDTO = SaveSystem.Load()?.inventory;

        if (inventoryDTO != null)
        {
            foreach (ResourceEntryDTO resource in inventoryDTO.resources)
            {
                _inventory.AddResource(resource.type, resource.value);
            }
        }
    }

    public void CollectResource(ResourceType resourceType, int value)
    {
        _inventory.AddResource(resourceType, value);
        UpdateUI();
    }

    public void UseResource(ResourceType resourceType, int value)
    {
        _inventory.RemoveResource(resourceType, value);
        UpdateUI();
    }

    public void UpdateUI()
    {
        foreach (KeyValuePair<ResourceType, int> resource in _inventory.Inventory)
        {
            _inventoryView.UpdateUIElement(resource.Key, resource.Value.ToString());
        }
    }
}