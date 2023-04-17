-- Add/modify columns 
alter table FIN_COM_UNDRUGINFO add OTHER_NAME VARCHAR2(50);
alter table FIN_COM_UNDRUGINFO add OTHER_SPELL VARCHAR2(16);
alter table FIN_COM_UNDRUGINFO add OTHER_WB VARCHAR2(16);
alter table FIN_COM_UNDRUGINFO add OTHER_CUSTOM VARCHAR2(16);
alter table FIN_COM_UNDRUGINFO add ENGLISH_NAME VARCHAR2(32);
alter table FIN_COM_UNDRUGINFO add ENGLISH_OTHER VARCHAR2(32);
alter table FIN_COM_UNDRUGINFO add ENGLISH_REGULAR VARCHAR2(32);
alter table FIN_COM_UNDRUGINFO add STOP_RESEON VARCHAR2(50);
-- Add/modify columns 
alter table FIN_COM_UNDRUGINFO modify GB_CODE VARCHAR2(16);


-- Add comments to the columns 
comment on column FIN_COM_UNDRUGINFO.OTHER_NAME
  is '别名';
comment on column FIN_COM_UNDRUGINFO.OTHER_SPELL
  is '别名拼音码';
comment on column FIN_COM_UNDRUGINFO.OTHER_WB
  is '别名五笔码';
comment on column FIN_COM_UNDRUGINFO.OTHER_CUSTOM
  is '别名自定义码';
comment on column FIN_COM_UNDRUGINFO.ENGLISH_NAME
  is '英文名';
comment on column FIN_COM_UNDRUGINFO.ENGLISH_OTHER
  is '英文别名';
comment on column FIN_COM_UNDRUGINFO.ENGLISH_REGULAR
  is '英文通用名';
comment on column FIN_COM_UNDRUGINFO.STOP_RESEON
  is '停用原因';
