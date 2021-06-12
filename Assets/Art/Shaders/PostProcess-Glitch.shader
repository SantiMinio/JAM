// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "My Shaders/PostProcess/Glitch"
{
	Properties
	{
		_MainTex ( "Screen", 2D ) = "black" {}
		[NoScaleOffset]_Texture("Texture", 2D) = "white" {}
		[NoScaleOffset]_Texture1("Texture1", 2D) = "white" {}
		_PixelsY("PixelsY", Float) = 500
		_IntensityNormal("Intensity Normal", Range( 0 , 0.5)) = 0.02
		_TextureSample1("Texture Sample 1", 2D) = "bump" {}
		_SpeedGlitch("Speed Glitch", Float) = 0
		_SpeedLines("Speed Lines", Float) = 0
		_PixelsX("PixelsX", Float) = 500
		_Numberoflines("Number of lines", Range( 0 , 1)) = 0.3401067
		_OpacityLines("Opacity Lines", Range( 0 , 1)) = 0
		_GrayscaleOpacity("Grayscale Opacity", Range( 0 , 1)) = 0
		_GrayscaleColor("GrayscaleColor", Color) = (0,0,0,0)
		_PixelOpacity("Pixel Opacity", Range( 0 , 1)) = 1

	}

	SubShader
	{
		
		
		ZTest Always
		Cull Off
		ZWrite Off

		
		Pass
		{ 
			CGPROGRAM 

			

			#pragma vertex vert_img_custom 
			#pragma fragment frag
			#pragma target 3.0
			#include "UnityCG.cginc"
			#include "UnityStandardUtils.cginc"
			#include "UnityShaderVariables.cginc"


			struct appdata_img_custom
			{
				float4 vertex : POSITION;
				half2 texcoord : TEXCOORD0;
				
			};

			struct v2f_img_custom
			{
				float4 pos : SV_POSITION;
				half2 uv   : TEXCOORD0;
				half2 stereoUV : TEXCOORD2;
		#if UNITY_UV_STARTS_AT_TOP
				half4 uv2 : TEXCOORD1;
				half4 stereoUV2 : TEXCOORD3;
		#endif
				float4 ase_texcoord4 : TEXCOORD4;
			};

			uniform sampler2D _MainTex;
			uniform half4 _MainTex_TexelSize;
			uniform half4 _MainTex_ST;
			
			uniform float _IntensityNormal;
			uniform sampler2D _TextureSample1;
			uniform sampler2D _Texture;
			uniform float _SpeedGlitch;
			uniform float _PixelsX;
			uniform float _PixelsY;
			uniform float _PixelOpacity;
			uniform float4 _GrayscaleColor;
			uniform float _GrayscaleOpacity;
			uniform sampler2D _Texture1;
			uniform float _SpeedLines;
			uniform float _Numberoflines;
			uniform float _OpacityLines;


			v2f_img_custom vert_img_custom ( appdata_img_custom v  )
			{
				v2f_img_custom o;
				float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord4 = screenPos;
				
				o.pos = UnityObjectToClipPos( v.vertex );
				o.uv = float4( v.texcoord.xy, 1, 1 );

				#if UNITY_UV_STARTS_AT_TOP
					o.uv2 = float4( v.texcoord.xy, 1, 1 );
					o.stereoUV2 = UnityStereoScreenSpaceUVAdjust ( o.uv2, _MainTex_ST );

					if ( _MainTex_TexelSize.y < 0.0 )
						o.uv.y = 1.0 - o.uv.y;
				#endif
				o.stereoUV = UnityStereoScreenSpaceUVAdjust ( o.uv, _MainTex_ST );
				return o;
			}

			half4 frag ( v2f_img_custom i ) : SV_Target
			{
				#ifdef UNITY_UV_STARTS_AT_TOP
					half2 uv = i.uv2;
					half2 stereoUV = i.stereoUV2;
				#else
					half2 uv = i.uv;
					half2 stereoUV = i.stereoUV;
				#endif	
				
				half4 finalColor;

				// ase common template code
				float2 temp_cast_0 = (_SpeedGlitch).xx;
				float2 uv054 = i.uv.xy * float2( 1,1 ) + float2( 0,0 );
				float4 color53 = IsGammaSpace() ? float4(0,1,0,0) : float4(0,1,0,0);
				float2 panner50 = ( 1.0 * _Time.y * temp_cast_0 + ( float4( uv054, 0.0 , 0.0 ) * color53 ).rg);
				float pixelWidth57 =  1.0f / _PixelsX;
				float pixelHeight57 = 1.0f / _PixelsY;
				half2 pixelateduv57 = half2((int)(panner50.x / pixelWidth57) * pixelWidth57, (int)(panner50.y / pixelHeight57) * pixelHeight57);
				float2 lerpResult90 = lerp( panner50 , pixelateduv57 , _PixelOpacity);
				float4 screenPos = i.ase_texcoord4;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float4 GlitchDistortiong94 = ( float4( UnpackScaleNormal( tex2D( _TextureSample1, tex2D( _Texture, lerpResult90 ).rg ), _IntensityNormal ) , 0.0 ) + ase_screenPosNorm );
				float4 tex2DNode1 = tex2D( _MainTex, GlitchDistortiong94.xy );
				float grayscale4 = Luminance(tex2DNode1.rgb);
				float4 lerpResult75 = lerp( tex2DNode1 , ( grayscale4 * _GrayscaleColor ) , _GrayscaleOpacity);
				float2 temp_cast_7 = (_SpeedLines).xx;
				float2 uv081 = i.uv.xy * float2( 1,1 ) + float2( 0,0 );
				float4 color82 = IsGammaSpace() ? float4(0,1,0,0) : float4(0,1,0,0);
				float2 panner86 = ( 1.0 * _Time.y * temp_cast_7 + ( float4( uv081, 0.0 , 0.0 ) * color82 ).rg);
				float pixelWidth87 =  1.0f / _PixelsX;
				float pixelHeight87 = 1.0f / _PixelsY;
				half2 pixelateduv87 = half2((int)(panner86.x / pixelWidth87) * pixelWidth87, (int)(panner86.y / pixelHeight87) * pixelHeight87);
				float4 Lines80 = tex2D( _Texture1, pixelateduv87 );
				float4 temp_cast_10 = (_Numberoflines).xxxx;
				float4 temp_output_71_0 = ( 1.0 - saturate( step( Lines80 , temp_cast_10 ) ) );
				float4 lerpResult68 = lerp( temp_output_71_0 , lerpResult75 , temp_output_71_0);
				float4 lerpResult73 = lerp( lerpResult75 , lerpResult68 , _OpacityLines);
				

				finalColor = lerpResult73;

				return finalColor;
			} 
			ENDCG 
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=17200
0;414;835;274;4568.664;189.0256;5.682858;True;False
Node;AmplifyShaderEditor.CommentaryNode;93;-3881.77,225.379;Inherit;False;2468.84;724.9059;;14;39;45;2;8;90;91;57;50;52;60;53;54;48;94;Glitch Distortion;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;54;-3831.77,275.48;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;53;-3829.646,405.1829;Inherit;False;Constant;_Color1;Color 1;5;0;Create;True;0;0;False;0;0,1,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;60;-3562.356,405.6079;Inherit;False;Property;_SpeedGlitch;Speed Glitch;5;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;52;-3545.604,279.2148;Inherit;False;2;2;0;FLOAT2;0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PannerNode;50;-3360.457,275.379;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.01,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;49;-3258.963,-254.6042;Inherit;False;Property;_PixelsX;PixelsX;7;0;Create;True;0;0;False;0;500;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-3261.7,-158.045;Inherit;False;Property;_PixelsY;PixelsY;2;0;Create;True;0;0;False;0;500;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;92;-3534.532,-835.548;Inherit;False;1684.527;386.7023;;8;82;81;83;84;86;87;88;80;Lines;1,1,1,1;0;0
Node;AmplifyShaderEditor.TFHCPixelate;57;-3041.001,398.5792;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT;500;False;2;FLOAT;500;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;91;-3133.693,538.4255;Inherit;False;Property;_PixelOpacity;Pixel Opacity;12;0;Create;True;0;0;False;0;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;90;-2790.228,328.7065;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;82;-3482.408,-655.8458;Inherit;False;Constant;_Color0;Color 0;5;0;Create;True;0;0;False;0;0,1,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;81;-3484.532,-785.548;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;84;-3198.368,-781.8134;Inherit;False;2;2;0;FLOAT2;0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;2;-2600.921,352.2143;Inherit;True;Property;_Texture;Texture;0;1;[NoScaleOffset];Create;True;0;0;False;0;-1;23cabce06019d8f44a5d3e0b2150d565;23cabce06019d8f44a5d3e0b2150d565;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;83;-3215.119,-655.4207;Inherit;False;Property;_SpeedLines;Speed Lines;6;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-2540.379,615.0278;Inherit;False;Property;_IntensityNormal;Intensity Normal;3;0;Create;True;0;0;False;0;0.02;0;0;0.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;39;-2221.052,473.7373;Inherit;True;Property;_TextureSample1;Texture Sample 1;4;0;Create;True;0;0;False;0;-1;e08c295755c0885479ad19f518286ff2;e08c295755c0885479ad19f518286ff2;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;86;-2957.399,-777.9874;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.01,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;45;-2115.579,775.3279;Inherit;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCPixelate;87;-2739.572,-762.3251;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT;500;False;2;FLOAT;500;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;48;-1837.635,584.9728;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SamplerNode;88;-2505.51,-727.6254;Inherit;True;Property;_Texture1;Texture1;1;1;[NoScaleOffset];Create;True;0;0;False;0;-1;23cabce06019d8f44a5d3e0b2150d565;23cabce06019d8f44a5d3e0b2150d565;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;94;-1659.014,570.9114;Float;False;GlitchDistortiong;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;3;-1480.274,-215.9471;Inherit;False;0;0;_MainTex;Shader;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;95;-1666.152,-124.3053;Inherit;False;94;GlitchDistortiong;1;0;OBJECT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;80;-2093.005,-663.3702;Inherit;False;Lines;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;70;-1266.376,-301.7394;Inherit;False;Property;_Numberoflines;Number of lines;8;0;Create;True;0;0;False;0;0.3401067;0.3401067;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-1290.879,-217.5011;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;89;-1177.548,-403.0177;Inherit;False;80;Lines;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCGrayscale;4;-966.6183,-141.4115;Inherit;False;0;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;69;-876.7303,-372.0318;Inherit;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;79;-984.4453,58.64694;Inherit;False;Property;_GrayscaleColor;GrayscaleColor;11;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;72;-710.4867,-350.525;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;77;-1016.841,-33.64091;Inherit;False;Property;_GrayscaleOpacity;Grayscale Opacity;10;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;78;-674.3234,-18.61039;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;71;-536.1403,-323.6157;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;75;-588.1903,-212.6172;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;68;-259.2627,-346.7628;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;74;-365.8247,-145.3735;Inherit;False;Property;_OpacityLines;Opacity Lines;9;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;73;-60.95105,-247.4483;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;247.9658,-222.0019;Float;False;True;2;ASEMaterialInspector;0;7;My Shaders/PostProcess/Glitch;c71b220b631b6344493ea3cf87110c93;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;1;False;False;False;True;2;False;-1;False;False;True;2;False;-1;True;7;False;-1;False;True;0;False;0;False;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;0;1;True;False;0
WireConnection;52;0;54;0
WireConnection;52;1;53;0
WireConnection;50;0;52;0
WireConnection;50;2;60;0
WireConnection;57;0;50;0
WireConnection;57;1;49;0
WireConnection;57;2;7;0
WireConnection;90;0;50;0
WireConnection;90;1;57;0
WireConnection;90;2;91;0
WireConnection;84;0;81;0
WireConnection;84;1;82;0
WireConnection;2;1;90;0
WireConnection;39;1;2;0
WireConnection;39;5;8;0
WireConnection;86;0;84;0
WireConnection;86;2;83;0
WireConnection;87;0;86;0
WireConnection;87;1;49;0
WireConnection;87;2;7;0
WireConnection;48;0;39;0
WireConnection;48;1;45;0
WireConnection;88;1;87;0
WireConnection;94;0;48;0
WireConnection;80;0;88;0
WireConnection;1;0;3;0
WireConnection;1;1;95;0
WireConnection;4;0;1;0
WireConnection;69;0;89;0
WireConnection;69;1;70;0
WireConnection;72;0;69;0
WireConnection;78;0;4;0
WireConnection;78;1;79;0
WireConnection;71;0;72;0
WireConnection;75;0;1;0
WireConnection;75;1;78;0
WireConnection;75;2;77;0
WireConnection;68;0;71;0
WireConnection;68;1;75;0
WireConnection;68;2;71;0
WireConnection;73;0;75;0
WireConnection;73;1;68;0
WireConnection;73;2;74;0
WireConnection;0;0;73;0
ASEEND*/
//CHKSM=5BCA5674716C7724DBBD064E0BC14FB8AA6AD764