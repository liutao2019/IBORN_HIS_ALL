--------------------Nurse.CallQueue.Insert--------------------
insert into com_sql
values(
'Nurse.CallQueue.Insert',
'
INSERT INTO MET_NUO_CALLQUEUE
	(
	SEQ_NO,
	PATIENT_ID,
	SEE_NO,
	CARD_NO,
	PATIENT_NAME,
	PATIENT_SEX,
	DEPT_CODE,
	DEPT_NAME,
	NURSE_CELL_CODE,
	NURSE_CELL_NAME,
	ROOM_ID,
	ROOM_NAME,
	CONSOLE_ID,
	CONSOLE_NAME,
	NOON_ID,
	CALL_TYPE,
	SPEAK_NAME,
	MEMO,
	OPER_CODE,
	OPER_DATE
	)
VALUES 
	(
SEQ_MET_NUO_CALLQUEUE.NEXTVAL,
	''{1}'',
	''{2}'',
	''{3}'',
	''{4}'',
	''{5}'',
	''{6}'',
	''{7}'',
	''{8}'',
	''{9}'',
	''{10}'',
	''{11}'',
	''{12}'',
	''{13}'',
	''{14}'',
	''{15}'',
	''{16}'',
	''{17}'',
	''{18}'',
	sysdate
	)
  
',
'插入分诊叫号申请',
'分诊叫号',
'',
'门诊护士',
'',
'',
'1',
'',
'000000',
sysdate);

--------------------Nurse.CallQueue.Delete--------------------

insert into com_sql
values(
'Nurse.CallQueue.Delete',
'
DELETE FROM MET_NUO_CALLQUEUE t 
WHERE t.SEQ_NO=''{0}''
',
'删除叫号申请信息',
'分诊叫号',
'',
'门诊护士',
'',
'',
'1',
'',
'000000',
sysdate);

--------------------Nurse.CallQueue.Query.ByNurseCodeAndNoon--------------------
insert into com_sql
values(
'Nurse.CallQueue.Query.ByNurseCodeAndNoon',
'
SELECT 
	SEQ_NO,
	PATIENT_ID,
	SEE_NO,
	CARD_NO,
	PATIENT_NAME,
	PATIENT_SEX,
	DEPT_CODE,
	DEPT_NAME,
	NURSE_CELL_CODE,
	NURSE_CELL_NAME,
	ROOM_ID,
	ROOM_NAME,
	CONSOLE_ID,
	CONSOLE_NAME,
	NOON_ID,
	CALL_TYPE,
	SPEAK_NAME,
	MEMO,
	OPER_CODE,
	OPER_DATE
FROM MET_NUO_CALLQUEUE
WHERE NURSE_CELL_CODE=''{0}''
AND NOON_ID=''{1}''


',
'根据护士站和午别查找对应叫号申请信息',
'分诊叫号',
'',
'门诊护士',
'',
'',
'1',
'',
'000000',
sysdate);