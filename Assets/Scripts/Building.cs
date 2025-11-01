using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private string _uniqueID;

    [Header("Production Settings")]
    [SerializeField] private string _resourceName = "Wood";
    [SerializeField] private float _produceDuration = 1f;
    [SerializeField] private int _maxResource = 1000;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _resourceText;

    private int _currentResource = 0;
    private Coroutine _produceResourceCor;

    public string UniqueID => _uniqueID;
    public string ResourceName => _resourceName;
    public int CurrentResource => _currentResource;

    #region Unity Lifecycle

    private void Awake()
    {
        UpdateResourceText();
    }

    private void Start()
    {
        LoadBuildingInfo();
    }

    private void OnEnable()
    {
        _produceResourceCor = StartCoroutine(ProduceResourceCoroutine());
    }

    private void OnDisable()
    {
        if (_produceResourceCor != null)
            StopCoroutine(_produceResourceCor);
    }

    #endregion

    #region Resource Collection/Production

    public int CollectAllResources()
    {
        if (Enum.TryParse(_resourceName, out ResourceType parsedType))
        {
            int collected = _currentResource;
            _currentResource = 0;

            UpdateResourceText();
            GlobalEvents.InvokeOnResourceCollected(parsedType, collected);

            return collected;
        }

        return 0;
    }

    private IEnumerator ProduceResourceCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_produceDuration);

            if (_currentResource < _maxResource)
            {
                _currentResource += 1;
                UpdateResourceText();
            }
        }
    }

    #endregion

    #region Identifier (GUID)

    public void GenerateID()
    {
        if (string.IsNullOrEmpty(_uniqueID))
            _uniqueID = Guid.NewGuid().ToString();
    }

    #endregion

    #region UI

    private void UpdateResourceText()
    {
        if (_resourceText != null)
        {
            _resourceText.text = $"{_resourceName}: {_currentResource}/{_maxResource}";
        }
    }

    #endregion

    #region Saving & Loading

    private void LoadBuildingInfo()
    {
        List<BuildingDTO> loadedBuildings = SaveSystem.Load()?.buildings;

        if (loadedBuildings != null)
        {
            for (int i = 0; i < loadedBuildings.Count; i++)
            {
                if (loadedBuildings[i].id == _uniqueID)
                {
                    _currentResource = loadedBuildings[i].currentResource;
                    break;
                }
            }
        }

        UpdateResourceText();
    }

    #endregion
}