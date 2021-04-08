Shader "Unlit/Toon"
{
    // tutorial from https://roystan.net/articles/toon-shader.html
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [HDR]
        _MainColor ("Main Color", Color) = (0.4,0.4,0.4,1)
        [HDR]
        _AmbientColor ("Ambient Color", Color) = (0.4,0.4,0.4,1)
        [HDR]
        _SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
        _Glossiness("Glossiness", Float) = 32

        [HDR]
        _RimColor ("Rim Color", Color) = (1,1,1,1)
        _RimAmount ("Rim Amount", Range(0,1)) = 0.716
        _RimThreshold("Rim Threshold", Range(0,1)) = 0.1
    }
    SubShader
    {
        Tags 
        { 
            "LightMode" = "ForwardBase"
            "PassFlags" = "OnlyDirectional"
        }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            // for lighting
            #include "Lighting.cginc"
            // for shadow
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
                float3 worldNormal : NORMAL;
                float3 viewDir : TEXCOORD1;
                // for shadow
                SHADOW_COORDS(2)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _AmbientColor;
            float _Glossiness;
            float4 _SpecularColor;
            float4 _RimColor;
            float _RimAmount;
            float _RimThreshold;
            float4 _MainColor;

            v2f vert (appdata v)
            {
                v2f o;
                // o.pos is necessary for TRANSFER_SHADOW to work
                o.pos= UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal =UnityObjectToWorldNormal(v.normal); 
                o.viewDir = WorldSpaceViewDir(v.vertex);
                // for shadow
                TRANSFER_SHADOW(o)
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {

                // Calculate normal and light direction
                float3 normal = normalize(i.worldNormal);
                float NdotL = dot(_WorldSpaceLightPos0, normal);

                // shadow 
                float shadow = SHADOW_ATTENUATION(i);

                 float attenuation = LIGHT_ATTENUATION(i); 
                // calculate light intensity
                float lightIntensity = smoothstep(0, 0.01, NdotL * shadow);
                float4 light = lightIntensity * _LightColor0  ;

                float3 viewDir = normalize(i.viewDir);

                // calculate specular 
                float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
                float NdotH = dot(normal, halfVector);

                float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness );
                float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
                float4 specular =specularIntensitySmooth * _SpecularColor * _LightColor0; 

                // Rim lighting
                float4 rimDot = 1 - dot(viewDir, normal);
                float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
                rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
                float4 rim = rimIntensity * _RimColor  * _LightColor0;
    
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col * (light + _AmbientColor + specular + rim) * _MainColor;
            }
            ENDCG
        }
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}
