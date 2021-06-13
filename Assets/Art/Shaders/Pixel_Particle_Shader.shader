// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Pixel Particle Shader"
{
	Properties
	{
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
		[HDR]_ColorTexture("Color Texture", Color) = (1,1,1,1)
		[NoScaleOffset]_AlbedoTextura("AlbedoTextura", 2D) = "white" {}
		_OpacityValue("Opacity Value", Range( 0 , 1)) = 1
		[Toggle]_GrayscaleOn("Grayscale On", Float) = 1
		_Pixels("Pixels", Float) = 24
		[Toggle]_Pixelson("Pixels on", Float) = 0

	}


	Category 
	{
		SubShader
		{
		LOD 0

			Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMask RGB
			Cull Off
			Lighting Off 
			ZWrite Off
			ZTest LEqual
			
			Pass {
			
				CGPROGRAM
				
				#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
				#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
				#endif
				
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 2.0
				#pragma multi_compile_instancing
				#pragma multi_compile_particles
				#pragma multi_compile_fog
				#define ASE_NEEDS_FRAG_COLOR


				#include "UnityCG.cginc"

				struct appdata_t 
				{
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
					
				};

				struct v2f 
				{
					float4 vertex : SV_POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					UNITY_FOG_COORDS(1)
					#ifdef SOFTPARTICLES_ON
					float4 projPos : TEXCOORD2;
					#endif
					UNITY_VERTEX_INPUT_INSTANCE_ID
					UNITY_VERTEX_OUTPUT_STEREO
					
				};
				
				
				#if UNITY_VERSION >= 560
				UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
				#else
				uniform sampler2D_float _CameraDepthTexture;
				#endif

				//Don't delete this comment
				// uniform sampler2D_float _CameraDepthTexture;

				uniform sampler2D _MainTex;
				uniform fixed4 _TintColor;
				uniform float4 _MainTex_ST;
				uniform float _InvFade;
				uniform half4 _ColorTexture;
				uniform sampler2D _AlbedoTextura;
				uniform half _Pixels;
				uniform half _Pixelson;
				uniform half _GrayscaleOn;
				uniform half _OpacityValue;


				v2f vert ( appdata_t v  )
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					

					v.vertex.xyz +=  float3( 0, 0, 0 ) ;
					o.vertex = UnityObjectToClipPos(v.vertex);
					#ifdef SOFTPARTICLES_ON
						o.projPos = ComputeScreenPos (o.vertex);
						COMPUTE_EYEDEPTH(o.projPos.z);
					#endif
					o.color = v.color;
					o.texcoord = v.texcoord;
					UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}

				fixed4 frag ( v2f i  ) : SV_Target
				{
					UNITY_SETUP_INSTANCE_ID( i );
					UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( i );

					#ifdef SOFTPARTICLES_ON
						float sceneZ = LinearEyeDepth (SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)));
						float partZ = i.projPos.z;
						float fade = saturate (_InvFade * (sceneZ-partZ));
						i.color.a *= fade;
					#endif

					half4 appendResult18 = (half4(_ColorTexture.r , _ColorTexture.g , _ColorTexture.b , i.color.b));
					half2 texCoord22 = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
					float pixelWidth23 =  1.0f / _Pixels;
					float pixelHeight23 = 1.0f / _Pixels;
					half2 pixelateduv23 = half2((int)(texCoord22.x / pixelWidth23) * pixelWidth23, (int)(texCoord22.y / pixelHeight23) * pixelHeight23);
					half2 lerpResult30 = lerp( texCoord22 , pixelateduv23 , (( _Pixelson )?( 1.0 ):( 0.0 )));
					half4 tex2DNode2 = tex2D( _AlbedoTextura, lerpResult30 );
					half grayscale15 = Luminance(tex2DNode2.rgb);
					half4 temp_cast_1 = (grayscale15).xxxx;
					half4 lerpResult16 = lerp( tex2DNode2 , temp_cast_1 , (( _GrayscaleOn )?( 1.0 ):( 0.0 )));
					half4 lerpResult45 = lerp( float4( 0,0,0,0 ) , ( appendResult18 * lerpResult16 ) , tex2DNode2.a);
					half4 lerpResult4 = lerp( float4( 0,0,0,0 ) , lerpResult45 , _OpacityValue);
					

					fixed4 col = lerpResult4;
					UNITY_APPLY_FOG(i.fogCoord, col);
					return col;
				}
				ENDCG 
			}
		}	
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18900
1920;208;1360;707;1989.944;895.9653;2.152813;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;22;-1330.299,36.96724;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;24;-1390.305,196.826;Inherit;False;Property;_Pixels;Pixels;4;0;Create;True;0;0;0;False;0;False;24;150;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;31;-1221.891,303.0878;Inherit;False;Property;_Pixelson;Pixels on;5;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCPixelate;23;-1191.927,184.2712;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;30;-1013.891,1.087799;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;2;-851.4046,2.40177;Inherit;True;Property;_AlbedoTextura;AlbedoTextura;1;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;0e6dbe24d642a804fbeff1c00a3d034e;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCGrayscale;15;-231.6655,84.51642;Inherit;False;0;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;26;-192.0624,166.7269;Inherit;False;Property;_GrayscaleOn;Grayscale On;3;0;Create;True;0;0;0;False;0;False;1;True;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;8;-554.3835,-287.1602;Inherit;False;Property;_ColorTexture;Color Texture;0;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;19;-720.6962,192.8578;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;18;-158.0759,-223.1788;Inherit;False;COLOR;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;16;12.68517,9.806572;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;193.4877,-43.88274;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;17;147.6613,267.2957;Inherit;False;Property;_OpacityValue;Opacity Value;2;0;Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;45;354.4696,-2.547891;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;4;567.5626,-37.16257;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;41;796.5974,-33.26105;Half;False;True;-1;2;ASEMaterialInspector;0;7;Pixel Particle Shader;0b6a9f8b4f707c74ca64c0be8e590de0;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;False;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;-1;False;True;True;True;True;False;0;False;-1;False;False;False;False;False;False;False;False;False;True;2;False;-1;True;3;False;-1;False;True;4;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;0;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;23;0;22;0
WireConnection;23;1;24;0
WireConnection;23;2;24;0
WireConnection;30;0;22;0
WireConnection;30;1;23;0
WireConnection;30;2;31;0
WireConnection;2;1;30;0
WireConnection;15;0;2;0
WireConnection;18;0;8;1
WireConnection;18;1;8;2
WireConnection;18;2;8;3
WireConnection;18;3;19;3
WireConnection;16;0;2;0
WireConnection;16;1;15;0
WireConnection;16;2;26;0
WireConnection;7;0;18;0
WireConnection;7;1;16;0
WireConnection;45;1;7;0
WireConnection;45;2;2;4
WireConnection;4;1;45;0
WireConnection;4;2;17;0
WireConnection;41;0;4;0
ASEEND*/
//CHKSM=BCB7A3B3C089AEF010F0F196A05325317BB510BC