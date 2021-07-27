using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{

  [SerializeField] private Light DirectionalLight;
   [SerializeField] private LightingPreset Preset;
   private float TimeOfDay;



    private void Update()
    {
        TimeOfDay = System.DateTime.Now.Hour + (System.DateTime.Now.Minute / 60f);
        if (Preset == null)
            return;
        if (Application.isPlaying)
        {
            UpdateLighting(TimeOfDay / 24f);
            
        }
    }
    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);
        if(DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }

    }
    public void OnValidate()
    {
        
        if (DirectionalLight != null)
            return;
        if(RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach(Light light in lights)
            {
                if(light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }


}
