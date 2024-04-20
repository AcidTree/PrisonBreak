using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;


namespace PrisonBreaks
{
    [StaticConstructorOnStartup]
    public static class main
    {
        static main()
        {
            var harmony = new Harmony("com.lessprisonbreaks");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            Log.Message("Prison breaks patched");
        }

        [HarmonyPatch(typeof(PrisonBreakUtility), nameof(PrisonBreakUtility.CanParticipateInPrisonBreak))]
        class Patch
        {
            [HarmonyPrefix]
            static bool Prefix(ref bool __result, Pawn pawn)
            {
                if (MoodThresholdExtensions.CurrentMoodThresholdFor(pawn) == MoodThreshold.None)
                {
                    __result = false;
#if DEBUG
                    Log.Message("patching prison break for: " + pawn.Name.ToString());
#endif
                    return false;
                }
                return true;
            }
        }
    }
}