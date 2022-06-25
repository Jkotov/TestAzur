Shader "Custom/Water"
{
     Properties
    {
        [HDR]Color0("Color0", Color) = (1,1,1,1)
    	[HDR]Color1("Color1", Color) = (1,1,1,1)
    	Speed("Speed", float) = 1
    	Zoom("Zoom", float) = 4
    }
   
    CGINCLUDE
    #include "UnityCG.cginc"
 
    half4 Color0;
    half4 Color1;
    half Speed;
    half Zoom;
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
 
    v2f vert (appdata v)
    {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.uv = v.uv;
        return o;
    }
	half2 random2(half2 p)
    {
		return frac(sin(dot(p,half2(12.9898, 78.233))))*43458.5453;
	}
	half4 frag(v2f i) : SV_Target
	{
		half2 uv = i.uv;
		uv *= Zoom;
		half2 iuv = floor(uv);
		half2 fuv = frac(uv);
		half minDist = 1.0;
		for (int y = -1; y <= 1; y++)
		{
			for (int x = -1; x <= 1; x++)
			{
				half2 neighbour = float2(float(x), float(y));
				half2 pos = random2(iuv + neighbour);
				pos = 0.5 + 0.5 * sin(_Time * Speed + 6.2236 * pos);
				half2 diff = neighbour + pos - fuv;
				half dist = length(diff);
				minDist = min(minDist, dist);
			}
		}
		half4 color = lerp(Color0, Color1, minDist * minDist);
		return color;
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
       
    }
}
