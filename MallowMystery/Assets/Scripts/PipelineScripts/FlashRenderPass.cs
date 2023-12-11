using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace PipelineScripts
{
    public class FlashRenderPass: ScriptableRenderPass
    {
        [SerializeField] private Material _material;
        private ClueFlashSettings _settings;

        private RTHandle _source;
        private RTHandle _renderTarget;
        private string _profilerTag;
        
        public void Setup(ScriptableRenderer renderer, string profilerTag)
        {
            _profilerTag = profilerTag;
            _source = renderer.cameraColorTargetHandle;
            VolumeStack stack = VolumeManager.instance.stack;
            _settings = stack.GetComponent<ClueFlashSettings>();
            renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
            if (_settings != null && _settings.IsActive())
            {
                renderer.EnqueuePass(this);
                _material = new Material(_material);
            }
        }

        
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (!_settings.IsActive())
            {
                return;
            }
            
            CommandBuffer cmd = CommandBufferPool.Get(_profilerTag);
            cmd.Blit(_source,_renderTarget);
            _material.SetTexture("_Maintex", _renderTarget);
            _material.SetFloat("_Flash_Speed", _settings.flashSpeed.value);
            cmd.Blit(_renderTarget, _source,_material);
            cmd.Clear();
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(Shader.PropertyToID("_MainTex"));
        }
    }
}
