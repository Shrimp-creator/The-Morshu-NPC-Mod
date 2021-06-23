using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TheMorshuMod.NPCs.RupeeMerchant
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
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (!player.active)
                {
                    continue;
                }

                foreach (Item item in player.inventory)
                {
                    if (item.type == ItemID.Ruby)
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
            switch (Main.rand.Next(3))
            {
                case 0:
                    return "Lamp Oil? Rope? Bombs? You want it? It's yours, my friend.";
                case 1:
                    return "Sorry, I can't give credit.";
                default:
                    return "This will surely make me mmmmmm richer.";
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
            if (Main.hardMode)
            {
                shop.item[nextSlot].SetDefaults(ItemID.SuperHealingPotion);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.SuperManaPotion);
                nextSlot++;
            }
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
                        //shop.item[nextSlot].shopCustomPrice = 1;
                        //shop.item[nextSlot].shopSpecialCurrency = TheMorshuMod.RubyCustomCurrencyId;
                        nextSlot++;
                    }
                }
            }
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem(npc.getRect(), ItemID.Gel);
                Item.NewItem(npc.getRect(), ItemID.Rope);
                Item.NewItem(npc.getRect(), ItemID.Bomb);
            }
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
            projType = ModContent.ProjectileType<Projectiles.MorshuProjectile>();
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 5f;
            randomOffset = 2f;
        }
    }
}