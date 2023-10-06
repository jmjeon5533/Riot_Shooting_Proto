// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Lighting_ATM"
{
	Properties
	{
		_noise_17("noise_17", 2D) = "white" {}
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Lighting_Mian("Lighting_Mian", 2D) = "white" {}
		_UV_distortion("UV_distortion", Range( 0 , 5)) = 3.472613
		_Move_X("Move_X", Range( -1 , 1)) = 0
		_Move_Y("Move_Y", Range( -1 , 1)) = 0
		_Final_Intence("Final_Intence", Range( 0 , 10)) = 2.667703
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

		uniform float _Final_Intence;
		uniform sampler2D _Lighting_Mian;
		uniform float _Move_X;
		uniform float _Move_Y;
		uniform sampler2D _noise_17;
		uniform sampler2D _TextureSample0;
		uniform float _UV_distortion;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 appendResult19 = (float2(_Move_X , _Move_Y));
			float2 panner2 = ( 0.2 * _Time.y * float2( 1,1 ) + i.uv_texcoord);
			float2 panner5 = ( 0.2 * _Time.y * float2( -1,-1 ) + i.uv_texcoord);
			float4 temp_output_30_0 = ( _Final_Intence * tex2D( _Lighting_Mian, ( float4( ( i.uv_texcoord + appendResult19 ), 0.0 , 0.0 ) + ( ( tex2D( _noise_17, panner2 ) * tex2D( _TextureSample0, panner5 ) ) * _UV_distortion ) ).rg ) );
			float4 color26 = IsGammaSpace() ? float4(1,0.9536773,0.509804,0) : float4(1,0.8978412,0.223228,0);
			o.Emission = ( temp_output_30_0 * i.vertexColor * color26 ).rgb;
			o.Alpha = ( temp_output_30_0 * i.vertexColor.a ).r;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16700
-3;24;1920;995;1974.912;637.1236;2.550866;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;1;-1377.108,7.250677;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;4;-1385.616,295.6128;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;2;-1078.708,86.15069;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,1;False;1;FLOAT;0.2;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;5;-1087.216,374.5128;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-1,-1;False;1;FLOAT;0.2;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;6;-770.6844,342.1169;Float;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;b9b336ee80eb332419f5b85d02842a97;73431de1a3b466746af1909267f44cc7;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;17;-1209.981,-453.9858;Float;False;Property;_Move_X;Move_X;5;0;Create;True;0;0;False;0;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;3;-764.1765,116.7548;Float;True;Property;_noise_17;noise_17;1;0;Create;True;0;0;False;0;b9b336ee80eb332419f5b85d02842a97;73431de1a3b466746af1909267f44cc7;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;18;-1180.981,-211.9858;Float;False;Property;_Move_Y;Move_Y;6;0;Create;True;0;0;False;0;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-643.6794,574.4299;Float;False;Property;_UV_distortion;UV_distortion;4;0;Create;True;0;0;False;0;3.472613;0;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-452.6794,243.43;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;15;-891.359,-532.95;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;19;-870.9808,-188.9858;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-192.6795,279.43;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;20;-402.9808,-278.9858;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;14;53.62055,241.2299;Float;False;2;2;0;FLOAT2;0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;31;407.0778,-131.338;Float;False;Property;_Final_Intence;Final_Intence;7;0;Create;True;0;0;False;0;2.667703;0;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;7;278.402,272.113;Float;True;Property;_Lighting_Mian;Lighting_Mian;3;0;Create;True;0;0;False;0;57f0b36284214a0479b6dc407d631a0a;57f0b36284214a0479b6dc407d631a0a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;617.8542,279.3021;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;26;831.6522,41.96655;Float;False;Constant;_Color0;Color 0;6;0;Create;True;0;0;False;0;1,0.9536773,0.509804,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;27;621.6505,540.1353;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;867.6503,539.1353;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;21;-1290.056,855.2753;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;22;-1032.349,924.5944;Float;True;True;True;True;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;23;-720.3489,963.5944;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;24;-377.3489,1047.594;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-751.3489,1192.594;Float;False;Property;_Power_EXP;Power_EXP;8;0;Create;True;0;0;False;0;0;0;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;870.6502,258.1351;Float;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1075.703,218.1936;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;Lighting_ATM;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;4;1;False;-1;1;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;0;1;0
WireConnection;5;0;4;0
WireConnection;6;1;5;0
WireConnection;3;1;2;0
WireConnection;11;0;3;0
WireConnection;11;1;6;0
WireConnection;19;0;17;0
WireConnection;19;1;18;0
WireConnection;13;0;11;0
WireConnection;13;1;12;0
WireConnection;20;0;15;0
WireConnection;20;1;19;0
WireConnection;14;0;20;0
WireConnection;14;1;13;0
WireConnection;7;1;14;0
WireConnection;30;0;31;0
WireConnection;30;1;7;0
WireConnection;29;0;30;0
WireConnection;29;1;27;4
WireConnection;22;0;21;2
WireConnection;23;0;22;0
WireConnection;24;0;23;0
WireConnection;24;1;25;0
WireConnection;28;0;30;0
WireConnection;28;1;27;0
WireConnection;28;2;26;0
WireConnection;0;2;28;0
WireConnection;0;9;29;0
ASEEND*/
//CHKSM=D9A1C080D5ACFB6B9C4564E0D0A716A7AEA94ACC