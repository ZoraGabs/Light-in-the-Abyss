Shader "Custom/shader_plane"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}        // Textura principal
        _Tiling ("Tiling", Vector) = (1, 1, 0, 0)    // Escala da textura (X e Y)
        _Color ("Color", Color) = (0.1, 0.2, 0.3, 1) // Cor para modificar a textura
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

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

            // Declarando a textura e a cor
            sampler2D _MainTex;  // Textura principal
            float2 _Tiling;      // Escala da textura
            fixed4 _Color;       // Cor para multiplicar a textura

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Ajusta a UV para fazer o tiling
                float2 uvTiled = i.uv * _Tiling;

                // Amostra a textura e multiplica pela cor
                fixed4 texColor = tex2D(_MainTex, uvTiled) * _Color;

                return texColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
