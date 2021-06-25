using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TheMorshuMod.NPCs.King
{
    [AutoloadHead]

    public class TheMorshuMod : ModNPC
    {
        public override string Texture
        {
            get { return "TheMorshuMod/NPCs/King"; }
        }

        public override string[] AltTextures
        {
            get { return new[] { "TheMorshuMod/NPCs/King_Party" }; }
        }

        public override bool Autoload(ref string name)
        {
            name = "King";
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
            NPCID.Sets.HatOffsetY[npc.type] = -6;
        }

        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 18;
            npc.height = 40;
            npc.aiStyle = 7;
            npc.damage = 15;
            npc.defense = 20;
            npc.lifeMax = 250;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            animationType = NPCID.Guide;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            if (numTownNPCs >= 12)
            {
                return true;
            }

            return false;
        }

        public override string TownNPCName()
        {
            return "Harkinian";
        }

        public override string GetChat()
        {
            if (Main.moonPhase == 0)
            {
                return "Why don't you rid me of this old sword?";
            }
            else
            {
                switch (Main.rand.Next(3))
                {
                    case 0:
                        return "I wonder what's for dinner.";
                    case 1:
                        return "OAH HA HA HA , enough!";
                    default:
                        return "This peace is what all true warriors strive for.";
                }
            }
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
            //button2 = "Cloak";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                shop = true;
            }
            else
            {
                /*Main.npcChatText = "The Magic Cape! Don't cause too much trouble now.";

                for (int k = 0; k < 255; k++)
                {
                    Player player = Main.player[k];
                    if (!player.active)
                    {
                        continue;
                    }

                    Main.player[k].AddBuff(BuffID.Invisibility, 36000);
                }*/
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            if (Main.hardMode)
            {
                shop.item[nextSlot].SetDefaults(ItemID.EnchantedBoomerang);
                shop.item[nextSlot].shopCustomPrice = 100000;
                nextSlot++;
            }
            else
            {
                shop.item[nextSlot].SetDefaults(ItemID.WoodenBoomerang);
                shop.item[nextSlot].shopCustomPrice = 1000;
                nextSlot++;
            }

            if (NPC.downedBoss3)
            {
                shop.item[nextSlot].SetDefaults(ItemID.CobaltShield);
                shop.item[nextSlot].shopCustomPrice = 50000;
                nextSlot++;
            }

            if (Main.hardMode)
            {
                shop.item[nextSlot].SetDefaults(ItemID.HerosHat);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.HerosShirt);
                shop.item[nextSlot].shopCustomPrice = 10000;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.HerosPants);
                shop.item[nextSlot].shopCustomPrice = 10000;
                nextSlot++;
            }

            if (NPC.downedBoss2)
            {
                shop.item[nextSlot].SetDefaults(ItemID.Bottle);
                shop.item[nextSlot].shopCustomPrice = 1500;
                nextSlot++;
            }

            shop.item[nextSlot].SetDefaults(ItemID.PixieDust);
            shop.item[nextSlot].shopCustomPrice = 250;
            nextSlot++;

            if (NPC.downedMechBossAny)
            {
                if (Main.moonPhase == 0)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.BrokenHeroSword);
                    shop.item[nextSlot].shopCustomPrice = 250000;
                    nextSlot++;
                }
            }
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(5) == 0)
            {
                Item.NewItem(npc.getRect(), ItemID.GoldCrown);
            }
            else if (Main.rand.Next(5) < 3)
            {
                Item.NewItem(npc.getRect(), ItemID.PixieDust, Main.rand.Next(4));
            }
        }

        public override bool CanGoToStatue(bool toKingStatue)
        {
            return true;
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 20;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 30;
            randExtraCooldown = 30;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ProjectileID.TopazBolt;
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 5f;
            randomOffset = 0f;
        }
    }
}