Shader "CustomShaders/FlatColor" {

	Properties {
		_MainTex ("Texture", 2D) = "" {}
		_Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
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
			
			//base input struct
			struct vertexInput {
				float4 vertex : POSITION;
				float4 vcolor : COLOR;
			};
			
			struct vertexOutput {
				float4 pos : SV_POSITION;
				float4 color : COLOR;
			};
			
			//vertex function
			vertexOutput vert(vertexInput v) {
				vertexOutput o;
				
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.vcolor;
				
				return o;
			}
			
			//fragment function
            float4 frag(vertexOutput i) : COLOR {
				return _Color * i.color;
			}
			
			ENDCG
		}
		
	}
	
	//Fallback "Diffuse"
}