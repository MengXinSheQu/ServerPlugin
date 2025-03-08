using Exiled.API.Features;

namespace ServerPlugin
{
    public class Plugin : Plugin<Config>
    {
        public const string Package = "caixukun.steve.serverplugin";
        public static Plugin Ins { get; } = new Plugin();
        public static string PluginVersion { get; } = "1.0.1";
        public override string Author { get; } = "萌新社区开发团队";
        public override string Name { get; } = "ServerPlugin";
        public EventHandlers EventHandlers { get; } = new EventHandlers();
        public override void OnEnabled() => EventHandlers.RegEvent();
        public override void OnDisabled() => EventHandlers.UnRegEvent();
    }
}