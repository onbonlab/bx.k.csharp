

/************************************************************************
 * file:	Led5kSDK.h
 * brief:	Header file of Network communication library in network part
 * author:	niu.zhimin
 * date:	2018-03-14
 ***********************************************************************/

#pragma once
#ifdef LED5KSDK_EXPORTS
#define LED5KSDK_API extern "C" __declspec(dllexport)
#else
#define LED5KSDK_API extern "C" __declspec(dllimport)
#endif

typedef enum _BX5K_ERR
{
	ERR_NO				=0, //No Error 
	ERR_OUTOFGROUP		=1, //Command Group Error 
	ERR_NOCMD			=2, //Command Not Found 
	ERR_BUSY			=3, //The Controller is busy now 
	ERR_MEMORYVOLUME	=4, //Out of the Memory Volume 
	ERR_CHECKSUM		=5, //CRC16 Checksum Error 
	ERR_FILENOTEXIST	=6, //File Not Exist 
	ERR_FLASH			=7, //Flash Access Error 
	ERR_FILE_DOWNLOAD   =8, //File Download Error 
	ERR_FILE_NAME		=9, //Filename Error 
	ERR_FILE_TYPE		=10,//File type Error 
	ERR_FILE_CRC16		=11,//File CRC16 Error 
	ERR_FONT_NOT_EXIST  =12,//Font Library Not Exist 
	ERR_FIRMWARE_TYPE   =13,//Firmware Type Error (Check the controller type) 
	ERR_DATE_TIME_FORMAT=14,//Date Time format error 
	ERR_FILE_EXIST		=15,//File Exist for File overwrite 
	ERR_FILE_BLOCK_NUM  =16,//File block number error 
	ERR_COMMUNICATE		=100,//ͨ��ʧ��
	ERR_PROTOCOL		=101,//Э�����ݲ���ȷ
	ERR_TIMEOUT			=102,//ͨ�ų�ʱ
	ERR_NETCLOSE		=103,//ͨ�ŶϿ�
	ERR_INVALID_HAND	=104,//��Ч���
	ERR_PARAMETER		=105,//��������
	ERR_SHOULDREPEAT	=106,//��Ҫ�ظ��ϴ����ݰ�
	ERR_FILE			=107,//��Ч�ļ�
}bx5k_err;

typedef enum:BYTE{
	COM_ONESTOPBIT  =        0,
	COM_ONE5STOPBITS =       1,
	COM_TWOSTOPBITS  =       2,
}serial_stopbits;

typedef enum:BYTE{
	COM_NOPARITY       =     0,
	COM_ODDPARITY      =     1,
	COM_EVENPARITY     =     2,
	COM_MARKPARITY     =     3,
	COM_SPACEPARITY    =     4,
}serial_parity;

typedef enum:BYTE{
	COM_4BITS		=	4,
	COM_5BITS		=	5,
	COM_6BITS		=	6,
	COM_7BITS		=	7,
	COM_8BITS		=	8,
}serial_databits;

typedef enum:BYTE{
	BX_Any			=	0xFE,
	BX_5K1			=	0x51,
	BX_5K2			=	0x58,
	BX_5MK2			=	0x53,
	BX_5MK1			=	0x54,
	BX_5K1Q_YY      =   0x5c,
    BX_6K1          =   0x61,
    BX_6K2          =   0x62,
    BX_6K3          =   0x63,
    BX_6K1_YY       =   0x64,
    BX_6K2_YY       =   0x65,
    BX_6K3_YY       =   0x66,
    BX_6K1_4G       =   0x67,
    BX_6K2_4G       =   0x68,
    LZ_5K1          =   0x5F,
}bx_5k_card_type;
#pragma pack (push,1)

