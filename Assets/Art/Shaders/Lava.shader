// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Lava"
{
	Properties
	{
		_EdgeLength ( "Edge length", Range( 2, 50 ) ) = 50
		[NoScaleOffset]_AlbedoTexture("Albedo Texture", 2D) = "white" {}
		[HDR]_Color2("Color 2", Color) = (0,0,0,1)
		[HDR]_Color1("Color 1", Color) = (0,0,0,1)
		_SpeedX("Speed X", Float) = 0
		_SpeedY("Speed Y", Float) = 0
		_GrayscalePower("GrayscalePower", Float) = 0
		_Timescale("Timescale", Float) = 0
		_Float0("Float 0", Float) = 0
		_PreSinMultiplier("PreSinMultiplier", Float) = 0
		_OffsetTiling("OffsetTiling", Float) = 1
		_FlowmapTime("FlowmapTime", Float) = 0
		_VertexOffsetNoise("VertexOffsetNoise", 2D) = "white" {}
		_OffsetIntensity("OffsetIntensity", Float) = 0
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "Tessellation.cginc"
		#pragma target 4.6
		#pragma surface surf Unlit alpha:fade keepalpha addshadow fullforwardshadows vertex:vertexDataFunc tessellate:tessFunction 
		struct Input
		{
			float3 worldPos;
		};

		uniform sampler2D _VertexOffsetNoise;
		uniform float _Timescale;
		uniform float _SpeedX;
		uniform float _SpeedY;
		uniform float _OffsetTiling;
		uniform float _OffsetIntensity;
		uniform float4 _Color1;
		uniform float4 _Color2;
		uniform sampler2D _AlbedoTexture;
		uniform float _Float0;
		uniform float _PreSinMultiplier;
		uniform float _FlowmapTime;
		uniform float _GrayscalePower;
		uniform float _EdgeLength;

		float4 tessFunction( appdata_full v0, appdata_full v1, appdata_full v2 )
		{
			return UnityEdgeLengthBasedTess (v0.vertex, v1.vertex, v2.vertex, _EdgeLength);
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float mulTime74 = _Time.y * _Timescale;
			float2 appendResult56 = (float2(_SpeedX , _SpeedY));
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float2 appendResult79 = (float2(ase_worldPos.x , ase_worldPos.z));
			float2 panner57 = ( mulTime74 * appendResult56 + ( appendResult79 / _OffsetTiling ));
			float3 ase_vertexNormal = v.normal.xyz;
			v.vertex.xyz += ( tex2Dlod( _VertexOffsetNoise, float4( panner57, 0, 0.0) ).r * ase_vertexNormal * _OffsetIntensity );
			v.vertex.w = 1;
		}

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float3 ase_worldPos = i.worldPos;
			float2 appendResult104 = (float2(ase_worldPos.x , ase_worldPos.z));
			float mulTime90 = _Time.y * _FlowmapTime;
			float saferPower61 = max( (0.0 + (sin( ( ( tex2D( _AlbedoTexture, ( appendResult104 / _Float0 ) ).r * _PreSinMultiplier ) + mulTime90 ) ) - -1.0) * (1.0 - 0.0) / (1.0 - -1.0)) , 0.0001 );
			float4 lerpResult4 = lerp( _Color1 , _Color2 , saturate( pow( saferPower61 , _GrayscalePower ) ));
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
912;81;542;671;3133.107;306.2747;2.294031;False;False
Node;AmplifyShaderEditor.WorldPosInputsNode;103;-2367.364,-207.5983;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;102;-2113.364,-25.59808;Inherit;False;Property;_Float0;Float 0;12;0;Create;True;0;0;0;False;0;False;0;150;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;104;-2131.364,-199.5983;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;105;-1912.507,-205.2001;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;88;-1559.659,-58.31372;Inherit;False;Property;_PreSinMultiplier;PreSinMultiplier;13;0;Create;True;0;0;0;False;0;False;0;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;60;-1675.528,-269.2469;Inherit;True;Property;_AlbedoTexture;Albedo Texture;5;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;None;3db2c86478b05884f9941f5d081ca46e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;93;-1606.762,26.16794;Inherit;False;Property;_FlowmapTime;FlowmapTime;15;0;Create;True;0;0;0;False;0;False;0;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;87;-1289.976,-204.3369;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;90;-1401.762,20.16794;Inherit;False;1;0;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;77;-2018.592,293.4009;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleAddOpNode;92;-1123.762,-31.83206;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;75;-1520.118,723.1464;Inherit;False;Property;_Timescale;Timescale;11;0;Create;True;0;0;0;False;0;False;0;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-1523.976,566.3781;Inherit;False;Property;_SpeedX;Speed X;8;0;Create;True;0;0;0;False;0;False;0;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;79;-1782.591,301.4009;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-1533.076,628.8474;Inherit;False;Property;_SpeedY;Speed Y;9;0;Create;True;0;0;0;False;0;False;0;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;106;-1849.73,433.0834;Inherit;False;Property;_OffsetTiling;OffsetTiling;14;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;84;-946.8545,-41.31927;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;86;-774.7546,-55.71928;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;64;-809.6084,-326.2487;Inherit;False;Property;_GrayscalePower;GrayscalePower;10;0;Create;True;0;0;0;False;0;False;0;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;74;-1383.527,725.6556;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;82;-1563.735,295.7991;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;56;-1375.498,568.0779;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PowerNode;61;-516.6365,-166.4153;Inherit;True;True;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;57;-1069.888,339.5876;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;99;-747.9984,573.5658;Inherit;False;Property;_OffsetIntensity;OffsetIntensity;18;0;Create;True;0;0;0;False;0;False;0;0.75;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;97;-741.4202,404.5317;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;6;-410.2808,-430.7211;Inherit;False;Property;_Color2;Color 2;6;1;[HDR];Create;True;0;0;0;False;0;False;0,0,0,1;6.422235,2.040004,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;94;-253.6419,-193.6918;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;5;-417.7968,-599.0927;Inherit;False;Property;_Color1;Color 1;7;1;[HDR];Create;True;0;0;0;False;0;False;0,0,0,1;0.759,0.123054,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;95;-840.2124,189.4602;Inherit;True;Property;_VertexOffsetNoise;VertexOffsetNoise;16;0;Create;True;0;0;0;False;0;False;-1;None;6c18df5db11f0614d95210014b3af9e0;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;98;-471.2022,246.8322;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;4;-49.73442,-288.836;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;100;-221.9984,357.5658;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;96;-735.0319,685.2206;Inherit;False;Property;_Direction;Direction;17;0;Create;True;0;0;0;False;0;False;0,0,0;0,1,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;130.0235,-333.5891;Float;False;True;-1;6;ASEMaterialInspector;0;0;Unlit;Lava;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;True;2;50;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;0;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;104;0;103;1
WireConnection;104;1;103;3
WireConnection;105;0;104;0
WireConnection;105;1;102;0
WireConnection;60;1;105;0
WireConnection;87;0;60;1
WireConnection;87;1;88;0
WireConnection;90;0;93;0
WireConnection;92;0;87;0
WireConnection;92;1;90;0
WireConnection;79;0;77;1
WireConnection;79;1;77;3
WireConnection;84;0;92;0
WireConnection;86;0;84;0
WireConnection;74;0;75;0
WireConnection;82;0;79;0
WireConnection;82;1;106;0
WireConnection;56;0;12;0
WireConnection;56;1;24;0
WireConnection;61;0;86;0
WireConnection;61;1;64;0
WireConnection;57;0;82;0
WireConnection;57;2;56;0
WireConnection;57;1;74;0
WireConnection;94;0;61;0
WireConnection;95;1;57;0
WireConnection;98;0;95;1
WireConnection;98;1;97;0
WireConnection;98;2;99;0
WireConnection;4;0;5;0
WireConnection;4;1;6;0
WireConnection;4;2;94;0
WireConnection;0;2;4;0
WireConnection;0;11;98;0
ASEEND*/
//CHKSM=6E4F543F653793044D1F27D65F72647878C32C2C