

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
	ERR_COMMUNICATE		=100,//通信失败
	ERR_PROTOCOL		=101,//协议数据不正确
	ERR_TIMEOUT			=102,//通信超时
	ERR_NETCLOSE		=103,//通信断开
	ERR_INVALID_HAND	=104,//无效句柄
	ERR_PARAMETER		=105,//参数错误
	ERR_SHOULDREPEAT	=106,//需要重复上次数据包
	ERR_FILE			=107,//无效文件
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

//-------区域格式------
// area header | data |
//---------------------
typedef struct _BX_5K_AREA
{
	BYTE	AreaType ;//一个字节
	USHORT	AreaX;  //2个字节
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
	DWORD	DataLen;   //4个字节
}bx_5k_area_header;

//-------区域格式------
// area header | data |
//---------------------
typedef struct _BX_5K_SOUND
{
	BYTE	StoreFlag;
	BYTE	SoundPerson;//一个字节
	BYTE	SoundVolum;
	BYTE	SoundSpeed;
	BYTE	SoundDataMode; 
	DWORD	SoundReplayTimes; 
	DWORD	SoundReplayDelay;
	BYTE	SoundReservedParaLen; 
	DWORD	SoundDataLen;
}bx_5k_sound;

#pragma pack(pop)
//初始化
LED5KSDK_API void WINAPI InitSdk(BYTE minorVer, BYTE majorVer);
//释放
LED5KSDK_API void WINAPI ReleaseSdk();
//广播通讯连接
LED5KSDK_API DWORD WINAPI CreateBroadCast(char *broad_ip,UINT broad_port,bx_5k_card_type card_type,unsigned char* BarCodes,BYTE Option,int mode);
//固定IP通讯连接
LED5KSDK_API DWORD WINAPI CreateClient(char *led_ip,UINT led_port,bx_5k_card_type card_type,int tmout_sec,int mode,void (CALLBACK* pClose)(DWORD dwHand,int err_code)=NULL);
//TCP modbus通讯连接
LED5KSDK_API DWORD WINAPI CreateTcpModbus(char *led_ip,bx_5k_card_type card_type,void (CALLBACK* pClose)(DWORD dwHand,int err_code)=NULL);
//串口通讯连接
LED5KSDK_API DWORD WINAPI CreateComClient(BYTE com,DWORD baudrate,bx_5k_card_type card_type,int mode,USHORT ScreenID);
//串口modbus通讯连接
LED5KSDK_API DWORD WINAPI CreateComModbus(BYTE com,DWORD baudrate,serial_parity Parity,serial_databits DataBits,serial_stopbits StopBits,bx_5k_card_type card_type,USHORT ScreenID);
//销毁通讯
LED5KSDK_API void WINAPI Destroy(DWORD dwHand);
//设置通讯超时
LED5KSDK_API void WINAPI SetTimeout(DWORD dwHand,DWORD nSec);
//服务器通讯超时
LED5KSDK_API void WINAPI SetLedAliveTick(DWORD dwHand,int time_sec);
//gprs通讯超时
LED5KSDK_API void WINAPI SetGprsAliveTick(DWORD dwHand,int time_sec);
//打开服务器
LED5KSDK_API BOOL WINAPI StartServer(UINT port,void (CALLBACK* pConnectFunc)(DWORD dwHand,unsigned char* pid),void (CALLBACK* pCloseFunc)(DWORD dwHand,unsigned char* pid,int err_code));
//关闭服务器
LED5KSDK_API void WINAPI CloseServer();
//服务器查询
LED5KSDK_API int WINAPI ServerGetCardList(unsigned char* pList);
//启动gprs服务器
LED5KSDK_API BOOL WINAPI StartGprsServer(UINT port,void (CALLBACK* pConnectFunc)(DWORD dwHand,unsigned char* pid),void (CALLBACK* pCloseFunc)(DWORD dwHand,unsigned char* pid,int err_code));
//关闭gprs服务器                 
LED5KSDK_API void WINAPI CloseGprsServer();
//gprs查询
LED5KSDK_API int WINAPI GprsServerGetCardList(unsigned char* pList);

