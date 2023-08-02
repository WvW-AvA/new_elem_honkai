Shader "Unlit/InfoDocker_OutLine"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture",2D) = "white"{}
		_Color("color",Color) = (1,1,1,1)
		_Intensity("intensity",Range(0,1))=0.0
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			Cull Off
			Lighting Off
			ZWrite[unity_GUIZTestMode]
			Blend SrcAlpha OneMinusSrcAlpha

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 2.0
				// make fog work
				#pragma multi_compile_fog

				#include "UnityCG.cginc"
				#include "UnityUI.cginc"

				struct appdata_t
				{
					float4 vertex:POSITION;
					float4 color:COLOR;
					float2 uv:TEXCOORD0;
				};
				struct v2f
				{
					float4 vertex:POSITION;
					float4 color:COLOR;
					float2 uv:TEXCOORD0;
					float4 worldPosition:TEXCOORD1;
				};

				float3 hsb2rgb(float3 c)
				{
                    float4 K=float4(1.0,2.0/3.0,1.0/3.0,3.0);
                    fixed3 p=abs(frac(c.xxx+K.xyz)*6 -K.www);
                    return c.z*lerp(K.xxx,saturate(p-K.xxx),c.y);
				}

				float3 rgb2hsb(in float3 c)
				{
                    float4 K=float4(0.0,-1.0/3.0,2.0/3.0,-1.0);
                    float4 p=lerp(float4 (c.bg,K.wz),float4(c.bg,K.xy),step(c.b,c.g));
                    float4 q=lerp(float4 (p.xyw,c.r),float4(c.r,p.yzx),step(p.x,c.r));
                    float d=q.x-min(q.w,q.y);
                    float e=1.0e-10;
                    return fixed3(abs((q.z+q.w-q.y)/(6.0*d+e)),d/(q.x+e),q.x);

				}

				v2f vert(appdata_t i)
				{
					v2f o;
					o.worldPosition = i.vertex;
					o.vertex = UnityObjectToClipPos(i.vertex);
					o.uv = i.uv;
					o.color = i.color;
					return o;
				}
				sampler2D _MainTex;
				float _Intensity;
				fixed4 _Color;
				fixed4 frag(v2f i) : SV_Target
				{
					float4 texCol = tex2D(_MainTex,i.uv);
                    float3 col=rgb2hsb(_Color.xyz);
                    col.z=_Intensity;
                    float3 c_col= hsb2rgb(col);
					texCol.xyz=c_col;
                    texCol.w=texCol.w* clamp((1.0-i.uv.y)*(1.0-i.uv.y)*(1.0-i.uv.y)*3,0,1);
                    return texCol;
				}
				ENDCG
			}
		}
}
