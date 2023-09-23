// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Magic_Circle_Rotate"
{
	Properties
	{
		_1234444("1234444", 2D) = "white" {}
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_UV_distortion("UV_distortion", Range( 0 , 5)) = 0.5294118
		_Move_Y("Move_Y", Range( 0 , 1)) = 0
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
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

		uniform sampler2D _TextureSample1;
		uniform float _Move_Y;
		uniform sampler2D _1234444;
		uniform sampler2D _TextureSample0;
		uniform float _UV_distortion;
		uniform float _Final_Intence;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float cos34 = cos( -1.0 * _Time.y );
			float sin34 = sin( -1.0 * _Time.y );
			float2 rotator34 = mul( i.uv_texcoord - float2( 0.5,0.5 ) , float2x2( cos34 , -sin34 , sin34 , cos34 )) + float2( 0.5,0.5 );
			float2 appendResult17 = (float2(0.0 , _Move_Y));
			float2 panner6 = ( 1.0 * _Time.y * float2( 0.2,0.1 ) + i.uv_texcoord);
			float2 panner9 = ( 1.0 * _Time.y * float2( -0.2,-0.02 ) + i.uv_texcoord);
			float clampResult22 = clamp( i.uv_texcoord.y , 0.0 , 1.0 );
			float4 temp_output_26_0 = ( tex2D( _TextureSample1, ( float4( ( rotator34 + appendResult17 ), 0.0 , 0.0 ) + ( ( tex2D( _1234444, panner6 ) * tex2D( _TextureSample0, panner9 ) ) * _UV_distortion ) ).rg ) * _Final_Intence * pow( clampResult22 , 0.0 ) );
			float4 color29 = IsGammaSpace() ? float4(1,0.7880987,0.6556604,0) : float4(1,0.5838514,0.387421,0);
			o.Emission = ( temp_output_26_0 * i.vertexColor * color29 ).rgb;
			o.Alpha = ( temp_output_26_0 * i.vertexColor.a ).r;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16700
0;0;1920;1019;1067.275;687.0399;1;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-1227.656,-93.06519;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;7;-1226.323,172.6015;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;6;-927.6561,-60.06519;Float;True;3;0;FLOAT2;0,0;False;2;FLOAT2;0.2,0.1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;9;-934.3229,199.6015;Float;True;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.2,-0.02;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;2;-523.6561,-53.06519;Float;True;Property;_1234444;1234444;1;0;Create;True;0;0;False;0;b9b336ee80eb332419f5b85d02842a97;b6db440f1cf570d4386b8defe4f9b4d0;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;13;-944.7634,-571.8925;Float;False;Constant;_Move_X;Move_X;4;0;Create;True;0;0;False;0;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-950.7634,-439.8925;Float;False;Property;_Move_Y;Move_Y;4;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;8;-566.3229,195.6015;Float;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;b9b336ee80eb332419f5b85d02842a97;b6db440f1cf570d4386b8defe4f9b4d0;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;15;-578.7634,-647.8925;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-190.7634,62.10751;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;20;-83.59617,683.3116;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RotatorNode;34;-319.9206,-497.1206;Float;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;-1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-269.7634,269.1075;Float;False;Property;_UV_distortion;UV_distortion;3;0;Create;True;0;0;False;0;0.5294118;0;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;17;-564.7634,-341.8925;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;21;158.104,704.5116;Float;True;True;True;True;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-4.763428,91.10751;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;18;-46.89903,-323.3339;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;19;212.2366,100.3075;Float;False;2;2;0;FLOAT2;0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;24;451.2098,939.4468;Float;False;Constant;_Power_EXP;Power_EXP;5;0;Create;True;0;0;False;0;0;0;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;22;447.2098,712.4469;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;30;669.9418,68.44344;Float;False;Property;_Final_Intence;Final_Intence;6;0;Create;True;0;0;False;0;2.667703;0;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;25;502.624,219.0554;Float;True;Property;_TextureSample1;Texture Sample 1;5;0;Create;True;0;0;False;0;b6db440f1cf570d4386b8defe4f9b4d0;b6db440f1cf570d4386b8defe4f9b4d0;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;23;724.3978,770.3297;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;29;1151.919,-46.76505;Float;False;Constant;_Color0;Color 0;6;0;Create;True;0;0;False;0;1,0.7880987,0.6556604,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;31;940.6173,502.1032;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;846.3,223.8623;Float;True;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;1186.617,501.1031;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;32;1189.617,220.1032;Float;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1467.8,142.5;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;Magic_Circle_Rotate;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;4;1;False;-1;1;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;6;0;3;0
WireConnection;9;0;7;0
WireConnection;2;1;6;0
WireConnection;8;1;9;0
WireConnection;10;0;2;0
WireConnection;10;1;8;0
WireConnection;34;0;15;0
WireConnection;17;0;13;0
WireConnection;17;1;14;0
WireConnection;21;0;20;2
WireConnection;11;0;10;0
WireConnection;11;1;12;0
WireConnection;18;0;34;0
WireConnection;18;1;17;0
WireConnection;19;0;18;0
WireConnection;19;1;11;0
WireConnection;22;0;21;0
WireConnection;25;1;19;0
WireConnection;23;0;22;0
WireConnection;23;1;24;0
WireConnection;26;0;25;0
WireConnection;26;1;30;0
WireConnection;26;2;23;0
WireConnection;33;0;26;0
WireConnection;33;1;31;4
WireConnection;32;0;26;0
WireConnection;32;1;31;0
WireConnection;32;2;29;0
WireConnection;0;2;32;0
WireConnection;0;9;33;0
ASEEND*/
//CHKSM=70EC70573EA67CCFC1FA1440CE94105AA92974C1