

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 7.00.0500 */
/* at Thu May 20 01:20:36 2010
 */
/* Compiler settings for .\GenericIntercomTranslator.idl:
    Oicf, W1, Zp8, env=Win32 (32b run)
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
//@@MIDL_FILE_HEADING(  )

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 475
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __GenericIntercomTranslator_h__
#define __GenericIntercomTranslator_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IDistributeEvent_FWD_DEFINED__
#define __IDistributeEvent_FWD_DEFINED__
typedef interface IDistributeEvent IDistributeEvent;
#endif 	/* __IDistributeEvent_FWD_DEFINED__ */


#ifndef __ITranslate_FWD_DEFINED__
#define __ITranslate_FWD_DEFINED__
typedef interface ITranslate ITranslate;
#endif 	/* __ITranslate_FWD_DEFINED__ */


#ifndef __IComConfig_FWD_DEFINED__
#define __IComConfig_FWD_DEFINED__
typedef interface IComConfig IComConfig;
#endif 	/* __IComConfig_FWD_DEFINED__ */


#ifndef __IIntercom_FWD_DEFINED__
#define __IIntercom_FWD_DEFINED__
typedef interface IIntercom IIntercom;
#endif 	/* __IIntercom_FWD_DEFINED__ */


#ifndef __IntercomTrans_FWD_DEFINED__
#define __IntercomTrans_FWD_DEFINED__

#ifdef __cplusplus
typedef class IntercomTrans IntercomTrans;
#else
typedef struct IntercomTrans IntercomTrans;
#endif /* __cplusplus */

#endif 	/* __IntercomTrans_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"
#include "time.h"

