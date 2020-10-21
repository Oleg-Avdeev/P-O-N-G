Shader "Unlit/HSL"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float3 hue2rgb(float hue) {
                hue = frac(hue); //only use fractional part of hue, making it loop
                float r = abs(hue * 6 - 3) - 1; //red
                float g = 2 - abs(hue * 6 - 2); //green
                float b = 2 - abs(hue * 6 - 4); //blue
                float3 rgb = float3(r,g,b); //combine components
                rgb = saturate(rgb); //clamp between 0 and 1
                return rgb;
            }

            fixed4 frag(v2f i) : SV_TARGET{
            	float3 col = hue2rgb(i.uv);
            	return float4(col, 1);
            }
            ENDCG
        }
    }
}
