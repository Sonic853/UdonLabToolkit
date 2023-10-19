
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class TextureSpliter2 : UdonSharpBehaviour
    {
        /// <summary>
        /// 需要分割的图集
        /// </summary>
        [Header("需要分割的图集")]
        public Texture2D content;
        /// <summary>
        /// 图集横排数量
        /// </summary>
        [Header("图集横排数量")]
        public int atlasWidth = 1;
        /// <summary>
        /// 图集纵排数量
        /// </summary>
        [Header("图集纵排数量")]
        public int atlasHeight = 1;
        /// <summary>
        /// 分割后的贴图
        /// </summary>
        [Header("分割后的贴图")]
        public Texture2D[] textures;
        /// <summary>
        /// 是否在启动时自动分割
        /// </summary>
        [Header("是否在启动时自动分割")]
        public bool loadOnStart = true;
        void Start()
        {
            if (loadOnStart)
                SpliteTexture();
        }
        public bool SpliteTexture()
        {
            if (content == null) return false;
            if (textures.Length != atlasWidth * atlasHeight)
                textures = new Texture2D[atlasWidth * atlasHeight];
            if (atlasWidth > 1 || atlasHeight > 1)
            {
                var atlaswh = atlasWidth * atlasHeight;
                for (int i = 0; i < atlaswh; i++)
                {
                    var uv = new Rect(
                        i % atlasWidth / (float)atlasWidth,
                        1 - i / atlasWidth / (float)atlasHeight - 1f / atlasHeight,
                        1f / atlasWidth,
                        1f / atlasHeight
                    );
                    var sprite = Sprite.Create(
                        content,
                        new Rect(
                            uv.x * content.width,
                            uv.y * content.height,
                            uv.width * content.width,
                            uv.height * content.height
                        ),
                        new Vector2(0.5f, 0.5f),
                        100,
                        0,
                        SpriteMeshType.FullRect,
                        new Vector4(0, 0, 0, 0),
                        false
                    );
                    textures[i] = sprite.texture;
                }
            }
            else
            {
                textures[0] = content;
            }
            return true;
        }
    }
}
