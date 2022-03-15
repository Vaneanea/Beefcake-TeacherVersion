Shader "Custom/PizzaCar_CleanableShader"
{
    Properties
    {
       _Color("Color", Color) = (1,1,1,1)
        _CleanTex("Clean Texture", 2D) = "white" {}
        _DirtTex("Dirt Texture", 2D) = "white"
        _SplatTex("Splat Map", 2D) = "white"
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _CleanTex;
        sampler2D _DirtTex;
        sampler2D _SplatTex;

        struct Input
        {
            float2 uv_CleanTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 dirtTex = tex2D(_CleanTex, IN.uv_CleanTex);
            fixed4 cleanTex = tex2D(_DirtTex, IN.uv_CleanTex);
            fixed4 splatTex = tex2D(_SplatTex, IN.uv_CleanTex);

            o.Albedo = (dirtTex.rgba * splatTex.r) + (cleanTex.rgba * (1.0 - splatTex.r));
        }
        ENDCG
    }
    FallBack "Diffuse"
}
