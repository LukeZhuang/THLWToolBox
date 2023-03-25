/****** Script for SelectTopNRows command from SSMS  ******/
select B.[name],B.[symbol_name], C.[name],C.[symbol_name]
from [PersonRelationTable] as A
inner join [UnitTable] as B
on A.[person_id]=B.[person_id]
inner join [UnitTable] as C
on A.[target_person_id]=C.[person_id]
where B.[alias_name] != '_TEST' and C.[alias_name] != '_TEST'
order by B.[id], C.[id]

--SELECT A.name, A.symbol_name, A.role as role, B.trust_characteristic_name,B.trust_characteristic_description
--  FROM [UnitTable] as A  inner join  [CharacteristicTableRaw] as B
--  on A.characteristic_id=B.id
--  where A.name_kana != '(empty)' and A.alias_name != '_TEST' and A.role=7
  --B.trust_characteristic_name = N'灵力连携'


--select C.role, count(C.role) as cnt FROM
--(SELECT A.name, A.symbol_name, A.role as role, B.trust_characteristic_name
--  FROM [UnitTable] as A  inner join  [CharacteristicTableRaw] as B
--  on A.characteristic_id=B.id
--  where A.name_kana != '(empty)' and A.alias_name != '_TEST' and B.trust_characteristic_name = N'灵力连携') C
--  group by C.role order by cnt DESC