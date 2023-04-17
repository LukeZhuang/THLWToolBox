/****** Script for SelectTopNRows command from SSMS  ******/

--SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitData] ON;
--INSERT INTO [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitData] (
--[id],[name],[name_kana],[alias_name],[short_name],[person_id],[album_id],[role],[exp_id],[symbol_name],[symbol_title],[symbol_description],[life_point],[yang_attack],[yang_defense],[yin_attack],[yin_defense],[speed],[shot1_id],[shot2_id],[spellcard1_id],[spellcard2_id],[spellcard3_id],[spellcard4_id],[spellcard5_id],[skill1_id],[skill2_id],[skill3_id],[resist_id],[characteristic_id],[ability_id],[recycle_id],[default_costume_id],[drop_text],[limitbreak_item_id],[spellcard_bgm_id],[is_show]
--)
--SELECT * FROM [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[UnitTableRaw]
--where symbol_name != '(empty)' and (shot1_id != 10011 or id = 1001) and alias_name != '_TEST';
--SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitData] OFF;


--SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PersonRelationData] ON;
--INSERT INTO [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PersonRelationData] (
--[id],[person_id],[target_person_id],[description]
--)
--SELECT * FROM [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PersonRelationTableRaw];
--SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PersonRelationData] OFF;


--SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitShotData] ON;
--INSERT INTO [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitShotData] (
--[id],[name],[description],[specification],[magazine0_bullet_id],[magazine0_bullet_range],[magazine0_bullet_value],[magazine0_bullet_power_rate],[magazine1_boost_count],[magazine1_bullet_id],[magazine1_bullet_range],[magazine1_bullet_value],[magazine1_bullet_power_rate],[magazine2_boost_count],[magazine2_bullet_id],[magazine2_bullet_range],[magazine2_bullet_value],[magazine2_bullet_power_rate],[magazine3_boost_count],[magazine3_bullet_id],[magazine3_bullet_range],[magazine3_bullet_value],[magazine3_bullet_power_rate],[magazine4_boost_count],[magazine4_bullet_id],[magazine4_bullet_range],[magazine4_bullet_value],[magazine4_bullet_power_rate],[magazine5_boost_count],[magazine5_bullet_id],[magazine5_bullet_range],[magazine5_bullet_value],[magazine5_bullet_power_rate],[phantasm_power_up_rate],[shot_level0_power_rate],[shot_level1_power_rate],[shot_level2_power_rate],[shot_level3_power_rate],[shot_level4_power_rate],[shot_level5_power_rate]
--)
--select B.* from [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitData] as A inner join [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[ShotTableRaw] as B on A.shot1_id = B.id or A.shot2_id = B.id
--SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitShotData] OFF;


SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitSpellcardData] ON;
INSERT INTO [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitSpellcardData] (
[id],[name],[description],[specification],[type],[magazine0_bullet_id],[magazine0_bullet_range],[magazine0_bullet_value],[magazine0_bullet_power_rate],[magazine1_boost_count],[magazine1_bullet_id],[magazine1_bullet_range],[magazine1_bullet_value],[magazine1_bullet_power_rate],[magazine2_boost_count],[magazine2_bullet_id],[magazine2_bullet_range],[magazine2_bullet_value],[magazine2_bullet_power_rate],[magazine3_boost_count],[magazine3_bullet_id],[magazine3_bullet_range],[magazine3_bullet_value],[magazine3_bullet_power_rate],[magazine4_boost_count],[magazine4_bullet_id],[magazine4_bullet_range],[magazine4_bullet_value],[magazine4_bullet_power_rate],[magazine5_boost_count],[magazine5_bullet_id],[magazine5_bullet_range],[magazine5_bullet_value],[magazine5_bullet_power_rate],[phantasm_power_up_rate],[shot_level0_power_rate],[shot_level1_power_rate],[shot_level2_power_rate],[shot_level3_power_rate],[shot_level4_power_rate],[shot_level5_power_rate],[spellcard_skill1_effect_id],[spellcard_skill1_level_type],[spellcard_skill1_level_value],[spellcard_skill1_timing],[spellcard_skill2_effect_id],[spellcard_skill2_level_type],[spellcard_skill2_level_value],[spellcard_skill2_timing],[spellcard_skill3_effect_id],[spellcard_skill3_level_type],[spellcard_skill3_level_value],[spellcard_skill3_timing],[spellcard_skill4_effect_id],[spellcard_skill4_level_type],[spellcard_skill4_level_value],[spellcard_skill4_timing],[spellcard_skill5_effect_id],[spellcard_skill5_level_type],[spellcard_skill5_level_value],[spellcard_skill5_timing]
)
select B.* from [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitData] as A inner join [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[SpellcardTableRaw] as B on A.spellcard1_id = B.id or A.spellcard2_id = B.id or A.spellcard3_id = B.id or A.spellcard4_id = B.id or A.spellcard5_id = B.id
SET IDENTITY_INSERT [THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitSpellcardData] OFF;