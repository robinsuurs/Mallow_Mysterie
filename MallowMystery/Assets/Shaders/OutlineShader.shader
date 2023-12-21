Shader "Custom/OutlineShader"
{
    Properties
    {
        _MainTex("main texture",2d) = "white"{}
        _EdgeThreshold("edge threshold", Range(0,1)) = 0.5
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
                float4 positionSS   : TEXCOORD1;
            };

            sampler2D _MainTex;
            
            
             CBUFFER_START(UnityPerMaterial)
                float4 _mainTex_st;
                float _EdgeThreshold;
             CBUFFER_END
            
            float sobel(float2 UV)
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
                
                 float edge_x = 0;
                 float edge_y = 0;
                
                 UNITY_UNROLL
                 for (int Iterator = 0; Iterator < 9; ++Iterator)
                 {
                     float Depth = SampleSceneDepth(UV + GSobelSamplePoints[Iterator]);
                     // calculation data
                     const float kernel_x = GSobelXMatrix[Iterator];
                     const float kernel_y = GSobelYMatrix[Iterator];
                    
                     edge_x += Depth * kernel_x;
                     edge_y += Depth * kernel_y;
                 }

				
				return sqrt(edge_x*edge_x + edge_y*edge_y);
			}
            // The vertex shader definition with properties defined in the Varyings
            // structure. The type of the vert function must match the type (struct)
            // that it returns.
            Varyings vert(Attributes IN)
            {
                 
                 Varyings OUT;
                 
                 OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
                 OUT.positionSS = ComputeScreenPos(OUT.positionCS);
                 OUT.UV = IN.UV;
                 

                 return OUT;
            }
            
            // The fragment shader definition.
            float4 frag(Varyings IN) : SV_Target
            {
                float2 screenUVs = IN.positionSS.xy / IN.positionSS.w;
                float4 edgeColor = float4(1,1,1,1);
                float outline_mask = sobel(screenUVs);

                edgeColor = outline_mask> _EdgeThreshold ? float4(1,1,1,1) : float4(0,0,0,0);
                
                return edgeColor;
            }
            ENDHLSL
                    
        }   
    }
}