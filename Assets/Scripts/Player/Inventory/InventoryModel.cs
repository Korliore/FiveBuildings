using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryModel
{
    private Dictionary<ResourceType, int> _inventory = new Dictionary<ResourceType, int>()
    {
        { ResourceType.Wood, 0 },
        { ResourceType.Stone, 0 },
        { ResourceType.Coal, 0 },
        { ResourceType.Sand, 0 },
        { ResourceType.Gold, 0 },
    };

    public IReadOnlyDictionary<ResourceType, int> Inventory => _inventory;

    public void AddResource(ResourceType resourceType, int value = 1)
    {
        if (_inventory.ContainsKey(resourceType))
            _inventory[resourceType] = Mathf.Clamp(_inventory[resourceType] + value, 0, int.MaxValue);
        else
            ResourceWarning();
    }

    public void RemoveResource(ResourceType resourceType, int value = 1)
    {
        if (_inventory.ContainsKey(resourceType))
            _inventory[resourceType] = Mathf.Clamp(_inventory[resourceType] - value, 0, int.MaxValue);
        else
            ResourceWarning();
    }

    public int GetResourceValue(ResourceType resourceType)
    {
        if (_inventory.ContainsKey(resourceType))
            return _inventory[resourceType];
        else
        {
            ResourceWarning();
            return -1;
        }
    }

    private void ResourceWarning()
    {
        Console.WriteLine("ResourceType not exists");
    }
}

public enum ResourceType
{
    Wood,
    Stone,
    Coal,
    Sand,
    Gold
}