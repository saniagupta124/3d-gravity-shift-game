using UnityEngine;

public class SkyboxBlender : MonoBehaviour
{
    public Material nightMaterial;
    public Material sunsetMaterial;
    private Material skyMaterial;

    public float blendSpeed = 0.5f;
    private float t = 0f;

    void Start()
    {
        // Create a runtime instance
        skyMaterial = new Material(nightMaterial);
        RenderSettings.skybox = skyMaterial;
        DynamicGI.UpdateEnvironment(); // <- initial lighting update
    }

    void Update()
    {
        if (t < 1f)
        {
            t += Time.deltaTime * blendSpeed;

            BlendMaterialProperties(nightMaterial, sunsetMaterial, t);
            DynamicGI.UpdateEnvironment(); // <- keep lighting synced
        }
    }

    void BlendMaterialProperties(Material from, Material to, float t)
    {
        // Interpolate color and float properties manually
        skyMaterial.SetColor("_SunDiscColor", Color.Lerp(from.GetColor("_SunDiscColor"), to.GetColor("_SunDiscColor"), t));
        skyMaterial.SetFloat("_SunDiscMultiplier", Mathf.Lerp(from.GetFloat("_SunDiscMultiplier"), to.GetFloat("_SunDiscMultiplier"), t));
        skyMaterial.SetFloat("_SunDiscExponent", Mathf.Lerp(from.GetFloat("_SunDiscExponent"), to.GetFloat("_SunDiscExponent"), t));

        skyMaterial.SetColor("_SunHaloColor", Color.Lerp(from.GetColor("_SunHaloColor"), to.GetColor("_SunHaloColor"), t));
        skyMaterial.SetFloat("_SunHaloExponent", Mathf.Lerp(from.GetFloat("_SunHaloExponent"), to.GetFloat("_SunHaloExponent"), t));
        skyMaterial.SetFloat("_SunHaloContribution", Mathf.Lerp(from.GetFloat("_SunHaloContribution"), to.GetFloat("_SunHaloContribution"), t));

        skyMaterial.SetColor("_HorizonLineColor", Color.Lerp(from.GetColor("_HorizonLineColor"), to.GetColor("_HorizonLineColor"), t));
        skyMaterial.SetFloat("_HorizonLineExponent", Mathf.Lerp(from.GetFloat("_HorizonLineExponent"), to.GetFloat("_HorizonLineExponent"), t));
        skyMaterial.SetFloat("_HorizonLineContribution", Mathf.Lerp(from.GetFloat("_HorizonLineContribution"), to.GetFloat("_HorizonLineContribution"), t));

        skyMaterial.SetColor("_SkyGradientTop", Color.Lerp(from.GetColor("_SkyGradientTop"), to.GetColor("_SkyGradientTop"), t));
        skyMaterial.SetColor("_SkyGradientBottom", Color.Lerp(from.GetColor("_SkyGradientBottom"), to.GetColor("_SkyGradientBottom"), t));
        skyMaterial.SetFloat("_SkyGradientExponent", Mathf.Lerp(from.GetFloat("_SkyGradientExponent"), to.GetFloat("_SkyGradientExponent"), t));
    }
}
