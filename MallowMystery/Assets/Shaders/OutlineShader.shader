Shader "Custom/OutlineShader"
{
    Properties
    {
        _ForegroundColor ("FG Color", Color) = (1, 1, 1, 1)
        _BackgroundColor ("BG Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
        }
      

        Pass
        {
            Tags
            {
                "LightMode" = "UniversalForward"
            }
           
            HLSLPROGRAM
          
            #pragma vertex vert
            #pragma fragment frag

          
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

            // The structure definition defines which variables it contains.
            struct Attributes
            {
                float4 positionOS   : POSITION;
                
            };

            struct Varyings
            {
                float4 positionCS  : SV_POSITION;
                float2 positionSS   : TEXCOORD0;
            };
    
            
             CBUFFER_START(UnityPerMaterial)
                float4 _ForegroundColor;
                float4 _BackgroundColor;
             CBUFFER_END
            
            // The vertex shader definition with properties defined in the Varyings
            // structure. The type of the vert function must match the type (struct)
            // that it returns.
            Varyings vert(Attributes IN)
            {
                 
                 Varyings OUT;
                 
                 OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
                 OUT.positionSS = ComputeScreenPos(OUT.positionCS);
                 

                 return OUT;
            }
            
            // The fragment shader definition.
            float4 frag(Varyings IN) : SV_Target
            {
                float2 screenUVs = IN.positionSS.xy / IN.positionSS.w;
                float rawDepth = tex2D(_CameraDepthTexture, screenUVs).x;
                
            }
            ENDHLSL
                    
        }   
    }
}