﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static FunctionTesting.UtilityFunctions;

namespace FunctionTesting
{
    class Program {
        static void Main(string[] args) {
			int tabs = 6;
			List<string> headers = new List<string>();
			string localizationDataFilePath = @$"C:\Users\Isaac\Desktop\TerrariaDev\ConsoleApp1\LocalizationData.txt";
			string configFilePath = @$"C:\Users\Isaac\Desktop\TerrariaDev\ConsoleApp1\Config.txt";
			//$Mods.WeaponEnchantments.Config.individualStrengthsEnabled.Label
			string txt = "[Label(\"Server Config\")]\r\n    public class ServerConfig : ModConfig {\r\n        public override ConfigScope Mode => ConfigScope.ServerSide;\r\n\r\n        //Server Config\r\n        [Header(\"Server Config\")]\r\n        [Label(\"Presets and Multipliers\")]\r\n        [ReloadRequired]\r\n        public PresetData presetData;\r\n\r\n        [Header(\"Individual Enchantment Strengths\")]\r\n\r\n        [Label(\"Individual Strengths Enabled\")]\r\n        [Tooltip(\"Enabling this will cause the Indvidual strength values selected below to overite all other settings.\")]\r\n        [ReloadRequired]\r\n        [DefaultValue(false)]\r\n        public bool individualStrengthsEnabled;\r\n\r\n        [Label(\"Individual Strengths\")]\r\n        [Tooltip(\"Modify individual enchantment strengths by value\\n(NOT PERCENTAGE!)\\n(Overrides all other options)\")]\r\n        public List<Pair> individualStrengths = new List<Pair>();\r\n\r\n        //Enchantment Settings\r\n        [Header(\"Enchantment Settings\")]\r\n        [Label(\"Damage type converting enchantments always override.\")]\r\n        [Tooltip(\"Some mods like Stars Above change weapon damage types.  If this option is enabled, Enchantments that change the damage type will always change the weapon's damage type.\\n\" +\r\n            \"If not selected, the damage type will only be changed if the weapon is currently it's original damage type.\")]\r\n        [DefaultValue(true)]\r\n        public bool AlwaysOverrideDamageType;\r\n\r\n        [Label(\"Life Steal Enchantment limiting (Affect on Vanilla Life Steal Limit) (%)\")]\r\n        [Tooltip(\"Use a value above 100% to limmt lifesteal more, less than 100% to limit less.  0 to have not limit.\\n\" +\r\n            \"Vanilla Terraria uses a lifesteal limiting system: In the below example, the values used are in normal mode(Expert/Master mode values in parenthesis)\\n\" +\r\n            \"It has a pool of 80(70) that is saved for you to gain lifestea from.  Gaining life through lifesteal reduces this pool.\\n\" +\r\n            \"The pool is restored by 36(30) points per second.  If the pool value is negative, you cannot gain life from lifesteal.\\n\" +\r\n            \"This config value changes how much the life you heal from lifesteal enchantments affects this limit.\\n\" +\r\n            \"Example: 200%  You gain 200 life from lifesteal.  200 * 200% = 400.  80(70) pool - 400 healed = -320(-330) pool.\\n\" +\r\n            \"It will take 320/36(330/30) seconds -> 8.9(11) seconds for the pool to be positive again so you can gain life from lifesteal again.\\n\" +\r\n            \"Note: the mechanic does not have a cap on how much you can gain at once.  It will just take longer to recover the more you gain.\")]\r\n        [DefaultValue(100)]\r\n        [Range(0, 10000)]\r\n        public int AffectOnVanillaLifeStealLimmit;\r\n\r\n        [Label(\"Speed Enchantment Auto Reuse Enabled (%)\")]\r\n        [Tooltip(\"The strength that a Speed Enchantment will start giving the Auto Reuse stat.\\n\" +\r\n            \"Set to 0 for all Speed enchantments to give auto reuse.  Set to 10000 to to prevent any gaining auto reuse (unless you strength multiplier is huge)\")]\r\n        [Range(0, 10000)]\r\n        [DefaultValue(10)]\r\n        [ReloadRequired]\r\n        public int AttackSpeedEnchantmentAutoReuseSetpoint;\r\n\r\n        [Label(\"Auto Reuse Disabled on Magic Missile type weapons\")]\r\n        [Tooltip(\"Auto Reuse on weapons like Magic Missile allow you to continuously shoot the projectiles to stack up damage infinitely.\")]\r\n        [DefaultValue(true)]\r\n        [ReloadRequired]\r\n        public bool AutoReuseDisabledOnMagicMissile;\r\n\r\n        [Label(\"Buff cooldown duration (seconds)\")]\r\n        [Tooltip(\"Affects buff cooldown and duration.\")]\r\n        [DefaultValue(15)]\r\n        [Range(1, 600)]\r\n        [ReloadRequired]\r\n        public int BuffDuration;\r\n\r\n        [Label(\"Amaterasu Self Growth Per Tick\")]\r\n        [Tooltip(\"Affects how quickly Amaterasu damage will go up naturally (Not when being hit again with a World Ablaze weapon.)\")]\r\n        [DefaultValue(5)]\r\n        [Range(0, 1000000)]\r\n        public int AmaterasuSelfGrowthPerTick;\r\n\r\n        [Label(\"Reduce recipes to minimum.\")]\r\n        [Tooltip(\"Removes all recipes that jump between tiers to reduce clutter when viewing recipes.\\n\" +\r\n            \"Also makes all essence recipes 4 to 1 instead of scaling with enchanting table tier.\")]\r\n        [DefaultValue(false)]\r\n        [ReloadRequired]\r\n        public bool ReduceRecipesToMinimum;\r\n\r\n        [Label(\"Enchantment Capacity Cost Multiplier(%)\")]\r\n        [Tooltip(\"Affects how much the enchantments cost to apply to an item.  Base values are 1/2/3/4/5 for utility, 2/4/6/8/10 for normal and 3/6/9/12/15 for unique.\")]\r\n        [DefaultValue(100)]\r\n        [Range(0, 1400)]\r\n        [ReloadRequired]\r\n        public int ConfigCapacityCostMultiplier;\r\n\r\n        [Label(\"Remove enchantment restrictions (Use at your own risk!)\")]\r\n        [Tooltip(\"Removes things like Unique, Max 1 and weapon or item type specific enchantments.\")]\r\n        [DefaultValue(false)]\r\n        [ReloadRequired]\r\n        public bool RemoveEnchantmentRestrictions;\r\n\r\n        //Essence and Experience\r\n        [Header(\"Essence and Experience\")]\r\n        [Label(\"Boss Essence Multiplier(%)\")]\r\n        [Tooltip(\"Modify the ammount of essence recieved from bosses.\")]\r\n        [Range(0, 10000)]\r\n        [DefaultValue(100)]\r\n        [ReloadRequired]\r\n        public int BossEssenceMultiplier;\r\n\r\n        [Label(\"Non-Boss Essence Multiplier(%)\")]\r\n        [Tooltip(\"Modify the ammount of essence recieved from non-boss enemies.\")]\r\n        [Range(0, 10000)]\r\n        [DefaultValue(100)]\r\n        [ReloadRequired]\r\n        public int EssenceMultiplier;\r\n\r\n        [Label(\"Boss Experience Multiplier(%)\")]\r\n        [Tooltip(\"Modify the ammount of experience recieved from bosses.\")]\r\n        [Range(0, 10000)]\r\n        [DefaultValue(100)]\r\n        public int BossExperienceMultiplier;\r\n\r\n        [Label(\"Non-Boss Experience Multiplier(%)\")]\r\n        [Tooltip(\"Modify the ammount of experience recieved from non-boss enemies.\")]\r\n        [Range(0, 10000)]\r\n        [DefaultValue(100)]\r\n        public int ExperienceMultiplier;\r\n\r\n        [Label(\"Gathering Experience Multiplier(%)\")]\r\n        [Tooltip(\"Modify the ammount of experience recieved from Mining/chopping/fishing\")]\r\n        [Range(0, 10000)]\r\n        [DefaultValue(100)]\r\n        public int GatheringExperienceMultiplier;\r\n\r\n        [Label(\"Essence Grab Range Multiplier\")]\r\n        [Tooltip(\"Affects how far the essence can be away from the player when it starts moving towards the player.\")]\r\n        [DefaultValue(10)]\r\n        [Range(1, 100)]\r\n        public int EssenceGrabRange;\r\n\r\n        //Enchantment Drop Rates(%)\r\n        [Header(\"Enchantment Drop Rates(%)\")]\r\n        [Label(\"Boss Enchantment Drop Rate(%)\")]\r\n        [Tooltip(\"Adjust the drop rate of enchantments from bosses.\\n(Default is 50%)\")]\r\n        [Range(0, 100)]\r\n        [DefaultValue(50)]\r\n        [ReloadRequired]\r\n        public int BossEnchantmentDropChance;\r\n\r\n        [Label(\"Non-Boss Enchantment Drop Rate(%)\")]\r\n        [Tooltip(\"Adjust the drop rate of enchantments from non -boss enemies.\\n(Default is 100%)\")]\r\n        [Range(0, 1000)]\r\n        [DefaultValue(100)]\r\n        [ReloadRequired]\r\n        public int EnchantmentDropChance;\r\n\r\n        [Label(\"Chest Enchantment Spawn Chance(%)\")]\r\n        [Tooltip(\"Adjust the chance of finding enchantments in chests.  Can be over 100%.  Does not affect Biome chests.(They are always 100%)\")]\r\n        [Range(0, 100000)]\r\n        [DefaultValue(50)]\r\n        public int ChestSpawnChance;\r\n\r\n        [Label(\"Crate Enchantment Drop Chance Multiplier(%)\")]\r\n        [Tooltip(\"Adjust the chance of finding enchantments in fishing crates.\")]\r\n        [Range(0, 10000)]\r\n        [DefaultValue(100)]\r\n        public int CrateDropChance;\r\n\r\n        //Other Drop Rates\r\n        [Header(\"Other Drop Rates\")]\r\n        [Label(\"Prevent pre-hard mode bosses from dropping power boosters.\")]\r\n        [Tooltip(\"Does not include wall of flesh.\")]\r\n        [DefaultValue(true)]\r\n        [ReloadRequired]\r\n        public bool PreventPowerBoosterFromPreHardMode;\r\n\r\n        //Enchanting Table Options\r\n        [Header(\"Enchanting Table Options\")]\r\n        [Label(\"Recieve ores up to Chlorophyte from Offering items.\")]\r\n        [Tooltip(\"Disabling this option only allows you to recieve Iron, Silver, Gold (Or their equivelents based on world gen.).\\n\" +\r\n            \"(Only Works in hard mode.  Chlorophyte only after killing a mechanical boss.)\")]\r\n        [DefaultValue(true)]\r\n        public bool AllowHighTierOres;\r\n\r\n        [Label(\"Enchantment Slots On Weapons\")]\r\n        [Tooltip(\"1st slot is a normal slot.\\n\" +\r\n            \"2nd slot is the utility slot.\\n\" +\r\n            \"3rd-5th are normal slots.\")]\r\n        [DefaultValue(5)]\r\n        [Range(0, 5)]\r\n        [ReloadRequired]\r\n        public int EnchantmentSlotsOnWeapons;\r\n\r\n        [Label(\"Enchantment Slots On Armor\")]\r\n        [Tooltip(\"1st slot is a normal slot.\\n\" +\r\n            \"2nd slot is the utility slot.\\n\" +\r\n            \"3rd-5th are normal slots.\")]\r\n        [DefaultValue(3)]\r\n        [Range(0, 5)]\r\n        [ReloadRequired]\r\n        public int EnchantmentSlotsOnArmor;\r\n\r\n        [Label(\"Enchantment Slots On Accissories\")]\r\n        [Tooltip(\"1st slot is a normal slot.\\n\" +\r\n            \"2nd slot is the utility slot.\\n\" +\r\n            \"3rd-5th are normal slots.\")]\r\n        [DefaultValue(1)]\r\n        [Range(0, 5)]\r\n        [ReloadRequired]\r\n        public int EnchantmentSlotsOnAccessories;\r\n\r\n        [Label(\"Enchantment Slots On Fishing Poles\")]\r\n        [Tooltip(\"1st slot is a normal slot.\\n\" +\r\n            \"2nd slot is the utility slot.\\n\" +\r\n            \"3rd-5th are normal slots.\")]\r\n        [DefaultValue(5)]\r\n        [Range(0, 5)]\r\n        [ReloadRequired]\r\n        public int EnchantmentSlotsOnFishingPoles;\r\n\r\n        [Label(\"Enchantment Slots On Tools\")]\r\n        [Tooltip(\"1st slot is a normal slot.\\n\" +\r\n            \"2nd slot is the utility slot.\\n\" +\r\n            \"3rd-5th are normal slots.\\n\" +\r\n            \"The Clentaminator is the only tool so far.\")]\r\n        [DefaultValue(5)]\r\n        [Range(0, 5)]\r\n        [ReloadRequired]\r\n        public int EnchantmentSlotsOnTools;\r\n\r\n        [Label(\"Reduce Offer Efficiency By Table Tier\")]\r\n        [Tooltip(\"When offering items, you recieve essence equivelent to the experience on the item.\\n\" +\r\n            \"Enabling this will cause the wood table to be 60% efficient.\\n\" +\r\n            \"Each table gains 10% efficiency.  100% with Ultimate table.\")]\r\n        [DefaultValue(false)]\r\n        public bool ReduceOfferEfficiencyByTableTier;\r\n\r\n        [Label(\"Reduce Offer Efficiency By Base Infusion Power\")]\r\n        [Tooltip(\"When offering items, you recieve essence equivelent to the experience on the item.\\n\" +\r\n            \"Enabling this will cause weapons to be 100% efficient at Infusion power of 0 to 80% efficient at infusion power of 1100 (and above).\")]\r\n        [DefaultValue(false)]\r\n        public bool ReduceOfferEfficiencyByBaseInfusionPower;\r\n\r\n        //General Game Changes\r\n        [Header(\"General Game Changes\")]\r\n        [Label(\"Convert excess armor penetration to bonus damage\")]\r\n        [Tooltip(\"Example: Enemy has 4 defense, Your weapon has 10 armor penetration.\\n\" +\r\n            \"10 - 4 = 6 excess armor penetration (not doing anything)\\nGain 3 bonus damage (6/2 = 3)\")]\r\n        [DefaultValue(true)]\r\n        public bool ArmorPenetration;\r\n\r\n        [Label(\"Disable Minion Critical hits\")]\r\n        [Tooltip(\"In vanilla, minions arent affected by weapon critical chance.\\n\" +\r\n            \"Weapon Enchantments gives minions a critical hit chance based on weapon crit chance.\\n\" +\r\n            \"This option disables the crits(vanilla mechanics)\")]\r\n        [DefaultValue(false)]\r\n        public bool DisableMinionCrits;\r\n\r\n        [Label(\"Disable Weapon critical strike chance per level\")]\r\n        [Tooltip(\"Weapons gain critical strike chance equal to thier level * Global Enchantment Strength Multiplier.\")]\r\n        [DefaultValue(false)]\r\n        [ReloadRequired]\r\n        public bool CritPerLevelDisabled;\r\n\r\n        [Label(\"Damage instead of critical chance per level\")]\r\n        [Tooltip(\"Weapons gain damage per level instead of critical strike chance equal to their level * Global Enchantment Strength Multiplier\")]\r\n        [DefaultValue(false)]\r\n        [ReloadRequired]\r\n        public bool DamagePerLevelInstead;\r\n\r\n\t\t[Label(\"Disable armor and accessory damage reduction per level\")]\r\n\t\t[Tooltip(\"Armor and accessories gain damage reduction equal to thier level * the appropriate setpoint below for the world difficulty.\")]\r\n\t\t[DefaultValue(false)]\r\n\t\tpublic bool DamageReductionPerLevelDisabled;\r\n\r\n        [Label(\"Calculate Damage Reduction before player defense\")]\r\n        [Tooltip(\"By default, damage reduction is applied after player defense.  Select this to apply before.\\nBefore will cause you to take much less damage.\")]\r\n        [DefaultValue(false)]\r\n        public bool CalculateDamageReductionBeforeDefense;\r\n\r\n\t\t[ReloadRequired]\r\n        [Label(\"Armor and accessory Damage Reductions\")]\r\n        public List<ArmorDamageReduction> ArmorDamageReductions = new() { new(0), new(1), new(2), new(3) };\r\n\r\n        [Label(\"Critical hit chance effective over 100% chance\")]\r\n        [Tooltip(\"Vanilla terraria caps critical hit chance at 100%.  By default, Weapon Enchantments calculates extra crits after 100%.\\n\" +\r\n            \"120% critical chance is 100% to double the damage then 20% chance to crit to increase the damge.  See the next config option for more info.\")]\r\n        [DefaultValue(true)]\r\n        public bool AllowCriticalChancePast100;\r\n\r\n        [Label(\"Multiplicative critical hits past the first.\")]\r\n        [Tooltip(\"Weapon Enchantments makes use of critical strike chance past 100% to allow you to crit again.\\n\" +\r\n            \"By default, this is an additive bonus: 1st crit 200% damage, 2nd 300% damage, 3rd 400% damage.....\\n\" +\r\n            \"Enabling this makes them multiplicative instead: 1st crit 200% damage, 2nd crit 400% damage, 3rd crit 400% damage... \")]\r\n        [DefaultValue(false)]\r\n        public bool MultiplicativeCriticalHits;\r\n\r\n        [Label(\"Infusion Damage Multiplier (Divides by 1000, 1 -> 0.001)\")]\r\n        [DefaultValue(1300)]\r\n        [Range(1000, 2000)]\r\n        [Tooltip(\"Changes the damage multiplier from infusion.  DamageMultiplier = InfusionDamageMultiplier^((InfusionPower - BaseInfusionPower) / 100)\\n\" +\r\n\t\t\t\"Example: Iron Broadsword, Damage = 10, BaseInfusionPower = 31  infused with a Meowmere, Infusion Power 1100.\\n\" +\r\n\t\t\t\"Iron Broadsword damage = 10 * 1.3^((1100 - 31) / 100) = 10 * 1.3^10.69 = 10 * 16.52 = 165 damage.\\n\" +\r\n            \"Setting this multiplier to 1000 will prevent you from infusing weapons as well as provide no damage bonus to already infused weapons.\")]\r\n        [ReloadRequired]\r\n        public int InfusionDamageMultiplier;\r\n\r\n        [Tooltip(\"This will prevent you from infusing armor items and will ignore infused set bonues.\")]\r\n        [ReloadRequired]\r\n        [DefaultValue(false)]\r\n        public bool DisableArmorInfusion;\r\n\r\n\t\t[Label(\"Minion Life Steal Multiplier (%)\")]\r\n        [Tooltip(\"Allows you to reduce the ammount of healing recieved by minions with the Lifesteal Enchantment.\")]\r\n        [DefaultValue(50)]\r\n        [Range(0, 100)]\r\n        public int MinionLifeStealMultiplier;\r\n\r\n        //Random Extra Stuff\r\n        [Header(\"Random Extra Stuff\")]\r\n        [Label(\"Start with a Drill Containment Unit\")]\r\n        [Tooltip(\"All players will get a Drill Containment Unit when they first spawn.\\nThis is just for fun when you feel like a faster playthrough.\")]\r\n        [DefaultValue(false)]\r\n        public bool DCUStart;\r\n\r\n        [Label(\"Disable Ability to research Weapon Enchantment items\")]\r\n        [DefaultValue(false)]\r\n        [ReloadRequired]\r\n        public bool DisableResearch;\r\n\r\n        public ServerConfig() {\r\n            presetData = new PresetData();\r\n        }\r\n\r\n        [OnDeserialized]\r\n        internal void OnDeserializedMethod(StreamingContext context) {\r\n            // If you change ModConfig fields between versions, your users might notice their configuration is lost when they update their mod.\r\n            // We can use [JsonExtensionData] to capture serialized data and manually restore them to new fields.\r\n            // Imagine in a previous version of this mod, we had a field \"OldmodifiedEnchantmentStrengths\" and we want to preserve that data in \"modifiedEnchantmentStrengths\".\r\n            // To test this, insert the following into ExampleMod_ModConfigShowcase.json: \"OldmodifiedEnchantmentStrengths\": [ 99, 999],\r\n            /*if (_additionalData.TryGetValue(\"OldmodifiedEnchantmentStrengths\", out var token))\r\n            {\r\n                var OldmodifiedEnchantmentStrengths = token.ToObject<List<int>>();\r\n                modifiedEnchantmentStrengths.AddRange(OldmodifiedEnchantmentStrengths);\r\n            }\r\n            _additionalData.Clear(); // make sure to clear this or it'll crash.*/\r\n        }\r\n    }\r\n\r\n    [Label(\"ClientConfig\")]\r\n    public class ClientConfig : ModConfig {\r\n        public override ConfigScope Mode => ConfigScope.ClientSide;\r\n        //Enchanting Table Options\r\n        [Header(\"Enchanting Table Options\")]\r\n        [Label(\"Automatically send essence to UI\")]\r\n        [Tooltip(\"Automatically send essence from your inventory to the UI essence slots.\\n(Disables while the UI is open.)\")]\r\n        [DefaultValue(true)]\r\n        public bool teleportEssence;\r\n\r\n        [Label(\"Offer all of the same item.\")]\r\n        [Tooltip(\"Search your inventory for all items of the same type that was offered and offer them too if they have 0 experience and no power booster installed.\")]\r\n        [DefaultValue(false)]\r\n        public bool OfferAll;\r\n\r\n        [Label(\"Allow shift click to move favorited items into the enchanting table.\")]\r\n        [DefaultValue(false)]\r\n        public bool AllowShiftClickMoveFavoritedItems;\r\n\r\n        [Label(\"Always display Infusion Power\")]\r\n        [Tooltip(\"Enable to display item's Infusion Power always instead of just when the enchanting table is open.\")]\r\n        [DefaultValue(true)]\r\n        public bool AlwaysDisplayInfusionPower;\r\n\r\n        [Label(\"Percentage of offered Item value converted to essence.\")]\r\n        [DefaultValue(50)]\r\n        [Range(0, 100)]\r\n        public int PercentOfferEssence;\r\n\r\n        [Label(\"Allow crafting enchantments into lower tier enchantments.\")]\r\n        [DefaultValue(true)]\r\n        [ReloadRequired]\r\n        public bool AllowCraftingIntoLowerTier;\r\n\r\n        [Label(\"Allow Infusing items to lower infusion Powers\")]\r\n        [Tooltip(\"Warning: This will allow you to consume a weak weapon to downgrade a strong weapon.\")]\r\n        [DefaultValue(false)]\r\n        public bool AllowInfusingToLowerPower;\r\n\r\n        //Display Settings\r\n        [Header(\"Display Settings\")]\r\n        [Label(\"\\\"Points\\\" instead of \\\"Enchantment Capacity\\\"\")]\r\n        [Tooltip(\"Tooltips will show Points Available instead of Enchantment Capacity Available\")]\r\n        [DefaultValue(false)]\r\n        public bool UsePointsAsTooltip;\r\n\r\n        [Label(\"Use Alternate Enchantment Essence Textures\")]\r\n        [Tooltip(\"The default colors are color blind friendly.  The alternate textures have minor differences, but were voted to be kept.\")]\r\n        [DefaultValue(false)]\r\n        [ReloadRequired]\r\n        public bool UseAlternateEnchantmentEssenceTextures;\r\n\r\n        [Label(\"Display approximate weapon damage in the tooltip\")]\r\n        [Tooltip(\"Damage enchantments are calculated after enemy armor reduces damage instead of directly changing the item's damage.\\n\" +\r\n            \"This displays the damage against a 0 armor enemy.\")]\r\n        [DefaultValue(true)]\r\n        public bool DisplayApproximateWeaponDamageTooltip;\r\n\r\n        //Error messages\r\n        [Header(\"Error Messages\")]\r\n        [Label(\"Disable All Error Messages In Chat\")]\r\n        [Tooltip(\"Prevents messages showing up in your chat that ask you to: \\n\" +\r\n            \"\\\"Please report this to andro951(Weapon Enchantments) allong with a description of what you were doing at the time.\\\"\")]\r\n        [DefaultValue(false)]\r\n        public bool DisableAllErrorMessagesInChat {\r\n            set {\r\n                if (value) {\r\n                    OnlyShowErrorMessagesInChatOnce = false;\r\n                }\r\n                else {\r\n                    LogMethods.LoggedChatMessagesIDs.Clear();\r\n                }\r\n\r\n                _disableAllErrorMessagesInChat = value;\r\n            }\r\n\r\n            get => _disableAllErrorMessagesInChat;\r\n        }\r\n\r\n        [JsonIgnore]\r\n        private bool _disableAllErrorMessagesInChat;\r\n\r\n        [Label(\"OnlyShowErrorMessagesInChatOnce\")]\r\n        [Tooltip(\"Messages will continue to show up in your chat, but only once during a game session.\\n\" +\r\n            \"(The error message must be the exact same as a previous message to be prevented.)\")]\r\n        [DefaultValue(true)]\r\n        public bool OnlyShowErrorMessagesInChatOnce {\r\n            set {\r\n                if (value) {\r\n                    DisableAllErrorMessagesInChat = false;\r\n                }\r\n                else {\r\n                    LogMethods.LoggedChatMessagesIDs.Clear();\r\n                }\r\n\r\n                _onlyShowErrorMessagesInChatOnce = value;\r\n            }\r\n\r\n            get => _onlyShowErrorMessagesInChatOnce;\r\n        }\r\n\r\n        private bool _onlyShowErrorMessagesInChatOnce;\r\n\r\n        //Logging Information\r\n        [Header(\"Logging Information\")]\r\n        [Label(\"Log a List of Enchantment Tooltips\")]\r\n        [Tooltip(\"The list is printed to the client.log when you enter a world.\\nThe client.log default location is C:\\\\Steam\\\\SteamApps\\\\common\\\\tModLoader\\\\tModLoader-Logs\")]\r\n        [DefaultValue(false)]\r\n        [ReloadRequired]\r\n        public bool PrintEnchantmentTooltips;\r\n\r\n        [Label(\"Log a List of Enchantment Drop sources\")]\r\n        [Tooltip(\"The list is printed to the client.log when you enter a world.\\nThe client.log default location is C:\\\\Steam\\\\SteamApps\\\\common\\\\tModLoader\\\\tModLoader-Logs\")]\r\n        [DefaultValue(false)]\r\n        [ReloadRequired]\r\n        public bool PrintEnchantmentDrops;\r\n\r\n        [Label(\"Log all translation lists\")]\r\n        [Tooltip(\"The lists are printed to the client.log when you enter a world.\\nThe client.log default location is C:\\\\Steam\\\\SteamApps\\\\common\\\\tModLoader\\\\tModLoader-Logs\")]\r\n        [DefaultValue(false)]\r\n        [ReloadRequired]\r\n        public bool PrintLocalizationLists;\r\n\r\n        [Label(\"Log all wiki info\")]\r\n        [Tooltip(\"The info is printed to the client.log when you enter a world.\\nThe client.log default location is C:\\\\Steam\\\\SteamApps\\\\common\\\\tModLoader\\\\tModLoader-Logs\")]\r\n        [DefaultValue(false)]\r\n        [ReloadRequired]\r\n        public bool PrintWikiInfo;\r\n\r\n        [Label(\"Log all weapon infusion powers\")]\r\n        [Tooltip(\"The info is printed to the client.log when you enter a world.\\nThe client.log default location is C:\\\\Steam\\\\SteamApps\\\\common\\\\tModLoader\\\\tModLoader-Logs\")]\r\n        [DefaultValue(false)]\r\n        [ReloadRequired]\r\n        public bool PrintWeaponInfusionPowers;\r\n\r\n        //Mod Testing Tools\r\n        [Header(\"Mod Testing Tools\")]\r\n        [Label(\"Enable swapping weapons with num keys (Weapons sorted by infusion power)\")]\r\n        [Tooltip(\"Use num1 and num3 to swap between all weapons.  Use num4 and num6 to swap between only modded weapons.\\n\" +\r\n            \"Will not replace enchanted or modified weapons.\")]\r\n        [DefaultValue(false)]\r\n        public bool EnableSwappingWeapons;\r\n\r\n        [Label(\"Enable Target Dummy Dps calculation and logging\")]\r\n        [Tooltip(\"Tracks damage to targets from all sources and tracks them.  Press num0 to start then again to stop.\\n\" +\r\n            $\"Press num8 to print all stored dps values to the client.log\\\\nThe client.log default location is C:\\\\Steam\\\\SteamApps\\\\common\\\\tModLoader\\\\tModLoader-Logs\\n\" +\r\n            $\"Starting a new test by pressing num0 resets the previous dps data for the held item to allow re-doing a test.\")]\r\n\t\t[DefaultValue(false)]\r\n\t\t[ReloadRequired]\r\n\t\tpublic bool LogDummyDPS;\r\n\t}\r\n    public class Pair {\r\n        [Tooltip(\"Only Select Enchantment Items.\\nLikely to cause an error if selecting any other item.\")]\r\n        [Label(\"Enchantment\")]\r\n        [ReloadRequired]\r\n        public ItemDefinition itemDefinition;\r\n\r\n        [Label(\"Strength (1000 = 1, 10 = 1%)\")]\r\n        [Tooltip(\"Take care when adjusting this value.\\nStrength is the exact value used.\\nExample 40% Damage enchantment is 0.4\\n10 Defense is 10\")]\r\n        [Range(0, 100000)]\r\n        [ReloadRequired]\r\n        public int Strength;\r\n\r\n        public override string ToString(){\r\n            return $\"Enchantment: {(itemDefinition != null && itemDefinition.Type != 0 ? $\"{itemDefinition.Name}: {Strength / 10}%\" : \"None Selected\")}\";\r\n        }\r\n\r\n        public override bool Equals(object obj){\r\n            if (obj is Pair other)\r\n                return itemDefinition == other.itemDefinition && Strength == other.Strength;\r\n            \r\n            return base.Equals(obj);\r\n        }\r\n\r\n        public override int GetHashCode(){\r\n            return new { itemDefinition, Strength }.GetHashCode();\r\n        }\r\n    }\r\n    public class ArmorDamageReduction {\r\n\t\t[JsonIgnore]\r\n\t\tpublic static readonly int[,] DamageReductionPerLevel = {\r\n\t\t\t{ 25000, 12500 },\r\n\t\t\t{ 18750, 9375 },\r\n\t\t\t{ 12500, 6250 },\r\n\t\t\t{ 62500, 31250 },\r\n\t\t};\r\n\r\n        [JsonIgnore]\r\n        short GameModeID;\r\n\r\n        [Label(\"Armor DR Per Level (100000 = 1%)\")]\r\n        [Tooltip(\"250000 (2.5%) is the maximum which would be 100% damage reduction at level 40.\")]\r\n        [Range(0, 250000)]\r\n        public int ArmorDamageReductionPerLevel;\r\n\r\n\t\t[Label(\"Accessory DR Per Level (100000 = 1%)\")]\r\n\t\t[Tooltip(\"250000 (2.5%) is the maximum which would be 100% damage reduction at level 40.\")]\r\n\t\t[Range(0, 250000)]\r\n\t\tpublic int AccessoryDamageReductionPerLevel;\r\n\t\tpublic ArmorDamageReduction(short gameMode) {\r\n            GameModeID = gameMode;\r\n            ArmorDamageReductionPerLevel = DamageReductionPerLevel[gameMode, 0];\r\n\t\t\tAccessoryDamageReductionPerLevel = DamageReductionPerLevel[gameMode, 1];\r\n\t\t}\r\n\t\tpublic override bool Equals(object obj) {\r\n\t\t\tif (obj is ArmorDamageReduction other) {\r\n\t\t\t\tif (GameModeID != other.GameModeID)\r\n\t\t\t\t\treturn false;\r\n\r\n\t\t\t\tif (ArmorDamageReductionPerLevel != other.ArmorDamageReductionPerLevel)\r\n\t\t\t\t\treturn false;\r\n\r\n\t\t\t\tif (AccessoryDamageReductionPerLevel != other.AccessoryDamageReductionPerLevel)\r\n\t\t\t\t\treturn false;\r\n\r\n\t\t\t\treturn true;\r\n\t\t\t}\r\n\r\n\t\t\treturn base.Equals(obj);\r\n\t\t}\r\n\t\tpublic override int GetHashCode() {\r\n\t\t\treturn new {\r\n\t\t\t\tGameModeID,\r\n                ArmorDamageReductionPerLevel,\r\n                AccessoryDamageReductionPerLevel\r\n\t\t\t}.GetHashCode();\r\n\t\t}\r\n\t\tpublic override string ToString() {\r\n            return $\"{GameModeID.ToGameModeIDName()}\" +\r\n                $\", Armor {(ArmorDamageReductionPerLevel/100000f).S(5)}% ({(ArmorDamageReductionPerLevel / 2500f).S(5)}% at 40)\" +\r\n                $\", Accessory {(AccessoryDamageReductionPerLevel / 100000f).S(5)}% ({(AccessoryDamageReductionPerLevel / 2500f).S(5)}% at 40)\";\r\n\t\t}\r\n\t}\r\n    public class PresetData {\r\n        [JsonIgnore]\r\n        private static List<int> presetValues = new List<int> { 250, 100, 50, 25 };\r\n\r\n        [JsonIgnore]\r\n        private static List<string> presetNames = new List<string>() { \"Journey\", \"Normal\", \"Expert\", \"Master\" };\r\n\r\n        //Automatic Preset based on world difficulty\r\n        [Label(\"Automatically Match Preset to World Difficulty\")]\r\n        [DefaultValue(true)]\r\n        [ReloadRequired]\r\n        public bool AutomaticallyMatchPreseTtoWorldDifficulty {\r\n            get => _automaticallyMatchPreseTtoWorldDifficulty;\r\n            set {\r\n                _automaticallyMatchPreseTtoWorldDifficulty = value;\r\n                if (value) {\r\n                    _preset = \"Automatic\";\r\n                }\r\n                else {\r\n                    GlobalEnchantmentStrengthMultiplier = _globalEnchantmentStrengthMultiplier;\r\n                }\r\n            }\r\n        }\r\n\r\n        private bool _automaticallyMatchPreseTtoWorldDifficulty;\r\n\r\n        //Presets\r\n        [Header(\"Presets\")]\r\n        [DrawTicks]\r\n        [OptionStrings(new string[] { \"Journey\", \"Normal\", \"Expert\", \"Master\", \"Automatic\", \"Custom\" })]\r\n        [DefaultValue(\"Normal\")]\r\n        [Tooltip(\"Journey, Normal, Expert, Master, Automatic, Custom \\n(Custom can't be selected here.  It is set automatically when adjusting the Global Strength Multiplier.)\")]\r\n        [ReloadRequired]\r\n        public string Preset {\r\n            get => _automaticallyMatchPreseTtoWorldDifficulty ? \"Automatic\" : _preset;\r\n            set {\r\n                _preset = value;\r\n                if (presetNames.Contains(value))\r\n                    _globalEnchantmentStrengthMultiplier = presetValues[presetNames.IndexOf(value)];\r\n            }\r\n        }\r\n        private string _preset;\r\n\r\n        //Multipliers\r\n        [Header(\"Multipliers\")]\r\n        [Label(\"Global Enchantment Strength Multiplier (%)\")]\r\n        [Range(0, 250)]\r\n        [DefaultValue(100)]\r\n        [Tooltip(\"Adjusts all enchantment strengths based on recomended enchantment changes.\\n\" +\r\n            \"Uses the same calculations as the presets but allows you to pick a different number.\\n\" +\r\n            \"preset values are; Journey: 250, Normal: 100, Expert: 50, Master: 25 (Overides Ppreset)\")]\r\n        [ReloadRequired]\r\n        public int GlobalEnchantmentStrengthMultiplier {\r\n            get => _globalEnchantmentStrengthMultiplier;\r\n            set {\r\n                _globalEnchantmentStrengthMultiplier = value;\r\n                Preset = presetValues.Contains(_globalEnchantmentStrengthMultiplier) ? presetNames[presetValues.IndexOf(_globalEnchantmentStrengthMultiplier)] : \"Custom\";\r\n            }\r\n        }\r\n        private int _globalEnchantmentStrengthMultiplier;\r\n\r\n        [Header(\"Rarity Enchantment Strength Multipliers\")]\r\n        [Label(\"Basic\")]\r\n        [Tooltip(\"Affects the strength of all Basic Enchantments.  Overides all multipliers except individual enchantment strength multipliers.  Set to -1 for this multiplier to be ignored.\")]\r\n        [Range(-1, 10000)]\r\n        [DefaultValue(-1)]\r\n        [ReloadRequired]\r\n        public int BasicEnchantmentStrengthMultiplier { set; get; }\r\n\r\n        [Label(\"Common\")]\r\n        [Tooltip(\"Affects the strength of all Common Enchantments.  Overides all multipliers except individual enchantment strength multipliers.  Set to -1 for this multiplier to be ignored.\")]\r\n        [Range(-1, 10000)]\r\n        [DefaultValue(-1)]\r\n        [ReloadRequired]\r\n        public int CommonEnchantmentStrengthMultiplier { set; get; }\r\n\r\n        [Label(\"Rare\")]\r\n        [Tooltip(\"Affects the strength of all Rare Enchantments.  Overides all multipliers except individual enchantment strength multipliers.  Set to -1 for this multiplier to be ignored.\")]\r\n        [Range(-1, 10000)]\r\n        [DefaultValue(-1)]\r\n        [ReloadRequired]\r\n        public int RareEnchantmentStrengthMultiplier { set; get; }\r\n\r\n        [Label(\"Epic\")]\r\n        [Tooltip(\"Affects the strength of all Epic Enchantments.  Overides all multipliers except individual enchantment strength multipliers.  Set to -1 for this multiplier to be ignored.\")]\r\n        [Range(-1, 10000)]\r\n        [DefaultValue(-1)]\r\n        [ReloadRequired]\r\n        public int EpicEnchantmentStrengthMultiplier { set; get; }\r\n\r\n        [Label(\"Legendary\")]\r\n        [Tooltip(\"Affects the strength of all Legendary Enchantments.  Overides all multipliers except individual enchantment strength multipliers.  Set to -1 for this multiplier to be ignored.\")]\r\n        [Range(-1, 10000)]\r\n        [DefaultValue(-1)]\r\n        [ReloadRequired]\r\n        public int LegendaryEnchantmentStrengthMultiplier { set; get; }\r\n\r\n        public PresetData() {\r\n            AutomaticallyMatchPreseTtoWorldDifficulty = true;\r\n            Preset = \"Normal\";\r\n            BasicEnchantmentStrengthMultiplier = -1;\r\n            CommonEnchantmentStrengthMultiplier = -1;\r\n            RareEnchantmentStrengthMultiplier = -1;\r\n            EpicEnchantmentStrengthMultiplier = -1;\r\n            LegendaryEnchantmentStrengthMultiplier = -1;\r\n        }\r\n\r\n        public override bool Equals(object obj) {\r\n            if (obj is PresetData other) {\r\n                if (Preset != other.Preset)\r\n                    return false;\r\n\r\n                if (GlobalEnchantmentStrengthMultiplier != other.GlobalEnchantmentStrengthMultiplier)\r\n                    return false;\r\n\r\n                if (BasicEnchantmentStrengthMultiplier != other.BasicEnchantmentStrengthMultiplier)\r\n                    return false;\r\n\r\n                if (CommonEnchantmentStrengthMultiplier != other.CommonEnchantmentStrengthMultiplier)\r\n                    return false;\r\n\r\n                if (RareEnchantmentStrengthMultiplier != other.RareEnchantmentStrengthMultiplier)\r\n                    return false;\r\n\r\n                if (EpicEnchantmentStrengthMultiplier != other.EpicEnchantmentStrengthMultiplier)\r\n                    return false;\r\n\r\n                if (LegendaryEnchantmentStrengthMultiplier != other.LegendaryEnchantmentStrengthMultiplier)\r\n                    return false;\r\n\r\n                return true;\r\n            }\r\n            \r\n            return base.Equals(obj);\r\n        }\r\n\r\n        public override int GetHashCode() {\r\n\t\t\treturn new {\r\n\t\t\t\tPreset,\r\n\t\t\t\tGlobalEnchantmentStrengthMultiplier,\r\n\t\t\t\tBasicEnchantmentStrengthMultiplier,\r\n\t\t\t\tCommonEnchantmentStrengthMultiplier,\r\n                RareEnchantmentStrengthMultiplier,\r\n                EpicEnchantmentStrengthMultiplier,\r\n                LegendaryEnchantmentStrengthMultiplier\r\n\t\t\t}.GetHashCode();\r\n\t\t}";
			string outTxt = $"{tabs.Tabs()}{"{"} L_ID1.Config.ToString(), new(children: new() {"{"}\n";
			string newTxt = "";
			bool first = true;
			string label = "";
			string tooltip = "";
			string header = "";
			string variableName = "";
			string lastLine = "";
			int lastLineLength = 0;
			string className = "";
			string nextClassName = "";
			while (txt.Length > 0) {
				int end = txt.IndexOf("\")]");
				int nextPublic = txt.IndexOf("public");
				int nextClass = txt.IndexOf("class") + 6;
				string c1 = nextClass > 5 ? txt.Substring(nextClass) : "";
				int nextClassEnd = c1.IndexOf(" ");
				string c2 = nextClassEnd > 0 ? c1.Substring(0, nextClassEnd) : "";
				if (nextClassName != c2) {
					className = nextClassName;
					nextClassName = c2;
				}

				string s1 = txt.Substring(nextPublic);
				int i1 = s1.IndexOf(" ") + 1;
				string s2 = s1.Substring(i1);
				int i2 = s2.IndexOf(" ") + 1;
				variableName = s2.Substring(i2);
				int endOfVariableName = variableName.IndexOf(" ");
				int semi = variableName.IndexOf(";");
				if (endOfVariableName < 0 || semi > 0 && semi < endOfVariableName)
					endOfVariableName = semi;

				if (endOfVariableName > 0)
					variableName = variableName.Substring(0, endOfVariableName);

				int globalEndOfVariableName = nextPublic + i1 + i2 + endOfVariableName;
				bool variableNameIsClass = nextClass == nextPublic + 13;

				if (end < 0 || nextPublic < 0) {
					newTxt += txt;
					break;
				}

				string line = txt.Substring(0, nextPublic);
				string txtBetweenEndAndVariableNameEnd = txt.Substring(nextPublic, globalEndOfVariableName - nextPublic + 1);
				txt = txt.Substring(globalEndOfVariableName + 1);
				//if (end > endOfVariableName)
				//	continue;

				bool serverConfig = txt.IndexOf("[Label(\"ClientConfig\")]") > 0;

				string classTxt = variableNameIsClass ? nextClassName : $"{className}.{variableName}";
				List<string> terms = new List<string>() { "[Label(", "[Tooltip(", "[Header(" };
				List<(int, string)> dict = terms.Select(s => (line.IndexOf(s), s)).Where(p => p.Item1 >= 0 && p.Item1 < globalEndOfVariableName).OrderBy(p => p.Item1).ToList();
				int lastEnd = 0;
				for (int i = 0; i < dict.Count; i++) {
					(int num, string s) current = dict[i];
					string subS = line.Substring(current.num);
					int nextEnd = subS.IndexOf("\")]");
					if (nextEnd < 0)
						continue;

					int start = current.s.Length + 1;
					string s = subS.Substring(start, nextEnd - start);
					if (current.s == "[Label(") {
						label = s;
					}
					else if (current.s == "[Tooltip(") {
						tooltip = s;
						string tooltipTabe = $"\\n\" +\r\n{tabs.Tabs(4)}";
						tooltip = tooltip.Replace("\\n\" +\r\n", tooltipTabe);
					}
					else {
						header = s;
					}

					string newS = $"\"$Mods.WeaponEnchantments.Config.{(current.s == "[Header(" ? header.RemoveSpaces() : $"{variableName}.{current.s.Substring(1, current.s.Length - 2)}")}\")]";
					string newTxtLineSub = line.Substring(lastEnd, current.num - lastEnd);
					string newTxtLine = $"{newTxtLineSub}{current.s}{newS}";
					newTxt += newTxtLine;
					lastEnd = nextEnd + current.num + 3;
				}

				string lastNewLine = dict.Count > 0 ? line.Substring(lastEnd, line.Length - lastEnd) : line;
				newTxt += lastNewLine;
				newTxt += txtBetweenEndAndVariableNameEnd;

				//newTxt += variableName;
				if (header != "") {
					string headerString = ToHeaderEntry(tabs, header.RemoveSpaces(), header);
					if (!headers.Contains(headerString))
						headers.Add(headerString);
				}

				if (variableName != "" && (label != "" || tooltip != "")) {
					if (first) {
						first = false;
					}
					else {
						outTxt += ",\n";
					}

					outTxt += ToDictionaryEntry(tabs, variableName, label, tooltip, classTxt);
				}

				variableName = "";
				label = "";
				tooltip = "";
				lastLine = line;
				lastLineLength = lastLine.Length;

				/*
				string temp = outTxt;
				temp += $"\n{tabs.Tabs(1)}{"}"}, dict: new() {"{"}\n";
				temp += headers.JoinList(",\n");
				temp += $"\n{tabs.Tabs()}{"}"}) {"}"}";
				File.WriteAllText(outFilePath, temp);
				*/
			}

			//string variableName = "individualStrengthsEnabled";
			//string label = "Individual Strengths Enabled";
			//string tooltip = "Enabling this will cause the Indvidual strength values selected below to overite all other settings.";
			//outTxt += ToDictionaryEntry(tabs, variableName, label, tooltip);

			//Console.WriteLine("Hello World!");
			outTxt += $"\n{tabs.Tabs(1)}{"}"}, dict: new() {"{"}\n";
			outTxt += headers.JoinList(",\n");
			outTxt += $"\n{tabs.Tabs()}{"}"}) {"}"}";
            File.WriteAllText(localizationDataFilePath, outTxt);
			File.WriteAllText(configFilePath, newTxt);
		}
	}
    public static class UtilityFunctions {
		public static string ToDictionaryEntry(int tabs, string variableName, string label, string tooltip, string classTxt) {
			Dictionary<string, string> dict = new Dictionary<string, string>() {
				{ "L_ID3.Label.ToString()", label },
				{ "L_ID3.Tooltip.ToString()", tooltip }
			};
			List<string> lines = new List<string>() {
				$"{tabs.Tabs(1)}{"{"} nameof({classTxt}), new(dict: new() {"{"}"
			};

			if (label != "")
				lines.Add($"{tabs.Tabs(2)}{"{"} L_ID3.Label.ToString(), {label.Quotes()} {"}"}{(tooltip != "" ? "," : "")}");

			if (tooltip != "")
				lines.Add($"{tabs.Tabs(2)}{"{"} L_ID3.Tooltip.ToString(), {tooltip.Quotes()} {"}"}");

			lines.Add($"{tabs.Tabs(1)}{"}"}) {"}"}");

			string result = lines.JoinList("\n");
			//string temp = dict.Select(p => $"{p.Key}, {p.Value}".Brackets()).S(typeName, tabs, true, true).Brackets(false);
			//string result = $" nameof(WEMod.serverConfig.{variableName}), new(dict: new() {$"\n{tabs.Tabs(1)}{$" L_ID3.Label.ToString(), {label.Quotes()} ".Brackets()}".Brackets()}\n\t\t\t\t\t\t\t{"}"}) ".Brackets();

			return result;
		}
		public static string ToHeaderEntry(int tabs, string headerID, string headerText) => $"{tabs.Tabs(2)}{$"{headerID.Quotes()}, {headerText.Quotes()}".Brackets()}";
		public delegate string ToStringDelegate<T>(T t);
		public static string StringList<T>(this IEnumerable<T> enumerable, ToStringDelegate<T> toString, string name = null) => $"{(name != null ? $"{name} " : "")}{enumerable.Select(v => toString(v)).JoinList(", ").Brackets()}";
		public static string S(this IEnumerable<string> strings, string nameAndType = "", int tabs = 2, bool isArgument = false, bool brackets = false) => $"\n{tabs.Tabs()}{(brackets ? "{ " : "")}{nameAndType} {"{"}\n{tabs.Tabs(1)}{strings.JoinList($",\n{tabs.Tabs(1)}")}\n{tabs.Tabs()}{"}"}{(brackets ? " }" : "")}{(isArgument ? "" : ";")}";
		public static string JoinList(this IEnumerable<string> list, string joinString = "<br/>", string last = null) {
			string text = "";
			bool firstString = true;
			int count = list.Count();
			int i = 0;
			foreach (string s in list) {
				if (firstString) {
					firstString = false;
				}
				else {
					text += i == count - 1 ? last ?? joinString : joinString;
				}

				text += s;
				i++;
			}

			return text;
		}
		public static string Brackets(this string s, bool spaces = true) => "{" + (spaces ? " " : "") + s + (spaces ? " " : "") + "}";
		public static string Tabs(this int num) => num > 0 ? new string('\t', num) : "";
		public static string Tabs(this int num, int numAdd) => num + numAdd > 0 ? new string('\t', num + numAdd) : "";
		public static string Quotes(this string s) => "\"" + s + "\"";
		public static string RemoveSpaces(this string s) {
			bool started = false;
			int start = 0;
			int end;
			string finalString = "";
			for (int i = 0; i < s.Length; i++) {
				char c = s[i];
				if (started) {
					if (c == ' ') {
						started = false;
						end = i;
						finalString += s.Substring(start, end - start);
					}
				}
				else {
					if (c != ' ') {
						started = true;
						start = i;
					}
				}
			}
			if (started)
				finalString += s.Substring(start, s.Length - start);

			return finalString;
		}
	}
}