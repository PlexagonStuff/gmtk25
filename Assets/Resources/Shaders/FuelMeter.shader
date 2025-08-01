Shader "Unlit/FuelMeter"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _FuelLevel ("Fuel Level", Float) = 0.0
        _MinPercent ("Clamp Color Min", Float) = 0.0
        _MaxPercent ("Clamp Color Max", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float _FuelLevel;
            float _MinPercent;
            float _MaxPercent;

            struct meshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float ilerp(float a, float b, float v)
            {
                return (v-a)/(b-a);
            }


            v2f vert (meshData v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 mask = i.uv.y < _FuelLevel;

                clip(mask - 0.5);

                float t = ilerp(_MinPercent, _MaxPercent, _FuelLevel);
                float4 col = mask*saturate(lerp(float4(1, 0, 0, 1), float4(0, 1, 0, 1), t));

                float sideFade = 1 - 2*abs(0.5 - i.uv.x);
                sideFade = saturate(sideFade + 0.25);
                return sideFade * col;
            }
            ENDCG
        }
    }
}
