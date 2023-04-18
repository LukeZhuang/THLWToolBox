/****** Script for SelectTopNRows command from SSMS  ******/

/* step 0: import data manually as RAW_NEW_xxx */
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

/* step 2: clear old data */
delete from [dbo].[RAW_CharacteristicTable];
delete from [dbo].[RAW_PersonRelationTable];
delete from [dbo].[RAW_PictureTable];
delete from [dbo].[RAW_RaceTable];
delete from [dbo].[RAW_ShotTable];
delete from [dbo].[RAW_SpellcardTable];
delete from [dbo].[RAW_UnitRaceTable];
delete from [dbo].[RAW_UnitTable];


/* step 3: copy new data */
insert into [dbo].[RAW_CharacteristicTable] select * from [dbo].[RAW_NEW_CharacteristicTable];
insert into [dbo].[RAW_PersonRelationTable] select * from [dbo].[RAW_NEW_PersonRelationTable];
insert into [dbo].[RAW_PictureTable] select * from [dbo].[RAW_NEW_PictureTable];
insert into [dbo].[RAW_RaceTable] select * from [dbo].[RAW_NEW_RaceTable];
insert into [dbo].[RAW_ShotTable] select * from [dbo].[RAW_NEW_ShotTable];
insert into [dbo].[RAW_SpellcardTable] select * from [dbo].[RAW_NEW_SpellcardTable];
insert into [dbo].[RAW_UnitRaceTable] select * from [dbo].[RAW_NEW_UnitRaceTable];
insert into [dbo].[RAW_UnitTable] select * from [dbo].[RAW_NEW_UnitTable];


/* step 4: drop new data table */
drop table [dbo].[RAW_NEW_CharacteristicTable];
drop table [dbo].[RAW_NEW_PersonRelationTable];
drop table [dbo].[RAW_NEW_PictureTable];
drop table [dbo].[RAW_NEW_RaceTable];
drop table [dbo].[RAW_NEW_ShotTable];
drop table [dbo].[RAW_NEW_SpellcardTable];
drop table [dbo].[RAW_NEW_UnitRaceTable];
drop table [dbo].[RAW_NEW_UnitTable];


/* step 5: prepare data for website */
delete from [dbo].[PersonRelationData];
delete from [dbo].[PictureData];
delete from [dbo].[PlayerUnitCharacteristicData];
delete from [dbo].[PlayerUnitData];
delete from [dbo].[PlayerUnitShotData];
delete from [dbo].[PlayerUnitSpellcardData];

SET IDENTITY_INSERT [dbo].[PlayerUnitData] ON;
insert into [dbo].[PlayerUnitData] (
[id],[name],[name_kana],[alias_name],[short_name],[person_id],[album_id],[role],[exp_id],[symbol_name],[symbol_title],[symbol_description],[life_point],[yang_attack],[yang_defense],[yin_attack],[yin_defense],[speed],[shot1_id],[shot2_id],[spellcard1_id],[spellcard2_id],[spellcard3_id],[spellcard4_id],[spellcard5_id],[skill1_id],[skill2_id],[skill3_id],[resist_id],[characteristic_id],[ability_id],[recycle_id],[default_costume_id],[drop_text],[limitbreak_item_id],[spellcard_bgm_id],[is_show]
)
select * from [dbo].[RAW_UnitTable]
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
[id],[name],[description],[specification],[magazine0_bullet_id],[magazine0_bullet_range],[magazine0_bullet_value],[magazine0_bullet_power_rate],[magazine1_boost_count],[magazine1_bullet_id],[magazine1_bullet_range],[magazine1_bullet_value],[magazine1_bullet_power_rate],[magazine2_boost_count],[magazine2_bullet_id],[magazine2_bullet_range],[magazine2_bullet_value],[magazine2_bullet_power_rate],[magazine3_boost_count],[magazine3_bullet_id],[magazine3_bullet_range],[magazine3_bullet_value],[magazine3_bullet_power_rate],[magazine4_boost_count],[magazine4_bullet_id],[magazine4_bullet_range],[magazine4_bullet_value],[magazine4_bullet_power_rate],[magazine5_boost_count],[magazine5_bullet_id],[magazine5_bullet_range],[magazine5_bullet_value],[magazine5_bullet_power_rate],[phantasm_power_up_rate],[shot_level0_power_rate],[shot_level1_power_rate],[shot_level2_power_rate],[shot_level3_power_rate],[shot_level4_power_rate],[shot_level5_power_rate]
)
select B.* from [dbo].[PlayerUnitData] as A inner join [dbo].[RAW_ShotTable] as B on A.shot1_id = B.id or A.shot2_id = B.id
SET IDENTITY_INSERT [dbo].[PlayerUnitShotData] OFF;

