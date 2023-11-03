Shader "Unlit/SeeThrough"
{
     // The properties block of the Unity shader. In this example this block is empty
    // because the output color is predefined in the fragment shader code.
    Properties
    {
        _BaseColor("Color", Color) = (1,1,1,1)
        _SeeThroughCenter("See through position", Vector) = (0,0,0)
//       [MainTexture] _BaseMap("Base Map", 2D) = "white"
    }

    // The SubShader block containing the Shader code.
    SubShader
    {
        // SubShader Tags define when and under which conditions a SubShader block or
        // a pass is executed.
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            // The HLSL code block. Unity SRP uses the HLSL language.
            HLSLPROGRAM
            // This line defines the name of the vertex shader.
            #pragma vertex vert
            // This line defines the name of the fragment shader.
            #pragma fragment frag

            // The Core.hlsl file contains definitions of frequently used HLSL
            // macros and functions, and also contains #include references to other
            // HLSL files (for example, Common.hlsl, SpaceTransforms.hlsl, etc.).
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // The structure definition defines which variables it contains.
            // This example uses the Attributes structure as an input structure in
            // the vertex shader.
            struct Attributes
            {
                // The positionOS variable contains the vertex positions in object
                // space.
                float4 positionOS   : POSITION;
                float2 uv           : TEXCOORD0;
            };

            struct Varyings
            {
                // The positions in this struct must have the SV_POSITION semantic.
                float4 positionHCS  : SV_POSITION;
                float2 uv           : TEXCOORD0;
            };
    
            
             CBUFFER_START(UnityPerMaterial)
                float4 _BaseColor;
                float4 _SeeThroughCenter;
             CBUFFER_END
            
            // The vertex shader definition with properties defined in the Varyings
            // structure. The type of the vert function must match the type (struct)
            // that it returns.
            Varyings vert(Attributes IN)
            {
                // Declaring the output object (OUT) with the Varyings struct.
                Varyings OUT;
                 //adding the texture tiling to the vertex shader

                // The TransformObjectToHClip function transforms vertex positions
                // from object space to homogenous clip space.
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                 //centering
                OUT.uv = IN.uv*2-1;
                // Returning the output.
                return OUT;
            }
            
            // The fragment shader definition.
            float4 frag(Varyings IN) : SV_Target
            {
                float2 circlePos = IN.uv*_SeeThroughCenter;
                float4 circle =length(circlePos)-.3;
                return step(0,circle);
                float4 color = _BaseColor;
                return circle;
            }
            ENDHLSL
        }
    }
}
