// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Exp_Crystals"
{
	Properties
	{
		_TextureSample3("Texture Sample 3", 2D) = "white" {}
		_Float3("Float 3", Float) = 2
		_TextureSample4("Texture Sample 4", 2D) = "white" {}
		_TextureSample5("Texture Sample 5", 2D) = "white" {}
		_Float2("Float 2", Range( 0 , 10)) = 5
		_Color0("Color 0", Color) = (0.3064949,1,0.01415092,0)
		_Float0("Float 0", Float) = 0.1
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
			float4 vertexColor : COLOR;
		};

		uniform sampler2D _TextureSample3;
		uniform sampler2D _TextureSample4;
		uniform float _Float3;
		uniform sampler2D _TextureSample5;
		uniform float _Float0;
		uniform float _Float2;
		uniform float4 _Color0;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 temp_output_27_0 = ( i.uv_texcoord + 0 );
			float2 panner31 = ( ( 0.0 * _Float3 ) * float2( -0.1,1 ) + temp_output_27_0);
			float mulTime26 = _Time.y * 0.3;
			float2 panner30 = ( ( ( _Float3 * 0.25 ) * mulTime26 ) * float2( 0.1,0.75 ) + temp_output_27_0);
			float2 appendResult5 = (float2(0.0 , ( ( tex2D( _TextureSample4, panner31 ).g + tex2D( _TextureSample5, panner30 ).g ) / 2.0 )));
			float clampResult11 = clamp( i.uv_texcoord.y , 0.0 , 1.0 );
			float4 temp_output_17_0 = ( tex2D( _TextureSample3, ( 0 + ( appendResult5 * _Float0 ) ) ) * _Float2 * pow( clampResult11 , 0.0 ) );
			o.Emission = ( temp_output_17_0 * i.vertexColor * _Color0 ).rgb;
			o.Alpha = ( temp_output_17_0 * i.vertexColor.a ).r;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16700
0;0;1920;1019;1719.223;951.3073;1.920842;True;True
Node;AmplifyShaderEditor.RangedFloatNode;22;-2717.747,-14.1629;Float;False;Property;_Float3;Float 3;2;0;Create;True;0;0;False;0;2;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;26;-2722.307,256.624;Float;False;1;0;FLOAT;0.3;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;25;-2545.345,-517.8844;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-2334.748,88.83713;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.25;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;23;-2311.127,-405.0625;Float;False;-1;;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-1997.748,234.8372;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-1993.748,11.83713;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;27;-1881.807,-271.1536;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;30;-1615.623,124.3372;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.1,0.75;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;31;-1597.823,-143.2984;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.1,1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;33;-1349.987,129.1399;Float;True;Property;_TextureSample5;Texture Sample 5;5;0;Create;True;0;0;False;0;cd460ee4ac5c1e746b7a734cc7cc64dd;cd460ee4ac5c1e746b7a734cc7cc64dd;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;32;-1324.648,-65.99928;Float;True;Property;_TextureSample4;Texture Sample 4;4;0;Create;True;0;0;False;0;cd460ee4ac5c1e746b7a734cc7cc64dd;cd460ee4ac5c1e746b7a734cc7cc64dd;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;34;-955.5554,-158.0196;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;35;-749.7554,-92.37978;Float;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;5;-546.9224,-155.6819;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;6;-452.5354,442.9612;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;7;-509.9073,130.3521;Float;False;Property;_Float0;Float 0;9;0;Create;True;0;0;False;0;0.1;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;9;-396.0533,-419.0423;Float;True;-1;;1;0;OBJECT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;10;-210.8344,464.1613;Float;True;True;True;True;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-326.3624,-158.6278;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;12;82.27063,699.0966;Float;False;Constant;_Float1;Float 1;5;0;Create;True;0;0;False;0;0;0;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;11;78.27063,472.0966;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;13;-76.82837,-163.384;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PowerNode;14;363.3799,321.5597;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;16;387.355,-463.3842;Float;False;Property;_Float2;Float 2;7;0;Create;True;0;0;False;0;5;1.117647;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;15;202.5286,-138.2039;Float;True;Property;_TextureSample3;Texture Sample 3;1;0;Create;True;0;0;False;0;4457edc3ef08f4644aa0ea0cf042de56;42ea7f5e834b5d446b2dd259a8b2a32f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;19;867.275,-430.1493;Float;False;Property;_Color0;Color 0;8;0;Create;True;0;0;False;0;0.3064949,1,0.01415092,0;0.1459308,1,0.01415092,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;659.3551,-143.3843;Float;True;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;18;658.81,100.463;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;991.6721,111.2188;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;36;-1997.348,-780.9405;Float;False;myVarName;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;37;-1327.749,-296.5994;Float;True;Property;_TextureSample6;Texture Sample 6;6;0;Create;True;0;0;False;0;1e1d5dae733cbb34da4944b45828bf7a;cd460ee4ac5c1e746b7a734cc7cc64dd;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;38;-1598.147,-361.7818;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;39;-1993.617,-541.9403;Float;False;myVarName;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;40;-2111.592,-205.565;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-2361.748,-181.1629;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;41;-2596.748,-235.1629;Float;False;Property;_Float4;Float 4;3;0;Create;True;0;0;False;0;4;4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;1061.546,-144.5638;Float;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1591.366,-25.42118;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;Exp_Crystals;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;False;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;4;1;False;-1;1;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;24;0;22;0
WireConnection;29;0;24;0
WireConnection;29;1;26;0
WireConnection;28;1;22;0
WireConnection;27;0;25;0
WireConnection;27;1;23;0
WireConnection;30;0;27;0
WireConnection;30;1;29;0
WireConnection;31;0;27;0
WireConnection;31;1;28;0
WireConnection;33;1;30;0
WireConnection;32;1;31;0
WireConnection;34;0;32;2
WireConnection;34;1;33;2
WireConnection;35;0;34;0
WireConnection;5;1;35;0
WireConnection;10;0;6;2
WireConnection;8;0;5;0
WireConnection;8;1;7;0
WireConnection;11;0;10;0
WireConnection;13;0;9;0
WireConnection;13;1;8;0
WireConnection;14;0;11;0
WireConnection;14;1;12;0
WireConnection;15;1;13;0
WireConnection;17;0;15;0
WireConnection;17;1;16;0
WireConnection;17;2;14;0
WireConnection;21;0;17;0
WireConnection;21;1;18;4
WireConnection;36;0;25;0
WireConnection;37;1;38;0
WireConnection;38;0;27;0
WireConnection;38;1;40;0
WireConnection;39;0;25;2
WireConnection;40;0;42;0
WireConnection;42;0;41;0
WireConnection;42;1;26;0
WireConnection;20;0;17;0
WireConnection;20;1;18;0
WireConnection;20;2;19;0
WireConnection;0;2;20;0
WireConnection;0;9;21;0
ASEEND*/
//CHKSM=A1026D1F9E6E9315B8BF52A9F96A14BC9352BEDB