//-------�����ʽ------
// area header | data |
//---------------------
typedef struct _BX_5K_AREA
{
	BYTE	AreaType ;//һ���ֽ�
	USHORT	AreaX;  //2���ֽ�
	USHORT	AreaY;
	USHORT	AreaWidth; 
	USHORT	AreaHeight;
	BYTE	DynamicAreaLoc;
	BYTE	Lines_sizes ;
	BYTE	RunMode; 
	USHORT	Timeout ;
	BYTE	Reserved[3];
	BYTE	SingleLine; 
	BYTE	NewLine ;
	BYTE	DisplayMode; 
	BYTE	ExitMode;
	BYTE	Speed; 
	BYTE	StayTime; 
	DWORD	DataLen;   //4���ֽ�
}bx_5k_area_header;

//-------�����ʽ------
// area header | data |
//---------------------
typedef struct _BX_5K_SOUND
{
	BYTE	StoreFlag;
	BYTE	SoundPerson;//һ���ֽ�
	BYTE	SoundVolum;
	BYTE	SoundSpeed;
	BYTE	SoundDataMode; 
	DWORD	SoundReplayTimes; 
	DWORD	SoundReplayDelay;
	BYTE	SoundReservedParaLen; 
	DWORD	SoundDataLen;
}bx_5k_sound;

#pragma pack(pop)
//��ʼ��
LED5KSDK_API void WINAPI InitSdk(BYTE minorVer, BYTE majorVer);
//�ͷ�
LED5KSDK_API void WINAPI ReleaseSdk();
//�㲥ͨѶ����
LED5KSDK_API DWORD WINAPI CreateBroadCast(char *broad_ip,UINT broad_port,bx_5k_card_type card_type,unsigned char* BarCodes,BYTE Option,int mode);
//�̶�IPͨѶ����
LED5KSDK_API DWORD WINAPI CreateClient(char *led_ip,UINT led_port,bx_5k_card_type card_type,int tmout_sec,int mode,void (CALLBACK* pClose)(DWORD dwHand,int err_code)=NULL);
//TCP modbusͨѶ����
LED5KSDK_API DWORD WINAPI CreateTcpModbus(char *led_ip,bx_5k_card_type card_type,void (CALLBACK* pClose)(DWORD dwHand,int err_code)=NULL);
//����ͨѶ����
LED5KSDK_API DWORD WINAPI CreateComClient(BYTE com,DWORD baudrate,bx_5k_card_type card_type,int mode,USHORT ScreenID);
//����modbusͨѶ����
LED5KSDK_API DWORD WINAPI CreateComModbus(BYTE com,DWORD baudrate,serial_parity Parity,serial_databits DataBits,serial_stopbits StopBits,bx_5k_card_type card_type,USHORT ScreenID);
//����ͨѶ
LED5KSDK_API void WINAPI Destroy(DWORD dwHand);
//����ͨѶ��ʱ
LED5KSDK_API void WINAPI SetTimeout(DWORD dwHand,DWORD nSec);
//������ͨѶ��ʱ
LED5KSDK_API void WINAPI SetLedAliveTick(DWORD dwHand,int time_sec);
//gprsͨѶ��ʱ
LED5KSDK_API void WINAPI SetGprsAliveTick(DWORD dwHand,int time_sec);
//�򿪷�����
LED5KSDK_API BOOL WINAPI StartServer(UINT port,void (CALLBACK* pConnectFunc)(DWORD dwHand,unsigned char* pid),void (CALLBACK* pCloseFunc)(DWORD dwHand,unsigned char* pid,int err_code));
//�رշ�����
LED5KSDK_API void WINAPI CloseServer();
//��������ѯ
LED5KSDK_API int WINAPI ServerGetCardList(unsigned char* pList);
//����gprs������
LED5KSDK_API BOOL WINAPI StartGprsServer(UINT port,void (CALLBACK* pConnectFunc)(DWORD dwHand,unsigned char* pid),void (CALLBACK* pCloseFunc)(DWORD dwHand,unsigned char* pid,int err_code));
//�ر�gprs������                 
LED5KSDK_API void WINAPI CloseGprsServer();
//gprs��ѯ
LED5KSDK_API int WINAPI GprsServerGetCardList(unsigned char* pList);

