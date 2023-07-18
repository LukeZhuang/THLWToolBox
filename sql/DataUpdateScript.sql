/****** Script for SelectTopNRows command from SSMS  ******/

/* update version info */
USE [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7]
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
IF NOT EXISTS (SELECT * FROM [TouhouLostWordRawData].INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'UPDATEDATE')
BEGIN
    CREATE TABLE [TouhouLostWordRawData].[dbo].[UPDATEDATE] (
	  date nvarchar(255)
	)
END
INSERT INTO [TouhouLostWordRawData].[dbo].[UPDATEDATE] (date) VALUES ('20230417')

/* step 2: clear old data */
delete from [TouhouLostWordRawData].[dbo].[CharacteristicTable];
delete from [TouhouLostWordRawData].[dbo].[PersonRelationTable];
delete from [TouhouLostWordRawData].[dbo].[PictureTable];
delete from [TouhouLostWordRawData].[dbo].[RaceTable];
delete from [TouhouLostWordRawData].[dbo].[ShotTable];
delete from [TouhouLostWordRawData].[dbo].[SpellcardTable];
delete from [TouhouLostWordRawData].[dbo].[UnitRaceTable];
delete from [TouhouLostWordRawData].[dbo].[UnitTable];
delete from [TouhouLostWordRawData].[dbo].[BulletTable];
delete from [TouhouLostWordRawData].[dbo].[BulletCriticalRaceTable];


/* step 3: copy new data */
insert into [TouhouLostWordRawData].[dbo].[CharacteristicTable] select * from [TouhouLostWordRawData].[dbo].[CharacteristicTableNew];
insert into [TouhouLostWordRawData].[dbo].[PersonRelationTable] select * from [TouhouLostWordRawData].[dbo].[PersonRelationTableNew];
insert into [TouhouLostWordRawData].[dbo].[PictureTable] select * from [TouhouLostWordRawData].[dbo].[PictureTableNew];
insert into [TouhouLostWordRawData].[dbo].[RaceTable] select * from [TouhouLostWordRawData].[dbo].[RaceTableNew];
insert into [TouhouLostWordRawData].[dbo].[ShotTable] select * from [TouhouLostWordRawData].[dbo].[ShotTableNew];
insert into [TouhouLostWordRawData].[dbo].[SpellcardTable] select * from [TouhouLostWordRawData].[dbo].[SpellcardTableNew];
insert into [TouhouLostWordRawData].[dbo].[UnitRaceTable] select * from [TouhouLostWordRawData].[dbo].[UnitRaceTableNew];
insert into [TouhouLostWordRawData].[dbo].[UnitTable] select * from [TouhouLostWordRawData].[dbo].[UnitTableNew];
insert into [TouhouLostWordRawData].[dbo].[BulletTable] select * from [TouhouLostWordRawData].[dbo].[BulletTableNew];
insert into [TouhouLostWordRawData].[dbo].[BulletCriticalRaceTable] select * from [TouhouLostWordRawData].[dbo].[BulletCriticalRaceTableNew];


/* step 4: drop new data table */
drop table [TouhouLostWordRawData].[dbo].[CharacteristicTableNew];
drop table [TouhouLostWordRawData].[dbo].[PersonRelationTableNew];
drop table [TouhouLostWordRawData].[dbo].[PictureTableNew];
drop table [TouhouLostWordRawData].[dbo].[RaceTableNew];
drop table [TouhouLostWordRawData].[dbo].[ShotTableNew];
drop table [TouhouLostWordRawData].[dbo].[SpellcardTableNew];
drop table [TouhouLostWordRawData].[dbo].[UnitRaceTableNew];
drop table [TouhouLostWordRawData].[dbo].[UnitTableNew];
drop table [TouhouLostWordRawData].[dbo].[BulletTableNew];
drop table [TouhouLostWordRawData].[dbo].[BulletCriticalRaceTableNew];


/* step 5: prepare data for website */
delete from [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PersonRelationData];
delete from [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PictureData];
delete from [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitCharacteristicData];
delete from [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitData];
delete from [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitShotData];
delete from [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitSpellcardData];
delete from [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[RaceData];
delete from [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitRaceData];
delete from [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitBulletData];
delete from [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitBulletCriticalRaceData];

SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitData] ON;
insert into [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitData] (
[id],[name],[name_kana],[alias_name],[short_name],[person_id],[album_id],[role],[exp_id],[symbol_name],[symbol_title],[symbol_description],[life_point],[yang_attack],[yang_defense],[yin_attack],[yin_defense],[speed],[shot1_id],[shot2_id],[spellcard1_id],[spellcard2_id],[spellcard3_id],[spellcard4_id],[spellcard5_id],[skill1_id],[skill2_id],[skill3_id],[resist_id],[characteristic_id],[ability_id],[recycle_id],[default_costume_id],[drop_text],[limitbreak_item_id],[spellcard_bgm_id],[name_sub],[name_kana_sub],[short_name_sub],[is_show]
)
select * from [TouhouLostWordRawData].[dbo].[UnitTable]
where symbol_name != '(empty)' and (shot1_id != 10011 or id = 1001) and alias_name != '_TEST';
SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitData] OFF;

SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PersonRelationData] ON;
INSERT INTO [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PersonRelationData] (
[id],[person_id],[target_person_id],[description]
)
SELECT * FROM [TouhouLostWordRawData].[dbo].[PersonRelationTable];
SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PersonRelationData] OFF;

SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitShotData] ON;
INSERT INTO [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitShotData] (
[id],[name],[description],[specification],[magazine0_bullet_id],[magazine0_bullet_range],[magazine0_bullet_value],[magazine0_bullet_power_rate],[magazine1_boost_count],[magazine1_bullet_id],[magazine1_bullet_range],[magazine1_bullet_value],[magazine1_bullet_power_rate],[magazine2_boost_count],[magazine2_bullet_id],[magazine2_bullet_range],[magazine2_bullet_value],[magazine2_bullet_power_rate],[magazine3_boost_count],[magazine3_bullet_id],[magazine3_bullet_range],[magazine3_bullet_value],[magazine3_bullet_power_rate],[magazine4_boost_count],[magazine4_bullet_id],[magazine4_bullet_range],[magazine4_bullet_value],[magazine4_bullet_power_rate],[magazine5_boost_count],[magazine5_bullet_id],[magazine5_bullet_range],[magazine5_bullet_value],[magazine5_bullet_power_rate],[phantasm_power_up_rate],[shot_level0_power_rate],[shot_level1_power_rate],[shot_level2_power_rate],[shot_level3_power_rate],[shot_level4_power_rate],[shot_level5_power_rate]
)
select B.* from [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitData] as A inner join [TouhouLostWordRawData].[dbo].[ShotTable] as B on A.shot1_id = B.id or A.shot2_id = B.id
SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitShotData] OFF;

SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitSpellcardData] ON;
INSERT INTO [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitSpellcardData] (
[id],[name],[description],[specification],[type],[magazine0_bullet_id],[magazine0_bullet_range],[magazine0_bullet_value],[magazine0_bullet_power_rate],[magazine1_boost_count],[magazine1_bullet_id],[magazine1_bullet_range],[magazine1_bullet_value],[magazine1_bullet_power_rate],[magazine2_boost_count],[magazine2_bullet_id],[magazine2_bullet_range],[magazine2_bullet_value],[magazine2_bullet_power_rate],[magazine3_boost_count],[magazine3_bullet_id],[magazine3_bullet_range],[magazine3_bullet_value],[magazine3_bullet_power_rate],[magazine4_boost_count],[magazine4_bullet_id],[magazine4_bullet_range],[magazine4_bullet_value],[magazine4_bullet_power_rate],[magazine5_boost_count],[magazine5_bullet_id],[magazine5_bullet_range],[magazine5_bullet_value],[magazine5_bullet_power_rate],[phantasm_power_up_rate],[shot_level0_power_rate],[shot_level1_power_rate],[shot_level2_power_rate],[shot_level3_power_rate],[shot_level4_power_rate],[shot_level5_power_rate],[spellcard_skill1_effect_id],[spellcard_skill1_level_type],[spellcard_skill1_level_value],[spellcard_skill1_timing],[spellcard_skill2_effect_id],[spellcard_skill2_level_type],[spellcard_skill2_level_value],[spellcard_skill2_timing],[spellcard_skill3_effect_id],[spellcard_skill3_level_type],[spellcard_skill3_level_value],[spellcard_skill3_timing],[spellcard_skill4_effect_id],[spellcard_skill4_level_type],[spellcard_skill4_level_value],[spellcard_skill4_timing],[spellcard_skill5_effect_id],[spellcard_skill5_level_type],[spellcard_skill5_level_value],[spellcard_skill5_timing]
)
select B.* from [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitData] as A inner join [TouhouLostWordRawData].[dbo].[SpellcardTable] as B on A.spellcard1_id = B.id or A.spellcard2_id = B.id or A.spellcard3_id = B.id or A.spellcard4_id = B.id or A.spellcard5_id = B.id
SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitSpellcardData] OFF;

SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PictureData] ON;
INSERT INTO [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PictureData] (
[id],[name],[album_id],[type],[rare],[illustrator_name],[circle_name],[flavor_text1],[flavor_text2],[flavor_text3],[flavor_text4],[flavor_text5],[correction1_type],[correction1_value],[correction1_diff],[correction2_type],[correction2_value],[correction2_diff],[picture_characteristic1_effect_type],[picture_characteristic1_effect_subtype],[picture_characteristic1_effect_value],[picture_characteristic1_effect_value_max],[picture_characteristic1_effect_turn],[picture_characteristic1_effect_range],[picture_characteristic2_effect_type],[picture_characteristic2_effect_subtype],[picture_characteristic2_effect_value],[picture_characteristic2_effect_value_max],[picture_characteristic2_effect_turn],[picture_characteristic2_effect_range],[picture_characteristic3_effect_type],[picture_characteristic3_effect_subtype],[picture_characteristic3_effect_value],[picture_characteristic3_effect_value_max],[picture_characteristic3_effect_turn],[picture_characteristic3_effect_range],[picture_characteristic_text],[picture_characteristic_text_max],[recycle_id],[is_show]
)
select * from [TouhouLostWordRawData].[dbo].[PictureTable]
SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PictureData] OFF;

SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[RaceData] ON;
INSERT INTO [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[RaceData] (
[id],[name],[description],[alias_name]
)
select * from [TouhouLostWordRawData].[dbo].[RaceTable]
SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[RaceData] OFF;

SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitRaceData] ON;
INSERT INTO [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitRaceData] (
[id],[unit_id],[race_id]
)
select * from [TouhouLostWordRawData].[dbo].[UnitRaceTable]
SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitRaceData] OFF;

SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitBulletData] ON;
INSERT INTO [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitBulletData] (
[id],[name],[description],[element],[type],[category],[power],[hit],[critical],[bullet1_addon_id],[bullet1_addon_value],[bullet2_addon_id],[bullet2_addon_value],[bullet3_addon_id],[bullet3_addon_value],[bullet1_extraeffect_id],[bullet1_extraeffect_success_rate],[bullet2_extraeffect_id],[bullet2_extraeffect_success_rate],[bullet3_extraeffect_id],[bullet3_extraeffect_success_rate]
)
select B.* from [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitSpellcardData] as A inner join [TouhouLostWordRawData].[dbo].[BulletTable] as B on (A.magazine0_bullet_id = B.id or A.magazine1_bullet_id = B.id or A.magazine2_bullet_id = B.id or A.magazine3_bullet_id = B.id or A.magazine4_bullet_id = B.id or A.magazine5_bullet_id = B.id)
INSERT INTO [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitBulletData] (
[id],[name],[description],[element],[type],[category],[power],[hit],[critical],[bullet1_addon_id],[bullet1_addon_value],[bullet2_addon_id],[bullet2_addon_value],[bullet3_addon_id],[bullet3_addon_value],[bullet1_extraeffect_id],[bullet1_extraeffect_success_rate],[bullet2_extraeffect_id],[bullet2_extraeffect_success_rate],[bullet3_extraeffect_id],[bullet3_extraeffect_success_rate]
)
select B.* from [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitShotData] as A inner join [TouhouLostWordRawData].[dbo].[BulletTable] as B on (A.magazine0_bullet_id = B.id or A.magazine1_bullet_id = B.id or A.magazine2_bullet_id = B.id or A.magazine3_bullet_id = B.id or A.magazine4_bullet_id = B.id or A.magazine5_bullet_id = B.id)
SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitBulletData] OFF;

SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitBulletCriticalRaceData] ON;
INSERT INTO [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitBulletCriticalRaceData] (
[id],[bullet_id],[race_id]
)
select B.* from [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitBulletData] as A inner join [TouhouLostWordRawData].[dbo].[BulletCriticalRaceTable] as B on (A.id = B.bullet_id)
SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitBulletCriticalRaceData] OFF;

SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitCharacteristicData] ON;
INSERT INTO [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitCharacteristicData] (
[id],[characteristic1_name],[characteristic1_description],[characteristic1_type],[characteristic1_effect_type],[characteristic1_effect_subtype],[characteristic1_rate],[characteristic1_effect_value],[characteristic1_icon_filename],[characteristic2_name],[characteristic2_description],[characteristic2_type],[characteristic2_effect_type],[characteristic2_effect_subtype],[characteristic2_rate],[characteristic2_effect_value],[characteristic2_icon_filename],[characteristic3_name],[characteristic3_description],[characteristic3_type],[characteristic3_effect_type],[characteristic3_effect_subtype],[characteristic3_rate],[characteristic3_effect_value],[characteristic3_icon_filename],[trust_characteristic_name],[trust_characteristic_description],[trust_characteristic_rear_effect_type],[trust_characteristic_rear_effect_subtype],[trust_characteristic_avent_effect_type],[trust_characteristic_avent_effect_subtype]
)
select B.* from [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitData] as A inner join [TouhouLostWordRawData].[dbo].[CharacteristicTable] as B on A.characteristic_id = B.id
SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitCharacteristicData] OFF;