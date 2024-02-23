using UnityEditor;
using UnityEngine;

namespace FiveBabbittGames
{
    public static class UnityEditorBackgroundColor
    {

        static readonly Color defaultColor = new Color(0.2196f, 0.2196f, 0.2196f);

        static readonly Color selectedColor = new Color(0.1725f, 0.3647f, 0.5294f);

        static readonly Color selectedUnfocusedColor = new Color(0.3f, 0.3f, 0.3f);

        static readonly Color hoveredColor = new Color(0.2706f, 0.2706f, 0.2706f);

        public static Color Get(bool isSelected, bool isHovered, bool isWindowFocused)
        {
            if (isSelected)
            {
                if (isWindowFocused)
                {
                    return selectedColor;
                }
                else
                {
                    return selectedUnfocusedColor;
                }
            }
            else if (isHovered)
            {
                return hoveredColor;
            }
            else
            {
                return defaultColor;
            }
        }
    }
}