#define COMMAND_API LED5KSDK_API int WINAPI
//ping
COMMAND_API CON_PING(DWORD dwHand);
//复位
COMMAND_API CON_Reset(DWORD dwHand);
//查询控制器状态
COMMAND_API CON_ControllerStatus(DWORD dwHand,BYTE* pStatus,USHORT* len);
//查询字库信息
COMMAND_API CON_CheckCurrentFont(DWORD dwHand,BYTE* fontStatus,USHORT* len);
//回读客户信息
COMMAND_API CON_CheckCurrentCustomer(DWORD dwHand,BYTE* CustomerStatus,USHORT* len);
//参数回读
COMMAND_API CON_ReadScreen(DWORD dwHand,BYTE* ScreenStatus,USHORT* len);
//校时
COMMAND_API CON_SytemClockCorrect(DWORD dwHand);
//查询固件
COMMAND_API CON_CheckCurrentFirmware(DWORD dwHand,char* FirmwareName,char* FirmwareVersion,char* FirmwareDateTime);
//激活固件
COMMAND_API CON_FirmwareActivate(DWORD dwHand,char* FirmwareName);
//设置屏号
COMMAND_API CON_SetScreenID(DWORD dwHand,USHORT newScreenID);
//读取屏号
COMMAND_API CON_ReadScreenID(DWORD dwHand,USHORT* pScreenID);

//开机
COMMAND_API SCREEN_ForceOnOff(DWORD dwHand,BYTE OnOffFlag);
//设置定时开关
COMMAND_API SCREEN_TimeTurnOnOff(DWORD dwHand,BYTE* pTimer,int nGroup);	
//设置亮度
COMMAND_API SCREEN_SetBrightness(DWORD dwHand,BYTE BrightnessType,BYTE CurrentBrightness,BYTE* BrightnessValue);	
//设置上电等待时间
COMMAND_API SCREEN_SetWaitTime(DWORD dwHand,BYTE WaitTime);		
//锁定节目
COMMAND_API SCREEN_LockProgram(DWORD dwHand,BYTE LockFlag,BYTE StoreMode,char* ProgramFileName);	
//删除动态区
COMMAND_API SCREEN_DelDynamicArea(DWORD dwHand,BYTE DeleteAreaId);  
//更新动态区
COMMAND_API SCREEN_SendDynamicArea(DWORD dwHand,bx_5k_area_header header,USHORT TextLen,BYTE* AreaText);
//更新声音动态区
COMMAND_API SCREEN_SendSoundDynamicArea(DWORD dwHand,bx_5k_area_header header,USHORT TextLen,BYTE* AreaText,BYTE SoundMode,BYTE SoundPerson,BYTE SoundVolume,BYTE SoundSpeed,int sound_len,BYTE* sounddata);
COMMAND_API SCREEN_Test(DWORD dwHand,BYTE TestTime);	
//取消定时
COMMAND_API SCREEN_CancelTimeOnOff(DWORD dwHand);	

//  设置特殊应用劢态区
COMMAND_API BX5MK_SetSpecialAppDynamic(DWORD dwHand,USHORT AreaX,USHORT AreaY,USHORT AreaW,USHORT AreaH,BYTE DataType,BYTE Pagetotal,BYTE RunState,USHORT Timeout,BYTE SingleLine,BYTE Lines_sizes,BYTE NewLine,USHORT StayTime); 
//  发送分页数据
COMMAND_API BX5MK_SendPageData(DWORD dwHand,BYTE PageNum,USHORT PageDataLen,BYTE* PageData);
//  发送点阵信息
COMMAND_API BX5MK_SendLatticeMessage(DWORD dwHand,BYTE BlockFlag,USHORT BlockAddr,BYTE* BlockData,USHORT BlockDataLen);
//  删除特殊应用劢态区
COMMAND_API BX5MK_DelSpecialAppDynamic(DWORD dwHand);
// 设置 IP 地址
COMMAND_API BX5MK_SetIPAddress(DWORD dwHand,BYTE ConnnectMode,char* ip,char* SubnetMask,char* Gateway,USHORT port,BYTE ServerMode,char* ServerIPAddress,USHORT ServerPort,char* ServerAccessPassword,USHORT HeartBeatInterval,char* NetID);
//  设置 MAC 地址
COMMAND_API BX5MK_SetMACAddress(DWORD dwHand,BYTE* MAC);
// 网络搜索 
COMMAND_API BX5MK_WebSearch(DWORD dwHand,BYTE* recv_buff,USHORT* recv_len);
//  删除页数据
COMMAND_API BX5MK_DelPageData(DWORD dwHand,BYTE PageLog);

