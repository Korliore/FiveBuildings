using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private static string _savePath => Path.Combine(Application.persistentDataPath, "save.json");

    [Serializable]
    public class SaveDTO
    {
        public List<BuildingDTO> buildings;
        public InventoryDTO inventory;
        public AudioSettingsDTO audioSettings;
    }

    public static void Save(List<Building> buildings, InventoryModel inventory, AudioController audioSettings)
    {
        SaveDTO save = new SaveDTO()
        {
            buildings = new List<BuildingDTO>(),
            inventory = new InventoryDTO(),
            audioSettings = new AudioSettingsDTO()
        };

        foreach (Building building in buildings)
        {
            save.buildings.Add(new BuildingDTO
            {
                id = building.UniqueID,
                currentResource = building.CurrentResource
            });
        }

        foreach (KeyValuePair<ResourceType, int> inventoryElement in inventory.Inventory)
        {
            save.inventory.resources.Add(new ResourceEntryDTO
            {
                type = inventoryElement.Key,
                value = inventoryElement.Value
            });
        }

        save.audioSettings.masterVolume = audioSettings.MasterVolume;
        save.audioSettings.musicVolume = audioSettings.MusicVolume;
        save.audioSettings.sfxVolume = audioSettings.SFXVolume;

        string json = JsonUtility.ToJson(save, true);
        File.WriteAllText(_savePath, json);

        Debug.Log("Game Saved");
    }

    public static SaveDTO Load()
    {
        if (!CheckSaveFileExists()) return null;

        string json = File.ReadAllText(_savePath);
        return JsonUtility.FromJson<SaveDTO>(json);
    }

    #region Utility

    private static bool CheckSaveFileExists()
    {
        if (!File.Exists(_savePath))
        {
            Debug.LogWarning("Save file not found");
            return false;
        }

        return true;
    }

    #endregion
}