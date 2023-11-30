using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CustomPostProcessRenderFeature: ScriptableRendererFeature
{
    private FlashRenderPass _pass;
    public override void Create()
    {
        name = "Clue Flash";
        _pass = new FlashRenderPass();
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
       
    }
}