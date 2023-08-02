using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class LogColor
{
    public enum EColor : uint
    {
        light_purple = 0xE9BEFFFF,
        purple = 0xCD69FFFF,
        deep_purple = 0xAA00FFFF,

        light_red = 0xFFB7B7FF,
        red = 0xFF6F6FFF,
        deep_red = 0xFF0000FF,

        light_green = 0xC0FFCDFF,
        green = 0x64FF85FF,
        deep_green = 0x00FF36FF,

        light_blue = 0xC0CFFFFF,
        blue = 0x6E91FFFF,
        deep_blue = 0x003EFFFF,

        light_yellow = 0xFFFDAEFF,
        yellow = 0xFFFA61FF,
        deep_yellow = 0xFFF700FF,

        light_magenta = 0xFFB2F2FF,
        magenta = 0xFF54E3F,
        deep_magenta = 0xFF00D4FF
    }
    public static readonly string InputManager = Dye(EColor.yellow, "InputManager:");
    public static readonly string SaveSystem = Dye(EColor.light_red, "SaveSystem:");
    public static readonly string UIManager = Dye(EColor.deep_purple, "UIManager:");
    public static readonly string ResourceManager = Dye(EColor.light_red, "ResourceManager:");
    public static readonly string GameManager = Dye(EColor.green, "GameManager:");
    public static readonly string BuffManager = Dye(EColor.yellow, "BuffManager:");

    public static readonly string BaseFSM = Dye(EColor.magenta, "BaseFSM");
    public static readonly string EnemyFSM = Dye(EColor.deep_magenta, "EnemyFSM:");
    public static readonly string PlayerFSM = Dye(EColor.light_magenta, "PlayerFSM:");
    public static readonly string MapObjectFSM = Dye(EColor.magenta, "MapObjectFSM:");
    public static readonly string Player = Dye(EColor.blue, "Player:");
    public static readonly string Monster = Dye(EColor.red, "Monster:");
    public static string Dye(EColor color, string str)
    {
        return string.Format("<b><color=#{0:X}>{1}</color></b>", color, str);
    }
}