#define COMMAND_API LED5KSDK_API int WINAPI
//ping
COMMAND_API CON_PING(DWORD dwHand);
//��λ
COMMAND_API CON_Reset(DWORD dwHand);
//��ѯ������״̬
COMMAND_API CON_ControllerStatus(DWORD dwHand,BYTE* pStatus,USHORT* len);
//��ѯ�ֿ���Ϣ
COMMAND_API CON_CheckCurrentFont(DWORD dwHand,BYTE* fontStatus,USHORT* len);
//�ض��ͻ���Ϣ
COMMAND_API CON_CheckCurrentCustomer(DWORD dwHand,BYTE* CustomerStatus,USHORT* len);
//�����ض�
COMMAND_API CON_ReadScreen(DWORD dwHand,BYTE* ScreenStatus,USHORT* len);
//Уʱ
COMMAND_API CON_SytemClockCorrect(DWORD dwHand);
//��ѯ�̼�
COMMAND_API CON_CheckCurrentFirmware(DWORD dwHand,char* FirmwareName,char* FirmwareVersion,char* FirmwareDateTime);
//����̼�
COMMAND_API CON_FirmwareActivate(DWORD dwHand,char* FirmwareName);
//��������
COMMAND_API CON_SetScreenID(DWORD dwHand,USHORT newScreenID);
//��ȡ����
COMMAND_API CON_ReadScreenID(DWORD dwHand,USHORT* pScreenID);

//����
COMMAND_API SCREEN_ForceOnOff(DWORD dwHand,BYTE OnOffFlag);
//���ö�ʱ����
COMMAND_API SCREEN_TimeTurnOnOff(DWORD dwHand,BYTE* pTimer,int nGroup);	
//��������
COMMAND_API SCREEN_SetBrightness(DWORD dwHand,BYTE BrightnessType,BYTE CurrentBrightness,BYTE* BrightnessValue);	
//�����ϵ�ȴ�ʱ��
COMMAND_API SCREEN_SetWaitTime(DWORD dwHand,BYTE WaitTime);		
//������Ŀ
COMMAND_API SCREEN_LockProgram(DWORD dwHand,BYTE LockFlag,BYTE StoreMode,char* ProgramFileName);	
//ɾ����̬��
COMMAND_API SCREEN_DelDynamicArea(DWORD dwHand,BYTE DeleteAreaId);  
//���¶�̬��
COMMAND_API SCREEN_SendDynamicArea(DWORD dwHand,bx_5k_area_header header,USHORT TextLen,BYTE* AreaText);
//����������̬��
COMMAND_API SCREEN_SendSoundDynamicArea(DWORD dwHand,bx_5k_area_header header,USHORT TextLen,BYTE* AreaText,BYTE SoundMode,BYTE SoundPerson,BYTE SoundVolume,BYTE SoundSpeed,int sound_len,BYTE* sounddata);
COMMAND_API SCREEN_Test(DWORD dwHand,BYTE TestTime);	
//ȡ����ʱ
COMMAND_API SCREEN_CancelTimeOnOff(DWORD dwHand);	

//  ��������Ӧ��۽̬��
COMMAND_API BX5MK_SetSpecialAppDynamic(DWORD dwHand,USHORT AreaX,USHORT AreaY,USHORT AreaW,USHORT AreaH,BYTE DataType,BYTE Pagetotal,BYTE RunState,USHORT Timeout,BYTE SingleLine,BYTE Lines_sizes,BYTE NewLine,USHORT StayTime); 
//  ���ͷ�ҳ����
COMMAND_API BX5MK_SendPageData(DWORD dwHand,BYTE PageNum,USHORT PageDataLen,BYTE* PageData);
//  ���͵�����Ϣ
COMMAND_API BX5MK_SendLatticeMessage(DWORD dwHand,BYTE BlockFlag,USHORT BlockAddr,BYTE* BlockData,USHORT BlockDataLen);
//  ɾ������Ӧ��۽̬��
COMMAND_API BX5MK_DelSpecialAppDynamic(DWORD dwHand);
// ���� IP ��ַ
COMMAND_API BX5MK_SetIPAddress(DWORD dwHand,BYTE ConnnectMode,char* ip,char* SubnetMask,char* Gateway,USHORT port,BYTE ServerMode,char* ServerIPAddress,USHORT ServerPort,char* ServerAccessPassword,USHORT HeartBeatInterval,char* NetID);
//  ���� MAC ��ַ
COMMAND_API BX5MK_SetMACAddress(DWORD dwHand,BYTE* MAC);
// �������� 
COMMAND_API BX5MK_WebSearch(DWORD dwHand,BYTE* recv_buff,USHORT* recv_len);
//  ɾ��ҳ����
COMMAND_API BX5MK_DelPageData(DWORD dwHand,BYTE PageLog);

