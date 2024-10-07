Shader "Custom/shader1"
{
    Properties
    {
        _ColorTex ("Color Texture (with Alpha)", 2D) = "white" {}  // Textura principal com alfa
        _SandTex ("Sand Texture", 2D) = "white" {}                 // Textura de areia
        _MaskTex ("Mask Texture", 2D) = "white" {}                 // Máscara
        _SandTiling ("Sand Texture Tiling", Vector) = (1, 1, 0, 0) // Escala da textura de areia (X e Y)
        _SandColor ("Sand Color", Color) = (0.1, 0.2, 0.3, 1)      // Cor para modificar a textura de areia
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

            // Declarando as texturas
            sampler2D _ColorTex;  // Textura de cor com alfa
            sampler2D _SandTex;   // Textura de areia
            sampler2D _MaskTex;   // Máscara

            float2 _SandTiling;   // Escala da textura de areia
            fixed4 _SandColor;    // Cor para modificar a textura de areia

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Amostra a textura de cor
                fixed4 colorTex = tex2D(_ColorTex, i.uv);

                // Ajusta a UV da textura de areia para fazer o tiling
                float2 sandUV = i.uv * _SandTiling;

                // Amostra a textura de areia e multiplica pela cor customizada
                fixed4 sandTex = tex2D(_SandTex, sandUV) * _SandColor;

                // Amostra a máscara
                fixed4 maskTex = tex2D(_MaskTex, i.uv);

                // Mistura a textura de areia e a de cor com base na máscara e no canal alfa
                fixed4 finalColor = lerp(sandTex, colorTex, maskTex.r * colorTex.a);

                return finalColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
