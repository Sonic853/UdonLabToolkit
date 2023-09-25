
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class TextureSpliter : UdonSharpBehaviour
    {
        public Texture2D content;
        public ImageDisplayer[] displayer;
        public bool SpliteTexture()
        {
            if (content == null) return false;
            foreach (var item in displayer)
            {
                if (item == null
                || item.atlasWidth < 1
                || item.atlasHeight < 1)
                    continue;
                if (item.atlasWidth > 1 || item.atlasHeight > 1)
                {
                    // 根据 item 里的图集参数计算 UV
                    var atlasIndex = item.atlasIndex >= item.atlasWidth * item.atlasHeight ? 0 : item.atlasIndex;
                    // 图集中的第几张图（从 0 开始，从左上到右下）
                    var uv = new Rect(
                        atlasIndex % item.atlasWidth / (float)item.atlasWidth,
                        1 - atlasIndex / item.atlasWidth / (float)item.atlasHeight - 1f / item.atlasHeight,
                        1f / item.atlasWidth,
                        1f / item.atlasHeight
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
                    item.SetSprite(sprite);
                }
                else
                {
                    item.SetTexture(content);
                }
            }
            return true;
        }
    }
}