//��ʽ��
COMMAND_API OFS_Formatting(DWORD dwHand);
//ɾ����Ŀ
COMMAND_API OFS_DeleteFile(DWORD dwHand,USHORT FileNumber,char* pFileNameList);	
//д���ļ�
COMMAND_API OFS_BeginSendMultiFiles(DWORD dwHand);	
//д�ļ�
COMMAND_API OFS_SendFile(DWORD dwHand,BYTE overwrite,char* pFilePath);
//���ͽ�Ŀ
COMMAND_API OFS_SendFileData(DWORD dwHand,BYTE overwrite,char* pFileName,USHORT DisplayType,BYTE PlayTimes,BYTE* ProgramLife,BYTE ProgramWeek,BYTE ProgramTime,BYTE* Period,BYTE AreaNum, BYTE* AreaDataList,int AreaDataListLen);
//����д���ļ�
COMMAND_API OFS_EndSendMultiFiles(DWORD dwHand);
//�ֿ�
COMMAND_API OFS_SendFontData(DWORD dwHand,BYTE overwrite,char* pFileName,BYTE FontWidth,BYTE FontHeight,BYTE* LibData,int LibData_len, BYTE FontEncode,void (CALLBACK* pCloseFunc)(int total,int sendlen));
//���ÿͻ���Ϣ
COMMAND_API OFS_SetFontInformation(DWORD dwHand,BYTE OverWrite,char* ClientMsg);
//��������
COMMAND_API OFS_SendScreenData(DWORD dwHand,BYTE overwrite,char* pFileName,USHORT Address,BYTE Baudrate,USHORT ScreenWith,USHORT ScreenHeight,
	BYTE Color,BYTE	MirrorMode,BYTE	OE,BYTE	DA,BYTE	RowOrder,BYTE FreqPar,BYTE OEAngle,BYTE CommTimeout,BYTE TipLanguage,BYTE LatticeMode);
//�̼�
COMMAND_API OFS_SendFirmWareData(DWORD dwHand,BYTE overwrite,char* pFileName, BYTE* FirmWareData,int FirmWareDataLen,void (CALLBACK* pCloseFunc)(int total,int sendlen));
//
COMMAND_API OFS_SendScanData(DWORD dwHand,BYTE overwrite,char* pFileName, BYTE* ScanData,int ScanDataLen);
//����
COMMAND_API SCREEN_SendSound(DWORD dwHand,bx_5k_sound sound,USHORT TextLen,BYTE* AreaText);
//���ͱ��
COMMAND_API OFS_SendTable(DWORD dwHand,USHORT OriginX,USHORT OriginY,USHORT TableWidth,USHORT TableHeight,BYTE RowNum,BYTE LineNum,BYTE CellNum, BYTE* TableDataList,int TableDataListLen);


COMMAND_API SendAndRecvBuff(DWORD dwHand, BYTE cmd_group, BYTE cmd, BYTE* cmd_data, USHORT data_len, BYTE* recv_data, USHORT* p_recv_len);
COMMAND_API SendBuff(DWORD dwHand, BYTE cmd_group, BYTE cmd, BYTE* cmd_data, USHORT data_len);
//���Ͷද̬��
COMMAND_API SCREEN_SendDynamicAreas(DWORD dwHand,BYTE AreaNum,USHORT TextLen,BYTE* AreaText);
//�ⲿIO��ƽ
COMMAND_API CON_IOlevel(DWORD dwHand,BYTE Exp1,BYTE Exp2,BYTE LightDaPin);




