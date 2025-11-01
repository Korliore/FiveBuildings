using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveLoadController : MonoBehaviour
{
    [SerializeField] private InventoryController _inventory;
    [SerializeField] private AudioController _audioController;

    private List<Building> _allBuildings = new List<Building>();

    private void Awake()
    {
        _allBuildings = FindObjectsByType<Building>(FindObjectsSortMode.None).ToList();
    }

    private void OnApplicationQuit()
    {
        SaveAll();
    }

    private void SaveAll()
    {
        SaveSystem.Save(_allBuildings, _inventory.Inventory, _audioController);
    }
}
