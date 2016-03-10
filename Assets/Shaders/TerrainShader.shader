// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:32464,y:32881,varname:node_4013,prsc:2|diff-9436-OUT;n:type:ShaderForge.SFN_VertexColor,id:3434,x:31377,y:33218,varname:node_3434,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:1675,x:31415,y:32705,ptovrint:False,ptlb:SEA,ptin:_SEA,varname:node_1675,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b66bceaf0cc0ace4e9bdc92f14bba709,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:7086,x:31415,y:32895,ptovrint:False,ptlb:BEACH,ptin:_BEACH,varname:node_7086,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e6e7abfd01541984db62a8d677d6b2d7,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:9397,x:31641,y:33062,ptovrint:False,ptlb:GRASSLAND,ptin:_GRASSLAND,varname:node_9397,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:448,x:31856,y:33172,ptovrint:False,ptlb:MOUNTAIN,ptin:_MOUNTAIN,varname:node_448,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:335d39ac920954c01ac193e693291d4a,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:7016,x:31641,y:32854,varname:node_7016,prsc:2|A-1675-RGB,B-7086-RGB,T-3434-G;n:type:ShaderForge.SFN_Lerp,id:7697,x:31856,y:32954,varname:node_7697,prsc:2|A-7016-OUT,B-9397-RGB,T-3434-B;n:type:ShaderForge.SFN_Lerp,id:9436,x:32102,y:33070,varname:node_9436,prsc:2|A-7697-OUT,B-448-RGB,T-3434-A;proporder:1675-7086-9397-448;pass:END;sub:END;*/

Shader "Shader Forge/TerrainShader" {
    Properties {
        _SEA ("SEA", 2D) = "white" {}
        _BEACH ("BEACH", 2D) = "white" {}
        _GRASSLAND ("GRASSLAND", 2D) = "white" {}
        _MOUNTAIN ("MOUNTAIN", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _SEA; uniform float4 _SEA_ST;
            uniform sampler2D _BEACH; uniform float4 _BEACH_ST;
            uniform sampler2D _GRASSLAND; uniform float4 _GRASSLAND_ST;
            uniform sampler2D _MOUNTAIN; uniform float4 _MOUNTAIN_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _SEA_var = tex2D(_SEA,TRANSFORM_TEX(i.uv0, _SEA));
                float4 _BEACH_var = tex2D(_BEACH,TRANSFORM_TEX(i.uv0, _BEACH));
                float4 _GRASSLAND_var = tex2D(_GRASSLAND,TRANSFORM_TEX(i.uv0, _GRASSLAND));
                float4 _MOUNTAIN_var = tex2D(_MOUNTAIN,TRANSFORM_TEX(i.uv0, _MOUNTAIN));
                float3 diffuseColor = lerp(lerp(lerp(_SEA_var.rgb,_BEACH_var.rgb,i.vertexColor.g),_GRASSLAND_var.rgb,i.vertexColor.b),_MOUNTAIN_var.rgb,i.vertexColor.a);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _SEA; uniform float4 _SEA_ST;
            uniform sampler2D _BEACH; uniform float4 _BEACH_ST;
            uniform sampler2D _GRASSLAND; uniform float4 _GRASSLAND_ST;
            uniform sampler2D _MOUNTAIN; uniform float4 _MOUNTAIN_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _SEA_var = tex2D(_SEA,TRANSFORM_TEX(i.uv0, _SEA));
                float4 _BEACH_var = tex2D(_BEACH,TRANSFORM_TEX(i.uv0, _BEACH));
                float4 _GRASSLAND_var = tex2D(_GRASSLAND,TRANSFORM_TEX(i.uv0, _GRASSLAND));
                float4 _MOUNTAIN_var = tex2D(_MOUNTAIN,TRANSFORM_TEX(i.uv0, _MOUNTAIN));
                float3 diffuseColor = lerp(lerp(lerp(_SEA_var.rgb,_BEACH_var.rgb,i.vertexColor.g),_GRASSLAND_var.rgb,i.vertexColor.b),_MOUNTAIN_var.rgb,i.vertexColor.a);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
