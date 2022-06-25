Shader "Custom/GradientWithShadows"
{
     Properties
    {
        [HDR]Color0("Color0", Color) = (1,1,1,1)
        [HDR]Color1("Color1", Color) = (1,1,1,1)
        AnimationSpeed("AnimationSpeed", float) = 0
    }
   
    CGINCLUDE
    #include "UnityCG.cginc"
 
    half4 Color0;
    half4 Color1;
    half AnimationSpeed;
    struct appdata
    {
        half4 vertex : POSITION;
        half2 uv : TEXCOORD0;
    };
 
    struct v2f
    {
        half2 uv : TEXCOORD0;
        half4 vertex : SV_POSITION;
    };
 
    v2f vert (appdata v)
    {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.uv = v.uv;
        return o;
    }
   
    half4 frag (v2f i) : SV_Target
    {
        return lerp(Color0, Color1, i.uv.y + (sin(AnimationSpeed * _Time) + 1.0) * .1 );
    }
    struct v2fShadow {
        V2F_SHADOW_CASTER;
        UNITY_VERTEX_OUTPUT_STEREO
    };
 
    v2fShadow vertShadow( appdata_base v )
    {
        v2fShadow o;
        UNITY_SETUP_INSTANCE_ID(v);
        UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
        TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
        return o;
    }
 
    float4 fragShadow( v2fShadow i ) : SV_Target
    {
        SHADOW_CASTER_FRAGMENT(i)
    }
   
    ENDCG
       
    SubShader
    {
        Pass
        {
            Tags { "RenderType"="Opaque" "Queue" = "Geometry" }
           
            CGPROGRAM
            #pragma target 3.0
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
       
        Pass
        {
            Tags { "LightMode" = "ShadowCaster" }
           
            CGPROGRAM
            #pragma vertex vertShadow
            #pragma fragment fragShadow
            #pragma target 3.0
            #pragma multi_compile_shadowcaster
            ENDCG
        }
    }
}