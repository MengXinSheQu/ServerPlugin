using Exiled.API.Features;

namespace ServerPlugin
{
    public class Plugin : Plugin<Config>
    {
        public const string Package = "steve.serverplugin";
        internal static Plugin Instance { get; private set; }
        public static string PluginVersion { get; } = "1.0.1";
        public override string Author { get; } = "萌新社区开发团队";
        public override string Name { get; } = "ServerPlugin";
        internal EventHandlers EventHandlers { get; } = new EventHandlers();
        public override void OnEnabled() 
        {
            Instance = this;
            EventHandlers.RegEvent(); 
        }
        public override void OnDisabled()
        {
            EventHandlers.UnRegEvent();
            Instance = null;
        }
    }
}