#ifdef __cplusplus
extern "C"{
#endif 


/* interface __MIDL_itf_GenericIntercomTranslator_0000_0000 */
/* [local] */ 

#ifndef LMSGTYPE_H
#define LMSGTYPE_H

#pragma pack(8)
#include "LNL_Events.h"
#define MAX_PANELS_SERVER			256
#define LNL_TRANS_OPERATION_NOP		0
#define LNL_TRANS_OPERATION_ADD		1
#define LNL_TRANS_OPERATION_CHG		2
#define LNL_TRANS_OPERATION_DEL		3
#define LNL_TRANS_OPERATION_PULSE	4
#define LNL_MAX_ASSET_SIZE			32
#define LNL_ASSET_ID_SIZE			12
#define LNL_MAX_ASSET_OWNERS			8
#define LNL_MAX_ASSET_ARGS			16
#define LNL_MAX_ASSET_TYPES_PER_GROUP	64
#define LNL_MAX_EVENTID_SIZE			11
#define LNL_MAX_TITLE_SIZE			128
#define LNL_MAX_USERNAME_SIZE		32
#define LNL_MAX_PASSWORD_SIZE		32
#define LNL_MAX_ASSET_GROUPS_PER_PANEL		65535
#define LNL_DEFAULT_ASSET_GROUPS_PER_PANEL	128
#define LNL_ASSET_ARG_TYPE_ASSET_TYPE			1
#define LNL_ASSET_ARG_TYPE_ASSET_CLASS			2
#define LNL_ASSET_ARG_TYPE_ASSET_DISABLE_CODES	3
#define MAX_SIO_PER_PANEL			64
#define MAX_SNMP_MANAGER_IPADDRESSES			4
#define DEVICE_PORT_2		2
#define DEVICE_PORT_3		3
#define DEVICE_PORT_4		4
#define DEVICE_PORT_5		5
#define DEVICE_PORT_6		6
#define DEVICE_PORT_23		7
#define DEVICE_PORT_45		8
#define DEVICE_PORT_12		9
#define DEVICE_PORT_34		10
#define FIRST_OUTPUT_ID		17
#define FIRST_ALARMPANEL_ID	(MAX_READERS + 1)
#define LAST_ALARMPANEL_ID	(MAX_READERS + 16)
#define LAST_ALARMPANEL_ID2	(MAX_READERS + MAX_ALARM_PANELS)
#define LAST_ALARMPANEL_ID3	(MAX_READERS + MAX_ALARM_PANELS_NEW)
#define FIRST_READER_ID		1
#define LAST_READER_ID		64
#define FIRST_INPUT_ID		1
#define LAST_INPUT_ID		16
#define LNL_ALL_PANELS		-1
#define ACTIVATE_OPERATION_MASK	0x80
#define INPUT_ARG_MASK		0x3F
#define INPUT_SHIFT			6
#define AAP_LUN				0x00
#define CONTROLLER_LUN		0x01
#define READER_LUN			0x02
#define ALARMPANEL_LUN		0x03
#define HOST_LUN			0x06
#define AREA_LUN			0xFC
#define ZONE_LUN			0xFB
#define IV_LUN				0xFD
#define TIMEZONE_LUN		0xFE
#define TRIGGERVAR_LUN		0xFF
#define REX_INPUT_ID		255
#define DC_INPUT_ID		254
#define NOTIFIER_ANNUNCIATOR_OFFSET			0
#define NOTIFIER_LOOP_OFFSET					64
#define NOTIFIER_PANEL_MODULE_OFFSET			100
#define NOTIFIER_BELL_CIRCUIT_OFFSET			200
#define NOTIFIER_LOOP_DETECTOR_OFFSET		0
#define NOTIFIER_LOOP_MODULE_OFFSET			1000
#define PYROTRONICS_MODULE_OFFSET			65
#define OFFSET_INTRUSION_ZONE_ID				0
#define FIRST_INTRUSION_ZONE_ID				1
#define LAST_INTRUSION_ZONE_ID				10000
#define OFFSET_INTRUSION_OFFBOARDRELAY_ID	10000
#define FIRST_INTRUSION_OFFBOARDRELAY_ID		10001
#define LAST_INTRUSION_OFFBOARDRELAY_ID		15000
#define OFFSET_INTRUSION_DOOR_ID				15000
#define FIRST_INTRUSION_DOOR_ID				15001
#define LAST_INTRUSION_DOOR_ID				20000
#define OFFSET_INTRUSION_ONBOARDRELAY_ID		20000
#define FIRST_INTRUSION_ONBOARDRELAY_ID		20001
#define LAST_INTRUSION_ONBOARDRELAY_ID		21000
#define OFFSET_INTRUSION_AREA_ID				21000
#define FIRST_INTRUSION_AREA_ID				21001
#define LAST_INTRUSION_AREA_ID				23000
#define ZONE_AAP_CABINET_TAMPER		0
#define ZONE_AAP_POWERFAIL			1
#define ZONE_AAP_COMMLOSS			255
#define ZONE_CTRL_COMMSTATUS		0
#define ZONE_CTRL_CABINET_TAMPER	1
#define ZONE_CTRL_POWERFAIL			2
#define ZONE_RDR_INPTAMPER			0
#define ZONE_RDR_FORCEDOPEN			1
#define ZONE_RDR_HELDOPEN			2
#define ZONE_RDR_AUXINPUT			3
#define ZONE_RDR_DIDDLE				4
#define ZONE_RDR_ACCESSACT			5
#define ZONE_RDR_DOORCONTACT			6
#define ZONE_RDR_AUXINPUT2			7
#define ZONE_RDR_AUXINPUT3			8
#define ZONE_HOST_COMMSTATUS		0
#define ENCRYPTED_COMMUNICATION_NONE					0
#define ENCRYPTED_COMMUNICATION_NOT_REQUIRED			1
#define ENCRYPTED_COMMUNICATION_REQUIRED				2
#define ENCRYPTED_COMMUNICATION_REQUIRED_CUSTOM_KEY	3
// video standards
#define LNL_VIDEO_STANDARD_PAL	0
#define LNL_VIDEO_STANDARD_NTSC	1
#define LNL_VIDEO_STANDARD_SECAM	2
// video picture size
#define LNL_VIDEO_PICTURE_SIZE_LDVR_DEFAULT             (-1)
#define LNL_VIDEO_PICTURE_SIZE_CIF                      (0)
#define LNL_VIDEO_PICTURE_SIZE_CIF_SQUARE               (1)
#define LNL_VIDEO_PICTURE_SIZE_QCIF                     (2)
#define LNL_VIDEO_PICTURE_SIZE_QCIF_SQUARE              (3)
#define LNL_VIDEO_PICTURE_SIZE_D1                       (4)
#define LNL_VIDEO_PICTURE_SIZE_D1_SQUARE                (5)
#define LNL_VIDEO_PICTURE_SIZE_D1_2FIELDS_VERT          (6)
#define LNL_VIDEO_PICTURE_SIZE_D1_2FIELDS_VERT_SQUARE   (7)
#define LNL_VIDEO_PICTURE_SIZE_2CIF                     (8)
#define LNL_VIDEO_PICTURE_SIZE_2CIF_SQUARE              (9)
typedef short FLAG;

typedef struct _OLDDATA_CNA
    {
    DWORD sl_CardNumber;
    } 	OLDDATA_CNA;

typedef struct _DATA_CNA
    {
    __int64 sl_CardNumber;
    } 	DATA_CNA;

#define REASON_WRONG_CARDFORMAT		0x00 
#define REASON_DENYCNT_EXCEEDED		0x01
#define REASON_REQBY_PINONLY		    0x02
typedef struct _DATA_FC
    {
    DWORD sl_FacilityCode;
    DWORD sl_IssueCode;
    } 	DATA_FC;

#define REASON_WRONG_FACILITYCODE	0x00
typedef struct _OLDDATA_CA
    {
    DWORD sl_CardNumber;
    DWORD sl_IssueCode;
    DWORD sl_BioScore;
    } 	OLDDATA_CA;

typedef struct _DATA_CA
    {
    __int64 sl_CardNumber;
    DWORD sl_IssueCode;
    DWORD sl_BioScore;
    } 	DATA_CA;

#define REASON_GRANTED				0x00
#define REASON_GRANTED_DURESS		0x01
#define REASON_INVALID_BADGE		    0x02
#define REASON_WRONG_ISSUECODE		0x03
#define REASON_WRONG_PIN			    0x04
#define REASON_WRONG_ACCESSLEVEL	    0x05
#define REASON_ANTIPASSBACK			0x06
#define REASON_INACTIVE_BADGE		0x07
#define REASON_GRANTED_FCODEONLY	    0x08
#define REASON_GRANTED_NOENTRY		0x09
#define REASON_DURESS_NOENTRY		0x0a
#define REASON_GRANTED_FCNOENTRY	    0x0b
#define REASON_AREA_LIMITEXCEEDED	0x0c
#define REASON_TIMEOUT_EXCEEDED		0x0d
#define REASON_AREA_CLOSED			0x0e
#define REASON_READER_EXCLUDED		0x0f
#define REASON_DURESS				0x10
#define REASON_COMMAND_AUTHORITY	    0x11
#define REASON_DENIED_UNMASK		    0x12
#define REASON_GRANTED_APB_USED		0x13
#define REASON_GRANTED_APB_NOTUSED	0x14
typedef struct _DATA_STATUS_CHG
    {
    BYTE sb_NewStatus;
    BYTE sb_OldStatus;
    short sw_ComStatus;
    } 	DATA_STATUS_CHG;

#define STATUS_NOT_CONFIGURED	0x00
#define STATUS_ONLINE			0x10
#define STATUS_ACTIVE_MASKED	    0x19
#define STATUS_ALARM_ACTIVE		0x11
#define STATUS_TAMPER_ACTIVE	    0x02
#define STATUS_LINE_ERROR		0x04
#define STATUS_SHORTED_LINE		0x05
#define STATUS_OPEN_LINE		    0x06
#define STATUS_GROUNDED_LOOP	    0x07
#define STATUS_MASKED_BIT		0x08
#define STATUS_FAULT_BIT			0x04
#define INPUT_ARG_NOP	0x00
#define INPUT_ARG_FALSE	0x01
#define INPUT_ARG_TRUE	0x02
#define INPUT_ARG_PULSE	0x03
#define CMD_DOOR_SHUNT			1
#define CMD_SET_FUNC_TRUE		2
#define CMD_SET_FUNC_FALSE		3
#define CMD_SET_FUNC_CMD4		4
#define CMD_SET_FUNC_CMD5		5
#define CMD_SET_FUNC_CMD6		6
#define CMD_SET_FUNC_CMD7		7
#define CMD_SET_FUNC_CMD8		8
#define CMD_SET_FUNC_CMD9		9
#define CMD_SET_FUNC_CMD10		10
#define CMD_SET_FUNC_CMD11		11
#define CMD_SET_FUNC_CMD12		12
#define CMD_SET_FUNC_CMD13		13
#define CMD_SET_FUNC_CMD14		14
#define CMD_SET_FUNC_CMD15		15
typedef struct _DATA_INTERCOM
    {
    DWORD sl_IntercomData;
    DWORD sl_LineNumber;
    } 	DATA_INTERCOM;

typedef struct _DATA_VIDEOEVENT
    {
    long Channel;
    SYSTEMTIME StartTime;
    SYSTEMTIME EndTime;
    } 	DATA_VIDEOEVENT;

typedef struct _DATA_TRANSMITTER
    {
    DWORD TransmitterID;
    } 	DATA_TRANSMITTER;

typedef struct _OLDDATA_WKSTN_CARD
    {
    DWORD sl_CardNumber;
    DWORD sl_WorkstationID;
    } 	OLDDATA_WKSTN_CARD;

typedef struct _DATA_WKSTN_CARD
    {
    __int64 sl_CardNumber;
    DWORD sl_WorkstationID;
    } 	DATA_WKSTN_CARD;

typedef struct _OLDDATA_CMDREQ
    {
    DWORD sl_CardNumber;
    } 	OLDDATA_CMDREQ;

typedef struct _DATA_CMDREQ
    {
    __int64 sl_CardNumber;
    } 	DATA_CMDREQ;

typedef struct _DATA_USERCMD
    {
    __int64 sl_CardNumber;
    DWORD sl_AlarmMaskGroup;
    } 	DATA_USERCMD;

typedef struct _DATA_ALARMMASKGROUP
    {
    DWORD sl_AlarmMaskGroup;
    } 	DATA_ALARMMASKGROUP;

typedef struct _DATA_EVENT_ROUTING
    {
    DWORD sl_SourceWorkstation;
    } 	DATA_EVENT_ROUTING;

#define RES_DOOR_SHUNT_CANCELED			1
typedef struct _DATA_CMDRES
    {
    BYTE sb_Results;
    } 	DATA_CMDRES;

typedef struct _DATA_STATUSREQUEST
    {
    DWORD sl_StatusType;
    DWORD sl_Status;
    unsigned char sc_String[ 32 ];
    } 	DATA_STATUSREQUEST;

typedef struct _DATA_STATUSREQUEST2
    {
    DWORD sl_StatusType;
    __int64 sl_Status;
    unsigned char sc_String[ 64 ];
    } 	DATA_STATUSREQUEST2;


enum LineStatus
    {	LineStatusSecure	= 0,
	LineStatusAlarm	= ( LineStatusSecure + 1 ) ,
	LineStatusShortedLine	= ( LineStatusAlarm + 1 ) ,
	LineStatusOpenLine	= ( LineStatusShortedLine + 1 ) ,
	LineStatusGroundedLine	= ( LineStatusOpenLine + 1 ) ,
	LineStatusLineError	= ( LineStatusGroundedLine + 1 ) 
    } ;
#define DATA_SRQ_COMM_STATE			0
#define DATA_SRQ_POWERINPUT_STATE	1
#define DATA_SRQ_CABINET_STATE		2
#define DATA_SRQ_READER_MODE			3
#define DATA_SRQ_RDR_TAMPER_STATE	4
#define DATA_SRQ_RDR_FORCED_STATE	5
#define DATA_SRQ_RDR_HELD_STATE		6
#define DATA_SRQ_RDR_FORCED_MASK		7
#define DATA_SRQ_RDR_HELD_MASK		8
#define DATA_SRQ_AUX_INPUT_STATE		9
#define DATA_SRQ_AUX_INPUT_MASKED	10
#define DATA_SRQ_ALARM_INPUT_STATE	11
#define DATA_SRQ_ALARM_INPUT_MASKED	12
#define DATA_SRQ_AUX_OUTPUT_STATE	13
#define DATA_SRQ_ALARM_OUTPUT_STATE	14
#define DATA_SRQ_PANEL_TIME			15
#define DATA_SRQ_PANEL_MEM_MAX		16
#define DATA_SRQ_DEVICE_MAX_CARDS	17
#define DATA_SRQ_DEVICE_CUR_CARDS	18
#define DATA_SRQ_FIRMWARE_REV		19
#define DATA_SRQ_PANEL_MEM_FREE		20
#define DATA_SRQ_MASKGROUP_MASKLVL	21
#define DATA_SRQ_MASKGROUP_ACTPTS	22
#define DATA_SRQ_AREA_OPEN_STATUS	23
#define DATA_SRQ_AREA_OCCUPANCY		24
#define DATA_SRQ_FIRMWARE_DOWNLOAD	25
#define DATA_SRQ_PANEL_DOWNLOAD		26
#define DATA_SRQ_ALARM_OUTPUT_MODE	27
#define DATA_SRQ_DEVICE_BMP_STATE	28
#define DATA_SRQ_AUX_INPUT_BMP_STATE	29
#define DATA_SRQ_ALARM_INPUT_BMP_STATE	30
#define DATA_SRQ_RDR_BMP_STATE			31
#define DATA_SRQ_INTERCOM_BUSY_STATE		32
#define DATA_SRQ_INTERCOM_HANDSET_STATE	33
#define DATA_SRQ_PANEL_MAX_ASSETS		34
#define DATA_SRQ_PANEL_CUR_ASSETS		35
#define DATA_SRQ_TRANSMITTER_SUPERVISION 36
#define DATA_SRQ_PANELTYPE_MISMATCH		37
#define DATA_SRQ_RDR_CONTROL_FLAGS		38
#define DATA_SRQ_PANEL_MAX_BIO1			39
#define DATA_SRQ_PANEL_CUR_BIO1			40
#define DATA_SRQ_ALT_RDR					41
#define DATA_SRQ_PANEL_OPTIONS_MISMATCH	42
#define DATA_SRQ_PANEL_MAX_BIO2			43
#define DATA_SRQ_PANEL_CUR_BIO2			44
#define DATA_SRQ_INVALID_DEVICE_SERIAL_NUM 45
#define DATA_SRQ_DEVICE_TYPE_MISMATCH	46
#define DATA_SRQ_INTRUSION_PANEL_STATUS		  47
#define DATA_SRQ_INTRUSION_ZONE_ALARM_STATUS  48
#define DATA_SRQ_INTRUSION_ZONE_OTHER_STATUS  49
#define DATA_SRQ_INTRUSION_RELAY_STATUS		  50
#define DATA_SRQ_INTRUSION_DOOR_MODE		  51
#define DATA_SRQ_INTRUSION_DOOR_OTHER_STATUS  52
#define DATA_SRQ_INTRUSION_AREA_ARMING_STATUS 53
#define DATA_SRQ_INTRUSION_AREA_ALARM_STATUS   54
#define DATA_SRQ_INTRUSION_AREA_OTHER_STATUS		55
#define DATA_SRQ_FIRMWARE_REV_STRING				56
#define DATA_SRQ_DOOR_OPEN						57
#define DATA_SRQ_RDR_DOOR_MASKED					58
#define DATA_SRQ_PANEL_MAX_BIO3					59
#define DATA_SRQ_PANEL_CUR_BIO3					60
#define DATA_SRQ_GATEWAY_COMM					61
#define DATA_SRQ_RDR_OFFLINE						62
#define DATA_SRQ_RDR_MRDT_OFFLINE				63
#define DATA_SRQ_PANEL_POWER_UP_DIP_SWITCHES		64
#define DATA_SRQ_PANEL_FLASH_SIZE				65
#define DATA_SRQ_DOOR_CONTACT_COMM_STATUS		66
#define DATA_SRQ_ENCRYPTED_CONNECTION			67
#define DATA_SRQ_ENCRYPTION_ERROR				68
#define DATA_SRQ_KEY_UPDATE_PENDING				69
#define DATA_SRQ_ENCRYPTION_BMP_STATE			70
#define DATA_SRQ_INTERCOM_BLOCKED_STATE			71
#define DATA_SRQ_AREA_SPECIAL_OCCUPANCY			72
#define DATA_SRQ_AREA_MULTI_OCCUPANCY_MODE		73
#define DATA_SRQ_IVAS_APPLICATION_STATUS			74
#define DATA_SRQ_EVENT_POLLING_STOPPED_STATUS	75
#define DATA_SRQ_DEVICE_SERIAL_NUMBER			76
#define DATA_SRQ_ELV_DISPATCHING_PANEL_STATE		77
#define DATA_SRQ_ELV_KEYPAD_TERMINAL_MODE		78
#define DATA_SRQ_INVALID_OEM_CODE				79
#define DATA_SRQ_DUAL_PATH_STATUS				80
#define DATA_SRQ_DEVICE_MARKED_OFFLINE			81
#define DATA_SRQ_AREA_DELETED					82
#define DATA_SRQ_FUNCLIST_DELETED				83
#define DATA_SRQ_MASKGROUP_DELETED				84
#define DATA_SRQ_DEVICE_DELETED					85
#define DATA_SRQ_ACCOUNT_GROUP_DELETED			86
#define DATA_SRQ_DEVICE_REFRESH					87
#define DATA_SRQ_AREA_REFRESH					88
#define DATA_SRQ_FUNCLIST_REFRESH				89
#define DATA_SRQ_MASKGROUP_REFRESH				90
#define DATA_SRQ_INTRUSION_MASKGROUP_STATE1		91
#define DATA_SRQ_INTRUSION_MASKGROUP_STATE2		92
#define DATA_SRQ_INTRUSION_POINT_STATE			93
#define DATA_SRQ_RDR_FORCED_INTRUSION_STATE		94
#define DATA_SRQ_RDR_HELD_INTRUSION_STATE		95
#define DATA_SRQ_RDR_DOOR_INTRUSION_STATE		96
#define DATA_SRQ_AUX_INPUT_INTRUSION_STATE		97
#define DATA_SRQ_DOOR_UNLOCKED					98
#define DATA_SRQ_DOOR_DEADBOLT_ENGAGED			99
#define DATA_SRQ_BATTERY_LEVEL					100
#define DATA_SRQ_LOCK_CYCLES						101
#define DATA_SRQ_DOWNSTREAM_ENCRYPTION_STATUS	102
#define DATA_SRQ_WIRELESS_DIAGNOSTICS			103
#define DATA_SRQ_DEVICE_EXT_STATUS				104
#define DATA_SRQ_FIRMWARE_DOWNLOAD_NONE							0
#define DATA_SRQ_FIRMWARE_DOWNLOAD_IN_PROGRESS					1
#define DATA_SRQ_FIRMWARE_DOWNLOAD_PENDING						2
#define DATA_SRQ_VAL_CORRECT_PLAIN								0
#define DATA_SRQ_VAL_CORRECT_FIPS_BYPASSED						1
#define DATA_SRQ_VAL_CORRECT_ENCRYPTED							2
#define DATA_SRQ_VAL_CORRECT_ENCRYPTED_FIPS						3
#define DATA_SRQ_VAL_INVALID_PLAIN_KEY_MISMATCH					4
#define DATA_SRQ_VAL_INVALID_PLAIN_NO_SUPPORT					5
#define DATA_SRQ_VAL_INVALID_ENCRYPTION_FROM_PLAIN				6
#define DATA_SRQ_VAL_INVALID_ENCRYPTION_FROM_ENCRYPTED_INACTIVE	7
#define DATA_SRQ_VAL_INVALID_ENCRYPTION_FROM_ENCRYPTED_DEFAULT	8
#define DATA_SRQ_VAL_CONTROLLER_NO_ENCRYPTION_ERROR			0
#define DATA_SRQ_VAL_CONTROLLER_REQUIRES_ENCRYPTION			1
#define DATA_SRQ_VAL_CONTROLLER_DOES_NOT_SUPPPORT_ENCRYPTION	2
#define DATA_SRQ_VAL_KEY_MISMATCH							3
#define DATA_SRQ_VAL_BAD_PASSWORD							4
#define DATA_SRQ_BIT_ENCRYPTED_CONNECTION			0x0F
#define DATA_SRQ_BIT_ENCRYPTION_ERROR				0x70
#define DATA_SRQ_BIT_ENCRYPTION_KEY_UPDATE_PENDING	0x80
#define DATA_SRQ_DOWNSTREAM_CONNECTION_PLAIN				0
#define DATA_SRQ_DOWNSTREAM_CONNECTION_ENCRYPTED_DEFAULT	1
#define DATA_SRQ_DOWNSTREAM_CONNECTION_ENCRYPTED_CUSTOM	2
#define DATA_SRQ_BIT_DEV_ONLINE		0x01
#define DATA_SRQ_BIT_DEV_POWERFAIL	0x02
#define DATA_SRQ_BIT_DEV_CABINET		0x04
#define DATA_SRQ_BIT_INPUT_MASKED	0x10
// The first set of bits are for the reader mode
#define DATA_SRQ_BIT_RDR_TAMPER			0x10
#define DATA_SRQ_BIT_RDR_HELDOPEN		0x20
#define DATA_SRQ_BIT_RDR_FORCEOPEN		0x40
#define DATA_SRQ_BIT_RDR_FORCEMASK		0x80
#define DATA_SRQ_BIT_RDR_HELDMASK		0x100
#define DATA_SRQ_BIT_RDR_EXTENDEDHELD	0x200
#define DATA_SRQ_BIT_RDR_LOW_BATTERY		0x400
#define DATA_SRQ_BIT_RDR_MOTOR_STALLED	0x800
#define DATA_SRQ_BIT_RDR_OFFLINE			0x1000
#define DATA_SRQ_BIT_RDR_MRDT_OFFLINE	0x2000
#define DATA_SRQ_INVALID_OEM_CODE_VALID	((DWORD)-1)
#define EVENT_DATA_TYPE_CNA		0x00	// Access Request , card number not available use us_DataCNA
#define EVENT_DATA_TYPE_FC		0x01	// Access Request , Facility Code Available	use us_DataFC
#define EVENT_DATA_TYPE_CA		0x02	// Access Request, Card Number Available	use us_DataCA
#define EVENT_DATA_TYPE_STATUS	0x03	// Status Change Event use us_StatusChg
#define EVENT_STOR_TYPE_CNA		0x04	// Access Request , card number not available use us_DataCNA
#define EVENT_STOR_TYPE_FC		0x05	// Access Request , Facility Code Available	use us_DataFC
#define EVENT_STOR_TYPE_CA		0x06	// Access Request, Card Number Available	use us_DataCA
#define EVENT_STOR_TYPE_STATUS	0x07	// Status Change Event use us_StatusChg
#define EVENT_IV_TYPE_CNA		0x08
#define EVENT_IV_TYPE_FC			0x09
#define EVENT_IV_TYPE_CA			0x0A	// 10
#define EVENT_IV_TYPE_STATUS		0x0B	// 11
#define EVENT_IV_TYPE_HOST		0x0C	// 12
#define EVENT_IV_TYPE_CMD_REQ	0x0D	// 13
#define EVENT_IV_TYPE_CMD_RES	0x0E	// 14
#define EVENT_IV_STOR_CMD_REQ	0x11	// 17
#define EVENT_IV_STOR_CMD_RES	0x12	// 18
#define EVENT_DATA_TYPE_STATUSREQUEST	0x13 //19
#define EVENT_DATA_ASSET					0x14
#define EVENT_DATA_TYPE_INTERCOM			0x15
#define EVENT_DATA_TYPE_VIDEO			0x16
#define EVENT_DATA_TYPE_TRANSMITTER		0x17
#define EVENT_DATA_TYPE_WKSTN_CARD		0x18
#define EVENT_DATA_TYPE_RECEIVER			0x19
#define EVENT_DATA_TYPE_AREAAPB			0x1A
#define EVENT_DATA_TYPE_INTRUSION		0x1B
#define EVENT_DATA_TYPE_USERCMD			0x1C
#define EVENT_DATA_TYPE_ALARMMASKGROUP	0x1D
#define EVENT_DATA_TYPE_EVENT_ROUTING	0x1E
typedef struct _DATA_AREAAPB
    {
    long sl_AreaAPBID;
    } 	DATA_AREAAPB;

typedef struct _OLDDATA_ASSET
    {
    DWORD sl_CardNumber;
    BYTE sl_AssetID[ 32 ];
    long sl_EventType;
    long sl_EventID;
    } 	OLDDATA_ASSET;

typedef struct _DATA_ASSET
    {
    __int64 sl_CardNumber;
    BYTE sl_AssetID[ 32 ];
    long sl_EventType;
    long sl_EventID;
    } 	DATA_ASSET;

typedef struct _DATA_RECEIVER
    {
    short ReceiverID;
    short LineNum;
    short Area;
    short UserID;
    BYTE EventCode[ 10 ];
    } 	DATA_RECEIVER;

typedef struct _DATA_INTRUSION
    {
    short Area;
    short UserID;
    } 	DATA_INTRUSION;

typedef /* [switch_type] */ /* [switch_type] */ union _OLDEVENTDATA
    {
    OLDDATA_CNA us_DataCNA;
    DATA_STATUS_CHG us_DataStatusChg;
    OLDDATA_CA us_DataCA;
    DATA_FC us_DataFC;
    OLDDATA_CMDREQ us_DataCmdReq;
    DATA_CMDRES us_DataCmdRes;
    DATA_STATUSREQUEST us_StatusRequest;
    OLDDATA_ASSET us_AssetData;
    DATA_INTERCOM us_IntercomData;
    DATA_VIDEOEVENT us_VideoData;
    DATA_TRANSMITTER us_TransmitterData;
    OLDDATA_WKSTN_CARD us_WorkstationCardData;
    DATA_RECEIVER us_ReceiverData;
    DATA_AREAAPB us_AreaAPBData;
    DATA_INTRUSION us_IntrusionData;
    } 	OLDEVENTDATA;

typedef /* [switch_type] */ /* [switch_type] */ union _EVENTDATA
    {
    DATA_CNA us_DataCNA;
    DATA_STATUS_CHG us_DataStatusChg;
    DATA_CA us_DataCA;
    DATA_FC us_DataFC;
    DATA_CMDREQ us_DataCmdReq;
    DATA_CMDRES us_DataCmdRes;
    DATA_STATUSREQUEST us_StatusRequest;
    DATA_ASSET us_AssetData;
    DATA_INTERCOM us_IntercomData;
    DATA_VIDEOEVENT us_VideoData;
    DATA_TRANSMITTER us_TransmitterData;
    DATA_WKSTN_CARD us_WorkstationCardData;
    DATA_RECEIVER us_ReceiverData;
    DATA_AREAAPB us_AreaAPBData;
    DATA_INTRUSION us_IntrusionData;
    DATA_USERCMD us_UserCmdData;
    DATA_ALARMMASKGROUP us_AlarmMaskGroupData;
    DATA_EVENT_ROUTING us_EventRoutingData;
    } 	EVENTDATA;

typedef /* [switch_type] */ /* [switch_type] */ union _EVENTDATA2
    {
    DATA_CNA us_DataCNA;
    DATA_STATUS_CHG us_DataStatusChg;
    DATA_CA us_DataCA;
    DATA_FC us_DataFC;
    DATA_CMDREQ us_DataCmdReq;
    DATA_CMDRES us_DataCmdRes;
    DATA_STATUSREQUEST2 us_StatusRequest;
    DATA_ASSET us_AssetData;
    DATA_INTERCOM us_IntercomData;
    DATA_VIDEOEVENT us_VideoData;
    DATA_TRANSMITTER us_TransmitterData;
    DATA_WKSTN_CARD us_WorkstationCardData;
    DATA_RECEIVER us_ReceiverData;
    DATA_AREAAPB us_AreaAPBData;
    DATA_INTRUSION us_IntrusionData;
    DATA_USERCMD us_UserCmdData;
    DATA_ALARMMASKGROUP us_AlarmMaskGroupData;
    DATA_EVENT_ROUTING us_EventRoutingData;
    } 	EVENTDATA2;

typedef struct _EVENTTYPEMASKS
    {
    BYTE sb_GrantedEvents;
    BYTE sb_DeniedEvents;
    BYTE sb_EmergencyEvents;
    BYTE sb_AreaControlEvents;
    BYTE sb_SystemEvents;
    BYTE sb_Asset;
    BYTE sb_Fire1;
    BYTE sb_Fire2;
    BYTE sb_Fire3;
    BYTE sb_Intercom;
    BYTE sb_Video;
    BYTE sb_Transmitter;
    BYTE sb_NetworkWorkstation;
    BYTE sb_NetworkCard;
    BYTE sb_NetworkPin;
    BYTE sb_NetworkUser;
    BYTE sb_NetworkServer;
    BYTE sb_Biometric;
    BYTE sb_Trouble;
    BYTE sb_Digitize;
    BYTE sb_Burglary;
    BYTE sb_Temperature;
    BYTE sb_Gas;
    BYTE sb_RelaySounder;
    BYTE sb_Medical;
    BYTE sb_Water;
    BYTE sb_C900;
    BYTE sb_OpenClose;
    BYTE sb_Generic;
    BYTE sb_POS;
    BYTE sb_PP;
    } 	EVENTTYPEMASKS;

#define L_EVENTTYPE_GRANTED			0x00	// logical group for access granted events
#define L_EVENTTYPE_DENIED			0x01	// logical group for access denied events
#define L_EVENTTYPE_EMERGENCY		0x02	// logical group for emergency events
#define L_EVENTTYPE_AREACONTROL		0x03	// logical group for area control events
#define L_EVENTTYPE_SYSTEM			0x04	// logical group for system events
#define L_EVENTTYPE_ASSET			0x05	// logical group for Asset events
#define L_EVENTTYPE_HOSTMSG			0x06	// logical group for host type messages
#define L_EVENTTYPE_FIRE				0x07	// logical group for fire events
#define L_EVENTTYPE_FIRE2			0x08	// logical group for fire events
#define L_EVENTTYPE_FIRE3			0x09	// logical group for fire events
#define L_EVENTTYPE_INTERCOM			0x0a	// logical group for intercom events
#define L_EVENTTYPE_VIDEO			0x0b	// logical group for video events
#define L_EVENTTYPE_TRANSMITTER		0x0c	// logical group for transmitter events
#define L_EVENTTYPE_NETWORK_WORKSTATION 0x0d // logical group for network workstation events (like NonStopID WorkstationEvent)
#define L_EVENTTYPE_NETWORK_CARD		0x0e	// logical group for network card events (like NonStopID CardEvent)
#define L_EVENTTYPE_NETWORK_PIN		0x0f	// logical group for network pin events (like NonStopID PINEvent)
#define L_EVENTTYPE_NETWORK_USER		0x10	// logical group for network user events (like NonStopID UserEvent)
#define L_EVENTTYPE_NETWORK_SERVER	0x11	// logical group for netword server events (like NonStopID ServerEvent)
#define L_EVENTTYPE_BIOMETRIC		0x12	// logical group for biometric events
#define L_EVENTTYPE_TROUBLE			0x13
#define L_EVENTTYPE_DIGITIZE			0x14
#define L_EVENTTYPE_BURGLARY			0x15
#define L_EVENTTYPE_TEMPERATURE		0x16
#define L_EVENTTYPE_GAS				0x17
#define L_EVENTTYPE_RELAY_SOUNDER	0x18
#define L_EVENTTYPE_MEDICAL			0x19
#define L_EVENTTYPE_WATER			0x1a
#define L_EVENTTYPE_C900				0x1b
#define L_EVENTTYPE_OPEN_CLOSE		0x1c
#define L_EVENTTYPE_MUSTER			0x1d
#define L_EVENTTYPE_GENERIC			0x1e	// generic events that have their description pulled from event text
#define L_EVENTTYPE_POS				0x1f	// Point of Sale event type
#define L_EVENTTYPE_PP				0x20	// Portable Programmer event type
#define L_NUMBER_EVENTTYPES			0x21	// count of the number of event types there are
#define LNLMSG_TYPE_EVENT			0
#define LNLMSG_TYPE_STATUS			1
#define LNLMSG_TYPE_VIDEOEVENT		2
#define LNLMSG_TYPE_SHUTDOWNTHREAD	3
#define LNLMSG_TYPE_TEXT_MESSAGE		4
#define LNLMSG_TYPE_ROUTING_EVENT	5
#define L_DEVICETYPE_UNKNOWN				0
#define L_DEVICETYPE_CCTVCAMERA			1
#define L_DEVICETYPE_IVAPP_CCTVCAMERA	2
typedef struct _OLDLNLMESSAGE
    {
    DWORD sl_Size;
    BYTE sb_MessageType;
    DWORD sl_SerialNumber;
    DWORD sl_Time;
    short ss_AccessPanelID;
    short sb_DeviceID;
    short sb_InputDevID;
    BYTE sb_EventType;
    short sb_EventID;
    DWORD sl_InitiatingEventID;
    BYTE sb_IV;
    BYTE sb_InputArg;
    BYTE sb_InternalLUN;
    BYTE sb_InternalZoneID;
    BYTE sb_InternalDeviceID;
    BYTE sb_EventDataType;
    BYTE sb_EventParam;
    DWORD sl_EventParamValue;
    DWORD sl_TransmitterID;
    DWORD sl_TransmitterInputID;
    BYTE sb_DeviceType;
    BYTE sb_AssociatedText;
    DWORD sl_SegmentID;
    union _OLDEVENTDATA su_EventData;
    } 	OLDLNLMESSAGE;

typedef struct _LNLMESSAGE
    {
    DWORD sl_Size;
    BYTE sb_MessageType;
    BYTE sb_EventType;
    BYTE sb_AssociatedText;
    BYTE sb_TotalNumberOfTextMessagesToFollow;
    DWORD sl_SerialNumber;
    DWORD sl_Time;
    DWORD sl_InitiatingEventID;
    DWORD sl_EventParamValue;
    DWORD sl_TransmitterID;
    DWORD sl_TransmitterInputID;
    DWORD sl_SegmentID;
    short ss_AccessPanelID;
    short sb_DeviceID;
    short sb_InputDevID;
    short sb_EventID;
    BYTE sb_IV;
    BYTE sb_InputArg;
    BYTE sb_InternalLUN;
    BYTE sb_InternalZoneID;
    BYTE sb_InternalDeviceID;
    BYTE sb_EventDataType;
    BYTE sb_EventParam;
    BYTE sb_DeviceType;
    union _EVENTDATA su_EventData;
    } 	LNLMESSAGE;

typedef struct _LNLMESSAGE2
    {
    DWORD sl_Size;
    BYTE sb_MessageType;
    BYTE sb_EventType;
    BYTE sb_AssociatedText;
    BYTE sb_TotalNumberOfTextMessagesToFollow;
    DWORD sl_SerialNumber;
    DWORD sl_Time;
    DWORD sl_InitiatingEventID;
    DWORD sl_EventParamValue;
    DWORD sl_TransmitterID;
    DWORD sl_TransmitterInputID;
    DWORD sl_SegmentID;
    short ss_AccessPanelID;
    short sb_DeviceID;
    short sb_InputDevID;
    short sb_EventID;
    BYTE sb_IV;
    BYTE sb_InputArg;
    BYTE sb_InternalLUN;
    BYTE sb_InternalZoneID;
    BYTE sb_InternalDeviceID;
    BYTE sb_EventDataType;
    BYTE sb_EventParam;
    BYTE sb_DeviceType;
    union _EVENTDATA2 su_EventData;
    long sb_UserID;
    } 	LNLMESSAGE2;

#define AAP_RPLYID_EVENT	    0x04	// standard event poll response
#define AAM_RPLYID_EVENT	    0x08	// standard event poll response
#define LOCAL_PRINTOUT_SIZE	16
#define MAX_READERS			64
#define MAX_ALARM_PANELS		32
#define MAX_ALARM_PANELS_NEW	128
typedef struct _LNLDATE
    {
    BYTE sb_Year;
    BYTE sb_Month;
    BYTE sb_Day;
    } 	LNLDATE;

typedef struct _GENERIC_CMD
    {
    long si_PanelID;
    long si_SegmentID;
    __int64 si_ID;
    } 	GENERIC_CMD;

#define MAX_PIN_DIGITS					9
#define MAX_ACCLEVELS_CARDHOLDER			128
#define MAX_ONITY_PIN_DIGITS				4
#define MAX_ONITY_PRIORITY_EVENTS		20
#define MAX_NGP_READERS_PER_DOOR			2
#define MAX_NGP_LCD_SHORTNAME			(16 + 1) // We add 1 for the NULL terminating character
#define SHORT_NGP_LCD_NAME				(12 + 1) // We add 1 for the NULL terminating character
#define MAX_NGP_URL_NAME					(32 + 1) // We add 1 for the NULL terminating character
#define MAX_EQUIPMENT_PSEUDO_POINTS		16
typedef struct _BADGE_EXT
    {
    long sb_AccLevel1;
    long sb_AccLevel2;
    long sb_AccLevel3;
    long sb_AccLevel4;
    long sb_AccLevel5;
    long sb_AccLevel6;
    long sl_FirstRdrs;
    long sl_SecndRdrs;
    long sb_InclList[ 64 ];
    long sl_AccessLevels[ 128 ];
    } 	BADGE_EXT;

typedef struct _BADGE
    {
    long si_PanelID;
    long si_SegmentID;
    DWORD sl_CardNumber;
    long sb_AccessLevel;
    BYTE sb_IssueCode;
    BYTE sb_APBLocation;
    unsigned char sb_PIN[ 9 ];
    LNLDATE ss_ActivationDate;
    LNLDATE ss_DeactivationDate;
    FLAG sb_CheckBeforeDeny;
    FLAG sb_CheckBeforeGrant;
    FLAG sb_APBExempt;
    FLAG sb_APBOneFreePass;
    FLAG sb_APBNotUsed;
    BYTE sb_TimedAPBDelay;
    BYTE sb_UseLimit;
    BYTE sb_CommandAuthority;
    long sl_VDT;
    BYTE sb_PassageMode;
    BYTE sb_DeadboltOverride;
    long sl_AssetGroup;
    BADGE_EXT ss_BdgExts;
    BYTE sb_FirstCardUnlockAuthority;
    } 	BADGE;

typedef struct _BADGE_EXT2
    {
    long sl_FirstRdrs;
    long sl_SecndRdrs;
    long sb_InclList[ 64 ];
    long sl_AccessLevels[ 128 ];
    } 	BADGE_EXT2;

typedef struct _BADGE2
    {
    long si_PanelID;
    long si_SegmentID;
    __int64 sl_CardNumber;
    long sb_AccessLevel;
    long sb_IssueCode;
    BYTE sb_APBLocation;
    unsigned char sb_PIN[ 9 ];
    SYSTEMTIME ss_ActivationDate;
    SYSTEMTIME ss_DeactivationDate;
    FLAG sb_CheckBeforeDeny;
    FLAG sb_CheckBeforeGrant;
    FLAG sb_APBExempt;
    FLAG sb_APBOneFreePass;
    FLAG sb_APBNotUsed;
    BYTE sb_TimedAPBDelay;
    BYTE sb_UseLimit;
    BYTE sb_CommandAuthority;
    long sl_VDT;
    BYTE sb_PassageMode;
    BYTE sb_DeadboltOverride;
    long sl_AssetGroup;
    BADGE_EXT2 ss_BdgExts;
    BYTE sb_FirstCardUnlockAuthority;
    BYTE twoManPersonType;
    BYTE armDisarmCommandAuthority;
    } 	BADGE2;

typedef struct _ACCESS_LEVEL_ENTRY
    {
    long nReaderID;
    long nTimezoneOrFloorID;
    } 	ACCESS_LEVEL_ENTRY;

typedef struct _BADGE3
    {
    long si_PanelID;
    long si_SegmentID;
    __int64 sl_CardNumber;
    long sb_IssueCode;
    WORD nCardType;
    BYTE sb_APBLocation;
    unsigned char sb_PIN[ 9 ];
    SYSTEMTIME ss_ActivationDate;
    SYSTEMTIME ss_DeactivationDate;
    FLAG sb_CheckBeforeDeny;
    FLAG sb_CheckBeforeGrant;
    FLAG sb_APBExempt;
    FLAG sb_APBOneFreePass;
    FLAG sb_APBNotUsed;
    BYTE sb_TimedAPBDelay;
    BYTE sb_UseLimit;
    BYTE sb_CommandAuthority;
    long sl_VDT;
    BYTE sb_PassageMode;
    BYTE sb_DeadboltOverride;
    long sl_AssetGroup;
    BYTE sb_FirstCardUnlockAuthority;
    BYTE twoManPersonType;
    BYTE armDisarmCommandAuthority;
    long sl_AccessLevels[ 128 ];
    BYTE nPrecAccessType;
    BYTE sb_PendingEnrollment;
    BYTE sb_ValidationForSecondary;
    long nUserId;
    wchar_t userName[ 13 ];
    short nLanguage;
    long nPrecAccessLevels;
    ACCESS_LEVEL_ENTRY *pPrecAccessLevels;
    long nOwnedReaders;
    long *pOwnedReaderIds;
    } 	BADGE3;

#define MAX_BADGES_PER_CMD_DEF		41
#define MAX_BADGES_PER_CMD_ISSUE		41
#define MAX_BADGES_PER_CMD_EXCL		33
#define MAX_BADGES_PER_CMD_INCL		11
#define MAX_BADGES_PER_CMD_TAPB		38
#define MAX_BADGES_PER_CMD_ISS_TAPB	35
#define MAX_BADGES_PER_CMD_LINX		25
typedef struct _BULKBADGE
    {
    long si_PanelID;
    long si_NumBadges;
    BADGE ss_Badges[ 41 ];
    } 	BULKBADGE;

typedef struct _BULKBADGE2
    {
    long si_PanelID;
    long si_NumBadges;
    BADGE2 ss_Badges[ 41 ];
    } 	BULKBADGE2;

typedef struct _BULKBADGE3
    {
    long si_PanelID;
    long si_NumBadges;
    BADGE3 ss_Badges[ 41 ];
    } 	BULKBADGE3;

#define REC_EXT_SIXACCLEVELS			1
#define REC_EXT_SIXACC_RDREXCL		2
#define REC_EXT_SIXACC_2ISSUECD		5
#define REC_EXT_SIXACC_RDRLIST		8
#define REC_EXT_SIXACC_TIMEDAPB		10
#define REC_EXT_SIXACC_TIMEDAPB_2IS	11
#define AAM_OPTION_USELIMIT			0x81
#define AAM_OPTION_AREAAPB			0x82
#define LNL_BIO_TYPE_RSI_HANDKEYII				101
#define LNL_BIO_TYPE_IDENTIX_FINGERSCAN_V20		102
#define LNL_BIO_TYPE_BIOSCRYPT_VFLEX				103
#define LNL_BIOMETRIC_TEMPLATE_SIZE				2000
#define LNL_MAX_BIO_TYPES_PER_PANEL			5
typedef struct _BIOMETRIC_DEF
    {
    long NumBioRecords;
    long BioType;
    long Flags;
    long MinScoreDflt;
    long MinScore;
    long TemplateSize;
    } 	BIOMETRIC_DEF;

typedef struct _ADD_BIOMETRIC_DEF
    {
    long si_PanelID;
    long si_SegmentID;
    BIOMETRIC_DEF biometricDef;
    } 	ADD_BIOMETRIC_DEF;

typedef struct _EXTENDED_HELD_DEF
    {
    unsigned char CommandCode[ 6 ];
    long MinExtendedHeldTime;
    long MaxExtendedHeldTime;
    long PreAlarmTime;
    } 	EXTENDED_HELD_DEF;

typedef struct _ADD_EXTENDED_HELD_DEF
    {
    long si_PanelID;
    long si_SegmentID;
    EXTENDED_HELD_DEF extendedHeldDef;
    } 	ADD_EXTENDED_HELD_DEF;

#define MAX_USER_COMMAND_MACROS 20
#define COMMAND_MACRO_LENGTH 16
#define KEYPAD_DISPLAY_LENGTH	16
#define MAX_KEYPAD_STRINGS		8
typedef struct _COMMAND_MACRO
    {
    short type;
    short keySpec;
    unsigned char macro[ 16 ];
    } 	COMMAND_MACRO;

typedef struct _USER_COMMAND_MACROS
    {
    long panelID;
    short readerNumber;
    short numMacros;
    COMMAND_MACRO macroList[ 20 ];
    } 	USER_COMMAND_MACROS;

typedef struct _KEYPAD_DISPLAY_STRING
    {
    unsigned char displayString[ 16 ];
    } 	KEYPAD_DISPLAY_STRING;

typedef struct _KEYPAD_DISPLAY
    {
    long panelID;
    short readerNumber;
    short keypadDisplaySpec;
    KEYPAD_DISPLAY_STRING stringArray[ 8 ];
    } 	KEYPAD_DISPLAY;

typedef struct _CONTROLLER_ENCRTYPTION_CFG
    {
    long si_PanelID;
    int si_EncryptionType;
    int si_ActiveMasterKey;
    int si_AllowAutomaticEncryptionDowngrade;
    long si_SegmentID;
    BYTE sb_EncryptedConnection;
    BYTE sb_NextConnectionDowngradable;
    BYTE sb_MasterKeyUpdatePending;
    int si_ConnectUsingPreviousMasterKey;
    } 	CONTROLLER_ENCRTYPTION_CFG;

typedef struct _FIPS_MODE_CONTROLLER_ENCRTYPTION_PARAMS
    {
    BYTE sb_FipsModeFlag;
    BYTE sb_ControllerBypassFlag;
    int si_ActiveMasterKeyNumber;
    } 	FIPS_MODE_CONTROLLER_ENCRTYPTION_PARAMS;

typedef struct _CARD_REC_CFG
    {
    long si_PanelID;
    DWORD sl_NumberCards;
    BYTE sb_CardType;
    BYTE sb_PinType;
    BYTE sb_ExtType;
    BYTE sb_Duress;
    FLAG sb_ExpDate;
    FLAG sb_ActDate;
    FLAG sb_APB;
    FLAG sb_PINSearch;
    FLAG sb_TimedAPB;
    FLAG sb_PrecisionAccess;
    FLAG sb_AssetManagement;
    FLAG sb_ElevatorControl;
    FLAG sb_FirstCardUnlock;
    BYTE sb_NumReaders;
    long sl_SegmentID;
    long sl_TotalMemory;
    long sl_SegmentConverter;
    long sl_NumAccessLevels;
    long sl_AccessLevelsPerCardholder;
    long sl_NumTimezones;
    long sl_NumHolidays;
    long sl_NumAssets;
    long sl_AssetSize;
    long sl_NumCardsPerCardholder;
    BIOMETRIC_DEF biometricCfg[ 5 ];
    } 	CARD_REC_CFG;

#define MAX_ISSUECODEDIGITS			2
#define MAX_FACCODEDIGITS			9
#define MAX_CARDDIGITS				9
#define MAG_CARD_FORMAT				0x0000
#define TWO_DIGITISSUECODE_BIT		0x0040
#define WEIGAND_CARD_FORMAT			0x0080
#define SMARTCARD_FORMAT				0x0120
typedef struct _CARDFMT_MAGNETIC
    {
    BYTE sb_RefIndex1;
    BYTE sb_RefIndex2;
    BYTE sb_DistToIndex1;
    BYTE sb_DistToIndex2;
    BYTE sb_DistToEnd;
    BYTE sb_FacFromStart;
    BYTE sb_CardFromStart;
    BYTE sb_IssueFromStart;
    BYTE sb_FacOffset;
    BYTE sb_CardOffset;
    BYTE sb_IssueOffset;
    BYTE sb_FacDigits;
    BYTE sb_CardDigits;
    BYTE sb_IssueDigits;
    BYTE sb_Spare1;
    BYTE sb_Spare2;
    } 	CARDFMT_MAGNETIC;

typedef struct _CARDFMT_WEIGAND
    {
    BYTE sb_NumBitsOnCard;
    BYTE sb_BitsForEven;
    BYTE sb_BitsForOdd;
    BYTE sb_BitsFacCode;
    BYTE sb_IndexToFirstFac;
    BYTE sb_BitsCardNum;
    BYTE sb_IndexToFirstCard;
    BYTE sb_ParityStep;
    short sb_IndexToFirstIssue;
    short sb_BitsIssueCode;
    } 	CARDFMT_WEIGAND;

typedef /* [switch_type] */ /* [switch_type] */ union _CARD_DATA
    {
    CARDFMT_MAGNETIC ss_Mag;
    CARDFMT_WEIGAND ss_Weigand;
    } 	CARD_DATA;

typedef struct _CARD_FORMAT_CFG
    {
    long si_PanelID;
    long si_SegmentID;
    FLAG sb_FormatType;
    short si_FormatID;
    DWORD sl_FacilityCode;
    DWORD sl_CardNumberOffset;
    FLAG sb_EnforceGuestRules;
    FLAG sb_SmartCard;
    union _CARD_DATA su_CardData;
    } 	CARD_FORMAT_CFG;

#define CARD_FORMAT_TYPE_MAGNETIC		1
#define CARD_FORMAT_TYPE_WIEGAND			2
#define CARD_FORMAT_TYPE_NONSTOP_ID		3
#define CARD_FORMAT_TYPE_SCARD			4
#define CARD_FORMAT_FLAGS_DURESS			0x0001
#define CARD_FORMAT_FLAGS_REVERSED		0x0002
#define CARD_FORMAT_FLAGS_GUEST			0x0004
typedef struct _CARDFMT_WIEGAND2
    {
    WORD sb_NumBitsOnCard;
    WORD sb_BitsForEven;
    WORD sb_BitsForOdd;
    WORD sb_ParityStep;
    WORD sb_IndexToFirstFac;
    WORD sb_BitsFacCode;
    WORD sb_IndexToFirstCard;
    WORD sb_BitsCardNum;
    WORD sb_BitsIssueCode;
    WORD sb_IndexToFirstIssue;
    WORD sb_AdaBitNo;
    WORD sb_AdaBits;
    WORD sb_ActivateDateBitNo;
    WORD sb_ActivateDateBits;
    WORD sb_DeactivateDateBitNo;
    WORD sb_DeactivateDateBits;
    WORD sb_AuthorizationBitNo;
    WORD sb_AuthorizationBits;
    } 	CARDFMT_WIEGAND2;

typedef struct _CARDFMT_SCARD_LENEL_ICLASS
    {
    BYTE ApplicationKey[ 8 ];
    WORD nLocationBook;
    WORD nLocationPage;
    WORD nLocationApp;
    } 	CARDFMT_SCARD_LENEL_ICLASS;

typedef struct _CARDFMT_SCARD_CSN
    {
    WORD nCardType;
    } 	CARDFMT_SCARD_CSN;

typedef /* [switch_type] */ /* [switch_type] */ union _CARD_DATA2
    {
    CARDFMT_MAGNETIC Magnetic;
    CARDFMT_WIEGAND2 Wiegand;
    CARDFMT_SCARD_LENEL_ICLASS LenelIClass;
    CARDFMT_SCARD_CSN CSN;
     /* Empty union arm */ 
    } 	CARD_DATA2;

typedef struct _CARD_FORMAT_CFG2
    {
    long nPanelID;
    long nSegmentID;
    long nFormatID;
    WORD nType;
    DWORD nFlags;
    WORD nScardAppType;
    DWORD nFacilityCode;
    DWORD nCardNumberOffset;
    union _CARD_DATA2 CardData;
    } 	CARD_FORMAT_CFG2;

#define MAX_HOLIDAYS	255
typedef struct _HOLIDAY
    {
    short ss_Year;
    short ss_Month;
    short ss_DayOfMonth;
    short ss_HolidayType;
    short ss_NumDays;
    } 	HOLIDAY;

typedef struct _HOLIDAYMSG
    {
    long si_PanelID;
    long si_SegmentID;
    short ss_NumHolidays;
    HOLIDAY ss_Holidays[ 255 ];
    } 	HOLIDAYMSG;

typedef struct TRecxLHolidayTag
    {
    BYTE w8Defined;
    BYTE w8HolidayNum;
    BYTE w8StartMonth;
    BYTE w8Type;
    BYTE w8Range;
    BYTE w8StartDay;
    struct 
        {
        BYTE w8Month;
        BYTE w8Day;
        } 	RecEndDate;
    WORD wNumDays;
    } 	TRecxLHoliday;

typedef struct _HOLIDAYMSG2
    {
    long si_PanelID;
    long si_SegmentID;
    short ss_NumHolidays;
    HOLIDAY ss_Holidays[ 255 ];
    TRecxLHoliday xLHoliday;
    } 	HOLIDAYMSG2;

#define MIN_TZ_NUM			3	// minimum allowable timezone
#define MAX_TZ_NUM			255	// max allowable timezone number
#define MAX_TZ_INTERVALS	    6	// six timezones maximum
#define TIMEZONE_TYPE_FLAG_ONITY_INTEGRA	0x0001
#define TIMEZONE_TYPE_FLAG_ONITY_ILS		0x0002
typedef struct _TZ_INTERVAL
    {
    BYTE sb_StartHour;
    BYTE sb_StartMin;
    BYTE sb_EndHour;
    BYTE sb_EndMin;
    BYTE sb_DaysOfWeek;
    BYTE sb_Holidays;
    } 	TZ_INTERVAL;

typedef struct _TIMEZONE
    {
    long si_PanelID;
    long si_SegmentID;
    long sb_TzNum;
    BYTE sb_NumIntervals;
    TZ_INTERVAL ss_Intervals[ 6 ];
    } 	TIMEZONE;

typedef struct _T_WINDOW
    {
    WORD sw_Open;
    WORD sw_Close;
    BYTE sb_WeekdayMap;
    } 	T_WINDOW;

typedef struct _TIMEZONE2
    {
    long si_PanelID;
    long si_SegmentID;
    long sb_TzNum;
    WORD si_TypeFlags;
    BYTE sb_NumIntervals;
    TZ_INTERVAL ss_Intervals[ 6 ];
    BYTE sb_Defined;
    BYTE sb_ScheduleNum;
    T_WINDOW ss_Windows[ 6 ];
    BYTE sb_HolidaySchedule[ 3 ];
    BYTE sb_MidnightHolidayMode;
    } 	TIMEZONE2;

#define MAX_ACCESSLEVELS	255
#define MAX_ELEVATORACCESSLEVELS	127
#define MAX_LINXACCESSLEVELS		3072
typedef struct _ACCESS_LEVEL
    {
    long si_PanelID;
    long si_SegmentID;
    long sb_AccessLevel;
    BYTE sb_AccProcess;
    long sb_ReaderTZ[ 64 ];
    } 	ACCESS_LEVEL;

typedef struct _ACCESS_LEVEL2
    {
    long nPanelID;
    long nSegmentID;
    long nAccessLevelID;
    long nEntries;
    ACCESS_LEVEL_ENTRY *pEntries;
    } 	ACCESS_LEVEL2;

typedef struct _EXTENDED_ACCESS_LEVEL
    {
    long si_PanelID;
    long si_SegmentID;
    long si_AccessLevelID;
    short sb_EscortCode;
    SYSTEMTIME sb_ActivationDateTime;
    SYSTEMTIME sb_DeactivationDateTime;
    } 	EXTENDED_ACCESS_LEVEL;

typedef struct _DWN_ACCESS_LEVEL
    {
    long si_PanelID;
    long sb_AccessLevel;
    BYTE sb_DwnReaders[ 64 ];
    } 	DWN_ACCESS_LEVEL;

typedef struct _CMD_ACCESS_LEVEL
    {
    long si_PanelID;
    long sb_AccessLevel;
    BYTE sb_DwnReaders[ 64 ];
    } 	CMD_ACCESS_LEVEL;

typedef struct _READER_DOWNLOAD
    {
    long si_PanelID;
    long si_NumReaders;
    BYTE sb_Readers[ 64 ];
    } 	READER_DOWNLOAD;

typedef struct _READER_ID_ARRAY
    {
    long nPanelID;
    long nAccessLevelID;
    long nReaders;
    long *pReaderIDs;
    } 	READER_ID_ARRAY;

#define ACC_MODE_LOCKED					0
#define ACC_MODE_CARDONLY				1
#define ACC_MODE_PIN_OR_CARD				2
#define ACC_MODE_PIN_AND_CARD			3
#define ACC_MODE_UNLOCKED				4
#define ACC_MODE_FACCODE_ONLY			5
#define ACC_MODE_CYPHERLOCK				6
#define ACC_MODE_AUTOMATIC				7
#define ACC_MODE_FIRST_CARD_UNLOCK		8
#define ACC_MODE_DISABLE_2CARD			16
#define ACC_MODE_ENABLE_2CARD			17
#define ACC_MODE_DISABLE_BIO_VERIFY		18
#define ACC_MODE_ENABLE_BIO_VERIFY		19
#define ACC_MODE_DISABLE_BIO_ENROLL		20
#define ACC_MODE_ENABLE_BIO_ENROLL		21
#define ACC_MODE_DISABLE_FIRST_CARD_UNLOCK	24
#define ACC_MODE_ENABLE_FIRST_CARD_UNLOCK	25
#define ACC_MODE_PIN_AND_CARD_UNLOCKED		26
#define ACC_MODE_BLOCKED						27
#define ACC_MODE_SECURED						28
#define ACC_MODE_UNSECURED					29
#define ACC_MODE_PIN_ONLY					30
#define ACC_MODE_LOCK_READER					31
#define ACC_MODE_CIPHER_OR_CARD				32
#define ACC_MODE_DEFAULT_READER_MODE			100
#define ACC_MODE_UNKNOWN						255
typedef struct _READER_MODE
    {
    long si_PanelID;
    short sb_ReaderNumber;
    BYTE sb_AccessMode;
    } 	READER_MODE;

#define RDR_OUTMODE_OFF			0x08
#define RDR_OUTMODE_ON			0x09
#define RDR_OUTMODE_PULSE		0x80
#define RDR_OUTMODE_IGNORE		0x00
typedef struct _READER_OUTPUT_CTRL
    {
    long si_PanelID;
    short sb_ReaderNumber;
    BYTE sb_Output1;
    BYTE sb_Output2;
    } 	READER_OUTPUT_CTRL;

#define RDR_ALARM_MASKOFF		0x00
#define RDR_ALARM_MASKON		    0x01
typedef struct _READER_ALARM_MASK
    {
    long si_PanelID;
    short sb_ReaderNumber;
    BYTE sb_ForcedOpen;
    BYTE sb_HeldOpen;
    BYTE sb_AuxAlarm;
    BYTE sb_AuxAlarm2;
    BYTE sb_AuxAlarm3;
    } 	READER_ALARM_MASK;

// controller types for Readers
#define CTRL_TYPE_UNDEFINED		0
#define CTRL_TYPE_AMS103_102		1
#define CTRL_TYPE_DIA100			3
#define CTRL_TYPE_AIA100			4
#define CTRL_TYPE_AMS100			6
#define CTRL_TYPE_AP510_520		8
#define CTRL_TYPE_AP500			9
#define CTRL_TYPE_2RDR_BIOSCRYPT_RDR			0x10
#define CTRL_TYPE_SLV_BIOSCRYPT_RDR			0x11
#define CTRL_TYPE_SINGLE_BIOSCRYPT_RDR		0x12
#define CTRL_TYPE_2RDR_OSDP_RDR				0x13
#define CTRL_TYPE_SLV_OSDP_RDR				0x14
#define CTRL_TYPE_SINGLE_OSDP_RDR			0x15
#define CTRL_TYPE_WEIGAND_RDR				0x70
#define CTRL_TYPE_MAG_W_WEIGAND_OUT			0x71
#define CTRL_TYPE_2RDR_INTERFACE				0x72
#define CTRL_TYPE_2RDR_WEIGAND_RDR			0x73
#define CTRL_TYPE_2RDR_MAG_W_WEIGAND_OUT		0x74
#define CTRL_TYPE_SLV_INTERFACE				0x75
#define CTRL_TYPE_SLV_WEIGAND_RDR			0x76
#define CTRL_TYPE_SLV_MAG_W_WEIGAND_OUT		0x77
#define CTRL_TYPE_2SDI6_INTERFACE			0x78
#define CTRL_TYPE_2SDI6_WEIGAND_RDR			0x79
#define CTRL_TYPE_2SDI6_MAG_W_WEIGAND_OUT	0x7A
#define CTRL_TYPE_SLVSDI6_INTERFACE			0x7B
#define CTRL_TYPE_SLVSDI6_WEIGAND_RDR		0x7C
#define CTRL_TYPE_SLVSDI6_MAG_W_WEIGAND_OUT	0x7D
#define CTRL_TYPE_2RDR_F2F_RDR				0x7E
#define CTRL_TYPE_SLV_F2F_RDR				0x7F
#define CTRL_TYPE_F2F_RDR					0x80
#define CTRL_TYPE_MR16IN			0x81
#define CTRL_TYPE_MR16OUT		0x82
#define CTRL_TYPE_SDI12IN		0x83
#define CTRL_TYPE_2SDI4_INTERFACE				0x84
#define CTRL_TYPE_2SDI4_WEIGAND_RDR				0x85
#define CTRL_TYPE_2SDI4_MAG_W_WEIGAND_OUT		0x86
#define CTRL_TYPE_SLVSDI4_INTERFACE				0x87
#define CTRL_TYPE_SLVSDI4_WEIGAND_RDR			0x88
#define CTRL_TYPE_SLVSDI4_MAG_W_WEIGAND_OUT		0x89
#define CTRL_TYPE_DMPKEYPAD_INTERFACE			0x90
#define CTRL_TYPE_HID_GATEWAY_INTERFACE			0x91
#define CTRL_TYPE_BIO_RSI_GATEWAY_INTERFACE		0x94
#define CTRL_TYPE_BEST_OFFLINE					0x92
#define CTRL_TYPE_BEST_OFFLINE_GUEST				0x93
#define CTRL_TYPE_BEST_GUEST						0x95
#define CTRL_TYPE_BIO_IDENTIX_FINGERSCAN_V20		0x96
#define CTRL_TYPE_RS485_LCD_KEYPAD_ALL_OTHER				0x97
#define CTRL_TYPE_RS485_LCD_KEYPAD_WEIGAND_RDR			0x98
#define CTRL_TYPE_RS485_LCD_KEYPAD_MAG_W_WEIGAND_OUT		0x99
#define CTRL_TYPE_BIO_BIOSCRYPT_VFLEX					0x9A
#define CTRL_TYPE_RECOGNITION_SOURCE_WEIGAND_RDR			0x9B
#define CTRL_TYPE_RECOGNITION_SOURCE_MAG_RDR			0x9D
#define CTRL_TYPE_GENERIC_READER						0x9C
#define CTRL_TYPE_2220_2RDR_INTERFACE			0x9E
#define CTRL_TYPE_2220_2RDR_WEIGAND_RDR			0x9F
#define CTRL_TYPE_2220_2RDR_MAG_W_WEIGAND_OUT	0x20
#define CTRL_TYPE_2220_2RDR_F2F_RDR				0x21
#define CTRL_TYPE_2220_2RDR_BIOSCRYPT_RDR		0x22
#define CTRL_TYPE_2220_2RDR_OSDP_RDR				0x29
#define CTRL_TYPE_2220_SLV_INTERFACE				0x23
#define CTRL_TYPE_2220_SLV_WEIGAND_RDR			0x24
#define CTRL_TYPE_2220_SLV_MAG_W_WEIGAND_OUT		0x25
#define CTRL_TYPE_2220_SLV_F2F_RDR				0x26
#define CTRL_TYPE_2220_SLV_BIOSCRYPT_RDR			0x27
#define CTRL_TYPE_2220_SLV_OSDP_RDR				0x28
#define CTRL_TYPE_HID_WIEGAND_RDR1_INTERFACE		0x30
#define CTRL_TYPE_HID_CLOCK_DATA_RDR1_INTERFACE	0x31
#define CTRL_TYPE_HID_WIEGAND_RDR2_INTERFACE		0x32
#define CTRL_TYPE_HID_CLOCK_DATA_RDR2_INTERFACE	0x33
#define CTRL_TYPE_ONITY_LOCK						52
#define CTRL_TYPE_ONITY_ILS_LOCK_FIRST			53
#define CTRL_TYPE_ONITY_ILS_LOCK_MAGNETIC		53
#define CTRL_TYPE_ONITY_ILS_LOCK_ICLASS			54
#define CTRL_TYPE_ONITY_ILS_LOCK_PROX			55
#define CTRL_TYPE_ONITY_ILS_LOCK_LAST			61
//Door types for Guardall (Should be continuous with readers)
#define CTRL_TYPE_NGP_ONBOARD_DOOR1				0x3E // 62
#define CTRL_TYPE_NGP_ONBOARD_DOOR2				0x3F
#define CTRL_TYPE_NGP_OUT_READER					0x40
#define CTRL_TYPE_NGP_DUAL_INTF_DOOR1			0x41
#define CTRL_TYPE_NGP_DUAL_INTF_DOOR2			0x42
#define CTRL_TYPE_NGP_SINGLE_INTF_DOOR			0x43
#define CTRL_TYPE_2210_2RDR_INTERFACE			0x44
#define CTRL_TYPE_2210_2RDR_WEIGAND_RDR			0x45
#define CTRL_TYPE_2210_2RDR_MAG_W_WEIGAND_OUT	0x46
#define CTRL_TYPE_2210_2RDR_F2F_RDR				0x47
#define CTRL_TYPE_2210_2RDR_BIOSCRYPT_RDR		0x48
#define CTRL_TYPE_2210_2RDR_OSDP_RDR				0x49
#define CTRL_TYPE_2210_SLV_INTERFACE				0x4A
#define CTRL_TYPE_2210_SLV_WEIGAND_RDR			0x4B
#define CTRL_TYPE_2210_SLV_MAG_W_WEIGAND_OUT		0x4C
#define CTRL_TYPE_2210_SLV_F2F_RDR				0x4D
#define CTRL_TYPE_2210_SLV_BIOSCRYPT_RDR			0x4E
#define CTRL_TYPE_2210_SLV_OSDP_RDR				0x4F
//NGP IN Reader
#define CTRL_TYPE_NGP_IN_READER				    0x50

// controller ID's for readers
#define CTRL_ID_UNDEFINED		0x00
#define CTRL_ID_AMS103_102		0x67
#define CTRL_ID_DIA100			0x04
#define CTRL_ID_AIA100			0x04
#define CTRL_ID_AMS100			0x06
#define CTRL_ID_AP510_520		0x08
#define CTRL_ID_AP500			0x09
#define MAX_FUNCS_READER	7
#define MAX_INSTANT_CMDS_READER	7
#define CTRL_TYPE_PYRO_GENERIC				0x120
#define CTRL_TYPE_PYRO_MMB_1					0x121
#define CTRL_TYPE_PYRO_MKB_1					0x122
#define CTRL_TYPE_PYRO_XLD_1					0x123
#define CTRL_TYPE_PYRO_ALD_2					0x124
#define CTRL_TYPE_PYRO_CZM_4					0x125
#define CTRL_TYPE_PYRO_CRM_4					0x127
#define CTRL_TYPE_PYRO_CSM_4					0x129
#define CTRL_TYPE_PYRO_ANN_1					0x12B
#define CTRL_TYPE_PYRO_PSR_1					0x12F
#define CTRL_TYPE_PYRO_MOI_1					0x130
#define CTRL_TYPE_PYRO_CMI_300				0x132
#define CTRL_TYPE_PYRO_ALD_2I				0x133
#define CTRL_TYPE_PYRO_NET_7					0x134
#define CTRL_TYPE_PYRO_OCC_1					0x135
#define CTRL_TYPE_PYRO_ACM_1					0x136
#define CTRL_TYPE_PYRO_ACM_1_X				0x137
#define CTRL_TYPE_PYRO_ACM1_MR				0x138
#define CTRL_TYPE_PYRO_DMC_1					0x139
#define CTRL_TYPE_PYRO_NIM_1					0x13A
#define CTRL_TYPE_PYRO_NIM_FSI				0x13B
#define CTRL_TYPE_PYRO_PSEUDO_MOD			0x13C
#define CTRL_TYPE_PYRO_CSGM					0x13E
#define CTRL_TYPE_VISONICS_GENERIC				0xA0
#define CTRL_TYPE_SI540							0xA1
#define CTRL_TYPE_SI544							0xA2
#define CTRL_TYPE_SI561							0xA3
#define CTRL_TYPE_SLC5							0xA4
#define CTRL_TYPE_SR500							0xA5
#define CTRL_TYPE_SR520							0xA6
#define CTRL_TYPE_SR521							0xA7
#define CTRL_TYPE_SR522							0xA8
#define CTRL_TYPE_SRP50							0xA9
#define CTRL_TYPE_SRP51							0xAA
#define CTRL_TYPE_SVC580							0xAB
#define CTRL_TYPE_WR200_PAS						0xAC
#define CTRL_TYPE_SCP522							0xAD
#define CTRL_TYPE_LDVS_INPUT_PANEL				0xAE
#define CTRL_TYPE_LNVR_PANEL						0xAF
#define CTRL_TYPE_NGP_ALARM_PANEL				0xB2
#define CTRL_TYPE_NGP_LCD_KEYPAD					0xB3
#define CTRL_TYPE_NGP_POWER_SUPPLY				0xB4
#define CTRL_TYPE_NGP_ONBOARD_ALARM_PANEL		0xB5
#define CTRL_TYPE_GENERIC_OUTPUT_PANEL			0xFE
#define CTRL_TYPE_GENERIC_INPUT_PANEL			0xFF
// Transmitter Types
#define TRANSMITTER_TYPE_UNKNOWN			0x0000
// Portable Transmitters
#define TRANSMITTER_TYPE_PORTABLE		0x1000
#define TRANSMITTER_TYPE_MCT_101_S		0x1001
#define TRANSMITTER_TYPE_MCT_102_S		0x1002
#define TRANSMITTER_TYPE_MCT_104_S		0x1003
#define TRANSMITTER_TYPE_MCT_201_S		0x1004
#define TRANSMITTER_TYPE_MCT_211_S		0x1005
#define TRANSMITTER_TYPE_MCT_IR_201_S	0x1006
#define TRANSMITTER_TYPE_WT_101_S		0x1007
#define TRANSMITTER_TYPE_WT_102_S		0x1008
#define TRANSMITTER_TYPE_WT_211_S		0x1009
#define TRANSMITTER_TYPE_WT_IR_201_S		0x100A
#define TRANSMITTER_TYPE_WT_201_S		0x100B
#define TRANSMITTER_TYPE_WT_104_S		0x100C	// Not supported currently
#define TRANSMITTER_TYPE_MCT_IR_252_WPS	0x100D
// Fixed Transmitters
#define TRANSMITTER_TYPE_FIXED			0x2000
#define TRANSMITTER_TYPE_MCD_1000_S		0x2001	// Not supported currently
#define TRANSMITTER_TYPE_MCPIR_2000_S	0x2002
#define TRANSMITTER_TYPE_MCPIR_3000_S	0x2003
#define TRANSMITTER_TYPE_MCT_100_S		0x2004
#define TRANSMITTER_TYPE_MCT_302_S		0x2005
#define TRANSMITTER_TYPE_WST_400_S		0x2006	// Not supported currently
#define TRANSMITTER_TYPE_WT_301_S		0x2007
#define TRANSMITTER_TYPE_SRN_2000_WS		0x2008	// Not supported currently
#define TRANSMITTER_TYPE_SPD_1000		0x2009	// Not supported currently
#define TRANSMITTER_TYPE_SPD_2000		0x200A
#define TRANSMITTER_TYPE_SPD_3000		0x200B	// Not supported currently
#define TRANSMITTER_TYPE_WT_100_S		0x200C	// Not supported currently
#define TRANSMITTER_TYPE_MCT_423			0x200D
#define TRANSMITTER_TYPE_MCT_501_S		0x200E
#define TRANSMITTER_TYPE_MCT_425_S		0x200F
// Man Down Transmitters
#define TRANSMITTER_TYPE_MANDOWN			0x3000
#define TRANSMITTER_TYPE_MCT_101_MDS		0x3001
#define TRANSMITTER_TYPE_MDT_122_S		0x3002
#define KEYPAD_MODE_NONE		0
#define KEYPAD_MODE_STANDARD	1
#define KEYPAD_MODE_HUGHES	2
#define KEYPAD_MODE_INDALA	3
#define KEYPAD_MODE_NOTAMPER	4
#define KEYPAD_MODE_APOLLO   5
#define STRIKE_MODE_CLOSE		0
#define STRIKE_MODE_OPEN		1
typedef struct _ELEVATOR_HW_INFO
    {
    long si_AccessPanelID;
    long si_ReaderID;
    long si_AlarmPanelID;
    long si_PanelType;
    long si_FloorNumber;
    long si_NextPanelID;
    } 	ELEVATOR_HW_INFO;

// Defines for the READERDEF structure
#define READERDEF_READERNAME_LEN		64
#define READERDEF_MAX_CARD_FORMATS	8
#define ALTERNATE_READER_NONE		0
#define ALTERNATE_READER_NORMAL		1
#define ALTERNATE_READER_BIO1		2
typedef struct _INTRUSION_CMD_CONFIG
    {
    long accessTimeOut;
    long authorityLevel0;
    long authorityLevel1;
    long authorityLevel2;
    } 	INTRUSION_CMD_CONFIG;

typedef struct _INTRUSION_CMD_CONFIG2
    {
    long accessTimeOut;
    long authorityLevel0;
    long authorityLevel1;
    long authorityLevel2;
    short nDisplaySpec;
    } 	INTRUSION_CMD_CONFIG2;

typedef struct _READERDEF
    {
    long si_PanelID;
    short sb_ReaderNumber;
    BYTE sb_Activate;
    BYTE sb_AAPPort;
    BYTE sb_CommAddress;
    BYTE sb_CtrlType;
    BYTE sb_StrikeTime;
    long sb_OpenTime;
    BYTE sb_OfflineMode;
    BYTE sb_AccessMode;
    BYTE sb_DenyIfDuress;
    BYTE sb_ElevatorReader;
    BYTE sb_DoNotWait;
    BYTE sb_Downloadable;
    BYTE sb_CardSpecification;
    BYTE sb_PairedMaster;
    BYTE sb_PairedSlave;
    BYTE sb_UseAuxAsBolt;
    BYTE sb_FastBoltAlarm;
    BYTE sb_AcceptCmds;
    BYTE sb_PreAlarm;
    BYTE sb_2CardCtrl;
    long sb_CardFormat[ 8 ];
    BYTE sb_DiddleCnt;
    short sb_AreaLeaving;
    short sb_AreaEntering;
    long sb_ForcedOpenTZ;
    long sb_HeldOpenTZ;
    long sb_AuxInputTZ;
    long sb_Output1TZ;
    long sb_Output2TZ;
    BYTE sb_Output1State;
    BYTE sb_Output2State;
    BYTE sb_MaskForcedOpen;
    BYTE sb_MaskHeldOpen;
    BYTE sb_MaskAuxAlarm;
    BYTE sb_MaskAuxAlarm2;
    long sb_DayModeTZ;
    long sl_DayFloors;
    long sl_FacCodeFloors;
    long sl_CyperCode;
    BYTE sb_TimedAPB;
    BYTE sb_SoftAPB;
    long sb_LogGrantTZ;
    long sb_LogDenyTZ;
    long sb_LogStatusTZ;
    BYTE sb_DecUseLimit;
    BYTE sb_NoRexOnStrike;
    long sl_LINXAddr;
    long sl_Out1Pulse;
    long sl_Out2Pulse;
    long sb_AuxInput2TZ;
    long sb_AuxInput3TZ;
    long sb_ElevatorAccessLevel;
    BYTE sb_SlaveID;
    BYTE sb_MaskAuxAlarm3;
    BYTE sb_FuncList[ 7 ];
    BYTE sb_TermList[ 7 ];
    BYTE sb_KeypadMode;
    BYTE sb_StrikeMode;
    BYTE sb_TwoWireLED;
    long sb_StrikeTime2;
    long sb_OpenTime2;
    long sl_DCSupervision;
    long sl_REXSupervision;
    long sl_Aux1Supervision;
    long sl_Aux2Supervision;
    long sl_Aux1Latched;
    long sl_Aux1EntryDelay;
    long sl_Aux1ExitDelay;
    long sl_Aux2Latched;
    long sl_Aux2EntryDelay;
    long sl_Aux2ExitDelay;
    ELEVATOR_HW_INFO sl_ElevatorInputID[ 8 ];
    ELEVATOR_HW_INFO sl_ElevatorOutputID[ 8 ];
    long sl_TrackElevatorInputRequests;
    long sl_OnlineFacEAL;
    BYTE sb_ChassisType;
    long sl_DeniedAttemptsCntTO;
    wchar_t sl_ReaderName[ 64 ];
    BYTE sb_DisableAsset;
    BYTE sb_LookAhead;
    BYTE sb_LookAheadOffset;
    BYTE sb_LookAheadRange;
    long sl_Chassis_Volume;
    long sl_ReaderMemory;
    BYTE sb_UseActivationDate;
    BYTE sb_UseExpirationDate;
    long sl_NumCardholders;
    long sl_FirstBadgeNumber;
    long sl_NumberOfBadges;
    long sl_altrdr_sio;
    long sl_altrdr_number;
    long sl_altrdr_spec;
    BYTE sb_AlternateReader;
    BYTE sb_BioVerify;
    BYTE sb_BioEnroll;
    BYTE sb_HostDecisionOnGrant;
    BYTE sb_HostDecisionProceedWithGrant;
    BYTE sb_GlobalAPB;
    long sl_AuxIn1HoldTime;
    long sl_AuxIn2HoldTime;
    long sl_DCDebounce;
    long sl_REXDebounce;
    long sl_AuxIn1Debounce;
    long sl_AuxIn2Debounce;
    BYTE sb_CipherMode;
    BYTE sb_FirstCardUnlock;
    BYTE sb_FirstCardUnlockAuthorityRequired;
    } 	READERDEF;

typedef struct _READERDEF2
    {
    long si_PanelID;
    short sb_ReaderNumber;
    BYTE sb_Activate;
    BYTE sb_AAPPort;
    BYTE sb_CommAddress;
    BYTE sb_CtrlType;
    BYTE sb_StrikeTime;
    long sb_OpenTime;
    BYTE sb_OfflineMode;
    BYTE sb_AccessMode;
    BYTE sb_DenyIfDuress;
    BYTE sb_ElevatorReader;
    BYTE sb_DoNotWait;
    BYTE sb_Downloadable;
    BYTE sb_CardSpecification;
    BYTE sb_PairedMaster;
    BYTE sb_PairedSlave;
    BYTE sb_UseAuxAsBolt;
    BYTE sb_FastBoltAlarm;
    BYTE sb_AcceptCmds;
    BYTE sb_PreAlarm;
    BYTE sb_2CardCtrl;
    long sb_CardFormat[ 8 ];
    BYTE sb_DiddleCnt;
    short sb_AreaLeaving;
    short sb_AreaEntering;
    long sb_ForcedOpenTZ;
    long sb_HeldOpenTZ;
    long sb_AuxInputTZ;
    long sb_Output1TZ;
    long sb_Output2TZ;
    BYTE sb_Output1State;
    BYTE sb_Output2State;
    BYTE sb_MaskForcedOpen;
    BYTE sb_MaskHeldOpen;
    BYTE sb_MaskAuxAlarm;
    BYTE sb_MaskAuxAlarm2;
    long sb_DayModeTZ;
    long sl_DayFloors;
    long sl_FacCodeFloors;
    long sl_CyperCode;
    BYTE sb_TimedAPB;
    BYTE sb_SoftAPB;
    long sb_LogGrantTZ;
    long sb_LogDenyTZ;
    long sb_LogStatusTZ;
    BYTE sb_DecUseLimit;
    BYTE sb_NoRexOnStrike;
    long sl_Out1Pulse;
    long sl_Out2Pulse;
    long sb_AuxInput2TZ;
    long sb_AuxInput3TZ;
    long sb_ElevatorAccessLevel;
    BYTE sb_SlaveID;
    BYTE sb_MaskAuxAlarm3;
    BYTE sb_FuncList[ 14 ];
    BYTE sb_TermList[ 7 ];
    BYTE sb_KeypadMode;
    BYTE sb_StrikeMode;
    BYTE sb_TwoWireLED;
    long sb_StrikeTime2;
    long sb_OpenTime2;
    long sl_DCSupervision;
    long sl_REXSupervision;
    long sl_Aux1Supervision;
    long sl_Aux2Supervision;
    long sl_Aux1Latched;
    long sl_Aux1EntryDelay;
    long sl_Aux1ExitDelay;
    long sl_Aux2Latched;
    long sl_Aux2EntryDelay;
    long sl_Aux2ExitDelay;
    ELEVATOR_HW_INFO sl_ElevatorInputID[ 8 ];
    ELEVATOR_HW_INFO sl_ElevatorOutputID[ 8 ];
    long sl_TrackElevatorInputRequests;
    long sl_OnlineFacEAL;
    BYTE sb_ChassisType;
    long sl_DeniedAttemptsCntTO;
    wchar_t sl_ReaderName[ 64 ];
    BYTE sb_DisableAsset;
    BYTE sb_LookAhead;
    BYTE sb_LookAheadOffset;
    BYTE sb_LookAheadRange;
    long sl_Chassis_Volume;
    long sl_ReaderMemory;
    BYTE sb_UseActivationDate;
    BYTE sb_UseExpirationDate;
    long sl_NumCardholders;
    long sl_FirstBadgeNumber;
    long sl_NumberOfBadges;
    long sl_altrdr_sio;
    long sl_altrdr_number;
    long sl_altrdr_spec;
    BYTE sb_AlternateReader;
    BYTE sb_BioVerify;
    BYTE sb_BioEnroll;
    BYTE sb_HostDecisionOnGrant;
    BYTE sb_HostDecisionProceedWithGrant;
    BYTE sb_GlobalAPB;
    long sl_AuxIn1HoldTime;
    long sl_AuxIn2HoldTime;
    long sl_DCDebounce;
    long sl_REXDebounce;
    long sl_AuxIn1Debounce;
    long sl_AuxIn2Debounce;
    BYTE sb_CipherMode;
    BYTE sb_FirstCardUnlock;
    BYTE sb_FirstCardUnlockAuthorityRequired;
    long alternateReaderID;
    BYTE turnstileSupport;
    long readerSIO;
    long readerNumber;
    long prealarmTimeout;
    BYTE tailgateOption;
    wchar_t aux1Name[ 32 ];
    wchar_t aux2Name[ 32 ];
    BYTE allowArmDisarmCommand;
    BYTE rexEvents;
    long defaultIntrusionArea;
    INTRUSION_CMD_CONFIG intrusionCmdA;
    INTRUSION_CMD_CONFIG intrusionCmdB;
    BYTE instantCommandModes[ 7 ];
    BYTE assetRequired;
    } 	READERDEF2;

typedef struct _ONITY_READERDEF
    {
    DWORD nOptions;
    BYTE nAuthorization;
    unsigned char KeypadCode[ 4 ];
    DWORD nRelockTimer;
    DWORD nHeartbeatInterval;
    BYTE nChannelId;
    BYTE nOutputPower;
    BYTE nFrequencyAgility;
    BYTE nCardValidationPrecedence;
    DWORD PriorityEvents[ 20 ];
    WORD nPriorityEventsCount;
    DWORD nReadAuditsInterval;
    DWORD nHeldOpenAlarmDuration;
    } 	ONITY_READERDEF;

typedef struct _NGP_READER_CONFIG
    {
    short nReaderNumber;
    BYTE bIsDefined;
    short nArea;
    BYTE bIsDecrementUseLimit;
    BYTE bIsHostDecisionOnGrant;
    BYTE nHostDecisionOfflineMode;
    long nHostDecisionTimeout;
    short nAllowedArea;
    BYTE bIsAllowedAreaCheck;
    BYTE bIsAntipassbackReader;
    BYTE bIsLockoutInWindow;
    BYTE bIsCommandStation;
    BYTE bIsLogAPBViolationOnly;
    BYTE nReaderModeInWindow;
    BYTE nReaderModeOutWindow;
    BYTE nCardModeInWindow;
    BYTE nCardModeOutWindow;
    BYTE nGroupNumber;
    long nReaderModeSchedule;
    long nLockoutSchedule;
    long nCardModeSchedule;
    BYTE nEnableDisableCardTypes;
    BYTE nEnableDisableMode;
    BYTE bIsEnablingReader;
    BYTE bIsUnlockOnEnableDisable;
    } 	NGP_READER_CONFIG;

typedef struct _NGP_LCD_KEYPAD_CONFIG
    {
    short nAnnunciationAreas;
    short *pAnnunciationAreaIds;
    short nArmDisarmAreas;
    short *pArmDisarmAreaIds;
    short nExitDelayAreas;
    short *pExitDelayAreaIds;
    BYTE nDefaultDisplayMode;
    BYTE nArmedLEDDisplay;
    BYTE nArmingToneMode;
    BYTE nAutoDisarmAllOnSilenceMode;
    BYTE nVerifyUserMode;
    BYTE bIsTripleCardMode;
    long lModeSchedule;
    BYTE nSingleBadgeModeInWindow;
    BYTE nSingleBadgeModeOutWindow;
    BYTE nHoldBadgeModeModeInWindow;
    BYTE nHoldBadgeModeModeOutWindow;
    BYTE nHoldBadgeTime;
    BYTE bIsDisarmAlwaysRequirePIN;
    short sDoorNumber;
    BYTE bIsccessControl;
    BYTE bIsInOrOutReader;
    BYTE bIsInOutStation;
    } 	NGP_LCD_KEYPAD_CONFIG;

typedef struct _NGP_MODULE_CONFIG
    {
    short moduleId;
    int serialNumber;
    NGP_LCD_KEYPAD_CONFIG keypadNGP;
    } 	NGP_MODULE_CONFIG;

typedef struct _NGP_DOOR_INTERLOCK_CONFIG
    {
    BYTE bIsInterlockRequired;
    BYTE nInterlockDelay;
    short sInterlockDoor1Number;
    short sInterlockDoor2Number;
    short sInterlockDoor3Number;
    } 	NGP_DOOR_INTERLOCK_CONFIG;

typedef struct _NGP_READERDEF
    {
    BYTE bIsPanelProcessRTE;
    BYTE nDoorProcessing;
    BYTE bIsREXRequired;
    BYTE nAuxInputMode;
    BYTE bIsReaderTamperRequired;
    BYTE nDoorAlarmTime;
    BYTE nAlarmOutputMode;
    BYTE nLEDMode;
    BYTE bIsRdrTamperPanelInput;
    BYTE nDoorCircuit;
    BYTE nReaderTamperCircuit;
    BYTE nDoorForcedHeldTime;
    BYTE bIsLockOnDoorClosure;
    BYTE bIsInsertionReader;
    BYTE bIsBuzzerClearOnClosure;
    BYTE nExtendedHoldTime;
    long nUnlockModeSchedule;
    BYTE nUnlockInWindow;
    BYTE nUnlockOutWindow;
    BYTE nArmingLevel;
    BYTE bIsDetectWanderingPatient;
    BYTE bIsLockOnWanderingPatient;
    BYTE bIsInOutStation;
    BYTE bIsDisableRexUnlock;
    BYTE bIsLogRTE;
    BYTE nAPBEntryDetection;
    short nSerialNumber;
    BYTE nHeldOpenProcessing_Tx;
    BYTE nHeldOpenProcessing_Siren;
    BYTE nHeldOpenProcessing_Alert;
    BYTE nForcedOpenProcessing_Tx;
    BYTE nForcedOpenProcessing_Siren;
    BYTE nForcedOpenProcessing_Alert;
    BYTE nMagLockProcessing_Tx;
    BYTE nMagLockProcessing_Siren;
    BYTE nMagLockProcessing_Alert;
    wchar_t shortName[ 13 ];
    NGP_READER_CONFIG readerConfig[ 2 ];
    NGP_MODULE_CONFIG moduleNGP;
    NGP_DOOR_INTERLOCK_CONFIG interlock;
    } 	NGP_READERDEF;

typedef struct _READERDEF3
    {
    long si_PanelID;
    short sb_ReaderNumber;
    BYTE sb_Activate;
    BYTE sb_AAPPort;
    BYTE sb_CommAddress;
    BYTE sb_CtrlType;
    BYTE sb_StrikeTime;
    long sb_OpenTime;
    BYTE sb_OfflineMode;
    BYTE sb_AccessMode;
    BYTE sb_DenyIfDuress;
    BYTE sb_ElevatorReader;
    BYTE sb_DoNotWait;
    BYTE sb_Downloadable;
    BYTE sb_CardSpecification;
    BYTE sb_PairedMaster;
    BYTE sb_PairedSlave;
    BYTE sb_UseAuxAsBolt;
    BYTE sb_FastBoltAlarm;
    BYTE sb_AcceptCmds;
    BYTE sb_PreAlarm;
    BYTE sb_2CardCtrl;
    long sb_CardFormat[ 8 ];
    BYTE sb_DiddleCnt;
    short sb_AreaLeaving;
    short sb_AreaEntering;
    long sb_ForcedOpenTZ;
    long sb_HeldOpenTZ;
    long sb_AuxInputTZ;
    long sb_Output1TZ;
    long sb_Output2TZ;
    BYTE sb_Output1State;
    BYTE sb_Output2State;
    BYTE sb_MaskForcedOpen;
    BYTE sb_MaskHeldOpen;
    BYTE sb_MaskAuxAlarm;
    BYTE sb_MaskAuxAlarm2;
    long sb_DayModeTZ;
    long sl_DayFloors;
    long sl_FacCodeFloors;
    long sl_CyperCode;
    BYTE sb_TimedAPB;
    BYTE sb_SoftAPB;
    long sb_LogGrantTZ;
    long sb_LogDenyTZ;
    long sb_LogStatusTZ;
    BYTE sb_DecUseLimit;
    BYTE sb_NoRexOnStrike;
    long sl_Out1Pulse;
    long sl_Out2Pulse;
    long sb_AuxInput2TZ;
    long sb_AuxInput3TZ;
    long sb_ElevatorAccessLevel;
    BYTE sb_SlaveID;
    BYTE sb_MaskAuxAlarm3;
    BYTE sb_FuncList[ 14 ];
    BYTE sb_TermList[ 7 ];
    BYTE sb_KeypadMode;
    BYTE sb_StrikeMode;
    BYTE sb_TwoWireLED;
    long sb_StrikeTime2;
    long sb_OpenTime2;
    long sl_DCSupervision;
    long sl_REXSupervision;
    long sl_Aux1Supervision;
    long sl_Aux2Supervision;
    long sl_Aux1Latched;
    long sl_Aux1EntryDelay;
    long sl_Aux1ExitDelay;
    long sl_Aux2Latched;
    long sl_Aux2EntryDelay;
    long sl_Aux2ExitDelay;
    ELEVATOR_HW_INFO sl_ElevatorInputID[ 8 ];
    ELEVATOR_HW_INFO sl_ElevatorOutputID[ 8 ];
    long sl_TrackElevatorInputRequests;
    long sl_OnlineFacEAL;
    BYTE sb_ChassisType;
    long sl_DeniedAttemptsCntTO;
    wchar_t sl_ReaderName[ 64 ];
    BYTE sb_DisableAsset;
    BYTE sb_LookAhead;
    BYTE sb_LookAheadOffset;
    BYTE sb_LookAheadRange;
    long sl_Chassis_Volume;
    long sl_ReaderMemory;
    BYTE sb_UseActivationDate;
    BYTE sb_UseExpirationDate;
    long sl_NumCardholders;
    long sl_FirstBadgeNumber;
    long sl_NumberOfBadges;
    long sl_altrdr_sio;
    long sl_altrdr_number;
    long sl_altrdr_spec;
    BYTE sb_AlternateReader;
    BYTE sb_BioVerify;
    BYTE sb_BioEnroll;
    BYTE sb_HostDecisionOnGrant;
    BYTE sb_HostDecisionProceedWithGrant;
    BYTE sb_GlobalAPB;
    long sl_AuxIn1HoldTime;
    long sl_AuxIn2HoldTime;
    long sl_DCDebounce;
    long sl_REXDebounce;
    long sl_AuxIn1Debounce;
    long sl_AuxIn2Debounce;
    BYTE sb_CipherMode;
    BYTE sb_FirstCardUnlock;
    BYTE sb_FirstCardUnlockAuthorityRequired;
    long alternateReaderID;
    BYTE turnstileSupport;
    long readerSIO;
    long readerNumber;
    long prealarmTimeout;
    BYTE tailgateOption;
    wchar_t aux1Name[ 32 ];
    wchar_t aux2Name[ 32 ];
    BYTE allowArmDisarmCommand;
    BYTE rexEvents;
    long defaultIntrusionArea;
    INTRUSION_CMD_CONFIG2 intrusionCmdA;
    INTRUSION_CMD_CONFIG2 intrusionCmdB;
    BYTE instantCommandModes[ 7 ];
    BYTE assetRequired;
    long sl_DiddleRestore;
    BYTE relaxedForcedOpen;
    BYTE readerAlarmShunt;
    long nEcryptedCommunicationMode;
    ONITY_READERDEF Onity;
    NGP_READERDEF NGP;
    BYTE useStrikeFollower;
    int strikeFollowPulse;
    int strikeFollowDelay;
    } 	READERDEF3;

typedef struct _READER_TZCTRL
    {
    long sb_Timezone;
    BYTE sb_StartMode;
    BYTE sb_EndMode;
    BYTE sb_StartFirstCardUnlockMode;
    BYTE sb_EndFirstCardUnlockMode;
    BYTE sb_StartVerifyMode;
    BYTE sb_EndVerifyMode;
    } 	READER_TZCTRL;

#define MAX_RDR_TZ_CTRL 125
typedef struct _READER_TZLIST
    {
    long si_PanelID;
    short sb_ReaderNumber;
    BYTE sb_NumberOfEntries;
    READER_TZCTRL ss_TZ[ 125 ];
    } 	READER_TZLIST;

typedef struct _AREA_TZCTRL
    {
    long sb_Timezone;
    BYTE sb_StartOpenCloseMode;
    BYTE sb_EndOpencloseMode;
    BYTE sb_StartTwoManMode;
    BYTE sb_EndTwoManMode;
    } 	AREA_TZCTRL;

#define MAX_AREA_TZ_CTRL 125
typedef struct _AREA_TZLIST
    {
    long si_PanelID;
    short sb_AreaNumber;
    BYTE sb_NumberOfEntries;
    AREA_TZCTRL ss_TZ[ 125 ];
    } 	AREA_TZLIST;

typedef struct _READER_DWNSPEC
    {
    long si_PanelID;
    long si_SegmentID;
    long sb_RecordNum;
    BYTE sb_CardType;
    BYTE sb_PinType;
    BYTE sb_Extension;
    long sl_NumCards;
    } 	READER_DWNSPEC;

typedef struct _ALARM_PANEL
    {
    long si_PanelID;
    long sb_PanelNumber;
    BYTE sb_Activate;
    BYTE sb_AAPPort;
    BYTE sb_CommAddress;
    BYTE sb_CtrlType;
    long sb_NextPanelID;
    } 	ALARM_PANEL;

typedef struct _ALARM_PANEL2
    {
    long si_PanelID;
    long sb_PanelNumber;
    BYTE sb_Activate;
    BYTE sb_AAPPort;
    BYTE sb_CommAddress;
    BYTE sb_CtrlType;
    long sb_NextPanelID;
    long alarmPanelSIO;
    } 	ALARM_PANEL2;

typedef struct _ALARM_PANEL3
    {
    long si_PanelID;
    long sb_PanelNumber;
    BYTE sb_Activate;
    BYTE sb_AAPPort;
    BYTE sb_CommAddress;
    BYTE sb_CtrlType;
    long sb_NextPanelID;
    long alarmPanelSIO;
    long nEcryptedCommunicationMode;
    long nAreaID;
    BYTE nExitDelayArmingLevel;
    BYTE sb_IsToneWarnings;
    BYTE sb_IsTamperMonitor;
    BYTE nNumInputs;
    BYTE nNumOutputs;
    NGP_MODULE_CONFIG moduleNGP;
    } 	ALARM_PANEL3;

#define MAX_ALARM_INPUTS 16
#define MAX_ALARM_OUTPUTS 16
#define MAX_CHANNELS 16
#define MAX_ALARM_INPUTS2 32
#define MAX_ALARM_OUTPUTS2 32
typedef struct _ALARM_INPUT
    {
    BYTE sb_DIADssAddress;
    long sb_MaskTimezone;
    FLAG sb_DIATamperNormallyOpen;
    FLAG sb_Configure;
    FLAG sb_MaskInput;
    long sb_LogTZ;
    FLAG sb_LatchMode;
    long sl_EntryDelay;
    long sl_ExitDelay;
    long sb_Supervision;
    long sb_TransOperation;
    long sl_HoldTime;
    long sl_Debounce;
    } 	ALARM_INPUT;

typedef struct _ALARM_INPUT_MSG
    {
    long si_PanelID;
    long sb_PanelNumber;
    ALARM_INPUT ss_Inputs[ 16 ];
    long sl_LINXAddr;
    BYTE sb_Operation;
    BYTE sb_CtrlType;
    } 	ALARM_INPUT_MSG;

typedef struct _ALARM_INPUT2
    {
    BYTE sb_DIADssAddress;
    long sb_MaskTimezone;
    FLAG sb_DIATamperNormallyOpen;
    FLAG sb_Configure;
    FLAG sb_MaskInput;
    long sb_LogTZ;
    FLAG sb_LatchMode;
    long sl_EntryDelay;
    long sl_ExitDelay;
    long sb_Supervision;
    long sb_TransOperation;
    long sl_HoldTime;
    long sl_Debounce;
    wchar_t inputName[ 32 ];
    } 	ALARM_INPUT2;

typedef struct _ALARM_INPUT_MSG2
    {
    long si_PanelID;
    long sb_PanelNumber;
    ALARM_INPUT2 ss_Inputs[ 16 ];
    long sl_LINXAddr;
    BYTE sb_Operation;
    BYTE sb_CtrlType;
    } 	ALARM_INPUT_MSG2;

typedef struct _ALARM_INPUT3
    {
    BYTE sb_DIADssAddress;
    long sb_MaskTimezone;
    FLAG sb_DIATamperNormallyOpen;
    FLAG sb_Configure;
    FLAG sb_MaskInput;
    long sb_LogTZ;
    FLAG sb_LatchMode;
    long sl_EntryDelay;
    long sl_ExitDelay;
    long sb_Supervision;
    long sb_TransOperation;
    long sl_HoldTime;
    long sl_Debounce;
    wchar_t inputName[ 32 ];
    long nAreaID;
    long nBufferAreaID;
    long nPointType;
    } 	ALARM_INPUT3;

typedef struct _ALARM_INPUT_MSG3
    {
    long si_PanelID;
    long sb_PanelNumber;
    ALARM_INPUT3 ss_Inputs[ 32 ];
    long sl_LINXAddr;
    BYTE sb_Operation;
    BYTE sb_CtrlType;
    } 	ALARM_INPUT_MSG3;

#define OUTPUT_LINKAGE_OFF 0x00
#define OUTPUT_LINKAGE_ON 0x01
#define OUTPUT_LINKAGE_LOCAL 0x02
#define OUTPUT_LINKAGE_PULSE 0x03
#define OUTPUT_LINKAGE_IGNORE 0xff
typedef struct ALARM_OUTPUT
    {
    long ss_PulseDuration;
    FLAG sb_OfflineLocalLink;
    FLAG sb_CommLoss;
    FLAG sb_AnyInput;
    FLAG sb_Cabinet;
    FLAG sb_PowerFault;
    FLAG sb_Inputs[ 16 ];
    long sb_Timezone;
    BYTE sb_OperationMode;
    FLAG sb_Input1;
    FLAG sb_Input2;
    BYTE sb_Online;
    long sb_TransOperation;
    } 	ALARM_OUTPUT;

typedef struct _ALARM_OUTPUT_MSG
    {
    long si_PanelID;
    long sb_PanelNumber;
    ALARM_OUTPUT ss_Outputs[ 16 ];
    long sl_LINXAddr;
    BYTE sb_Operation;
    BYTE sb_CtrlType;
    } 	ALARM_OUTPUT_MSG;

#define MAX_SCP_SYSTEM_INPUTS	256
typedef struct _ALARM_OUTPUT_LINKS
    {
    int si_NumOfInputs;
    int si_PanelID[ 256 ];
    int si_InputID[ 256 ];
    } 	ALARM_OUTPUT_LINKS;

typedef struct _ALARM_OUTPUT_LINKS_MSG
    {
    long si_PanelID;
    long sb_PanelNumber;
    BYTE sb_CtrlType;
    ALARM_OUTPUT_LINKS ss_Outputs[ 16 ];
    } 	ALARM_OUTPUT_LINKS_MSG;

typedef struct _SYSTEM_STATUS
    {
    long si_PanelID;
    BYTE sb_AsyncStatus;
    BYTE sb_DownloadingFirmware;
    BYTE sb_Devices[ 81 ];
    BYTE sb_DeviceCabinetTamper[ 81 ];
    BYTE sb_DevicePowerFail[ 81 ];
    long sb_FirmwareVersion[ 81 ];
    BYTE sb_InvalidDeviceType[ 81 ];
    long si_InvalidSerialNumber[ 81 ];
    } 	SYSTEM_STATUS;

typedef struct _SYSTEM_STATUS2
    {
    long si_PanelID;
    BYTE sb_AsyncStatus;
    BYTE sb_DownloadingFirmware;
    BYTE sb_Devices[ 97 ];
    BYTE sb_DeviceCabinetTamper[ 97 ];
    BYTE sb_DevicePowerFail[ 97 ];
    long sb_FirmwareVersion[ 97 ];
    BYTE sb_InvalidDeviceType[ 97 ];
    long si_InvalidSerialNumber[ 97 ];
    } 	SYSTEM_STATUS2;

typedef struct _SYSTEM_STATUS_ENTRY
    {
    long nDeviceType;
    long nDeviceId;
    BYTE nStatus;
    __int64 nExtStatus;
    __int64 nExtMaskedStatus;
    long nFirmwareVersion;
    long nActualSIOType;
    long nSerialNumber;
    BYTE nAlternateReaderStatus;
    BYTE nDeviceMode;
    } 	SYSTEM_STATUS_ENTRY;

typedef struct _SYSTEM_STATUS3
    {
    long nPanelId;
    BYTE bAsyncStatus;
    BYTE bDownloadingFirmware;
    long nEntries;
    SYSTEM_STATUS_ENTRY *pEntries;
    } 	SYSTEM_STATUS3;

#define SYSTEM_STATUS_ONLINE_STATUS				0x01
#define SYSTEM_STATUS_OPTIONS_MISMATCH_STATUS	0x02
#define SYSTEM_STATUS_ENCRYPTED_DEFAULT_KEY		0x04
#define SYSTEM_STATUS_ENCRYPTED_CUSTOM_KEY		0x08
#define RDRSTATUS_TAMPER			0x01
#define RDRSTATUS_FORCED			0x02
#define RDRSTATUS_HELD			0x04
#define RDRSTATUS_AUX			0x08
#define RDRSTATUS_AUX2			0x10
#define RDRSTATUS_CNTTAMPER		0x20
#define RDRSTATUS_AUX3			0x40
#define RDRSTATUS_BIO_VERIFY		0x80
#define RDRSTATUS_DC_GND_FLT		0x100
#define RDRSTATUS_DC_SHRT_FLT	0x200
#define RDRSTATUS_DC_OPEN_FLT	0x400
#define RDRSTATUS_DC_GEN_FLT		0x800
#define RDRSTATUS_RX_GND_FLT		0x1000
#define RDRSTATUS_RX_SHRT_FLT	0x2000
#define RDRSTATUS_RX_OPEN_FLT	0x4000
#define RDRSTATUS_RX_GEN_FLT		0x8000
#define RDRSTATUS_AX1_GND_FLT	0x10000
#define RDRSTATUS_AX1_SHRT_FLT	0x20000
#define RDRSTATUS_AX1_OPEN_FLT	0x40000
#define RDRSTATUS_AX1_GEN_FLT	0x80000
#define RDRSTATUS_AX2_GND_FLT	0x100000
#define RDRSTATUS_AX2_SHRT_FLT	0x200000
#define RDRSTATUS_AX2_OPEN_FLT	0x400000
#define RDRSTATUS_AX2_GEN_FLT	0x800000
#define RDRSTATUS_AUXOUT1		0x1000000
#define RDRSTATUS_AUXOUT2		0x2000000
#define RDRSTATUS_CABINET_TAMPER		0x4000000
#define RDRSTATUS_POWER_FAIL			0x8000000
#define RDRSTATUS_FIRST_CARD_UNLOCK	0x10000000
#define RDRSTATUS_EXTENDED_HELD_MODE	0x20000000
#define RDRSTATUS_CIPHER_MODE		0x40000000
#define RDRSTATUS_LOW_BATTERY			0x80000000
#define RDRSTATUS_MOTOR_STALLED			0x100000000
#define RDRSTATUS_READHEAD_OFFLINE		0x200000000
#define RDRSTATUS_MRDT_OFFLINE			0x400000000
#define RDRSTATUS_DOOR_CONTACT_OFFLINE   0x800000000
#define RDRSTATUS_DOOR_OPEN				0x1000000000
#define RDRSTATUS_DEADBOLT_ENGAGED		0x2000000000
#define RDRSTATUS_DOOR_UNLOCKED			0x4000000000
#define STATUS_EXT_CABINET_TAMPER	RDRSTATUS_CABINET_TAMPER
#define STATUS_EXT_POWER_FAIL		RDRSTATUS_POWER_FAIL
#define SET_DC_CLR_FLT			0xFFFFFFFFFFFFF0FF
#define SET_RX_CLR_FLT			0xFFFFFFFFFFFF0FFF
#define SET_AX1_CLR_FLT			0xFFFFFFFFFFF0FFFF
#define SET_AX2_CLR_FLT			0xFFFFFFFFFF0FFFFF
#define CTLSTATUS_CABINET	0x01
#define CTLSTATUS_POWERFAIL	0x02
#define CTLSTATUS_DATACOMM	0x04
#define ALRM_STATUS_SECURE		0
#define ALRM_STATUS_ACTIVE		1
#define ALRM_STATUS_GND_FLT		2
#define ALRM_STATUS_SHRT_FLT		3
#define ALRM_STATUS_OPEN_FLT		4
#define ALRM_STATUS_GEN_FLT		5
typedef struct _READER_STATUS
    {
    long si_PanelID;
    BYTE sb_AsyncStatus;
    long sb_Devices[ 65 ];
    BYTE sb_DeviceMaskStatus[ 65 ];
    BYTE sb_DeviceMode[ 65 ];
    BYTE sb_AlternateReaderStatus[ 65 ];
    } 	READER_STATUS;

typedef struct _READER_STATUS2
    {
    long si_PanelID;
    BYTE sb_AsyncStatus;
    __int64 sb_Devices[ 65 ];
    BYTE sb_DeviceMaskStatus[ 65 ];
    BYTE sb_DeviceMode[ 65 ];
    BYTE sb_AlternateReaderStatus[ 65 ];
    } 	READER_STATUS2;

typedef struct _ALARMINPUT_STATUS
    {
    BYTE sb_Devices[ 17 ];
    BYTE sb_DeviceMaskStatus[ 17 ];
    } 	ALARMINPUT_STATUS;

typedef struct _ALARMPANEL_STATUS
    {
    long si_PanelID;
    BYTE sb_AsyncStatus;
    ALARMINPUT_STATUS ss_AlarmInputs[ 16 ];
    } 	ALARMPANEL_STATUS;

typedef struct _ALARMPANEL_STATUS2
    {
    long si_PanelID;
    BYTE sb_AsyncStatus;
    ALARMINPUT_STATUS ss_AlarmInputs[ 32 ];
    } 	ALARMPANEL_STATUS2;

typedef struct _TZI
    {
    long Bias;
    long StandardBias;
    long DaylightBias;
    SYSTEMTIME StandardDate;
    SYSTEMTIME DaylightDate;
    long UTCoffset;
    BYTE DST;
    } 	TZI;

#define MAX_TERMINALS_PER_ELEVATOR_PANEL 256
#define ELEVATOR_DISPATCHING_MAX_ELEVATOR_FLOOR_BYTES 32
typedef struct _ELEVATOR_FLOORLIST
    {
    long si_ElevatorFloorListID;
    long si_Timezone;
    BYTE sb_FloorList[ 32 ];
    } 	ELEVATOR_FLOORLIST;

typedef struct _ELEVATOR_KEYPAD_TERMINAL
    {
    short terminalID;
    short operationalMode;
    ELEVATOR_FLOORLIST allowedFloors;
    } 	ELEVATOR_KEYPAD_TERMINAL;

typedef struct _ELEVATOR_KEYPAD_TERMINALS
    {
    long numTerminals;
    ELEVATOR_KEYPAD_TERMINAL terminalArray[ 256 ];
    } 	ELEVATOR_KEYPAD_TERMINALS;

typedef struct _ELV_DISPATCHING_CREDENTIAL_DATA
    {
    BOOL sb_IsCredentialValid;
    __int64 sl_CardNumber;
    BYTE sb_CredentialFlags;
    BYTE sb_AuthorizedFloors[ 32 ];
    BYTE sb_DefaultFloor;
    DWORD sl_Time;
    short ss_AccessPanelID;
    short sb_ReaderID;
    } 	ELV_DISPATCHING_CREDENTIAL_DATA;

typedef struct _ELEVATOR_TERMINAL_CONFIG
    {
    int sl_AccessPanelID;
    int sl_ReaderID;
    int sl_ElevatorControllerID;
    int sl_AccessEventDuration;
    BOOL sb_DeleteTerminal;
    ELEVATOR_KEYPAD_TERMINAL terminalConfig;
    } 	ELEVATOR_TERMINAL_CONFIG;

typedef struct _OFFLINELOCK_CONFIG
    {
    DWORD nSystemCode;
    BYTE nAuthorizations;
    BYTE nDateFormat;
    WORD nLoginAttempts;
    BYTE bAutoLogout;
    WORD nIdleTime;
    BYTE bDeactivateData;
    WORD nDeactivateTime;
    BYTE bAfcModeEnabled;
    WORD nLookAheadRange;
    WORD nMissedHeartbeats;
    BYTE nChannelId;
    BYTE nOutputPower;
    BYTE nFrequencyAgility;
    } 	OFFLINELOCK_CONFIG;

typedef struct _USERDEF
    {
    long nUserId;
    BSTR bstrUserName;
    BSTR bstrLoginName;
    BSTR bstrPassword;
    ULONG nOptions;
    } 	USERDEF;

typedef struct _NGP_POINT_TYPE
    {
    BYTE nPreprocess;
    BYTE nLevel;
    BYTE nClass;
    BYTE bSirenOff;
    BYTE bSirenStay;
    BYTE bSirenOn;
    BYTE bSounderOff;
    BYTE bSounderStay;
    BYTE bSounderOn;
    BYTE bTransmitOff;
    BYTE bTransmitStay;
    BYTE bTransmitOn;
    BYTE bBypass;
    BYTE bChime;
    BYTE bWarning;
    } 	NGP_POINT_TYPE;

typedef struct _NGP_CONTROLLER_SETTINGS
    {
    long si_PanelID;
    long iPanelCode;
    WORD w16SerialNumber;
    int iRegion;
    BYTE bFireAccessOverride;
    BYTE bEnableWallTamper;
    BYTE bRingbackRequired;
    long iSirenTime;
    long iConfirmedAlarmTimeout;
    long iAlarmsPerPointState;
    long iPointResetTime;
    BYTE iFallBackMode;
    BYTE iEscortRequiredMode;
    BYTE iSTUOutputBase;
    BYTE iSTUNumOutputs;
    wchar_t wSystemMessage[ 17 ];
    BYTE bFastRestore;
    BYTE bDelayScreen;
    BYTE bRingBack;
    BYTE bDisarmByKeypad;
    BYTE bReverseTones;
    BYTE bConfirmedResetService;
    BYTE bConfirmedResetMaster;
    BYTE bConfirmedResetChallenged;
    BYTE bConfirmed_ResetRemote;
    long iTelcoAccountNumber;
    long iTelcoReportMode;
    long iTelcoReportFormat;
    long iTelcoCountry;
    long iTelcoSequence;
    BYTE bTelcoPrioritizedReporting;
    BYTE bTelcoNeverAllowBlindDial;
    wchar_t wTelcoPrimaryPhoneNum[ 17 ];
    wchar_t wTelcoBackupPhoneNum[ 17 ];
    long iSIPAccountNumber;
    long iHSCMode;
    wchar_t wHSCPrimaryHost[ 33 ];
    long iHSCPrimaryPort;
    wchar_t wHSCSecondaryHost[ 33 ];
    long iHSCSecondaryPort;
    wchar_t wHSCProxy[ 33 ];
    long iHSCProxyPort;
    long iTelcoModemType;
    BYTE bSTUSupportsLineFail;
    BYTE bSTULineFailPolarity;
    NGP_POINT_TYPE sEquipmentPseudoPoints[ 16 ];
    } 	NGP_CONTROLLER_SETTINGS;

#define DEFNUM_AUTH_SCHEDULE     3
typedef struct _PERMISSION_PROFILE
    {
    long nPanelID;
    long nSegmentID;
    long nProfileID;
    wchar_t shortName[ 13 ];
    BYTE bAccessOff;
    BYTE bAccessStay;
    BYTE bAccessOn;
    BYTE bEscort;
    BYTE bVisitor;
    BYTE bMasterOverride;
    BYTE bWanderingPatient;
    BYTE bResetDoorAlarm;
    BYTE bPanicToken;
    BYTE bDefined;
    BYTE bEmergencyOff;
    BYTE bIsolate;
    BYTE bBypass;
    BYTE bAutoLiftBypass;
    BYTE bTest;
    BYTE bServiceTest;
    BYTE bSilenceAlarm;
    BYTE bStatus;
    BYTE bHistory;
    BYTE bFKeysPIN;
    BYTE bWorkLate;
    BYTE bSuspendSchedule;
    long nSchedule[ 3 ];
    long nArmOnScheduleId;
    BYTE nArmOnType;
    long nArmOffScheduleId;
    BYTE nArmOffType;
    long nArmStayScheduleId;
    BYTE nArmStayType;
    long nTokenDisarmOffNotStayScheduleId;
    BYTE nTokenDisarmOffNotStayType;
    long nTokenDisarmAutoAllOffScheduleId;
    BYTE nTokenDisarmAutoAllOffType;
    long nDoorCommandScheduleId;
    BYTE nDoorCommandType;
    } 	PERMISSION_PROFILE;

// Defines for the ACCESSPANEL_DRIVERPARMS structure
#define DRIVERPARMS_PHONENUM_LEN		64
#define DRIVERPARMS_TAPINAME_LEN		255
#define DRIVERPARMS_PASSWORD_LEN		64
#define DRIVERPARMS_PANELNAME_LEN	64
#define DRIVERPARMS_COMPUTERNAME_LEN	128
#define DRIVERPARMS_CLSID_LEN	64
#define DRIVERPARMS_SNMP_COMMUNITY_LEN	64
#define DRIVERPARMS_USERNAME_LEN	256
#define DRIVERPARMS_FIRMWARE_REV_STRING_LEN	64
typedef struct _ACCESSPANEL_DRIVERPARMS
    {
    long si_PanelID;
    long si_ExtType;
    long si_ComPort;
    long si_BaudRate;
    long si_SecondaryBaudRate;
    BYTE si_TransType;
    BYTE si_PanelType;
    long sl_PanelClass;
    long si_Address;
    long si_ComPort2;
    long si_PrimaryIP;
    long si_SubnetMask;
    long si_SecondaryIP;
    BYTE si_UseLan;
    BYTE si_UseLan2;
    CARD_REC_CFG ss_CardCfg;
    unsigned char si_HostNumber1[ 64 ];
    unsigned char si_HostNumber2[ 64 ];
    unsigned char si_PanelNumber1[ 64 ];
    unsigned char si_PanelNumber2[ 64 ];
    wchar_t si_TapiName[ 255 ];
    wchar_t si_TapiName2[ 255 ];
    BYTE si_Dialback;
    long si_DialupTZ;
    long si_DialupTZSecond;
    long si_WireType;
    TZI ss_worldTZ;
    BYTE sb_DST;
    long sl_EventSerialNumber;
    long sl_DialBackEventThreshold;
    long sl_DialBackEventThreshold2;
    long si_DialupTZ2;
    GUID ss_PanelCLSID;
    wchar_t si_PanelName[ 64 ];
    long sl_VidArchiveEventsThreshold;
    wchar_t s_Password[ 64 ];
    long si_ByteSize;
    long si_Parity;
    long si_StopBits;
    BYTE sb_DisplayError;
    long sl_VideoInputStandard;
    long sl_VideoTimeLapseEnabled;
    long sl_VideoTimeLapseMSPerFrame;
    long sl_VideoTimeLapsePreRollMS;
    int si_HeartbeatDelay;
    int si_StartChar;
    int si_EndChar;
    int si_LanPort;
    BYTE sb_GlobalAPBSegment;
    long si_FunctionLevel;
    long si_EventIndex;
    long si_PanelUserGroupID;
    wchar_t si_ComputerName[ 128 ];
    wchar_t si_ClsID[ 64 ];
    wchar_t si_SNMPSetCommunity[ 64 ];
    wchar_t si_SNMPGetCommunity[ 64 ];
    long sioMapping[ 64 ];
    wchar_t si_UserName[ 256 ];
    long sl_FlashSize;
    long sl_DipSwitchSettings;
    wchar_t si_FirmwareRevisionString[ 64 ];
    BYTE sb_DownloadPlainFirmware;
    long sl_MaxElevatorFloors;
    CONTROLLER_ENCRTYPTION_CFG ss_ControllerEncryptionCfg;
    BYTE sb_DownstreamEncryptionEnabled;
    BYTE sb_ExtendedOptionsEnabled;
    BYTE IssueCodeMode;
    BYTE supportSpecialTwoManRule;
    BYTE supportArmDisarmCommand;
    ELEVATOR_KEYPAD_TERMINALS ss_ElevatorKeypadTerminals;
    int si_LanPort2;
    wchar_t si_ComputerName2[ 128 ];
    long numAssetGroups;
    long numClassesPerAssetGroup;
    BYTE sb_BlockIntrusionLevelAccessControl;
    BYTE sb_AutoClockSynch;
    OFFLINELOCK_CONFIG ss_OfflineLockCfg;
    BYTE isSelectiveDownloadInUse;
    long RecoveryTimeoutSecs;
    NGP_CONTROLLER_SETTINGS ss_NGPControllerSettings;
    long sl_AuxServerID;
    long sl_AuxServerPort;
    wchar_t s_AuxServerIP[ 128 ];
    wchar_t s_AuxUserName[ 256 ];
    wchar_t s_AuxPassword[ 64 ];
    } 	ACCESSPANEL_DRIVERPARMS;

typedef struct _VIDEOPANEL_PARMS
    {
    BYTE si_TransType;
    long si_PanelID;
    long si_PrimaryIP;
    GUID ss_PanelCLSID;
    wchar_t sb_DriverLocation[ 256 ];
    TZI ss_worldTZ;
    BYTE sb_VidAutoArchiveEvents;
    long sl_VidArchiveEventsThreshold;
    long sl_VidArchiveEventsFreeAmt;
    BYTE sb_VidAutoPurgeEvents;
    long sl_VidArchiveEventsLocID;
    BYTE sb_VidArchiveCont;
    SYSTEMTIME ss_ArchiveContStartTime;
    long sl_VidArchivingTz;
    long sl_VidDelayedArchivingBuffer;
    wchar_t sb_VidArchiveEventsPath[ 256 ];
    wchar_t sb_VidArchiveContPath[ 256 ];
    wchar_t sb_VidArchiveContPath2[ 256 ];
    long ss_UIType;
    } 	VIDEOPANEL_PARMS;

#define MAX_AREAS_PER_AAP	64
#define MAX_AREAS_PER_AAM16	16
#define MAX_AREAS_NGP	150
#define MAX_AREA_GROUP_COUNT_NGP 16
#define MAX_AREAS_PER_AREA_GROUP_NGP 150
typedef struct _AREAAPB
    {
    long si_PanelID;
    int si_AreaID;
    FLAG si_TwoManControl;
    FLAG si_AreaClosed;
    FLAG si_DenyAllIfClosed;
    int si_ReaderID;
    int si_MaxOccupancy;
    int si_MaxCount;
    int si_MinCount;
    int si_RuleID;
    int si_RuleTerm;
    int si_MinArg;
    int si_MaxArg;
    short si_AreaType;
    int si_AreaAPBID;
    long si_SegmentID;
    } 	AREAAPB;

typedef struct _AREAAPB2
    {
    long si_PanelID;
    int si_AreaID;
    FLAG si_TwoManControl;
    FLAG si_AreaClosed;
    FLAG si_DenyAllIfClosed;
    int si_ReaderID;
    int si_MaxOccupancy;
    int si_MaxCount;
    int si_MinCount;
    int si_RuleID;
    int si_RuleTerm;
    int si_MinArg;
    int si_MaxArg;
    short si_AreaType;
    int si_AreaAPBID;
    long si_SegmentID;
    __int64 Owner;
    short RequestRelayDelay;
    long RequestRelaySIO;
    long RequestRelayNum;
    } 	AREAAPB2;

typedef struct _AREAAPB3
    {
    long si_PanelID;
    int si_AreaID;
    FLAG si_TwoManControl;
    FLAG si_AreaClosed;
    FLAG si_DenyAllIfClosed;
    int si_ReaderID;
    int si_MaxOccupancy;
    int si_MaxCount;
    int si_MinCount;
    int si_RuleID;
    int si_RuleTerm;
    int si_MinArg;
    int si_MaxArg;
    short si_AreaType;
    int si_AreaAPBID;
    long si_SegmentID;
    __int64 Owner;
    short RequestRelayDelay;
    long RequestRelaySIO;
    long RequestRelayNum;
    short InterlockMode;
    } 	AREAAPB3;

#define AREA_INTERLOCK_MODE_PASSAGEWAY 1
#define AREA_INTERLOCK_MODE_ONE_DOOR_ONLY 3
#define AAP_MAX_ELEVATOR_FLOORS 32
#define LNL_MAX_ELEVATOR_FLOORS	128
typedef struct _ELEVATOR_ACCLEVEL
    {
    long si_PanelID;
    long si_SegmentID;
    long si_ElevatorAccessLevel;
    long sb_FloorList[ 128 ];
    } 	ELEVATOR_ACCLEVEL;

typedef struct _ELEVATOR_ACCLEVEL_PERTZ
    {
    long si_PanelID;
    long si_SegmentID;
    long si_ElevatorAccessLevel;
    long si_Timezone;
    long sb_FloorList[ 128 ];
    } 	ELEVATOR_ACCLEVEL_PERTZ;

typedef struct _AREA_APB_TABLE
    {
    int si_FromArea;
    int si_ToArea;
    int si_APBDelay;
    } 	AREA_APB_TABLE;

typedef struct _AREA_APB_CFGMSG
    {
    long si_PanelID;
    AREA_APB_TABLE ss_Table[ 64 ];
    } 	AREA_APB_CFGMSG;

typedef struct _AREA_NGP
    {
    long nPanelID;
    long nSegmentID;
    long nAreaID;
    wchar_t shortName[ 13 ];
    short nEntryDelay;
    short nExitDelay;
    short nPreAlarmDelay;
    short nGarageDelay;
    BYTE bAPBAutoReset;
    short nWorkLateInput;
    BYTE bIsAPBSecureEntryExit;
    BYTE bIsAPBNoOutsideCheck;
    BYTE bIsStayOnFailToExit;
    BYTE bIsLimitWorkLateToMidnight;
    BYTE bIsAlarmOnFailToExit;
    BYTE bIsTerminateExitDelay;
    BYTE bIsRequireHotKeyPIN;
    int nReportMode;
    BYTE bIsDualCustody;
    BYTE bIsOpenInterlock;
    BYTE bIsTransmitOnFailToClose;
    BYTE bIsAutoArmOnFailToClose;
    BYTE bIsUnauthorizedOpens;
    BYTE bIsAutoArmDoorClose;
    short nOutOfWindowOpen;
    short nInWindowOpen;
    short nAutoSchdStayMode;
    long nAutoStayModeSchedule;
    long nArmDisarmSchedule;
    short nExtendedAutoArmDelay;
    short nAutoDisarmToOffBlind;
    short nArmWarningMode;
    short nFailToExitMode;
    short nExtendDelayOnFailToExit;
    BYTE bIsMultiTenant;
    short nIncrementMode;
    BYTE bIsBellSquawkOnArming;
    short nAutoDisarmOnValidTokenInWindow;
    short nAutoDisarmOnValidTokenOutWindow;
    short nArmingRules;
    short nDisarmingRules;
    short nArmingPriority;
    short nDisarmingPriority;
    short nCounterResetOnDisarmToOff;
    short nCounterResetOnArmToOn;
    short nCounterResetBeforeInWin;
    short nCounterMaxOccupancy;
    short nCounterMinOccupancy;
    short nExtendedAutoArmMode;
    short nExtendedAutoArmWarningLevel;
    short nExtendedAutoArmLevel;
    short nExtendedAutoArmOnlyIfOutWindow;
    short nActivityTimeout;
    short nActivityIncludeEntryExitRoute;
    short nActivityIncludeDoors;
    short nActivityAlarmOnNoActivity;
    BYTE bBadCardLockoutAllUsers;
    BYTE bBadCardGenerateTones;
    short nCommonAreas;
    short *pCommonAreaIds;
    short nPriorityAreas;
    short *pPriorityAreaIds;
    } 	AREA_NGP;

typedef struct _AREA_GROUP
    {
    long nPanelID;
    long nAreaGroupID;
    wchar_t shortName[ 13 ];
    short nNumAreas;
    short *pAreaIds;
    } 	AREA_GROUP;

typedef struct _AREA_PROFILE_ENTRY
    {
    long nProfileID;
    long nAreaID;
    } 	AREA_PROFILE_ENTRY;

typedef struct _AUTHORITY_NGP
    {
    long nPanelID;
    long nSegmentID;
    long nAccessLevelID;
    BYTE bDefined;
    wchar_t shortName[ 13 ];
    SYSTEMTIME ss_StartDate;
    SYSTEMTIME ss_EndDate;
    short nUserEditGroup;
    long nReaderTZEntries;
    ACCESS_LEVEL_ENTRY *pReaderTZEntries;
    long nAreaProfileEntries;
    AREA_PROFILE_ENTRY *pAreaProfileEntries;
    } 	AUTHORITY_NGP;

typedef struct TRecReaderConfigTag
    {
    unsigned char Area;
    unsigned char Antipassback;
    unsigned char LockoutInWindow;
    unsigned char Defined;
    unsigned char CommandStation;
    unsigned char ClassMapAInWindow;
    unsigned char ClassMapBInWindow;
    unsigned char ClassMapCInWindow;
    unsigned char ClassMapAOutWindow;
    unsigned char ClassMapBOutWindow;
    unsigned char ClassMapCOutWindow;
    unsigned char LogAPBViolationOnly;
    unsigned char ReaderModeInWindow;
    unsigned char ReaderModeOutWindow;
    unsigned char CardModeInWindow;
    unsigned char CardModeOutWindow;
    unsigned char GroupNumber;
    unsigned char ReaderModeSchedule;
    unsigned char ClassMapSchedule;
    unsigned char LockoutSchedule;
    unsigned char CardModeSchedule;
    unsigned char EnableDisableCardTypes;
    unsigned char EnableClassChecking;
    unsigned char EnableDisableMode;
    unsigned char IsEnablingReader;
    unsigned char UnlockOnEnableDisable;
    } 	TRecReaderConfig;

typedef struct TRecDoorTag
    {
    int i32PanelID;
    unsigned short w16DoorNumber;
    unsigned char Exists;
    unsigned short Name[ 4 ];
    unsigned char Pod;
    unsigned char PodDoorIndex;
    unsigned char ElevatorController;
    struct TRecDoorConfig
        {
        unsigned char UnlockTime;
        unsigned char PanelProcessRTE;
        unsigned char ChallengedDHOTime;
        unsigned char DoorProcessing;
        unsigned char RequestToExitRequired;
        unsigned char DoorHeldOpenTime;
        unsigned char AuxInputMode;
        unsigned char ReaderTamperRequired;
        unsigned char DoorAlarmTime;
        unsigned char AlarmOutputMode;
        unsigned char LEDMode;
        unsigned char ChallengedUnlockTime;
        unsigned char ProcessReaderTamperInputAsPanelInputPoint;
        unsigned char DoorCircuit;
        unsigned char RTECircuit;
        unsigned char ReaderTamperCircuit;
        unsigned char AuxCircuit;
        unsigned char DoorForceHeldTime;
        unsigned char DoNotLockOnDoorClosure;
        unsigned char InsertionReader;
        unsigned char ForceBuzzerClearsOnClosure;
        } 	RecDoorConfig;
    unsigned char UnlockModeSchedule;
    unsigned char UnlockInWindow;
    unsigned char UnlockOutWindow;
    unsigned char Arming_Level;
    unsigned char DetectWanderingPatient;
    unsigned char LockOnWanderingPatient;
    unsigned char PodTokenFormat;
    unsigned char InOutStation;
    unsigned char DoNotUnlockOnPanelRTE;
    unsigned char LogRTE;
    unsigned char EntryDetection;
    TRecReaderConfig RecReaderConfig[ 2 ];
    struct TRecDoorInterlock
        {
        unsigned char DoorNum1;
        unsigned char DoorNum2;
        unsigned char DoorNum3;
        unsigned char InterLockReq;
        unsigned char Delay;
        } 	RecDoorInterlock;
    struct TRecHeldForcedMagConfig
        {
        unsigned char HeldOpenProcessing_TxDay;
        unsigned char HeldOpenProcessing_TxStay;
        unsigned char HeldOpenProcessing_TxNight;
        unsigned char HeldOpenProcessing_SirenDay;
        unsigned char HeldOpenProcessing_SirenStay;
        unsigned char HeldOpenProcessing_SirenNight;
        unsigned char HeldOpenProcessing_SonalertDay;
        unsigned char HeldOpenProcessing_SonalertStay;
        unsigned char HeldOpenProcessing_SonalertNight;
        unsigned char ForcedOpenProcessing_TxDay;
        unsigned char ForcedOpenProcessing_TxStay;
        unsigned char ForcedOpenProcessing_TxNight;
        unsigned char ForcedOpenProcessing_SirenDay;
        unsigned char ForcedOpenProcessing_SirenStay;
        unsigned char ForcedOpenProcessing_SirenNight;
        unsigned char TurnStyle;
        unsigned char ForcedOpenProcessing_SonalertDay;
        unsigned char ForcedOpenProcessing_SonalertStay;
        unsigned char ForcedOpenProcessing_SonalertNight;
        unsigned char MagLockProcessing_TxDay;
        unsigned char MagLockProcessing_TxStay;
        unsigned char MagLockProcessing_TxNight;
        unsigned char MagLockProcessing_SirenDay;
        unsigned char MagLockProcessing_SirenStay;
        unsigned char MagLockProcessing_SirenNight;
        unsigned char MagLockProcessing_SonalertDay;
        unsigned char MagLockProcessing_SonalertStay;
        unsigned char MagLockProcessing_SonalertNight;
        unsigned char ForcedOpenProcessingSchedule;
        unsigned char HeldOpenProcessingSchedule;
        } 	RecHeldForcedMagConfig;
    } 	TRecDoor;

#define MAX_ZONEDEFS	200
#define MAX_ZONES	64
#define MAX_INTRUSION_POINTS	64
#define MAX_INTRUSION_FUNCS	16
#define ZONE_DEF_STANDARD 0
#define ZONE_DEF_INTRUSION 1
#define ZONE_CABINET_TAMPER		0x80
#define ZONE_POWER_FAILURE		0x81
#define ZONE_COMM_LOSS			0x83
#define ZONE_RDR_TAMPER			0x84
#define ZONE_RDR_FORCED_OPEN		0x85
#define ZONE_RDR_HELD_OPEN		0x86
#define ZONE_RDR_AUX_INPUT		0x87
#define ZONE_RDR_DIDDLE_ALERT	0x88
#define ZONE_RDR_DOOR_CONTACT	0x89
#define ZONE_RDR_CARD_ACCESS		0x8a
#define ZONE_RDR_AUX_INPUT2		0x8b
#define ZONE_HOST_COMM_LOSS		0x8c
#define ZONE_RDR_AUX_INPUT3		0x8d
#define ZONE_TYPE_24HOUR			0x0000
#define ZONE_TYPE_PERIMETER		0x0001
#define ZONE_TYPE_INTERIOR		0x0002
#define ZONE_MODE_NORMAL			0x0000
#define ZONE_MODE_DISABLED		0x0020
#define ZONE_ENTRY_DELAY_INSTANT		0x0000
#define ZONE_ENTRY_DELAY_TRIGGER		0x0040
#define ZONE_ENTRY_DELAY_FOLLOW		0x0080
#define ZONE_GROUP_STATE_DISARMED			1
#define ZONE_GROUP_STATE_DISARMED_FAULT		2
#define ZONE_GROUP_STATE_ARMED_AWAY			3
#define ZONE_GROUP_STATE_ARMED_STAY			4
#define ZONE_GROUP_STATE_ARMED_INSTANT		5
#define ZONE_GROUP_STATE_ARMED_ENTRY_DELAY	6
#define ZONE_GROUP_STATE_ARMING_EXIT_DELAY	7
#define ZONE_GROUP_STATE_ALARM				8
#define ZONE_GROUP_STATE_ALARM_CANCELLED		9
#define ZONE_GROUP_DISARM				1
#define ZONE_GROUP_ARM					2
#define ZONE_GROUP_POINT_MODE_CONTROL	3
#define ZONE_GROUP_ARM_AWAY_ARG			1
#define ZONE_GROUP_ARM_STAY_ARG			2
#define ZONE_GROUP_ARM_INSTANT_ARG		3
#define ZONE_GROUP_POINT_ENABLE_ARG		1
#define ZONE_GROUP_POINT_BYPASS_ARG		2
#define ZONE_GROUP_POINT_DISABLE_ARG		3
typedef struct _ZONE_DEF
    {
    int si_DeviceID;
    int si_ZoneID;
    } 	ZONE_DEF;

typedef struct _ZONE_DEF2
    {
    int si_DeviceID;
    int si_ZoneID;
    int si_IntrusionPointType;
    int si_IntrusionPointMode;
    int si_IntrusionPointEntryDelayType;
    BOOL sb_IntrusionPointChimeEnabled;
    } 	ZONE_DEF2;

typedef struct _INTRUSION_STATE_FUNCTION_LINK
    {
    int si_PanelFuncListID;
    BYTE sb_EnterMode;
    BYTE sb_ExitMode;
    } 	INTRUSION_STATE_FUNCTION_LINK;

typedef struct _ZONE_GROUP_CFGMSG
    {
    long si_PanelID;
    long sl_GroupID;
    long sl_NumZones;
    long sl_MaskLevel;
    long sl_SetMaskLevel;
    long sl_FunctionListID;
    long sl_MaskAction;
    long sl_UnmaskAction;
    ZONE_DEF ss_ZoneDef[ 200 ];
    } 	ZONE_GROUP_CFGMSG;

typedef struct _ZONE_GROUP_CFGMSG2
    {
    long si_PanelID;
    long sl_GroupID;
    long sl_NumZones;
    long sl_MaskLevel;
    long sl_SetMaskLevel;
    long sl_FunctionListID;
    long sl_MaskAction;
    long sl_UnmaskAction;
    long si_EntryDelay;
    long si_ExitDelay;
    int si_PowerUpState;
    int si_GroupType;
    short si_NumIntrusionFuncs;
    ZONE_DEF2 ss_ZoneDef[ 200 ];
    INTRUSION_STATE_FUNCTION_LINK ss_GroupFuncs[ 16 ];
    } 	ZONE_GROUP_CFGMSG2;

#define MAX_ZONEDEFS	200
#define MAX_FUNC_ARGS	6
#define MAX_IV_TERMS	24
#define IV_FUNC_TYPE_NOP					0x00
#define IV_FUNC_TYPE_ALARM_RELAY			0x01
#define IV_FUNC_TYPE_READER_RELAY		0x02
#define IV_FUNC_TYPE_AREA				0x03
#define IV_FUNC_TYPE_ZONE_GROUP			0x04
#define IV_FUNC_TYPE_EVENTLOG			0x05
#define IV_FUNC_TYPE_READER_MODE			0x06
#define IV_FUNC_TYPE_TIMEZONE			0x07
#define IV_FUNC_TYPE_TEST_ZONES			0x08
#define IV_FUNC_TYPE_CHAIN_IV			0x3F
#define IV_OP_CODE_NOP		0x00
#define IV_OP_CODE_FALSE		0x01
#define IV_OP_CODE_TRUE		0x02
#define IV_OP_CODE_PULSE		0x03
#define IV_CMD_NOP			0x00
#define IV_CMD_CLEAR			0x01
#define IV_TYPE_LOGIC_OR_EXECUTE				0x00
#define IV_TYPE_UNCONDITIONAL_EXECUTE		0x01
typedef struct _IV_FUNCTION_LIST
    {
    long si_PanelID;
    int si_IVID;
    int si_Command;
    int si_ExecuteType;
    long si_FunctionList[ 6 ];
    } 	IV_FUNCTION_LIST;

typedef struct _ACCESS_LEVELS_PER_READER
    {
    long si_PanelID;
    long sl_ReaderID;
    long sl_ReaderPath;
    long si_LastAccessLevel;
    long sl_AccessLevels[ 3073 ];
    long si_SegmentID;
    } 	ACCESS_LEVELS_PER_READER;

#define LINX_MAX_BOARDS_PER_PANEL 8
typedef struct _LINX_HARDWARE_MAP
    {
    BYTE sb_Operation;
    long si_PanelID;
    long si_ParentPanelID;
    long si_DestPanelID;
    long sl_AddressLength;
    long sl_AddressPath;
    long sl_Inventory[ 8 ];
    } 	LINX_HARDWARE_MAP;

typedef struct _LINX_PROCESS
    {
    long si_PanelID;
    BYTE sb_DevType;
    long sl_DevNum;
    long sl_Command;
    long sl_Time;
    } 	LINX_PROCESS;

typedef struct _LINX_PROCESS_DEF
    {
    BYTE sb_Operation;
    long si_PanelID;
    long si_DestPanelID;
    long sl_AddressLength;
    long sl_AddressPath;
    long sl_ProcNum;
    BYTE sb_NumDevices;
    LINX_PROCESS ss_Process[ 8 ];
    } 	LINX_PROCESS_DEF;

typedef struct _LINX_ALARM_DEF
    {
    BYTE sb_Operation;
    long si_PanelID;
    long si_DestPanelID;
    long sl_AddressLength;
    long sl_AddressPath;
    long sl_AlarmId;
    BYTE sl_ShuntMin;
    BYTE sl_ShuntSec;
    long sl_ActiveTZone;
    long sl_DevType;
    long sl_DevNum;
    long sl_EvId;
    long sl_EvType;
    long sl_ProcNum;
    long sl_AlarmType;
    } 	LINX_ALARM_DEF;

#define MAX_PANELS_PER_MACHINE 255
#define MAX_IV_LINKS_INMSG	  160
typedef struct _EVENTGROUP_MAP_MEMBER
    {
    long EventTypeID;
    long EventID;
    long TimezoneID;
    } 	EVENTGROUP_MAP_MEMBER;

typedef struct _EVENTGROUP_MAP
    {
    long si_EventGroupID;
    long numMembers;
    EVENTGROUP_MAP_MEMBER *groupMembers;
    } 	EVENTGROUP_MAP;

typedef struct _MONITORZONE_MAP_MEMBER
    {
    long panelID;
    long deviceID;
    long inputoutputID;
    long eventGroupID;
    short entireDevice;
    } 	MONITORZONE_MAP_MEMBER;

typedef struct _MONITORZONE_MAP
    {
    long si_MonitorZoneID;
    long numDevices;
    MONITORZONE_MAP_MEMBER *zoneMembers;
    } 	MONITORZONE_MAP;

typedef struct _MONITOR_STATION_ZONE_LINK
    {
    long si_MonitorStationID;
    long si_MonitorZoneID;
    int si_QueueEvents;
    int si_DeleteLink;
    } 	MONITOR_STATION_ZONE_LINK;

typedef struct _ALARMZONE_MASK_STATUSRPT
    {
    long si_PanelID;
    BYTE sb_AsyncStatus;
    BYTE sb_MaskLevel[ 64 ];
    } 	ALARMZONE_MASK_STATUSRPT;

typedef struct _INTRUSION_GROUP_POINT_STATUS
    {
    int si_PtLink;
    int si_PtFlags;
    int si_PtStatus;
    } 	INTRUSION_GROUP_POINT_STATUS;

typedef struct _INTRUSION_GROUP_POINT_LIST
    {
    INTRUSION_GROUP_POINT_STATUS sPtStatus[ 64 ];
    } 	INTRUSION_GROUP_POINT_LIST;

typedef struct _INTRUSION_MASKGROUP_STATUSRPT
    {
    long si_PanelID;
    BYTE sb_AsyncStatus;
    int si_IntrusionGroupState[ 64 ];
    int si_IntrusionGroupTotalPoints[ 64 ];
    int si_IntrusionGroupActivePoints[ 64 ];
    int si_IntrusionGroupFaultedPoints[ 64 ];
    int si_IntrusionGroupOfflinePoints[ 64 ];
    int si_IntrusionGroupBypassedPoints[ 64 ];
    int si_IntrusionGroupDisabledPoints[ 64 ];
    INTRUSION_GROUP_POINT_LIST sPtList[ 64 ];
    } 	INTRUSION_MASKGROUP_STATUSRPT;

typedef struct _INTRUSION_MASKGROUP_STATE_DEF
    {
    long si_PanelID;
    long sl_PanelNumber;
    int si_IntrusionGroupNumber;
    int si_Command;
    int si_Arg1;
    int si_Arg2;
    int si_Arg3;
    } 	INTRUSION_MASKGROUP_STATE_DEF;

#define LNL_STATUS_AREA_CLOSED	0x8000
typedef struct _AREA_STATUSRPT
    {
    long si_PanelID;
    BYTE sb_AsyncStatus;
    long sl_AreaStatus[ 64 ];
    long sl_AreaPersonCount[ 64 ];
    } 	AREA_STATUSRPT;

typedef struct _AREA_STATUSRPT_2
    {
    long si_PanelID;
    BYTE sb_AsyncStatus;
    long sl_AreaStatus[ 150 ];
    long sl_AreaPersonCount[ 150 ];
    long sl_AreaArmStatus[ 150 ];
    long sl_AreaAdditionalStatus[ 150 ];
    } 	AREA_STATUSRPT_2;

#define DIRECT_IV_EXECUTE_FUNC_NOP		0
#define DIRECT_IV_EXECUTE_FUNC_CHANGED	1
#define DIRECT_IV_EXECUTE_FUNC_FALSE		2
#define DIRECT_IV_EXECUTE_FUNC_TRUE		3
#define DIRECT_IV_EXECUTE_FUNC_PULSE		4
typedef struct _EXECUTE_IV_MSG
    {
    long si_PanelID;
    long sl_IVID;
    long sl_Function;
    BYTE sb_AlterTerms[ 24 ];
    BYTE sb_ValuesTerms[ 24 ];
    } 	EXECUTE_IV_MSG;

typedef struct _IV_LINK
    {
    int si_LUN;
    int si_DeviceID;
    int si_ZoneID;
    int si_Operation;
    int si_IV;
    int si_Term;
    } 	IV_LINK;

typedef struct _ZONE_IV_LINKAGE
    {
    long si_PanelID;
    long si_NumLinks;
    IV_LINK ss_IVLink[ 160 ];
    } 	ZONE_IV_LINKAGE;

typedef struct _RESET_CARDHOLDER_USELIMITS
    {
    long si_PanelID;
    DWORD sl_CardNumber;
    BYTE sb_UseLimit;
    } 	RESET_CARDHOLDER_USELIMITS;

typedef struct _RESET_CARDHOLDER_USELIMITS2
    {
    long si_PanelID;
    __int64 sl_CardNumber;
    BYTE sb_UseLimit;
    } 	RESET_CARDHOLDER_USELIMITS2;

typedef struct _GRANT_ONE_FREE_PASS
    {
    long si_PanelID;
    DWORD sl_CardNumber;
    } 	GRANT_ONE_FREE_PASS;

typedef struct _GRANT_ONE_FREE_PASS2
    {
    long si_PanelID;
    __int64 sl_CardNumber;
    } 	GRANT_ONE_FREE_PASS2;

typedef struct _ADD_ASSET
    {
    long si_PanelID;
    long si_SegmentID;
    BYTE sb_AssetScanID[ 32 ];
    __int64 sl_AssetOwners[ 8 ];
    long sl_AssetArgs[ 16 ];
    } 	ADD_ASSET;

typedef struct _DEL_ASSET
    {
    long si_PanelID;
    long si_SegmentID;
    BYTE sb_AssetScanID[ 32 ];
    } 	DEL_ASSET;

typedef struct _ADD_ASSET_GROUP
    {
    long si_PanelID;
    long si_SegmentID;
    long si_AssetGroupID;
    long si_AssetTypes[ 64 ];
    } 	ADD_ASSET_GROUP;

#define VIDCOMMAND_NOOP			0
#define VIDCOMMAND_START			1
#define VIDCOMMAND_END			2
#define VIDCOMMAND_ARCHIVE		3
#define VIDCOMMAND_EVRECORD_NOOP			4
#define VIDCOMMAND_EVRECORD_START		5
#define VIDCOMMAND_EVRECORD_END			6
#define MOTIONMASK_WIDTH			32
#define MOTIONMASK_HEIGHT		24
#define TIME_LAPSE_RECORD_MODE_DISABLED	0
#define TIME_LAPSE_RECORD_MODE_MOTION	1
typedef struct _VIDEO_RECORDER_DATA
    {
    long ServerID;
    long VideoStandard;
    long TimeLapseMSPerFrame;
    long TimeLapsePreRollMS;
    } 	VIDEO_RECORDER_DATA;

typedef struct _VIDEO_RECORDER_DATA2
    {
    long ServerID;
    long VideoStandard;
    long TimeLapseMSPerFrame;
    long TimeLapsePreRollMS;
    long VideoSize;
    long AutoDeleteDays;
    long vrd2DaysKeepVideo;
    long vrd2CPUTimezone;
    long vrd2CPUThreshold;
    long vrd2NetworkTimezone;
    long vrd2NetworkThreshold;
    long vrd2HDDReadTimezone;
    long vrd2HDDReadThreshold;
    long vrd2HDDWriteTimezone;
    long vrd2HDDWriteThreshold;
    long vrd2LiveClients;
    long vrd2RecordedClientsByDevice;
    long vrd2AutoClockSynch;
    DWORD Reserved3;
    DWORD Reserved4;
    } 	VIDEO_RECORDER_DATA2;

typedef struct _INTELLIGENT_VIDEO_CHANNEL
    {
    long ivcServerID;
    long ivcRecorderType;
    long ivcRecorderID;
    wchar_t ivcRecorderIP[ 128 ];
    wchar_t ivcRecorderUserName[ 32 ];
    wchar_t ivcRecorderPassword[ 32 ];
    long ivcChannel;
    long ivcCameraID;
    long ivcFailoverID;
    wchar_t ivcFailoverIP[ 128 ];
    wchar_t ivcFailoverUserName[ 32 ];
    wchar_t ivcFailoverPassword[ 32 ];
    long ivcFailoverChannel;
    long ivcEventsCount;
    long ivcChannelConfigCount;
    long ivcReserved1;
    long ivcReserved2;
    long ivcReserved3;
    long ivcReserved4;
    wchar_t ivcUrlRecorderType[ 32 ];
    } 	INTELLIGENT_VIDEO_CHANNEL;

typedef struct _INTELLIGENT_VIDEO_APPLICATION
    {
    long ivaServerID;
    long ivaApplicationID;
    GUID ivaApplicationType;
    BYTE ivaApplicationOnline;
    long ivaApplicationConfigSize;
    long ivaReserved1;
    long ivaReserved2;
    long ivaReserved3;
    long ivaReserved4;
    } 	INTELLIGENT_VIDEO_APPLICATION;

typedef struct CAMERA_INPUT_DATA
    {
    long cidServerID;
    long cidCameraID;
    long cidChannel;
    long cidInputNumber;
    long cidSupervision;
    long cidTimeZone;
    BYTE cidOnline;
    } 	CAMERA_INPUT_DATA;

typedef struct CAMERA_OUTPUT_DATA
    {
    long codServerID;
    long codCameraID;
    long codOutputNumber;
    } 	CAMERA_OUTPUT_DATA;

typedef struct _CAMERA_DATA
    {
    long ServerID;
    long CameraID;
    long Channel;
    wchar_t Title[ 128 ];
    long cdEventPostroll;
    long Bright;
    long Contrast;
    long Color;
    long Hue;
    long FrameRate;
    long IntraFrameRate;
    long MotionBitRate;
    long NonMotionBitRate;
    long InMotionLevel;
    long PreRoll;
    long PostRoll;
    long State;
    BYTE MotionDetection;
    long MotionTimeZone;
    BYTE MotionMask[ 32 ][ 24 ];
    BYTE ContArchive;
    long ContArchiveTimeZone;
    } 	CAMERA_DATA;

typedef struct _CAMERA_DATA2
    {
    long ServerID;
    long CameraID;
    long Channel;
    wchar_t Title[ 128 ];
    long cdEventPostroll;
    long Bright;
    long Contrast;
    long Color;
    long Hue;
    long FrameRate;
    long IntraFrameRate;
    long MotionBitRate;
    long NonMotionBitRate;
    long InMotionLevel;
    long PreRoll;
    long PostRoll;
    long State;
    BYTE MotionDetection;
    long MotionTimeZone;
    BYTE MotionMask[ 32 ][ 24 ];
    BYTE ContArchive;
    long ContArchiveTimeZone;
    long Quality;
    long TimeLapseRecordMode;
    BYTE SurveillanceOnly;
    long IPAddress;
    BYTE Online;
    DWORD cdPortNumber;
    DWORD cdStaticTimeZone;
    DWORD cdCriteriaCount;
    DWORD cdBlindCamTimeZone;
    } 	CAMERA_DATA2;

typedef struct _CAMERA_DATA3
    {
    long ServerID;
    long CameraID;
    long Channel;
    wchar_t Title[ 128 ];
    long cdEventPostroll;
    long Bright;
    long Contrast;
    long Color;
    long Hue;
    long FrameRate;
    long IntraFrameRate;
    long MotionBitRate;
    long NonMotionBitRate;
    long InMotionLevel;
    long PreRoll;
    long PostRoll;
    long State;
    BYTE MotionDetection;
    long MotionTimeZone;
    BYTE MotionMask[ 32 ][ 24 ];
    BYTE ContArchive;
    long ContArchiveTimeZone;
    long Quality;
    long TimeLapseRecordMode;
    BYTE SurveillanceOnly;
    long IPAddress;
    BYTE Online;
    DWORD cdPortNumber;
    DWORD cdStaticTimeZone;
    DWORD cdCriteriaCount;
    DWORD cdBlindCamTimeZone;
    CLSID CameraGUID;
    long CameraType;
    wchar_t UserName[ 32 ];
    wchar_t Password[ 32 ];
    SIZE Resolution;
    short Rotation;
    BYTE DisplayTitle;
    BYTE DisplayDate;
    BYTE DisplayTime;
    long Monochrome;
    long Sharpness;
    long Gamma;
    long WhiteBalance;
    long BacklightComp;
    long Exposure;
    long Iris;
    } 	CAMERA_DATA3;

typedef struct EVENT_CRITERIA
    {
    BYTE ecTriggerEventRecording;
    long ecCriteriaID;
    long ecProcessorID;
    long ecTimeZoneID;
    BYTE ecTriggerAlarm;
    } 	EVENT_CRITERIA;

typedef struct EVENT_CRITERIA2
    {
    BYTE ecTriggerEventRecording;
    long ecCriteriaID;
    long ecProcessorID;
    long ecTimeZoneID;
    BYTE ecTriggerAlarm;
    wchar_t ecName[ 64 ];
    long ecConfigSize;
    long ecThreshold;
    long ecIntelligentVideoServerID;
    long ecChannelInstanceID;
    } 	EVENT_CRITERIA2;

typedef struct CHANNEL_CONFIG
    {
    long ccInstanceID;
    long ccSize;
    } 	CHANNEL_CONFIG;

typedef /* [switch_type] */ union CD4
    {
    struct __MIDL___MIDL_itf_GenericIntercomTranslator_0000_0000_0002
        {
        long cdPrimaryRecorderID;
        long cdPrimaryChannel;
        long cdFailOverRecorderIP;
        } 	;
    EVENT_CRITERIA cdReserved5[ 5 ];
    } 	CD4;

typedef struct _CAMERA_DATA4
    {
    long ServerID;
    long CameraID;
    long Channel;
    wchar_t Title[ 128 ];
    long cdEventPostroll;
    long Bright;
    long Contrast;
    long Color;
    long Hue;
    long FrameRate;
    long IntraFrameRate;
    long MotionBitRate;
    long NonMotionBitRate;
    long InMotionLevel;
    long PreRoll;
    long PostRoll;
    long State;
    BYTE MotionDetection;
    long MotionTimeZone;
    BYTE MotionMask[ 32 ][ 24 ];
    BYTE ContArchive;
    long ContArchiveTimeZone;
    long Quality;
    long TimeLapseRecordMode;
    BYTE SurveillanceOnly;
    long IPAddress;
    BYTE Online;
    DWORD cdPortNumber;
    DWORD cdStaticTimeZone;
    DWORD cdCriteriaCount;
    DWORD cdBlindCamTimeZone;
    CLSID CameraGUID;
    long CameraType;
    wchar_t UserName[ 32 ];
    wchar_t Password[ 32 ];
    SIZE Resolution;
    short Rotation;
    BYTE DisplayTitle;
    BYTE DisplayDate;
    BYTE DisplayTime;
    long Monochrome;
    long Sharpness;
    long Gamma;
    long WhiteBalance;
    long BacklightComp;
    long Exposure;
    long Iris;
    wchar_t SourceName[ 32 ];
    wchar_t Standard[ 32 ];
    long InputNumber;
    long EventFrameRateMs;
    long EventResolution;
    long EventQuality;
    long EventPreroll;
    long LiveFrameRate;
    long AudioChannel;
    long cd4Bitrate;
    BYTE btMotionTriggersEventRecording;
    CD4 cd4;
    long cd4AudioVolume;
    long cd4AudioSource;
    BYTE cd4AudioRecording;
    BYTE cd4EventAudioRecording;
    BYTE cd4RecordOnEvent;
    BYTE cd4UseCameraTimeStamps;
    BYTE cd4OnCameraRecordingActivated;
    long cd4OnCameraRecordingTimeZone;
    long cd4FTPPortNumber;
    wchar_t cd4FTPRoot[ 32 ];
    long cd4RecordingTimeZone;
    BYTE cd4AllowAudioIntercom;
    BYTE cd4AllowDirectConnect;
    long cd4DirectConnectTimeout;
    long cd4RTPProtocol;
    long cd4RTSPPort;
    long cd4Redundant;
    } 	CAMERA_DATA4;

typedef struct _CAMERA_PTZ_PRESET
    {
    int PresetIndex;
    wchar_t PresetName[ 512 ];
    } 	CAMERA_PTZ_PRESET;

#define OBJECT_MAP_TYPE_ALL          -1
#define OBJECT_MAP_TYPE_CAMERA        0
#define OBJECT_MAP_TYPE_PTZPRESET     1

enum LNR_FEATURE_FLAGS
    {	Feature_Unsupported	= -2,
	Feature_RemoveFlag	= -1
    } ;

enum CAMERA_CAPS_FLAGS
    {	Camera_Prop_Auto	= 1,
	Camera_Prop_Manual	= 2
    } ;
typedef struct _CAMERA_PROP_RANGE
    {
    long Min;
    long Max;
    long Step;
    long Default;
    BYTE Flags;
    } 	CAMERA_PROP_RANGE;

typedef struct _RESOLUTION_RANGE
    {
    CAMERA_PROP_RANGE Width;
    CAMERA_PROP_RANGE Height;
    } 	RESOLUTION_RANGE;

typedef struct _DISCRETE_VALUE
    {
    long dvId;
    wchar_t dvName[ 256 ];
    } 	DISCRETE_VALUE;

typedef struct _DISCRETE_TEXT_PAIR
    {
    wchar_t dvName[ 256 ];
    wchar_t dvValue[ 256 ];
    } 	DISCRETE_TEXT_PAIR;

typedef struct _CAMERA_CAPS3
    {
    long ServerID;
    long CameraID;
    long Channel;
    BYTE Title;
    BYTE MotionDetect;
    CAMERA_PROP_RANGE Bright;
    CAMERA_PROP_RANGE Contrast;
    CAMERA_PROP_RANGE Color;
    CAMERA_PROP_RANGE Hue;
    CAMERA_PROP_RANGE Quality;
    BYTE UserName;
    BYTE Password;
    CAMERA_PROP_RANGE Rotation;
    BYTE DisplayTitle;
    BYTE DisplayDate;
    BYTE DisplayTime;
    BYTE Monochrome;
    CAMERA_PROP_RANGE Sharpness;
    CAMERA_PROP_RANGE Gamma;
    CAMERA_PROP_RANGE WhiteBalance;
    CAMERA_PROP_RANGE BacklightComp;
    CAMERA_PROP_RANGE Exposure;
    CAMERA_PROP_RANGE Iris;
    short nResolutions;
    SIZE *pResolutions;
    CAMERA_PROP_RANGE FrameRate;
    CAMERA_PROP_RANGE IntraFrameRate;
    } 	CAMERA_CAPS3;

typedef struct CAMERA_CAPS4
    {
    CAMERA_CAPS3 cc3;
    long InputNumber;
    short nRangeResolutionItemsCount;
    RESOLUTION_RANGE *pRangeResolutions;
    long nDefaultResolutionID;
    short nFrameRateItemsCount;
    CAMERA_PROP_RANGE *pFrameRates;
    BYTE ccPort;
    long ccDefaultPort;
    long cc4VideoOnEventMaxPreroll;
    BYTE cc4VideoOnEvent;
    BYTE cc4CameraTimeStamps;
    BYTE cc4Reserved;
    BYTE cc4Audio;
    CAMERA_PROP_RANGE cc4AudioQuality;
    CAMERA_PROP_RANGE cc4AudioVolumes;
    short cc4AudioSourcesItemsCount;
    DISCRETE_VALUE *cc4pAudioSources;
    long cc4DefaultAudioSource;
    CAMERA_PROP_RANGE cc4Inputs;
    CAMERA_PROP_RANGE cc4Outputs;
    BYTE cc4CameraMemory;
    BYTE cc4CameraMemoryPort;
    CAMERA_PROP_RANGE cc4CameraMemoryPortRange;
    BYTE cc4CameraMemoryRoot;
    short cc4CameraMemoryRootsItemsCount;
    DISCRETE_TEXT_PAIR *cc4pCameraMemoryRoots;
    BYTE cc4CameraMotionDetect;
    short cc4BitrateItemsCount;
    DISCRETE_VALUE *cc4pBitrateValues;
    long cc4DefaultBitrate;
    BYTE cc4AllowAudioIntercom;
    BYTE cc4AllowIP;
    BYTE cc4CameraRTP;
    short cc4CameraProtocolItemsCount;
    DISCRETE_VALUE *cc4CameraProtocolValues;
    long cc4DefaultProtocol;
    BYTE cc4CameraRTSPPort;
    long cc4CameraRTSPDefaultPort;
    } 	CAMERA_CAPS4;

typedef struct CHANGE_PASSWORD_CAPS
    {
    BYTE Supported;
    DWORD PasswordMinLength;
    DWORD PasswordMaxLength;
    BYTE PasswordCharSet[ 32 ];
    } 	CHANGE_PASSWORD_CAPS;

typedef struct CAMERA_CAPS5
    {
    CAMERA_CAPS4 cc4;
    CHANGE_PASSWORD_CAPS ChangePassword;
    DWORD Reserved1;
    DWORD Reserved2;
    } 	CAMERA_CAPS5;

typedef struct RECORDER_CAPS
    {
    CHANGE_PASSWORD_CAPS ChangePassword;
    DWORD Reserved1;
    DWORD Reserved2;
    } 	RECORDER_CAPS;

typedef struct IVS_CAPS
    {
    CHANGE_PASSWORD_CAPS ChangePassword;
    DWORD Reserved1;
    DWORD Reserved2;
    } 	IVS_CAPS;

typedef struct _MONITOR_DATA
    {
    long ServerID;
    long MonitorID;
    long Channel;
    wchar_t Title[ 128 ];
    long Brightness;
    long Color;
    long PlaySpeed;
    long DisplayTitle;
    long DisplayDate;
    long DisplayTime;
    long DisplayPlayStatus;
    long DisplayBarGenerator;
    long State;
    } 	MONITOR_DATA;

typedef struct _TRANSMITTER_DEF
    {
    long PanelID;
    long TransmitterDBID;
    long Type;
    long BaseID;
    BYTE AlwaysMask;
    long MaskTZ;
    BYTE DeleteFlag;
    BYTE TamperEnabled;
    BYTE SupervisionEnabled;
    BYTE RestoralEnabled;
    } 	TRANSMITTER_DEF;

typedef struct _BUS_DEVICE_CFG
    {
    long PanelID;
    long SiteID;
    long BusDeviceID;
    long Command;
    long Data;
    } 	BUS_DEVICE_CFG;

typedef struct _ADD_BIOMETRIC_TEMPLATE
    {
    long PanelID;
    long si_SegmentID;
    __int64 Card_Number;
    long BioType;
    long Flags;
    long MinScore;
    long TemplateSize;
    BYTE BioTemplate[ 2000 ];
    } 	ADD_BIOMETRIC_TEMPLATE;

#define RECEIVER_ACCOUNTNUM_LENGTH			20
#define EVENT_CODE_LENGTH					10
typedef struct _RECEIVER_ACCOUNT_INFO
    {
    wchar_t AccountNum[ 20 ];
    long AccountID;
    long TemplateID;
    short ExpectedEventFlag;
    long ExpectedEventDuration;
    long deleteAccount;
    long scheduledEventsTimezoneID;
    } 	RECEIVER_ACCOUNT_INFO;

typedef struct _EVENT_CODE_TEMPLATE_DEF
    {
    long TemplateID;
    long baseTemplateID;
    long deleteTemplate;
    } 	EVENT_CODE_TEMPLATE_DEF;

typedef struct _LENEL_EOL_TABLE_ENTRY
    {
    long ReportingPriority;
    long StatusCode;
    long ResistanceCode1;
    long ResistanceCode2;
    } 	LENEL_EOL_TABLE_ENTRY;

typedef struct _LENEL_EOL_TABLE
    {
    long PanelID;
    long SegmentID;
    long TableID;
    LENEL_EOL_TABLE_ENTRY entries[ 8 ];
    } 	LENEL_EOL_TABLE;

#define MAX_INTERCOM_DEVICE_IDENTIFIER_LENGTH	64
typedef struct _INTERCOM_STATION_DEF
    {
    long PanelID;
    long StationNum;
    long StationType;
    BYTE DeleteStation;
    long ExternalNumber;
    wchar_t DeviceIdentifier[ 64 ];
    } 	INTERCOM_STATION_DEF;

typedef struct _READER_INFO
    {
    long CtrlType;
    long altRdrID;
    short altRdrSpec;
    short rexEvents;
    } 	READER_INFO;

#define SNMP_AGENTADDRESS_LENGTH			20
typedef struct _SNMP_AGENT_INFO
    {
    wchar_t AgentAddress[ 20 ];
    long AgentID;
    long deleteAgent;
    } 	SNMP_AGENT_INFO;

#define MAX_SNMP_COMMUNITY_LEN			20
#define CAMERA_OUTPUT_OFFSET					1000
#define CAMERA_OUTPUT_DEACTIVATE				    0
#define CAMERA_OUTPUT_PULSE						1
#define CAMERA_OUTPUT_ACTIVATE					2
// Defines for the IVSERVER structure
#define IVSERVER_USERNAME_LEN	256
#define IVSERVER_PASSWORD_LEN	256
#define IVSERVER_ADRESS_LEN		64
typedef struct _IVSERVER
    {
    long si_PanelID;
    long si_SegmentID;
    long si_IVServerID;
    wchar_t si_Address[ 64 ];
    wchar_t si_UserName[ 256 ];
    wchar_t si_Password[ 256 ];
    } 	IVSERVER;

/*
 * These defines are used to indicate what information is required
 * to be sent during a database download to the hardware.  The order
 * in which they appear in the structure is the order in which they 
 * are sent.
 *
 */
#define DATABASE_DOWNLOAD_NONE						0x00
#define DATABASE_DOWNLOAD_ACCESS_PANEL				0x01
#define DATABASE_DOWNLOAD_DOWNLOAD_RECORDS			0x02
#define DATABASE_DOWNLOAD_CARD_FORMATS				0x03
#define DATABASE_DOWNLOAD_ALARM_PANELS				0x04
#define DATABASE_DOWNLOAD_HOLIDAYS					0x05
#define DATABASE_DOWNLOAD_TIMEZONES					0x06
#define DATABASE_DOWNLOAD_ELEVATOR_ACCESS_LEVELS		0x07
#define DATABASE_DOWNLOAD_ACCESS_LEVELS				0x08
#define DATABASE_DOWNLOAD_AREA_SPEC_TABLE			0x09
#define DATABASE_DOWNLOAD_AREAS						0x0A
#define DATABASE_DOWNLOAD_CARDHOLDERS				0x0B
#define DATABASE_DOWNLOAD_AREA_RESET					0x0C
#define DATABASE_DOWNLOAD_ALARM_MASK_GROUPS			0x0D
#define DATABASE_DOWNLOAD_ASSETS						0x0E
#define DATABASE_DOWNLOAD_ASSET_GROUPS				0x0F
#define DATABASE_DOWNLOAD_READERS					0x10
#define DATABASE_DOWNLOAD_READER_TIMEZONE_LIST		0x11
#define DATABASE_DOWNLOAD_FUNCTION_LISTS				0x12
#define DATABASE_DOWNLOAD_FUNCTION_LINKS				0x13
#define DATABASE_DOWNLOAD_TIMEZONES_AND_ACTIVATE		0x14
#define DATABASE_DOWNLOAD_VIDEO_PANEL				0x15
#define DATABASE_DOWNLOAD_CAMERAS					0x16
#define DATABASE_DOWNLOAD_MONITORS					0x17
#define DATABASE_DOWNLOAD_TRANSMITTERS				0x18
#define DATABASE_DOWNLOAD_SET_CLOCK					0x19
#define DATABASE_DOWNLOAD_RESET						0x1A
#define DATABASE_DOWNLOAD_START_POLLING				0x1B
#define DATABASE_DOWNLOAD_ALARM_PANEL_CFG			0x1C
#define DATABASE_DOWNLOAD_SET_CLOCK_AT_END			0x1D
#define DATABASE_DOWNLOAD_READER_PANEL_PARAMETERS    0x1E
#define DATABASE_DOWNLOAD_SWITCHER_PANEL				0x1F
#define DATABASE_DOWNLOAD_EXTENDED_HELD_COMMAND		0x20
#define DATABASE_DOWNLOAD_EOL_TABLES					0x21
#define DATABASE_DOWNLOAD_EXTENDED_OPTIONS			0x25
#define DATABASE_DOWNLOAD_IVS_PANEL					0x26
#define DATABASE_DOWNLOAD_IVS_CHANNELS				0x27
#define DATABASE_DOWNLOAD_AREA_TIMEZONE_LIST			0x28
#define DATABASE_DOWNLOAD_IVAS_PANEL					0x29
#define DATABASE_DOWNLOAD_IVAS_CHANNELS				0x2A
#define DATABASE_DOWNLOAD_ONDEMAND_BADGE_DELETE		0x2B
#define DATABASE_DOWNLOAD_ARM_DISARM_COMMAND			0x2C
#define DATABASE_DOWNLOAD_READER_KEYPAD_MACROS		0x2D
#define DATABASE_DOWNLOAD_READER_KEYPAD_DISPLAY		0x2E
#define DATABASE_DOWNLOAD_SAVE_DATABASE				0x2F
#define DATABASE_DOWNLOAD_EXPIRED_ONDEMAND_BADGE		0x30
#define DATABASE_DOWNLOAD_INTRUSION_COMMAND			0x31
#define DATABASE_DOWNLOAD_USERS						0x32
#define DATABASE_DOWNLOAD_PANEL_CARD_FORMATS			0x33
#define DATABASE_DOWNLOAD_PERMISSION_PROFILES		0x34
#define DATABASE_DOWNLOAD_AUTHORITIES				0x35
#define DATABASE_DOWNLOAD_AREA_GROUPS				0x36
#define INTERCOM_OUTPUT_MODE_OFF		0x00
#define INTERCOM_OUTPUT_MODE_ON		0x01
#define INTERCOM_OUTPUT_MODE_PULSE	0x02
#define LOCAL_APB_OPEN_AREA_CMD	0x02
#define LOCAL_APB_CLOSE_AREA_CMD	0x01
#define MONITORSTATION_TYPE_MONITOR			0
#define MONITORSTATION_TYPE_LINKAGESERVER	1
typedef 
enum _CommandType
    {	eInitialize	= 0,
	ePanLeft	= ( eInitialize + 1 ) ,
	ePanRight	= ( ePanLeft + 1 ) ,
	eTiltUp	= ( ePanRight + 1 ) ,
	eTiltDown	= ( eTiltUp + 1 ) ,
	eZoomIn	= ( eTiltDown + 1 ) ,
	eZoomOut	= ( eZoomIn + 1 ) ,
	eFocusNear	= ( eZoomOut + 1 ) ,
	eFocusFar	= ( eFocusNear + 1 ) ,
	eIrisOpen	= ( eFocusFar + 1 ) ,
	eIrisClose	= ( eIrisOpen + 1 ) ,
	eStopCamera	= ( eIrisClose + 1 ) ,
	eSelectPreset	= ( eStopCamera + 1 ) ,
	eSwitchTo	= ( eSelectPreset + 1 ) ,
	eSelectCamera	= ( eSwitchTo + 1 ) 
    } 	CommandType;

typedef 
enum _LnlDownloadFirmwareStatus
    {	dfsDownloadError	= -1,
	dfsDownloadInProgress	= 0,
	dfsDownloadComplete	= 1
    } 	LnlDownloadFirmwareStatus;

typedef struct _DEVICE_STATUS
    {
    DWORD deviceID;
    DWORD deviceStatus;
    } 	DEVICE_STATUS;

#define INTERCOM_DEVICE_STATUS_FLAG_ONLINE		0x1
#define INTERCOM_DEVICE_STATUS_FLAG_BUSY		0x2
#define INTERCOM_DEVICE_STATUS_FLAG_BLOCKED		0x4

#pragma pack()
typedef struct _GeneralInfoStructType
    {
    unsigned char OnString[ 12 ];
    unsigned char OffString[ 12 ];
    unsigned char EditorName[ 40 ];
    unsigned char VersionString[ 10 ];
    int SubMenuList[ 20 ];
    unsigned char *OLAMemory;
    int VariantID;
    int SystemID;
    unsigned char WNumberAsString[ 10 ];
    int SizeOfPSU;
    unsigned char PCST_Level;
    int HelpFileIndex;
    unsigned char HelpFileName[ 20 ];
    } 	GeneralInfoStructType;

typedef /* [public][public] */ struct __MIDL___MIDL_itf_GenericIntercomTranslator_0000_0000_0003
    {
    unsigned int UpperLimit;
    unsigned int LowerLimit;
    } 	Limits_t;

typedef struct _ColumnInfoStructType
    {
    unsigned char ColumnType;
    unsigned char ColumnName[ 15 ];
    unsigned char ColumnTitle[ 15 ];
    Limits_t Limits;
    unsigned int TextMaxLength;
    unsigned int DisplayOptions;
    unsigned char *OptionList;
    } 	ColumnInfoStructType;

typedef struct _SUPER_ColumnInfoListStructType
    {
    unsigned char headerList[ 6240 ];
    } 	SUPER_ColumnInfoStructType;

typedef struct _SectionListStructType
    {
    unsigned char SectionTitle[ 20 ];
    unsigned char SectionIndex;
    unsigned char DBFName[ 15 ];
    int HelpIndex;
    } 	SectionListStructType;

typedef struct _SUPER_SectionListStructType
    {
    SectionListStructType sectionList[ 120 ];
    } 	SUPER_SectionListStructType;

typedef struct _DLLPanelSizeData
    {
    int SetGroupSize;
    int KeypadSize;
    int ConcSize;
    int CircuitSize;
    int UserSize;
    int AreaSize;
    int DoorSize;
    int OPMSize;
    int DormitoriesSize;
    int SDISize;
    int SerialModules;
    int Schedules;
    int SDSize;
    int PanelUserSize;
    int Outputs;
    int CameraSize;
    int XIBDetSize;
    int HSSchedules;
    } 	DLLPanelSizeData;

#endif //LMSGTYPE_H


extern RPC_IF_HANDLE __MIDL_itf_GenericIntercomTranslator_0000_0000_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_GenericIntercomTranslator_0000_0000_v0_0_s_ifspec;

#ifndef __IDistributeEvent_INTERFACE_DEFINED__
#define __IDistributeEvent_INTERFACE_DEFINED__

/* interface IDistributeEvent */
/* [uuid][object] */ 


EXTERN_C const IID IID_IDistributeEvent;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("2C3C6821-61FC-11d2-8B80-00A0C91FDE82")
    IDistributeEvent : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE Lnl_DistributeLnlMessage( 
            /* [in] */ OLDLNLMESSAGE *Message) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_StartDatabaseThread( 
            /* [in] */ int PanelID,
            /* [in] */ BYTE *DownloadArray) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_DistributeDisplayTextMessage( 
            /* [in] */ BSTR Message) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_WriteDownloadError( 
            /* [in] */ int vi_PanelID,
            /* [in] */ BYTE *pro_Ptr,
            /* [in] */ int vi_DataSize) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_DistributeCurrentVideoPosition( 
            /* [in] */ long ErrorCode,
            /* [in] */ long CurrentPosition) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_PanelCommunicationType( 
            /* [in] */ int PanelID,
            /* [out] */ int *CommType) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_IsTimezoneActive( 
            /* [in] */ long timeZone,
            /* [out] */ BOOL *active) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_StartReaderDownload( 
            /* [in] */ int PanelID,
            /* [in] */ int numReaders,
            /* [in] */ BYTE *ReaderArray,
            /* [in] */ BYTE *DownloadArray) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_AbortDatabaseThread( 
            /* [in] */ int PanelID) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_WorkstationLookup( 
            /* [in] */ BSTR WorkstationName,
            /* [out] */ int *WorkstationID) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_DistributeAccountMessage( 
            /* [in] */ BSTR AccountNum,
            /* [in] */ BSTR DefaultAccountName,
            /* [in] */ OLDLNLMESSAGE *Message,
            /* [in] */ BSTR TextInfo,
            /* [in] */ long DefaultTemplate) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_DistributeLnlMessageWithText( 
            /* [in] */ OLDLNLMESSAGE *Message,
            /* [in] */ BSTR TextInfo) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_HostBasedDecisionRequest( 
            /* [in] */ long panelID,
            /* [in] */ long readerID,
            /* [in] */ __int64 badgeID,
            /* [in] */ long time) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_UpdatePanelEventIndex( 
            /* [in] */ long panelID,
            /* [in] */ long eventIndex) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_LookupReaderInfo( 
            /* [in] */ long panelID,
            /* [in] */ long readerID,
            /* [out] */ READER_INFO *readerInfo) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_LookupWorldTZ( 
            /* [in] */ long panelID,
            /* [out] */ TZI *tzInfo) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_DistributeLnlMessageEx( 
            /* [in] */ LNLMESSAGE *Message) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_DistributeAccountMessageEx( 
            /* [in] */ BSTR AccountNum,
            /* [in] */ BSTR DefaultAccountName,
            /* [in] */ LNLMESSAGE *Message,
            /* [in] */ BSTR TextInfo,
            /* [in] */ long DefaultTemplate) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_DistributeLnlMessageWithTextEx( 
            /* [in] */ LNLMESSAGE *Message,
            /* [in] */ BSTR TextInfo) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_DistributeSNMPAgentMessageEx( 
            /* [in] */ BSTR Address,
            /* [in] */ BSTR DefaultAgentName,
            /* [in] */ LNLMESSAGE *Message,
            /* [in] */ BSTR TextInfo,
            /* [in] */ long ManagerID) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_DistributeOPCMessageEx( 
            /* [in] */ BSTR OPCSource,
            /* [in] */ LNLMESSAGE *Message,
            /* [in] */ BSTR TextInfo) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_UpdateRecordedFlashSize( 
            /* [in] */ long panelID,
            /* [in] */ long newFlashSize) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_UpdateRecordedDipSwitchSettings( 
            /* [in] */ long panelID,
            /* [in] */ long newDipSwitchSettings) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_UpdateRecordedFirmwareRev( 
            /* [in] */ long panelID,
            /* [in] */ BSTR newFirmwareRev) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_UpdateNextConnectionDowngradable( 
            /* [in] */ long panelID,
            /* [in] */ BOOL vb_NextConnectionDowngradable) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_UpdateMasterKeyUpdatePending( 
            /* [in] */ long panelID,
            /* [in] */ BOOL vb_MasterKeyUpdatePending) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_UpdateClearPreviousMasterKey( 
            /* [in] */ long panelID) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_LookupMasterKey( 
            /* [in] */ long panelID,
            /* [in] */ long segmentID,
            /* [in] */ long whichkey,
            /* [in] */ long allocatedBytes,
            /* [out] */ long *MasterKeySize,
            /* [out] */ BYTE *MasterKey) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_IsSelectiveDownloadPanel( 
            /* [in] */ long panelID,
            /* [out] */ BOOL *selectiveDownloadPanel) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_UpdateBadgeRemainingUses( 
            /* [in] */ long panelID,
            /* [in] */ __int64 badgeID,
            /* [in] */ int usesLeft,
            /* [in] */ BOOL updateFailedRPC) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_LookupFIPSModeMasterKey( 
            /* [in] */ long whichkey,
            /* [in] */ long allocatedBytes,
            /* [out] */ long *MasterKeySize,
            /* [out] */ BYTE *MasterKey) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_LookupFIPSModeParameters( 
            /* [in] */ long panelID,
            /* [out] */ FIPS_MODE_CONTROLLER_ENCRTYPTION_PARAMS *fipsPARAMS) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IDistributeEventVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IDistributeEvent * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IDistributeEvent * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IDistributeEvent * This);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_DistributeLnlMessage )( 
            IDistributeEvent * This,
            /* [in] */ OLDLNLMESSAGE *Message);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_StartDatabaseThread )( 
            IDistributeEvent * This,
            /* [in] */ int PanelID,
            /* [in] */ BYTE *DownloadArray);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_DistributeDisplayTextMessage )( 
            IDistributeEvent * This,
            /* [in] */ BSTR Message);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_WriteDownloadError )( 
            IDistributeEvent * This,
            /* [in] */ int vi_PanelID,
            /* [in] */ BYTE *pro_Ptr,
            /* [in] */ int vi_DataSize);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_DistributeCurrentVideoPosition )( 
            IDistributeEvent * This,
            /* [in] */ long ErrorCode,
            /* [in] */ long CurrentPosition);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_PanelCommunicationType )( 
            IDistributeEvent * This,
            /* [in] */ int PanelID,
            /* [out] */ int *CommType);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_IsTimezoneActive )( 
            IDistributeEvent * This,
            /* [in] */ long timeZone,
            /* [out] */ BOOL *active);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_StartReaderDownload )( 
            IDistributeEvent * This,
            /* [in] */ int PanelID,
            /* [in] */ int numReaders,
            /* [in] */ BYTE *ReaderArray,
            /* [in] */ BYTE *DownloadArray);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_AbortDatabaseThread )( 
            IDistributeEvent * This,
            /* [in] */ int PanelID);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_WorkstationLookup )( 
            IDistributeEvent * This,
            /* [in] */ BSTR WorkstationName,
            /* [out] */ int *WorkstationID);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_DistributeAccountMessage )( 
            IDistributeEvent * This,
            /* [in] */ BSTR AccountNum,
            /* [in] */ BSTR DefaultAccountName,
            /* [in] */ OLDLNLMESSAGE *Message,
            /* [in] */ BSTR TextInfo,
            /* [in] */ long DefaultTemplate);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_DistributeLnlMessageWithText )( 
            IDistributeEvent * This,
            /* [in] */ OLDLNLMESSAGE *Message,
            /* [in] */ BSTR TextInfo);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_HostBasedDecisionRequest )( 
            IDistributeEvent * This,
            /* [in] */ long panelID,
            /* [in] */ long readerID,
            /* [in] */ __int64 badgeID,
            /* [in] */ long time);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_UpdatePanelEventIndex )( 
            IDistributeEvent * This,
            /* [in] */ long panelID,
            /* [in] */ long eventIndex);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_LookupReaderInfo )( 
            IDistributeEvent * This,
            /* [in] */ long panelID,
            /* [in] */ long readerID,
            /* [out] */ READER_INFO *readerInfo);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_LookupWorldTZ )( 
            IDistributeEvent * This,
            /* [in] */ long panelID,
            /* [out] */ TZI *tzInfo);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_DistributeLnlMessageEx )( 
            IDistributeEvent * This,
            /* [in] */ LNLMESSAGE *Message);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_DistributeAccountMessageEx )( 
            IDistributeEvent * This,
            /* [in] */ BSTR AccountNum,
            /* [in] */ BSTR DefaultAccountName,
            /* [in] */ LNLMESSAGE *Message,
            /* [in] */ BSTR TextInfo,
            /* [in] */ long DefaultTemplate);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_DistributeLnlMessageWithTextEx )( 
            IDistributeEvent * This,
            /* [in] */ LNLMESSAGE *Message,
            /* [in] */ BSTR TextInfo);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_DistributeSNMPAgentMessageEx )( 
            IDistributeEvent * This,
            /* [in] */ BSTR Address,
            /* [in] */ BSTR DefaultAgentName,
            /* [in] */ LNLMESSAGE *Message,
            /* [in] */ BSTR TextInfo,
            /* [in] */ long ManagerID);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_DistributeOPCMessageEx )( 
            IDistributeEvent * This,
            /* [in] */ BSTR OPCSource,
            /* [in] */ LNLMESSAGE *Message,
            /* [in] */ BSTR TextInfo);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_UpdateRecordedFlashSize )( 
            IDistributeEvent * This,
            /* [in] */ long panelID,
            /* [in] */ long newFlashSize);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_UpdateRecordedDipSwitchSettings )( 
            IDistributeEvent * This,
            /* [in] */ long panelID,
            /* [in] */ long newDipSwitchSettings);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_UpdateRecordedFirmwareRev )( 
            IDistributeEvent * This,
            /* [in] */ long panelID,
            /* [in] */ BSTR newFirmwareRev);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_UpdateNextConnectionDowngradable )( 
            IDistributeEvent * This,
            /* [in] */ long panelID,
            /* [in] */ BOOL vb_NextConnectionDowngradable);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_UpdateMasterKeyUpdatePending )( 
            IDistributeEvent * This,
            /* [in] */ long panelID,
            /* [in] */ BOOL vb_MasterKeyUpdatePending);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_UpdateClearPreviousMasterKey )( 
            IDistributeEvent * This,
            /* [in] */ long panelID);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_LookupMasterKey )( 
            IDistributeEvent * This,
            /* [in] */ long panelID,
            /* [in] */ long segmentID,
            /* [in] */ long whichkey,
            /* [in] */ long allocatedBytes,
            /* [out] */ long *MasterKeySize,
            /* [out] */ BYTE *MasterKey);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_IsSelectiveDownloadPanel )( 
            IDistributeEvent * This,
            /* [in] */ long panelID,
            /* [out] */ BOOL *selectiveDownloadPanel);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_UpdateBadgeRemainingUses )( 
            IDistributeEvent * This,
            /* [in] */ long panelID,
            /* [in] */ __int64 badgeID,
            /* [in] */ int usesLeft,
            /* [in] */ BOOL updateFailedRPC);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_LookupFIPSModeMasterKey )( 
            IDistributeEvent * This,
            /* [in] */ long whichkey,
            /* [in] */ long allocatedBytes,
            /* [out] */ long *MasterKeySize,
            /* [out] */ BYTE *MasterKey);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_LookupFIPSModeParameters )( 
            IDistributeEvent * This,
            /* [in] */ long panelID,
            /* [out] */ FIPS_MODE_CONTROLLER_ENCRTYPTION_PARAMS *fipsPARAMS);
        
        END_INTERFACE
    } IDistributeEventVtbl;

    interface IDistributeEvent
    {
        CONST_VTBL struct IDistributeEventVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IDistributeEvent_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IDistributeEvent_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IDistributeEvent_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IDistributeEvent_Lnl_DistributeLnlMessage(This,Message)	\
    ( (This)->lpVtbl -> Lnl_DistributeLnlMessage(This,Message) ) 

#define IDistributeEvent_Lnl_StartDatabaseThread(This,PanelID,DownloadArray)	\
    ( (This)->lpVtbl -> Lnl_StartDatabaseThread(This,PanelID,DownloadArray) ) 

#define IDistributeEvent_Lnl_DistributeDisplayTextMessage(This,Message)	\
    ( (This)->lpVtbl -> Lnl_DistributeDisplayTextMessage(This,Message) ) 

#define IDistributeEvent_Lnl_WriteDownloadError(This,vi_PanelID,pro_Ptr,vi_DataSize)	\
    ( (This)->lpVtbl -> Lnl_WriteDownloadError(This,vi_PanelID,pro_Ptr,vi_DataSize) ) 

#define IDistributeEvent_Lnl_DistributeCurrentVideoPosition(This,ErrorCode,CurrentPosition)	\
    ( (This)->lpVtbl -> Lnl_DistributeCurrentVideoPosition(This,ErrorCode,CurrentPosition) ) 

#define IDistributeEvent_Lnl_PanelCommunicationType(This,PanelID,CommType)	\
    ( (This)->lpVtbl -> Lnl_PanelCommunicationType(This,PanelID,CommType) ) 

#define IDistributeEvent_Lnl_IsTimezoneActive(This,timeZone,active)	\
    ( (This)->lpVtbl -> Lnl_IsTimezoneActive(This,timeZone,active) ) 

#define IDistributeEvent_Lnl_StartReaderDownload(This,PanelID,numReaders,ReaderArray,DownloadArray)	\
    ( (This)->lpVtbl -> Lnl_StartReaderDownload(This,PanelID,numReaders,ReaderArray,DownloadArray) ) 

#define IDistributeEvent_Lnl_AbortDatabaseThread(This,PanelID)	\
    ( (This)->lpVtbl -> Lnl_AbortDatabaseThread(This,PanelID) ) 

#define IDistributeEvent_Lnl_WorkstationLookup(This,WorkstationName,WorkstationID)	\
    ( (This)->lpVtbl -> Lnl_WorkstationLookup(This,WorkstationName,WorkstationID) ) 

#define IDistributeEvent_Lnl_DistributeAccountMessage(This,AccountNum,DefaultAccountName,Message,TextInfo,DefaultTemplate)	\
    ( (This)->lpVtbl -> Lnl_DistributeAccountMessage(This,AccountNum,DefaultAccountName,Message,TextInfo,DefaultTemplate) ) 

#define IDistributeEvent_Lnl_DistributeLnlMessageWithText(This,Message,TextInfo)	\
    ( (This)->lpVtbl -> Lnl_DistributeLnlMessageWithText(This,Message,TextInfo) ) 

#define IDistributeEvent_Lnl_HostBasedDecisionRequest(This,panelID,readerID,badgeID,time)	\
    ( (This)->lpVtbl -> Lnl_HostBasedDecisionRequest(This,panelID,readerID,badgeID,time) ) 

#define IDistributeEvent_Lnl_UpdatePanelEventIndex(This,panelID,eventIndex)	\
    ( (This)->lpVtbl -> Lnl_UpdatePanelEventIndex(This,panelID,eventIndex) ) 

#define IDistributeEvent_Lnl_LookupReaderInfo(This,panelID,readerID,readerInfo)	\
    ( (This)->lpVtbl -> Lnl_LookupReaderInfo(This,panelID,readerID,readerInfo) ) 

#define IDistributeEvent_Lnl_LookupWorldTZ(This,panelID,tzInfo)	\
    ( (This)->lpVtbl -> Lnl_LookupWorldTZ(This,panelID,tzInfo) ) 

#define IDistributeEvent_Lnl_DistributeLnlMessageEx(This,Message)	\
    ( (This)->lpVtbl -> Lnl_DistributeLnlMessageEx(This,Message) ) 

#define IDistributeEvent_Lnl_DistributeAccountMessageEx(This,AccountNum,DefaultAccountName,Message,TextInfo,DefaultTemplate)	\
    ( (This)->lpVtbl -> Lnl_DistributeAccountMessageEx(This,AccountNum,DefaultAccountName,Message,TextInfo,DefaultTemplate) ) 

#define IDistributeEvent_Lnl_DistributeLnlMessageWithTextEx(This,Message,TextInfo)	\
    ( (This)->lpVtbl -> Lnl_DistributeLnlMessageWithTextEx(This,Message,TextInfo) ) 

#define IDistributeEvent_Lnl_DistributeSNMPAgentMessageEx(This,Address,DefaultAgentName,Message,TextInfo,ManagerID)	\
    ( (This)->lpVtbl -> Lnl_DistributeSNMPAgentMessageEx(This,Address,DefaultAgentName,Message,TextInfo,ManagerID) ) 

#define IDistributeEvent_Lnl_DistributeOPCMessageEx(This,OPCSource,Message,TextInfo)	\
    ( (This)->lpVtbl -> Lnl_DistributeOPCMessageEx(This,OPCSource,Message,TextInfo) ) 

#define IDistributeEvent_Lnl_UpdateRecordedFlashSize(This,panelID,newFlashSize)	\
    ( (This)->lpVtbl -> Lnl_UpdateRecordedFlashSize(This,panelID,newFlashSize) ) 

#define IDistributeEvent_Lnl_UpdateRecordedDipSwitchSettings(This,panelID,newDipSwitchSettings)	\
    ( (This)->lpVtbl -> Lnl_UpdateRecordedDipSwitchSettings(This,panelID,newDipSwitchSettings) ) 

#define IDistributeEvent_Lnl_UpdateRecordedFirmwareRev(This,panelID,newFirmwareRev)	\
    ( (This)->lpVtbl -> Lnl_UpdateRecordedFirmwareRev(This,panelID,newFirmwareRev) ) 

#define IDistributeEvent_Lnl_UpdateNextConnectionDowngradable(This,panelID,vb_NextConnectionDowngradable)	\
    ( (This)->lpVtbl -> Lnl_UpdateNextConnectionDowngradable(This,panelID,vb_NextConnectionDowngradable) ) 

#define IDistributeEvent_Lnl_UpdateMasterKeyUpdatePending(This,panelID,vb_MasterKeyUpdatePending)	\
    ( (This)->lpVtbl -> Lnl_UpdateMasterKeyUpdatePending(This,panelID,vb_MasterKeyUpdatePending) ) 

#define IDistributeEvent_Lnl_UpdateClearPreviousMasterKey(This,panelID)	\
    ( (This)->lpVtbl -> Lnl_UpdateClearPreviousMasterKey(This,panelID) ) 

#define IDistributeEvent_Lnl_LookupMasterKey(This,panelID,segmentID,whichkey,allocatedBytes,MasterKeySize,MasterKey)	\
    ( (This)->lpVtbl -> Lnl_LookupMasterKey(This,panelID,segmentID,whichkey,allocatedBytes,MasterKeySize,MasterKey) ) 

#define IDistributeEvent_Lnl_IsSelectiveDownloadPanel(This,panelID,selectiveDownloadPanel)	\
    ( (This)->lpVtbl -> Lnl_IsSelectiveDownloadPanel(This,panelID,selectiveDownloadPanel) ) 

#define IDistributeEvent_Lnl_UpdateBadgeRemainingUses(This,panelID,badgeID,usesLeft,updateFailedRPC)	\
    ( (This)->lpVtbl -> Lnl_UpdateBadgeRemainingUses(This,panelID,badgeID,usesLeft,updateFailedRPC) ) 

#define IDistributeEvent_Lnl_LookupFIPSModeMasterKey(This,whichkey,allocatedBytes,MasterKeySize,MasterKey)	\
    ( (This)->lpVtbl -> Lnl_LookupFIPSModeMasterKey(This,whichkey,allocatedBytes,MasterKeySize,MasterKey) ) 

#define IDistributeEvent_Lnl_LookupFIPSModeParameters(This,panelID,fipsPARAMS)	\
    ( (This)->lpVtbl -> Lnl_LookupFIPSModeParameters(This,panelID,fipsPARAMS) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IDistributeEvent_INTERFACE_DEFINED__ */


#ifndef __ITranslate_INTERFACE_DEFINED__
#define __ITranslate_INTERFACE_DEFINED__

/* interface ITranslate */
/* [unique][helpstring][uuid][object] */ 


EXTERN_C const IID IID_ITranslate;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("338C2B61-61FB-11D2-8B80-00A0C91FDE82")
    ITranslate : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE Lnl_CreateCommunicationObject( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_ConnectCommunicationManager( 
            /* [in] */ REFIID iid,
            /* [in] */ IUnknown *pIUnknown) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_RequireCTManager( 
            /* [out] */ int *ManagerRequired) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_InitCommunication( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_InitializePanel( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_SetPanelOffline( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_SetPanelID( 
            /* [in] */ int panel_id) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_SetEventCounter( 
            /* [in] */ long vl_Counter) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_GetPanelID( 
            /* [out] */ int *panel_id) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_IncrementMonitorReq( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_DecrementMonitorReq( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_PollPanelForEvents( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_UnsolicitedEventRequest( 
            /* [in] */ long ll_EventNumber,
            /* [in] */ BOOL vb_WaitForEvent) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_GetPanelEventSerialNumber( 
            /* [out] */ long *num) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_CloseCommunication( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_GetSystemStatus( 
            /* [out] */ SYSTEM_STATUS *p_Status) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_ReadClock( 
            /* [out] */ struct tm *prl_Time) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_SendPanelNextDirectCmd( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_SetClock( 
            /* [in] */ long vl_Time,
            /* [in] */ BOOL vb_WaitForRsp) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_SetDBEnabledFlag( 
            /* [in] */ BOOL vb_IsDBLoggingEvents) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_DownloadFirmware( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_IsDownloadFinished( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_SetDownloadInProgressState( 
            /* [in] */ BOOL State) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_ConnectToPanel( 
            /* [in] */ long vb_Connect,
            /* [in] */ BOOL vb_GenerateEvent) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_IssueColdStartCmd( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_ResetPowerUpFlag( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_SetCommunicationType( 
            /* [in] */ int vi_Value) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_GetCommunicationType( 
            /* [out] */ int *Value) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_SetCommunicationType2( 
            /* [in] */ int vi_Value) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_GetCommunicationType2( 
            /* [out] */ int *Value) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_SetDST( 
            /* [in] */ BOOL vi_Value) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_GetDST( 
            /* [out] */ BOOL *Value) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_SetPanelState( 
            /* [in] */ int vi_Value) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_GetPanelState( 
            /* [out] */ int *Value) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_SetDirectCmd( 
            /* [in] */ BYTE *pro_Data) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_SetPanelType( 
            /* [in] */ int Value) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_GetPanelType( 
            /* [out] */ int *Value) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_SetPanelTZInfo( 
            /* [in] */ TZI *Value) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_GetPanelTZInfo( 
            /* [out] */ TZI *Value) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_SetPanelName( 
            /* [in] */ BSTR panelName) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_IsPanelOnline( 
            /* [out] */ BOOL *Value) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_GetVersionInfo( 
            /* [out] */ long *major,
            /* [out] */ long *minor,
            /* [out] */ long *build) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_SetPanelPassword( 
            /* [in] */ BSTR password) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ITranslateVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ITranslate * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ITranslate * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ITranslate * This);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_CreateCommunicationObject )( 
            ITranslate * This);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_ConnectCommunicationManager )( 
            ITranslate * This,
            /* [in] */ REFIID iid,
            /* [in] */ IUnknown *pIUnknown);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_RequireCTManager )( 
            ITranslate * This,
            /* [out] */ int *ManagerRequired);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_InitCommunication )( 
            ITranslate * This);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_InitializePanel )( 
            ITranslate * This);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_SetPanelOffline )( 
            ITranslate * This);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_SetPanelID )( 
            ITranslate * This,
            /* [in] */ int panel_id);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_SetEventCounter )( 
            ITranslate * This,
            /* [in] */ long vl_Counter);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_GetPanelID )( 
            ITranslate * This,
            /* [out] */ int *panel_id);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_IncrementMonitorReq )( 
            ITranslate * This);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_DecrementMonitorReq )( 
            ITranslate * This);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_PollPanelForEvents )( 
            ITranslate * This);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_UnsolicitedEventRequest )( 
            ITranslate * This,
            /* [in] */ long ll_EventNumber,
            /* [in] */ BOOL vb_WaitForEvent);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_GetPanelEventSerialNumber )( 
            ITranslate * This,
            /* [out] */ long *num);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_CloseCommunication )( 
            ITranslate * This);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_GetSystemStatus )( 
            ITranslate * This,
            /* [out] */ SYSTEM_STATUS *p_Status);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_ReadClock )( 
            ITranslate * This,
            /* [out] */ struct tm *prl_Time);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_SendPanelNextDirectCmd )( 
            ITranslate * This);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_SetClock )( 
            ITranslate * This,
            /* [in] */ long vl_Time,
            /* [in] */ BOOL vb_WaitForRsp);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_SetDBEnabledFlag )( 
            ITranslate * This,
            /* [in] */ BOOL vb_IsDBLoggingEvents);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_DownloadFirmware )( 
            ITranslate * This);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_IsDownloadFinished )( 
            ITranslate * This);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_SetDownloadInProgressState )( 
            ITranslate * This,
            /* [in] */ BOOL State);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_ConnectToPanel )( 
            ITranslate * This,
            /* [in] */ long vb_Connect,
            /* [in] */ BOOL vb_GenerateEvent);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_IssueColdStartCmd )( 
            ITranslate * This);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_ResetPowerUpFlag )( 
            ITranslate * This);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_SetCommunicationType )( 
            ITranslate * This,
            /* [in] */ int vi_Value);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_GetCommunicationType )( 
            ITranslate * This,
            /* [out] */ int *Value);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_SetCommunicationType2 )( 
            ITranslate * This,
            /* [in] */ int vi_Value);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_GetCommunicationType2 )( 
            ITranslate * This,
            /* [out] */ int *Value);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_SetDST )( 
            ITranslate * This,
            /* [in] */ BOOL vi_Value);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_GetDST )( 
            ITranslate * This,
            /* [out] */ BOOL *Value);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_SetPanelState )( 
            ITranslate * This,
            /* [in] */ int vi_Value);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_GetPanelState )( 
            ITranslate * This,
            /* [out] */ int *Value);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_SetDirectCmd )( 
            ITranslate * This,
            /* [in] */ BYTE *pro_Data);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_SetPanelType )( 
            ITranslate * This,
            /* [in] */ int Value);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_GetPanelType )( 
            ITranslate * This,
            /* [out] */ int *Value);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_SetPanelTZInfo )( 
            ITranslate * This,
            /* [in] */ TZI *Value);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_GetPanelTZInfo )( 
            ITranslate * This,
            /* [out] */ TZI *Value);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_SetPanelName )( 
            ITranslate * This,
            /* [in] */ BSTR panelName);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_IsPanelOnline )( 
            ITranslate * This,
            /* [out] */ BOOL *Value);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_GetVersionInfo )( 
            ITranslate * This,
            /* [out] */ long *major,
            /* [out] */ long *minor,
            /* [out] */ long *build);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_SetPanelPassword )( 
            ITranslate * This,
            /* [in] */ BSTR password);
        
        END_INTERFACE
    } ITranslateVtbl;

    interface ITranslate
    {
        CONST_VTBL struct ITranslateVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ITranslate_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ITranslate_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ITranslate_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define ITranslate_Lnl_CreateCommunicationObject(This)	\
    ( (This)->lpVtbl -> Lnl_CreateCommunicationObject(This) ) 

#define ITranslate_Lnl_ConnectCommunicationManager(This,iid,pIUnknown)	\
    ( (This)->lpVtbl -> Lnl_ConnectCommunicationManager(This,iid,pIUnknown) ) 

#define ITranslate_Lnl_RequireCTManager(This,ManagerRequired)	\
    ( (This)->lpVtbl -> Lnl_RequireCTManager(This,ManagerRequired) ) 

#define ITranslate_Lnl_InitCommunication(This)	\
    ( (This)->lpVtbl -> Lnl_InitCommunication(This) ) 

#define ITranslate_Lnl_InitializePanel(This)	\
    ( (This)->lpVtbl -> Lnl_InitializePanel(This) ) 

#define ITranslate_Lnl_SetPanelOffline(This)	\
    ( (This)->lpVtbl -> Lnl_SetPanelOffline(This) ) 

#define ITranslate_Lnl_SetPanelID(This,panel_id)	\
    ( (This)->lpVtbl -> Lnl_SetPanelID(This,panel_id) ) 

#define ITranslate_Lnl_SetEventCounter(This,vl_Counter)	\
    ( (This)->lpVtbl -> Lnl_SetEventCounter(This,vl_Counter) ) 

#define ITranslate_Lnl_GetPanelID(This,panel_id)	\
    ( (This)->lpVtbl -> Lnl_GetPanelID(This,panel_id) ) 

#define ITranslate_Lnl_IncrementMonitorReq(This)	\
    ( (This)->lpVtbl -> Lnl_IncrementMonitorReq(This) ) 

#define ITranslate_Lnl_DecrementMonitorReq(This)	\
    ( (This)->lpVtbl -> Lnl_DecrementMonitorReq(This) ) 

#define ITranslate_Lnl_PollPanelForEvents(This)	\
    ( (This)->lpVtbl -> Lnl_PollPanelForEvents(This) ) 

#define ITranslate_Lnl_UnsolicitedEventRequest(This,ll_EventNumber,vb_WaitForEvent)	\
    ( (This)->lpVtbl -> Lnl_UnsolicitedEventRequest(This,ll_EventNumber,vb_WaitForEvent) ) 

#define ITranslate_Lnl_GetPanelEventSerialNumber(This,num)	\
    ( (This)->lpVtbl -> Lnl_GetPanelEventSerialNumber(This,num) ) 

#define ITranslate_Lnl_CloseCommunication(This)	\
    ( (This)->lpVtbl -> Lnl_CloseCommunication(This) ) 

#define ITranslate_Lnl_GetSystemStatus(This,p_Status)	\
    ( (This)->lpVtbl -> Lnl_GetSystemStatus(This,p_Status) ) 

#define ITranslate_Lnl_ReadClock(This,prl_Time)	\
    ( (This)->lpVtbl -> Lnl_ReadClock(This,prl_Time) ) 

#define ITranslate_Lnl_SendPanelNextDirectCmd(This)	\
    ( (This)->lpVtbl -> Lnl_SendPanelNextDirectCmd(This) ) 

#define ITranslate_Lnl_SetClock(This,vl_Time,vb_WaitForRsp)	\
    ( (This)->lpVtbl -> Lnl_SetClock(This,vl_Time,vb_WaitForRsp) ) 

#define ITranslate_Lnl_SetDBEnabledFlag(This,vb_IsDBLoggingEvents)	\
    ( (This)->lpVtbl -> Lnl_SetDBEnabledFlag(This,vb_IsDBLoggingEvents) ) 

#define ITranslate_Lnl_DownloadFirmware(This)	\
    ( (This)->lpVtbl -> Lnl_DownloadFirmware(This) ) 

#define ITranslate_Lnl_IsDownloadFinished(This)	\
    ( (This)->lpVtbl -> Lnl_IsDownloadFinished(This) ) 

#define ITranslate_Lnl_SetDownloadInProgressState(This,State)	\
    ( (This)->lpVtbl -> Lnl_SetDownloadInProgressState(This,State) ) 

#define ITranslate_Lnl_ConnectToPanel(This,vb_Connect,vb_GenerateEvent)	\
    ( (This)->lpVtbl -> Lnl_ConnectToPanel(This,vb_Connect,vb_GenerateEvent) ) 

#define ITranslate_Lnl_IssueColdStartCmd(This)	\
    ( (This)->lpVtbl -> Lnl_IssueColdStartCmd(This) ) 

#define ITranslate_Lnl_ResetPowerUpFlag(This)	\
    ( (This)->lpVtbl -> Lnl_ResetPowerUpFlag(This) ) 

#define ITranslate_Lnl_SetCommunicationType(This,vi_Value)	\
    ( (This)->lpVtbl -> Lnl_SetCommunicationType(This,vi_Value) ) 

#define ITranslate_Lnl_GetCommunicationType(This,Value)	\
    ( (This)->lpVtbl -> Lnl_GetCommunicationType(This,Value) ) 

#define ITranslate_Lnl_SetCommunicationType2(This,vi_Value)	\
    ( (This)->lpVtbl -> Lnl_SetCommunicationType2(This,vi_Value) ) 

#define ITranslate_Lnl_GetCommunicationType2(This,Value)	\
    ( (This)->lpVtbl -> Lnl_GetCommunicationType2(This,Value) ) 

#define ITranslate_Lnl_SetDST(This,vi_Value)	\
    ( (This)->lpVtbl -> Lnl_SetDST(This,vi_Value) ) 

#define ITranslate_Lnl_GetDST(This,Value)	\
    ( (This)->lpVtbl -> Lnl_GetDST(This,Value) ) 

#define ITranslate_Lnl_SetPanelState(This,vi_Value)	\
    ( (This)->lpVtbl -> Lnl_SetPanelState(This,vi_Value) ) 

#define ITranslate_Lnl_GetPanelState(This,Value)	\
    ( (This)->lpVtbl -> Lnl_GetPanelState(This,Value) ) 

#define ITranslate_Lnl_SetDirectCmd(This,pro_Data)	\
    ( (This)->lpVtbl -> Lnl_SetDirectCmd(This,pro_Data) ) 

#define ITranslate_Lnl_SetPanelType(This,Value)	\
    ( (This)->lpVtbl -> Lnl_SetPanelType(This,Value) ) 

#define ITranslate_Lnl_GetPanelType(This,Value)	\
    ( (This)->lpVtbl -> Lnl_GetPanelType(This,Value) ) 

#define ITranslate_Lnl_SetPanelTZInfo(This,Value)	\
    ( (This)->lpVtbl -> Lnl_SetPanelTZInfo(This,Value) ) 

#define ITranslate_Lnl_GetPanelTZInfo(This,Value)	\
    ( (This)->lpVtbl -> Lnl_GetPanelTZInfo(This,Value) ) 

#define ITranslate_Lnl_SetPanelName(This,panelName)	\
    ( (This)->lpVtbl -> Lnl_SetPanelName(This,panelName) ) 

#define ITranslate_Lnl_IsPanelOnline(This,Value)	\
    ( (This)->lpVtbl -> Lnl_IsPanelOnline(This,Value) ) 

#define ITranslate_Lnl_GetVersionInfo(This,major,minor,build)	\
    ( (This)->lpVtbl -> Lnl_GetVersionInfo(This,major,minor,build) ) 

#define ITranslate_Lnl_SetPanelPassword(This,password)	\
    ( (This)->lpVtbl -> Lnl_SetPanelPassword(This,password) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ITranslate_INTERFACE_DEFINED__ */


#ifndef __IComConfig_INTERFACE_DEFINED__
#define __IComConfig_INTERFACE_DEFINED__

/* interface IComConfig */
/* [unique][helpstring][uuid][object] */ 


EXTERN_C const IID IID_IComConfig;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("7D2DC8A1-5035-11d3-859D-00C04F5807EA")
    IComConfig : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE Lnl_SetComPort( 
            /* [in] */ int ComPort) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_GetComPort( 
            /* [out] */ int *ComPort) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_SetComPort2( 
            /* [in] */ int vi_Value) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_GetComPort2( 
            /* [out] */ int *Value) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_SetBaudRate( 
            /* [in] */ int vi_Value) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_GetBaudRate( 
            /* [out] */ int *Value) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_SetPanelAddress( 
            /* [in] */ int vi_Value) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_GetPanelAddress( 
            /* [out] */ int *Value) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_SetPrimaryIP( 
            /* [in] */ int vi_Value) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_GetPrimaryIP( 
            /* [out] */ int *Value) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_SetSecondaryIP( 
            /* [in] */ int vi_Value) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_GetSecondaryIP( 
            /* [out] */ int *Value) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_SetByteSize( 
            /* [in] */ int byteSize) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_GetByteSize( 
            /* [out] */ int *pByteSize) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_SetParity( 
            /* [in] */ int parity) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_GetParity( 
            /* [out] */ int *pParity) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_SetStopBits( 
            /* [in] */ int stopBits) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_GetStopBits( 
            /* [out] */ int *pStopBits) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IComConfigVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IComConfig * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IComConfig * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IComConfig * This);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_SetComPort )( 
            IComConfig * This,
            /* [in] */ int ComPort);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_GetComPort )( 
            IComConfig * This,
            /* [out] */ int *ComPort);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_SetComPort2 )( 
            IComConfig * This,
            /* [in] */ int vi_Value);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_GetComPort2 )( 
            IComConfig * This,
            /* [out] */ int *Value);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_SetBaudRate )( 
            IComConfig * This,
            /* [in] */ int vi_Value);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_GetBaudRate )( 
            IComConfig * This,
            /* [out] */ int *Value);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_SetPanelAddress )( 
            IComConfig * This,
            /* [in] */ int vi_Value);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_GetPanelAddress )( 
            IComConfig * This,
            /* [out] */ int *Value);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_SetPrimaryIP )( 
            IComConfig * This,
            /* [in] */ int vi_Value);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_GetPrimaryIP )( 
            IComConfig * This,
            /* [out] */ int *Value);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_SetSecondaryIP )( 
            IComConfig * This,
            /* [in] */ int vi_Value);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_GetSecondaryIP )( 
            IComConfig * This,
            /* [out] */ int *Value);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_SetByteSize )( 
            IComConfig * This,
            /* [in] */ int byteSize);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_GetByteSize )( 
            IComConfig * This,
            /* [out] */ int *pByteSize);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_SetParity )( 
            IComConfig * This,
            /* [in] */ int parity);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_GetParity )( 
            IComConfig * This,
            /* [out] */ int *pParity);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_SetStopBits )( 
            IComConfig * This,
            /* [in] */ int stopBits);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_GetStopBits )( 
            IComConfig * This,
            /* [out] */ int *pStopBits);
        
        END_INTERFACE
    } IComConfigVtbl;

    interface IComConfig
    {
        CONST_VTBL struct IComConfigVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IComConfig_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IComConfig_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IComConfig_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IComConfig_Lnl_SetComPort(This,ComPort)	\
    ( (This)->lpVtbl -> Lnl_SetComPort(This,ComPort) ) 

#define IComConfig_Lnl_GetComPort(This,ComPort)	\
    ( (This)->lpVtbl -> Lnl_GetComPort(This,ComPort) ) 

#define IComConfig_Lnl_SetComPort2(This,vi_Value)	\
    ( (This)->lpVtbl -> Lnl_SetComPort2(This,vi_Value) ) 

#define IComConfig_Lnl_GetComPort2(This,Value)	\
    ( (This)->lpVtbl -> Lnl_GetComPort2(This,Value) ) 

#define IComConfig_Lnl_SetBaudRate(This,vi_Value)	\
    ( (This)->lpVtbl -> Lnl_SetBaudRate(This,vi_Value) ) 

#define IComConfig_Lnl_GetBaudRate(This,Value)	\
    ( (This)->lpVtbl -> Lnl_GetBaudRate(This,Value) ) 

#define IComConfig_Lnl_SetPanelAddress(This,vi_Value)	\
    ( (This)->lpVtbl -> Lnl_SetPanelAddress(This,vi_Value) ) 

#define IComConfig_Lnl_GetPanelAddress(This,Value)	\
    ( (This)->lpVtbl -> Lnl_GetPanelAddress(This,Value) ) 

#define IComConfig_Lnl_SetPrimaryIP(This,vi_Value)	\
    ( (This)->lpVtbl -> Lnl_SetPrimaryIP(This,vi_Value) ) 

#define IComConfig_Lnl_GetPrimaryIP(This,Value)	\
    ( (This)->lpVtbl -> Lnl_GetPrimaryIP(This,Value) ) 

#define IComConfig_Lnl_SetSecondaryIP(This,vi_Value)	\
    ( (This)->lpVtbl -> Lnl_SetSecondaryIP(This,vi_Value) ) 

#define IComConfig_Lnl_GetSecondaryIP(This,Value)	\
    ( (This)->lpVtbl -> Lnl_GetSecondaryIP(This,Value) ) 

#define IComConfig_Lnl_SetByteSize(This,byteSize)	\
    ( (This)->lpVtbl -> Lnl_SetByteSize(This,byteSize) ) 

#define IComConfig_Lnl_GetByteSize(This,pByteSize)	\
    ( (This)->lpVtbl -> Lnl_GetByteSize(This,pByteSize) ) 

#define IComConfig_Lnl_SetParity(This,parity)	\
    ( (This)->lpVtbl -> Lnl_SetParity(This,parity) ) 

#define IComConfig_Lnl_GetParity(This,pParity)	\
    ( (This)->lpVtbl -> Lnl_GetParity(This,pParity) ) 

#define IComConfig_Lnl_SetStopBits(This,stopBits)	\
    ( (This)->lpVtbl -> Lnl_SetStopBits(This,stopBits) ) 

#define IComConfig_Lnl_GetStopBits(This,pStopBits)	\
    ( (This)->lpVtbl -> Lnl_GetStopBits(This,pStopBits) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IComConfig_INTERFACE_DEFINED__ */


#ifndef __IIntercom_INTERFACE_DEFINED__
#define __IIntercom_INTERFACE_DEFINED__

/* interface IIntercom */
/* [unique][helpstring][uuid][object] */ 


EXTERN_C const IID IID_IIntercom;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("151BB901-C291-11d2-9ACC-00A0C91FDE82")
    IIntercom : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE Lnl_CancelIntercomCall( 
            /* [in] */ long StationNum) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Lnl_PlaceIntercomCall( 
            /* [in] */ long Station1,
            /* [in] */ long Station2,
            /* [in] */ long Priority) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IIntercomVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IIntercom * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IIntercom * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IIntercom * This);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_CancelIntercomCall )( 
            IIntercom * This,
            /* [in] */ long StationNum);
        
        HRESULT ( STDMETHODCALLTYPE *Lnl_PlaceIntercomCall )( 
            IIntercom * This,
            /* [in] */ long Station1,
            /* [in] */ long Station2,
            /* [in] */ long Priority);
        
        END_INTERFACE
    } IIntercomVtbl;

    interface IIntercom
    {
        CONST_VTBL struct IIntercomVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IIntercom_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IIntercom_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IIntercom_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IIntercom_Lnl_CancelIntercomCall(This,StationNum)	\
    ( (This)->lpVtbl -> Lnl_CancelIntercomCall(This,StationNum) ) 

#define IIntercom_Lnl_PlaceIntercomCall(This,Station1,Station2,Priority)	\
    ( (This)->lpVtbl -> Lnl_PlaceIntercomCall(This,Station1,Station2,Priority) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IIntercom_INTERFACE_DEFINED__ */



#ifndef __GENERICINTERCOMTRANSLATORLib_LIBRARY_DEFINED__
#define __GENERICINTERCOMTRANSLATORLib_LIBRARY_DEFINED__

/* library GENERICINTERCOMTRANSLATORLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_GENERICINTERCOMTRANSLATORLib;

EXTERN_C const CLSID CLSID_IntercomTrans;

#ifdef __cplusplus

class DECLSPEC_UUID("0A4DBCA1-1160-11D4-86EB-00C04F5807EA")
IntercomTrans;
#endif
#endif /* __GENERICINTERCOMTRANSLATORLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

unsigned long             __RPC_USER  BSTR_UserSize(     unsigned long *, unsigned long            , BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserMarshal(  unsigned long *, unsigned char *, BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserUnmarshal(unsigned long *, unsigned char *, BSTR * ); 
void                      __RPC_USER  BSTR_UserFree(     unsigned long *, BSTR * ); 

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


