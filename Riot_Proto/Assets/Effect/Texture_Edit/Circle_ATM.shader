// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Rotate_Circle_ATM"
{
	Properties
	{
		_Lgithing_Skills("Lgithing_Skills", 2D) = "white" {}
		_Rotate_X("Rotate_X", Range( -1 , 1)) = 0.5
		_Rotate_Y("Rotate_Y", Range( -1 , 1)) = 0.5
		_Move_Y("Move_Y", Range( 0 , 1)) = 0
		_Move_X("Move_X", Range( -1 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Back
		ZWrite Off
		Blend One One
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Lgithing_Skills;
		uniform float _Rotate_X;
		uniform float _Rotate_Y;
		uniform float _Move_X;
		uniform float _Move_Y;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 appendResult34 = (float2(_Rotate_X , _Rotate_Y));
			float cos28 = cos( -1.0 * _Time.y );
			float sin28 = sin( -1.0 * _Time.y );
			float2 rotator28 = mul( i.uv_texcoord - appendResult34 , float2x2( cos28 , -sin28 , sin28 , cos28 )) + appendResult34;
			float2 appendResult30 = (float2(_Move_X , _Move_Y));
			o.Emission = tex2D( _Lgithing_Skills, ( rotator28 + appendResult30 ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16700
7;42;1920;977;2070.326;338.9965;1.3;True;True
Node;AmplifyShaderEditor.RangedFloatNode;31;-1507.649,-67.30465;Float;False;Property;_Rotate_X;Rotate_X;2;0;Create;True;0;0;False;0;0.5;0.5;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;32;-1515.449,163.3954;Float;False;Property;_Rotate_Y;Rotate_Y;3;0;Create;True;0;0;False;0;0.5;0.5;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;27;-1116.932,17.37115;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;26;-1534.932,541.3712;Float;False;Property;_Move_Y;Move_Y;4;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;34;-1122.549,285.9955;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-1528.932,409.3712;Float;False;Property;_Move_X;Move_X;5;0;Create;True;0;0;False;0;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;30;-1098.932,537.3712;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RotatorNode;28;-858.0891,168.143;Float;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;-1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;29;-585.0674,341.9298;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;8;-299.9244,321.6406;Float;True;Property;_Lgithing_Skills;Lgithing_Skills;1;0;Create;True;0;0;False;0;1b711b5c95fae0d42ac8370ed4e57b3b;1b711b5c95fae0d42ac8370ed4e57b3b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;67,216;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;Rotate_Circle_ATM;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;4;1;False;-1;1;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;34;0;31;0
WireConnection;34;1;32;0
WireConnection;30;0;25;0
WireConnection;30;1;26;0
WireConnection;28;0;27;0
WireConnection;28;1;34;0
WireConnection;29;0;28;0
WireConnection;29;1;30;0
WireConnection;8;1;29;0
WireConnection;0;2;8;0
ASEEND*/
//CHKSM=B2C4204F1DBCB658DFCA6A2A9415A009E19F05B3