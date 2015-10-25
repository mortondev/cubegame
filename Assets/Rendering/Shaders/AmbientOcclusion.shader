Shader "CubeShader" {
	Properties{
		//_Top("Top", Float) = 0.2
		//_Bottom("Bottom", Float) = 0.3
		_Color("Main Colour", Color) = (1.0, 0.0, 0.0, 1.0 )
	}
	SubShader {
		Pass {
			CGPROGRAM
// Upgrade NOTE: excluded shader from DX11 and Xbox360; has structs without semantics (struct v2f members vertex)
#pragma exclude_renderers d3d11 xbox360

			#pragma vertex vert
			#pragma fragment frag

			//uniform float _Top;
			//uniform float _Bottom;
			uniform float4 _Color;

			struct appdata {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f {
				float4 pos : VS_POSITION;
				float4 colour : COLOR;
			};

			v2f vert(appdata i) : SV_POSITION {
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, i.vertex);
				o.colour = i.vertex;
				return o;
			}

			float4 frag(v2f i) : COLOR {
				return _Color *	i.pos;
			}

			ENDCG
		}
	}
}