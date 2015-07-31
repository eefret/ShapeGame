Shader "CustomShaders/FlatColorBorder" {

	Properties {
		_MainTex ("Texture", 2D) = "" {}
		_Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_BoxSize ("Box Radius", Float) = 0.0
		_ColorBorder ("Border Color", Color) = (1.0, 1.0, 1.0, 1.0)
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
			uniform float4 _ColorBorder;
			uniform float _BoxSize;
			
			//base input struct
			struct vertexInput {
				float4 vertex : POSITION;
			};
			
			struct vertexOutput {
				float4 pos : SV_POSITION;
				float4 vert : TEXCOORD0;
			};
			
			//vertex function
			vertexOutput vert(vertexInput v) {
				vertexOutput o;
				
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.vert = v.vertex;
				
				return o;
			}
			
			//fragment function
            float4 frag(vertexOutput i) : COLOR {
            	float4 output;
            	output = _Color;
            	
            	if(i.vert.x > _BoxSize || i.vert.x < -_BoxSize || i.vert.y > _BoxSize || i.vert.y < -_BoxSize) {
            		output = _ColorBorder;
            	}
            	
				return output;
			}
			
			ENDCG
		}
		
	}
	
	//Fallback "Diffuse"
}