// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Pixel Particle Shader"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		[PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
		[HDR]_ColorTexture("Color Texture", Color) = (1,1,1,1)
		[NoScaleOffset]_AlbedoTextura("AlbedoTextura", 2D) = "white" {}
		_OpacityValue("Opacity Value", Range( 0 , 1)) = 1
		[Toggle]_GrayscaleOn("Grayscale On", Float) = 0
		_Pixels("Pixels", Float) = 24
		[Toggle]_Pixelson("Pixels on", Float) = 0

	}

	SubShader
	{
		LOD 0

		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha
		
		
		Pass
		{
		CGPROGRAM
			
			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "UnityCG.cginc"
			#define ASE_NEEDS_FRAG_COLOR


			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				
			};
			
			uniform fixed4 _Color;
			uniform float _EnableExternalAlpha;
			uniform sampler2D _MainTex;
			uniform sampler2D _AlphaTex;
			uniform float4 _ColorTexture;
			uniform sampler2D _AlbedoTextura;
			uniform float _Pixels;
			uniform float _Pixelson;
			uniform float _GrayscaleOn;
			uniform float _OpacityValue;

			
			v2f vert( appdata_t IN  )
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				UNITY_TRANSFER_INSTANCE_ID(IN, OUT);
				
				
				IN.vertex.xyz +=  float3(0,0,0) ; 
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);

#if ETC1_EXTERNAL_ALPHA
				// get the color from an external texture (usecase: Alpha support for ETC1 on android)
				fixed4 alpha = tex2D (_AlphaTex, uv);
				color.a = lerp (color.a, alpha.r, _EnableExternalAlpha);
#endif //ETC1_EXTERNAL_ALPHA

				return color;
			}
			
			fixed4 frag(v2f IN  ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				float2 texCoord22 = IN.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float pixelWidth23 =  1.0f / _Pixels;
				float pixelHeight23 = 1.0f / _Pixels;
				half2 pixelateduv23 = half2((int)(texCoord22.x / pixelWidth23) * pixelWidth23, (int)(texCoord22.y / pixelHeight23) * pixelHeight23);
				float2 lerpResult30 = lerp( texCoord22 , pixelateduv23 , (( _Pixelson )?( 1.0 ):( 0.0 )));
				float4 tex2DNode2 = tex2D( _AlbedoTextura, lerpResult30 );
				float4 appendResult18 = (float4(_ColorTexture.r , _ColorTexture.g , _ColorTexture.b , ( tex2DNode2 * IN.color.a ).r));
				float grayscale15 = Luminance(tex2DNode2.rgb);
				float4 temp_cast_2 = (grayscale15).xxxx;
				float4 lerpResult16 = lerp( tex2DNode2 , temp_cast_2 , (( _GrayscaleOn )?( 1.0 ):( 0.0 )));
				float4 lerpResult4 = lerp( float4( 0,0,0,0 ) , ( appendResult18 * lerpResult16 ) , _OpacityValue);
				
				fixed4 c = lerpResult4;
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18900
0;588;1450;411;1745.773;-60.43567;1;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;22;-1330.299,36.96724;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;24;-1390.305,196.826;Inherit;False;Property;_Pixels;Pixels;4;0;Create;True;0;0;0;False;0;False;24;150000;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;31;-1221.891,303.0878;Inherit;False;Property;_Pixelson;Pixels on;5;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCPixelate;23;-1191.927,184.2712;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;30;-1013.891,1.087799;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;2;-851.4046,2.40177;Inherit;True;Property;_AlbedoTextura;AlbedoTextura;1;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;None;0e6dbe24d642a804fbeff1c00a3d034e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;19;-763.7526,296.1928;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-464.8365,176.9776;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCGrayscale;15;-231.6655,84.51642;Inherit;False;0;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;8;-467.9474,-383.7654;Inherit;False;Property;_ColorTexture;Color Texture;0;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,1;1.844303,1.844303,1.844303,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ToggleSwitchNode;26;-192.0624,166.7269;Inherit;False;Property;_GrayscaleOn;Grayscale On;3;0;Create;True;0;0;0;False;0;False;0;True;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;18;-158.0759,-223.1788;Inherit;False;COLOR;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;16;12.68517,9.806572;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;17;80.92407,133.8212;Inherit;False;Property;_OpacityValue;Opacity Value;2;0;Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;232.2384,-39.57711;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;4;411.6163,-47.91064;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;21;639.442,-46.68576;Float;False;True;-1;2;ASEMaterialInspector;0;6;Pixel Particle Shader;0f8ba0101102bb14ebf021ddadce9b49;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;False;True;3;1;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;-1;False;False;False;False;False;False;False;False;False;False;False;True;2;False;-1;False;False;True;5;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;CanUseSpriteAtlas=True;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;23;0;22;0
WireConnection;23;1;24;0
WireConnection;23;2;24;0
WireConnection;30;0;22;0
WireConnection;30;1;23;0
WireConnection;30;2;31;0
WireConnection;2;1;30;0
WireConnection;29;0;2;0
WireConnection;29;1;19;4
WireConnection;15;0;2;0
WireConnection;18;0;8;1
WireConnection;18;1;8;2
WireConnection;18;2;8;3
WireConnection;18;3;29;0
WireConnection;16;0;2;0
WireConnection;16;1;15;0
WireConnection;16;2;26;0
WireConnection;7;0;18;0
WireConnection;7;1;16;0
WireConnection;4;1;7;0
WireConnection;4;2;17;0
WireConnection;21;0;4;0
ASEEND*/
//CHKSM=18067FF2A8A3CF4CC7C788CC6D8B96AFB7FA6B8C