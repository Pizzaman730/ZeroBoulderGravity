using BepInEx;
using BoplFixedMath;
using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace ZeroGravityBoulder
{
    [BepInPlugin("com.PizzaMan730.ZeroGravityBoulder", "ZeroGravityBoulder", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Logger.LogInfo("ZeroGravityBoulder has loaded!");

            Harmony harmony = new Harmony("com.PizzaMan730.ZeroGravityBoulder");


            MethodInfo original = AccessTools.Method(typeof(Boulder), "Drop");
            MethodInfo patch = AccessTools.Method(typeof(myPatches), "Drop_Patch");
            harmony.Patch(original, new HarmonyMethod(patch));
        }

        public class myPatches
        {
	        public static bool Drop_Patch(ref DPhysicsRoundedRect ___hitbox, ref Fix ___bouncinessWhenDropped01)
	        {
	        	if (___hitbox == null || ___hitbox.IsDestroyed || !___hitbox.initHasBeenCalled)
	        	{
	        		return false;
	        	}
	        	___hitbox.SetGravityScale(Fix.Zero);
	        	___hitbox.SetBounciness(___bouncinessWhenDropped01);
                return false;
            }
        }
    }    
}