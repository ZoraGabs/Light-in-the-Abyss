Shader "Custom/shader_brilho_lança"
{
    Properties
    {
        _Color ("Base Color", Color) = (0.5, 0.5, 0.5, 1)
        _EmissionColor ("Emission Color", Color) = (0.0, 0.0, 1.0, 1.0)
        _EmissionIntensity ("Emission Intensity", Range(0, 5)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _Color;
        fixed4 _EmissionColor;
        float _EmissionIntensity;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Cor base do objeto
            o.Albedo = _Color.rgb;

            // Aplicando a cor de emissão
            o.Emission = _EmissionColor.rgb * _EmissionIntensity;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
