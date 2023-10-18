// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Make_Lighting_shader"
{
	Properties
	{
		_MainTexture("Main Texture", 2D) = "white" {}
		_VFlowSpeed("V Flow Speed", Float) = 2
		_UFlowSpeed("U Flow Speed", Float) = 4
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_TextureSample2("Texture Sample 2", 2D) = "white" {}
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Final_Intence("Final_Intence", Range( 0 , 10)) = 2.667703
		_Color("Color", Color) = (1,0.9974309,0.7122642,0)
		_Flow_Power("Flow_Power", Float) = 0.1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
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
		struct Input
		{
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
		};

		uniform sampler2D _MainTexture;
		uniform sampler2D _TextureSample0;
		uniform float _UFlowSpeed;
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
			float2 UV16 = i.uv_texcoord;
			float2 temp_output_8_0 = ( i.uv_texcoord + 0 );
			float2 panner15 = ( sin( ( _UFlowSpeed * _Time.y ) ) * float2( 1,0 ) + temp_output_8_0);
			float2 panner13 = ( ( 0.0 * _VFlowSpeed ) * float2( -0.1,1 ) + temp_output_8_0);
			float2 panner12 = ( ( ( _VFlowSpeed * 0.25 ) * _Time.y ) * float2( 0.1,0.75 ) + temp_output_8_0);
			float2 appendResult21 = (float2(tex2D( _TextureSample0, panner15 ).r , ( ( tex2D( _TextureSample1, panner13 ).g + tex2D( _TextureSample2, panner12 ).g ) / 2.0 )));
			float clampResult33 = clamp( i.uv_texcoord.y , 0.0 , 1.0 );
			float4 temp_output_36_0 = ( tex2D( _MainTexture, ( UV16 + ( appendResult21 * _Flow_Power ) ) ) * _Final_Intence * pow( clampResult33 , 0.0 ) );
			o.Emission = ( temp_output_36_0 * i.vertexColor * _Color ).rgb;
			o.Alpha = ( temp_output_36_0 * i.vertexColor.a ).r;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16700
0;0;1920;1019;-860.6316;279.5646;1;True;True
Node;AmplifyShaderEditor.RangedFloatNode;3;-1728.964,190.2842;Float;False;Property;_VFlowSpeed;V Flow Speed;2;0;Create;True;0;0;False;0;2;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;4;-1556.561,-313.4374;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-1345.964,293.2842;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.25;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;5;-1322.343,-200.6155;Float;False;-1;;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;18;-1733.523,461.071;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-1607.964,-30.71578;Float;False;Property;_UFlowSpeed;U Flow Speed;3;0;Create;True;0;0;False;0;4;4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-1008.964,439.2842;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-1004.964,216.2842;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;8;-893.0227,-66.70642;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;12;-626.8397,328.7843;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.1,0.75;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;13;-609.0387,61.14864;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.1,1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-1372.964,23.28422;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;14;-1122.808,-1.11787;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;26;-335.8645,138.4478;Float;True;Property;_TextureSample1;Texture Sample 1;4;0;Create;True;0;0;False;0;cd460ee4ac5c1e746b7a734cc7cc64dd;cd460ee4ac5c1e746b7a734cc7cc64dd;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;27;-361.2035,333.587;Float;True;Property;_TextureSample2;Texture Sample 2;5;0;Create;True;0;0;False;0;cd460ee4ac5c1e746b7a734cc7cc64dd;cd460ee4ac5c1e746b7a734cc7cc64dd;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;15;-609.3636,-157.3348;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;25;33.22873,46.42744;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;19;239.0284,112.0673;Float;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;28;-338.9655,-92.15225;Float;True;Property;_TextureSample0;Texture Sample 0;6;0;Create;True;0;0;False;0;cd460ee4ac5c1e746b7a734cc7cc64dd;cd460ee4ac5c1e746b7a734cc7cc64dd;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;16;-1008.564,-576.4935;Float;False;UV;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;21;441.8616,48.76515;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;30;536.2488,647.4082;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;20;478.8767,334.7993;Float;False;Property;_Flow_Power;Flow_Power;9;0;Create;True;0;0;False;0;0.1;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;662.4213,45.81924;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;29;592.7303,-214.5953;Float;True;16;UV;1;0;OBJECT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;31;777.9489,668.6082;Float;True;True;True;True;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;23;911.9558,41.06313;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;32;1071.055,903.5434;Float;False;Constant;_Power_EXP;Power_EXP;5;0;Create;True;0;0;False;0;0;0;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;33;1067.055,676.5435;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;24;1191.312,66.24324;Float;True;Property;_MainTexture;Main Texture;1;0;Create;True;0;0;False;0;5904d27ee1d359f4891704c299d1fb85;5904d27ee1d359f4891704c299d1fb85;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;34;1352.164,526.0067;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;35;1376.139,-258.9372;Float;False;Property;_Final_Intence;Final_Intence;7;0;Create;True;0;0;False;0;2.667703;0;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;1648.139,61.06282;Float;True;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;40;1647.594,304.9102;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;38;1856.059,-225.7023;Float;False;Property;_Color;Color;8;0;Create;True;0;0;False;0;1,0.9974309,0.7122642,0;1,0.9919136,0.3066038,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;1980.456,315.666;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;17;-1004.833,-337.4933;Float;False;V;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;1983.238,45.09161;Float;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;2;2264.078,50.18232;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;Make_Lighting_shader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;4;1;False;-1;1;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;6;0;3;0
WireConnection;9;0;6;0
WireConnection;9;1;18;0
WireConnection;10;1;3;0
WireConnection;8;0;4;0
WireConnection;8;1;5;0
WireConnection;12;0;8;0
WireConnection;12;1;9;0
WireConnection;13;0;8;0
WireConnection;13;1;10;0
WireConnection;11;0;7;0
WireConnection;11;1;18;0
WireConnection;14;0;11;0
WireConnection;26;1;13;0
WireConnection;27;1;12;0
WireConnection;15;0;8;0
WireConnection;15;1;14;0
WireConnection;25;0;26;2
WireConnection;25;1;27;2
WireConnection;19;0;25;0
WireConnection;28;1;15;0
WireConnection;16;0;4;0
WireConnection;21;0;28;1
WireConnection;21;1;19;0
WireConnection;22;0;21;0
WireConnection;22;1;20;0
WireConnection;31;0;30;2
WireConnection;23;0;29;0
WireConnection;23;1;22;0
WireConnection;33;0;31;0
WireConnection;24;1;23;0
WireConnection;34;0;33;0
WireConnection;34;1;32;0
WireConnection;36;0;24;0
WireConnection;36;1;35;0
WireConnection;36;2;34;0
WireConnection;39;0;36;0
WireConnection;39;1;40;4
WireConnection;17;0;4;2
WireConnection;37;0;36;0
WireConnection;37;1;40;0
WireConnection;37;2;38;0
WireConnection;2;2;37;0
WireConnection;2;9;39;0
ASEEND*/
//CHKSM=55E4C7B0B66783DACA435890C6DD911820B42820