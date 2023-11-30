using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

    public class FlashRenderPass: ScriptableRenderPass
    {
        [SerializeField] private Material _material;
        private ClueFlashSettings _settings;

        private RenderTargetIdentifier _source;
        private RenderTargetIdentifier _renderTarget;
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
            }
        }
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            throw new System.NotImplementedException();
        }
    }
