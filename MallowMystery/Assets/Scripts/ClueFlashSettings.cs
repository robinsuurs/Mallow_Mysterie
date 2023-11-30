using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

[VolumeComponentMenuForRenderPipeline("Custom/Clue Flash", typeof(UniversalRenderPipeline))]
public class ClueFlashSettings : VolumeComponent, IPostProcessComponent
{
    [SerializeField] private FloatParameter flashSpeed = new FloatParameter(0.0f);
    
    public bool IsActive()
    {
        //render this effect when:
        //Speed is higher than 0 and the Component is set active in the engine 
        return flashSpeed.value>0.0f && active;
    }

    public bool IsTileCompatible()
    {
        //don't know what this is, and we don't use it
        return false;
    }
}
