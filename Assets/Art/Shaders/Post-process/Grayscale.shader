// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Grayscale"
{
	Properties
	{
		_MainTex ( "Screen", 2D ) = "black" {}
		_Grayscale("Grayscale", Range( 0 , 1)) = 1
		_MasOcuro("MasOcuro", Range( 0 , 1)) = 0
		_Pixeles("Pixeles", Float) = 30
		_ScaleNoise("ScaleNoise", Float) = 8.09
		_Effect("Effect", Range( 0 , 1)) = 1
		_Speed("Speed", Float) = 1
		_Corazon_puto("Corazon_puto", 2D) = "white" {}
		_Float2("Float 2", Float) = 45
		_Float3("Float 3", Float) = 0
		_HeartEffect("HeartEffect", Range( 0 , 1)) = 0

	}

	SubShader
	{
		LOD 0

		
		
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
			
			uniform float _MasOcuro;
			uniform float _Grayscale;
			uniform float _Speed;
			uniform float _Pixeles;
			uniform float _ScaleNoise;
			uniform float _Effect;
			uniform sampler2D _Corazon_puto;
			uniform float _Float3;
			uniform float _Float2;
			uniform float _HeartEffect;
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
			


			v2f_img_custom vert_img_custom ( appdata_img_custom v  )
			{
				v2f_img_custom o;
				o.ase_texcoord4 = v.vertex;
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
				float2 texCoord6 = i.uv.xy * float2( 1,1 ) + float2( 0,0 );
				float4 temp_output_9_0 = ( tex2D( _MainTex, texCoord6 ) / (1.0 + (_MasOcuro - 0.0) * (3.0 - 1.0) / (1.0 - 0.0)) );
				float grayscale3 = Luminance(temp_output_9_0.rgb);
				float4 temp_cast_1 = (grayscale3).xxxx;
				float4 lerpResult2 = lerp( temp_output_9_0 , temp_cast_1 , _Grayscale);
				float mulTime24 = _Time.y * _Speed;
				float2 texCoord13 = i.uv.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner21 = ( mulTime24 * float2( 0,1 ) + texCoord13);
				float pixelWidth14 =  1.0f / _Pixeles;
				float pixelHeight14 = 1.0f / _Pixeles;
				half2 pixelateduv14 = half2((int)(panner21.x / pixelWidth14) * pixelWidth14, (int)(panner21.y / pixelHeight14) * pixelHeight14);
				float simplePerlin2D12 = snoise( pixelateduv14*_ScaleNoise );
				simplePerlin2D12 = simplePerlin2D12*0.5 + 0.5;
				float lerpResult29 = lerp( 0.0 , saturate( step( simplePerlin2D12 , _Effect ) ) , ( saturate( ( i.ase_texcoord4.xyz.y + (-1.02 + (_Effect - 0.0) * (1.0 - -1.02) / (1.0 - 0.0)) ) ) * 2.0 ));
				float4 lerpResult19 = lerp( float4( 0,0,0,0 ) , saturate( lerpResult2 ) , lerpResult29);
				float2 temp_cast_2 = (_Float3).xx;
				float2 texCoord42 = i.uv.xy * temp_cast_2 + float2( 0.5,0.5 );
				float2 temp_cast_3 = (_Float2).xx;
				float2 texCoord53 = i.uv.xy * temp_cast_3 + float2( -20,-20 );
				float2 lerpResult52 = lerp( texCoord42 , texCoord53 , (0.0 + (_HeartEffect - 0.0) * (0.2 - 0.0) / (1.0 - 0.0)));
				float ifLocalVar60 = 0;
				if( 1.0 == _HeartEffect )
				ifLocalVar60 = _HeartEffect;
				float lerpResult64 = lerp( 0.0 , ( 1.0 - tex2D( _Corazon_puto, lerpResult52 ).a ) , ( 1.0 - ifLocalVar60 ));
				float4 lerpResult40 = lerp( float4( 0,0,0,0 ) , lerpResult19 , lerpResult64);
				

				finalColor = lerpResult40;

				return finalColor;
			} 
			ENDCG 
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18900
0;73;1920;928;-729.3045;-163.5517;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;23;-1407.559,596.6022;Inherit;False;Property;_Speed;Speed;5;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;13;-1269.083,437.9602;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;24;-1214.301,592.6949;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-1263.985,170.7539;Inherit;False;Property;_MasOcuro;MasOcuro;1;0;Create;True;0;0;0;False;0;False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;6;-1296.104,-16.27394;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;5;-1211.578,-87.96149;Inherit;False;0;0;_MainTex;Shader;False;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;15;-1016.69,583.1628;Inherit;False;Property;_Pixeles;Pixeles;2;0;Create;True;0;0;0;False;0;False;30;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-619.0286,847.5999;Inherit;False;Property;_Effect;Effect;4;0;Create;True;0;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;21;-1006.301,456.6949;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;47;1380.693,383.8655;Inherit;False;Constant;_Vector1;Vector 1;9;0;Create;True;0;0;0;False;0;False;0.5,0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;46;1355.776,643.1668;Inherit;False;Constant;_Vector0;Vector 0;9;0;Create;True;0;0;0;False;0;False;-20,-20;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;51;1393.693,294.8655;Inherit;False;Property;_Float3;Float 3;9;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;44;1366.602,547.1609;Inherit;False;Property;_Float2;Float 2;8;0;Create;True;0;0;0;False;0;False;45;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;49;1317.693,788.8655;Inherit;False;Property;_HeartEffect;HeartEffect;10;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;11;-892.9099,176.2816;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;1;False;4;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;25;-213.4218,699.3674;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCPixelate;14;-776.2405,480.8717;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TFHCRemapNode;32;-217.5264,908.2986;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-1.02;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-994.8546,-75.47385;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;16;-712.5629,609.4032;Inherit;False;Property;_ScaleNoise;ScaleNoise;3;0;Create;True;0;0;0;False;0;False;8.09;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;53;1556.941,522.3575;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;26;29.47363,731.2986;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;9;-623.9039,-69.18844;Inherit;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;12;-509.3782,475.4745;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;42;1574.716,360.8251;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;56;1717.539,684.39;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;0.2;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;61;2029.658,690.7087;Inherit;False;Constant;_Float4;Float 4;12;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;52;2239.844,446.8011;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;36;140.4438,979.3896;Inherit;False;Constant;_Float1;Float 1;7;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;37;266.7464,722.5204;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-285.6066,125.1313;Inherit;False;Property;_Grayscale;Grayscale;0;0;Create;True;0;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;17;-198.1404,476.7535;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCGrayscale;3;-263.2466,0.2654037;Inherit;False;0;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;28;282.9525,474.7449;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;2;16.21631,-50.55258;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;453.684,732.8928;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;60;2485.876,724.1545;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;38;2441.525,412.4071;Inherit;True;Property;_Corazon_puto;Corazon_puto;7;0;Create;True;0;0;0;False;0;False;-1;bf691899b50982342a09c9a33c312a79;bf691899b50982342a09c9a33c312a79;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;29;856.1962,443.8625;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;54;304.8534,-42.67001;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;41;2751.957,416.3759;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;63;2814.428,695.1625;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;64;3056.968,407.6628;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;19;1188.706,-38.45925;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;27;-429.5264,988.2986;Inherit;False;Property;_Float0;Float 0;6;0;Create;True;0;0;0;False;0;False;2;2.68;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCPixelate;58;1973.641,368.5065;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TFHCPixelate;57;1969.2,499.3939;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;59;1768.181,450.8071;Inherit;False;Property;_PixelHeart;PixelHeart;11;0;Create;False;0;0;0;False;0;False;3;60;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;40;3189.65,74.31791;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;3654.775,97.75559;Float;False;True;-1;2;ASEMaterialInspector;0;2;Grayscale;c71b220b631b6344493ea3cf87110c93;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;-1;False;False;False;False;False;False;False;False;False;False;False;True;2;False;-1;True;7;False;-1;False;True;0;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;24;0;23;0
WireConnection;21;0;13;0
WireConnection;21;1;24;0
WireConnection;11;0;8;0
WireConnection;14;0;21;0
WireConnection;14;1;15;0
WireConnection;14;2;15;0
WireConnection;32;0;18;0
WireConnection;1;0;5;0
WireConnection;1;1;6;0
WireConnection;53;0;44;0
WireConnection;53;1;46;0
WireConnection;26;0;25;2
WireConnection;26;1;32;0
WireConnection;9;0;1;0
WireConnection;9;1;11;0
WireConnection;12;0;14;0
WireConnection;12;1;16;0
WireConnection;42;0;51;0
WireConnection;42;1;47;0
WireConnection;56;0;49;0
WireConnection;52;0;42;0
WireConnection;52;1;53;0
WireConnection;52;2;56;0
WireConnection;37;0;26;0
WireConnection;17;0;12;0
WireConnection;17;1;18;0
WireConnection;3;0;9;0
WireConnection;28;0;17;0
WireConnection;2;0;9;0
WireConnection;2;1;3;0
WireConnection;2;2;4;0
WireConnection;35;0;37;0
WireConnection;35;1;36;0
WireConnection;60;0;61;0
WireConnection;60;1;49;0
WireConnection;60;3;49;0
WireConnection;38;1;52;0
WireConnection;29;1;28;0
WireConnection;29;2;35;0
WireConnection;54;0;2;0
WireConnection;41;0;38;4
WireConnection;63;0;60;0
WireConnection;64;1;41;0
WireConnection;64;2;63;0
WireConnection;19;1;54;0
WireConnection;19;2;29;0
WireConnection;58;0;42;0
WireConnection;58;1;59;0
WireConnection;58;2;59;0
WireConnection;57;0;53;0
WireConnection;57;1;59;0
WireConnection;57;2;59;0
WireConnection;40;1;19;0
WireConnection;40;2;64;0
WireConnection;0;0;40;0
ASEEND*/
//CHKSM=15099DB30B2EEDCB7B17923378DF474C08DEC09A