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
  is '����';
comment on column FIN_COM_UNDRUGINFO.OTHER_SPELL
  is '����ƴ����';
comment on column FIN_COM_UNDRUGINFO.OTHER_WB
  is '���������';
comment on column FIN_COM_UNDRUGINFO.OTHER_CUSTOM
  is '�����Զ�����';
comment on column FIN_COM_UNDRUGINFO.ENGLISH_NAME
  is 'Ӣ����';
comment on column FIN_COM_UNDRUGINFO.ENGLISH_OTHER
  is 'Ӣ�ı���';
comment on column FIN_COM_UNDRUGINFO.ENGLISH_REGULAR
  is 'Ӣ��ͨ����';
comment on column FIN_COM_UNDRUGINFO.STOP_RESEON
  is 'ͣ��ԭ��';
