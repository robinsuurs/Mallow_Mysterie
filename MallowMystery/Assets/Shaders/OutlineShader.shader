Shader "Custom/OutlineShader"
{
    Properties
    {
        _EdgeThickness("line thickness", Range(1,10))=1
        _EdgeThreshold("edge threshold", Range(0,10))=0.5
        _MainTex("Main Tex",2D)="white" {}
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
           
            "RenderPipeline" = "UniversalPipeline"
        }
      

        Pass
        {
           
           
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
            //DMS needed to read the Blit texture correctly
            Texture2DMS<float4> _MainTex;
            
            
             CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float _EdgeThickness;
                float _EdgeThreshold;
             CBUFFER_END


            //Sobel edge detection algorithm
            float sobel(float2 UV, float thickness)
			{

                 //points to sample around the current pixel
			    static const float2 GSobelSamplePoints[9] =
                {
                    float2(-1, +1), float2(+0, +1), float2(+1, +1),
                    float2(-1, +0), float2(+0, +0), float2(+1, +0),
                    float2(-1, -1), float2(+0, -1), float2(+1, -1),
                };
                //sobel matrix for the X direction
                static const float GSobelXMatrix[9] =
                {
                    +1, +0, -1,
                    +2, +0, -2,
                    +1, +0, -1,
                };
                //sobel matrix for the Y direction
                static const float GSobelYMatrix[9] =
                {
                    +1, +2, +1,
                    +0, +0, +0,
                    -1, -2, -1,
                };
                
                 float edge = 0;
                 
                
                 UNITY_UNROLL
                 for (int Iterator = 0; Iterator < 9; ++Iterator)
                 {
                     //sample all points
                     float Depth = SampleSceneDepth(UV + GSobelSamplePoints[Iterator] * thickness).r;
                     const float kernel_x = GSobelXMatrix[Iterator];
                     const float kernel_y = GSobelYMatrix[Iterator];
                     
                     //add together the samples in both directions
                     //should be 0 if no differance in depth and higher depending on the depth differance
                     edge = edge + Depth * float2 (kernel_x,kernel_y);
                 }

				
				return length(edge);
			}

            void Unity_Blend_Overwrite_float4(float4 Base, float4 Blend, float Opacity, out float4 Out)
            {
                 Out = lerp(Base, Blend, Opacity);
            }
            
            // The vertex shader definition with properties defined in the Varyings
            // structure. The type of the vert function must match the type (struct)
            // that it returns.
            Varyings vert(Attributes IN)
            {
                 
                 Varyings OUT;
                 
                 OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
                 OUT.positionSS = ComputeScreenPos(OUT.positionCS);
                 OUT.UV = TRANSFORM_TEX(IN.UV, _MainTex);
               
                 

                 return OUT;
            }
            
            // The fragment shader definition.
            float4 frag(Varyings IN) : SV_Target
            {
                //find the current pixel position on the screen
                float2 screenUVs = IN.positionSS.xy / IN.positionSS.w;

                //set the color of the edge
                float4 edgeColor = float4(0,0,0,1);
                
                float outline_mask = sobel(screenUVs, _EdgeThickness);
                
                //filter out differences that are too low
                outline_mask = step(outline_mask,_EdgeThreshold);
               
                float4 OUT;
                //blend the blit texture and the outline color depending on the difference in depth
                Unity_Blend_Overwrite_float4(_MainTex_ST,edgeColor, outline_mask,OUT);
                
                 return OUT;
            }
            ENDHLSL
                    
        }   
    }
}