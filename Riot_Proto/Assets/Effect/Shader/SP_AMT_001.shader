// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SP_AMT_001"
{
	Properties
	{
		_Cloud3("Cloud 3", 2D) = "white" {}
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Line4("Line 4", 2D) = "white" {}
		_UV_distortion_Intence("UV_distortion_Intence", Range( 0 , 5)) = 2
		_Move_X("Move_X", Range( -1 , 1)) = 0
		_Power_Exp("Power_Exp", Range( 0 , 5)) = 0.7753187
		_Final_Intence("Final_Intence", Range( 0 , 50)) = 4.248047
		_Color0("Color 0", Color) = (0.3254717,0.5685565,1,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] _tex4coord2( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Off
		ZWrite Off
		Blend One One
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha noshadow 
		#undef TRANSFORM_TEX
		#define TRANSFORM_TEX(tex,name) float4(tex.xy * name##_ST.xy + name##_ST.zw, tex.z, tex.w)
		struct Input
		{
			float2 uv_texcoord;
			float4 uv2_tex4coord2;
			float4 vertexColor : COLOR;
		};

		uniform sampler2D _Line4;
		uniform float _Move_X;
		uniform sampler2D _Cloud3;
		uniform sampler2D _TextureSample0;
		uniform float _UV_distortion_Intence;
		uniform float _Power_Exp;
		uniform float _Final_Intence;
		uniform float4 _Color0;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 appendResult18 = (float2(_Move_X , i.uv2_tex4coord2.x));
			float2 panner2 = ( 1.0 * _Time.y * float2( 0.2,0.1 ) + i.uv_texcoord);
			float2 panner4 = ( 1.0 * _Time.y * float2( -0.2,-0.02 ) + i.uv_texcoord);
			float clampResult23 = clamp( i.uv_texcoord.y , 0.0 , 1.0 );
			float4 temp_output_26_0 = ( tex2D( _Line4, ( ( i.uv_texcoord + appendResult18 ) + ( ( tex2D( _Cloud3, panner2 ).r * tex2D( _TextureSample0, panner4 ).g ) * _UV_distortion_Intence ) ) ) * pow( clampResult23 , _Power_Exp ) * _Final_Intence );
			o.Emission = ( temp_output_26_0 * i.vertexColor * _Color0 ).rgb;
			o.Alpha = ( temp_output_26_0 * i.vertexColor.a ).r;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16700
-366;960;1920;1019;-294.8208;238.7658;1;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;1;-1028.394,-7.297688;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-1001.773,296.0876;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;2;-706.5788,-92.70409;Float;True;3;0;FLOAT2;0,0;False;2;FLOAT2;0.2,0.1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;4;-709.4589,250.0085;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.2,-0.02;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-571.2346,-557.4503;Float;False;Property;_Move_X;Move_X;6;0;Create;True;0;0;False;0;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;5;-446.6002,-58.10125;Float;True;Property;_Cloud3;Cloud 3;1;0;Create;True;0;0;False;0;7df66675bfe2c824bae2ba812a4838cd;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;32;-602.3294,-344.2636;Float;False;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;7;-463.9501,247.5432;Float;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;7df66675bfe2c824bae2ba812a4838cd;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;10;-137.8361,361.7317;Float;False;Property;_UV_distortion_Intence;UV_distortion_Intence;4;0;Create;True;0;0;False;0;2;0;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-121.6283,2.523209;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;18;-258.2452,-299.6226;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;16;-271.9136,-586.161;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;21;135.5467,439.1867;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;22;388.9874,451.5061;Float;False;True;True;True;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;15;59.49597,-347.8903;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;126.8967,73.10846;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;14;422.7122,61.04352;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ClampOpNode;23;627.8203,458.8973;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;25;611.8203,670.8973;Float;False;Property;_Power_Exp;Power_Exp;7;0;Create;True;0;0;False;0;0.7753187;0;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;6;748.6898,137.793;Float;True;Property;_Line4;Line 4;3;0;Create;True;0;0;False;0;03a7d169469c1af41bb03241a7b7e23d;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;24;920.8203,489.8973;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;27;949.4073,-88.93548;Float;False;Property;_Final_Intence;Final_Intence;8;0;Create;True;0;0;False;0;4.248047;0;0;50;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;1153.535,263.3164;Float;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;30;1160.467,506.7608;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;31;1365.061,-16.84378;Float;False;Property;_Color0;Color 0;9;0;Create;True;0;0;False;0;0.3254717,0.5685565,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;20;-1100.891,-459.7415;Float;False;Property;_Move_Y;Move_Y;5;0;Create;True;0;0;False;0;1;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;1383.676,203.2888;Float;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;1411.991,449.4082;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;13;56.44781,-633.2944;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1620.053,216.8269;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;SP_AMT_001;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;4;1;False;-1;1;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;0;1;0
WireConnection;4;0;3;0
WireConnection;5;1;2;0
WireConnection;7;1;4;0
WireConnection;8;0;5;1
WireConnection;8;1;7;2
WireConnection;18;0;19;0
WireConnection;18;1;32;1
WireConnection;22;0;21;2
WireConnection;15;0;16;0
WireConnection;15;1;18;0
WireConnection;9;0;8;0
WireConnection;9;1;10;0
WireConnection;14;0;15;0
WireConnection;14;1;9;0
WireConnection;23;0;22;0
WireConnection;6;1;14;0
WireConnection;24;0;23;0
WireConnection;24;1;25;0
WireConnection;26;0;6;0
WireConnection;26;1;24;0
WireConnection;26;2;27;0
WireConnection;28;0;26;0
WireConnection;28;1;30;0
WireConnection;28;2;31;0
WireConnection;29;0;26;0
WireConnection;29;1;30;4
WireConnection;0;2;28;0
WireConnection;0;9;29;0
ASEEND*/
//CHKSM=A8EC35E8907BD224BB3754735AA335F5FCEDC1CD