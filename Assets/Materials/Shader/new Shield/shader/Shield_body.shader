Shader "Shield/body"
{  
    Properties
    {
        _MatCap("MatCap",2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "Queue"="Geometry"
            "RenderType"="Opaque"
        }
        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        CBUFFER_START(UnityPerMaterial)
        float4 _MatCap_ST;
        CBUFFER_END
        ENDHLSL

        Pass
        {
            Tags{"LightMode"="Universal2D"}
            HLSLPROGRAM 
            #pragma vertex vert
            #pragma fragment frag

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD;
                float3 normal : NORMAL;
            };
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD;
                float3 normal : TEXCOORD1;
            };

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);
            TEXTURE2D(_MatCap);
            SAMPLER(sampler_MatCap);
            v2f vert(appdata i)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(i.vertex.xyz);
				o.normal = TransformObjectToWorld(i.normal.xyz);
                o.uv=i.uv;
                return o;
            }

            float4 frag(v2f v):SV_Target
            {
                float3 world_Normal = normalize(v.normal);
                float3 view_normal = mul(unity_MatrixV, world_Normal); 
                
                //MatCap
                float3 N01_ViewSpace = view_normal * 0.5 + 0.5;
                float3 MatCap =SAMPLE_TEXTURE2D(_MatCap, sampler_MatCap, N01_ViewSpace.xy);
                return float4(MatCap.xyz,1);
            }
            ENDHLSL           
        }
    }
}
