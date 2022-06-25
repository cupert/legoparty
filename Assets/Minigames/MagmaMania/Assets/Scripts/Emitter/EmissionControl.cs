using UnityEngine;

public class EmissionControl : MonoBehaviour
{
    new Renderer renderer;
    Material material;
    [SerializeField] float emissionStartValue = 1.9f;
    [SerializeField] float emissionAddValue = 0.05f;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        material = GetComponent<MeshRenderer>().material;
        material.SetFloat("_EmissionPower", 1.9f);
    }

    public void ChangeEmissionValue()
    {
        float newEmissionValue = emissionStartValue + emissionAddValue;
        material.SetFloat("_EmissionPower", newEmissionValue);
        emissionStartValue = newEmissionValue;
        renderer.UpdateGIMaterials();
        DynamicGI.UpdateEnvironment();
    }
}
