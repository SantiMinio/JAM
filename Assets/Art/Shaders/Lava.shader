// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Lava"
{
	Properties
	{
		[NoScaleOffset]_AlbedoTexture("Albedo Texture", 2D) = "white" {}
		[NoScaleOffset]_FlowTexture("Flow Texture", 2D) = "white" {}
		[HDR]_Color2("Color 2", Color) = (0,0,0,1)
		_Float0("Float 0", Float) = 6.4
		[HDR]_Color1("Color 1", Color) = (0,0,0,1)
		_TillingAlbedo("Tilling Albedo", Vector) = (1,1,0,0)
		_SpeedX("Speed X", Float) = 0
		_SpeedY("Speed Y", Float) = 0
		_FlowwMap("Floww Map", Range( 0 , 0.05)) = 0
		_NOiseScale("NOise Scale", Float) = 6.01
		_Float1("Float 1", Range( 0 , 1)) = 0
		_Pixels("Pixels", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _Color1;
		uniform float4 _Color2;
		uniform sampler2D _AlbedoTexture;
		uniform float _SpeedX;
		uniform float _SpeedY;
		uniform float2 _TillingAlbedo;
		uniform float _Pixels;
		uniform sampler2D _FlowTexture;
		uniform float _NOiseScale;
		uniform float _Float1;
		uniform float _FlowwMap;
		uniform float _Float0;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 appendResult25 = (float2(_SpeedX , _SpeedY));
			float2 uv_TexCoord7 = i.uv_texcoord * _TillingAlbedo;
			float pixelWidth49 =  1.0f / _Pixels;
			float pixelHeight49 = 1.0f / _Pixels;
			half2 pixelateduv49 = half2((int)(uv_TexCoord7.x / pixelWidth49) * pixelWidth49, (int)(uv_TexCoord7.y / pixelHeight49) * pixelHeight49);
			float mulTime40 = _Time.y * 0.3;
			float2 uv_TexCoord46 = i.uv_texcoord * float2( -0.33,-0.32 );
			float2 panner45 = ( mulTime40 * float2( 0.008,0 ) + uv_TexCoord46);
			float simplePerlin2D44 = snoise( panner45*_NOiseScale );
			simplePerlin2D44 = simplePerlin2D44*0.5 + 0.5;
			float saferPower47 = max( simplePerlin2D44 , 0.0001 );
			float2 temp_cast_1 = (pow( saferPower47 , _Float1 )).xx;
			float4 lerpResult22 = lerp( float4( pixelateduv49, 0.0 , 0.0 ) , tex2D( _FlowTexture, temp_cast_1 ) , _FlowwMap);
			float2 panner9 = ( _Time.y * appendResult25 + lerpResult22.rg);
			float4 saferPower2 = max( tex2D( _AlbedoTexture, panner9 ) , 0.0001 );
			float4 temp_cast_3 = (_Float0).xxxx;
			float4 lerpResult4 = lerp( _Color1 , _Color2 , pow( saferPower2 , temp_cast_3 ));
			o.Emission = lerpResult4.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
0;616;1643;383;1486.8;-143.1909;1.376103;True;False
Node;AmplifyShaderEditor.RangedFloatNode;43;-3006.519,533.2982;Inherit;False;Constant;_Float3;Float 3;9;0;Create;True;0;0;0;False;0;False;0.3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;46;-2689.864,610.8748;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;-0.33,-0.32;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;40;-2743.176,412.6016;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;45;-2443.111,568.4241;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.008,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;39;-2362.939,721.7444;Inherit;False;Property;_NOiseScale;NOise Scale;9;0;Create;True;0;0;0;False;0;False;6.01;5.38;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;44;-2188.71,613.2239;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;48;-2222.504,416.287;Inherit;False;Property;_Float1;Float 1;10;0;Create;True;0;0;0;False;0;False;0;0.7625506;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;8;-2461.296,-47.79335;Inherit;False;Property;_TillingAlbedo;Tilling Albedo;5;0;Create;True;0;0;0;False;0;False;1,1;3,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;7;-2273.387,-66.70171;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;50;-2196.416,76.62222;Inherit;False;Property;_Pixels;Pixels;11;0;Create;True;0;0;0;False;0;False;0;100;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;47;-1905.421,420.4049;Inherit;False;True;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-1427,177;Inherit;False;Property;_SpeedX;Speed X;6;0;Create;True;0;0;0;False;0;False;0;0.03;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-1719.341,296.0339;Inherit;False;Property;_FlowwMap;Floww Map;8;0;Create;True;0;0;0;False;0;False;0;0.03042382;0;0.05;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-1421.894,248.8251;Inherit;False;Property;_SpeedY;Speed Y;7;0;Create;True;0;0;0;False;0;False;0;0.03;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;20;-1753.18,108.1921;Inherit;True;Property;_FlowTexture;Flow Texture;1;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;None;ddeaa52be50776e4bba0e2cb1dba44f5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCPixelate;49;-1971.416,-25.37778;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;22;-1427.95,51.78085;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleTimeNode;13;-1256.926,283.8706;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;25;-1242.894,165.8251;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;9;-1044.926,49.87057;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;1;-841.2083,16.65099;Inherit;True;Property;_AlbedoTexture;Albedo Texture;0;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;None;198c8572a7366de4880f7b2a2e7c516d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;3;-728.8178,224.8416;Inherit;False;Property;_Float0;Float 0;3;0;Create;True;0;0;0;False;0;False;6.4;4.12;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;2;-510.6178,97.84155;Inherit;True;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;6;-429.2808,-263.2212;Inherit;False;Property;_Color2;Color 2;2;1;[HDR];Create;True;0;0;0;False;0;False;0,0,0,1;7.464264,2.371002,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;5;-401.7968,-437.5927;Inherit;False;Property;_Color1;Color 1;4;1;[HDR];Create;True;0;0;0;False;0;False;0,0,0,1;2.639016,0.1552362,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;4;27.67617,13.46331;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;451.2497,51.11712;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;Lava;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;40;0;43;0
WireConnection;45;0;46;0
WireConnection;45;1;40;0
WireConnection;44;0;45;0
WireConnection;44;1;39;0
WireConnection;7;0;8;0
WireConnection;47;0;44;0
WireConnection;47;1;48;0
WireConnection;20;1;47;0
WireConnection;49;0;7;0
WireConnection;49;1;50;0
WireConnection;49;2;50;0
WireConnection;22;0;49;0
WireConnection;22;1;20;0
WireConnection;22;2;23;0
WireConnection;25;0;12;0
WireConnection;25;1;24;0
WireConnection;9;0;22;0
WireConnection;9;2;25;0
WireConnection;9;1;13;0
WireConnection;1;1;9;0
WireConnection;2;0;1;0
WireConnection;2;1;3;0
WireConnection;4;0;5;0
WireConnection;4;1;6;0
WireConnection;4;2;2;0
WireConnection;0;2;4;0
ASEEND*/
//CHKSM=69512302C239065FBD38862ABA12730F9C412C06