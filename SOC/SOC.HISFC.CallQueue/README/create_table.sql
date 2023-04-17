-- Create table
create table MET_NUO_CALLQUEUE
(
  SEQ_NO          VARCHAR2(20) not null,
  PATIENT_ID      VARCHAR2(10),
  SEE_NO          VARCHAR2(10),
  CARD_NO         VARCHAR2(10),
  PATIENT_NAME    VARCHAR2(20),
  PATIENT_SEX     VARCHAR2(1),
  DEPT_CODE       VARCHAR2(4),
  DEPT_NAME       VARCHAR2(50),
  NURSE_CELL_CODE VARCHAR2(6),
  NURSE_CELL_NAME VARCHAR2(20),
  ROOM_ID         VARCHAR2(6),
  ROOM_NAME       VARCHAR2(20),
  CONSOLE_ID      VARCHAR2(6),
  CONSOLE_NAME    VARCHAR2(20),
  NOON_ID         VARCHAR2(6),
  CALL_TYPE       VARCHAR2(20),
  SPEAK_NAME      VARCHAR2(50),
  MEMO            VARCHAR2(50),
  OPER_CODE       VARCHAR2(6),
  OPER_DATE       DATE
)
tablespace HISDATA
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    minextents 1
    maxextents unlimited
  );
-- Add comments to the table 
comment on table MET_NUO_CALLQUEUE
  is '分诊叫号排队表';
-- Add comments to the columns 
comment on column MET_NUO_CALLQUEUE.SEQ_NO
  is '叫号序号';
comment on column MET_NUO_CALLQUEUE.PATIENT_ID
  is '患者流水号';
comment on column MET_NUO_CALLQUEUE.SEE_NO
  is '看诊序号';
comment on column MET_NUO_CALLQUEUE.CARD_NO
  is '患者就诊号';
comment on column MET_NUO_CALLQUEUE.PATIENT_NAME
  is '患者姓名';
comment on column MET_NUO_CALLQUEUE.PATIENT_SEX
  is '患者性别';
comment on column MET_NUO_CALLQUEUE.DEPT_CODE
  is '看诊科室';
comment on column MET_NUO_CALLQUEUE.DEPT_NAME
  is '看诊科室名称';
comment on column MET_NUO_CALLQUEUE.NURSE_CELL_CODE
  is '护士站编码';
comment on column MET_NUO_CALLQUEUE.NURSE_CELL_NAME
  is '护士站名称';
comment on column MET_NUO_CALLQUEUE.ROOM_ID
  is '诊室';
comment on column MET_NUO_CALLQUEUE.ROOM_NAME
  is '诊室名称';
comment on column MET_NUO_CALLQUEUE.CONSOLE_ID
  is '诊台';
comment on column MET_NUO_CALLQUEUE.CONSOLE_NAME
  is '诊台名称';
comment on column MET_NUO_CALLQUEUE.NOON_ID
  is '午别编号';
comment on column MET_NUO_CALLQUEUE.CALL_TYPE
  is '叫号类型（扩展）';
comment on column MET_NUO_CALLQUEUE.SPEAK_NAME
  is '呼叫信息';
comment on column MET_NUO_CALLQUEUE.MEMO
  is '备注';
comment on column MET_NUO_CALLQUEUE.OPER_CODE
  is '操作员';
comment on column MET_NUO_CALLQUEUE.OPER_DATE
  is '操作时间';
-- Create/Recreate primary, unique and foreign key constraints 
alter table MET_NUO_CALLQUEUE
  add constraint PK_MET_NUO_CALLQUEUE primary key (SEQ_NO)
  using index 
  tablespace HISDATA
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    minextents 1
    maxextents unlimited
  );
-- Create/Recreate indexes 
create index IDX_MET_NUO_CALLQUEUE_1 on MET_NUO_CALLQUEUE (NURSE_CELL_CODE, NOON_ID)
  tablespace HISDATA
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    minextents 1
    maxextents unlimited
  );

-- Create sequence 
create sequence SEQ_MET_NUO_CALLQUEUE
minvalue 1
maxvalue 9999999999
start with 1
increment by 1
cache 10;
