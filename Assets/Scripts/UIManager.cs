using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class UIManager
    {
        /// <summary>
        /// Change Node's MiniMap color representation
        /// </summary>
        public static void ChangeUIColor(GameObject node, Color color)
        {
            var spriteRenderer = node.transform.GetComponentInChildren<SpriteRenderer>();

            if (spriteRenderer != null)
                spriteRenderer.color = color;
            else
                throw new NullReferenceException("Coudn't find SpriteRenderer in Node's GameObject");
        }
    }
}
