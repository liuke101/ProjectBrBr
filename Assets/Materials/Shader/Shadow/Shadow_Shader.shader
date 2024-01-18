Shader "Shadow/material"
{
    Properties
    {
        _ShadowColor("ShadowColor",Color)=(1,1,1,0.5)
        _Transparency("Transparency",Range(0,1))=1
    }
    // ---------------------------【子着色器】---------------------------
    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "RenderPipeline" = "UniversalPipeLine" }
        
        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        CBUFFER_START(UnityPerMaterial)
        float4 _ShadowTex_ST;
        float4 _ShadowColor;
        float _Transparency;
        CBUFFER_END
        ENDHLSL
        
        Pass
        {
            Tags{"LightMode" = "Universal2D"} 
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = v.uv;
                o.uv.y = 1 - o.uv.y;
                return o;
            }

            //通过c#获取到人物纹理传递过来
            TEXTURE2D(_ShadowTex);
            SAMPLER(sampler_ShadowTex);

            float4 frag (v2f i) : SV_Target
            {
                // 采样传过来的纹理
                float4 col = SAMPLE_TEXTURE2D(_ShadowTex, sampler_ShadowTex, i.uv);
                // 这里用step代替if
                // 当 透明度值大于1时, 就呈现黑色(即影子)
                col.rgb = (1 - step(0,col.a)) + _ShadowColor.rgb;
                return col *_Transparency;  
            }
            ENDHLSL
        }
    }
}