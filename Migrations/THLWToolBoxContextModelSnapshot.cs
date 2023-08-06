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

            modelBuilder.Entity("THLWToolBox.Models.DataTypes.PersonRelationData", b =>
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

            modelBuilder.Entity("THLWToolBox.Models.DataTypes.PictureData", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

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

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("PictureData");
                });

            modelBuilder.Entity("THLWToolBox.Models.DataTypes.PlayerUnitAbilityData", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("barrier_ability_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("blackout_barrier_type")
                        .HasColumnType("int");

                    b.Property<string>("boost_ability_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("boost_power_divergence_range")
                        .HasColumnType("int");

                    b.Property<int>("boost_power_divergence_type")
                        .HasColumnType("int");

                    b.Property<int>("burning_barrier_type")
                        .HasColumnType("int");

                    b.Property<int>("electrified_barrier_type")
                        .HasColumnType("int");

                    b.Property<string>("element_ability_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("frozen_barrier_type")
                        .HasColumnType("int");

                    b.Property<int>("good_element_give_damage_rate")
                        .HasColumnType("int");

                    b.Property<int>("good_element_take_damage_rate")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("poisoning_barrier_type")
                        .HasColumnType("int");

                    b.Property<string>("purge_ability_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("purge_barrier_diffusion_range")
                        .HasColumnType("int");

                    b.Property<int>("purge_barrier_diffusion_type")
                        .HasColumnType("int");

                    b.Property<string>("resist_ability_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("weak_element_give_damage_rate")
                        .HasColumnType("int");

                    b.Property<int>("weak_element_take_damage_rate")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("PlayerUnitAbilityData");
                });

            modelBuilder.Entity("THLWToolBox.Models.DataTypes.PlayerUnitBulletData", b =>
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

            modelBuilder.Entity("THLWToolBox.Models.DataTypes.PlayerUnitCharacteristicData", b =>
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

            modelBuilder.Entity("THLWToolBox.Models.DataTypes.PlayerUnitData", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("ability_id")
                        .HasColumnType("int");

                    b.Property<int>("characteristic_id")
                        .HasColumnType("int");

                    b.Property<int>("life_point")
                        .HasColumnType("int");

                    b.Property<int>("limitbreak_item_id")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("person_id")
                        .HasColumnType("int");

                    b.Property<int>("resist_id")
                        .HasColumnType("int");

                    b.Property<int>("role")
                        .HasColumnType("int");

                    b.Property<string>("short_name")
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

                    b.Property<string>("symbol_name")
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

            modelBuilder.Entity("THLWToolBox.Models.DataTypes.PlayerUnitHitCheckOrderData", b =>
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

            modelBuilder.Entity("THLWToolBox.Models.DataTypes.PlayerUnitRaceData", b =>
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

            modelBuilder.Entity("THLWToolBox.Models.DataTypes.PlayerUnitShotData", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

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

                    b.HasKey("id");

                    b.ToTable("PlayerUnitShotData");
                });

            modelBuilder.Entity("THLWToolBox.Models.DataTypes.PlayerUnitSkillData", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("effect1_id")
                        .HasColumnType("int");

                    b.Property<int>("effect1_level_type")
                        .HasColumnType("int");

                    b.Property<int>("effect1_level_value")
                        .HasColumnType("int");

                    b.Property<int>("effect2_id")
                        .HasColumnType("int");

                    b.Property<int>("effect2_level_type")
                        .HasColumnType("int");

                    b.Property<int>("effect2_level_value")
                        .HasColumnType("int");

                    b.Property<int>("effect3_id")
                        .HasColumnType("int");

                    b.Property<int>("effect3_level_type")
                        .HasColumnType("int");

                    b.Property<int>("effect3_level_value")
                        .HasColumnType("int");

                    b.Property<int>("exp_id")
                        .HasColumnType("int");

                    b.Property<int>("level10_turn")
                        .HasColumnType("int");

                    b.Property<int>("level1_turn")
                        .HasColumnType("int");

                    b.Property<int>("level2_turn")
                        .HasColumnType("int");

                    b.Property<int>("level3_turn")
                        .HasColumnType("int");

                    b.Property<int>("level4_turn")
                        .HasColumnType("int");

                    b.Property<int>("level5_turn")
                        .HasColumnType("int");

                    b.Property<int>("level6_turn")
                        .HasColumnType("int");

                    b.Property<int>("level7_turn")
                        .HasColumnType("int");

                    b.Property<int>("level8_turn")
                        .HasColumnType("int");

                    b.Property<int>("level9_turn")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("PlayerUnitSkillData");
                });

            modelBuilder.Entity("THLWToolBox.Models.DataTypes.PlayerUnitSkillEffectData", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("level10_add_value")
                        .HasColumnType("int");

                    b.Property<int>("level10_success_rate")
                        .HasColumnType("int");

                    b.Property<int>("level10_value")
                        .HasColumnType("int");

                    b.Property<int>("level1_add_value")
                        .HasColumnType("int");

                    b.Property<int>("level1_success_rate")
                        .HasColumnType("int");

                    b.Property<int>("level1_value")
                        .HasColumnType("int");

                    b.Property<int>("level2_add_value")
                        .HasColumnType("int");

                    b.Property<int>("level2_success_rate")
                        .HasColumnType("int");

                    b.Property<int>("level2_value")
                        .HasColumnType("int");

                    b.Property<int>("level3_add_value")
                        .HasColumnType("int");

                    b.Property<int>("level3_success_rate")
                        .HasColumnType("int");

                    b.Property<int>("level3_value")
                        .HasColumnType("int");

                    b.Property<int>("level4_add_value")
                        .HasColumnType("int");

                    b.Property<int>("level4_success_rate")
                        .HasColumnType("int");

                    b.Property<int>("level4_value")
                        .HasColumnType("int");

                    b.Property<int>("level5_add_value")
                        .HasColumnType("int");

                    b.Property<int>("level5_success_rate")
                        .HasColumnType("int");

                    b.Property<int>("level5_value")
                        .HasColumnType("int");

                    b.Property<int>("level6_add_value")
                        .HasColumnType("int");

                    b.Property<int>("level6_success_rate")
                        .HasColumnType("int");

                    b.Property<int>("level6_value")
                        .HasColumnType("int");

                    b.Property<int>("level7_add_value")
                        .HasColumnType("int");

                    b.Property<int>("level7_success_rate")
                        .HasColumnType("int");

                    b.Property<int>("level7_value")
                        .HasColumnType("int");

                    b.Property<int>("level8_add_value")
                        .HasColumnType("int");

                    b.Property<int>("level8_success_rate")
                        .HasColumnType("int");

                    b.Property<int>("level8_value")
                        .HasColumnType("int");

                    b.Property<int>("level9_add_value")
                        .HasColumnType("int");

                    b.Property<int>("level9_success_rate")
                        .HasColumnType("int");

                    b.Property<int>("level9_value")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("range")
                        .HasColumnType("int");

                    b.Property<int>("subtype")
                        .HasColumnType("int");

                    b.Property<int>("turn")
                        .HasColumnType("int");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("PlayerUnitSkillEffectData");
                });

            modelBuilder.Entity("THLWToolBox.Models.DataTypes.PlayerUnitSpellcardData", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

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

            modelBuilder.Entity("THLWToolBox.Models.DataTypes.RaceData", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("RaceData");
                });

            modelBuilder.Entity("THLWToolBox.Models.DataTypes.VersionHistoryData", b =>
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
#pragma warning restore 612, 618
        }
    }
}
