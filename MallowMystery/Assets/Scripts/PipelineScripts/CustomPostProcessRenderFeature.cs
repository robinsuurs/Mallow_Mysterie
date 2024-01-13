using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace PipelineScripts
{
    public class CustomPostProcessRenderFeature: ScriptableRendererFeature
    {
        [SerializeField] private Shader clueShader;
        private FlashRenderPass _pass;
        public override void Create()
        {
            name = "Clue Flash";
            _pass = new FlashRenderPass();
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
        
            _pass.Setup(renderer, "Clue Flash Post Process");
        }
    }
}