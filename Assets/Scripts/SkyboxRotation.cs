using UnityEngine;

public class SkyboxRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 1f;

    private Material _skyboxMaterial;

    private void Start()
    {
        _skyboxMaterial = RenderSettings.skybox;
    }

    private void Update()
    {
        RotateSkybox();
    }

    private void RotateSkybox()
    {
        if (_skyboxMaterial.HasProperty("_Rotation"))
        {
            float rotation = _skyboxMaterial.GetFloat("_Rotation");
            rotation += _rotationSpeed * Time.deltaTime;
            _skyboxMaterial.SetFloat("_Rotation", rotation);
        }
    }
}