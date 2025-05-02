using Exiled.API.Interfaces;
using System.ComponentModel;

namespace ServerPlugin
{
    public class Config : IConfig
    {
        [Description("Enabled Plugin / 启动插件")]
        public bool IsEnabled { get; set; } = true;
        [Description("Enabled Debug / 启动DeBug")]
        public bool Debug { get; set; } = false;
        [Description("SCP-207 & SCP-1853 no hurt /SCP-1853不中毒")]
        public bool DisablePoison { get; set; } = true;
        [Description("SCP-207 no hurt / SCP-207不中毒")]
        public bool DisableSCP207Hurt { get; set; } = true;
        [Description("Inf Ammo / 无限子弹")]
        public bool InfAmmo { get; set; } = true;
        [Description("No Fog / 无迷雾")]
        public bool NoFog { get; set; } = true;
        [Description("Max count to take SCP330 / 拿糖最大数量")]
        public ushort Candys { get; set; } = 2;
        [Description("RemoteKeycard / 免手持卡")]
        public bool RemoteKeycard { get; set; } = true;
        [Description("SCP Can RemoteKeycard / 免手持卡作用于SCP")]
        public bool RemoteKeycard_Scp { get; set; } = false;
        [Description("Specator can use .bc / 旁观者可使用.bc")]
        public bool SpecatorChat { get; set; } = false;
        [Description("Mute player can use .bc / 旁观者可使用.bc")]
        public bool MuteChat { get; set; } = false;
    }
}
