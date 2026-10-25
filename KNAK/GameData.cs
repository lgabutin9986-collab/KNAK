using System;

namespace KNAK
{
    public static class GameData
    {
        // 1. Pera ng player (Magagamit sa Main Game at Shop)
        public static int PlayerMoney { get; set; } = 1500;

        // 2. Mga Upgrades sa Shop (False pa habang hindi pa binibili)
        public static bool HasDoubleBurner { get; set; } = false;
        public static bool HasHeavyDeepFryer { get; set; } = false;
        public static bool HasCommercialDispenser { get; set; } = false;
        public static bool HasPremiumBeef { get; set; } = false;

        // 3. Kasalukuyang Araw sa Campaign Mode (Day 1 hanggang 7)
        public static int CurrentDay { get; set; } = 1;
    }
}