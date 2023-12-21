Shader "Custom/OutlineShader"
{
    Properties
    {
        _MainTex("main texture",2d) = "white"{}
        
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
                float2 UV           : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS   : SV_POSITION;
                float2 UV           : TEXCOORD0;
            };

            sampler2D _MainTex;
            
             CBUFFER_START(UnityPerMaterial)
                float4 _mainTex_st;
             CBUFFER_END
            
            float2 sobel(float2 uv)
			{

                 
			    static const float2 GSobelSamplePoints[9] =
                {
                    float2(-1, +1), float2(+0, +1), float2(+1, +1),
                    float2(-1, +0), float2(+0, +0), float2(+1, +0),
                    float2(-1, -1), float2(+0, -1), float2(+1, -1),
                };

                static const float GSobelXMatrix[9] =
                {
                    +1, +0, -1,
                    +2, +0, -2,
                    +1, +0, -1,
                };

                static const float GSobelYMatrix[9] =
                {
                    +1, +2, +1,
                    +0, +0, +0,
                    -1, -2, -1,
                };
                
                float2 edge;
                
                 
                 for (int Iterator = 0; Iterator < 9; ++Iterator)
                 {
                     float Depth = SampleSceneDepth(UV + GSobelSamplePoints[Iterator] * Thickness);
                     // calculation data
                    const float2 Kernel = float2(GSobelXMatrix[Iterator], GSobelYMatrix[Iterator]);
                     
                 }

				
				return sqrt(edge);
			}
            // The vertex shader definition with properties defined in the Varyings
            // structure. The type of the vert function must match the type (struct)
            // that it returns.
            Varyings vert(Attributes IN)
            {
                 
                 Varyings OUT;
                 
                 OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
                 OUT.UV = IN.UV;
                 

                 return OUT;
            }
            
            // The fragment shader definition.
            float4 frag(Varyings IN) : SV_Target
            {
                
              
                
            }
            ENDHLSL
                    
        }   
    }
}