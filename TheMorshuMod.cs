using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TheMorshuMod.NPCs.Morshu
{
    [AutoloadHead]

    public class TheMorshuMod : ModNPC
    {
        public override string Texture
        {
            get { return "TheMorshuMod/NPCs/Morshu"; }
        }

        public override string[] AltTextures
        {
            get { return new[] { "TheMorshuMod/NPCs/Morshu_Alt_1" }; }
        }

        public override bool Autoload(ref string name)
        {
            name = "Rupee Merchant";
            return mod.Properties.Autoload;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 26;
            NPCID.Sets.ExtraFramesCount[npc.type] = 9;
            NPCID.Sets.AttackFrameCount[npc.type] = 5;
            NPCID.Sets.DangerDetectRange[npc.type] = 700;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 90;
            NPCID.Sets.AttackAverageChance[npc.type] = 30;
            NPCID.Sets.HatOffsetY[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 18;
            npc.height = 40;
            npc.aiStyle = 7;
            npc.damage = 100;
            npc.defense = 20;
            npc.lifeMax = 400;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0;
            animationType = NPCID.Guide;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (!player.active)
                {
                    continue;
                }

                foreach (Item item in player.inventory)
                {
                    if (item.type == ItemID.LargeRuby)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override string TownNPCName()
        {
            return "Morshu";
        }

        public override string GetChat()
        {
            switch (Main.rand.Next(4))
            {
                case 0:
                    return "Lamp Oil? Rope? Bombs? You want it? It's yours, my friend.";
                case 1:
                    return "Sorry, I can't give credit.";
                case 2:
                    return "This will surely make me mmmmmm richer.";
                default:
                    return "Go ahead and buy it, if you're able.";
            }
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
            //button2 = "Haggle";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                shop = true;
            }
            /*else
            {
                Main.npcChatText = "I'm sorry! I can't give credit. Come back when you're a little mmmmm richer.";
            }*/
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            shop.item[nextSlot].SetDefaults(ItemID.Gel);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.RopeCoil);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.Bomb);
            nextSlot++;
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (!player.active)
                {
                    continue;
                }

                foreach (Item item in player.inventory)
                {
                    if (item.type == ItemID.LargeRuby)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.LifeCrystal);
                        nextSlot++;
                    }
                }
            }
            if (Main.hardMode)
            {
                shop.item[nextSlot].SetDefaults(ItemID.SuperHealingPotion);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.SuperManaPotion);
                nextSlot++;
            }
        }

        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), ItemID.Ruby);
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 99;
            knockback = 5f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 80;
            randExtraCooldown = 10;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<Projectiles.MorshuProjectile>();
            attackDelay = 3;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 5f;
            randomOffset = 2f;
        }
    }

    namespace Autoloader
    {
        class Autoloader : Mod
        {
            public Autoloader()
            {
                Properties = new ModProperties()
                {
                    Autoload = true,
                    AutoloadGores = true,
                    AutoloadSounds = true
                };
            }
        }
    }
}