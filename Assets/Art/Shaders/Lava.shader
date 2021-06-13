// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Lava"
{
	Properties
	{
		[NoScaleOffset]_AlbedoTexture("Albedo Texture", 2D) = "white" {}
		[NoScaleOffset]_AlbedoTexture1("Albedo Texture", 2D) = "white" {}
		[NoScaleOffset]_FlowTexture("Flow Texture", 2D) = "white" {}
		[HDR]_Color2("Color 2", Color) = (0,0,0,1)
		_Float0("Float 0", Float) = 6.4
		[HDR]_Color1("Color 1", Color) = (0,0,0,1)
		_TillingAlbedo("Tilling Albedo", Vector) = (1,1,0,0)
		_SpeedX("Speed X", Float) = 0
		_SpeedY("Speed Y", Float) = 0
		_FlowwMap("Floww Map", Range( 0 , 1)) = 0
		_Pixels("Pixels", Float) = 0
		_Depth("Depth", Range( 0 , 10)) = 0
		_Float2("Float 2", Float) = 0
		[HDR]_DepthColor("Depth Color", Color) = (5.278032,0.2276798,0,1)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit alpha:fade keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
		};

		uniform float4 _DepthColor;
		uniform float4 _Color1;
		uniform float4 _Color2;
		uniform sampler2D _AlbedoTexture1;
		uniform float _SpeedX;
		uniform float _SpeedY;
		uniform float2 _TillingAlbedo;
		uniform float _Pixels;
		uniform float _Float2;
		uniform sampler2D _AlbedoTexture;
		uniform sampler2D _FlowTexture;
		uniform float _FlowwMap;
		uniform float _Float0;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _Depth;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 appendResult56 = (float2(-_SpeedX , -_SpeedY));
			float2 uv_TexCoord7 = i.uv_texcoord * _TillingAlbedo;
			float pixelWidth49 =  1.0f / _Pixels;
			float pixelHeight49 = 1.0f / _Pixels;
			half2 pixelateduv49 = half2((int)(uv_TexCoord7.x / pixelWidth49) * pixelWidth49, (int)(uv_TexCoord7.y / pixelHeight49) * pixelHeight49);
			float2 panner57 = ( _Time.y * appendResult56 + pixelateduv49);
			float4 saferPower61 = max( tex2D( _AlbedoTexture1, panner57 ) , 0.0001 );
			float4 temp_cast_0 = (_Float2).xxxx;
			float2 appendResult25 = (float2(_SpeedX , _SpeedY));
			float4 lerpResult22 = lerp( float4( pixelateduv49, 0.0 , 0.0 ) , tex2D( _FlowTexture, pixelateduv49 ) , _FlowwMap);
			float2 panner9 = ( _Time.y * appendResult25 + lerpResult22.rg);
			float4 saferPower2 = max( tex2D( _AlbedoTexture, panner9 ) , 0.0001 );
			float4 temp_cast_3 = (_Float0).xxxx;
			float4 lerpResult4 = lerp( _Color1 , _Color2 , ( pow( saferPower61 , temp_cast_0 ) * pow( saferPower2 , temp_cast_3 ) ));
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float screenDepth52 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth52 = abs( ( screenDepth52 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _Depth ) );
			float4 lerpResult51 = lerp( _DepthColor , lerpResult4 , saturate( distanceDepth52 ));
			o.Emission = lerpResult51.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
307;503;946;496;1006.556;132.825;2.016212;True;False
Node;AmplifyShaderEditor.Vector2Node;8;-2461.296,-47.79335;Inherit;False;Property;_TillingAlbedo;Tilling Albedo;6;0;Create;True;0;0;0;False;0;False;1,1;3,13.48;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;7;-2273.387,-66.70171;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;50;-2196.416,76.62222;Inherit;False;Property;_Pixels;Pixels;12;0;Create;True;0;0;0;False;0;False;0;100;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-1421.894,248.8251;Inherit;False;Property;_SpeedY;Speed Y;8;0;Create;True;0;0;0;False;0;False;0;0.03;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-1427,177;Inherit;False;Property;_SpeedX;Speed X;7;0;Create;True;0;0;0;False;0;False;0;0.03;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCPixelate;49;-1971.416,-25.37778;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NegateNode;58;-1505.917,-241.5441;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;20;-1753.18,108.1921;Inherit;True;Property;_FlowTexture;Flow Texture;2;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;None;ddeaa52be50776e4bba0e2cb1dba44f5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NegateNode;59;-1447.415,-167.4439;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-1719.341,296.0339;Inherit;False;Property;_FlowwMap;Floww Map;9;0;Create;True;0;0;0;False;0;False;0;0.064;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;22;-1427.95,51.78085;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleTimeNode;13;-1256.926,283.8706;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;56;-1282.316,-264.9442;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;25;-1242.894,165.8251;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;57;-1108.115,-290.944;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;9;-1044.926,49.87057;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;60;-862.9308,-245.5915;Inherit;True;Property;_AlbedoTexture1;Albedo Texture;1;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;None;198c8572a7366de4880f7b2a2e7c516d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;3;-799.0181,301.5413;Inherit;False;Property;_Float0;Float 0;4;0;Create;True;0;0;0;False;0;False;6.4;4.12;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-841.2083,16.65099;Inherit;True;Property;_AlbedoTexture;Albedo Texture;0;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;None;198c8572a7366de4880f7b2a2e7c516d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;64;-849.6084,-392.2487;Inherit;False;Property;_Float2;Float 2;14;0;Create;True;0;0;0;False;0;False;0;1.93;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;54;-845.3167,498.8063;Inherit;False;Property;_Depth;Depth;13;0;Create;True;0;0;0;False;0;False;0;0.94;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;2;-483.3185,132.6416;Inherit;True;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;61;-514.1362,-161.4153;Inherit;True;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;5;-245.7968,-444.0927;Inherit;False;Property;_Color1;Color 1;5;1;[HDR];Create;True;0;0;0;False;0;False;0,0,0,1;0.245283,0,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;71;-213.8552,10.07825;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.DepthFade;52;-517.7166,452.0066;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;6;-273.2808,-269.7211;Inherit;False;Property;_Color2;Color 2;3;1;[HDR];Create;True;0;0;0;False;0;False;0,0,0,1;7.464264,2.371002,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;72;-113.9357,243.0094;Inherit;False;Property;_DepthColor;Depth Color;15;1;[HDR];Create;True;0;0;0;False;0;False;5.278032,0.2276798,0,1;5.278032,0.2276798,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;4;184.6762,-35.53669;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;53;-177.1167,427.3064;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;51;166.0861,383.1061;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;39;-2362.939,721.7444;Inherit;False;Property;_NOiseScale;NOise Scale;10;0;Create;True;0;0;0;False;0;False;6.01;5.38;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;43;-3006.519,533.2982;Inherit;False;Constant;_Float3;Float 3;9;0;Create;True;0;0;0;False;0;False;10.72;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;47;-1905.421,420.4049;Inherit;True;True;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;45;-2443.111,568.4241;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.008,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleTimeNode;40;-2743.176,412.6016;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;46;-2689.864,610.8748;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;-0.33,-0.32;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NoiseGeneratorNode;44;-2188.71,613.2239;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;48;-2222.504,416.287;Inherit;False;Property;_Float1;Float 1;11;0;Create;True;0;0;0;False;0;False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;451.2497,51.11712;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;Lava;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;7;0;8;0
WireConnection;49;0;7;0
WireConnection;49;1;50;0
WireConnection;49;2;50;0
WireConnection;58;0;12;0
WireConnection;20;1;49;0
WireConnection;59;0;24;0
WireConnection;22;0;49;0
WireConnection;22;1;20;0
WireConnection;22;2;23;0
WireConnection;56;0;58;0
WireConnection;56;1;59;0
WireConnection;25;0;12;0
WireConnection;25;1;24;0
WireConnection;57;0;49;0
WireConnection;57;2;56;0
WireConnection;57;1;13;0
WireConnection;9;0;22;0
WireConnection;9;2;25;0
WireConnection;9;1;13;0
WireConnection;60;1;57;0
WireConnection;1;1;9;0
WireConnection;2;0;1;0
WireConnection;2;1;3;0
WireConnection;61;0;60;0
WireConnection;61;1;64;0
WireConnection;71;0;61;0
WireConnection;71;1;2;0
WireConnection;52;0;54;0
WireConnection;4;0;5;0
WireConnection;4;1;6;0
WireConnection;4;2;71;0
WireConnection;53;0;52;0
WireConnection;51;0;72;0
WireConnection;51;1;4;0
WireConnection;51;2;53;0
WireConnection;47;0;44;0
WireConnection;47;1;48;0
WireConnection;45;0;46;0
WireConnection;45;1;40;0
WireConnection;40;0;43;0
WireConnection;44;0;45;0
WireConnection;44;1;39;0
WireConnection;0;2;51;0
ASEEND*/
//CHKSM=E808A01A0CF778F6B8286CA72CC5FEE0C8C59403