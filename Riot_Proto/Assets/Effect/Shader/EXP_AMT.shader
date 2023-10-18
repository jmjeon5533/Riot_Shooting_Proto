// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "EXP_AMT"
{
	Properties
	{
		_MainTexture("Main Texture", 2D) = "white" {}
		_VFlowSpeed("V Flow Speed", Float) = 2
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_TextureSample2("Texture Sample 2", 2D) = "white" {}
		_Final_Intence("Final_Intence", Range( 0 , 10)) = 5
		_Color("Color", Color) = (0.3064949,1,0.01415092,0)
		_Flow_Power("Flow_Power", Float) = 0.1
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

		uniform sampler2D _MainTexture;
		uniform sampler2D _TextureSample1;
		uniform float _VFlowSpeed;
		uniform sampler2D _TextureSample2;
		uniform float _Flow_Power;
		uniform float _Final_Intence;
		uniform float4 _Color;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 UV38 = i.uv_texcoord;
			float2 temp_output_29_0 = ( i.uv_texcoord + 0 );
			float2 panner31 = ( ( 0.0 * _VFlowSpeed ) * float2( -0.1,1 ) + temp_output_29_0);
			float mulTime25 = _Time.y * 0.3;
			float2 panner30 = ( ( ( _VFlowSpeed * 0.25 ) * mulTime25 ) * float2( 0.1,0.75 ) + temp_output_29_0);
			float2 appendResult4 = (float2(0.0 , ( ( tex2D( _TextureSample1, panner31 ).g + tex2D( _TextureSample2, panner30 ).g ) / 2.0 )));
			float clampResult12 = clamp( i.uv_texcoord.y , 0.0 , 1.0 );
			float4 temp_output_16_0 = ( tex2D( _MainTexture, ( UV38 + ( appendResult4 * _Flow_Power ) ) ) * _Final_Intence * pow( clampResult12 , 0.0 ) );
			o.Emission = ( temp_output_16_0 * i.vertexColor * _Color ).rgb;
			o.Alpha = ( temp_output_16_0 * i.vertexColor.a ).r;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16700
0;0;1920;1019;1489.479;307.9828;1;True;True
Node;AmplifyShaderEditor.RangedFloatNode;21;-4021.638,92.16327;Float;False;Property;_VFlowSpeed;V Flow Speed;2;0;Create;True;0;0;False;0;2;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;24;-3615.018,-298.7363;Float;False;-1;;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-3638.639,195.1633;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.25;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;22;-3849.236,-411.5582;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;25;-4026.198,362.9502;Float;False;1;0;FLOAT;0.3;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;29;-3185.698,-164.8274;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-3297.639,118.1633;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-3301.639,341.1634;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;30;-2919.514,230.6634;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.1,0.75;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;31;-2901.714,-36.97228;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.1,1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;34;-2628.539,40.32689;Float;True;Property;_TextureSample1;Texture Sample 1;4;0;Create;True;0;0;False;0;cd460ee4ac5c1e746b7a734cc7cc64dd;cd460ee4ac5c1e746b7a734cc7cc64dd;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;35;-2653.878,235.4661;Float;True;Property;_TextureSample2;Texture Sample 2;5;0;Create;True;0;0;False;0;cd460ee4ac5c1e746b7a734cc7cc64dd;cd460ee4ac5c1e746b7a734cc7cc64dd;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;2;-2259.446,-51.69347;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;3;-2053.646,13.94639;Float;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;4;-1850.813,-49.35577;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;5;-1756.426,549.2874;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;6;-1813.798,236.6783;Float;False;Property;_Flow_Power;Flow_Power;9;0;Create;True;0;0;False;0;0.1;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;38;-3301.239,-674.6143;Float;False;UV;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-1630.253,-52.30167;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;8;-1699.944,-312.7161;Float;True;38;UV;1;0;OBJECT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;9;-1514.725,570.4874;Float;True;True;True;True;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;12;-1225.62,578.4228;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-1221.62,805.4227;Float;False;Constant;_Power_EXP;Power_EXP;5;0;Create;True;0;0;False;0;0;0;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;10;-1380.719,-57.0578;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PowerNode;14;-940.5107,427.8859;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;13;-1101.362,-31.87768;Float;True;Property;_MainTexture;Main Texture;1;0;Create;True;0;0;False;0;4457edc3ef08f4644aa0ea0cf042de56;5904d27ee1d359f4891704c299d1fb85;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;15;-916.5356,-357.058;Float;False;Property;_Final_Intence;Final_Intence;7;0;Create;True;0;0;False;0;5;0;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-644.5355,-37.0581;Float;True;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;17;-645.0806,206.7892;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;18;-436.6156,-323.8231;Float;False;Property;_Color;Color;8;0;Create;True;0;0;False;0;0.3064949,1,0.01415092,0;1,0.9919136,0.3066038,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-242.3449,-38.23766;Float;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;-312.2185,217.545;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;37;-2631.64,-190.2732;Float;True;Property;_TextureSample0;Texture Sample 0;6;0;Create;True;0;0;False;0;1e1d5dae733cbb34da4944b45828bf7a;cd460ee4ac5c1e746b7a734cc7cc64dd;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;36;-2902.038,-255.4556;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;39;-3297.508,-435.6141;Float;False;V;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;33;-3415.483,-99.23882;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-3900.639,-128.8367;Float;False;Property;_UFlowSpeed;U Flow Speed;3;0;Create;True;0;0;False;0;4;4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;32;-3665.639,-74.8367;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;EXP_AMT;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;4;1;False;-1;1;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;23;0;21;0
WireConnection;29;0;22;0
WireConnection;29;1;24;0
WireConnection;28;1;21;0
WireConnection;27;0;23;0
WireConnection;27;1;25;0
WireConnection;30;0;29;0
WireConnection;30;1;27;0
WireConnection;31;0;29;0
WireConnection;31;1;28;0
WireConnection;34;1;31;0
WireConnection;35;1;30;0
WireConnection;2;0;34;2
WireConnection;2;1;35;2
WireConnection;3;0;2;0
WireConnection;4;1;3;0
WireConnection;38;0;22;0
WireConnection;7;0;4;0
WireConnection;7;1;6;0
WireConnection;9;0;5;2
WireConnection;12;0;9;0
WireConnection;10;0;8;0
WireConnection;10;1;7;0
WireConnection;14;0;12;0
WireConnection;14;1;11;0
WireConnection;13;1;10;0
WireConnection;16;0;13;0
WireConnection;16;1;15;0
WireConnection;16;2;14;0
WireConnection;20;0;16;0
WireConnection;20;1;17;0
WireConnection;20;2;18;0
WireConnection;19;0;16;0
WireConnection;19;1;17;4
WireConnection;37;1;36;0
WireConnection;36;0;29;0
WireConnection;36;1;33;0
WireConnection;39;0;22;2
WireConnection;33;0;32;0
WireConnection;32;0;26;0
WireConnection;32;1;25;0
WireConnection;0;2;20;0
WireConnection;0;9;19;0
ASEEND*/
//CHKSM=A1B2D0C6801A8866ACAA1BED46BBF41305D4BCFF