SET IDENTITY_INSERT [dbo].[PlayerUnitSpellcardData] ON;
INSERT INTO [dbo].[PlayerUnitSpellcardData] (
[id],[name],[description],[specification],[type],[magazine0_bullet_id],[magazine0_bullet_range],[magazine0_bullet_value],[magazine0_bullet_power_rate],[magazine1_boost_count],[magazine1_bullet_id],[magazine1_bullet_range],[magazine1_bullet_value],[magazine1_bullet_power_rate],[magazine2_boost_count],[magazine2_bullet_id],[magazine2_bullet_range],[magazine2_bullet_value],[magazine2_bullet_power_rate],[magazine3_boost_count],[magazine3_bullet_id],[magazine3_bullet_range],[magazine3_bullet_value],[magazine3_bullet_power_rate],[magazine4_boost_count],[magazine4_bullet_id],[magazine4_bullet_range],[magazine4_bullet_value],[magazine4_bullet_power_rate],[magazine5_boost_count],[magazine5_bullet_id],[magazine5_bullet_range],[magazine5_bullet_value],[magazine5_bullet_power_rate],[phantasm_power_up_rate],[shot_level0_power_rate],[shot_level1_power_rate],[shot_level2_power_rate],[shot_level3_power_rate],[shot_level4_power_rate],[shot_level5_power_rate],[spellcard_skill1_effect_id],[spellcard_skill1_level_type],[spellcard_skill1_level_value],[spellcard_skill1_timing],[spellcard_skill2_effect_id],[spellcard_skill2_level_type],[spellcard_skill2_level_value],[spellcard_skill2_timing],[spellcard_skill3_effect_id],[spellcard_skill3_level_type],[spellcard_skill3_level_value],[spellcard_skill3_timing],[spellcard_skill4_effect_id],[spellcard_skill4_level_type],[spellcard_skill4_level_value],[spellcard_skill4_timing],[spellcard_skill5_effect_id],[spellcard_skill5_level_type],[spellcard_skill5_level_value],[spellcard_skill5_timing]
)
select B.* from [dbo].[PlayerUnitData] as A inner join [dbo].[RAW_SpellcardTable] as B on A.spellcard1_id = B.id or A.spellcard2_id = B.id or A.spellcard3_id = B.id or A.spellcard4_id = B.id or A.spellcard5_id = B.id
SET IDENTITY_INSERT [dbo].[PlayerUnitSpellcardData] OFF;

SET IDENTITY_INSERT [dbo].[PictureData] ON;
INSERT INTO [dbo].[PictureData] (
[id],[name],[album_id],[type],[rare],[illustrator_name],[circle_name],[flavor_text1],[flavor_text2],[flavor_text3],[flavor_text4],[flavor_text5],[correction1_type],[correction1_value],[correction1_diff],[correction2_type],[correction2_value],[correction2_diff],[picture_characteristic1_effect_type],[picture_characteristic1_effect_subtype],[picture_characteristic1_effect_value],[picture_characteristic1_effect_value_max],[picture_characteristic1_effect_turn],[picture_characteristic1_effect_range],[picture_characteristic2_effect_type],[picture_characteristic2_effect_subtype],[picture_characteristic2_effect_value],[picture_characteristic2_effect_value_max],[picture_characteristic2_effect_turn],[picture_characteristic2_effect_range],[picture_characteristic3_effect_type],[picture_characteristic3_effect_subtype],[picture_characteristic3_effect_value],[picture_characteristic3_effect_value_max],[picture_characteristic3_effect_turn],[picture_characteristic3_effect_range],[picture_characteristic_text],[picture_characteristic_text_max],[recycle_id],[is_show]
)
select * from [dbo].[RAW_PictureTable]
SET IDENTITY_INSERT [dbo].[PictureData] OFF;

SET IDENTITY_INSERT [dbo].[RaceData] ON;
INSERT INTO [dbo].[RaceData] (
[id],[name],[description],[alias_name]
)
select * from [dbo].[RAW_RaceTable]
SET IDENTITY_INSERT [dbo].[RaceData] OFF;

SET IDENTITY_INSERT [dbo].[PlayerUnitRaceData] ON;
INSERT INTO [dbo].[PlayerUnitRaceData] (
[id],[unit_id],[race_id]
)
select * from [dbo].[RAW_UnitRaceTable]
SET IDENTITY_INSERT [dbo].[PlayerUnitRaceData] OFF;
