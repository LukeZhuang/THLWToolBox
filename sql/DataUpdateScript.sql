/****** Script for SelectTopNRows command from SSMS  ******/

/* update version info */
USE [thlwtoolbox-database]
GO

INSERT INTO [dbo].[VersionHistoryData]
           ([ReleaseDate]
           ,[description])
     VALUES
           ('2023-06-24', N'v1.1更新，界面大优化，绘卷筛选器优化。发布公告：https://www.bilibili.com/read/cv24562484')
GO


/* step 0: import data manually */
/********************************/
/********************************/
/********************************/


/* step 1: update date */
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'UPDATEDATE')
BEGIN
    CREATE TABLE [dbo].[UPDATEDATE] (
	  date nvarchar(255)
	)
END
INSERT INTO [dbo].[UPDATEDATE] (date) VALUES ('20230417')

/* step 2: drop old tables */
DROP TABLE [dbo].[RAW_AbilityTable];
DROP TABLE [dbo].[RAW_BulletCriticalRaceTable];
DROP TABLE [dbo].[RAW_BulletExtraEffectTable];
DROP TABLE [dbo].[RAW_BulletTable];
DROP TABLE [dbo].[RAW_CharacteristicTable];
DROP TABLE [dbo].[RAW_HitCheckOrderTable];
DROP TABLE [dbo].[RAW_PersonRelationTable];
DROP TABLE [dbo].[RAW_PictureTable];
DROP TABLE [dbo].[RAW_RaceTable];
DROP TABLE [dbo].[RAW_ShotTable];
DROP TABLE [dbo].[RAW_SkillEffectTable];
DROP TABLE [dbo].[RAW_SkillTable];
DROP TABLE [dbo].[RAW_SpellcardTable];
DROP TABLE [dbo].[RAW_UnitRaceTable];
DROP TABLE [dbo].[RAW_UnitTable];


