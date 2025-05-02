using CommandSystem;
using Exiled.API.Features;
using RemoteAdmin;
using System;
using System.Linq;

namespace ServerPlugin.Command
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class AdminChat : ICommand,IUsageProvider
    {
        public string Command { get; } = "ac";
        public string[] Aliases { get; } = new string[1] { "ac" };
        public string Description { get; } = "呼叫管理员(不要使用该指令骚扰管理员)";
        public string[] Usage { get; } = new string[] { "*文本" };
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender is PlayerCommandSender playerCommandSender)
            {
                int AdminCount = Player.List.Where(x => x.RemoteAdminAccess).ToList().Count;
                Player player = Player.Get(playerCommandSender.PlayerId);
                if (player.IsVerified && player != null)
                {
                    if (arguments.Count > 0)
                    {
                        if (player.RemoteAdminAccess)
                        {
                            response = "<color=red>管理不能呼叫管理！</color>";
                            return true;
                        }
                        if (AdminCount != 0)
                        {
                            foreach (Player AdminPlayer in Player.List.Where(x => x.RemoteAdminAccess))
                                AdminPlayer.Broadcast(5, $"<size=55%><color=#00FFFF>[管理呼叫]</color> {player.Nickname} : {string.Join(" ", arguments)}</size>");
                            Log.Info($"[Plugin-Call]-[呼叫][{player.Nickname}][{player.UserId}]:[{string.Join(" ", arguments)}]");
                        }
                        else
                        {
                            Log.Info($"[Plugin-Call]-[未响应呼叫][{player.Nickname}][{player.UserId}]:[{string.Join(" ", arguments)}]");
                            response = "<color=red>服务器暂无管理员!</color>";
                            return false;
                        }
                    }
                    else
                    {
                        response = "<color=red>请输入文本!</color>";
                        return false;
                    }
                }
                else
                {
                    response = "<color=red>你还没加入服务器！</color>";
                    return false;
                }

            }
            response = "<color=green>信息已发送!</color>";
            return true;
        }
    }
}
