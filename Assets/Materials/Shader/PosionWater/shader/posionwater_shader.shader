// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "posionwater"
{
	Properties
	{
		[HideInInspector] _AlphaCutoff("Alpha Cutoff ", Range(0, 1)) = 0.5
		[HideInInspector] _EmissionColor("Emission Color", Color) = (1,1,1,1)
		[ASEBegin]_SurfaceRange("SurfaceRange", Float) = 10
		_NoiseRange("NoiseRange", Float) = 1
		_TimeSpeed("TimeSpeed", Float) = 10
		_NoiseIntensity("NoiseIntensity", Float) = 5
		_WaterColor("WaterColor", Color) = (0.1097415,0.7264151,0,0)
		[HDR]_SurfaceColor("SurfaceColor", Color) = (0.2588235,1.498039,0,0)
		[ASEEnd][HDR]_WaveColor("WaveColor", Color) = (0.05258589,0.3018868,0,0)

	}

	SubShader
	{
		LOD 0

		

		Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Transparent" "Queue"="Transparent" }

		Cull Off
		HLSLINCLUDE
		#pragma target 2.0
		
		#pragma prefer_hlslcc gles
		#pragma exclude_renderers d3d11_9x 

		ENDHLSL

		
		Pass
		{
			Name "Unlit"
			

			Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
			ZTest LEqual
			ZWrite Off
			Offset 0 , 0
			ColorMask RGBA
			

			HLSLPROGRAM
			
			#define ASE_SRP_VERSION 999999

			
			#pragma vertex vert
			#pragma fragment frag

			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA

			#define _SURFACE_TYPE_TRANSPARENT 1
			#define SHADERPASS_SPRITEUNLIT

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

			

			CBUFFER_START( UnityPerMaterial )
			float4 _SurfaceColor;
			float4 _WaveColor;
			float4 _WaterColor;
			float _SurfaceRange;
			float _NoiseRange;
			float _TimeSpeed;
			float _NoiseIntensity;
			CBUFFER_END


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float4 uv0 : TEXCOORD0;
				float4 color : COLOR;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				float4 texCoord0 : TEXCOORD0;
				float4 color : TEXCOORD1;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			#if ETC1_EXTERNAL_ALPHA
				TEXTURE2D( _AlphaTex ); SAMPLER( sampler_AlphaTex );
				float _EnableAlphaTexture;
			#endif

			float4 _RendererColor;

					float2 voronoihash22( float2 p )
					{
						
						p = float2( dot( p, float2( 127.1, 311.7 ) ), dot( p, float2( 269.5, 183.3 ) ) );
						return frac( sin( p ) *43758.5453);
					}
			
					float voronoi22( float2 v, float time, inout float2 id, inout float2 mr, float smoothness, inout float2 smoothId )
					{
						float2 n = floor( v );
						float2 f = frac( v );
						float F1 = 8.0;
						float F2 = 8.0; float2 mg = 0;
						for ( int j = -1; j <= 1; j++ )
						{
							for ( int i = -1; i <= 1; i++ )
						 	{
						 		float2 g = float2( i, j );
						 		float2 o = voronoihash22( n + g );
								o = ( sin( time + o * 6.2831 ) * 0.5 + 0.5 ); float2 r = f - g - o;
								float d = 0.707 * sqrt(dot( r, r ));
						 		if( d<F1 ) {
						 			F2 = F1;
						 			F1 = d; mg = g; mr = r; id = o;
						 		} else if( d<F2 ) {
						 			F2 = d;
						
						 		}
						 	}
						}
						return F1;
					}
			

			VertexOutput vert( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );

				
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3( 0, 0, 0 );
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif
				v.normal = v.normal;

				VertexPositionInputs vertexInput = GetVertexPositionInputs( v.vertex.xyz );

				o.texCoord0 = v.uv0;
				o.color = v.color;
				o.clipPos = vertexInput.positionCS;

				return o;
			}

			half4 frag( VertexOutput IN  ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				float2 texCoord19 = IN.texCoord0.xy * float2( 1,1 ) + float2( 0,0 );
				float2 texCoord23 = IN.texCoord0.xy * float2( 1,1 ) + float2( 0,0 );
				float mulTime30 = _TimeParameters.x * ( _TimeSpeed * 0.01 );
				float time22 = ( mulTime30 * 100.0 );
				float2 voronoiSmoothId0 = 0;
				float2 texCoord29 = IN.texCoord0.xy * float2( 1,1 ) + float2( 0,0 );
				float2 coords22 = ( texCoord29 + frac( mulTime30 ) ) * 20.0;
				float2 id22 = 0;
				float2 uv22 = 0;
				float voroi22 = voronoi22( coords22, time22, id22, uv22, 0, voronoiSmoothId0 );
				
				float4 Color = ( saturate( ( pow( texCoord19.y , _SurfaceRange ) * _SurfaceColor ) ) + ( _WaveColor * saturate( ( pow( texCoord23.y , _NoiseRange ) * pow( voroi22 , _NoiseIntensity ) ) ) ) + _WaterColor );

				#if ETC1_EXTERNAL_ALPHA
					float4 alpha = SAMPLE_TEXTURE2D( _AlphaTex, sampler_AlphaTex, IN.texCoord0.xy );
					Color.a = lerp( Color.a, alpha.r, _EnableAlphaTexture );
				#endif

				Color *= IN.color;

				return Color;
			}

			ENDHLSL
		}
	}
	CustomEditor "UnityEditor.ShaderGraph.PBRMasterGUI"
	Fallback "Hidden/InternalErrorShader"
	
}
/*ASEBEGIN
Version=18912
459.3333;286;1058.667;684.3334;1109.305;1005.113;2.398366;True;False
Node;AmplifyShaderEditor.CommentaryNode;35;-1986.708,-308.8368;Inherit;False;2283.838;1060.514;Wave;21;18;27;22;23;24;25;26;29;28;20;30;31;34;21;32;5;7;11;10;15;12;Wave;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;32;-1929.422,471.1823;Inherit;False;Constant;_Float0;Float 0;3;0;Create;True;0;0;0;False;0;False;0.01;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-1936.708,357.0269;Inherit;False;Property;_TimeSpeed;TimeSpeed;2;0;Create;True;0;0;0;False;0;False;10;2.9;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;-1720.822,392.4821;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;30;-1532.935,380.8781;Inherit;True;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;27;-1285.682,636.6768;Inherit;False;Constant;_Float1;Float 1;1;0;Create;True;0;0;0;False;0;False;100;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;28;-1320.596,366.5891;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;29;-1391.228,217.3244;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-1133.305,515.9672;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;25;-1136.307,276.7498;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;23;-1342.899,-175.852;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;36;-1490.023,-901.4905;Inherit;False;1301.588;524.9257;Comment;7;19;6;9;13;14;16;17;Surface;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-952.9713,587.6387;Inherit;False;Constant;_Float2;Float 2;1;0;Create;True;0;0;0;False;0;False;20;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-1137.404,12.05563;Inherit;False;Property;_NoiseRange;NoiseRange;1;0;Create;True;0;0;0;False;0;False;1;2.4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;19;-1440.023,-851.4905;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;34;-745.9521,563.1086;Inherit;False;Property;_NoiseIntensity;NoiseIntensity;3;0;Create;True;0;0;0;False;0;False;5;5.7;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.VoronoiNode;22;-810.7196,237.4615;Inherit;True;0;1;1;0;1;False;1;False;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;3;FLOAT;0;FLOAT2;1;FLOAT2;2
Node;AmplifyShaderEditor.BreakToComponentsNode;21;-1077.157,-172.2647;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.PowerNode;15;-591.1704,247.0251;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-1108.494,-571.3065;Inherit;False;Property;_SurfaceRange;SurfaceRange;0;0;Create;True;0;0;0;False;0;False;10;25.6;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;18;-819.7255,-162.6649;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;16;-1174.281,-847.9025;Inherit;True;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-392.5951,-28.50586;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;13;-916.8488,-838.3035;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;14;-895.4901,-583.5648;Inherit;False;Property;_SurfaceColor;SurfaceColor;5;1;[HDR];Create;True;0;0;0;False;0;False;0.2588235,1.498039,0,0;0.6603774,0.6296543,0.1100628,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;10;-333.6352,-258.8368;Inherit;False;Property;_WaveColor;WaveColor;6;1;[HDR];Create;True;0;0;0;False;0;False;0.05258589,0.3018868,0,0;0.08259161,0.8207547,0.08259161,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-611.588,-836.2765;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;11;-144.2742,-28.65097;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;8;377.7822,-3.470928;Inherit;False;Property;_WaterColor;WaterColor;4;0;Create;True;0;0;0;False;0;False;0.1097415,0.7264151,0,0;0.002254613,0.3584906,0.04466366,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;62.12997,-196.2529;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;6;-386.4352,-835.4335;Inherit;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;33;563.5693,-408.595;Inherit;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;76;886.671,-403.0323;Float;False;True;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;13;posionwater;cf964e524c8e69742b1d21fbe2ebcc4a;True;Unlit;0;0;Unlit;3;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;0;True;17;d3d9;d3d11;glcore;gles;gles3;metal;vulkan;xbox360;xboxone;xboxseries;ps4;playstation;psp2;n3ds;wiiu;switch;nomrt;0;False;True;2;5;False;-1;10;False;-1;3;1;False;-1;10;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;-1;False;False;False;False;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;True;2;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;0;False;False;0;Hidden/InternalErrorShader;0;0;Standard;1;Vertex Position;1;0;1;True;False;;False;0
WireConnection;31;0;5;0
WireConnection;31;1;32;0
WireConnection;30;0;31;0
WireConnection;28;0;30;0
WireConnection;26;0;30;0
WireConnection;26;1;27;0
WireConnection;25;0;29;0
WireConnection;25;1;28;0
WireConnection;22;0;25;0
WireConnection;22;1;26;0
WireConnection;22;2;24;0
WireConnection;21;0;23;0
WireConnection;15;0;22;0
WireConnection;15;1;34;0
WireConnection;18;0;21;1
WireConnection;18;1;20;0
WireConnection;16;0;19;0
WireConnection;12;0;18;0
WireConnection;12;1;15;0
WireConnection;13;0;16;1
WireConnection;13;1;17;0
WireConnection;9;0;13;0
WireConnection;9;1;14;0
WireConnection;11;0;12;0
WireConnection;7;0;10;0
WireConnection;7;1;11;0
WireConnection;6;0;9;0
WireConnection;33;0;6;0
WireConnection;33;1;7;0
WireConnection;33;2;8;0
WireConnection;76;1;33;0
ASEEND*/
//CHKSM=6AB77F6FA9A8EDB0393EF713BFD008F96D29AE23