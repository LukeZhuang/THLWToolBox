/****** Script for SelectTopNRows command from SSMS  ******/
SELECT * FROM UnitTableRaw where symbol_name != '(empty)' and (shot1_id != 10011 or id = 1001) and alias_name != '_TEST'