#ifndef SSSE_READER_H_
#define SSSE_READER_H_

#ifdef __cplusplus
extern "C" {
#endif

typedef struct tagCardUserInfo {
	char card_id[33]; // 卡识别码（内部包含应用城市代码等信息）
	char card_no[10]; // 卡号
	char card_issue_date[9]; // 发卡日期
	char user_id[19]; // 身份证号码
	char user_name[31]; // 姓名
	char user_sex[2]; // 性别
	char user_phone_number[16]; // 联系电话
} CardUserInfo;

int __stdcall icc_reader_open(const char *name);
int __stdcall icc_reader_close(int reader_handle);
int __stdcall icc_reader_get_card_user_info(int reader_handle, const char *pin, CardUserInfo *card_user_info, int *retry_times);

void __stdcall get_error_message(int error_code, char *error_string);

#ifdef __cplusplus
}
#endif

#endif // SSSE_READER_H_
