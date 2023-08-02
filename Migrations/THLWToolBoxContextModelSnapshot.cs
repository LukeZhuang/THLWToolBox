﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using THLWToolBox.Data;

#nullable disable

namespace THLWToolBox.Migrations
{
    [DbContext(typeof(THLWToolBoxContext))]
    partial class THLWToolBoxContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("THLWToolBox.Models.PersonRelationData", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("person_id")
                        .HasColumnType("int");

                    b.Property<int>("target_person_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("PersonRelationData");
                });

            modelBuilder.Entity("THLWToolBox.Models.PictureData", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("album_id")
                        .HasColumnType("int");

                    b.Property<string>("circle_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("correction1_diff")
                        .HasColumnType("int");

                    b.Property<int>("correction1_type")
                        .HasColumnType("int");

                    b.Property<int>("correction1_value")
                        .HasColumnType("int");

                    b.Property<int>("correction2_diff")
                        .HasColumnType("int");

                    b.Property<int>("correction2_type")
                        .HasColumnType("int");

                    b.Property<int>("correction2_value")
                        .HasColumnType("int");

                    b.Property<string>("flavor_text1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("flavor_text2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("flavor_text3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("flavor_text4")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("flavor_text5")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("illustrator_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("is_show")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("picture_characteristic1_effect_range")
                        .HasColumnType("int");

                    b.Property<int>("picture_characteristic1_effect_subtype")
                        .HasColumnType("int");

                    b.Property<int>("picture_characteristic1_effect_turn")
                        .HasColumnType("int");

                    b.Property<int>("picture_characteristic1_effect_type")
                        .HasColumnType("int");

                    b.Property<int>("picture_characteristic1_effect_value")
                        .HasColumnType("int");

                    b.Property<int>("picture_characteristic1_effect_value_max")
                        .HasColumnType("int");

                    b.Property<int>("picture_characteristic2_effect_range")
                        .HasColumnType("int");

                    b.Property<int>("picture_characteristic2_effect_subtype")
                        .HasColumnType("int");

                    b.Property<int>("picture_characteristic2_effect_turn")
                        .HasColumnType("int");

                    b.Property<int>("picture_characteristic2_effect_type")
                        .HasColumnType("int");

                    b.Property<int>("picture_characteristic2_effect_value")
                        .HasColumnType("int");

                    b.Property<int>("picture_characteristic2_effect_value_max")
                        .HasColumnType("int");

                    b.Property<int>("picture_characteristic3_effect_range")
                        .HasColumnType("int");

                    b.Property<int>("picture_characteristic3_effect_subtype")
                        .HasColumnType("int");

                    b.Property<int>("picture_characteristic3_effect_turn")
                        .HasColumnType("int");

                    b.Property<int>("picture_characteristic3_effect_type")
                        .HasColumnType("int");

                    b.Property<int>("picture_characteristic3_effect_value")
                        .HasColumnType("int");

                    b.Property<int>("picture_characteristic3_effect_value_max")
                        .HasColumnType("int");

                    b.Property<string>("picture_characteristic_text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("picture_characteristic_text_max")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("rare")
                        .HasColumnType("int");

                    b.Property<int>("recycle_id")
                        .HasColumnType("int");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("PictureData");
                });

            modelBuilder.Entity("THLWToolBox.Models.PlayerUnitBulletCriticalRaceData", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("bullet_id")
                        .HasColumnType("int");

                    b.Property<int>("race_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("PlayerUnitBulletCriticalRaceData");
                });

            modelBuilder.Entity("THLWToolBox.Models.PlayerUnitBulletData", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("bullet1_addon_id")
                        .HasColumnType("int");

                    b.Property<int>("bullet1_addon_value")
                        .HasColumnType("int");

                    b.Property<int>("bullet1_extraeffect_id")
                        .HasColumnType("int");

                    b.Property<int>("bullet1_extraeffect_success_rate")
                        .HasColumnType("int");

                    b.Property<int>("bullet2_addon_id")
                        .HasColumnType("int");

                    b.Property<int>("bullet2_addon_value")
                        .HasColumnType("int");

                    b.Property<int>("bullet2_extraeffect_id")
                        .HasColumnType("int");

                    b.Property<int>("bullet2_extraeffect_success_rate")
                        .HasColumnType("int");

                    b.Property<int>("bullet3_addon_id")
                        .HasColumnType("int");

                    b.Property<int>("bullet3_addon_value")
                        .HasColumnType("int");

                    b.Property<int>("bullet3_extraeffect_id")
                        .HasColumnType("int");

                    b.Property<int>("bullet3_extraeffect_success_rate")
                        .HasColumnType("int");

                    b.Property<int>("category")
                        .HasColumnType("int");

                    b.Property<int>("critical")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("element")
                        .HasColumnType("int");

                    b.Property<int>("hit")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("power")
                        .HasColumnType("real");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("PlayerUnitBulletData");
                });

            modelBuilder.Entity("THLWToolBox.Models.PlayerUnitCharacteristicData", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("characteristic1_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("characteristic1_effect_subtype")
                        .HasColumnType("int");

                    b.Property<int>("characteristic1_effect_type")
                        .HasColumnType("int");

                    b.Property<int>("characteristic1_effect_value")
                        .HasColumnType("int");

                    b.Property<string>("characteristic1_icon_filename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("characteristic1_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("characteristic1_rate")
                        .HasColumnType("int");

                    b.Property<int>("characteristic1_type")
                        .HasColumnType("int");

                    b.Property<string>("characteristic2_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("characteristic2_effect_subtype")
                        .HasColumnType("int");

                    b.Property<int>("characteristic2_effect_type")
                        .HasColumnType("int");

                    b.Property<int>("characteristic2_effect_value")
                        .HasColumnType("int");

                    b.Property<string>("characteristic2_icon_filename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("characteristic2_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("characteristic2_rate")
                        .HasColumnType("int");

                    b.Property<int>("characteristic2_type")
                        .HasColumnType("int");

                    b.Property<string>("characteristic3_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("characteristic3_effect_subtype")
                        .HasColumnType("int");

                    b.Property<int>("characteristic3_effect_type")
                        .HasColumnType("int");

                    b.Property<int>("characteristic3_effect_value")
                        .HasColumnType("int");

                    b.Property<string>("characteristic3_icon_filename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("characteristic3_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("characteristic3_rate")
                        .HasColumnType("int");

                    b.Property<int>("characteristic3_type")
                        .HasColumnType("int");

                    b.Property<int>("trust_characteristic_avent_effect_subtype")
                        .HasColumnType("int");

                    b.Property<int>("trust_characteristic_avent_effect_type")
                        .HasColumnType("int");

                    b.Property<string>("trust_characteristic_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("trust_characteristic_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("trust_characteristic_rear_effect_subtype")
                        .HasColumnType("int");

                    b.Property<int>("trust_characteristic_rear_effect_type")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("PlayerUnitCharacteristicData");
                });

            modelBuilder.Entity("THLWToolBox.Models.PlayerUnitData", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("ability_id")
                        .HasColumnType("int");

                    b.Property<int>("album_id")
                        .HasColumnType("int");

                    b.Property<string>("alias_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("characteristic_id")
                        .HasColumnType("int");

                    b.Property<int>("default_costume_id")
                        .HasColumnType("int");

                    b.Property<string>("drop_text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("exp_id")
                        .HasColumnType("int");

                    b.Property<int>("is_show")
                        .HasColumnType("int");

                    b.Property<int>("life_point")
                        .HasColumnType("int");

                    b.Property<int>("limitbreak_item_id")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name_kana")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name_kana_sub")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name_sub")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("person_id")
                        .HasColumnType("int");

                    b.Property<int>("recycle_id")
                        .HasColumnType("int");

                    b.Property<int>("resist_id")
                        .HasColumnType("int");

                    b.Property<int>("role")
                        .HasColumnType("int");

                    b.Property<string>("short_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("short_name_sub")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("shot1_id")
                        .HasColumnType("int");

                    b.Property<int>("shot2_id")
                        .HasColumnType("int");

                    b.Property<int>("skill1_id")
                        .HasColumnType("int");

                    b.Property<int>("skill2_id")
                        .HasColumnType("int");

                    b.Property<int>("skill3_id")
                        .HasColumnType("int");

                    b.Property<int>("speed")
                        .HasColumnType("int");

                    b.Property<int>("spellcard1_id")
                        .HasColumnType("int");

                    b.Property<int>("spellcard2_id")
                        .HasColumnType("int");

                    b.Property<int>("spellcard3_id")
                        .HasColumnType("int");

                    b.Property<int>("spellcard4_id")
                        .HasColumnType("int");

                    b.Property<int>("spellcard5_id")
                        .HasColumnType("int");

                    b.Property<int>("spellcard_bgm_id")
                        .HasColumnType("int");

                    b.Property<string>("symbol_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("symbol_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("symbol_title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("yang_attack")
                        .HasColumnType("int");

                    b.Property<int>("yang_defense")
                        .HasColumnType("int");

                    b.Property<int>("yin_attack")
                        .HasColumnType("int");

                    b.Property<int>("yin_defense")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("PlayerUnitData");
                });

            modelBuilder.Entity("THLWToolBox.Models.PlayerUnitHitCheckOrderData", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("barrage_id")
                        .HasColumnType("int");

                    b.Property<int>("boost_id")
                        .HasColumnType("int");

                    b.Property<string>("hit_check_order")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("unit_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("PlayerUnitHitCheckOrderData");
                });

            modelBuilder.Entity("THLWToolBox.Models.PlayerUnitRaceData", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("race_id")
                        .HasColumnType("int");

                    b.Property<int>("unit_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("PlayerUnitRaceData");
                });

            modelBuilder.Entity("THLWToolBox.Models.PlayerUnitShotData", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("magazine0_bullet_id")
                        .HasColumnType("int");

                    b.Property<int>("magazine0_bullet_power_rate")
                        .HasColumnType("int");

                    b.Property<int>("magazine0_bullet_range")
                        .HasColumnType("int");

                    b.Property<int>("magazine0_bullet_value")
                        .HasColumnType("int");

                    b.Property<int>("magazine1_boost_count")
                        .HasColumnType("int");

                    b.Property<int>("magazine1_bullet_id")
                        .HasColumnType("int");

                    b.Property<int>("magazine1_bullet_power_rate")
                        .HasColumnType("int");

                    b.Property<int>("magazine1_bullet_range")
                        .HasColumnType("int");

                    b.Property<int>("magazine1_bullet_value")
                        .HasColumnType("int");

                    b.Property<int>("magazine2_boost_count")
                        .HasColumnType("int");

                    b.Property<int>("magazine2_bullet_id")
                        .HasColumnType("int");

                    b.Property<int>("magazine2_bullet_power_rate")
                        .HasColumnType("int");

                    b.Property<int>("magazine2_bullet_range")
                        .HasColumnType("int");

                    b.Property<int>("magazine2_bullet_value")
                        .HasColumnType("int");

                    b.Property<int>("magazine3_boost_count")
                        .HasColumnType("int");

                    b.Property<int>("magazine3_bullet_id")
                        .HasColumnType("int");

                    b.Property<int>("magazine3_bullet_power_rate")
                        .HasColumnType("int");

                    b.Property<int>("magazine3_bullet_range")
                        .HasColumnType("int");

                    b.Property<int>("magazine3_bullet_value")
                        .HasColumnType("int");

                    b.Property<int>("magazine4_boost_count")
                        .HasColumnType("int");

                    b.Property<int>("magazine4_bullet_id")
                        .HasColumnType("int");

                    b.Property<int>("magazine4_bullet_power_rate")
                        .HasColumnType("int");

                    b.Property<int>("magazine4_bullet_range")
                        .HasColumnType("int");

                    b.Property<int>("magazine4_bullet_value")
                        .HasColumnType("int");

                    b.Property<int>("magazine5_boost_count")
                        .HasColumnType("int");

                    b.Property<int>("magazine5_bullet_id")
                        .HasColumnType("int");

                    b.Property<int>("magazine5_bullet_power_rate")
                        .HasColumnType("int");

                    b.Property<int>("magazine5_bullet_range")
                        .HasColumnType("int");

                    b.Property<int>("magazine5_bullet_value")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("phantasm_power_up_rate")
                        .HasColumnType("int");

                    b.Property<int>("shot_level0_power_rate")
                        .HasColumnType("int");

                    b.Property<int>("shot_level1_power_rate")
                        .HasColumnType("int");

                    b.Property<int>("shot_level2_power_rate")
                        .HasColumnType("int");

                    b.Property<int>("shot_level3_power_rate")
                        .HasColumnType("int");

                    b.Property<int>("shot_level4_power_rate")
                        .HasColumnType("int");

                    b.Property<int>("shot_level5_power_rate")
                        .HasColumnType("int");

                    b.Property<string>("specification")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("PlayerUnitShotData");
                });

            modelBuilder.Entity("THLWToolBox.Models.PlayerUnitSpellcardData", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("magazine0_bullet_id")
                        .HasColumnType("int");

                    b.Property<int>("magazine0_bullet_power_rate")
                        .HasColumnType("int");

                    b.Property<int>("magazine0_bullet_range")
                        .HasColumnType("int");

                    b.Property<int>("magazine0_bullet_value")
                        .HasColumnType("int");

                    b.Property<int>("magazine1_boost_count")
                        .HasColumnType("int");

                    b.Property<int>("magazine1_bullet_id")
                        .HasColumnType("int");

                    b.Property<int>("magazine1_bullet_power_rate")
                        .HasColumnType("int");

                    b.Property<int>("magazine1_bullet_range")
                        .HasColumnType("int");

                    b.Property<int>("magazine1_bullet_value")
                        .HasColumnType("int");

                    b.Property<int>("magazine2_boost_count")
                        .HasColumnType("int");

                    b.Property<int>("magazine2_bullet_id")
                        .HasColumnType("int");

                    b.Property<int>("magazine2_bullet_power_rate")
                        .HasColumnType("int");

                    b.Property<int>("magazine2_bullet_range")
                        .HasColumnType("int");

                    b.Property<int>("magazine2_bullet_value")
                        .HasColumnType("int");

                    b.Property<int>("magazine3_boost_count")
                        .HasColumnType("int");

                    b.Property<int>("magazine3_bullet_id")
                        .HasColumnType("int");

                    b.Property<int>("magazine3_bullet_power_rate")
                        .HasColumnType("int");

                    b.Property<int>("magazine3_bullet_range")
                        .HasColumnType("int");

                    b.Property<int>("magazine3_bullet_value")
                        .HasColumnType("int");

                    b.Property<int>("magazine4_boost_count")
                        .HasColumnType("int");

                    b.Property<int>("magazine4_bullet_id")
                        .HasColumnType("int");

                    b.Property<int>("magazine4_bullet_power_rate")
                        .HasColumnType("int");

                    b.Property<int>("magazine4_bullet_range")
                        .HasColumnType("int");

                    b.Property<int>("magazine4_bullet_value")
                        .HasColumnType("int");

                    b.Property<int>("magazine5_boost_count")
                        .HasColumnType("int");

                    b.Property<int>("magazine5_bullet_id")
                        .HasColumnType("int");

                    b.Property<int>("magazine5_bullet_power_rate")
                        .HasColumnType("int");

                    b.Property<int>("magazine5_bullet_range")
                        .HasColumnType("int");

                    b.Property<int>("magazine5_bullet_value")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("phantasm_power_up_rate")
                        .HasColumnType("int");

                    b.Property<int>("shot_level0_power_rate")
                        .HasColumnType("int");

                    b.Property<int>("shot_level1_power_rate")
                        .HasColumnType("int");

                    b.Property<int>("shot_level2_power_rate")
                        .HasColumnType("int");

                    b.Property<int>("shot_level3_power_rate")
                        .HasColumnType("int");

                    b.Property<int>("shot_level4_power_rate")
                        .HasColumnType("int");

                    b.Property<int>("shot_level5_power_rate")
                        .HasColumnType("int");

                    b.Property<string>("specification")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("spellcard_skill1_effect_id")
                        .HasColumnType("int");

                    b.Property<int>("spellcard_skill1_level_type")
                        .HasColumnType("int");

                    b.Property<int>("spellcard_skill1_level_value")
                        .HasColumnType("int");

                    b.Property<int>("spellcard_skill1_timing")
                        .HasColumnType("int");

                    b.Property<int>("spellcard_skill2_effect_id")
                        .HasColumnType("int");

                    b.Property<int>("spellcard_skill2_level_type")
                        .HasColumnType("int");

                    b.Property<int>("spellcard_skill2_level_value")
                        .HasColumnType("int");

                    b.Property<int>("spellcard_skill2_timing")
                        .HasColumnType("int");

                    b.Property<int>("spellcard_skill3_effect_id")
                        .HasColumnType("int");

                    b.Property<int>("spellcard_skill3_level_type")
                        .HasColumnType("int");

                    b.Property<int>("spellcard_skill3_level_value")
                        .HasColumnType("int");

                    b.Property<int>("spellcard_skill3_timing")
                        .HasColumnType("int");

                    b.Property<int>("spellcard_skill4_effect_id")
                        .HasColumnType("int");

                    b.Property<int>("spellcard_skill4_level_type")
                        .HasColumnType("int");

                    b.Property<int>("spellcard_skill4_level_value")
                        .HasColumnType("int");

                    b.Property<int>("spellcard_skill4_timing")
                        .HasColumnType("int");

                    b.Property<int>("spellcard_skill5_effect_id")
                        .HasColumnType("int");

                    b.Property<int>("spellcard_skill5_level_type")
                        .HasColumnType("int");

                    b.Property<int>("spellcard_skill5_level_value")
                        .HasColumnType("int");

                    b.Property<int>("spellcard_skill5_timing")
                        .HasColumnType("int");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("PlayerUnitSpellcardData");
                });

            modelBuilder.Entity("THLWToolBox.Models.RaceData", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("alias_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("RaceData");
                });

            modelBuilder.Entity("THLWToolBox.Models.VersionHistoryData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("VersionHistoryData");
                });
#pragma warning restore 612, 618
        }
    }
}
