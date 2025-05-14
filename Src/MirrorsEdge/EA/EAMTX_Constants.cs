// Decompiled with JetBrains decompiler
// Type: ea.EAMTX_Constants
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

#nullable disable
namespace ea
{
  public static class EAMTX_Constants
  {
    public const int MTX_ERR_CORE_MASTER_ITEM_NOT_FOUND = -10001;
    public const int MTX_ERR_CORE_USER_MUST_UPGRADE_APP = -10005;
    public const int MTX_ERR_CORE_USER_NOT_FOUND = -10002;
    public const int MTX_ERR_CORE_ITEM_NOT_FOUND = -10006;
    public const int MTX_ERR_CORE_USER_NOT_GRANTED = -10003;
    public const int MTX_ERR_CORE_DEVICE_NOT_GRANTED = -10004;
    public const int MTX_ERR_CORE_MASTER_ITEM_NOT_FOUND_FOR_LANG = -10007;
    public const int MTX_ERR_DRMC_USER_NOT_FOUND = -30001;
    public const int MTX_ERR_DRM_ITEM_NOT_FOUND = -30002;
    public const int MTX_ERR_DRM_ITEM_NOT_PURCHASED = -30004;
    public const int MTX_ERR_DEVICE_DOWNLOAD_LIMIT_EXCEEDED = -30005;
    public const int MTX_ERR_ACCOUNT_BLOCKED = -30006;
    public const int MTX_ERR_NORECORD_ORIGINAL_PURCHASE = -30007;
    public const int MTX_ERR_RESTORE_LIMIT_EXCEEDED = -30008;
    public const int MTX_ERR_DRMI_USER_NOT_FOUND = -31001;
    public const int MTX_ERR_NO_ITEM_FOR_SELLID = -31002;
    public const int MTX_ERR_ERROR_VALIDATING_RECEIPT = -31003;
    public const int MTX_ERR_RECEIPT_FAILED_VERIFICATION = -31004;
    public const int MTX_ERR_TRACK_USER_NOT_FOUND = -40001;
    public const int MTX_ERR_TRACK_NO_ITEM_FOR_SELLID = -40002;
    public const int MTX_ERR_TRACK_INVALID_EVENTID = -40003;
    public const int MTX_ERR_TRACK_INVALID_TIMESTAMP = -40005;
    public const int MTX_ERR_M2U_CORE_USER_NOT_FOUND = -50001;
    public const int MTX_ERR_M2U_CORE_ITEM_NOT_FOUND = -50002;
    public const int MTX_ERR_M2U_CORE_NO_BANNERS_AVAILABLE = -50003;
    public const int MTX_ERR_M2U_CORE_NO_TICKERS_AVAILABLE = -50004;
    public const int MTX_ERR_M2U_CORE_NO_MESSAGES_AVAILABLE = -50005;
    public const int MTX_ERR_M2U_USER_NOT_FOUND = -51001;
    public const int MTX_ERR_M2U_INVALID_TOKEN = -51002;
    public const int MTX_ERR_M2U_USER_NOTOPTED_MESSAGES = -51003;
    public const int MTX_ERR_PURCHASE_CANCELLED = -3333;
    public const int MTX_ERR_PURCHASE_UNKNOWN = -4444;
    public const int MTX_ERR_RESTORE_CANCELLED = -5555;
    public const int MTX_ERR_RESTORE_UNKNOWN = -6666;
    public const int MTX_ERR_PURCHASE_LOWVERSION = -7777;
    public const int MTX_ERR_INSUFF_SPACE = -8888;
    public const int BANNER_MAINMENU = 1;
    public const int BANNER_THUMBNAILS = 2;
    public const int BANNER_MYSTORE = 3;
    public const int TICKER_MAINMENU = 1;
    public const int TICKER_STORE = 2;
    public const int TYPE_IN_APP_MESSAGE_MAIN_MENU = 4;
    public const int EVT_APPSTART_NORMALLY = 10000;
    public const int EVT_APPSTART_FROMPUSH = 10001;
    public const int EVT_APPSTART_AFTERINSTALL = 10002;
    public const int EVT_APPSTART_AFTERUPGRADE = 10003;
    public const int EVT_APPEND_NORMALLY = 20000;
    public const int EVT_APPEND_ABNORMALLY = 20001;
    public const int EVT_OPT_FULL_PURCHASE = 30000;
    public const int EVT_MOREGAMES_ENTER = 30001;
    public const int EVT_MOREGAMES_CLICKTHROUGH = 30002;
    public const int EVT_MOREGAMES_GAMESELECT = 30004;
    public const int EVT_MOREGAMES_CATEGORYSELECT = 30005;
    public const int EVT_ENTER_FULL_GAME_OVERVIEW_SCREEN = 30008;
    public const int EVT_LITE_ED_GAME_DEMO_START = 30009;
    public const int EVT_LITE_ED_GAME_DEMO_END = 30010;
    public const int EVT_MAINMENU_BANNER_CLICK = 30011;
    public const int EVT_MAINMENU_TICKER_CLICK = 30012;
    public const int EVT_INSTORE_BANNER_CLICK = 30013;
    public const int EVT_INSTORE_TICKER_CLICK = 30014;
    public const int EVT_MOREGAMES_CLICKTHROUGH_FEATURED = 30017;
    public const int EVT_MOREGAMES_CLICKTHROUGH_SIDEBANNER = 30018;
    public const int EVT_IPAD_UPSELL_MESSAGE_DISPLAYED = 30019;
    public const int EVT_IPAD_UPSELL_MESSAGE_NOTHANKS_CLICKED = 30020;
    public const int EVT_IPAD_UPSELL_MESSAGE_OK_CLICKED = 30021;
    public const int EVT_IPAD_UPSELL_MESSAGE_LATER_CLICKED = 30022;
    public const int EVT_MTXVIEW_ENTER = 40000;
    public const int EVT_MTXVIEW_GAMECATEGORY = 40001;
    public const int EVT_MTXVIEW_ITEMSELECT = 40002;
    public const int EVT_MTXVIEW_ITEMPURCHASE = 40003;
    public const int EVT_MTXVIEW_ENTER_FROMCTX = 40004;
    public const int EVT_MTXVIEW_FREEITEM_DOWNLOADED = 40005;
    public const int EVT_MTXVIEW_ITEM_PURCHASED = 40006;
    public const int EVT_INGAME_EMAIL_OPEN = 50001;
    public const int EVT_INGAME_EMAIL_SEND = 50002;
    public const int EVT_INGAME_EMAIL_RECEIEVE = 50003;
    public const int EVT_MEDIAPICKER_OPEN = 50004;
    public const int EVT_ACCESS_BT_MENU = 50005;
    public const int EVT_BEGIN_BT_SESSION = 50006;
    public const int EVT_COMPLETE_BT_SESSION = 50007;
    public const int EVT_ACCESS_WIFI_MENU = 50008;
    public const int EVT_BEGIN_WIFI_SESSION = 50009;
    public const int EVT_COMPLETE_WIFI_SESSION = 50010;
    public const int EVT_USR_ISSUE_PUSH_NOTIFICATION_CHALLENGE = 50011;
    public const int EVT_LANGUAGE_SELECTED = 50012;
    public const int EVT_ACCESS_INGAME_SCREEN = 60001;
    public const int EVT_LEAVE_INGAME_SCREEN = 60002;
    public const int EVT_EVENTS_PURGED = 70000;
    public const int EVT_KEYTYPE_NONE = 0;
    public const int EVT_KEYTYPE_GAME_SELLID = 1;
    public const int EVT_KEYTYPE_MTX_SELLID = 2;
    public const int EVT_KEYTYPE_MTX_CATEGORY = 3;
    public const int EVT_KEYTYPE_SCREEN_NAME = 4;
    public const int EVT_KEYTYPE_EVENT_COUNT = 5;
    public const int EVT_KEYTYPE_DURATION = 7;
    public const int EVT_KEYTYPE_FREQUENCY = 8;
    public const int EVT_KEYTYPE_FEATURED = 10;
    public const int EVT_KEYTYPE_DMG_SECTION = 11;
    public const int EVT_KEYTYPE_GAME_PRODUCTID = 12;
    public const int EVT_KEYTYPE_DMG_CATEGORY = 13;
    public const int EVT_KEYTYPE_SCORE = 14;
    public const int EVT_KEYTYPE_ENUMERATION = 15;
    public const int EVT_KEYTYPE_TICKERID = 16;
    public const int EVT_KEYTYPE_BANNERID = 17;
    public const int EVT_KEYTYPE_MESSAGEID = 18;
  }
}
