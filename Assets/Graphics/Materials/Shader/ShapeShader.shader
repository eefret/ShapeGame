Shader "CustomShaders/ShapeShader" {

	Properties {
		_MainTex ("Texture", 2D) = "" {}
		_Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_SecondColor ("SecondColor", Color) = (1.0, 1.0, 1.0, 1.0)
		_Center ("Center", Vector) = (0.5, 0.5, 0.0, 0.0)
		_Angle ("Angle", Float) = 0.0
	}
	
	SubShader {
       	ZWrite On
       	Blend SrcAlpha OneMinusSrcAlpha
		
		Pass {
			CGPROGRAM
			
			//pragmas
			#pragma vertex vert
			#pragma fragment frag
			
			//user defined variables
			uniform float4 _Color;
			uniform float4 _SecondColor;
			uniform float4 _Center;
			uniform float _Angle;
			
			//base input struct
			struct vertexInput {
				float4 vertex : POSITION;
				float4 uvcoord : TEXCOORD0;
			};
			
			struct vertexOutput {
				float4 pos : SV_POSITION;
				float4 uv : TEXCOORD0;
			};
			
			//vertex function
			vertexOutput vert(vertexInput v) {
				vertexOutput o;
				
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uvcoord;
				
				return o;
			}
			
			//fragment function
            float4 frag(vertexOutput i) : COLOR {
            	float4 output = float4(1.0, 1.0, 1.0, 1.0);
            	if(_Angle > 0 && atan2(i.uv.x - _Center.x, i.uv.y - _Center.y) + 3.14 < _Angle) { 
            		output = _SecondColor;
            	} else {
            		output = _Color;
            	}
				return output;
			}
			
			ENDCG
		}
		
	}
	
	//Fallback "Diffuse"
}