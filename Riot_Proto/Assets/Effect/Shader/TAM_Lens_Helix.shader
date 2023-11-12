// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "TAM_Lens_Helix"
{
	Properties
	{
		_LensFFF("LensFFF", 2D) = "white" {}
		_Move_Y("Move_Y", Range( -1 , 1)) = 0
		_Mask_Intence("Mask_Intence", Range( 0 , 10)) = 3
		_Final_Intence("Final_Intence", Range( 0 , 20)) = 0
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

		uniform sampler2D _LensFFF;
		uniform float _Move_Y;
		uniform float _Mask_Intence;
		uniform float _Final_Intence;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 appendResult4 = (float2(i.uv2_tex4coord2.x , _Move_Y));
			float temp_output_9_0 = i.uv_texcoord.x;
			float clampResult10 = clamp( temp_output_9_0 , 0.0 , 1.0 );
			float4 temp_output_15_0 = ( tex2D( _LensFFF, ( i.uv_texcoord + appendResult4 ) ) * ( ( 1.0 - clampResult10 ) * temp_output_9_0 * _Mask_Intence ) );
			o.Emission = ( temp_output_15_0 * i.vertexColor * _Final_Intence ).rgb;
			o.Alpha = ( temp_output_15_0 * i.vertexColor.a ).r;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16700
0;0;1920;1019;2595.257;878.7656;2.769773;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;8;-1441.671,551.1938;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;7;-1500.542,293.5466;Float;False;Property;_Move_Y;Move_Y;2;0;Create;True;0;0;False;0;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;22;-1534.914,-35.13821;Float;True;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;9;-1133.671,538.1938;Float;False;True;True;True;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;2;-1173.92,-86.73667;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;10;-846.8825,562.5612;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;4;-1172.727,171.8621;Float;True;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-543.8827,866.5612;Float;False;Property;_Mask_Intence;Mask_Intence;3;0;Create;True;0;0;False;0;3;0;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;12;-541.8827,577.5612;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;5;-841.2264,62.66221;Float;True;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-344.8827,595.5612;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-421.2049,78.34167;Float;True;Property;_LensFFF;LensFFF;1;0;Create;True;0;0;False;0;c4280e3e17352cf419071ba732ab2a2d;c4280e3e17352cf419071ba732ab2a2d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;17;-52.3445,592.7899;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-31.6912,327.6081;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-8.842468,57.32942;Float;False;Property;_Final_Intence;Final_Intence;4;0;Create;True;0;0;False;0;0;0;0;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;292.6555,309.79;Float;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;257.6556,557.79;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;615.118,332.0134;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;TAM_Lens_Helix;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;4;1;False;-1;1;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;9;0;8;1
WireConnection;10;0;9;0
WireConnection;4;0;22;1
WireConnection;4;1;7;0
WireConnection;12;0;10;0
WireConnection;5;0;2;0
WireConnection;5;1;4;0
WireConnection;13;0;12;0
WireConnection;13;1;9;0
WireConnection;13;2;14;0
WireConnection;1;1;5;0
WireConnection;15;0;1;0
WireConnection;15;1;13;0
WireConnection;19;0;15;0
WireConnection;19;1;17;0
WireConnection;19;2;21;0
WireConnection;20;0;15;0
WireConnection;20;1;17;4
WireConnection;0;2;19;0
WireConnection;0;9;20;0
ASEEND*/
//CHKSM=71961876D8A5F52738009F28233E3B2807060A5C