/* Query which units' HitCheckOrder are missing */
SELECT A.id, A.name, A.symbol_name FROM
[THLWToolBoxContext-088877b4-245d-43ef-b004-d1d23cc730e7].[dbo].[PlayerUnitData] as A
LEFT JOIN
[TouhouLostWordRawData].[dbo].[HitCheckOrderTable] as B
ON A.id=B.unit_id
WHERE B.id IS NULL