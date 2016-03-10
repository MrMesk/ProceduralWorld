// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4795,x:32716,y:32678,varname:node_4795,prsc:2|emission-7509-OUT,alpha-6691-OUT;n:type:ShaderForge.SFN_DepthBlend,id:6691,x:32533,y:33138,varname:node_6691,prsc:2|DIST-3484-OUT;n:type:ShaderForge.SFN_Slider,id:3484,x:32156,y:33232,ptovrint:False,ptlb:Blend,ptin:_Blend,varname:node_3484,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Lerp,id:7509,x:31882,y:32948,varname:node_7509,prsc:2|A-2108-RGB,B-3097-RGB,T-9137-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:692,x:31439,y:33094,varname:node_692,prsc:2;n:type:ShaderForge.SFN_ObjectPosition,id:516,x:31439,y:33195,varname:node_516,prsc:2;n:type:ShaderForge.SFN_Add,id:9978,x:31628,y:33120,varname:node_9978,prsc:2|A-692-Y,B-516-Y,C-3014-OUT;n:type:ShaderForge.SFN_Color,id:2108,x:31457,y:32706,ptovrint:False,ptlb:Top,ptin:_Top,varname:node_2108,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Color,id:3097,x:31457,y:32870,ptovrint:False,ptlb:Bot,ptin:_Bot,varname:node_3097,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:3014,x:31408,y:33406,ptovrint:False,ptlb:Y Offset,ptin:_YOffset,varname:node_3014,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:9137,x:31842,y:33120,varname:node_9137,prsc:2|A-9978-OUT,B-8054-OUT;n:type:ShaderForge.SFN_Slider,id:8054,x:31681,y:33358,ptovrint:False,ptlb:Gradient,ptin:_Gradient,varname:node_8054,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:2;proporder:3484-2108-3097-3014-8054;pass:END;sub:END;*/

Shader "Shader Forge/sea2" {
    Properties {
        _Blend ("Blend", Range(0, 1)) = 1
        _Top ("Top", Color) = (0.5,0.5,0.5,1)
        _Bot ("Bot", Color) = (0.5,0.5,0.5,1)
        _YOffset ("Y Offset", Float ) = 0
        _Gradient ("Gradient", Range(0, 2)) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _CameraDepthTexture;
            uniform float _Blend;
            uniform float4 _Top;
            uniform float4 _Bot;
            uniform float _YOffset;
            uniform float _Gradient;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float4 projPos : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                float4 objPos = mul ( _Object2World, float4(0,0,0,1) );
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 objPos = mul ( _Object2World, float4(0,0,0,1) );
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
////// Lighting:
////// Emissive:
                float3 emissive = lerp(_Top.rgb,_Bot.rgb,((i.posWorld.g+objPos.g+_YOffset)*_Gradient));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,saturate((sceneZ-partZ)/_Blend));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
