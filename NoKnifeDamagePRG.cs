using System;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;


namespace NoKnifeDamagePRG;


public class NoKnifeDamagePRG : BasePlugin
{
    public override string ModuleAuthor => "sphaxa";
    public override string ModuleName => "NoKnifeDamage for PRG";
    public override string ModuleVersion => "v0.0.1";

    public override void Load(bool hotReload)
    {
        RegisterEventHandler<EventPlayerHurt>(OnPlayerHurt, HookMode.Pre);
    }

    private HookResult OnPlayerHurt(EventPlayerHurt @event, GameEventInfo info)
    {
        if (!@event.Userid.IsValid)
        {
            return HookResult.Continue;
        }

        CCSPlayerController player = @event.Userid;

        if (player.Connected != PlayerConnectedState.PlayerConnected)
        {
            return HookResult.Continue;
        }

        if (!player.PlayerPawn.IsValid)
        {
            return HookResult.Continue;
        }

        Console.WriteLine(@event.Weapon);

        if (@event.Weapon == "knife")
        {
            //@event.Health = @event.Health + @event.DmgHealth;
            //@event.Armor = @event.Armor + @event.DmgArmor;
            //@event.DmgArmor = 0;
            //@event.DmgHealth = 0;
            if (@event.Userid.PlayerPawn.Value.Health + @event.DmgHealth <= 100)
            {
                @event.Userid.PlayerPawn.Value.Health = @event.Userid.PlayerPawn.Value.Health + @event.DmgHealth;
            } else
            {
                @event.Userid.PlayerPawn.Value.Health = 100;
            }
            // TODO INVESTIGATE SLOWDOWN
        }

        return HookResult.Continue;
    }
}