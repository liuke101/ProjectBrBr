Shader "Shield/edge"
{  
    Properties  
    {  
        [HDR]_LaserColor ("LaserColor", Color) = (1,1,1,1)  
    }  
    SubShader  
    {  
        Tags   
{  
            "RenderType"="Transparent"  
            "Queue" = "Transparent"  
            "RenderPipeline" = "UniversalPipeLine"    
}  
          
        HLSLINCLUDE  
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"  
        CBUFFER_START(UnityPerMaterial)  
        float4 _LaserColor;
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
            };  
  
            struct v2f  
            {  
                float4 vertex     : SV_POSITION;  
            };  
            
            v2f vert (appdata v)  
            {  
                v2f o;  
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                return o;  
            }  
              
            float4 frag (v2f i) : SV_Target  
            {  
                return  _LaserColor;  
            }  
            ENDHLSL  
        }  
    }  
}
