using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Dyes;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace TheMorshuMod
{
    public class TheMorshuMod : Mod
    {
        public static int RubyCustomCurrencyId;

        public override void Load()
        {
            //FaceCustomCurrencyId = CustomCurrencyManager.RegisterCurrency(new ExampleCustomCurrency(ItemType<Items.Face>(), 999L));
        }
    }
}