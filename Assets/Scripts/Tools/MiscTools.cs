using UnityEngine;

namespace Tools
{
    public static class MiscTools
    {
        public static int ConvertVector2ToIndex(Vector2 dir,int height)
        {
            return ((int) Mathf.Sign(dir.x))*Mathf.CeilToInt(Mathf.Abs(dir.x)) + 
                ((int) Mathf.Sign(dir.y))*Mathf.FloorToInt(Mathf.Abs(dir.y))*height;
        }
    }
}