//格式化
COMMAND_API OFS_Formatting(DWORD dwHand);
//删除节目
COMMAND_API OFS_DeleteFile(DWORD dwHand,USHORT FileNumber,char* pFileNameList);	
//写多文件
COMMAND_API OFS_BeginSendMultiFiles(DWORD dwHand);	
//写文件
COMMAND_API OFS_SendFile(DWORD dwHand,BYTE overwrite,char* pFilePath);
//发送节目
COMMAND_API OFS_SendFileData(DWORD dwHand,BYTE overwrite,char* pFileName,USHORT DisplayType,BYTE PlayTimes,BYTE* ProgramLife,BYTE ProgramWeek,BYTE ProgramTime,BYTE* Period,BYTE AreaNum, BYTE* AreaDataList,int AreaDataListLen);
//结束写多文件
COMMAND_API OFS_EndSendMultiFiles(DWORD dwHand);
//字库
COMMAND_API OFS_SendFontData(DWORD dwHand,BYTE overwrite,char* pFileName,BYTE FontWidth,BYTE FontHeight,BYTE* LibData,int LibData_len, BYTE FontEncode,void (CALLBACK* pCloseFunc)(int total,int sendlen));
//设置客户信息
COMMAND_API OFS_SetFontInformation(DWORD dwHand,BYTE OverWrite,char* ClientMsg);
//设置屏参
COMMAND_API OFS_SendScreenData(DWORD dwHand,BYTE overwrite,char* pFileName,USHORT Address,BYTE Baudrate,USHORT ScreenWith,USHORT ScreenHeight,
	BYTE Color,BYTE	MirrorMode,BYTE	OE,BYTE	DA,BYTE	RowOrder,BYTE FreqPar,BYTE OEAngle,BYTE CommTimeout,BYTE TipLanguage,BYTE LatticeMode);
//固件
COMMAND_API OFS_SendFirmWareData(DWORD dwHand,BYTE overwrite,char* pFileName, BYTE* FirmWareData,int FirmWareDataLen,void (CALLBACK* pCloseFunc)(int total,int sendlen));
//
COMMAND_API OFS_SendScanData(DWORD dwHand,BYTE overwrite,char* pFileName, BYTE* ScanData,int ScanDataLen);
//语音
COMMAND_API SCREEN_SendSound(DWORD dwHand,bx_5k_sound sound,USHORT TextLen,BYTE* AreaText);
//发送表格
COMMAND_API OFS_SendTable(DWORD dwHand,USHORT OriginX,USHORT OriginY,USHORT TableWidth,USHORT TableHeight,BYTE RowNum,BYTE LineNum,BYTE CellNum, BYTE* TableDataList,int TableDataListLen);


COMMAND_API SendAndRecvBuff(DWORD dwHand, BYTE cmd_group, BYTE cmd, BYTE* cmd_data, USHORT data_len, BYTE* recv_data, USHORT* p_recv_len);
COMMAND_API SendBuff(DWORD dwHand, BYTE cmd_group, BYTE cmd, BYTE* cmd_data, USHORT data_len);
//发送多动态区
COMMAND_API SCREEN_SendDynamicAreas(DWORD dwHand,BYTE AreaNum,USHORT TextLen,BYTE* AreaText);
//外部IO电平
COMMAND_API CON_IOlevel(DWORD dwHand,BYTE Exp1,BYTE Exp2,BYTE LightDaPin);




