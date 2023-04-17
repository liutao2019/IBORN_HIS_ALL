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
'�������к�����',
'����к�',
'',
'���ﻤʿ',
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
'ɾ���к�������Ϣ',
'����к�',
'',
'���ﻤʿ',
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
'���ݻ�ʿվ�������Ҷ�Ӧ�к�������Ϣ',
'����к�',
'',
'���ﻤʿ',
'',
'',
'1',
'',
'000000',
sysdate);