/* step 3: create new raw table (note that this needs to be updated if table structure is changed) */
CREATE TABLE [dbo].[RAW_AbilityTable](
	[id] [int] NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[resist_ability_description] [nvarchar](max) NOT NULL,
	[good_element_take_damage_rate] [int] NOT NULL,
	[weak_element_take_damage_rate] [int] NOT NULL,
	[element_ability_description] [nvarchar](max) NOT NULL,
	[good_element_give_damage_rate] [int] NOT NULL,
	[weak_element_give_damage_rate] [int] NOT NULL,
	[barrier_ability_description] [nvarchar](max) NOT NULL,
	[burning_barrier_type] [int] NOT NULL,
	[frozen_barrier_type] [int] NOT NULL,
	[electrified_barrier_type] [int] NOT NULL,
	[poisoning_barrier_type] [int] NOT NULL,
	[blackout_barrier_type] [int] NOT NULL,
	[boost_ability_description] [nvarchar](max) NOT NULL,
	[boost_power_divergence_type] [int] NOT NULL,
	[boost_power_divergence_range] [int] NOT NULL,
	[purge_ability_description] [nvarchar](max) NOT NULL,
	[purge_barrier_diffusion_type] [int] NOT NULL,
	[purge_barrier_diffusion_range] [int] NOT NULL,
	[boost_buff_effect_type] [int] NOT NULL,
	[purge_buff_effect_type] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

CREATE TABLE [dbo].[RAW_BulletCriticalRaceTable](
	[id] [int] NOT NULL,
	[bullet_id] [int] NOT NULL,
	[race_id] [int] NOT NULL
) ON [PRIMARY];

CREATE TABLE [dbo].[RAW_BulletExtraEffectTable](
	[id] [int] NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[target] [int] NOT NULL,
	[turn] [int] NOT NULL,
	[type] [int] NOT NULL,
	[subtype] [int] NOT NULL,
	[value] [int] NOT NULL
) ON [PRIMARY];

CREATE TABLE [dbo].[RAW_BulletTable](
	[id] [int] NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[element] [int] NOT NULL,
	[type] [int] NOT NULL,
	[category] [int] NOT NULL,
	[power] [float] NOT NULL,
	[hit] [int] NOT NULL,
	[critical] [int] NOT NULL,
	[bullet1_addon_id] [int] NOT NULL,
	[bullet1_addon_value] [int] NOT NULL,
	[bullet2_addon_id] [int] NOT NULL,
	[bullet2_addon_value] [int] NOT NULL,
	[bullet3_addon_id] [int] NOT NULL,
	[bullet3_addon_value] [int] NOT NULL,
	[bullet1_extraeffect_id] [int] NOT NULL,
	[bullet1_extraeffect_success_rate] [int] NOT NULL,
	[bullet2_extraeffect_id] [int] NOT NULL,
	[bullet2_extraeffect_success_rate] [int] NOT NULL,
	[bullet3_extraeffect_id] [int] NOT NULL,
	[bullet3_extraeffect_success_rate] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

CREATE TABLE [dbo].[RAW_CharacteristicTable](
	[id] [int] NOT NULL,
	[characteristic1_name] [nvarchar](max) NOT NULL,
	[characteristic1_description] [nvarchar](max) NOT NULL,
	[characteristic1_type] [int] NOT NULL,
	[characteristic1_effect_type] [int] NOT NULL,
	[characteristic1_effect_subtype] [int] NOT NULL,
	[characteristic1_rate] [int] NOT NULL,
	[characteristic1_effect_value] [int] NOT NULL,
	[characteristic1_icon_filename] [nvarchar](max) NOT NULL,
	[characteristic2_name] [nvarchar](max) NOT NULL,
	[characteristic2_description] [nvarchar](max) NOT NULL,
	[characteristic2_type] [int] NOT NULL,
	[characteristic2_effect_type] [int] NOT NULL,
	[characteristic2_effect_subtype] [int] NOT NULL,
	[characteristic2_rate] [int] NOT NULL,
	[characteristic2_effect_value] [int] NOT NULL,
	[characteristic2_icon_filename] [nvarchar](max) NOT NULL,
	[characteristic3_name] [nvarchar](max) NOT NULL,
	[characteristic3_description] [nvarchar](max) NOT NULL,
	[characteristic3_type] [int] NOT NULL,
	[characteristic3_effect_type] [int] NOT NULL,
	[characteristic3_effect_subtype] [int] NOT NULL,
	[characteristic3_rate] [int] NOT NULL,
	[characteristic3_effect_value] [int] NOT NULL,
	[characteristic3_icon_filename] [nvarchar](max) NOT NULL,
	[trust_characteristic_name] [nvarchar](max) NOT NULL,
	[trust_characteristic_description] [nvarchar](max) NOT NULL,
	[trust_characteristic_rear_effect_type] [int] NOT NULL,
	[trust_characteristic_rear_effect_subtype] [int] NOT NULL,
	[trust_characteristic_avent_effect_type] [int] NOT NULL,
	[trust_characteristic_avent_effect_subtype] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

CREATE TABLE [dbo].[RAW_HitCheckOrderTable](
	[id] [int] NOT NULL,
	[unit_id] [int] NOT NULL,
	[barrage_id] [int] NOT NULL,
	[boost_id] [int] NOT NULL,
	[hit_check_order] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

CREATE TABLE [dbo].[RAW_PersonRelationTable](
	[id] [int] NOT NULL,
	[person_id] [int] NOT NULL,
	[target_person_id] [int] NOT NULL,
	[description] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

CREATE TABLE [dbo].[RAW_PictureTable](
	[id] [int] NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[album_id] [int] NOT NULL,
	[type] [int] NOT NULL,
	[rare] [int] NOT NULL,
	[illustrator_name] [nvarchar](max) NOT NULL,
	[circle_name] [nvarchar](max) NOT NULL,
	[flavor_text1] [nvarchar](max) NOT NULL,
	[flavor_text2] [nvarchar](max) NOT NULL,
	[flavor_text3] [nvarchar](max) NOT NULL,
	[flavor_text4] [nvarchar](max) NOT NULL,
	[flavor_text5] [nvarchar](max) NOT NULL,
	[correction1_type] [int] NOT NULL,
	[correction1_value] [int] NOT NULL,
	[correction1_diff] [int] NOT NULL,
	[correction2_type] [int] NOT NULL,
	[correction2_value] [int] NOT NULL,
	[correction2_diff] [int] NOT NULL,
	[picture_characteristic1_effect_type] [int] NOT NULL,
	[picture_characteristic1_effect_subtype] [int] NOT NULL,
	[picture_characteristic1_effect_value] [int] NOT NULL,
	[picture_characteristic1_effect_value_max] [int] NOT NULL,
	[picture_characteristic1_effect_turn] [int] NOT NULL,
	[picture_characteristic1_effect_range] [int] NOT NULL,
	[picture_characteristic2_effect_type] [int] NOT NULL,
	[picture_characteristic2_effect_subtype] [int] NOT NULL,
	[picture_characteristic2_effect_value] [int] NOT NULL,
	[picture_characteristic2_effect_value_max] [int] NOT NULL,
	[picture_characteristic2_effect_turn] [int] NOT NULL,
	[picture_characteristic2_effect_range] [int] NOT NULL,
	[picture_characteristic3_effect_type] [int] NOT NULL,
	[picture_characteristic3_effect_subtype] [int] NOT NULL,
	[picture_characteristic3_effect_value] [int] NOT NULL,
	[picture_characteristic3_effect_value_max] [int] NOT NULL,
	[picture_characteristic3_effect_turn] [int] NOT NULL,
	[picture_characteristic3_effect_range] [int] NOT NULL,
	[picture_characteristic_text] [nvarchar](max) NOT NULL,
	[picture_characteristic_text_max] [nvarchar](max) NOT NULL,
	[recycle_id] [int] NOT NULL,
	[is_show] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

CREATE TABLE [dbo].[RAW_RaceTable](
	[id] [int] NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[alias_name] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

CREATE TABLE [dbo].[RAW_ShotTable](
	[id] [int] NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[specification] [nvarchar](max) NOT NULL,
	[magazine0_bullet_id] [int] NOT NULL,
	[magazine0_bullet_range] [int] NOT NULL,
	[magazine0_bullet_value] [int] NOT NULL,
	[magazine0_bullet_power_rate] [int] NOT NULL,
	[magazine1_boost_count] [int] NOT NULL,
	[magazine1_bullet_id] [int] NOT NULL,
	[magazine1_bullet_range] [int] NOT NULL,
	[magazine1_bullet_value] [int] NOT NULL,
	[magazine1_bullet_power_rate] [int] NOT NULL,
	[magazine2_boost_count] [int] NOT NULL,
	[magazine2_bullet_id] [int] NOT NULL,
	[magazine2_bullet_range] [int] NOT NULL,
	[magazine2_bullet_value] [int] NOT NULL,
	[magazine2_bullet_power_rate] [int] NOT NULL,
	[magazine3_boost_count] [int] NOT NULL,
	[magazine3_bullet_id] [int] NOT NULL,
	[magazine3_bullet_range] [int] NOT NULL,
	[magazine3_bullet_value] [int] NOT NULL,
	[magazine3_bullet_power_rate] [int] NOT NULL,
	[magazine4_boost_count] [int] NOT NULL,
	[magazine4_bullet_id] [int] NOT NULL,
	[magazine4_bullet_range] [int] NOT NULL,
	[magazine4_bullet_value] [int] NOT NULL,
	[magazine4_bullet_power_rate] [int] NOT NULL,
	[magazine5_boost_count] [int] NOT NULL,
	[magazine5_bullet_id] [int] NOT NULL,
	[magazine5_bullet_range] [int] NOT NULL,
	[magazine5_bullet_value] [int] NOT NULL,
	[magazine5_bullet_power_rate] [int] NOT NULL,
	[phantasm_power_up_rate] [int] NOT NULL,
	[shot_level0_power_rate] [int] NOT NULL,
	[shot_level1_power_rate] [int] NOT NULL,
	[shot_level2_power_rate] [int] NOT NULL,
	[shot_level3_power_rate] [int] NOT NULL,
	[shot_level4_power_rate] [int] NOT NULL,
	[shot_level5_power_rate] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

CREATE TABLE [dbo].[RAW_SkillEffectTable](
	[id] [int] NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[type] [int] NOT NULL,
	[subtype] [int] NOT NULL,
	[range] [int] NOT NULL,
	[turn] [int] NOT NULL,
	[level1_value] [int] NOT NULL,
	[level1_success_rate] [int] NOT NULL,
	[level1_add_value] [int] NOT NULL,
	[level2_value] [int] NOT NULL,
	[level2_success_rate] [int] NOT NULL,
	[level2_add_value] [int] NOT NULL,
	[level3_value] [int] NOT NULL,
	[level3_success_rate] [int] NOT NULL,
	[level3_add_value] [int] NOT NULL,
	[level4_value] [int] NOT NULL,
	[level4_success_rate] [int] NOT NULL,
	[level4_add_value] [int] NOT NULL,
	[level5_value] [int] NOT NULL,
	[level5_success_rate] [int] NOT NULL,
	[level5_add_value] [int] NOT NULL,
	[level6_value] [int] NOT NULL,
	[level6_success_rate] [int] NOT NULL,
	[level6_add_value] [int] NOT NULL,
	[level7_value] [int] NOT NULL,
	[level7_success_rate] [int] NOT NULL,
	[level7_add_value] [int] NOT NULL,
	[level8_value] [int] NOT NULL,
	[level8_success_rate] [int] NOT NULL,
	[level8_add_value] [int] NOT NULL,
	[level9_value] [int] NOT NULL,
	[level9_success_rate] [int] NOT NULL,
	[level9_add_value] [int] NOT NULL,
	[level10_value] [int] NOT NULL,
	[level10_success_rate] [int] NOT NULL,
	[level10_add_value] [int] NOT NULL,
	[icon_filename] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

CREATE TABLE [dbo].[RAW_SkillTable](
	[id] [int] NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[type] [int] NOT NULL,
	[exp_id] [int] NOT NULL,
	[level1_turn] [int] NOT NULL,
	[level2_turn] [int] NOT NULL,
	[level3_turn] [int] NOT NULL,
	[level4_turn] [int] NOT NULL,
	[level5_turn] [int] NOT NULL,
	[level6_turn] [int] NOT NULL,
	[level7_turn] [int] NOT NULL,
	[level8_turn] [int] NOT NULL,
	[level9_turn] [int] NOT NULL,
	[level10_turn] [int] NOT NULL,
	[effect1_id] [int] NOT NULL,
	[effect1_level_type] [int] NOT NULL,
	[effect1_level_value] [int] NOT NULL,
	[effect2_id] [int] NOT NULL,
	[effect2_level_type] [int] NOT NULL,
	[effect2_level_value] [int] NOT NULL,
	[effect3_id] [int] NOT NULL,
	[effect3_level_type] [int] NOT NULL,
	[effect3_level_value] [int] NOT NULL,
	[icon_filename] [nvarchar](max) NOT NULL,
	[reincarnation_level_name] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

CREATE TABLE [dbo].[RAW_SpellcardTable](
	[id] [int] NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[specification] [nvarchar](max) NOT NULL,
	[type] [int] NOT NULL,
	[magazine0_bullet_id] [int] NOT NULL,
	[magazine0_bullet_range] [int] NOT NULL,
	[magazine0_bullet_value] [int] NOT NULL,
	[magazine0_bullet_power_rate] [int] NOT NULL,
	[magazine1_boost_count] [int] NOT NULL,
	[magazine1_bullet_id] [int] NOT NULL,
	[magazine1_bullet_range] [int] NOT NULL,
	[magazine1_bullet_value] [int] NOT NULL,
	[magazine1_bullet_power_rate] [int] NOT NULL,
	[magazine2_boost_count] [int] NOT NULL,
	[magazine2_bullet_id] [int] NOT NULL,
	[magazine2_bullet_range] [int] NOT NULL,
	[magazine2_bullet_value] [int] NOT NULL,
	[magazine2_bullet_power_rate] [int] NOT NULL,
	[magazine3_boost_count] [int] NOT NULL,
	[magazine3_bullet_id] [int] NOT NULL,
	[magazine3_bullet_range] [int] NOT NULL,
	[magazine3_bullet_value] [int] NOT NULL,
	[magazine3_bullet_power_rate] [int] NOT NULL,
	[magazine4_boost_count] [int] NOT NULL,
	[magazine4_bullet_id] [int] NOT NULL,
	[magazine4_bullet_range] [int] NOT NULL,
	[magazine4_bullet_value] [int] NOT NULL,
	[magazine4_bullet_power_rate] [int] NOT NULL,
	[magazine5_boost_count] [int] NOT NULL,
	[magazine5_bullet_id] [int] NOT NULL,
	[magazine5_bullet_range] [int] NOT NULL,
	[magazine5_bullet_value] [int] NOT NULL,
	[magazine5_bullet_power_rate] [int] NOT NULL,
	[phantasm_power_up_rate] [int] NOT NULL,
	[shot_level0_power_rate] [int] NOT NULL,
	[shot_level1_power_rate] [int] NOT NULL,
	[shot_level2_power_rate] [int] NOT NULL,
	[shot_level3_power_rate] [int] NOT NULL,
	[shot_level4_power_rate] [int] NOT NULL,
	[shot_level5_power_rate] [int] NOT NULL,
	[spellcard_skill1_effect_id] [int] NOT NULL,
	[spellcard_skill1_level_type] [int] NOT NULL,
	[spellcard_skill1_level_value] [int] NOT NULL,
	[spellcard_skill1_timing] [int] NOT NULL,
	[spellcard_skill2_effect_id] [int] NOT NULL,
	[spellcard_skill2_level_type] [int] NOT NULL,
	[spellcard_skill2_level_value] [int] NOT NULL,
	[spellcard_skill2_timing] [int] NOT NULL,
	[spellcard_skill3_effect_id] [int] NOT NULL,
	[spellcard_skill3_level_type] [int] NOT NULL,
	[spellcard_skill3_level_value] [int] NOT NULL,
	[spellcard_skill3_timing] [int] NOT NULL,
	[spellcard_skill4_effect_id] [int] NOT NULL,
	[spellcard_skill4_level_type] [int] NOT NULL,
	[spellcard_skill4_level_value] [int] NOT NULL,
	[spellcard_skill4_timing] [int] NOT NULL,
	[spellcard_skill5_effect_id] [int] NOT NULL,
	[spellcard_skill5_level_type] [int] NOT NULL,
	[spellcard_skill5_level_value] [int] NOT NULL,
	[spellcard_skill5_timing] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

CREATE TABLE [dbo].[RAW_UnitRaceTable](
	[id] [int] NOT NULL,
	[unit_id] [int] NOT NULL,
	[race_id] [int] NOT NULL
) ON [PRIMARY];

CREATE TABLE [dbo].[RAW_UnitTable](
	[id] [int] NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[name_kana] [nvarchar](max) NOT NULL,
	[alias_name] [nvarchar](max) NOT NULL,
	[short_name] [nvarchar](max) NOT NULL,
	[person_id] [int] NOT NULL,
	[album_id] [int] NOT NULL,
	[role] [int] NOT NULL,
	[exp_id] [int] NOT NULL,
	[symbol_name] [nvarchar](max) NOT NULL,
	[symbol_title] [nvarchar](max) NOT NULL,
	[symbol_description] [nvarchar](max) NOT NULL,
	[life_point] [int] NOT NULL,
	[yang_attack] [int] NOT NULL,
	[yang_defense] [int] NOT NULL,
	[yin_attack] [int] NOT NULL,
	[yin_defense] [int] NOT NULL,
	[speed] [int] NOT NULL,
	[shot1_id] [int] NOT NULL,
	[shot2_id] [int] NOT NULL,
	[spellcard1_id] [int] NOT NULL,
	[spellcard2_id] [int] NOT NULL,
	[spellcard3_id] [int] NOT NULL,
	[spellcard4_id] [int] NOT NULL,
	[spellcard5_id] [int] NOT NULL,
	[skill1_id] [int] NOT NULL,
	[skill2_id] [int] NOT NULL,
	[skill3_id] [int] NOT NULL,
	[resist_id] [int] NOT NULL,
	[characteristic_id] [int] NOT NULL,
	[ability_id] [int] NOT NULL,
	[recycle_id] [int] NOT NULL,
	[default_costume_id] [int] NOT NULL,
	[drop_text] [nvarchar](max) NOT NULL,
	[limitbreak_item_id] [int] NOT NULL,
	[spellcard_bgm_id] [int] NOT NULL,
	[name_sub] [nvarchar](max) NOT NULL,
	[name_kana_sub] [nvarchar](max) NOT NULL,
	[short_name_sub] [nvarchar](max) NOT NULL,
	[is_show] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];


/* step 4: insert data from csv (note that source of HitCheckOrder is special) */
TRUNCATE TABLE [dbo].[RAW_AbilityTable];
BULK INSERT [dbo].[RAW_AbilityTable]
FROM 'AbilityTable.csv'
WITH
(
	DATA_SOURCE='csvdatas',
	CODEPAGE = '65001',
	DATAFILETYPE = 'Char',
	FIRSTROW = 2,
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '0x0a',
	TABLOCK
);

TRUNCATE TABLE [dbo].[RAW_BulletCriticalRaceTable];
BULK INSERT [dbo].[RAW_BulletCriticalRaceTable]
FROM 'BulletCriticalRaceTable.csv'
WITH
(
	DATA_SOURCE='csvdatas',
	CODEPAGE = '65001',
	DATAFILETYPE = 'Char',
	FIRSTROW = 2,
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '0x0a',
	TABLOCK
);

TRUNCATE TABLE [dbo].[RAW_BulletExtraEffectTable];
BULK INSERT [dbo].[RAW_BulletExtraEffectTable]
FROM 'BulletExtraEffectTable.csv'
WITH
(
	DATA_SOURCE='csvdatas',
	CODEPAGE = '65001',
	DATAFILETYPE = 'Char',
	FIRSTROW = 2,
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '0x0a',
	TABLOCK
);

TRUNCATE TABLE [dbo].[RAW_BulletTable];
BULK INSERT [dbo].[RAW_BulletTable]
FROM 'BulletTable.csv'
WITH
(
	DATA_SOURCE='csvdatas',
	CODEPAGE = '65001',
	DATAFILETYPE = 'Char',
	FIRSTROW = 2,
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '0x0a',
	TABLOCK
);

TRUNCATE TABLE [dbo].[RAW_CharacteristicTable];
BULK INSERT [dbo].[RAW_CharacteristicTable]
FROM 'CharacteristicTable.csv'
WITH
(
	DATA_SOURCE='csvdatas',
	CODEPAGE = '65001',
	DATAFILETYPE = 'Char',
	FIRSTROW = 2,
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '0x0a',
	TABLOCK
);

TRUNCATE TABLE [dbo].[RAW_HitCheckOrderTable];
BULK INSERT [dbo].[RAW_HitCheckOrderTable]
FROM 'HitCheckOrderTable.csv'
WITH
(
	DATA_SOURCE='csvdatas',
	CODEPAGE = '65001',
	DATAFILETYPE = 'Char',
	FIRSTROW = 2,
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '0x0a',
	TABLOCK
);

TRUNCATE TABLE [dbo].[RAW_PersonRelationTable];
BULK INSERT [dbo].[RAW_PersonRelationTable]
FROM 'PersonRelationTable.csv'
WITH
(
	DATA_SOURCE='csvdatas',
	CODEPAGE = '65001',
	DATAFILETYPE = 'Char',
	FIRSTROW = 2,
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '0x0a',
	TABLOCK
);

TRUNCATE TABLE [dbo].[RAW_PictureTable];
BULK INSERT [dbo].[RAW_PictureTable]
FROM 'PictureTable.csv'
WITH
(
	DATA_SOURCE='csvdatas',
	CODEPAGE = '65001',
	DATAFILETYPE = 'Char',
	FIRSTROW = 2,
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '0x0a',
	TABLOCK
);

TRUNCATE TABLE [dbo].[RAW_RaceTable];
BULK INSERT [dbo].[RAW_RaceTable]
FROM 'RaceTable.csv'
WITH
(
	DATA_SOURCE='csvdatas',
	CODEPAGE = '65001',
	DATAFILETYPE = 'Char',
	FIRSTROW = 2,
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '0x0a',
	TABLOCK
);

TRUNCATE TABLE [dbo].[RAW_ShotTable];
BULK INSERT [dbo].[RAW_ShotTable]
FROM 'ShotTable.csv'
WITH
(
	DATA_SOURCE='csvdatas',
	CODEPAGE = '65001',
	DATAFILETYPE = 'Char',
	FIRSTROW = 2,
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '0x0a',
	TABLOCK
);

TRUNCATE TABLE [dbo].[RAW_SkillEffectTable];
BULK INSERT [dbo].[RAW_SkillEffectTable]
FROM 'SkillEffectTable.csv'
WITH
(
	DATA_SOURCE='csvdatas',
	CODEPAGE = '65001',
	DATAFILETYPE = 'Char',
	FIRSTROW = 2,
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '0x0a',
	TABLOCK
);

TRUNCATE TABLE [dbo].[RAW_SkillTable];
BULK INSERT [dbo].[RAW_SkillTable]
FROM 'SkillTable.csv'
WITH
(
	DATA_SOURCE='csvdatas',
	CODEPAGE = '65001',
	DATAFILETYPE = 'Char',
	FIRSTROW = 2,
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '0x0a',
	TABLOCK
);

TRUNCATE TABLE [dbo].[RAW_SpellcardTable];
BULK INSERT [dbo].[RAW_SpellcardTable]
FROM 'SpellcardTable.csv'
WITH
(
	DATA_SOURCE='csvdatas',
	CODEPAGE = '65001',
	DATAFILETYPE = 'Char',
	FIRSTROW = 2,
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '0x0a',
	TABLOCK
);

TRUNCATE TABLE [dbo].[RAW_UnitRaceTable];
BULK INSERT [dbo].[RAW_UnitRaceTable]
FROM 'UnitRaceTable.csv'
WITH
(
	DATA_SOURCE='csvdatas',
	CODEPAGE = '65001',
	DATAFILETYPE = 'Char',
	FIRSTROW = 2,
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '0x0a',
	TABLOCK
);

TRUNCATE TABLE [dbo].[RAW_UnitTable];
BULK INSERT [dbo].[RAW_UnitTable]
FROM 'UnitTable.csv'
WITH
(
	DATA_SOURCE='csvdatas',
	CODEPAGE = '65001',
	DATAFILETYPE = 'Char',
	FIRSTROW = 2,
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '0x0a',
	TABLOCK
);

/* step 5: prepare data for website */
delete from [dbo].[PersonRelationData];
delete from [dbo].[PictureData];
delete from [dbo].[PlayerUnitCharacteristicData];
delete from [dbo].[PlayerUnitData];
delete from [dbo].[PlayerUnitShotData];
delete from [dbo].[PlayerUnitSpellcardData];
delete from [dbo].[RaceData];
delete from [dbo].[PlayerUnitRaceData];
delete from [dbo].[PlayerUnitBulletData];
delete from [dbo].[PlayerUnitBulletCriticalRaceData];
delete from [dbo].[PlayerUnitHitCheckOrderData];
delete from [dbo].[PlayerUnitSkillData];
delete from [dbo].[PlayerUnitSkillEffectData];
delete from [dbo].[PlayerUnitAbilityData];
delete from [dbo].[BulletExtraEffectData];

SET IDENTITY_INSERT [dbo].[PlayerUnitData] ON;
insert into [dbo].[PlayerUnitData] (
[id],[name],[short_name],[person_id],[role],[symbol_name],[life_point],[yang_attack],[yang_defense],[yin_attack],[yin_defense],[speed],[shot1_id],[shot2_id],[spellcard1_id],[spellcard2_id],[spellcard3_id],[spellcard4_id],[spellcard5_id],[skill1_id],[skill2_id],[skill3_id],[resist_id],[characteristic_id],[ability_id],[limitbreak_item_id]
)
select
[id],[name],[short_name],[person_id],[role],[symbol_name],[life_point],[yang_attack],[yang_defense],[yin_attack],[yin_defense],[speed],[shot1_id],[shot2_id],[spellcard1_id],[spellcard2_id],[spellcard3_id],[spellcard4_id],[spellcard5_id],[skill1_id],[skill2_id],[skill3_id],[resist_id],[characteristic_id],[ability_id],[limitbreak_item_id]
from [dbo].[RAW_UnitTable]
where symbol_name != '(empty)' and (shot1_id != 10011 or id = 1001) and alias_name != '_TEST';
SET IDENTITY_INSERT [dbo].[PlayerUnitData] OFF;

SET IDENTITY_INSERT [dbo].[PersonRelationData] ON;
INSERT INTO [dbo].[PersonRelationData] (
[id],[person_id],[target_person_id],[description]
)
SELECT * FROM [dbo].[RAW_PersonRelationTable];
SET IDENTITY_INSERT [dbo].[PersonRelationData] OFF;

SET IDENTITY_INSERT [dbo].[PlayerUnitShotData] ON;
INSERT INTO [dbo].[PlayerUnitShotData] (
[id],[name],[magazine0_bullet_id],[magazine0_bullet_range],[magazine0_bullet_value],[magazine0_bullet_power_rate],[magazine1_boost_count],[magazine1_bullet_id],[magazine1_bullet_range],[magazine1_bullet_value],[magazine1_bullet_power_rate],[magazine2_boost_count],[magazine2_bullet_id],[magazine2_bullet_range],[magazine2_bullet_value],[magazine2_bullet_power_rate],[magazine3_boost_count],[magazine3_bullet_id],[magazine3_bullet_range],[magazine3_bullet_value],[magazine3_bullet_power_rate],[magazine4_boost_count],[magazine4_bullet_id],[magazine4_bullet_range],[magazine4_bullet_value],[magazine4_bullet_power_rate],[magazine5_boost_count],[magazine5_bullet_id],[magazine5_bullet_range],[magazine5_bullet_value],[magazine5_bullet_power_rate],[phantasm_power_up_rate],[shot_level0_power_rate],[shot_level1_power_rate],[shot_level2_power_rate],[shot_level3_power_rate],[shot_level4_power_rate],[shot_level5_power_rate]
)
select
B.[id],B.[name],B.[magazine0_bullet_id],B.[magazine0_bullet_range],B.[magazine0_bullet_value],B.[magazine0_bullet_power_rate],B.[magazine1_boost_count],B.[magazine1_bullet_id],B.[magazine1_bullet_range],B.[magazine1_bullet_value],B.[magazine1_bullet_power_rate],B.[magazine2_boost_count],B.[magazine2_bullet_id],B.[magazine2_bullet_range],B.[magazine2_bullet_value],B.[magazine2_bullet_power_rate],B.[magazine3_boost_count],B.[magazine3_bullet_id],B.[magazine3_bullet_range],B.[magazine3_bullet_value],B.[magazine3_bullet_power_rate],B.[magazine4_boost_count],B.[magazine4_bullet_id],B.[magazine4_bullet_range],B.[magazine4_bullet_value],B.[magazine4_bullet_power_rate],B.[magazine5_boost_count],B.[magazine5_bullet_id],B.[magazine5_bullet_range],B.[magazine5_bullet_value],B.[magazine5_bullet_power_rate],B.[phantasm_power_up_rate],B.[shot_level0_power_rate],B.[shot_level1_power_rate],B.[shot_level2_power_rate],B.[shot_level3_power_rate],B.[shot_level4_power_rate],B.[shot_level5_power_rate]
from [dbo].[PlayerUnitData] as A inner join [dbo].[RAW_ShotTable] as B on A.shot1_id = B.id or A.shot2_id = B.id
SET IDENTITY_INSERT [dbo].[PlayerUnitShotData] OFF;

SET IDENTITY_INSERT [dbo].[PlayerUnitSpellcardData] ON;
INSERT INTO [dbo].[PlayerUnitSpellcardData] (
[id],[name],[type],[magazine0_bullet_id],[magazine0_bullet_range],[magazine0_bullet_value],[magazine0_bullet_power_rate],[magazine1_boost_count],[magazine1_bullet_id],[magazine1_bullet_range],[magazine1_bullet_value],[magazine1_bullet_power_rate],[magazine2_boost_count],[magazine2_bullet_id],[magazine2_bullet_range],[magazine2_bullet_value],[magazine2_bullet_power_rate],[magazine3_boost_count],[magazine3_bullet_id],[magazine3_bullet_range],[magazine3_bullet_value],[magazine3_bullet_power_rate],[magazine4_boost_count],[magazine4_bullet_id],[magazine4_bullet_range],[magazine4_bullet_value],[magazine4_bullet_power_rate],[magazine5_boost_count],[magazine5_bullet_id],[magazine5_bullet_range],[magazine5_bullet_value],[magazine5_bullet_power_rate],[phantasm_power_up_rate],[shot_level0_power_rate],[shot_level1_power_rate],[shot_level2_power_rate],[shot_level3_power_rate],[shot_level4_power_rate],[shot_level5_power_rate],[spellcard_skill1_effect_id],[spellcard_skill1_level_type],[spellcard_skill1_level_value],[spellcard_skill1_timing],[spellcard_skill2_effect_id],[spellcard_skill2_level_type],[spellcard_skill2_level_value],[spellcard_skill2_timing],[spellcard_skill3_effect_id],[spellcard_skill3_level_type],[spellcard_skill3_level_value],[spellcard_skill3_timing],[spellcard_skill4_effect_id],[spellcard_skill4_level_type],[spellcard_skill4_level_value],[spellcard_skill4_timing],[spellcard_skill5_effect_id],[spellcard_skill5_level_type],[spellcard_skill5_level_value],[spellcard_skill5_timing]
)
select
B.[id],B.[name],B.[type],B.[magazine0_bullet_id],B.[magazine0_bullet_range],B.[magazine0_bullet_value],B.[magazine0_bullet_power_rate],B.[magazine1_boost_count],B.[magazine1_bullet_id],B.[magazine1_bullet_range],B.[magazine1_bullet_value],B.[magazine1_bullet_power_rate],B.[magazine2_boost_count],B.[magazine2_bullet_id],B.[magazine2_bullet_range],B.[magazine2_bullet_value],B.[magazine2_bullet_power_rate],B.[magazine3_boost_count],B.[magazine3_bullet_id],B.[magazine3_bullet_range],B.[magazine3_bullet_value],B.[magazine3_bullet_power_rate],B.[magazine4_boost_count],B.[magazine4_bullet_id],B.[magazine4_bullet_range],B.[magazine4_bullet_value],B.[magazine4_bullet_power_rate],B.[magazine5_boost_count],B.[magazine5_bullet_id],B.[magazine5_bullet_range],B.[magazine5_bullet_value],B.[magazine5_bullet_power_rate],B.[phantasm_power_up_rate],B.[shot_level0_power_rate],B.[shot_level1_power_rate],B.[shot_level2_power_rate],B.[shot_level3_power_rate],B.[shot_level4_power_rate],B.[shot_level5_power_rate],B.[spellcard_skill1_effect_id],B.[spellcard_skill1_level_type],B.[spellcard_skill1_level_value],B.[spellcard_skill1_timing],B.[spellcard_skill2_effect_id],B.[spellcard_skill2_level_type],B.[spellcard_skill2_level_value],B.[spellcard_skill2_timing],B.[spellcard_skill3_effect_id],B.[spellcard_skill3_level_type],B.[spellcard_skill3_level_value],B.[spellcard_skill3_timing],B.[spellcard_skill4_effect_id],B.[spellcard_skill4_level_type],B.[spellcard_skill4_level_value],B.[spellcard_skill4_timing],B.[spellcard_skill5_effect_id],B.[spellcard_skill5_level_type],B.[spellcard_skill5_level_value],B.[spellcard_skill5_timing]
from [dbo].[PlayerUnitData] as A inner join [dbo].[RAW_SpellcardTable] as B on A.spellcard1_id = B.id or A.spellcard2_id = B.id or A.spellcard3_id = B.id or A.spellcard4_id = B.id or A.spellcard5_id = B.id
SET IDENTITY_INSERT [dbo].[PlayerUnitSpellcardData] OFF;

SET IDENTITY_INSERT [dbo].[PictureData] ON;
INSERT INTO [dbo].[PictureData] (
[id],[name],[type],[rare],[correction1_type],[correction1_value],[correction1_diff],[correction2_type],[correction2_value],[correction2_diff],[picture_characteristic1_effect_type],[picture_characteristic1_effect_subtype],[picture_characteristic1_effect_value],[picture_characteristic1_effect_value_max],[picture_characteristic1_effect_turn],[picture_characteristic1_effect_range],[picture_characteristic2_effect_type],[picture_characteristic2_effect_subtype],[picture_characteristic2_effect_value],[picture_characteristic2_effect_value_max],[picture_characteristic2_effect_turn],[picture_characteristic2_effect_range],[picture_characteristic3_effect_type],[picture_characteristic3_effect_subtype],[picture_characteristic3_effect_value],[picture_characteristic3_effect_value_max],[picture_characteristic3_effect_turn],[picture_characteristic3_effect_range],[picture_characteristic_text],[picture_characteristic_text_max]
)
select
[id],[name],[type],[rare],[correction1_type],[correction1_value],[correction1_diff],[correction2_type],[correction2_value],[correction2_diff],[picture_characteristic1_effect_type],[picture_characteristic1_effect_subtype],[picture_characteristic1_effect_value],[picture_characteristic1_effect_value_max],[picture_characteristic1_effect_turn],[picture_characteristic1_effect_range],[picture_characteristic2_effect_type],[picture_characteristic2_effect_subtype],[picture_characteristic2_effect_value],[picture_characteristic2_effect_value_max],[picture_characteristic2_effect_turn],[picture_characteristic2_effect_range],[picture_characteristic3_effect_type],[picture_characteristic3_effect_subtype],[picture_characteristic3_effect_value],[picture_characteristic3_effect_value_max],[picture_characteristic3_effect_turn],[picture_characteristic3_effect_range],[picture_characteristic_text],[picture_characteristic_text_max]
from [dbo].[RAW_PictureTable]
SET IDENTITY_INSERT [dbo].[PictureData] OFF;

SET IDENTITY_INSERT [dbo].[RaceData] ON;
INSERT INTO [dbo].[RaceData] (
[id],[name]
)
select [id],[name] from [dbo].[RAW_RaceTable]
SET IDENTITY_INSERT [dbo].[RaceData] OFF;

SET IDENTITY_INSERT [dbo].[PlayerUnitRaceData] ON;
INSERT INTO [dbo].[PlayerUnitRaceData] (
[id],[unit_id],[race_id]
)
select * from [dbo].[RAW_UnitRaceTable]
SET IDENTITY_INSERT [dbo].[PlayerUnitRaceData] OFF;

SET IDENTITY_INSERT [dbo].[PlayerUnitBulletData] ON;
INSERT INTO [dbo].[PlayerUnitBulletData] (
[id],[name],[element],[type],[category],[power],[hit],[critical],[bullet1_addon_id],[bullet1_addon_value],[bullet2_addon_id],[bullet2_addon_value],[bullet3_addon_id],[bullet3_addon_value],[bullet1_extraeffect_id],[bullet1_extraeffect_success_rate],[bullet2_extraeffect_id],[bullet2_extraeffect_success_rate],[bullet3_extraeffect_id],[bullet3_extraeffect_success_rate]
)
select
B.[id],B.[name],B.[element],B.[type],B.[category],B.[power],B.[hit],B.[critical],B.[bullet1_addon_id],B.[bullet1_addon_value],B.[bullet2_addon_id],B.[bullet2_addon_value],B.[bullet3_addon_id],B.[bullet3_addon_value],B.[bullet1_extraeffect_id],B.[bullet1_extraeffect_success_rate],B.[bullet2_extraeffect_id],B.[bullet2_extraeffect_success_rate],B.[bullet3_extraeffect_id],B.[bullet3_extraeffect_success_rate]
from [dbo].[PlayerUnitSpellcardData] as A inner join [dbo].[RAW_BulletTable] as B on (A.magazine0_bullet_id = B.id or A.magazine1_bullet_id = B.id or A.magazine2_bullet_id = B.id or A.magazine3_bullet_id = B.id or A.magazine4_bullet_id = B.id or A.magazine5_bullet_id = B.id)
INSERT INTO [dbo].[PlayerUnitBulletData] (
[id],[name],[element],[type],[category],[power],[hit],[critical],[bullet1_addon_id],[bullet1_addon_value],[bullet2_addon_id],[bullet2_addon_value],[bullet3_addon_id],[bullet3_addon_value],[bullet1_extraeffect_id],[bullet1_extraeffect_success_rate],[bullet2_extraeffect_id],[bullet2_extraeffect_success_rate],[bullet3_extraeffect_id],[bullet3_extraeffect_success_rate]
)
select
B.[id],B.[name],B.[element],B.[type],B.[category],B.[power],B.[hit],B.[critical],B.[bullet1_addon_id],B.[bullet1_addon_value],B.[bullet2_addon_id],B.[bullet2_addon_value],B.[bullet3_addon_id],B.[bullet3_addon_value],B.[bullet1_extraeffect_id],B.[bullet1_extraeffect_success_rate],B.[bullet2_extraeffect_id],B.[bullet2_extraeffect_success_rate],B.[bullet3_extraeffect_id],B.[bullet3_extraeffect_success_rate]
from [dbo].[PlayerUnitShotData] as A inner join [dbo].[RAW_BulletTable] as B on (A.magazine0_bullet_id = B.id or A.magazine1_bullet_id = B.id or A.magazine2_bullet_id = B.id or A.magazine3_bullet_id = B.id or A.magazine4_bullet_id = B.id or A.magazine5_bullet_id = B.id)
SET IDENTITY_INSERT [dbo].[PlayerUnitBulletData] OFF;

SET IDENTITY_INSERT [dbo].[PlayerUnitBulletCriticalRaceData] ON;
INSERT INTO [dbo].[PlayerUnitBulletCriticalRaceData] (
[id],[bullet_id],[race_id]
)
select B.* from [dbo].[PlayerUnitBulletData] as A inner join [dbo].[RAW_BulletCriticalRaceTable] as B on (A.id = B.bullet_id)
SET IDENTITY_INSERT [dbo].[PlayerUnitBulletCriticalRaceData] OFF;

SET IDENTITY_INSERT [dbo].[PlayerUnitCharacteristicData] ON;
INSERT INTO [dbo].[PlayerUnitCharacteristicData] (
[id],[characteristic1_name],[characteristic1_description],[characteristic1_type],[characteristic1_effect_type],[characteristic1_effect_subtype],[characteristic1_rate],[characteristic1_effect_value],[characteristic2_name],[characteristic2_description],[characteristic2_type],[characteristic2_effect_type],[characteristic2_effect_subtype],[characteristic2_rate],[characteristic2_effect_value],[characteristic3_name],[characteristic3_description],[characteristic3_type],[characteristic3_effect_type],[characteristic3_effect_subtype],[characteristic3_rate],[characteristic3_effect_value],[trust_characteristic_name],[trust_characteristic_rear_effect_type],[trust_characteristic_rear_effect_subtype],[trust_characteristic_avent_effect_type],[trust_characteristic_avent_effect_subtype]
)
select
B.[id],B.[characteristic1_name],B.[characteristic1_description],B.[characteristic1_type],B.[characteristic1_effect_type],B.[characteristic1_effect_subtype],B.[characteristic1_rate],B.[characteristic1_effect_value],B.[characteristic2_name],B.[characteristic2_description],B.[characteristic2_type],B.[characteristic2_effect_type],B.[characteristic2_effect_subtype],B.[characteristic2_rate],B.[characteristic2_effect_value],B.[characteristic3_name],B.[characteristic3_description],B.[characteristic3_type],B.[characteristic3_effect_type],B.[characteristic3_effect_subtype],B.[characteristic3_rate],B.[characteristic3_effect_value],B.[trust_characteristic_name],B.[trust_characteristic_rear_effect_type],B.[trust_characteristic_rear_effect_subtype],B.[trust_characteristic_avent_effect_type],B.[trust_characteristic_avent_effect_subtype]
from [dbo].[PlayerUnitData] as A inner join [dbo].[RAW_CharacteristicTable] as B on A.characteristic_id = B.id
SET IDENTITY_INSERT [dbo].[PlayerUnitCharacteristicData] OFF;

SET IDENTITY_INSERT [dbo].[PlayerUnitHitCheckOrderData] ON;
INSERT INTO [dbo].[PlayerUnitHitCheckOrderData] (
[id],[unit_id],[barrage_id],[boost_id],[hit_check_order]
)
select B.* from [dbo].[PlayerUnitData] as A inner join [dbo].[RAW_HitCheckOrderTable] as B on A.id = B.unit_id
SET IDENTITY_INSERT [dbo].[PlayerUnitHitCheckOrderData] OFF;

SET IDENTITY_INSERT [dbo].[PlayerUnitSkillData] ON;
INSERT INTO [dbo].[PlayerUnitSkillData] (
[id],[name],[type],[exp_id],[level1_turn],[level2_turn],[level3_turn],[level4_turn],[level5_turn],[level6_turn],[level7_turn],[level8_turn],[level9_turn],[level10_turn],[effect1_id],[effect1_level_type],[effect1_level_value],[effect2_id],[effect2_level_type],[effect2_level_value],[effect3_id],[effect3_level_type],[effect3_level_value]
)
select
B.[id],B.[name],B.[type],B.[exp_id],B.[level1_turn],B.[level2_turn],B.[level3_turn],B.[level4_turn],B.[level5_turn],B.[level6_turn],B.[level7_turn],B.[level8_turn],B.[level9_turn],B.[level10_turn],B.[effect1_id],B.[effect1_level_type],B.[effect1_level_value],B.[effect2_id],B.[effect2_level_type],B.[effect2_level_value],B.[effect3_id],B.[effect3_level_type],B.[effect3_level_value]
from [dbo].[PlayerUnitData] as A inner join [dbo].[RAW_SkillTable] as B on A.skill1_id = B.id or A.skill2_id = B.id or A.skill3_id = B.id
SET IDENTITY_INSERT [dbo].[PlayerUnitSkillData] OFF;

SET IDENTITY_INSERT [dbo].[PlayerUnitSkillEffectData] ON;
INSERT INTO [dbo].[PlayerUnitSkillEffectData] (
[id],[name],[description],[type],[subtype],[range],[turn],[level1_value],[level1_success_rate],[level1_add_value],[level2_value],[level2_success_rate],[level2_add_value],[level3_value],[level3_success_rate],[level3_add_value],[level4_value],[level4_success_rate],[level4_add_value],[level5_value],[level5_success_rate],[level5_add_value],[level6_value],[level6_success_rate],[level6_add_value],[level7_value],[level7_success_rate],[level7_add_value],[level8_value],[level8_success_rate],[level8_add_value],[level9_value],[level9_success_rate],[level9_add_value],[level10_value],[level10_success_rate],[level10_add_value]
)
select
B.[id],B.[name],B.[description],B.[type],B.[subtype],B.[range],B.[turn],B.[level1_value],B.[level1_success_rate],B.[level1_add_value],B.[level2_value],B.[level2_success_rate],B.[level2_add_value],B.[level3_value],B.[level3_success_rate],B.[level3_add_value],B.[level4_value],B.[level4_success_rate],B.[level4_add_value],B.[level5_value],B.[level5_success_rate],B.[level5_add_value],B.[level6_value],B.[level6_success_rate],B.[level6_add_value],B.[level7_value],B.[level7_success_rate],B.[level7_add_value],B.[level8_value],B.[level8_success_rate],B.[level8_add_value],B.[level9_value],B.[level9_success_rate],B.[level9_add_value],B.[level10_value],B.[level10_success_rate],B.[level10_add_value]
from [dbo].[PlayerUnitSkillData] as A inner join [dbo].[RAW_SkillEffectTable] as B on A.effect1_id = B.id or A.effect2_id = B.id or A.effect3_id = B.id
INSERT INTO [dbo].[PlayerUnitSkillEffectData] (
[id],[name],[description],[type],[subtype],[range],[turn],[level1_value],[level1_success_rate],[level1_add_value],[level2_value],[level2_success_rate],[level2_add_value],[level3_value],[level3_success_rate],[level3_add_value],[level4_value],[level4_success_rate],[level4_add_value],[level5_value],[level5_success_rate],[level5_add_value],[level6_value],[level6_success_rate],[level6_add_value],[level7_value],[level7_success_rate],[level7_add_value],[level8_value],[level8_success_rate],[level8_add_value],[level9_value],[level9_success_rate],[level9_add_value],[level10_value],[level10_success_rate],[level10_add_value]
)
select
B.[id],B.[name],B.[description],B.[type],B.[subtype],B.[range],B.[turn],B.[level1_value],B.[level1_success_rate],B.[level1_add_value],B.[level2_value],B.[level2_success_rate],B.[level2_add_value],B.[level3_value],B.[level3_success_rate],B.[level3_add_value],B.[level4_value],B.[level4_success_rate],B.[level4_add_value],B.[level5_value],B.[level5_success_rate],B.[level5_add_value],B.[level6_value],B.[level6_success_rate],B.[level6_add_value],B.[level7_value],B.[level7_success_rate],B.[level7_add_value],B.[level8_value],B.[level8_success_rate],B.[level8_add_value],B.[level9_value],B.[level9_success_rate],B.[level9_add_value],B.[level10_value],B.[level10_success_rate],B.[level10_add_value]
from [dbo].[PlayerUnitSpellcardData] as A inner join [dbo].[RAW_SkillEffectTable] as B on A.spellcard_skill1_effect_id = B.id or A.spellcard_skill2_effect_id = B.id or A.spellcard_skill3_effect_id = B.id or A.spellcard_skill4_effect_id = B.id or A.spellcard_skill5_effect_id = B.id
SET IDENTITY_INSERT [dbo].[PlayerUnitSkillEffectData] OFF;

SET IDENTITY_INSERT [dbo].[PlayerUnitAbilityData] ON;
INSERT INTO [dbo].[PlayerUnitAbilityData] (
[id],[name],[resist_ability_description],[good_element_take_damage_rate],[weak_element_take_damage_rate],[element_ability_description],[good_element_give_damage_rate],[weak_element_give_damage_rate],[barrier_ability_description],[burning_barrier_type],[frozen_barrier_type],[electrified_barrier_type],[poisoning_barrier_type],[blackout_barrier_type],[boost_ability_description],[boost_power_divergence_type],[boost_power_divergence_range],[purge_ability_description],[purge_barrier_diffusion_type],[purge_barrier_diffusion_range],[boost_buff_effect_type],[purge_buff_effect_type]
)
select
B.[id],B.[name],B.[resist_ability_description],B.[good_element_take_damage_rate],B.[weak_element_take_damage_rate],B.[element_ability_description],B.[good_element_give_damage_rate],B.[weak_element_give_damage_rate],B.[barrier_ability_description],B.[burning_barrier_type],B.[frozen_barrier_type],B.[electrified_barrier_type],B.[poisoning_barrier_type],B.[blackout_barrier_type],B.[boost_ability_description],B.[boost_power_divergence_type],B.[boost_power_divergence_range],B.[purge_ability_description],B.[purge_barrier_diffusion_type],B.[purge_barrier_diffusion_range],[boost_buff_effect_type],[purge_buff_effect_type]
from [dbo].[PlayerUnitData] as A inner join [dbo].[RAW_AbilityTable] as B on A.ability_id = B.id
SET IDENTITY_INSERT [dbo].[PlayerUnitAbilityData] OFF;

SET IDENTITY_INSERT [dbo].[BulletExtraEffectData] ON;
INSERT INTO [dbo].[BulletExtraEffectData] (
[id],[name],[description],[target],[turn],[type],[subtype],[value]
)
select * from [dbo].[RAW_BulletExtraEffectTable]
SET IDENTITY_INSERT [dbo].[BulletExtraEffectData] OFF;