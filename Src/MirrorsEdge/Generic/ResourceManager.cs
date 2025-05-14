
// Type: generic.ResourceManager
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using microedition.m3g;
using midp;
using mirrorsedge_wp7;
using support;
using System;
using System.Collections.Generic;

#nullable disable
namespace generic
{
  public class ResourceManager
  {
    public const int SOUNDEVENTPOOL_FOOTSTEPS_CONCRETE = 0;
    public const int SOUNDEVENTPOOL_FOOTSTEPS_METALCRANE = 1;
    public const int SOUNDEVENTPOOL_FOOTSTEPS_METALDUCT = 2;
    public const int SOUNDEVENTPOOL_FOOTSTEPS_METALGANTRY = 3;
    public const int SOUNDEVENTPOOL_FOOTSTEPS_WOODPLANKS = 4;
    public const int SOUNDEVENTPOOL_BREATHS_NOSE_IN = 0;
    public const int SOUNDEVENTPOOL_BREATHS_NOSE_OUT = 1;
    public const int SOUNDEVENTPOOL_BREATHS_MOUTH_IN = 2;
    public const int SOUNDEVENTPOOL_BREATHS_MOUTH_OUT = 3;
    public const int SOUNDEVENTPOOL_BULLET_IMPACTS_GLOCK = 0;
    public const int SOUNDEVENTPOOL_MELEE_FIST_HEAD = 0;
    public const int SOUNDEVENTPOOL_MELEE_FOOT_BODY = 1;
    public const int SOUNDGROUP_MUSIC = 0;
    public const int SOUNDGROUP_UI = 1;
    public const int SOUNDGROUP_FAITH = 2;
    public const int SOUNDGROUP_AMBIENT = 3;
    public const int SOUNDGROUP_ENEMY = 4;
    public const int SOUNDGROUP_PITCHSHIFTED = 5;
    public const int SOUNDGROUP_EFFECTS = 6;
    public const int SOUNDFLAG_STATIC = 1;
    public const int SOUNDGROUPFLAG_MUSIC = 1;
    public const int SOUNDGROUPFLAG_SFX = 2;
    public const int FONTSTYLE_STYLE_FILL = 0;
    public const int FONTSTYLE_STYLE_OUTLINE = 1;
    public const int FONTSTYLE_STYLE_STROKE = 2;
    public const int FONT_DEBUG_TEXT = 0;
    public const int FONT_DEBUG_SMALL = 1;
    public const int FONT_REGULAR_BLACK = 2;
    public const int FONT_REGULAR_RED = 3;
    public const int FONT_COUNTDOWN_RED = 4;
    public const int FONT_SMALL_WHITE = 5;
    public const int FONT_MENU_ITEM_BLACK = 6;
    public const int FONT_MENU_ITEM_WHITE = 7;
    public const int FONT_MENU_ITEM_RED = 8;
    public const int FONT_INTRO_TAP_MESSAGE = 9;
    public const int FONT_MENU_ITEM_STRONG_WHITE = 10;
    public const int FONT_MENU_ACTIVE_SUBMENU = 11;
    public const int FONT_MENU_ITEM_STRONG_BLACK = 12;
    public const int FONT_MENU_SCROLL_ITEM = 13;
    public const int FONT_TITLE_LIGHT = 14;
    public const int FONT_SUB_TITLE_LIGHT = 15;
    public const int FONT_TITLE_BLACK = 16;
    public const int FONT_BADGES_COLUMN_TITLE = 17;
    public const int FONT_BADGES_NAME = 18;
    public const int FONT_BADGES_DESC = 19;
    public const int FONT_LOADING_LARGE_ORANGE = 20;
    public const int FONT_LOADING_SMALL_ORANGE = 21;
    public const int FONT_LOADING_LARGE_RED = 22;
    public const int FONT_LOADING_SMALL_RED = 23;
    public const int FONT_LOADING_LARGE_BLUE = 24;
    public const int FONT_LOADING_SMALL_BLUE = 25;
    public const int FONT_LEVEL_COMPLETE_TITLE = 26;
    public const int FONT_LEVEL_COMPLETE_SUBTITLE = 27;
    public const int FONT_LEVEL_COMPLETE_BAG_COUNT = 28;
    public const int FONT_LEVEL_COMPLETE_TIME_LABEL = 29;
    public const int FONT_LEVEL_COMPLETE_TIME = 30;
    public const int FONT_LEVEL_COMPLETE_BEST_TIME = 31;
    public const int FONT_LEVEL_COMPLETE_BEST_TIME_LABEL = 32;
    public const int FONT_PAUSED_TITLE_LARGE = 33;
    public const int FONT_PAUSED_TITLE_SMALL = 34;
    public const int FONT_UPSELL_TITLE_WHITE = 35;
    public const int FONT_UPSELL_TITLE_BLACK = 36;
    public const int ANIM_FAITH_LOADING = 0;
    public const int ANIM_NULL = 1;
    public const int ANIM_VIDEO = 2;
    public const int ANIM_COUNT = 3;
    public const int FRAME_COUNT = 14;
    public const int IMAGE_FILE_COUNT = 1;
    public const int IMAGE_SUBIMAGE_COUNT = 12;
    public const int PRIMITIVE_COUNT = 12;
    public const int PRIMITIVE_ATTRIB_COUNT = 48;
    public const int OBJECT_FLAG_PLAYER = 1;
    public const int OBJECT_FLAG_PERSON = 2;
    public const int OBJECT_FLAG_POLICE = 4;
    public const int OBJECT_FLAG_COLLECTABLE = 8;
    public const int OBJECT_FLAG_UPDATE = 16;
    public const int COLLIDES_WITH_NOTHING = 0;
    public const int COLLIDES_WITH_PLAYER = 1;
    public const int COLLIDES_WITH_EVERYTHING = 2;
    public const int OBJECT_TYPE_FAITH = 0;
    public const int OBJECT_TYPE_FAITH_GHOST = 1;
    public const int OBJECT_TYPE_POLICE_LIGHT = 2;
    public const int OBJECT_TYPE_POLICE_RIOT = 3;
    public const int OBJECT_TYPE_RIVAL_RUNNER = 4;
    public const int OBJECT_TYPE_RIVAL_RUNNER_BOSS = 5;
    public const int OBJECT_TYPE_CHOPPER = 6;
    public const int OBJECT_TYPE_PLANE = 7;
    public const int OBJECT_TYPE_EFFECT = 8;
    public const int OBJECT_TYPE_PLAYER_FINISH = 9;
    public const int OBJECT_TYPE_PLAYER_SOUND = 10;
    public const int OBJECT_TYPE_AMBIENT_SOUND = 11;
    public const int OBJECT_TYPE_MUSIC_CUE = 12;
    public const int OBJECT_TYPE_SOUND_ONESHOT = 13;
    public const int OBJECT_TYPE_CHECKPOINT = 14;
    public const int OBJECT_TYPE_POPUP_BOX = 15;
    public const int OBJECT_TYPE_ZIPLINE = 16;
    public const int OBJECT_TYPE_SWING = 17;
    public const int OBJECT_TYPE_REDBAG = 18;
    public const int OBJECT_TYPE_YELLOWBAG = 19;
    public const int OBJECT_TYPE_BULLET = 20;
    public const int OBJECT_TYPE_EFFECT_PIGEON_TAKEOFF = 21;
    public const int OBJECT_TYPE_EFFECT_HIT = 22;
    public const int MATERIAL_CONCRETE = 0;
    public const int MATERIAL_METALCRANE = 1;
    public const int MATERIAL_METALDUCT = 2;
    public const int MATERIAL_WOODPLANKS = 3;
    public const int MATERIAL_METALGANTRY = 4;
    public const int MATERIAL_METALPIPE = 5;
    public const int USERID_APP_ALPHA = 20;
    public const int USERID_APP_ALPHA_ADD = 21;
    public const int USERID_APP_MODULATE = 22;
    public const int USERID_APP_REPLACE = 23;
    public const int USERID_APP_ALPHA_THRESHOLD = 24;
    public const int USERID_APP_MAP_PRIMARY_BUILD_REPLACE = 25;
    public const int USERID_APP_TEX1_REPLACE = 25;
    public const int USERID_APP_MAP_PRIMARY_BUILD_ALPHA_THRESHOLD = 26;
    public const int USERID_APP_TEX1_ALPHA_THRESHOLD = 26;
    public const int USERID_APP_MAP_PRIMARY_OBJECT_1_REPLACE = 27;
    public const int USERID_APP_TEX2_REPLACE = 27;
    public const int USERID_APP_MAP_PRIMARY_OBJECT_1_ALPHA_THRESHOLD = 28;
    public const int USERID_APP_TEX2_ALPHA_THRESHOLD = 28;
    public const int USERID_APP_MAP_SKYDOME_REPLACE = 29;
    public const int USERID_APP_MAP_PRIMARY_BUILD_ALPHA = 30;
    public const int USERID_APP_TEX1_ALPHA = 30;
    public const int USERID_APP_MAP_PRIMARY_OBJECT_1_ALPHA = 31;
    public const int USERID_APP_TEX2_ALPHA = 31;
    public const int USERID_APP_MAP_TERTIARY_ALPHA_THRESHOLD = 32;
    public const int USERID_APP_MAP_PRIMARY_OBJECT_2_REPLACE = 33;
    public const int USERID_APP_TEX3_REPLACE = 33;
    public const int USERID_APP_MAP_PRIMARY_OBJECT_2_ALPHA = 34;
    public const int USERID_APP_TEX3_ALPHA = 34;
    public const int USERID_APP_MAP_PRIMARY_OBJECT_2_ALPHA_THRESHOLD = 35;
    public const int USERID_APP_TEX3_ALPHA_THRESHOLD = 35;
    public const int USERID_APP_TEX_SMOKE_ALPHA = 36;
    public const int USERID_APP_TEX_SMOKE_ALPHA_ADD = 37;
    public const int USERID_APP_TEX_STEAMJET_ALPHA_ADD = 38;
    public const int USERID_APP_MAP_PRIMARY_BUILD_ALPHA_ADD = 39;
    public const int USERID_APP_TEX1_ALPHA_ADD = 39;
    public const int USERID_APP_MAP_PRIMARY_OBJECT_1_ALPHA_ADD = 40;
    public const int USERID_APP_TEX2_ALPHA_ADD = 40;
    public const int USERID_APP_MAP_PRIMARY_OBJECT_2_ALPHA_ADD = 41;
    public const int USERID_APP_TEX3_ALPHA_ADD = 41;
    public const int USERID_APP_MAP_SECONDARY_REPLACE = 42;
    public const int USERID_APP_MAP_SECONDARY_ALPHA_THRESHOLD = 43;
    public const int USERID_APP_MAP_GRAFFITI_ALPHA = 44;
    public const int USERID_MESH_FAITH_SKINNEDMESH1_SKELETON_PELVIS = 100;
    public const int USERID_JOINT_ORIGIN_MOVEMENT = 101;
    public const int USERID_MESH_FAITH_BODY = 102;
    public const int USERID_MESH_RUNNER = 103;
    public const int USERID_MESH_BOSS_RUNNER = 104;
    public const int USERID_MESH_FAITH_HAIR_SKINNEDMESH1_SKELETON_RTOE = 105;
    public const int USERID_MESH_FAITH_HAIR_SKINNEDMESH1_SKELETON_LTOE = 106;
    public const int USERID_MESH_FAITH_HAIR_SKINNEDMESH1_SKELETON_RANKLE = 107;
    public const int USERID_MESH_FAITH_HAIR_SKINNEDMESH1_SKELETON_LANKLE = 108;
    public const int USERID_EYESANDTEETH_SKINNEDMESH1_SKELETON_RWRIST = 109;
    public const int USERID_EYESANDTEETH_SKINNEDMESH1_SKELETON_LWRIST = 110;
    public const int USERID_CAMERA_POSITION = 120;
    public const int USERID_SKY_DOME = 150;
    public const int USERID_BUILDINGS_SECONDARY_200 = 151;
    public const int USERID_BUILDINGS_TERTIARY_100 = 152;
    public const int USERID_JOINT_CAMERA_LOOK_AT = 175;
    public const int USERID_JOINT_CAMERA_LOOK_FROM = 176;
    public const int USERID_ZIP_ATTACH = 200;
    public const int USERID_INTOBJ_CP_DISH_RUNNERVISION_MESH1 = 201;
    public const int USERID_INTOBJ_CP_DISH_SHADOW_RUNNERVISION_MESH1 = 201;
    public const int USERID_INTOBJ_CP_DISH_DISH_RUNNERVISION_MESH1 = 202;
    public const int USERID_INTOBJ_CP_DISH_SHADOW_DISH_RUNNERVISION_MESH1 = 202;
    public const int USERID_FXOBJ_SMOKE_T1 = 203;
    public const int USERID_PLAYER_START = 252;
    public const int USERID_PLAYER_FINISH = 253;
    public const int USERID_EFF_STAR_LARGE_MESH1 = 254;
    public const int USERID_EFF_STAR_SMALL_MESH1 = 255;
    public const int USERID_COLOBJ_REDBAG = 270;
    public const int USERID_COLOBJ_YELLOWBAG = 271;
    public const int USERID_MESH_POLICE_RIOT = 300;
    public const int USERID_MESH_POLICE_LIGHT = 301;
    public const int USERID_LARGEROTOR = 401;
    public const int USERID_REARROTOR = 402;
    public const int USERID_MESHMUZZLEFLASH_LEFT = 403;
    public const int USERID_MESHMUZZLEFLASH_RIGHT = 404;
    public const int USERID_MESH_LIGHT01 = 405;
    public const int USERID_MESH_LIGHT02 = 406;
    public const int USERID_MESH_LIGHT03 = 407;
    public const int USERID_MESH_LIGHT04 = 408;
    public const int USERID_BUILD_STRAIGHT_03 = 65536;
    public const int USERID_BUILD_STRAIGHT_AC_03 = 65537;
    public const int USERID_BUILD_INSET_AC_RIGHT_04 = 65538;
    public const int USERID_BUILD_INSET_AC_LEFT_04 = 65539;
    public const int USERID_BUILD_SCAFOLDING_PLAT_RUNNERVISION = 65540;
    public const int USERID_BUILD_SCAFOLDING_PLAT = 65541;
    public const int USERID_BUILD_SCAFOLDING_MID = 65542;
    public const int USERID_BUILD_SCAFOLDING_PLAT_SHADOW_RUNNERVISION = 65543;
    public const int USERID_BUILD_SCAFOLDING_PLAT_SHADOW = 65544;
    public const int USERID_BUILD_SCAFOLDING_PLAT_SLOPE_LEFT_RUNNERVISION = 65545;
    public const int USERID_BUILD_SCAFOLDING_PLAT_SLOPE_LEFT = 65546;
    public const int USERID_BUILD_SCAFOLDING_PLAT_SINGLE_RUNNERVISION = 65547;
    public const int USERID_BUILD_SCAFOLDING_PLAT_SINGLE = 65548;
    public const int USERID_BUILD_SCAFOLDING_MID_RIGHT = 65549;
    public const int USERID_BUILD_SCAFOLDING_TOP_RIGHT = 65550;
    public const int USERID_BUILD_SCAFOLDING_TOP = 65551;
    public const int USERID_BUILD_SCAFOLDING_PLAT_SINGLE_SHADOW_RUNNERVISION = 65552;
    public const int USERID_BUILD_SCAFOLDING_PLAT_SINGLE_SHADOW = 65553;
    public const int USERID_BUILD_SCAFOLDING_BASE = 65554;
    public const int USERID_BUILD_SCAFOLDING_BASE_RIGHT = 65555;
    public const int USERID_BUILD_INSET_05 = 65556;
    public const int USERID_BUILD_INSET_RIGHT_04 = 65557;
    public const int USERID_BUILD_INSET_LEFT_04 = 65558;
    public const int USERID_BUILD_SCAFOLDING_PLAT_SLOPE_RIGHT_RUNNERVISION = 65559;
    public const int USERID_BUILD_SCAFOLDING_PLAT_SLOPE_RIGHT = 65560;
    public const int USERID_BUILD_SLOPE_LEFT_05_10_VENTPIPE = 65561;
    public const int USERID_BUILD_SLOPE_LEFT_05_10 = 65562;
    public const int USERID_BUILD_SLOPE_RIGHT_05_10_VENTPIPE = 65563;
    public const int USERID_BUILD_SLOPE_RIGHT_05_10 = 65564;
    public const int USERID_BUILD_STRAIGHT_THIN_MID_RIGHT_05_04 = 65565;
    public const int USERID_BUILD_STRAIGHT_THIN_MID_05_05 = 65566;
    public const int USERID_BUILD_STRAIGHT_THIN_06_05 = 65567;
    public const int USERID_BUILD_STRAIGHT_THIN_RIGHT_06_04 = 65568;
    public const int USERID_BUILD_STRAIGHT_THIN_LEFT_06_04 = 65569;
    public const int USERID_BUILD_STRAIGHT_THIN_06_10 = 65570;
    public const int USERID_BUILD_STRAIGHT_THIN_BASE_LEFT_05_04 = 65571;
    public const int USERID_BUILD_STRAIGHT_THIN_BASE_05_05 = 65572;
    public const int USERID_BUILD_STRAIGHT_THIN_BASE_RIGHT_05_04 = 65573;
    public const int USERID_BUILD_STRAIGHT_THIN_BASE_05_10 = 65574;
    public const int USERID_BUILD_STRAIGHT_THIN_MID_05_10 = 65575;
    public const int USERID_BUILD_STRAIGHT_THIN_06_03 = 65576;
    public const int USERID_BUILD_STRAIGHT_THIN_MID_05_03 = 65577;
    public const int USERID_BUILD_STRAIGHT_THIN_BASE_05_03 = 65578;
    public const int USERID_BUILD_STRAIGHT_THIN_OH_06_10 = 65579;
    public const int USERID_BUILD_STRAIGHT_THIN_OH_RIGHT_06_04 = 65580;
    public const int USERID_BUILD_STRAIGHT_THIN_OH_06_05 = 65581;
    public const int USERID_BUILD_STRAIGHT_THIN_OH_06_03 = 65582;
    public const int USERID_BUILD_STRAIGHT_THIN_OH_LEFT_06_04 = 65583;
    public const int USERID_BUILD_STRAIGHT_THIN_OH_SHADOW_06_10 = 65584;
    public const int USERID_BUILD_STRAIGHT_THIN_OH_SHADOW_06_05 = 65585;
    public const int USERID_BUILD_STRAIGHT_THIN_OH_SHADOW_06_03 = 65586;
    public const int USERID_BUILD_STRAIGHT_THIN_OH_CAP_03_10_VENT = 65587;
    public const int USERID_BUILD_STRAIGHT_THIN_OH_CAP_03_10 = 65588;
    public const int USERID_BUILD_STRAIGHT_THIN_OH_CAP_03_05_VENT = 65589;
    public const int USERID_BUILD_STRAIGHT_THIN_OH_CAP_03_05 = 65590;
    public const int USERID_BUILD_STRAIGHT_THIN_OH_CAP_03_03_VENT = 65591;
    public const int USERID_BUILD_STRAIGHT_THIN_OH_CAP_03_03 = 65592;
    public const int USERID_BUILD_STRAIGHT_THIN_MID_LEFT_05_04 = 65593;
    public const int USERID_BUILD_STRAIGHT_THIN_OH_MID_LEFT_06_04 = 65594;
    public const int USERID_BUILD_STRAIGHT_THIN_OH_MID_06_03 = 65595;
    public const int USERID_BUILD_STRAIGHT_THIN_OH_MID_06_05 = 65596;
    public const int USERID_BUILD_STRAIGHT_THIN_OH_MID_06_10 = 65597;
    public const int USERID_BUILD_STRAIGHT_THIN_OH_MID_RIGHT_06_04 = 65598;
    public const int USERID_BUILD_STRAIGHT_THIN_OH_CAP_RIGHT_03_04_VENT = 65599;
    public const int USERID_BUILD_STRAIGHT_THIN_OH_CAP_RIGHT_03_04 = 65600;
    public const int USERID_BUILD_STRAIGHT_THIN_OH_CAP_LEFT_03_04_VENT = 65601;
    public const int USERID_BUILD_STRAIGHT_THIN_OH_CAP_LEFT_03_04 = 65602;
    public const int USERID_BUILD_STRAIGHT_THIN_OH_SHADOW_LEFT_06_04 = 65603;
    public const int USERID_BUILD_STRAIGHT_THIN_OH_SHADOW_RIGHT_06_04 = 65604;
    public const int USERID_BUILD_SCAFOLDING_PLAT_VERT_LEFT_RUNNERVISION = 65605;
    public const int USERID_BUILD_SCAFOLDING_PLAT_VERT_LEFT = 65606;
    public const int USERID_BUILD_SCAFOLDING_PLAT_VERT_RIGHT_RUNNERVISION = 65607;
    public const int USERID_BUILD_SCAFOLDING_PLAT_VERT_RIGHT = 65608;
    public const int USERID_BUILD_STRAIGHT_OVERHANG_LEFT_04_RUNNERVISION = 65609;
    public const int USERID_BUILD_STRAIGHT_OVERHANG_LEFT_04 = 65610;
    public const int USERID_BUILD_STRAIGHT_OVERHANG_03_RUNNERVISION = 65611;
    public const int USERID_BUILD_STRAIGHT_OVERHANG_03 = 65612;
    public const int USERID_BUILD_STRAIGHT_OVERHANG_5_RUNNERVISION = 65613;
    public const int USERID_BUILD_STRAIGHT_OVERHANG_5 = 65614;
    public const int USERID_BUILD_STRAIGHT_OVERHANG_10_RUNNERVISION = 65615;
    public const int USERID_BUILD_STRAIGHT_OVERHANG_10 = 65616;
    public const int USERID_BUILD_STRAIGHT_OVERHANG_RIGHT_04_RUNNERVISION = 65617;
    public const int USERID_BUILD_STRAIGHT_OVERHANG_RIGHT_04 = 65618;
    public const int USERID_BUILD_INSET_AC_05 = 65619;
    public const int USERID_BUILD_INSET_AC_10 = 65620;
    public const int USERID_BUILD_STRAIGHT_AC_10 = 65621;
    public const int USERID_BUILD_STRAIGHT_05 = 65622;
    public const int USERID_BUILD_STRAIGHT_AC_LEFT_12 = 65623;
    public const int USERID_BUILD_STRAIGHT_INTENTRY_10 = 65624;
    public const int USERID_BUILD_INSET_10 = 65625;
    public const int USERID_BUILD_STRAIGHT_AC_05 = 65626;
    public const int USERID_BUILD_STRAIGHT_INSETBASE_05 = 65627;
    public const int USERID_BUILD_STRAIGHT_AC_RIGHT_06 = 65628;
    public const int USERID_BUILD_STRAIGHT_AC_LEFT_05 = 65629;
    public const int USERID_BUILD_STRAIGHT_10 = 65630;
    public const int USERID_BUILD_STRAIGHT_LEFT_04 = 65631;
    public const int USERID_BUILD_STRAIGHT_AC_LEFT_03 = 65632;
    public const int USERID_BUILD_STRAIGHT_INSETBASE_01 = 65633;
    public const int USERID_BUILD_STRAIGHT_RIGHT_04 = 65634;
    public const int USERID_BUILD_STRAIGHT_AC_RIGHT_03 = 65635;
    public const int USERID_BUILD_DOOR_RIGHT = 65636;
    public const int USERID_INTOBJ_BOX_A_02_RUNNERVISION = 131072;
    public const int USERID_INTOBJ_BOX_A_02 = 131073;
    public const int USERID_INTOBJ_AC_VENT_A_RUNNERVISION = 131074;
    public const int USERID_INTOBJ_AC_VENT_A = 131075;
    public const int USERID_INTOBJ_AC_SHADOW_A_RUNNERVISION = 131076;
    public const int USERID_INTOBJ_AC_SHADOW_A = 131077;
    public const int USERID_INTOBJ_AC_VENT_B_RUNNERVISION = 131078;
    public const int USERID_INTOBJ_AC_VENT_B = 131079;
    public const int USERID_INTOBJ_BOX_A_SHADOW_02_RUNNERVISION = 131080;
    public const int USERID_INTOBJ_BOX_A_SHADOW_02 = 131081;
    public const int USERID_INTOBJ_AC_A_RUNNERVISION = 131082;
    public const int USERID_INTOBJ_AC_A = 131083;
    public const int USERID_INTOBJ_SIGN_03_12 = 131084;
    public const int USERID_INTOBJ_SIGN_05_07 = 131085;
    public const int USERID_INTOBJ_SIGN_05_10 = 131086;
    public const int USERID_INTOBJ_SIGN_INSET_05 = 131087;
    public const int USERID_INTOBJ_VENTBRIDGE_LEFT_02_RUNNERVISION = 131088;
    public const int USERID_INTOBJ_VENTBRIDGE_LEFT_02 = 131089;
    public const int USERID_INTOBJ_VENTBRIDGE_RIGHT_02_RUNNERVISION = 131090;
    public const int USERID_INTOBJ_VENTBRIDGE_RIGHT_02 = 131091;
    public const int USERID_INTOBJ_VENTBRIDGE_05_RUNNERVISION = 131092;
    public const int USERID_INTOBJ_VENTBRIDGE_05 = 131093;
    public const int USERID_INTOBJ_VENTBRIDGE_COMB_05_RUNNERVISION = 131094;
    public const int USERID_INTOBJ_VENTBRIDGE_COMB_05 = 131095;
    public const int USERID_INTOBJ_SWINGSTAGE_05_RUNNERVISION = 131096;
    public const int USERID_INTOBJ_SWINGSTAGE_05 = 131097;
    public const int USERID_INTOBJ_POLE_RUNNERVISION = 131098;
    public const int USERID_INTOBJ_POLE = 131099;
    public const int USERID_INTOBJ_CRANE_SLOPE_RIGHT_10_RUNNERVISION = 131100;
    public const int USERID_INTOBJ_CRANE_SLOPE_RIGHT_10 = 131101;
    public const int USERID_INTOBJ_CRANE_SLOPE_RIGHT_20_RUNNERVISION = 131102;
    public const int USERID_INTOBJ_CRANE_SLOPE_RIGHT_20 = 131103;
    public const int USERID_INTOBJ_CRANE_SLOPE_LEFT_10_RUNNERVISION = 131104;
    public const int USERID_INTOBJ_CRANE_SLOPE_LEFT_10 = 131105;
    public const int USERID_INTOBJ_CRANE_SLOPE_LEFT_20_RUNNERVISION = 131106;
    public const int USERID_INTOBJ_CRANE_SLOPE_LEFT_20 = 131107;
    public const int USERID_INTOBJ_RAMP_LARGE_LEFT_RUNNERVISION = 131108;
    public const int USERID_INTOBJ_RAMP_LARGE_LEFT = 131109;
    public const int USERID_INTOBJ_RAMP_LARGE_RIGHT_RUNNERVISION = 131110;
    public const int USERID_INTOBJ_RAMP_LARGE_RIGHT = 131111;
    public const int USERID_INTOBJ_ZIPLINE_ZIP_ATTACH = 131112;
    public const int USERID_INTOBJ_ZIPLINE_RUNNERVISION = 131113;
    public const int USERID_INTOBJ_ZIPLINE = 131114;
    public const int USERID_INTOBJ_AC_FILLER_RUNNERVISION = 131115;
    public const int USERID_INTOBJ_AC_FILLER = 131116;
    public const int USERID_INTOBJ_CRANE_A_08_RUNNERVISION = 131117;
    public const int USERID_INTOBJ_CRANE_A_08 = 131118;
    public const int USERID_INTOBJ_CRANE_A_15_RUNNERVISION = 131119;
    public const int USERID_INTOBJ_CRANE_A_15 = 131120;
    public const int USERID_INTOBJ_RAMP_LEFT_RUNNERVISION = 131121;
    public const int USERID_INTOBJ_RAMP_LEFT = 131122;
    public const int USERID_INTOBJ_RAMP_RIGHT_RUNNERVISION = 131123;
    public const int USERID_INTOBJ_RAMP_RIGHT = 131124;
    public const int USERID_INTOBJ_CRANE_BASE = 131125;
    public const int USERID_INTOBJ_CRANE_BASEEXTEND = 131126;
    public const int USERID_INTOBJ_SIGN_03_07 = 131127;
    public const int USERID_INTOBJ_CP_DISH_SHADOW_RUNNERVISION = 131128;
    public const int USERID_INTOBJ_CP_DISH_SHADOW_DISH_RUNNERVISION = 131129;
    public const int USERID_INTOBJ_CP_DISH_SHADOW = 131130;
    public const int USERID_INTOBJ_CP_DISH_RUNNERVISION = 131131;
    public const int USERID_INTOBJ_CP_DISH_DISH_RUNNERVISION = 131132;
    public const int USERID_INTOBJ_CP_DISH = 131133;
    public const int USERID_INTOBJ_SWINGSTAGE_10_RUNNERVISION = 131134;
    public const int USERID_INTOBJ_SWINGSTAGE_10 = 131135;
    public const int USERID_INTOBJ_FENCE_03 = 131136;
    public const int USERID_INTOBJ_ZIPLINE_WALL_RIGHT_ZIP_ATTACH = 131137;
    public const int USERID_INTOBJ_ZIPLINE_WALL_RIGHT = 131138;
    public const int USERID_INTOBJ_ZIPLINE_WALL_LEFT_ZIP_ATTACH = 131139;
    public const int USERID_INTOBJ_ZIPLINE_WALL_LEFT = 131140;
    public const int USERID_INTOBJ_SWINGSTAGE_02_RUNNERVISION = 131141;
    public const int USERID_INTOBJ_SWINGSTAGE_02 = 131142;
    public const int USERID_INTOBJ_SIGN_INSET_10 = 131143;
    public const int USERID_SCNOBJ_AC_A = 196608;
    public const int USERID_SCNOBJ_AC_FAN_B = 196609;
    public const int USERID_SCNOBJ_AC_FAN_SHADOW_A = 196610;
    public const int USERID_SCNOBJ_POWERBOX_A = 196611;
    public const int USERID_SCNOBJ_BOX_A = 196612;
    public const int USERID_SCNOBJ_AC_FAN_A = 196613;
    public const int USERID_SCNOBJ_ANTEN_HIGH = 196614;
    public const int USERID_SCNOBJ_ANTEN_LOW = 196615;
    public const int USERID_SCNOBJ_AC_LARGE_B = 196616;
    public const int USERID_SCNOBJ_WALLFAN_SHADOW = 196617;
    public const int USERID_SCNOBJ_PIPEVENT_HIGH_A = 196618;
    public const int USERID_SCNOBJ_AC_LARGE_A = 196619;
    public const int USERID_SCNOBJ_AC_VENT_FAR = 196620;
    public const int USERID_SCNOBJ_AC_SYSTEM = 196621;
    public const int USERID_SCNOBJ_AC_VENT_CLOSE = 196622;
    public const int USERID_SCNOBJ_RAILING_LEFT = 196623;
    public const int USERID_SCNOBJ_PIPEVENT_LOW_A = 196624;
    public const int USERID_SCNOBJ_VENTBRIDGE_LEFT_02 = 196625;
    public const int USERID_SCNOBJ_VENTBRIDGE_RIGHT_02 = 196626;
    public const int USERID_SCNOBJ_VENTBRIDGE_05 = 196627;
    public const int USERID_SCNOBJ_VENTBRIDGE_COMB_05 = 196628;
    public const int USERID_SCNOBJ_AC_SYSTEM_A = 196629;
    public const int USERID_SCNOBJ_AC_SYSTEM_B = 196630;
    public const int USERID_SCNOBJ_AC_SYSTEM_C = 196631;
    public const int USERID_SCNOBJ_AC_SYSTEM_D = 196632;
    public const int USERID_SCNOBJ_AC_SYSTEM_E = 196633;
    public const int USERID_SCNOBJ_AC_SYSTEM_F = 196634;
    public const int USERID_SCNOBJ_AC_SYSTEM_G = 196635;
    public const int USERID_SCNOBJ_AC_SYSTEM_H = 196636;
    public const int USERID_SCNOBJ_AC_SYSTEM_I = 196637;
    public const int USERID_SCNOBJ_AC_SYSTEM_K = 196638;
    public const int USERID_SCNOBJ_AC_SYSTEM_L = 196639;
    public const int USERID_SCNOBJ_SMOKEVENT = 196640;
    public const int USERID_SCNOBJ_RAILING_RIGHT = 196641;
    public const int USERID_SCNOBJ_BUILD_SECONDARY_1 = 196642;
    public const int USERID_SCNOBJ_BUILD_SECONDARY_3 = 196643;
    public const int USERID_SCNOBJ_BUILD_SECONDARY_2 = 196644;
    public const int USERID_SCNOBJ_HIDEOUT = 196645;
    public const int USERID_SCNOBJ_RADIOTOWER = 196646;
    public const int USERID_SCNOBJ_HELIPAD = 196647;
    public const int USERID_SCNOBJ_SIGN_AD_01 = 196648;
    public const int USERID_SCNOBJ_SIGN_AD_02 = 196649;
    public const int USERID_SCNOBJ_SIGN_AD_03 = 196650;
    public const int USERID_SCNOBJ_SIGN_AD_04 = 196651;
    public const int USERID_SCNOBJ_SIGN_AD_05 = 196652;
    public const int USERID_SCNOBJ_SIGN_AD_06 = 196653;
    public const int USERID_SCNOBJ_SIGN_AD_07 = 196654;
    public const int USERID_SCNOBJ_SIGN_AD_08 = 196655;
    public const int USERID_SCNOBJ_SIGN_AD_09 = 196656;
    public const int USERID_SCNOBJ_SIGN_AD_10 = 196657;
    public const int USERID_SCNOBJ_SIGN_AD_11 = 196658;
    public const int USERID_SCNOBJ_SIGN_AD_12 = 196659;
    public const int USERID_SCNOBJ_SIGN_AD_13 = 196660;
    public const int USERID_SCNOBJ_SIGN_AD_14 = 196661;
    public const int USERID_SCNOBJ_SIGN_AD_15 = 196662;
    public const int USERID_SCNOBJ_SIGN_AD_16 = 196663;
    public const int USERID_SCNOBJ_SIGN_AD_17 = 196664;
    public const int USERID_SCNOBJ_SHARD = 196665;
    public const int USERID_ENC_STRAIGHT_LEFT_10 = 262144;
    public const int USERID_ENC_PILLARWINDOW_LEFT_10 = 262145;
    public const int USERID_ENC_STRAIGHT_PIPES_LEFT_10 = 262146;
    public const int USERID_ENC_STRAIGHT_10 = 262147;
    public const int USERID_ENC_STRAIGHT_START_10 = 262148;
    public const int USERID_ENC_STRAIGHT_DEEP_LEFT_10 = 262149;
    public const int USERID_ENC_STRAIGHT_DEEP_10 = 262150;
    public const int USERID_ENC_PILLARWINDOW_10 = 262151;
    public const int USERID_ENC_PILLARWINDOW_RIGHT_10 = 262152;
    public const int USERID_ENC_STRAIGHT_RIGHT_10 = 262153;
    public const int USERID_ENC_STRAIGHT_DEEP_RIGHT_10 = 262154;
    public const int USERID_ENC_STRAIGHT_PIPES_RIGHT_10 = 262155;
    public const int USERID_ENC_STRAIGHT_DEEP_CORRIDOR_10 = 262156;
    public const int USERID_ENC_ELEVATOR_STRAIGHT_20 = 262157;
    public const int USERID_ENC_STRAIGHT_DEEP_LC_LEFT_05 = 262158;
    public const int USERID_ENC_STRAIGHT_DEEP_LC_10 = 262159;
    public const int USERID_ENC_STRAIGHT_DEEP_LC_RIGHT_05 = 262160;
    public const int USERID_ENC_SLOPE_LEFT_10 = 262161;
    public const int USERID_ENC_HIGHFLOOR_20 = 262162;
    public const int USERID_ENC_BRANCH_RIGHT = 262163;
    public const int USERID_ENC_BRANCH_LEFT = 262164;
    public const int USERID_ENC_BRANCH_STRAIGHT_20 = 262165;
    public const int USERID_ENC_SLOPE_LEFT_20 = 262166;
    public const int USERID_ENC_SLOPE_RIGHT_10 = 262167;
    public const int USERID_ENC_ELEVATOR_STRAIGHT_06 = 262168;
    public const int USERID_ENC_SLOPE_RIGHT_20 = 262169;
    public const int USERID_ENC_DUCT_STAIGHT_10 = 262170;
    public const int USERID_ENC_DEEP_DUCT_ENTRY_10 = 262171;
    public const int USERID_ENC_DUCT_TJUNCT_TOP_10 = 262172;
    public const int USERID_ENC_DUCT_TJUNCT_10 = 262173;
    public const int USERID_ENC_DUCT_CAP_LEFT = 262174;
    public const int USERID_ENC_DUCT_VERTSTAIGHT_03 = 262175;
    public const int USERID_ENC_DUCT_VERTSTAIGHT_01 = 262176;
    public const int USERID_ENC_DUCT_VERTSTAIGHT_10 = 262177;
    public const int USERID_ENC_DUCT_CORNER_LEFT = 262178;
    public const int USERID_ENC_DUCT_CORNERTOP_RIGHT = 262179;
    public const int USERID_ENC_DUCT_CAP_RIGHT = 262180;
    public const int USERID_ENC_DUCT_CORNERTOP_LEFT = 262181;
    public const int USERID_ENC_DUCT_CORNER_RIGHT = 262182;
    public const int USERID_ENC_SLOPE_CAP_RIGHT = 262183;
    public const int USERID_ENC_SLOPE_CAP_LEFT = 262184;
    public const int USERID_ENC_ELEVATOR_INTSECTION_20 = 262185;
    public const int USERID_ENC_STRAIGHT_LEFT_BROKEN = 262186;
    public const int USERID_ENC_EXIT_LEFT = 262187;
    public const int USERID_ENC_EXIT_RIGHT = 262188;
    public const int USERID_ENC_STRAIGHT_HALL_10 = 262189;
    public const int USERID_ENC_DUCT_ENTRY_10 = 262190;
    public const int USERID_ENC_INTOBJ_CAMERA_RUNNERVISION = 327680;
    public const int USERID_ENC_INTOBJ_CAMERA = 327681;
    public const int USERID_ENC_INTOBJ_FLOORVENT_RUNNERVISION = 327682;
    public const int USERID_ENC_INTOBJ_FLOORVENT = 327683;
    public const int USERID_ENC_INTOBJ_FLOORVENT_DEEP_RUNNERVISION = 327684;
    public const int USERID_ENC_INTOBJ_FLOORVENT_DEEP = 327685;
    public const int USERID_ENC_INTOBJ_PIPING_DEEP_RUNNERVISION = 327686;
    public const int USERID_ENC_INTOBJ_PIPING_DEEP = 327687;
    public const int USERID_ENC_INTOBJ_PIPING_SLIDE_DEEP_RUNNERVISION = 327688;
    public const int USERID_ENC_INTOBJ_PIPING_SLIDE_DEEP = 327689;
    public const int USERID_ENC_INTOBJ_PIPING_RUNNERVISION = 327690;
    public const int USERID_ENC_INTOBJ_PIPING = 327691;
    public const int USERID_ENC_INTOBJ_PIPING_SLIDE_RUNNERVISION = 327692;
    public const int USERID_ENC_INTOBJ_PIPING_SLIDE = 327693;
    public const int USERID_ENC_INTOBJ_WALLRUN_LEFT_05 = 327694;
    public const int USERID_ENC_INTOBJ_WALLRUN_05 = 327695;
    public const int USERID_ENC_INTOBJ_WALLRUN_RIGHT_05 = 327696;
    public const int USERID_ENC_INTOBJ_POLE_RUNNERVISION = 327697;
    public const int USERID_ENC_INTOBJ_POLE = 327698;
    public const int USERID_ENC_INTOBJ_BOXES_RUNNERVISION = 327699;
    public const int USERID_ENC_INTOBJ_BOXES = 327700;
    public const int USERID_ENC_INTOBJ_ELEVATOR_RIGHT_RUNNERVISION = 327701;
    public const int USERID_ENC_INTOBJ_ELEVATOR_RIGHT = 327702;
    public const int USERID_ENC_INTOBJ_ELEVATOR_LEFT_RUNNERVISION = 327703;
    public const int USERID_ENC_INTOBJ_ELEVATOR_LEFT = 327704;
    public const int USERID_ENC_INTOBJ_FRAME_CAP = 327705;
    public const int USERID_ENC_INTOBJ_PIPE_RIGHT_05_RUNNERVISION = 327706;
    public const int USERID_ENC_INTOBJ_PIPE_RIGHT_05 = 327707;
    public const int USERID_ENC_INTOBJ_PIPE_05_RUNNERVISION = 327708;
    public const int USERID_ENC_INTOBJ_PIPE_05 = 327709;
    public const int USERID_ENC_INTOBJ_PIPE_LEFT_05_RUNNERVISION = 327710;
    public const int USERID_ENC_INTOBJ_PIPE_LEFT_05 = 327711;
    public const int USERID_ENC_INTOBJ_LIFTDOOR_LEFT = 327712;
    public const int USERID_ENC_INTOBJ_LIFTDOOR_RIGHT = 327713;
    public const int USERID_ENC_INTOBJ_ELEVATOR_SLOPE_LEFT = 327714;
    public const int USERID_ENC_INTOBJ_ELEVATOR_SLOPE_RIGHT = 327715;
    public const int USERID_ENC_INTOBJ_STEAMPIPE_RIGHT_RUNNERVISION = 327716;
    public const int USERID_ENC_INTOBJ_STEAMPIPE_RIGHT = 327717;
    public const int USERID_ENC_INTOBJ_STEAMPIPE_LEFT_RUNNERVISION = 327718;
    public const int USERID_ENC_INTOBJ_STEAMPIPE_LEFT = 327719;
    public const int USERID_ENC_INTOBJ_STEAMJET_UP = 327720;
    public const int USERID_ENC_INTOBJ_STEAMJET_DOWN = 327721;
    public const int USERID_ENC_INTOBJ_STEAMJET_RIGHT = 327722;
    public const int USERID_ENC_INTOBJ_STEAMJET_LEFT = 327723;
    public const int USERID_ENC_SCNOBJ_FRAME = 393216;
    public const int USERID_ENC_SCNOBJ_SET1 = 393217;
    public const int USERID_ENC_SCNOBJ_SET2 = 393218;
    public const int USERID_ENC_SCNOBJ_SET3 = 393219;
    public const int USERID_ENC_SCNOBJ_HYDRANT = 393220;
    public const int USERID_ENC_SCNOBJ_JAILBARS = 393221;
    public const int USERID_ENC_SCNOBJ_PIPES_DEEP = 393222;
    public const int USERID_ENC_SCNOBJ_DOORLIGHT_SLOPEUP_LEFT = 393223;
    public const int USERID_ENC_SCNOBJ_DOORLIGHT_SLOPEUP_RIGHT = 393224;
    public const int USERID_ENC_SCNOBJ_BOXSET2 = 393225;
    public const int USERID_ENC_SCNOBJ_BOXSET1 = 393226;
    public const int USERID_ENC_SCNOBJ_BOXSET3 = 393227;
    public const int USERID_ENC_SCNOBJ_GEN_DEEP = 393228;
    public const int USERID_ENC_SCNOBJ_GEN = 393229;
    public const int USERID_ENC_SCNOBJ_PIPES_DEEP_B = 393230;
    public const int USERID_ENC_SCNOBJ_VENTS_FOREGROUND = 393231;
    public const int USERID_ENC_SCNOBJ_VENTS_FOREGROUND_BRANCH = 393232;
    public const int USERID_ENC_SCNOBJ_JAILDOOR = 393233;
    public const int USERID_ENC_SCNOBJ_DOOR_LEFT = 393234;
    public const int USERID_ENC_SCNOBJ_GRAFFITI_01 = 393235;
    public const int USERID_ENC_SCNOBJ_GRAFFITI_02 = 393236;
    public const int USERID_ENC_SCNOBJ_GRAFFITI_03 = 393237;
    public const int USERID_ENC_SCNOBJ_GRAFFITI_04 = 393238;
    public const int USERID_ENC_SCNOBJ_DOOR_RIGHT = 393239;
    public const int USERID_ENC_SCNOBJ_BOXESDEEP = 393240;
    public const int USERID_ENC_SCNOBJ_PIPESBACKGROUND_LEFT_05 = 393241;
    public const int USERID_ENC_SCNOBJ_PIPESBACKGROUND_05 = 393242;
    public const int USERID_ENC_SCNOBJ_PIPESBACKGROUND_RIGHT_05 = 393243;
    public const int USERID_ENC_SCNOBJ_SILL_01 = 393244;
    public const int USERID_ENC_SCNOBJ_SILL_02 = 393245;
    public const int USERID_ENC_SCNOBJ_SILL_03 = 393246;
    public const int USERID_ENC_SCNOBJ_SILL_04 = 393247;
    public const int USERID_ENC_SCNOBJ_DOOR = 393248;
    public const int USERID_ENC_SCNOBJ_DOORLIGHT_LEFT = 393249;
    public const int USERID_ENC_SCNOBJ_DOORLIGHT_RIGHT = 393250;
    public const int USERID_ENC_SCNOBJ_DOORLIGHT_SLOPE_RIGHT = 393251;
    public const int USERID_ENC_SCNOBJ_DOORLIGHT_SLOPE_LEFT = 393252;
    public const int USERID_ENC_SCNOBJ_WALL_LEFT = 393253;
    public const int USERID_ENC_SCNOBJ_WALL_RIGHT = 393254;
    public const int USERID_ENC_SCNOBJ_BKGROUND_NIGHT = 393255;
    public const int USERID_ENC_SCNOBJ_BKGROUND_MIDDAY = 393256;
    public const int USERID_ENC_SCNOBJ_SIGN_RIGHT_02 = 393257;
    public const int USERID_ENC_SCNOBJ_SIGN_RIGHT = 393258;
    public const int USERID_ENC_SCNOBJ_SIGN_LEFT = 393259;
    public const int USERID_ENC_SCNOBJ_SIGN_01 = 393260;
    public const int USERID_ENC_SCNOBJ_SIGN_03 = 393261;
    public const int USERID_ENC_SCNOBJ_SIGN_02 = 393262;
    public const int USERID_ENC_SCNOBJ_SIGN_LEFT_02 = 393263;
    public const int USERID_UND_BASE_20 = 655360;
    public const int USERID_UND_CEILING_20 = 655361;
    public const int USERID_UND_CEILING_LEFT_END_20 = 655362;
    public const int USERID_UND_PILLARTOP_20 = 655363;
    public const int USERID_UND_WALL_LEFT_10 = 655364;
    public const int USERID_UND_WALL_LEFT_20 = 655365;
    public const int USERID_UND_WALL_LEFT_CORNER = 655366;
    public const int USERID_UND_CEILING_RIGHT_END_20 = 655367;
    public const int USERID_UND_WALL_RIGHT_10 = 655368;
    public const int USERID_UND_WALL_RIGHT_20 = 655369;
    public const int USERID_UND_PILLAR_BACK_10 = 655370;
    public const int USERID_UND_PILLAR_BACK_20 = 655371;
    public const int USERID_UND_PILLAR_CAP_BOTTOM_05 = 655372;
    public const int USERID_UND_PILLAR_CAP_TOP_05 = 655373;
    public const int USERID_UND_PILLAR_FRONT_10 = 655374;
    public const int USERID_UND_PILLAR_FRONT_20 = 655375;
    public const int USERID_UND_BACKGROUND = 655376;
    public const int USERID_UND_PILLAR_BACK_CAP_BOTTOM_05 = 655377;
    public const int USERID_UND_PILLAR_BACK_CAP_TOP_05 = 655378;
    public const int USERID_UND_PILLARBASE_20 = 655379;
    public const int USERID_UND_END_BIT = 655380;
    public const int USERID_UND_WALL_RIGHT_CORNER = 655381;
    public const int USERID_UND_SLIDE_10 = 655382;
    public const int USERID_UND_SLIDE_END = 655383;
    public const int USERID_UND_LIFT_SHAFT_BASE = 655384;
    public const int USERID_UND_INTOBJ_PIPE_10_RUNNERVISION = 720896;
    public const int USERID_UND_INTOBJ_PIPE_10 = 720897;
    public const int USERID_UND_INTOBJ_PIPE_BOTTOM_RUNNERVISION = 720898;
    public const int USERID_UND_INTOBJ_PIPE_BOTTOM = 720899;
    public const int USERID_UND_INTOBJ_PIPE_TOP_RUNNERVISION = 720900;
    public const int USERID_UND_INTOBJ_PIPE_TOP = 720901;
    public const int USERID_UND_INTOBJ_PIPE_05_RUNNERVISION = 720902;
    public const int USERID_UND_INTOBJ_PIPE_05 = 720903;
    public const int USERID_UND_INTOBJ_WALKWAY_03_RUNNERVISION = 720904;
    public const int USERID_UND_INTOBJ_WALKWAY_03 = 720905;
    public const int USERID_UND_INTOBJ_WALKWAY_05_RUNNERVISION = 720906;
    public const int USERID_UND_INTOBJ_WALKWAY_05 = 720907;
    public const int USERID_UND_INTOBJ_WALKWAY_LEFT_05_RUNNERVISION = 720908;
    public const int USERID_UND_INTOBJ_WALKWAY_LEFT_05 = 720909;
    public const int USERID_UND_INTOBJ_WALKWAY_RIGHT_05_RUNNERVISION = 720910;
    public const int USERID_UND_INTOBJ_WALKWAY_RIGHT_05 = 720911;
    public const int USERID_UND_INTOBJ_WALKWAY_SLOPE_LEFT_05_RUNNERVISION = 720912;
    public const int USERID_UND_INTOBJ_WALKWAY_SLOPE_LEFT_05 = 720913;
    public const int USERID_UND_INTOBJ_WALKWAY_SLOPE_RIGHT_05_RUNNERVISION = 720914;
    public const int USERID_UND_INTOBJ_WALKWAY_SLOPE_RIGHT_05 = 720915;
    public const int USERID_UND_INTOBJ_WALKWAY_NOLEGS_05_RUNNERVISION = 720916;
    public const int USERID_UND_INTOBJ_WALKWAY_NOLEGS_05 = 720917;
    public const int USERID_UND_INTOBJ_POLE_RUNNERVISION = 720918;
    public const int USERID_UND_INTOBJ_POLE = 720919;
    public const int USERID_UND_INTOBJ_ZIPLINE_RUNNERVISION = 720920;
    public const int USERID_UND_INTOBJ_ZIPLINE = 720921;
    public const int USERID_UND_INTOBJ_SLIDE = 720922;
    public const int USERID_UND_INTOBJ_BEAM_05_RUNNERVISION = 720923;
    public const int USERID_UND_INTOBJ_BEAM_05 = 720924;
    public const int USERID_UND_INTOBJ_BEAM_10_RUNNERVISION = 720925;
    public const int USERID_UND_INTOBJ_BEAM_10 = 720926;
    public const int USERID_UND_INTOBJ_CHECKPOINT_RUNNERVISION = 720927;
    public const int USERID_UND_INTOBJ_CHECKPOINT = 720928;
    public const int USERID_UND_INTOBJ_BOXES_RUNNERVISION = 720929;
    public const int USERID_UND_INTOBJ_BOXES = 720930;
    public const int USERID_UND_SCNOBJ_PILLAR_AIRCON = 786432;
    public const int USERID_UND_SCNOBJ_PILLAR_FUSEBOX = 786433;
    public const int ELEMENT_TYPE_ELEMENT_STRING = 0;
    public const int ELEMENT_TYPE_ELEMENT_IMAGE = 1;
    public const int MODEL_COLOUR_ORANGE = 0;
    public const int MODEL_COLOUR_RED = 1;
    public const int MODEL_COLOUR_BLUE = 2;
    public const int LOADING_SCREEN_TUTE_01 = 0;
    public const int LOADING_SCREEN_TUTE_02 = 1;
    public const int LOADING_SCREEN_CHAPTER_01 = 2;
    public const int LOADING_SCREEN_CHAPTER_01_TRIAL = 1;
    public const int LOADING_SCREEN_CHAPTER_01_02 = 3;
    public const int LOADING_SCREEN_CHAPTER_02 = 4;
    public const int LOADING_SCREEN_CHAPTER_02_02 = 5;
    public const int LOADING_SCREEN_CHAPTER_03 = 6;
    public const int LOADING_SCREEN_CHAPTER_03_02 = 7;
    public const int LOADING_SCREEN_CHAPTER_04 = 8;
    public const int LOADING_SCREEN_CHAPTER_04_02 = 9;
    public const int LOADING_SCREEN_CHAPTER_05 = 10;
    public const int LOADING_SCREEN_CHAPTER_05_02 = 11;
    public const int LOADING_SCREEN_CHAPTER_06 = 12;
    public const int LOADING_SCREEN_CHAPTER_06_02 = 13;
    public const int LOADING_SCREEN_GAME_EPILOGUE = 14;
    public const int ANIMATIONBLENDER_FAITH = 0;
    public const int ANIMATIONBLENDER_POLICE = 1;
    public const int SOUNDTRACK_RUN = 0;
    public const int SOUNDTRACK_CLIMB = 1;
    public const int SOUNDTRACK_JUMP = 2;
    public const int SOUNDTRACK_BOOST_JUMP = 3;
    public const int SOUNDTRACK_ZIPLINE_FAIL = 4;
    public const int SOUNDTRACK_SWING = 5;
    public const int SOUNDTRACK_LAND = 6;
    public const int SOUNDTRACK_ROLL = 7;
    public const int SOUNDTRACK_CRASH = 8;
    public const int SOUNDTRACK_BREATHING = 9;
    public const int SOUNDTRACK_DISARM = 10;
    public const int SEQUENCERFLAGS_LOOPED = 1;
    public const int COLOR_DEFAULT_BLACK = 0;
    public const int COLOR_WHITE = 1;
    public const int NUM_COLORS = 2;
    public const int COLOR_TOTAL_SIZE = 6;
    public const sbyte NULL_VALUE = -1;
    private static Type Idi = (Type) null;
    private static Type Anim3D = (Type) null;
    private static Type ChannelFaith = (Type) null;
    private static Type ChannelPolice = (Type) null;
    private static Type SoundEvent = (Type) null;
    private static Type SoundEventPoolset = (Type) null;
    private static Type SoundEventPoolVocal = (Type) null;
    private static Type SoundEventPoolGunshots = (Type) null;
    private static Dictionary<string, short> Enums = new Dictionary<string, short>();
    public static int[] RESOURCE_FILESIZE_LIST;
    public static readonly int[] RESOURCE_FILESIZE_LIST_FULL = new int[395]
    {
      115808,
      10297,
      388,
      585,
      530249,
      9824,
      503820,
      527077,
      510904,
      767558,
      1020394,
      747638,
      837640,
      1167995,
      1075893,
      375373,
      1579012,
      1138079,
      279617,
      826401,
      17075,
      427,
      243891,
      1905,
      101066,
      2167,
      3771,
      1064851,
      1097522,
      952596,
      1105954,
      1226279,
      1158012,
      962414,
      1192042,
      1224363,
      980587,
      951364,
      1129168,
      1095631,
      1167891,
      1392,
      92742,
      1635,
      83858,
      123456,
      119914,
      7437,
      121823,
      125466,
      122844,
      122301,
      18470,
      4766,
      59415,
      6155,
      384628,
      394005,
      392308,
      425889,
      406632,
      437704,
      414613,
      431590,
      416993,
      85017,
      452180,
      448613,
      415336,
      451215,
      274180,
      475865,
      343979,
      452076,
      330672,
      378717,
      429632,
      400511,
      955,
      1077,
      14047,
      4593,
      23018,
      8288,
      1826,
      119495,
      118448,
      110747,
      121177,
      110471,
      129262,
      253,
      574,
      1084,
      1573,
      201661,
      1098,
      1085,
      1442,
      1470,
      1472,
      1010941,
      8888,
      197151,
      197405,
      197903,
      1683,
      576,
      4955,
      573,
      1445,
      176,
      193,
      360943,
      334043,
      358964,
      412225,
      409767,
      345246,
      336467,
      343402,
      315471,
      360394,
      298718,
      365893,
      305008,
      396190,
      380683,
      275677,
      273963,
      271199,
      254130,
      262514,
      2776,
      11744,
      92715,
      387,
      382,
      254,
      1024,
      430,
      129,
      13760,
      13352,
      15245,
      15882,
      20085,
      17779,
      18865,
      27364,
      16040,
      4851,
      26393,
      27381,
      6,
      71,
      120,
      572,
      1579,
      1994,
      0,
      6154,
      516,
      198,
      1693,
      270,
      9568,
      14643,
      204,
      2513,
      29192,
      5277,
      18545,
      4742,
      49411,
      9362,
      3220,
      3448,
      5847,
      4327,
      217329,
      145405,
      170500,
      332754,
      323362,
      217536,
      204096,
      198958,
      175685,
      215936,
      195278,
      216306,
      172423,
      261812,
      231221,
      177476,
      184332,
      186960,
      143749,
      173519,
      558436,
      550485,
      571041,
      569205,
      605417,
      602040,
      295052,
      586069,
      552084,
      552147,
      137683,
      434940,
      556251,
      569497,
      557492,
      605674,
      605437,
      247556,
      586839,
      574420,
      557245,
      604203,
      595108,
      605368,
      604740,
      604834,
      586537,
      586134,
      50568,
      592127,
      588232,
      588632,
      587473,
      586150,
      587782,
      75814,
      50495,
      2044354,
      4018503,
      75010,
      68100,
      52366,
      57830,
      107906,
      141544,
      147680,
      37554,
      36084,
      35612,
      38906,
      37964,
      35612,
      33280,
      29440,
      38906,
      40316,
      37964,
      39376,
      39376,
      42668,
      31680,
      36544,
      35466,
      48456,
      44778,
      59412,
      56992,
      23468,
      24840,
      20394,
      18202,
      22568,
      14518,
      10448,
      10332,
      8858,
      10254,
      9108,
      13950,
      9406,
      10360,
      9948,
      10232,
      11566,
      9688,
      9606,
      24118,
      23952,
      27040,
      30976,
      42114,
      37792,
      17860,
      17530,
      17774,
      266018,
      38936,
      30116,
      36422,
      42992,
      38670,
      44316,
      36994,
      37524,
      39818,
      23552,
      22670,
      44078,
      17904,
      22538,
      21204,
      19710,
      18296,
      26074,
      17298,
      22464,
      30422,
      21486,
      32380,
      24824,
      20722,
      20262,
      193790,
      23252,
      23350,
      27662,
      23052,
      15792,
      17712,
      16848,
      17610,
      17552,
      17002,
      17184,
      16736,
      16864,
      17280,
      41986,
      37828,
      49066,
      36516,
      36468,
      42332,
      46706,
      41442,
      44078,
      55346,
      42322,
      46784,
      58368,
      47488,
      40064,
      51328,
      46350,
      59584,
      46190,
      34966,
      34966,
      34966,
      179438,
      80388,
      180736,
      151522,
      13696,
      168266,
      16710,
      141818,
      148020,
      230270,
      235622,
      252158,
      20964,
      19278,
      22164,
      33152,
      22040,
      27972,
      32126,
      14492,
      120732,
      98076,
      54986,
      25504,
      26336,
      29504,
      25736,
      26720,
      26752,
      25184,
      28384,
      29734,
      29146,
      12764,
      192512,
      12978
    };
    public static readonly int[] RESOURCE_FILESIZE_LIST_TRIAL = new int[128]
    {
      115808,
      10283,
      391,
      585,
      341065,
      9824,
      503910,
      279419,
      17080,
      427,
      2167,
      1561,
      379567,
      322456,
      1392,
      92742,
      1635,
      25873,
      7437,
      125466,
      18470,
      59415,
      6155,
      70267,
      394005,
      406632,
      475865,
      343979,
      4959,
      2386,
      6164,
      8288,
      1826,
      121177,
      222,
      407,
      459,
      870,
      304,
      316,
      697,
      959,
      586,
      292106,
      2266,
      63809,
      63809,
      63789,
      1430,
      1884,
      4493,
      1923,
      2125,
      141,
      169,
      289130,
      263704,
      330950,
      344828,
      379071,
      11744,
      92715,
      385,
      387,
      527,
      240,
      129,
      13760,
      6,
      71,
      120,
      88,
      189,
      834,
      0,
      5668,
      435,
      158,
      379,
      66,
      9589,
      2657,
      29192,
      9966,
      3811,
      4835,
      3852,
      568125,
      554158,
      2034244,
      4006832,
      107906,
      141544,
      147680,
      35466,
      14518,
      10448,
      24118,
      38936,
      30116,
      23552,
      22670,
      15792,
      17712,
      41986,
      37828,
      46784,
      58368,
      34966,
      34966,
      34966,
      179438,
      80388,
      180736,
      13696,
      168266,
      16710,
      148020,
      230270,
      20964,
      19278,
      120732,
      98076,
      25504,
      26336,
      12764,
      192512,
      12978
    };
    public static string[] RESOURCE_FILENAMES_LIST;
    public static readonly string[] RESOURCE_FILENAMES_LIST_FULL = new string[395]
    {
      "camera.m3g",
      "chopper.m3g",
      "effect_bullet.m3g",
      "effect_collectable.m3g",
      "faith.m3g",
      "faith_origin_offsets.m3g",
      "palette_chapter_01.m3g",
      "palette_chapter_01_02.m3g",
      "palette_chapter_02.m3g",
      "palette_chapter_02_02.m3g",
      "palette_chapter_03.m3g",
      "palette_chapter_03_02.m3g",
      "palette_chapter_04.m3g",
      "palette_chapter_04_02.m3g",
      "palette_chapter_05.m3g",
      "palette_chapter_05_02.m3g",
      "palette_chapter_06.m3g",
      "palette_chapter_06_02.m3g",
      "palette_tutorial.m3g",
      "palette_tutorial_adv.m3g",
      "pigeon.m3g",
      "plane_light.m3g",
      "Police.m3g",
      "Police_origin_offsets.m3g",
      "texture_boss_runner.m3g",
      "texture_bullet_effect.m3g",
      "texture_button_effect.m3g",
      "texture_chapterend_0_0.m3g",
      "texture_chapterend_0_1.m3g",
      "texture_chapterend_1_1.m3g",
      "texture_chapterend_1_2.m3g",
      "texture_chapterend_2_1.m3g",
      "texture_chapterend_2_2.m3g",
      "texture_chapterend_3_1.m3g",
      "texture_chapterend_3_2.m3g",
      "texture_chapterend_4_1.m3g",
      "texture_chapterend_4_2.m3g",
      "texture_chapterend_5_1.m3g",
      "texture_chapterend_5_2.m3g",
      "texture_chapterend_6_1.m3g",
      "texture_chapterend_6_2.m3g",
      "texture_checkpoint_effect.m3g",
      "texture_chopper.m3g",
      "texture_collect_effect.m3g",
      "texture_ea_logo.m3g",
      "texture_faith_dusk.m3g",
      "texture_faith_gen_interior.m3g",
      "texture_faith_ghost.m3g",
      "texture_faith_jail.m3g",
      "texture_faith_midday.m3g",
      "texture_faith_night.m3g",
      "texture_faith_underground.m3g",
      "texture_fx_particles.m3g",
      "texture_fx_shadow.m3g",
      "texture_fx_smoke_t1.m3g",
      "texture_fx_steamjet_t1.m3g",
      "texture_map_bg_backdrop_dusk.m3g",
      "texture_map_bg_backdrop_midday.m3g",
      "texture_map_bg_backdrop_night.m3g",
      "texture_map_buildings_dusk.m3g",
      "texture_map_buildings_midday.m3g",
      "texture_map_buildings_night.m3g",
      "texture_map_gen_interior_t1.m3g",
      "texture_map_gen_interior_t2.m3g",
      "texture_map_gen_interior_t3.m3g",
      "texture_map_graffiti_interio.m3g",
      "texture_map_jail_interior_t1.m3g",
      "texture_map_jail_interior_t2.m3g",
      "texture_map_jail_interior_t3.m3g",
      "texture_map_objects_dusk.m3g",
      "texture_map_objects_dusk_2.m3g",
      "texture_map_objects_midday.m3g",
      "texture_map_objects_midday_2.m3g",
      "texture_map_objects_night.m3g",
      "texture_map_objects_night_2.m3g",
      "texture_map_underground_interior_t1.m3g",
      "texture_map_underground_interior_t2.m3g",
      "texture_map_underground_interior_t3.m3g",
      "texture_me_blue_badge.m3g",
      "texture_me_red_badge.m3g",
      "texture_mirrorsedge_logo.m3g",
      "texture_mirrorsedge_logo_sml.m3g",
      "texture_pain_effect.m3g",
      "texture_pigeon.m3g",
      "texture_plane_light.m3g",
      "texture_police_riot.m3g",
      "texture_policelight.m3g",
      "texture_reflection_dusk.m3g",
      "texture_reflection_midday.m3g",
      "texture_reflection_night.m3g",
      "texture_Runner.m3g",
      "texture_sound_icon_off.m3g",
      "texture_sound_icon_on.m3g",
      "texture_sound_icon_slider.m3g",
      "texture_speedrun_star.m3g",
      "texture_ui_banner.m3g",
      "texture_ui_button_arrow_active.m3g",
      "texture_ui_button_arrow_disabled.m3g",
      "texture_ui_button_major.m3g",
      "texture_ui_button_mg.m3g",
      "texture_ui_button_minor.m3g",
      "texture_ui_city.m3g",
      "texture_ui_complete_bags.m3g",
      "texture_ui_loading_border.m3g",
      "texture_ui_loading_border_blue.m3g",
      "texture_ui_loading_border_red.m3g",
      "texture_ui_loading_text_blank.m3g",
      "texture_ui_ribbon_red_left.m3g",
      "texture_ui_ribbon_red_right.m3g",
      "texture_ui_ribbon_white_left.m3g",
      "texture_ui_ribbon_white_right.m3g",
      "texture_ui_ticker_bar.m3g",
      "texture_ui_window.m3g",
      "texture_unlockable_01.m3g",
      "texture_unlockable_02.m3g",
      "texture_unlockable_03.m3g",
      "texture_unlockable_04.m3g",
      "texture_unlockable_05.m3g",
      "texture_unlockable_06.m3g",
      "texture_unlockable_07.m3g",
      "texture_unlockable_08.m3g",
      "texture_unlockable_09.m3g",
      "texture_unlockable_10.m3g",
      "texture_unlockable_11.m3g",
      "texture_unlockable_12.m3g",
      "texture_unlockable_13.m3g",
      "texture_unlockable_14.m3g",
      "texture_unlockable_15.m3g",
      "texture_unlockable_16.m3g",
      "texture_unlockable_17.m3g",
      "texture_unlockable_18.m3g",
      "texture_unlockable_19.m3g",
      "texture_unlockable_20.m3g",
      "texture_unlockable_locked.m3g",
      "ui_camera.m3g",
      "ui_city.m3g",
      "ui_loading.m3g",
      "ui_loading_short.m3g",
      "achievement_data.bin",
      "anim3d.bin",
      "animation_blenders.bin",
      "animdata.bin",
      "chapter_01.bin",
      "chapter_01_02.bin",
      "chapter_02.bin",
      "chapter_02_02.bin",
      "chapter_03.bin",
      "chapter_03_02.bin",
      "chapter_04.bin",
      "chapter_04_02.bin",
      "chapter_05.bin",
      "chapter_05_02.bin",
      "chapter_06.bin",
      "chapter_06_02.bin",
      "color.bin",
      "game_objects.bin",
      "image.bin",
      "level_data.bin",
      "loading_screen_data.bin",
      "m3g_assets.bin",
      "materials.bin",
      "quads.bin",
      "runner_data.bin",
      "sound_sequencer.bin",
      "soundevents.bin",
      "soundpools.bin",
      "tutorial.bin",
      "tutorial_adv.bin",
      "unlockables.bin",
      "fonts.bin",
      "HelveticaNeueLTStd_MdExO.otf",
      "Connect_iphone",
      "loading_run",
      "Logout_iphone",
      "ui_banner_cropped",
      "ui_loading_icon_norunning",
      "ui_loading_icon_stars",
      "ui_loading_icon_tick",
      "ui_loading_text_blank",
      "ui_loading_text_blank_short",
      "unlockable_01",
      "unlockable_02",
      "unlockable_03",
      "unlockable_04",
      "unlockable_05",
      "unlockable_06",
      "unlockable_07",
      "unlockable_08",
      "unlockable_09",
      "unlockable_10",
      "unlockable_11",
      "unlockable_12",
      "unlockable_13",
      "unlockable_14",
      "unlockable_15",
      "unlockable_16",
      "unlockable_17",
      "unlockable_18",
      "unlockable_19",
      "unlockable_20",
      "Ambience_01",
      "Ambience_02",
      "Ambience_03",
      "Ambience_04",
      "Ambience_05",
      "Ambience_06",
      "Ambience_07",
      "Ambience_08",
      "Ambience_09",
      "Ambience_10",
      "BF_Casio_theme",
      "Chase_01",
      "Chase_02",
      "Chase_03",
      "Chase_04",
      "Chase_05",
      "Chase_06",
      "Chase_07",
      "Chase_08",
      "Combat_01",
      "Combat_02",
      "Combat_03",
      "Combat_04",
      "Combat_05",
      "Combat_06",
      "Combat_07",
      "Combat_08",
      "Combat_09",
      "Inverted_Stinger_01",
      "Puzzle_01",
      "Puzzle_02",
      "Puzzle_03",
      "Puzzle_04",
      "Puzzle_05",
      "Puzzle_06",
      "Puzzle_intro",
      "Reversed_stinger_01",
      "StillAlive",
      "StillAliveRemix",
      "sfx_AI_Scream_Short_03",
      "sfx_AI_Scream_Short_04",
      "sfx_AI_Scream_Short_05",
      "sfx_AI_Scream_Short_06",
      "sfx_bag_stinger",
      "sfx_Blades_Far",
      "sfx_Blades_Near",
      "sfx_Breath_Soft_Short_In_01",
      "sfx_Breath_Soft_Short_In_02",
      "sfx_Breath_Soft_Short_In_03",
      "sfx_Breath_Soft_Short_In_04",
      "sfx_Breath_Soft_Short_In_05",
      "sfx_Breath_Soft_Short_In_06",
      "sfx_Breath_Soft_Short_In_07",
      "sfx_Breath_Soft_Short_In_08",
      "sfx_Breath_Soft_Short_Out_01",
      "sfx_Breath_Soft_Short_Out_02",
      "sfx_Breath_Soft_Short_Out_03",
      "sfx_Breath_Soft_Short_Out_04",
      "sfx_Breath_Soft_Short_Out_05",
      "sfx_Breath_Soft_Short_Out_06",
      "sfx_Breath_Soft_Short_Out_07",
      "sfx_Breath_Soft_Short_Out_08",
      "sfx_checkpoint",
      "sfx_combat_start_1",
      "sfx_combat_start_2",
      "sfx_combat_start_3",
      "sfx_combat_start_4",
      "sfx_combat_success_1",
      "sfx_combat_success_2",
      "sfx_combat_success_3",
      "sfx_combat_success_4",
      "sfx_combat_success_5",
      "sfx_ConcreteFootStepRun_01",
      "sfx_ConcreteFootStepRun_02",
      "sfx_ConcreteFootStepRun_03",
      "sfx_ConcreteFootStepRun_04",
      "sfx_ConcreteFootStepRun_05",
      "sfx_ConcreteFootStepRun_06",
      "sfx_ConcreteFootStepRun_07",
      "sfx_ConcreteFootStepRun_08",
      "sfx_ConcreteFootStepRun_09",
      "sfx_ConcreteFootStepRun_10",
      "sfx_ConcreteFootStepRun_11",
      "sfx_ConcreteFootStepRun_12",
      "sfx_ConcreteFootStepRun_13",
      "sfx_ConcreteFootStepRun_14",
      "sfx_death_01",
      "sfx_Fist_Head_01",
      "sfx_Fist_Head_02",
      "sfx_Fist_Head_03",
      "sfx_Foot_Body_01",
      "sfx_Foot_Body_02",
      "sfx_GLOCK_01",
      "sfx_GLOCK_02",
      "sfx_GLOCK_03",
      "sfx_HeliStart_1",
      "sfx_Impact_01",
      "sfx_Impact_02",
      "sfx_Impact_03",
      "sfx_Impact_04",
      "sfx_Impact_05",
      "sfx_Impact_06",
      "sfx_Impact_07",
      "sfx_Impact_08",
      "sfx_Impact_09",
      "sfx_Impact_Med_01",
      "sfx_Impact_Med_02",
      "sfx_Impact_Med_03",
      "sfx_Impact_Med_04",
      "sfx_Impact_Med_05",
      "sfx_Impact_Med_06",
      "sfx_Impact_Med_07",
      "sfx_Impact_Med_08",
      "sfx_Impact_Med_09",
      "sfx_Impact_Med_10",
      "sfx_Impact_Med_11",
      "sfx_Impact_Med_12",
      "sfx_Impact_Med_13",
      "sfx_Impact_Med_14",
      "sfx_Impact_Soft_02",
      "sfx_Impact_Soft_04",
      "sfx_Impact_Soft_12",
      "sfx_Inverted_Stinger_01",
      "sfx_KickBack_01",
      "sfx_KickBack_02",
      "sfx_KickSide_01",
      "sfx_KickSide_02",
      "sfx_MetalCraneFootStepRun_01",
      "sfx_MetalCraneFootStepRun_02",
      "sfx_MetalCraneFootStepRun_03",
      "sfx_MetalCraneFootStepRun_04",
      "sfx_MetalCraneFootStepRun_05",
      "sfx_MetalCraneFootStepRun_06",
      "sfx_MetalCraneFootStepRun_07",
      "sfx_MetalCraneFootStepRun_08",
      "sfx_MetalCraneFootStepRun_09",
      "sfx_MetalCraneFootStepRun_10",
      "sfx_MetalDuctFootstepRun_01",
      "sfx_MetalDuctFootstepRun_02",
      "sfx_MetalDuctFootstepRun_03",
      "sfx_MetalDuctFootstepRun_04",
      "sfx_MetalDuctFootstepRun_05",
      "sfx_MetalDuctFootstepRun_06",
      "sfx_MetalDuctFootstepRun_07",
      "sfx_MetalDuctFootstepRun_08",
      "sfx_MetalDuctFootstepRun_09",
      "sfx_MetalDuctFootstepRun_11",
      "sfx_MetalDuctFootstepRun_12",
      "sfx_MetalGantryFootStepRun_01",
      "sfx_MetalGantryFootStepRun_02",
      "sfx_MetalGantryFootStepRun_03",
      "sfx_MetalGantryFootStepRun_04",
      "sfx_MetalGantryFootStepRun_05",
      "sfx_MetalGantryFootStepRun_06",
      "sfx_MetalGantryFootStepRun_07",
      "sfx_MetalGantryFootStepRun_08",
      "sfx_MiniMe_01",
      "sfx_MiniMe_02",
      "sfx_MiniMe_03",
      "sfx_Overhead",
      "sfx_pigeon_wings",
      "sfx_Puzzle_intro",
      "sfx_Reversed_stinger_01",
      "sfx_slide_in",
      "sfx_slide_loop",
      "sfx_slide_out",
      "sfx_steam",
      "sfx_Stinger_01",
      "sfx_Stinger_02",
      "sfx_Stinger_03",
      "sfx_Stinger_04",
      "sfx_Strain_Soft_1",
      "sfx_Strain_Soft_2",
      "sfx_Strain_Soft_3",
      "sfx_Strain_Soft_4",
      "sfx_Strain_Soft_5",
      "sfx_Strain_Soft_6",
      "sfx_Strain_Soft_7",
      "sfx_Strain_Soft_8",
      "sfx_ui_neg",
      "sfx_ui_pos",
      "sfx_wilhelm_scream",
      "sfx_WoodPlanksFootStepRun_01",
      "sfx_WoodPlanksFootStepRun_02",
      "sfx_WoodPlanksFootStepRun_03",
      "sfx_WoodPlanksFootStepRun_04",
      "sfx_WoodPlanksFootStepRun_05",
      "sfx_WoodPlanksFootStepRun_06",
      "sfx_WoodPlanksFootStepRun_07",
      "sfx_WoodPlanksFootStepRun_08",
      "sfx_WoodPlanksFootStepRun_09",
      "sfx_WoodPlanksFootStepRun_10",
      "sfx_zip_in",
      "sfx_zip_loop",
      "sfx_zip_out"
    };
    public static readonly string[] RESOURCE_FILENAMES_LIST_TRIAL = new string[129]
    {
      "trial/camera.m3g",
      "trial/chopper.m3g",
      "trial/effect_bullet.m3g",
      "trial/effect_collectable.m3g",
      "trial/faith.m3g",
      "trial/faith_origin_offsets.m3g",
      "trial/palette_chapter_01.m3g",
      "trial/palette_tutorial.m3g",
      "trial/pigeon.m3g",
      "trial/plane_light.m3g",
      "texture_bullet_effect.m3g",
      "texture_button_effect.m3g",
      "texture_chapterend_0_0.m3g",
      "texture_chapterend_1_1.m3g",
      "texture_checkpoint_effect.m3g",
      "texture_chopper.m3g",
      "texture_collect_effect.m3g",
      "texture_ea_logo.m3g",
      "texture_faith_ghost.m3g",
      "texture_faith_midday.m3g",
      "texture_fx_particles.m3g",
      "texture_fx_smoke_t1.m3g",
      "texture_fx_steamjet_t1.m3g",
      "trial/texture_intro_bg.m3g",
      "texture_map_bg_backdrop_midday.m3g",
      "texture_map_buildings_midday.m3g",
      "texture_map_objects_midday.m3g",
      "texture_map_objects_midday_2.m3g",
      "texture_mirrorsedge_logo.m3g",
      "texture_mirrorsedge_logo_sml.m3g",
      "texture_pain_effect.m3g",
      "texture_pigeon.m3g",
      "texture_plane_light.m3g",
      "texture_reflection_midday.m3g",
      "texture_sound_icon_off.m3g",
      "texture_sound_icon_on.m3g",
      "texture_sound_icon_slider.m3g",
      "texture_speedrun_star.m3g",
      "texture_ui_button_arrow_active.m3g",
      "texture_ui_button_arrow_disabled.m3g",
      "texture_ui_button_major.m3g",
      "texture_ui_button_mg.m3g",
      "texture_ui_button_minor.m3g",
      "texture_ui_city.m3g",
      "texture_ui_complete_bags.m3g",
      "texture_ui_loading_border.m3g",
      "texture_ui_loading_border_blue.m3g",
      "texture_ui_loading_border_red.m3g",
      "texture_ui_loading_text_blank.m3g",
      "texture_ui_ribbon_red_left.m3g",
      "texture_ui_ribbon_red_right.m3g",
      "texture_ui_ribbon_white_left.m3g",
      "texture_ui_ribbon_white_right.m3g",
      "texture_ui_ticker_bar.m3g",
      "texture_ui_window.m3g",
      "trial/texture_upsell_screen_1.m3g",
      "trial/texture_upsell_screen_2.m3g",
      "trial/texture_upsell_screen_3.m3g",
      "trial/texture_upsell_screen_4.m3g",
      "trial/texture_upsell_screen_5.m3g",
      "trial/ui_camera.m3g",
      "trial/ui_city.m3g",
      "trial/ui_loading.m3g",
      "trial/ui_loading_short.m3g",
      "trial/anim3d.bin",
      "trial/animation_blenders.bin",
      "animdata.bin",
      "trial/chapter_01.bin",
      "color.bin",
      "game_objects.bin",
      "trial/image.bin",
      "trial/level_data.bin",
      "trial/loading_screen_data.bin",
      "trial/m3g_assets.bin",
      "materials.bin",
      "trial/quads.bin",
      "trial/runner_data.bin",
      "trial/sound_sequencer.bin",
      "trial/soundevents.bin",
      "trial/soundpools.bin",
      "trial/tutorial.bin",
      "trial/fonts.bin",
      "HelveticaNeueLTStd_MdExO.otf",
      "loading_run",
      "trial/ui_loading_icon_stars",
      "trial/ui_loading_text_blank",
      "trial/ui_loading_text_blank_short",
      "Ambience_03",
      "Chase_02",
      "StillAlive",
      "StillAliveRemix",
      "sfx_bag_stinger",
      "sfx_Blades_Far",
      "sfx_Blades_Near",
      "sfx_checkpoint",
      "sfx_ConcreteFootStepRun_01",
      "sfx_ConcreteFootStepRun_02",
      "sfx_death_01",
      "sfx_Impact_01",
      "sfx_Impact_02",
      "sfx_Impact_Med_01",
      "sfx_Impact_Med_02",
      "sfx_MetalCraneFootStepRun_01",
      "sfx_MetalCraneFootStepRun_02",
      "sfx_MetalDuctFootstepRun_01",
      "sfx_MetalDuctFootstepRun_02",
      "sfx_MetalGantryFootStepRun_01",
      "sfx_MetalGantryFootStepRun_02",
      "sfx_MiniMe_01",
      "sfx_MiniMe_02",
      "sfx_MiniMe_03",
      "sfx_Overhead",
      "sfx_pigeon_wings",
      "sfx_Puzzle_intro",
      "sfx_slide_in",
      "sfx_slide_loop",
      "sfx_slide_out",
      "sfx_Stinger_01",
      "sfx_Stinger_02",
      "sfx_Strain_Soft_1",
      "sfx_Strain_Soft_2",
      "sfx_ui_neg",
      "sfx_ui_pos",
      "sfx_WoodPlanksFootStepRun_01",
      "sfx_WoodPlanksFootStepRun_02",
      "sfx_zip_in",
      "sfx_zip_loop",
      "sfx_zip_out",
      "achievement_data.bin"
    };
    public static int[] SOUND_EVENT_LOOKUP;
    public static readonly int[] SOUND_EVENT_LOOKUP_FULL = new int[111]
    {
      93,
      94,
      95,
      96,
      97,
      98,
      99,
      100,
      101,
      102,
      103,
      104,
      105,
      106,
      107,
      108,
      109,
      110,
      111,
      112,
      113,
      114,
      115,
      116,
      117,
      118,
      119,
      120,
      121,
      122,
      123,
      124,
      125,
      126,
      (int) sbyte.MaxValue,
      128,
      129,
      130,
      131,
      132,
      133,
      134,
      135,
      136,
      137,
      138,
      139,
      140,
      141,
      142,
      143,
      144,
      175,
      176,
      177,
      161,
      162,
      163,
      164,
      165,
      166,
      167,
      168,
      169,
      170,
      171,
      172,
      173,
      174,
      178,
      179,
      180,
      181,
      182,
      183,
      184,
      185,
      145,
      146,
      147,
      153,
      154,
      155,
      151,
      152,
      159,
      160,
      46,
      47,
      48,
      51,
      52,
      53,
      37,
      38,
      39,
      40,
      41,
      42,
      43,
      44,
      45,
      55,
      56,
      57,
      58,
      59,
      60,
      61,
      62,
      63
    };
    public static readonly int[] SOUND_EVENT_LOOKUP_TRIAL = new int[19]
    {
      26,
      27,
      28,
      29,
      30,
      31,
      32,
      33,
      34,
      35,
      36,
      37,
      38,
      39,
      10,
      11,
      12,
      6,
      7
    };
    public static int[] SOUND_DATA_SETS;
    public static readonly int[] SOUND_DATA_SETS_FULL = new int[187]
    {
      237,
      238,
      200,
      201,
      202,
      203,
      204,
      205,
      206,
      207,
      208,
      209,
      211,
      212,
      213,
      214,
      215,
      216,
      217,
      218,
      219,
      220,
      221,
      222,
      223,
      224,
      225,
      226,
      227,
      229,
      230,
      231,
      232,
      233,
      234,
      380,
      379,
      296,
      297,
      298,
      299,
      300,
      301,
      302,
      303,
      304,
      292,
      293,
      294,
      244,
      245,
      356,
      357,
      358,
      359,
      287,
      288,
      289,
      290,
      291,
      323,
      324,
      325,
      326,
      361,
      367,
      368,
      369,
      370,
      243,
      262,
      363,
      364,
      365,
      392,
      393,
      394,
      366,
      360,
      381,
      263,
      264,
      265,
      266,
      267,
      268,
      269,
      270,
      271,
      239,
      240,
      241,
      242,
      272,
      273,
      274,
      275,
      276,
      277,
      278,
      279,
      280,
      281,
      282,
      283,
      284,
      327,
      328,
      329,
      330,
      331,
      332,
      333,
      334,
      335,
      336,
      337,
      338,
      339,
      340,
      341,
      342,
      343,
      344,
      345,
      346,
      347,
      348,
      349,
      350,
      351,
      352,
      353,
      354,
      355,
      382,
      383,
      384,
      385,
      386,
      387,
      388,
      389,
      390,
      391,
      246,
      247,
      248,
      249,
      250,
      251,
      252,
      253,
      254,
      (int) byte.MaxValue,
      256,
      257,
      258,
      259,
      260,
      261,
      305,
      306,
      307,
      308,
      309,
      310,
      311,
      312,
      313,
      314,
      315,
      316,
      317,
      318,
      319,
      320,
      321,
      371,
      372,
      373,
      374,
      375,
      376,
      377,
      378,
      286
    };
    public static readonly int[] SOUND_DATA_SETS_TRIAL = new int[41]
    {
      89,
      90,
      87,
      88,
      122,
      121,
      98,
      99,
      92,
      93,
      108,
      109,
      110,
      111,
      113,
      117,
      118,
      91,
      94,
      114,
      115,
      116,
      125,
      126,
      (int) sbyte.MaxValue,
      112,
      95,
      96,
      102,
      103,
      104,
      105,
      106,
      107,
      123,
      124,
      100,
      101,
      119,
      120,
      97
    };
    public static readonly int[] UNLOCKABLE_QUAD_DATA = new int[20]
    {
      28,
      27,
      42,
      29,
      30,
      39,
      31,
      43,
      44,
      41,
      32,
      33,
      34,
      35,
      36,
      37,
      38,
      45,
      26,
      40
    };
    public static readonly int[] UNLOCKABLE_SAVEABLE_DATA = new int[20]
    {
      182,
      181,
      196,
      183,
      184,
      193,
      185,
      197,
      198,
      195,
      186,
      187,
      188,
      189,
      190,
      191,
      192,
      199,
      180,
      194
    };
    public static readonly int[] UNLOCKABLE_STRING_DATA = new int[20]
    {
      2304,
      2304,
      2304,
      2304,
      2304,
      2304,
      2304,
      2304,
      2304,
      2304,
      2304,
      2304,
      2304,
      2304,
      2304,
      2304,
      2304,
      2304,
      2304,
      2304
    };
    public static short[] IMAGE_RES_IDS;
    public static readonly short[] IMAGE_RES_IDS_FULL = new short[1]
    {
      (short) 172
    };
    public static readonly short[] IMAGE_RES_IDS_TRIAL = new short[1]
    {
      (short) 83
    };
    public static int ANIM_DATA_FILE;
    public static int IMAGE_DATA_FILE;
    public static readonly int[] MATERIAL_LOOKUP = new int[6]
    {
      0,
      1,
      2,
      4,
      3,
      3
    };
    public static int[] LOADING_IMAGE_LOOKUP;
    public static int[] LOADING_STRING_LOOKUP;
    public static int[] LOADING_FONT_LOOKUP;
    private static readonly int[] LOADING_IMAGE_LOOKUP_FULL = new int[60]
    {
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176,
      176
    };
    private static readonly int[] LOADING_STRING_LOOKUP_FULL = new int[60]
    {
      2267,
      2048,
      2268,
      2269,
      2270,
      2271,
      2272,
      2273,
      2274,
      2275,
      2271,
      2272,
      2276,
      2267,
      2048,
      2268,
      2277,
      2278,
      2274,
      2279,
      2267,
      2048,
      2268,
      2280,
      2281,
      2282,
      2283,
      2271,
      2272,
      2284,
      2267,
      2048,
      2268,
      2285,
      2286,
      2274,
      2287,
      2267,
      2048,
      2268,
      2288,
      2289,
      2290,
      2291,
      2271,
      2272,
      2292,
      2267,
      2048,
      2268,
      2293,
      2294,
      2295,
      2296,
      2271,
      2272,
      2297,
      2298,
      2299,
      2300
    };
    private static readonly int[] LOADING_FONT_LOOKUP_FULL = new int[60]
    {
      20,
      20,
      21,
      20,
      21,
      22,
      23,
      23,
      24,
      25,
      22,
      23,
      23,
      20,
      20,
      21,
      20,
      21,
      24,
      25,
      20,
      20,
      21,
      20,
      21,
      20,
      21,
      22,
      23,
      23,
      20,
      20,
      21,
      20,
      21,
      24,
      25,
      20,
      20,
      21,
      20,
      21,
      20,
      21,
      22,
      23,
      23,
      20,
      20,
      21,
      20,
      21,
      20,
      21,
      22,
      23,
      23,
      20,
      21,
      21
    };
    private static readonly int[] LOADING_IMAGE_LOOKUP_TRIAL = new int[7]
    {
      84,
      84,
      84,
      84,
      84,
      84,
      84
    };
    private static readonly int[] LOADING_STRING_LOOKUP_TRIAL = new int[7]
    {
      2267,
      2048,
      2268,
      2269,
      2270,
      2274,
      2275
    };
    private static readonly int[] LOADING_FONT_LOOKUP_TRIAL = new int[7]
    {
      20,
      20,
      21,
      20,
      21,
      24,
      25
    };
    public static int[] ANIMATION_CONTROLLER_LOOKUP;
    public static readonly int[] ANIMATION_CONTROLLER_LOOKUP_FULL = new int[106]
    {
      524288,
      524289,
      524290,
      524291,
      524292,
      524293,
      524294,
      524295,
      524296,
      524297,
      524298,
      524299,
      524300,
      524301,
      524302,
      524303,
      524304,
      524305,
      524306,
      524307,
      524308,
      524309,
      524310,
      524311,
      524312,
      524313,
      524314,
      524315,
      524316,
      524317,
      524318,
      524319,
      524320,
      524321,
      524322,
      524323,
      524324,
      524325,
      524326,
      524327,
      524328,
      524329,
      524330,
      524331,
      524332,
      524333,
      524334,
      524335,
      524336,
      524337,
      524338,
      524339,
      524340,
      524341,
      524342,
      524343,
      524344,
      524345,
      524346,
      524347,
      524348,
      524349,
      524350,
      524351,
      524352,
      524353,
      524354,
      524355,
      524356,
      524357,
      524358,
      524359,
      589824,
      589825,
      589826,
      589827,
      589828,
      589829,
      589830,
      589831,
      589832,
      589833,
      589834,
      589835,
      589836,
      589837,
      589838,
      589839,
      589840,
      589841,
      589842,
      589843,
      589844,
      589845,
      589846,
      589847,
      589848,
      589849,
      589850,
      589851,
      589852,
      589853,
      589854,
      589855,
      589856,
      589857
    };
    public static readonly int[] ANIMATION_CONTROLLER_LOOKUP_TRIAL = new int[59]
    {
      524288,
      524289,
      524290,
      524291,
      524292,
      524293,
      524294,
      524295,
      524296,
      524297,
      524298,
      524299,
      524300,
      524301,
      524302,
      524303,
      524304,
      524305,
      524306,
      524307,
      524308,
      524309,
      524310,
      524311,
      524312,
      524313,
      524314,
      524315,
      524316,
      524317,
      524318,
      524319,
      524320,
      524321,
      524322,
      524323,
      524324,
      524325,
      524326,
      524327,
      524328,
      524329,
      524330,
      524331,
      524332,
      524333,
      524334,
      524335,
      524336,
      524337,
      524338,
      524339,
      524340,
      524341,
      524342,
      524343,
      524344,
      524345,
      524346
    };
    public static readonly int[] SOUND_TRACK_SOUNDS = new int[0];
    public static int[] SOUND_TRACK_POOL_LOOKUPS;
    public static readonly int[] SOUND_TRACK_POOL_LOOKUPS_FULL = new int[20]
    {
      1,
      2,
      1,
      2,
      1,
      2,
      1,
      2,
      1,
      2,
      1,
      2,
      1,
      1,
      2,
      2,
      2,
      3,
      5,
      1
    };
    public static readonly int[] SOUND_TRACK_POOL_LOOKUPS_TRIAL = new int[14]
    {
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      0
    };
    public static int COLOR_DATA_FILE;

    public static void SetResources()
    {
      if (MirrorsEdge.TrialMode)
      {
        ResourceManager.LOADING_IMAGE_LOOKUP = ResourceManager.LOADING_IMAGE_LOOKUP_TRIAL;
        ResourceManager.LOADING_STRING_LOOKUP = ResourceManager.LOADING_STRING_LOOKUP_TRIAL;
        ResourceManager.LOADING_FONT_LOOKUP = ResourceManager.LOADING_FONT_LOOKUP_TRIAL;
        ResourceManager.ANIMATION_CONTROLLER_LOOKUP = ResourceManager.ANIMATION_CONTROLLER_LOOKUP_TRIAL;
        ResourceManager.RESOURCE_FILESIZE_LIST = ResourceManager.RESOURCE_FILESIZE_LIST_TRIAL;
        ResourceManager.RESOURCE_FILENAMES_LIST = ResourceManager.RESOURCE_FILENAMES_LIST_TRIAL;
        ResourceManager.SOUND_DATA_SETS = ResourceManager.SOUND_DATA_SETS_TRIAL;
        ResourceManager.SOUND_EVENT_LOOKUP = ResourceManager.SOUND_EVENT_LOOKUP_TRIAL;
        ResourceManager.SOUND_TRACK_POOL_LOOKUPS = ResourceManager.SOUND_TRACK_POOL_LOOKUPS_TRIAL;
        ResourceManager.IMAGE_RES_IDS = ResourceManager.IMAGE_RES_IDS_TRIAL;
        ResourceManager.ANIM_DATA_FILE = 66;
        ResourceManager.IMAGE_DATA_FILE = 70;
        ResourceManager.COLOR_DATA_FILE = 68;
        ResourceManager.Idi = typeof (ResourceManager.Idi_Trial);
        ResourceManager.Anim3D = typeof (ResourceManager.Anim3D_Trial);
        ResourceManager.ChannelFaith = typeof (ResourceManager.ChannelFaith_Trial);
        ResourceManager.SoundEvent = typeof (ResourceManager.SoundEvent_Trial);
        ResourceManager.SoundEventPoolset = typeof (ResourceManager.SoundEventPoolset_Trial);
        ResourceManager.SoundEventPoolVocal = typeof (ResourceManager.SoundEventPoolVocal_Trial);
        ResourceManager.SoundEventPoolGunshots = typeof (ResourceManager.SoundEventPoolGunshots_Trial);
      }
      else
      {
        ResourceManager.LOADING_IMAGE_LOOKUP = ResourceManager.LOADING_IMAGE_LOOKUP_FULL;
        ResourceManager.LOADING_STRING_LOOKUP = ResourceManager.LOADING_STRING_LOOKUP_FULL;
        ResourceManager.LOADING_FONT_LOOKUP = ResourceManager.LOADING_FONT_LOOKUP_FULL;
        ResourceManager.ANIMATION_CONTROLLER_LOOKUP = ResourceManager.ANIMATION_CONTROLLER_LOOKUP_FULL;
        ResourceManager.RESOURCE_FILESIZE_LIST = ResourceManager.RESOURCE_FILESIZE_LIST_FULL;
        ResourceManager.RESOURCE_FILENAMES_LIST = ResourceManager.RESOURCE_FILENAMES_LIST_FULL;
        ResourceManager.SOUND_DATA_SETS = ResourceManager.SOUND_DATA_SETS_FULL;
        ResourceManager.SOUND_EVENT_LOOKUP = ResourceManager.SOUND_EVENT_LOOKUP_FULL;
        ResourceManager.SOUND_TRACK_POOL_LOOKUPS = ResourceManager.SOUND_TRACK_POOL_LOOKUPS_FULL;
        ResourceManager.IMAGE_RES_IDS = ResourceManager.IMAGE_RES_IDS_FULL;
        ResourceManager.ANIM_DATA_FILE = 141;
        ResourceManager.IMAGE_DATA_FILE = 156;
        ResourceManager.COLOR_DATA_FILE = 154;
        ResourceManager.Idi = typeof (ResourceManager.Idi_Full);
        ResourceManager.Anim3D = typeof (ResourceManager.Anim3D_Full);
        ResourceManager.ChannelFaith = typeof (ResourceManager.ChannelFaith_Full);
        ResourceManager.ChannelPolice = typeof (ResourceManager.ChannelPolice_Full);
        ResourceManager.SoundEvent = typeof (ResourceManager.SoundEvent_Full);
        ResourceManager.SoundEventPoolset = typeof (ResourceManager.SoundEventPoolset_Full);
        ResourceManager.SoundEventPoolVocal = typeof (ResourceManager.SoundEventPoolVocal_Full);
        ResourceManager.SoundEventPoolGunshots = typeof (ResourceManager.SoundEventPoolGunshots_Full);
      }
      M3GAssets.SetResources();
      QuadManager.SetResources();
      LevelData.SetResources();
      MEdgeMap.SetResources();
      GameObjectRunner.SetResources();
    }

    public static short get(string name)
    {
      short num = -1;
      if (ResourceManager.Enums.TryGetValue(name, out num))
        return num;
      if (name.StartsWith("IDI_"))
        num = (short) (int) Enum.Parse(ResourceManager.Idi, name, false);
      else if (name.StartsWith("ANIM3D_"))
        num = (short) (int) Enum.Parse(ResourceManager.Anim3D, name, false);
      else if (name.StartsWith("CHANNEL_FAITH_"))
        num = (short) (int) Enum.Parse(ResourceManager.ChannelFaith, name, false);
      else if (name.StartsWith("CHANNEL_POLICE_"))
        num = (short) (int) Enum.Parse(ResourceManager.ChannelPolice, name, false);
      else if (name.StartsWith("SOUNDEVENT_"))
        num = (short) (int) Enum.Parse(ResourceManager.SoundEvent, name, false);
      else if (name.StartsWith("SOUNDEVENTPOOLSET_"))
        num = (short) (int) Enum.Parse(ResourceManager.SoundEventPoolset, name, false);
      else if (name.StartsWith("SOUNDEVENTPOOL_VOCAL_"))
        num = (short) (int) Enum.Parse(ResourceManager.SoundEventPoolVocal, name, false);
      else if (name.StartsWith("SOUNDEVENTPOOL_GUNSHOTS_"))
        num = (short) (int) Enum.Parse(ResourceManager.SoundEventPoolGunshots, name, false);
      ResourceManager.Enums.Add(name, num);
      return num;
    }

    public static string ID_TO_FILENAME(int _id) => ResourceManager.RESOURCE_FILENAMES_LIST[_id];

    public static int GET_FILESIZE_BY_RESID(int _id) => ResourceManager.RESOURCE_FILESIZE_LIST[_id];

    public Image loadImage(int resourceID)
    {
      return Image.createImage("res/" + ResourceManager.ID_TO_FILENAME(resourceID));
    }

    public InputStream loadBinaryFile(int resourceID)
    {
      return meClass.getResourceAsStream("res/" + ResourceManager.ID_TO_FILENAME(resourceID));
    }

    public List<Object3D> loadM3GFile(int resourceID)
    {
      Loader.currentObjectName = "res/" + ResourceManager.ID_TO_FILENAME(resourceID);
      return Loader.load(this.loadBinaryFile(resourceID));
    }

    public microedition.m3g.Node loadM3GNode(int resourceID)
    {
      List<Object3D> object3DList = this.loadM3GFile(resourceID);
      microedition.m3g.Node node = (microedition.m3g.Node) null;
      if (object3DList != null)
        node = (microedition.m3g.Node) object3DList[0];
      return node;
    }

    public Image2D loadM3GImage2D(int resourceID)
    {
      Image2D image2D = (Image2D) null;
      if (resourceID == (int) ResourceManager.get("IDI_UI_LOADING_M3G") || resourceID == (int) ResourceManager.get("IDI_UI_LOADING_SHORT_M3G") || resourceID == (int) ResourceManager.get("IDI_TEXTURE_UI_LOADING_BORDER_M3G") || resourceID == (int) ResourceManager.get("IDI_TEXTURE_UI_LOADING_BORDER_RED_M3G") || resourceID == (int) ResourceManager.get("IDI_TEXTURE_UI_LOADING_BORDER_BLUE_M3G"))
        Loader.Loader_no_mipmap = true;
      List<Object3D> object3DList = this.loadM3GFile(resourceID);
      if (Image2D.m3g_cast(object3DList[0]) != null)
        image2D = (Image2D) object3DList[0];
      for (int index = object3DList.Count - 1; index != -1; --index)
        object3DList[index] = (Object3D) null;
      return image2D;
    }

    public enum Idi_Full
    {
      IDI_CAMERA_M3G,
      IDI_CHOPPER_M3G,
      IDI_EFFECT_BULLET_M3G,
      IDI_EFFECT_COLLECTABLE_M3G,
      IDI_FAITH_M3G,
      IDI_FAITH_ORIGIN_OFFSETS_M3G,
      IDI_PALETTE_CHAPTER_01_M3G,
      IDI_PALETTE_CHAPTER_01_02_M3G,
      IDI_PALETTE_CHAPTER_02_M3G,
      IDI_PALETTE_CHAPTER_02_02_M3G,
      IDI_PALETTE_CHAPTER_03_M3G,
      IDI_PALETTE_CHAPTER_03_02_M3G,
      IDI_PALETTE_CHAPTER_04_M3G,
      IDI_PALETTE_CHAPTER_04_02_M3G,
      IDI_PALETTE_CHAPTER_05_M3G,
      IDI_PALETTE_CHAPTER_05_02_M3G,
      IDI_PALETTE_CHAPTER_06_M3G,
      IDI_PALETTE_CHAPTER_06_02_M3G,
      IDI_PALETTE_TUTORIAL_M3G,
      IDI_PALETTE_TUTORIAL_ADV_M3G,
      IDI_PIGEON_M3G,
      IDI_PLANE_LIGHT_M3G,
      IDI_POLICE_M3G,
      IDI_POLICE_ORIGIN_OFFSETS_M3G,
      IDI_TEXTURE_BOSS_RUNNER_M3G,
      IDI_TEXTURE_BULLET_EFFECT_M3G,
      IDI_TEXTURE_BUTTON_EFFECT_M3G,
      IDI_TEXTURE_CHAPTEREND_0_0_M3G,
      IDI_TEXTURE_CHAPTEREND_0_1_M3G,
      IDI_TEXTURE_CHAPTEREND_1_1_M3G,
      IDI_TEXTURE_CHAPTEREND_1_2_M3G,
      IDI_TEXTURE_CHAPTEREND_2_1_M3G,
      IDI_TEXTURE_CHAPTEREND_2_2_M3G,
      IDI_TEXTURE_CHAPTEREND_3_1_M3G,
      IDI_TEXTURE_CHAPTEREND_3_2_M3G,
      IDI_TEXTURE_CHAPTEREND_4_1_M3G,
      IDI_TEXTURE_CHAPTEREND_4_2_M3G,
      IDI_TEXTURE_CHAPTEREND_5_1_M3G,
      IDI_TEXTURE_CHAPTEREND_5_2_M3G,
      IDI_TEXTURE_CHAPTEREND_6_1_M3G,
      IDI_TEXTURE_CHAPTEREND_6_2_M3G,
      IDI_TEXTURE_CHECKPOINT_EFFECT_M3G,
      IDI_TEXTURE_CHOPPER_M3G,
      IDI_TEXTURE_COLLECT_EFFECT_M3G,
      IDI_TEXTURE_EA_LOGO_M3G,
      IDI_TEXTURE_FAITH_DUSK_M3G,
      IDI_TEXTURE_FAITH_GEN_INTERIOR_M3G,
      IDI_TEXTURE_FAITH_GHOST_M3G,
      IDI_TEXTURE_FAITH_JAIL_M3G,
      IDI_TEXTURE_FAITH_MIDDAY_M3G,
      IDI_TEXTURE_FAITH_NIGHT_M3G,
      IDI_TEXTURE_FAITH_UNDERGROUND_M3G,
      IDI_TEXTURE_FX_PARTICLES_M3G,
      IDI_TEXTURE_FX_SHADOW_M3G,
      IDI_TEXTURE_FX_SMOKE_T1_M3G,
      IDI_TEXTURE_FX_STEAMJET_T1_M3G,
      IDI_TEXTURE_MAP_BG_BACKDROP_DUSK_M3G,
      IDI_TEXTURE_MAP_BG_BACKDROP_MIDDAY_M3G,
      IDI_TEXTURE_MAP_BG_BACKDROP_NIGHT_M3G,
      IDI_TEXTURE_MAP_BUILDINGS_DUSK_M3G,
      IDI_TEXTURE_MAP_BUILDINGS_MIDDAY_M3G,
      IDI_TEXTURE_MAP_BUILDINGS_NIGHT_M3G,
      IDI_TEXTURE_MAP_GEN_INTERIOR_T1_M3G,
      IDI_TEXTURE_MAP_GEN_INTERIOR_T2_M3G,
      IDI_TEXTURE_MAP_GEN_INTERIOR_T3_M3G,
      IDI_TEXTURE_MAP_GRAFFITI_INTERIO_M3G,
      IDI_TEXTURE_MAP_JAIL_INTERIOR_T1_M3G,
      IDI_TEXTURE_MAP_JAIL_INTERIOR_T2_M3G,
      IDI_TEXTURE_MAP_JAIL_INTERIOR_T3_M3G,
      IDI_TEXTURE_MAP_OBJECTS_DUSK_M3G,
      IDI_TEXTURE_MAP_OBJECTS_DUSK_2_M3G,
      IDI_TEXTURE_MAP_OBJECTS_MIDDAY_M3G,
      IDI_TEXTURE_MAP_OBJECTS_MIDDAY_2_M3G,
      IDI_TEXTURE_MAP_OBJECTS_NIGHT_M3G,
      IDI_TEXTURE_MAP_OBJECTS_NIGHT_2_M3G,
      IDI_TEXTURE_MAP_UNDERGROUND_INTERIOR_T1_M3G,
      IDI_TEXTURE_MAP_UNDERGROUND_INTERIOR_T2_M3G,
      IDI_TEXTURE_MAP_UNDERGROUND_INTERIOR_T3_M3G,
      IDI_TEXTURE_ME_BLUE_BADGE_M3G,
      IDI_TEXTURE_ME_RED_BADGE_M3G,
      IDI_TEXTURE_MIRRORSEDGE_LOGO_M3G,
      IDI_TEXTURE_MIRRORSEDGE_LOGO_SML_M3G,
      IDI_TEXTURE_PAIN_EFFECT_M3G,
      IDI_TEXTURE_PIGEON_M3G,
      IDI_TEXTURE_PLANE_LIGHT_M3G,
      IDI_TEXTURE_POLICE_RIOT_M3G,
      IDI_TEXTURE_POLICELIGHT_M3G,
      IDI_TEXTURE_REFLECTION_DUSK_M3G,
      IDI_TEXTURE_REFLECTION_MIDDAY_M3G,
      IDI_TEXTURE_REFLECTION_NIGHT_M3G,
      IDI_TEXTURE_RUNNER_M3G,
      IDI_TEXTURE_SOUND_ICON_OFF_M3G,
      IDI_TEXTURE_SOUND_ICON_ON_M3G,
      IDI_TEXTURE_SOUND_ICON_SLIDER_M3G,
      IDI_TEXTURE_SPEEDRUN_STAR_M3G,
      IDI_TEXTURE_UI_BANNER_M3G,
      IDI_TEXTURE_UI_BUTTON_ARROW_ACTIVE_M3G,
      IDI_TEXTURE_UI_BUTTON_ARROW_DISABLED_M3G,
      IDI_TEXTURE_UI_BUTTON_MAJOR_M3G,
      IDI_TEXTURE_UI_BUTTON_MG_M3G,
      IDI_TEXTURE_UI_BUTTON_MINOR_M3G,
      IDI_TEXTURE_UI_CITY_M3G,
      IDI_TEXTURE_UI_COMPLETE_BAGS_M3G,
      IDI_TEXTURE_UI_LOADING_BORDER_M3G,
      IDI_TEXTURE_UI_LOADING_BORDER_BLUE_M3G,
      IDI_TEXTURE_UI_LOADING_BORDER_RED_M3G,
      IDI_TEXTURE_UI_LOADING_TEXT_BLANK_M3G,
      IDI_TEXTURE_UI_RIBBON_RED_LEFT_M3G,
      IDI_TEXTURE_UI_RIBBON_RED_RIGHT_M3G,
      IDI_TEXTURE_UI_RIBBON_WHITE_LEFT_M3G,
      IDI_TEXTURE_UI_RIBBON_WHITE_RIGHT_M3G,
      IDI_TEXTURE_UI_TICKER_BAR_M3G,
      IDI_TEXTURE_UI_WINDOW_M3G,
      IDI_TEXTURE_UNLOCKABLE_01_M3G,
      IDI_TEXTURE_UNLOCKABLE_02_M3G,
      IDI_TEXTURE_UNLOCKABLE_03_M3G,
      IDI_TEXTURE_UNLOCKABLE_04_M3G,
      IDI_TEXTURE_UNLOCKABLE_05_M3G,
      IDI_TEXTURE_UNLOCKABLE_06_M3G,
      IDI_TEXTURE_UNLOCKABLE_07_M3G,
      IDI_TEXTURE_UNLOCKABLE_08_M3G,
      IDI_TEXTURE_UNLOCKABLE_09_M3G,
      IDI_TEXTURE_UNLOCKABLE_10_M3G,
      IDI_TEXTURE_UNLOCKABLE_11_M3G,
      IDI_TEXTURE_UNLOCKABLE_12_M3G,
      IDI_TEXTURE_UNLOCKABLE_13_M3G,
      IDI_TEXTURE_UNLOCKABLE_14_M3G,
      IDI_TEXTURE_UNLOCKABLE_15_M3G,
      IDI_TEXTURE_UNLOCKABLE_16_M3G,
      IDI_TEXTURE_UNLOCKABLE_17_M3G,
      IDI_TEXTURE_UNLOCKABLE_18_M3G,
      IDI_TEXTURE_UNLOCKABLE_19_M3G,
      IDI_TEXTURE_UNLOCKABLE_20_M3G,
      IDI_TEXTURE_UNLOCKABLE_LOCKED_M3G,
      IDI_UI_CAMERA_M3G,
      IDI_UI_CITY_M3G,
      IDI_UI_LOADING_M3G,
      IDI_UI_LOADING_SHORT_M3G,
      IDI_ACHIEVEMENT_DATA_BIN,
      IDI_ANIM3D_BIN,
      IDI_ANIMATION_BLENDERS_BIN,
      IDI_ANIMDATA_BIN,
      IDI_CHAPTER_01_BIN,
      IDI_CHAPTER_01_02_BIN,
      IDI_CHAPTER_02_BIN,
      IDI_CHAPTER_02_02_BIN,
      IDI_CHAPTER_03_BIN,
      IDI_CHAPTER_03_02_BIN,
      IDI_CHAPTER_04_BIN,
      IDI_CHAPTER_04_02_BIN,
      IDI_CHAPTER_05_BIN,
      IDI_CHAPTER_05_02_BIN,
      IDI_CHAPTER_06_BIN,
      IDI_CHAPTER_06_02_BIN,
      IDI_COLOR_BIN,
      IDI_GAME_OBJECTS_BIN,
      IDI_IMAGE_BIN,
      IDI_LEVEL_DATA_BIN,
      IDI_LOADING_SCREEN_DATA_BIN,
      IDI_M3G_ASSETS_BIN,
      IDI_MATERIALS_BIN,
      IDI_QUADS_BIN,
      IDI_RUNNER_DATA_BIN,
      IDI_SOUND_SEQUENCER_BIN,
      IDI_SOUNDEVENTS_BIN,
      IDI_SOUNDPOOLS_BIN,
      IDI_TUTORIAL_BIN,
      IDI_TUTORIAL_ADV_BIN,
      IDI_UNLOCKABLES_BIN,
      IDI_FONTS_BIN,
      IDI_HELVETICANEUELTSTD_MDEXO_OTF,
      IDI_CONNECT_IPHONE_PNG,
      IDI_LOADING_RUN_PNG,
      IDI_LOGOUT_IPHONE_PNG,
      IDI_UI_BANNER_CROPPED_PNG,
      IDI_UI_LOADING_ICON_NORUNNING_PNG,
      IDI_UI_LOADING_ICON_STARS_PNG,
      IDI_UI_LOADING_ICON_TICK_PNG,
      IDI_UI_LOADING_TEXT_BLANK_PNG,
      IDI_UI_LOADING_TEXT_BLANK_SHORT_PNG,
      IDI_UNLOCKABLE_01_PNG,
      IDI_UNLOCKABLE_02_PNG,
      IDI_UNLOCKABLE_03_PNG,
      IDI_UNLOCKABLE_04_PNG,
      IDI_UNLOCKABLE_05_PNG,
      IDI_UNLOCKABLE_06_PNG,
      IDI_UNLOCKABLE_07_PNG,
      IDI_UNLOCKABLE_08_PNG,
      IDI_UNLOCKABLE_09_PNG,
      IDI_UNLOCKABLE_10_PNG,
      IDI_UNLOCKABLE_11_PNG,
      IDI_UNLOCKABLE_12_PNG,
      IDI_UNLOCKABLE_13_PNG,
      IDI_UNLOCKABLE_14_PNG,
      IDI_UNLOCKABLE_15_PNG,
      IDI_UNLOCKABLE_16_PNG,
      IDI_UNLOCKABLE_17_PNG,
      IDI_UNLOCKABLE_18_PNG,
      IDI_UNLOCKABLE_19_PNG,
      IDI_UNLOCKABLE_20_PNG,
      IDI_BGM_AMBIENCE_01_CAF,
      IDI_BGM_AMBIENCE_02_CAF,
      IDI_BGM_AMBIENCE_03_CAF,
      IDI_BGM_AMBIENCE_04_CAF,
      IDI_BGM_AMBIENCE_05_CAF,
      IDI_BGM_AMBIENCE_06_CAF,
      IDI_BGM_AMBIENCE_07_CAF,
      IDI_BGM_AMBIENCE_08_CAF,
      IDI_BGM_AMBIENCE_09_CAF,
      IDI_BGM_AMBIENCE_10_CAF,
      IDI_BGM_BF_CASIO_THEME_CAF,
      IDI_BGM_CHASE_01_CAF,
      IDI_BGM_CHASE_02_CAF,
      IDI_BGM_CHASE_03_CAF,
      IDI_BGM_CHASE_04_CAF,
      IDI_BGM_CHASE_05_CAF,
      IDI_BGM_CHASE_06_CAF,
      IDI_BGM_CHASE_07_CAF,
      IDI_BGM_CHASE_08_CAF,
      IDI_BGM_COMBAT_01_CAF,
      IDI_BGM_COMBAT_02_CAF,
      IDI_BGM_COMBAT_03_CAF,
      IDI_BGM_COMBAT_04_CAF,
      IDI_BGM_COMBAT_05_CAF,
      IDI_BGM_COMBAT_06_CAF,
      IDI_BGM_COMBAT_07_CAF,
      IDI_BGM_COMBAT_08_CAF,
      IDI_BGM_COMBAT_09_CAF,
      IDI_BGM_INVERTED_STINGER_01_CAF,
      IDI_BGM_PUZZLE_01_CAF,
      IDI_BGM_PUZZLE_02_CAF,
      IDI_BGM_PUZZLE_03_CAF,
      IDI_BGM_PUZZLE_04_CAF,
      IDI_BGM_PUZZLE_05_CAF,
      IDI_BGM_PUZZLE_06_CAF,
      IDI_BGM_PUZZLE_INTRO_CAF,
      IDI_BGM_REVERSED_STINGER_01_CAF,
      IDI_BGM_STILLALIVE_CAF,
      IDI_BGM_STILLALIVEREMIX_CAF,
      IDI_SFX_AI_SCREAM_SHORT_03_WAV,
      IDI_SFX_AI_SCREAM_SHORT_04_WAV,
      IDI_SFX_AI_SCREAM_SHORT_05_WAV,
      IDI_SFX_AI_SCREAM_SHORT_06_WAV,
      IDI_SFX_BAG_STINGER_WAV,
      IDI_SFX_BLADES_FAR_WAV,
      IDI_SFX_BLADES_NEAR_WAV,
      IDI_SFX_BREATH_SOFT_SHORT_IN_01_WAV,
      IDI_SFX_BREATH_SOFT_SHORT_IN_02_WAV,
      IDI_SFX_BREATH_SOFT_SHORT_IN_03_WAV,
      IDI_SFX_BREATH_SOFT_SHORT_IN_04_WAV,
      IDI_SFX_BREATH_SOFT_SHORT_IN_05_WAV,
      IDI_SFX_BREATH_SOFT_SHORT_IN_06_WAV,
      IDI_SFX_BREATH_SOFT_SHORT_IN_07_WAV,
      IDI_SFX_BREATH_SOFT_SHORT_IN_08_WAV,
      IDI_SFX_BREATH_SOFT_SHORT_OUT_01_WAV,
      IDI_SFX_BREATH_SOFT_SHORT_OUT_02_WAV,
      IDI_SFX_BREATH_SOFT_SHORT_OUT_03_WAV,
      IDI_SFX_BREATH_SOFT_SHORT_OUT_04_WAV,
      IDI_SFX_BREATH_SOFT_SHORT_OUT_05_WAV,
      IDI_SFX_BREATH_SOFT_SHORT_OUT_06_WAV,
      IDI_SFX_BREATH_SOFT_SHORT_OUT_07_WAV,
      IDI_SFX_BREATH_SOFT_SHORT_OUT_08_WAV,
      IDI_SFX_CHECKPOINT_WAV,
      IDI_SFX_COMBAT_START_1_WAV,
      IDI_SFX_COMBAT_START_2_WAV,
      IDI_SFX_COMBAT_START_3_WAV,
      IDI_SFX_COMBAT_START_4_WAV,
      IDI_SFX_COMBAT_SUCCESS_1_WAV,
      IDI_SFX_COMBAT_SUCCESS_2_WAV,
      IDI_SFX_COMBAT_SUCCESS_3_WAV,
      IDI_SFX_COMBAT_SUCCESS_4_WAV,
      IDI_SFX_COMBAT_SUCCESS_5_WAV,
      IDI_SFX_CONCRETEFOOTSTEPRUN_01_WAV,
      IDI_SFX_CONCRETEFOOTSTEPRUN_02_WAV,
      IDI_SFX_CONCRETEFOOTSTEPRUN_03_WAV,
      IDI_SFX_CONCRETEFOOTSTEPRUN_04_WAV,
      IDI_SFX_CONCRETEFOOTSTEPRUN_05_WAV,
      IDI_SFX_CONCRETEFOOTSTEPRUN_06_WAV,
      IDI_SFX_CONCRETEFOOTSTEPRUN_07_WAV,
      IDI_SFX_CONCRETEFOOTSTEPRUN_08_WAV,
      IDI_SFX_CONCRETEFOOTSTEPRUN_09_WAV,
      IDI_SFX_CONCRETEFOOTSTEPRUN_10_WAV,
      IDI_SFX_CONCRETEFOOTSTEPRUN_11_WAV,
      IDI_SFX_CONCRETEFOOTSTEPRUN_12_WAV,
      IDI_SFX_CONCRETEFOOTSTEPRUN_13_WAV,
      IDI_SFX_CONCRETEFOOTSTEPRUN_14_WAV,
      IDI_SFX_DEATH_01_WAV,
      IDI_SFX_FIST_HEAD_01_WAV,
      IDI_SFX_FIST_HEAD_02_WAV,
      IDI_SFX_FIST_HEAD_03_WAV,
      IDI_SFX_FOOT_BODY_01_WAV,
      IDI_SFX_FOOT_BODY_02_WAV,
      IDI_SFX_GLOCK_01_WAV,
      IDI_SFX_GLOCK_02_WAV,
      IDI_SFX_GLOCK_03_WAV,
      IDI_SFX_HELISTART_1_WAV,
      IDI_SFX_IMPACT_01_WAV,
      IDI_SFX_IMPACT_02_WAV,
      IDI_SFX_IMPACT_03_WAV,
      IDI_SFX_IMPACT_04_WAV,
      IDI_SFX_IMPACT_05_WAV,
      IDI_SFX_IMPACT_06_WAV,
      IDI_SFX_IMPACT_07_WAV,
      IDI_SFX_IMPACT_08_WAV,
      IDI_SFX_IMPACT_09_WAV,
      IDI_SFX_IMPACT_MED_01_WAV,
      IDI_SFX_IMPACT_MED_02_WAV,
      IDI_SFX_IMPACT_MED_03_WAV,
      IDI_SFX_IMPACT_MED_04_WAV,
      IDI_SFX_IMPACT_MED_05_WAV,
      IDI_SFX_IMPACT_MED_06_WAV,
      IDI_SFX_IMPACT_MED_07_WAV,
      IDI_SFX_IMPACT_MED_08_WAV,
      IDI_SFX_IMPACT_MED_09_WAV,
      IDI_SFX_IMPACT_MED_10_WAV,
      IDI_SFX_IMPACT_MED_11_WAV,
      IDI_SFX_IMPACT_MED_12_WAV,
      IDI_SFX_IMPACT_MED_13_WAV,
      IDI_SFX_IMPACT_MED_14_WAV,
      IDI_SFX_IMPACT_SOFT_02_WAV,
      IDI_SFX_IMPACT_SOFT_04_WAV,
      IDI_SFX_IMPACT_SOFT_12_WAV,
      IDI_SFX_INVERTED_STINGER_01_WAV,
      IDI_SFX_KICKBACK_01_WAV,
      IDI_SFX_KICKBACK_02_WAV,
      IDI_SFX_KICKSIDE_01_WAV,
      IDI_SFX_KICKSIDE_02_WAV,
      IDI_SFX_METALCRANEFOOTSTEPRUN_01_WAV,
      IDI_SFX_METALCRANEFOOTSTEPRUN_02_WAV,
      IDI_SFX_METALCRANEFOOTSTEPRUN_03_WAV,
      IDI_SFX_METALCRANEFOOTSTEPRUN_04_WAV,
      IDI_SFX_METALCRANEFOOTSTEPRUN_05_WAV,
      IDI_SFX_METALCRANEFOOTSTEPRUN_06_WAV,
      IDI_SFX_METALCRANEFOOTSTEPRUN_07_WAV,
      IDI_SFX_METALCRANEFOOTSTEPRUN_08_WAV,
      IDI_SFX_METALCRANEFOOTSTEPRUN_09_WAV,
      IDI_SFX_METALCRANEFOOTSTEPRUN_10_WAV,
      IDI_SFX_METALDUCTFOOTSTEPRUN_01_WAV,
      IDI_SFX_METALDUCTFOOTSTEPRUN_02_WAV,
      IDI_SFX_METALDUCTFOOTSTEPRUN_03_WAV,
      IDI_SFX_METALDUCTFOOTSTEPRUN_04_WAV,
      IDI_SFX_METALDUCTFOOTSTEPRUN_05_WAV,
      IDI_SFX_METALDUCTFOOTSTEPRUN_06_WAV,
      IDI_SFX_METALDUCTFOOTSTEPRUN_07_WAV,
      IDI_SFX_METALDUCTFOOTSTEPRUN_08_WAV,
      IDI_SFX_METALDUCTFOOTSTEPRUN_09_WAV,
      IDI_SFX_METALDUCTFOOTSTEPRUN_11_WAV,
      IDI_SFX_METALDUCTFOOTSTEPRUN_12_WAV,
      IDI_SFX_METALGANTRYFOOTSTEPRUN_01_WAV,
      IDI_SFX_METALGANTRYFOOTSTEPRUN_02_WAV,
      IDI_SFX_METALGANTRYFOOTSTEPRUN_03_WAV,
      IDI_SFX_METALGANTRYFOOTSTEPRUN_04_WAV,
      IDI_SFX_METALGANTRYFOOTSTEPRUN_05_WAV,
      IDI_SFX_METALGANTRYFOOTSTEPRUN_06_WAV,
      IDI_SFX_METALGANTRYFOOTSTEPRUN_07_WAV,
      IDI_SFX_METALGANTRYFOOTSTEPRUN_08_WAV,
      IDI_SFX_MINIME_01_WAV,
      IDI_SFX_MINIME_02_WAV,
      IDI_SFX_MINIME_03_WAV,
      IDI_SFX_OVERHEAD_WAV,
      IDI_SFX_PIGEON_WINGS_WAV,
      IDI_SFX_PUZZLE_INTRO_WAV,
      IDI_SFX_REVERSED_STINGER_01_WAV,
      IDI_SFX_SLIDE_IN_WAV,
      IDI_SFX_SLIDE_LOOP_WAV,
      IDI_SFX_SLIDE_OUT_WAV,
      IDI_SFX_STEAM_WAV,
      IDI_SFX_STINGER_01_WAV,
      IDI_SFX_STINGER_02_WAV,
      IDI_SFX_STINGER_03_WAV,
      IDI_SFX_STINGER_04_WAV,
      IDI_SFX_STRAIN_SOFT_1_WAV,
      IDI_SFX_STRAIN_SOFT_2_WAV,
      IDI_SFX_STRAIN_SOFT_3_WAV,
      IDI_SFX_STRAIN_SOFT_4_WAV,
      IDI_SFX_STRAIN_SOFT_5_WAV,
      IDI_SFX_STRAIN_SOFT_6_WAV,
      IDI_SFX_STRAIN_SOFT_7_WAV,
      IDI_SFX_STRAIN_SOFT_8_WAV,
      IDI_SFX_UI_NEG_WAV,
      IDI_SFX_UI_POS_WAV,
      IDI_SFX_WILHELM_SCREAM_WAV,
      IDI_SFX_WOODPLANKSFOOTSTEPRUN_01_WAV,
      IDI_SFX_WOODPLANKSFOOTSTEPRUN_02_WAV,
      IDI_SFX_WOODPLANKSFOOTSTEPRUN_03_WAV,
      IDI_SFX_WOODPLANKSFOOTSTEPRUN_04_WAV,
      IDI_SFX_WOODPLANKSFOOTSTEPRUN_05_WAV,
      IDI_SFX_WOODPLANKSFOOTSTEPRUN_06_WAV,
      IDI_SFX_WOODPLANKSFOOTSTEPRUN_07_WAV,
      IDI_SFX_WOODPLANKSFOOTSTEPRUN_08_WAV,
      IDI_SFX_WOODPLANKSFOOTSTEPRUN_09_WAV,
      IDI_SFX_WOODPLANKSFOOTSTEPRUN_10_WAV,
      IDI_SFX_ZIP_IN_WAV,
      IDI_SFX_ZIP_LOOP_WAV,
      IDI_SFX_ZIP_OUT_WAV,
      NUM_RES_FILES,
    }

    public enum Idi_Trial
    {
      IDI_CAMERA_M3G,
      IDI_CHOPPER_M3G,
      IDI_EFFECT_BULLET_M3G,
      IDI_EFFECT_COLLECTABLE_M3G,
      IDI_FAITH_M3G,
      IDI_FAITH_ORIGIN_OFFSETS_M3G,
      IDI_PALETTE_CHAPTER_01_M3G,
      IDI_PALETTE_TUTORIAL_M3G,
      IDI_PIGEON_M3G,
      IDI_PLANE_LIGHT_M3G,
      IDI_TEXTURE_BULLET_EFFECT_M3G,
      IDI_TEXTURE_BUTTON_EFFECT_M3G,
      IDI_TEXTURE_CHAPTEREND_0_0_M3G,
      IDI_TEXTURE_CHAPTEREND_1_1_M3G,
      IDI_TEXTURE_CHECKPOINT_EFFECT_M3G,
      IDI_TEXTURE_CHOPPER_M3G,
      IDI_TEXTURE_COLLECT_EFFECT_M3G,
      IDI_TEXTURE_EA_LOGO_M3G,
      IDI_TEXTURE_FAITH_GHOST_M3G,
      IDI_TEXTURE_FAITH_MIDDAY_M3G,
      IDI_TEXTURE_FX_PARTICLES_M3G,
      IDI_TEXTURE_FX_SMOKE_T1_M3G,
      IDI_TEXTURE_FX_STEAMJET_T1_M3G,
      IDI_TEXTURE_INTRO_BG_M3G,
      IDI_TEXTURE_MAP_BG_BACKDROP_MIDDAY_M3G,
      IDI_TEXTURE_MAP_BUILDINGS_MIDDAY_M3G,
      IDI_TEXTURE_MAP_OBJECTS_MIDDAY_M3G,
      IDI_TEXTURE_MAP_OBJECTS_MIDDAY_2_M3G,
      IDI_TEXTURE_MIRRORSEDGE_LOGO_M3G,
      IDI_TEXTURE_MIRRORSEDGE_LOGO_SML_M3G,
      IDI_TEXTURE_PAIN_EFFECT_M3G,
      IDI_TEXTURE_PIGEON_M3G,
      IDI_TEXTURE_PLANE_LIGHT_M3G,
      IDI_TEXTURE_REFLECTION_MIDDAY_M3G,
      IDI_TEXTURE_SOUND_ICON_OFF_M3G,
      IDI_TEXTURE_SOUND_ICON_ON_M3G,
      IDI_TEXTURE_SOUND_ICON_SLIDER_M3G,
      IDI_TEXTURE_SPEEDRUN_STAR_M3G,
      IDI_TEXTURE_UI_BUTTON_ARROW_ACTIVE_M3G,
      IDI_TEXTURE_UI_BUTTON_ARROW_DISABLED_M3G,
      IDI_TEXTURE_UI_BUTTON_MAJOR_M3G,
      IDI_TEXTURE_UI_BUTTON_MG_M3G,
      IDI_TEXTURE_UI_BUTTON_MINOR_M3G,
      IDI_TEXTURE_UI_CITY_M3G,
      IDI_TEXTURE_UI_COMPLETE_BAGS_M3G,
      IDI_TEXTURE_UI_LOADING_BORDER_M3G,
      IDI_TEXTURE_UI_LOADING_BORDER_BLUE_M3G,
      IDI_TEXTURE_UI_LOADING_BORDER_RED_M3G,
      IDI_TEXTURE_UI_LOADING_TEXT_BLANK_M3G,
      IDI_TEXTURE_UI_RIBBON_RED_LEFT_M3G,
      IDI_TEXTURE_UI_RIBBON_RED_RIGHT_M3G,
      IDI_TEXTURE_UI_RIBBON_WHITE_LEFT_M3G,
      IDI_TEXTURE_UI_RIBBON_WHITE_RIGHT_M3G,
      IDI_TEXTURE_UI_TICKER_BAR_M3G,
      IDI_TEXTURE_UI_WINDOW_M3G,
      IDI_TEXTURE_UPSELL_SCREEN_1_M3G,
      IDI_TEXTURE_UPSELL_SCREEN_2_M3G,
      IDI_TEXTURE_UPSELL_SCREEN_3_M3G,
      IDI_TEXTURE_UPSELL_SCREEN_4_M3G,
      IDI_TEXTURE_UPSELL_SCREEN_5_M3G,
      IDI_UI_CAMERA_M3G,
      IDI_UI_CITY_M3G,
      IDI_UI_LOADING_M3G,
      IDI_UI_LOADING_SHORT_M3G,
      IDI_ANIM3D_BIN,
      IDI_ANIMATION_BLENDERS_BIN,
      IDI_ANIMDATA_BIN,
      IDI_CHAPTER_01_BIN,
      IDI_COLOR_BIN,
      IDI_GAME_OBJECTS_BIN,
      IDI_IMAGE_BIN,
      IDI_LEVEL_DATA_BIN,
      IDI_LOADING_SCREEN_DATA_BIN,
      IDI_M3G_ASSETS_BIN,
      IDI_MATERIALS_BIN,
      IDI_QUADS_BIN,
      IDI_RUNNER_DATA_BIN,
      IDI_SOUND_SEQUENCER_BIN,
      IDI_SOUNDEVENTS_BIN,
      IDI_SOUNDPOOLS_BIN,
      IDI_TUTORIAL_BIN,
      IDI_FONTS_BIN,
      IDI_HELVETICANEUELTSTD_MDEXO_OTF,
      IDI_LOADING_RUN_PNG,
      IDI_UI_LOADING_ICON_STARS_PNG,
      IDI_UI_LOADING_TEXT_BLANK_PNG,
      IDI_UI_LOADING_TEXT_BLANK_SHORT_PNG,
      IDI_BGM_AMBIENCE_03_CAF,
      IDI_BGM_CHASE_02_CAF,
      IDI_BGM_STILLALIVE_CAF,
      IDI_BGM_STILLALIVEREMIX_CAF,
      IDI_SFX_BAG_STINGER_WAV,
      IDI_SFX_BLADES_FAR_WAV,
      IDI_SFX_BLADES_NEAR_WAV,
      IDI_SFX_CHECKPOINT_WAV,
      IDI_SFX_CONCRETEFOOTSTEPRUN_01_WAV,
      IDI_SFX_CONCRETEFOOTSTEPRUN_02_WAV,
      IDI_SFX_DEATH_01_WAV,
      IDI_SFX_IMPACT_01_WAV,
      IDI_SFX_IMPACT_02_WAV,
      IDI_SFX_IMPACT_MED_01_WAV,
      IDI_SFX_IMPACT_MED_02_WAV,
      IDI_SFX_METALCRANEFOOTSTEPRUN_01_WAV,
      IDI_SFX_METALCRANEFOOTSTEPRUN_02_WAV,
      IDI_SFX_METALDUCTFOOTSTEPRUN_01_WAV,
      IDI_SFX_METALDUCTFOOTSTEPRUN_02_WAV,
      IDI_SFX_METALGANTRYFOOTSTEPRUN_01_WAV,
      IDI_SFX_METALGANTRYFOOTSTEPRUN_02_WAV,
      IDI_SFX_MINIME_01_WAV,
      IDI_SFX_MINIME_02_WAV,
      IDI_SFX_MINIME_03_WAV,
      IDI_SFX_OVERHEAD_WAV,
      IDI_SFX_PIGEON_WINGS_WAV,
      IDI_SFX_PUZZLE_INTRO_WAV,
      IDI_SFX_SLIDE_IN_WAV,
      IDI_SFX_SLIDE_LOOP_WAV,
      IDI_SFX_SLIDE_OUT_WAV,
      IDI_SFX_STINGER_01_WAV,
      IDI_SFX_STINGER_02_WAV,
      IDI_SFX_STRAIN_SOFT_1_WAV,
      IDI_SFX_STRAIN_SOFT_2_WAV,
      IDI_SFX_UI_NEG_WAV,
      IDI_SFX_UI_POS_WAV,
      IDI_SFX_WOODPLANKSFOOTSTEPRUN_01_WAV,
      IDI_SFX_WOODPLANKSFOOTSTEPRUN_02_WAV,
      IDI_SFX_ZIP_IN_WAV,
      IDI_SFX_ZIP_LOOP_WAV,
      IDI_SFX_ZIP_OUT_WAV,
      IDI_ACHIEVEMENT_DATA_BIN,
      NUM_RES_FILES,
    }

    public enum IdiOffset_Full
    {
      IDI_OFFSET_ROOT = 0,
      IDI_OFFSET_DATA = 138, // 0x0000008A
      IDI_OFFSET_FONTS = 169, // 0x000000A9
      IDI_OFFSET_IMAGES = 171, // 0x000000AB
      IDI_OFFSET_SOUNDS = 200, // 0x000000C8
    }

    public enum IdiOffset_Trial
    {
      IDI_OFFSET_ROOT = 0,
      IDI_OFFSET_DATA = 64, // 0x00000040
      IDI_OFFSET_FONTS = 81, // 0x00000051
      IDI_OFFSET_IMAGES = 83, // 0x00000053
      IDI_OFFSET_SOUNDS = 87, // 0x00000057
    }

    public enum SoundEventPoolset_Full
    {
      SOUNDEVENTPOOLSET_FOOTSTEPS,
      SOUNDEVENTPOOLSET_VOCAL,
      SOUNDEVENTPOOLSET_BREATHS,
      SOUNDEVENTPOOLSET_GUNSHOTS,
      SOUNDEVENTPOOLSET_BULLET_IMPACTS,
      SOUNDEVENTPOOLSET_MELEE,
    }

    public enum SoundEventPoolset_Trial
    {
      SOUNDEVENTPOOLSET_FOOTSTEPS,
      SOUNDEVENTPOOLSET_VOCAL,
      SOUNDEVENTPOOLSET_GUNSHOTS,
      SOUNDEVENTPOOLSET_BULLET_IMPACTS,
    }

    public enum SoundEventPoolVocal_Full
    {
      SOUNDEVENTPOOL_VOCAL_SOFT_IMPACTS,
      SOUNDEVENTPOOL_VOCAL_MED_IMPACTS,
      SOUNDEVENTPOOL_VOCAL_SOFT_STRAINS,
    }

    public enum SoundEventPoolVocal_Trial
    {
      SOUNDEVENTPOOL_VOCAL_MED_IMPACTS,
      SOUNDEVENTPOOL_VOCAL_SOFT_STRAINS,
    }

    public enum SoundEventPoolGunshots_Full
    {
      SOUNDEVENTPOOL_GUNSHOTS_GLOCK,
      SOUNDEVENTPOOL_GUNSHOTS_MINIGUN,
    }

    public enum SoundEventPoolGunshots_Trial
    {
      SOUNDEVENTPOOL_GUNSHOTS_MINIGUN,
    }

    public enum SoundEvent_Full
    {
      SOUNDEVENT_BGM_MENU,
      SOUNDEVENT_BGM_GAME,
      SOUNDEVENT_BGM_AMBIENCE_01,
      SOUNDEVENT_BGM_AMBIENCE_02,
      SOUNDEVENT_BGM_AMBIENCE_03,
      SOUNDEVENT_BGM_AMBIENCE_04,
      SOUNDEVENT_BGM_AMBIENCE_05,
      SOUNDEVENT_BGM_AMBIENCE_06,
      SOUNDEVENT_BGM_AMBIENCE_07,
      SOUNDEVENT_BGM_AMBIENCE_08,
      SOUNDEVENT_BGM_AMBIENCE_09,
      SOUNDEVENT_BGM_AMBIENCE_10,
      SOUNDEVENT_BGM_CHASE_01,
      SOUNDEVENT_BGM_CHASE_02,
      SOUNDEVENT_BGM_CHASE_03,
      SOUNDEVENT_BGM_CHASE_04,
      SOUNDEVENT_BGM_CHASE_05,
      SOUNDEVENT_BGM_CHASE_06,
      SOUNDEVENT_BGM_CHASE_07,
      SOUNDEVENT_BGM_CHASE_08,
      SOUNDEVENT_BGM_COMBAT_01,
      SOUNDEVENT_BGM_COMBAT_02,
      SOUNDEVENT_BGM_COMBAT_03,
      SOUNDEVENT_BGM_COMBAT_04,
      SOUNDEVENT_BGM_COMBAT_05,
      SOUNDEVENT_BGM_COMBAT_06,
      SOUNDEVENT_BGM_COMBAT_07,
      SOUNDEVENT_BGM_COMBAT_08,
      SOUNDEVENT_BGM_COMBAT_09,
      SOUNDEVENT_BGM_PUZZLE_01,
      SOUNDEVENT_BGM_PUZZLE_02,
      SOUNDEVENT_BGM_PUZZLE_03,
      SOUNDEVENT_BGM_PUZZLE_04,
      SOUNDEVENT_BGM_PUZZLE_05,
      SOUNDEVENT_BGM_PUZZLE_06,
      SOUNDEVENT_SFX_UI_POSITIVE,
      SOUNDEVENT_SFX_UI_NEGATIVE,
      SOUNDEVENT_SFX_IMPACT_01,
      SOUNDEVENT_SFX_IMPACT_02,
      SOUNDEVENT_SFX_IMPACT_03,
      SOUNDEVENT_SFX_IMPACT_04,
      SOUNDEVENT_SFX_IMPACT_05,
      SOUNDEVENT_SFX_IMPACT_06,
      SOUNDEVENT_SFX_IMPACT_07,
      SOUNDEVENT_SFX_IMPACT_08,
      SOUNDEVENT_SFX_IMPACT_09,
      SOUNDEVENT_SFX_GLOCK_01,
      SOUNDEVENT_SFX_GLOCK_02,
      SOUNDEVENT_SFX_GLOCK_03,
      SOUNDEVENT_SFX_BLADES_FAR,
      SOUNDEVENT_SFX_BLADES_NEAR,
      SOUNDEVENT_SFX_MINIME_01,
      SOUNDEVENT_SFX_MINIME_02,
      SOUNDEVENT_SFX_MINIME_03,
      SOUNDEVENT_SFX_OVERHEAD,
      SOUNDEVENT_SFX_FIST_HEAD_01,
      SOUNDEVENT_SFX_FIST_HEAD_02,
      SOUNDEVENT_SFX_FIST_HEAD_03,
      SOUNDEVENT_SFX_FOOT_BODY_01,
      SOUNDEVENT_SFX_FOOT_BODY_02,
      SOUNDEVENT_SFX_KICKBACK_01,
      SOUNDEVENT_SFX_KICKBACK_02,
      SOUNDEVENT_SFX_KICKSIDE_01,
      SOUNDEVENT_SFX_KICKSIDE_02,
      SOUNDEVENT_SFX_PUZZLE_INTRO,
      SOUNDEVENT_SFX_STINGER_01,
      SOUNDEVENT_SFX_STINGER_02,
      SOUNDEVENT_SFX_STINGER_03,
      SOUNDEVENT_SFX_STINGER_04,
      SOUNDEVENT_SFX_BAG_STINGER,
      SOUNDEVENT_SFX_CHECKPOINT,
      SOUNDEVENT_SFX_SLIDE_IN,
      SOUNDEVENT_SFX_SLIDE_LOOP,
      SOUNDEVENT_SFX_SLIDE_OUT,
      SOUNDEVENT_SFX_ZIP_IN,
      SOUNDEVENT_SFX_ZIP_LOOP,
      SOUNDEVENT_SFX_ZIP_OUT,
      SOUNDEVENT_SFX_STEAM,
      SOUNDEVENT_SFX_PIGEON_WINGS,
      SOUNDEVENT_SFX_WILHELM_SCREAM,
      SOUNDEVENT_SFX_COMBAT_START_1,
      SOUNDEVENT_SFX_COMBAT_START_2,
      SOUNDEVENT_SFX_COMBAT_START_3,
      SOUNDEVENT_SFX_COMBAT_START_4,
      SOUNDEVENT_SFX_COMBAT_SUCCESS_1,
      SOUNDEVENT_SFX_COMBAT_SUCCESS_2,
      SOUNDEVENT_SFX_COMBAT_SUCCESS_3,
      SOUNDEVENT_SFX_COMBAT_SUCCESS_4,
      SOUNDEVENT_SFX_COMBAT_SUCCESS_5,
      SOUNDEVENT_SFX_SCREAM_SHORT_1,
      SOUNDEVENT_SFX_SCREAM_SHORT_2,
      SOUNDEVENT_SFX_SCREAM_SHORT_3,
      SOUNDEVENT_SFX_SCREAM_SHORT_4,
      SOUNDEVENT_SFX_CONCRETEFOOTSTEPRUN_01,
      SOUNDEVENT_SFX_CONCRETEFOOTSTEPRUN_02,
      SOUNDEVENT_SFX_CONCRETEFOOTSTEPRUN_03,
      SOUNDEVENT_SFX_CONCRETEFOOTSTEPRUN_04,
      SOUNDEVENT_SFX_CONCRETEFOOTSTEPRUN_05,
      SOUNDEVENT_SFX_CONCRETEFOOTSTEPRUN_06,
      SOUNDEVENT_SFX_CONCRETEFOOTSTEPRUN_07,
      SOUNDEVENT_SFX_CONCRETEFOOTSTEPRUN_08,
      SOUNDEVENT_SFX_CONCRETEFOOTSTEPRUN_09,
      SOUNDEVENT_SFX_CONCRETEFOOTSTEPRUN_10,
      SOUNDEVENT_SFX_CONCRETEFOOTSTEPRUN_11,
      SOUNDEVENT_SFX_CONCRETEFOOTSTEPRUN_12,
      SOUNDEVENT_SFX_CONCRETEFOOTSTEPRUN_13,
      SOUNDEVENT_SFX_METALCRANEFOOTSTEPRUN_01,
      SOUNDEVENT_SFX_METALCRANEFOOTSTEPRUN_02,
      SOUNDEVENT_SFX_METALCRANEFOOTSTEPRUN_03,
      SOUNDEVENT_SFX_METALCRANEFOOTSTEPRUN_04,
      SOUNDEVENT_SFX_METALCRANEFOOTSTEPRUN_05,
      SOUNDEVENT_SFX_METALCRANEFOOTSTEPRUN_06,
      SOUNDEVENT_SFX_METALCRANEFOOTSTEPRUN_07,
      SOUNDEVENT_SFX_METALCRANEFOOTSTEPRUN_08,
      SOUNDEVENT_SFX_METALCRANEFOOTSTEPRUN_09,
      SOUNDEVENT_SFX_METALCRANEFOOTSTEPRUN_10,
      SOUNDEVENT_SFX_METALDUCTFOOTSTEPRUN_01,
      SOUNDEVENT_SFX_METALDUCTFOOTSTEPRUN_02,
      SOUNDEVENT_SFX_METALDUCTFOOTSTEPRUN_03,
      SOUNDEVENT_SFX_METALDUCTFOOTSTEPRUN_04,
      SOUNDEVENT_SFX_METALDUCTFOOTSTEPRUN_05,
      SOUNDEVENT_SFX_METALDUCTFOOTSTEPRUN_06,
      SOUNDEVENT_SFX_METALDUCTFOOTSTEPRUN_07,
      SOUNDEVENT_SFX_METALDUCTFOOTSTEPRUN_08,
      SOUNDEVENT_SFX_METALDUCTFOOTSTEPRUN_09,
      SOUNDEVENT_SFX_METALDUCTFOOTSTEPRUN_11,
      SOUNDEVENT_SFX_METALDUCTFOOTSTEPRUN_12,
      SOUNDEVENT_SFX_METALGANTRYFOOTSTEPRUN_01,
      SOUNDEVENT_SFX_METALGANTRYFOOTSTEPRUN_02,
      SOUNDEVENT_SFX_METALGANTRYFOOTSTEPRUN_03,
      SOUNDEVENT_SFX_METALGANTRYFOOTSTEPRUN_04,
      SOUNDEVENT_SFX_METALGANTRYFOOTSTEPRUN_05,
      SOUNDEVENT_SFX_METALGANTRYFOOTSTEPRUN_06,
      SOUNDEVENT_SFX_METALGANTRYFOOTSTEPRUN_07,
      SOUNDEVENT_SFX_METALGANTRYFOOTSTEPRUN_08,
      SOUNDEVENT_SFX_WOODPLANKSFOOTSTEPRUN_01,
      SOUNDEVENT_SFX_WOODPLANKSFOOTSTEPRUN_02,
      SOUNDEVENT_SFX_WOODPLANKSFOOTSTEPRUN_03,
      SOUNDEVENT_SFX_WOODPLANKSFOOTSTEPRUN_04,
      SOUNDEVENT_SFX_WOODPLANKSFOOTSTEPRUN_05,
      SOUNDEVENT_SFX_WOODPLANKSFOOTSTEPRUN_06,
      SOUNDEVENT_SFX_WOODPLANKSFOOTSTEPRUN_07,
      SOUNDEVENT_SFX_WOODPLANKSFOOTSTEPRUN_08,
      SOUNDEVENT_SFX_WOODPLANKSFOOTSTEPRUN_09,
      SOUNDEVENT_SFX_WOODPLANKSFOOTSTEPRUN_10,
      SOUNDEVENT_SFX_BREATH_IN_01,
      SOUNDEVENT_SFX_BREATH_IN_02,
      SOUNDEVENT_SFX_BREATH_IN_03,
      SOUNDEVENT_SFX_BREATH_IN_04,
      SOUNDEVENT_SFX_BREATH_IN_05,
      SOUNDEVENT_SFX_BREATH_IN_06,
      SOUNDEVENT_SFX_BREATH_OPEN_IN_01,
      SOUNDEVENT_SFX_BREATH_OPEN_IN_02,
      SOUNDEVENT_SFX_BREATH_OUT_01,
      SOUNDEVENT_SFX_BREATH_OUT_02,
      SOUNDEVENT_SFX_BREATH_OUT_03,
      SOUNDEVENT_SFX_BREATH_OUT_04,
      SOUNDEVENT_SFX_BREATH_OUT_05,
      SOUNDEVENT_SFX_BREATH_OUT_06,
      SOUNDEVENT_SFX_BREATH_OPEN_OUT_01,
      SOUNDEVENT_SFX_BREATH_OPEN_OUT_02,
      SOUNDEVENT_SFX_IMPACT_MEDIUM_01,
      SOUNDEVENT_SFX_IMPACT_MEDIUM_02,
      SOUNDEVENT_SFX_IMPACT_MEDIUM_03,
      SOUNDEVENT_SFX_IMPACT_MEDIUM_04,
      SOUNDEVENT_SFX_IMPACT_MEDIUM_05,
      SOUNDEVENT_SFX_IMPACT_MEDIUM_06,
      SOUNDEVENT_SFX_IMPACT_MEDIUM_07,
      SOUNDEVENT_SFX_IMPACT_MEDIUM_08,
      SOUNDEVENT_SFX_IMPACT_MEDIUM_09,
      SOUNDEVENT_SFX_IMPACT_MEDIUM_10,
      SOUNDEVENT_SFX_IMPACT_MEDIUM_11,
      SOUNDEVENT_SFX_IMPACT_MEDIUM_12,
      SOUNDEVENT_SFX_IMPACT_MEDIUM_13,
      SOUNDEVENT_SFX_IMPACT_MEDIUM_14,
      SOUNDEVENT_SFX_IMPACT_SOFT_01,
      SOUNDEVENT_SFX_IMPACT_SOFT_02,
      SOUNDEVENT_SFX_IMPACT_SOFT_03,
      SOUNDEVENT_SFX_STRAIN_SOFT_01,
      SOUNDEVENT_SFX_STRAIN_SOFT_02,
      SOUNDEVENT_SFX_STRAIN_SOFT_03,
      SOUNDEVENT_SFX_STRAIN_SOFT_04,
      SOUNDEVENT_SFX_STRAIN_SOFT_05,
      SOUNDEVENT_SFX_STRAIN_SOFT_06,
      SOUNDEVENT_SFX_STRAIN_SOFT_07,
      SOUNDEVENT_SFX_STRAIN_SOFT_08,
      SOUNDEVENT_SFX_DEATH_01,
    }

    public enum SoundEvent_Trial
    {
      SOUNDEVENT_BGM_MENU,
      SOUNDEVENT_BGM_GAME,
      SOUNDEVENT_BGM_AMBIENCE_03,
      SOUNDEVENT_BGM_CHASE_02,
      SOUNDEVENT_SFX_UI_POSITIVE,
      SOUNDEVENT_SFX_UI_NEGATIVE,
      SOUNDEVENT_SFX_IMPACT_01,
      SOUNDEVENT_SFX_IMPACT_02,
      SOUNDEVENT_SFX_BLADES_FAR,
      SOUNDEVENT_SFX_BLADES_NEAR,
      SOUNDEVENT_SFX_MINIME_01,
      SOUNDEVENT_SFX_MINIME_02,
      SOUNDEVENT_SFX_MINIME_03,
      SOUNDEVENT_SFX_OVERHEAD,
      SOUNDEVENT_SFX_PUZZLE_INTRO,
      SOUNDEVENT_SFX_STINGER_01,
      SOUNDEVENT_SFX_STINGER_02,
      SOUNDEVENT_SFX_BAG_STINGER,
      SOUNDEVENT_SFX_CHECKPOINT,
      SOUNDEVENT_SFX_SLIDE_IN,
      SOUNDEVENT_SFX_SLIDE_LOOP,
      SOUNDEVENT_SFX_SLIDE_OUT,
      SOUNDEVENT_SFX_ZIP_IN,
      SOUNDEVENT_SFX_ZIP_LOOP,
      SOUNDEVENT_SFX_ZIP_OUT,
      SOUNDEVENT_SFX_PIGEON_WINGS,
      SOUNDEVENT_SFX_CONCRETEFOOTSTEPRUN_01,
      SOUNDEVENT_SFX_CONCRETEFOOTSTEPRUN_02,
      SOUNDEVENT_SFX_METALCRANEFOOTSTEPRUN_01,
      SOUNDEVENT_SFX_METALCRANEFOOTSTEPRUN_02,
      SOUNDEVENT_SFX_METALDUCTFOOTSTEPRUN_01,
      SOUNDEVENT_SFX_METALDUCTFOOTSTEPRUN_02,
      SOUNDEVENT_SFX_METALGANTRYFOOTSTEPRUN_01,
      SOUNDEVENT_SFX_METALGANTRYFOOTSTEPRUN_02,
      SOUNDEVENT_SFX_WOODPLANKSFOOTSTEPRUN_01,
      SOUNDEVENT_SFX_WOODPLANKSFOOTSTEPRUN_02,
      SOUNDEVENT_SFX_IMPACT_MEDIUM_01,
      SOUNDEVENT_SFX_IMPACT_MEDIUM_02,
      SOUNDEVENT_SFX_STRAIN_SOFT_01,
      SOUNDEVENT_SFX_STRAIN_SOFT_02,
      SOUNDEVENT_SFX_DEATH_01,
    }

    public enum Anim3D_Full
    {
      ANIM3D_NULL,
      ANIM3D_FAITH_INTRO_JUMPINGSTRETCHING,
      ANIM3D_FAITH_INTRO_LANDING,
      ANIM3D_FAITH_INTRO_2_1CAUTIOUS,
      ANIM3D_FAITH_INTRO_3_2DAZED,
      ANIM3D_FAITH_RUN_SLOW,
      ANIM3D_FAITH_RUN_MEDIUM,
      ANIM3D_FAITH_RUN_FAST,
      ANIM3D_FAITH_RUN_UPHILL,
      ANIM3D_FAITH_RUN_STOP_SLIDE,
      ANIM3D_FAITH_RUN_180_SLIDE_START,
      ANIM3D_FAITH_RUN_180_SLIDE_FINSH,
      ANIM3D_FAITH_IDLE,
      ANIM3D_FAITH_JUMP_PREJUMP,
      ANIM3D_FAITH_JUMP_RISING,
      ANIM3D_FAITH_JUMP_FALLING,
      ANIM3D_FAITH_JUMP_LANDING,
      ANIM3D_FAITH_JUMP_FROM_CLAMBER,
      ANIM3D_FAITH_VERTICAL_RUN,
      ANIM3D_FAITH_WALLRUN_RIGHT_START,
      ANIM3D_FAITH_WALLRUN_RIGHT_LOOP,
      ANIM3D_FAITH_WALLRUN_RIGHT_END,
      ANIM3D_FAITH_WALLRUN_LEFT_START,
      ANIM3D_FAITH_WALLRUN_LEFT_LOOP,
      ANIM3D_FAITH_WALLRUN_LEFT_END,
      ANIM3D_FAITH_SLIDE_START,
      ANIM3D_FAITH_SLIDE_LOOP,
      ANIM3D_FAITH_SLIDE_END,
      ANIM3D_FAITH_EDGE_VAULT,
      ANIM3D_FAITH_CLAMBER_ONE_METRE,
      ANIM3D_FAITH_CLAMBER_ONE_METRE_FROM_STATIC,
      ANIM3D_FAITH_CLAMBER_HALF_METRE,
      ANIM3D_FAITH_CLAMBER_HALF_METRE_FROM_STATIC,
      ANIM3D_FAITH_CLAMBER_FROM_HANG,
      ANIM3D_FAITH_HANG,
      ANIM3D_FAITH_HANG_TO_WALL_SLIDE,
      ANIM3D_FAITH_VERTICLE_WALL_SLIDE,
      ANIM3D_FAITH_VERTICLE_WALL_JUMP,
      ANIM3D_FAITH_WALL_SLIDE_OPTIMAL,
      ANIM3D_FAITH_WALL_SLIDE_LEFT,
      ANIM3D_FAITH_WALL_SLIDE_RIGHT,
      ANIM3D_FAITH_ROLL,
      ANIM3D_FAITH_LAND_BADLY,
      ANIM3D_FAITH_BALANCE_OPTIMAL,
      ANIM3D_FAITH_BALANCE_LEFT,
      ANIM3D_FAITH_BALANCE_RIGHT,
      ANIM3D_FAITH_SWING_IDLE,
      ANIM3D_FAITH_SWING_FORWARD,
      ANIM3D_FAITH_SWING_BACK,
      ANIM3D_FAITH_ZIPLINE_OPTIMAL,
      ANIM3D_FAITH_ZIPLINE_FORWARD,
      ANIM3D_FAITH_ZIPLINE_BACK,
      ANIM3D_FAITH_ZIPLINE_FAIL,
      ANIM3D_FAITH_DYNAMIC_CLIMB,
      ANIM3D_FAITH_DYNAMIC_CLIMB_REVERSE,
      ANIM3D_FAITH_DYNAMIC_CLIMB_REVERSE_HALFSTEP,
      ANIM3D_FAITH_STATIC_CLIMB,
      ANIM3D_FAITH_STATIC_CLIMB_REVERSE,
      ANIM3D_FAITH_POWERJUMP_JUMP,
      ANIM3D_FAITH_POWERJUMP_TRANSITION,
      ANIM3D_FAITH_POWERJUMP_LOOP,
      ANIM3D_FAITH_BALANCE_FALL,
      ANIM3D_FAITH_BALANCE_FALLEN,
      ANIM3D_FAITH_BALANCE_RECLIMB,
      ANIM3D_FAITH_MELEE_FAIL,
      ANIM3D_FAITH_MELEE_SUCCESS,
      ANIM3D_FAITH_MELEE_SUCCESS_HALFSTEP,
      ANIM3D_FAITH_MELEE_GETUP,
      ANIM3D_FAITH_PRONE_IDLE,
      ANIM3D_FAITH_PRONE_RECOVER,
      ANIM3D_FAITH_FLYING_KICK,
      ANIM3D_FAITH_FLYING_KICK_LOOP,
      ANIM3D_FAITH_FLYING_KICK_IMPACT,
      ANIM3D_FAITH_FALL,
      ANIM3D_FAITH_DEATH_LANDING,
      ANIM3D_FAITH_DEATH_RUNNING,
      ANIM3D_FAITH_ORIGIN_EDGE_VAULT,
      ANIM3D_FAITH_ORIGIN_CLAMBER_ONE_METRE,
      ANIM3D_FAITH_ORIGIN_CLAMBER_ONE_METRE_FROM_STATIC,
      ANIM3D_FAITH_ORIGIN_CLAMBER_HALF_METRE,
      ANIM3D_FAITH_ORIGIN_CLAMBER_HALF_METER_FROM_STATIC,
      ANIM3D_FAITH_ORIGIN_CLAMBER_FROM_HANG,
      ANIM3D_FAITH_ORIGIN_DYNAMIC_CLIMB,
      ANIM3D_FAITH_ORIGIN_DYNAMIC_CLIMB_REVERSE,
      ANIM3D_FAITH_ORIGIN_STATIC_CLIMB,
      ANIM3D_FAITH_ORIGIN_STATIC_CLIMB_REVERSE,
      ANIM3D_FAITH_ORIGIN_BALANCE_FALL,
      ANIM3D_FAITH_ORIGIN_MELEE_FAIL,
      ANIM3D_FAITH_ORIGIN_MELEE_SUCCESS,
      ANIM3D_FAITH_ORIGIN_MELEE_GETUP,
      ANIM3D_FAITH_ORIGIN_DEATH_RUNNING,
      ANIM3D_FAITH_ORIGIN_BALANCE_RECLIMB,
      ANIM3D_POLICE_IDLE,
      ANIM3D_POLICE_DRAW_PISTOL,
      ANIM3D_POLICE_LOWER_PISTOL,
      ANIM3D_POLICE_LOWERED_PISTOL,
      ANIM3D_POLICE_AIM_PISTOL,
      ANIM3D_POLICE_AIM_HOLD,
      ANIM3D_POLICE_FIRE_PISTOL,
      ANIM3D_POLICE_HOLSTER_PISTOL,
      ANIM3D_POLICE_CROUCH,
      ANIM3D_POLICE_CROUCH_LOWER_PISTOL,
      ANIM3D_POLICE_CROUCH_LOWERED_PISTOL,
      ANIM3D_POLICE_CROUCH_AIM_PISTOL,
      ANIM3D_POLICE_CROUCH_AIM_HOLD,
      ANIM3D_POLICE_CROUCH_FIRE,
      ANIM3D_POLICE_CROUCH_STAND,
      ANIM3D_POLICE_KNOCKDOWN_FORWARDS_LOW,
      ANIM3D_POLICE_KNOCKDOWN_FORWARDS_MEDIUM,
      ANIM3D_POLICE_KNOCKDOWN_FORWARDS_HIGH,
      ANIM3D_POLICE_KNOCKDOWN_BACKWARDS_LOW,
      ANIM3D_POLICE_KNOCKDOWN_BACKWARDS_MEDIUM,
      ANIM3D_POLICE_KNOCKDOWN_BACKWARDS_HIGH,
      ANIM3D_POLICE_KNOCKDOWN_IDLE_FORWARDS,
      ANIM3D_POLICE_KNOCKDOWN_IDLE_BACKWARDS,
      ANIM3D_POLICE_KNOCKDOWN_STAND_FORWARDS,
      ANIM3D_POLICE_KNOCKDOWN_STAND_BACKWARDS,
      ANIM3D_POLICE_RUN,
      ANIM3D_POLICE_WALK,
      ANIM3D_POLICE_MELEE_FAIL,
      ANIM3D_POLICE_MELEE_SUCCESS,
      ANIM3D_POLICE_MELEE_GETUP,
      ANIM3D_POLICE_FALL_FORWARDS,
      ANIM3D_POLICE_FALL_BACKWARDS,
      ANIM3D_POLICE_LAND_FORWARDS,
      ANIM3D_POLICE_LAND_BACKWARDS,
      ANIM3D_POLICE_ORIGIN_KNOCKDOWN_FORWARDS_LOW,
      ANIM3D_POLICE_ORIGIN_KNOCKDOWN_FORWARDS_MEDIUM,
      ANIM3D_POLICE_ORIGIN_KNOCKDOWN_FORWARDS_HIGH,
      ANIM3D_POLICE_ORIGIN_KNOCKDOWN_BACKWARDS_LOW,
      ANIM3D_POLICE_ORIGIN_KNOCKDOWN_BACKWARDS_MEDIUM,
      ANIM3D_POLICE_ORIGIN_KNOCKDOWN_BACKWARDS_HIGH,
      ANIM3D_POLICE_ORIGIN_MELEE_SUCCESS,
      ANIM3D_POLICE_OROGIN_MELEE_GETUP,
      ANIM3D_POLICE_ORIGIN_LAND,
      ANIM3D_CAMERA_INTRO_JUMPINGSTRETCHING,
      ANIM3D_CAMERA_INTRO_LANDING,
      ANIM3D_CAMERA_INTRO_2_1CAUTIOUS,
      ANIM3D_CAMERA_INTRO_3_2DAZED,
      ANIM3D_CAMERA_INTRO_SLIDING,
      ANIM3D_CAMERA_TEST,
      ANIM3D_CAMERA_PROTOTYPE_INTRO,
      ANIM3D_CAMERA_UI,
      ANIM3D_PIGEON_IDLE_01,
      ANIM3D_PIGEON_TAKEOFF,
      ANIM3D_PIGEON_FLYING_LOOP,
    }

    public enum Anim3D_Trial
    {
      ANIM3D_NULL,
      ANIM3D_FAITH_INTRO_JUMPINGSTRETCHING,
      ANIM3D_FAITH_RUN_SLOW,
      ANIM3D_FAITH_RUN_MEDIUM,
      ANIM3D_FAITH_RUN_FAST,
      ANIM3D_FAITH_RUN_UPHILL,
      ANIM3D_FAITH_RUN_STOP_SLIDE,
      ANIM3D_FAITH_RUN_180_SLIDE_START,
      ANIM3D_FAITH_RUN_180_SLIDE_FINSH,
      ANIM3D_FAITH_IDLE,
      ANIM3D_FAITH_JUMP_RISING,
      ANIM3D_FAITH_JUMP_FALLING,
      ANIM3D_FAITH_JUMP_LANDING,
      ANIM3D_FAITH_JUMP_FROM_CLAMBER,
      ANIM3D_FAITH_VERTICAL_RUN,
      ANIM3D_FAITH_WALLRUN_RIGHT_LOOP,
      ANIM3D_FAITH_WALLRUN_LEFT_LOOP,
      ANIM3D_FAITH_SLIDE_START,
      ANIM3D_FAITH_SLIDE_LOOP,
      ANIM3D_FAITH_SLIDE_END,
      ANIM3D_FAITH_EDGE_VAULT,
      ANIM3D_FAITH_CLAMBER_ONE_METRE,
      ANIM3D_FAITH_CLAMBER_ONE_METRE_FROM_STATIC,
      ANIM3D_FAITH_CLAMBER_HALF_METRE,
      ANIM3D_FAITH_CLAMBER_HALF_METRE_FROM_STATIC,
      ANIM3D_FAITH_CLAMBER_FROM_HANG,
      ANIM3D_FAITH_HANG,
      ANIM3D_FAITH_HANG_TO_WALL_SLIDE,
      ANIM3D_FAITH_VERTICLE_WALL_SLIDE,
      ANIM3D_FAITH_VERTICLE_WALL_JUMP,
      ANIM3D_FAITH_WALL_SLIDE_OPTIMAL,
      ANIM3D_FAITH_WALL_SLIDE_LEFT,
      ANIM3D_FAITH_WALL_SLIDE_RIGHT,
      ANIM3D_FAITH_ROLL,
      ANIM3D_FAITH_LAND_BADLY,
      ANIM3D_FAITH_BALANCE_OPTIMAL,
      ANIM3D_FAITH_BALANCE_LEFT,
      ANIM3D_FAITH_BALANCE_RIGHT,
      ANIM3D_FAITH_SWING_IDLE,
      ANIM3D_FAITH_SWING_FORWARD,
      ANIM3D_FAITH_SWING_BACK,
      ANIM3D_FAITH_ZIPLINE_OPTIMAL,
      ANIM3D_FAITH_ZIPLINE_FORWARD,
      ANIM3D_FAITH_ZIPLINE_BACK,
      ANIM3D_FAITH_ZIPLINE_FAIL,
      ANIM3D_FAITH_DYNAMIC_CLIMB,
      ANIM3D_FAITH_DYNAMIC_CLIMB_REVERSE,
      ANIM3D_FAITH_DYNAMIC_CLIMB_REVERSE_HALFSTEP,
      ANIM3D_FAITH_STATIC_CLIMB,
      ANIM3D_FAITH_STATIC_CLIMB_REVERSE,
      ANIM3D_FAITH_POWERJUMP_TRANSITION,
      ANIM3D_FAITH_POWERJUMP_LOOP,
      ANIM3D_FAITH_BALANCE_FALL,
      ANIM3D_FAITH_BALANCE_FALLEN,
      ANIM3D_FAITH_BALANCE_RECLIMB,
      ANIM3D_FAITH_PRONE_IDLE,
      ANIM3D_FAITH_PRONE_RECOVER,
      ANIM3D_FAITH_FLYING_KICK_LOOP,
      ANIM3D_FAITH_FLYING_KICK_IMPACT,
      ANIM3D_FAITH_DEATH_RUNNING,
      ANIM3D_FAITH_ORIGIN_EDGE_VAULT,
      ANIM3D_FAITH_ORIGIN_CLAMBER_ONE_METRE,
      ANIM3D_FAITH_ORIGIN_CLAMBER_ONE_METRE_FROM_STATIC,
      ANIM3D_FAITH_ORIGIN_CLAMBER_HALF_METRE,
      ANIM3D_FAITH_ORIGIN_CLAMBER_HALF_METER_FROM_STATIC,
      ANIM3D_FAITH_ORIGIN_CLAMBER_FROM_HANG,
      ANIM3D_FAITH_ORIGIN_DYNAMIC_CLIMB,
      ANIM3D_FAITH_ORIGIN_DYNAMIC_CLIMB_REVERSE,
      ANIM3D_FAITH_ORIGIN_STATIC_CLIMB,
      ANIM3D_FAITH_ORIGIN_STATIC_CLIMB_REVERSE,
      ANIM3D_CAMERA_INTRO_JUMPINGSTRETCHING,
      ANIM3D_CAMERA_UI,
      ANIM3D_PIGEON_IDLE_01,
      ANIM3D_PIGEON_TAKEOFF,
      ANIM3D_PIGEON_FLYING_LOOP,
    }

    public enum UserIDAnimation_Full
    {
      USERID_ANIMATION_CONTROLLER_FAITH_RUN_SLOW = 524288, // 0x00080000
      USERID_ANIMATION_CONTROLLER_FAITH_RUN_MEDIUM = 524289, // 0x00080001
      USERID_ANIMATION_CONTROLLER_FAITH_RUN_FAST = 524290, // 0x00080002
      USERID_ANIMATION_CONTROLLER_FAITH_RUN_UPHILL = 524291, // 0x00080003
      USERID_ANIMATION_CONTROLLER_FAITH_RUN_STOP_SLIDE = 524292, // 0x00080004
      USERID_ANIMATION_CONTROLLER_FAITH_RUN_180_SLIDE_START = 524293, // 0x00080005
      USERID_ANIMATION_CONTROLLER_FAITH_RUN_180_SLIDE_FINSH = 524294, // 0x00080006
      USERID_ANIMATION_CONTROLLER_FAITH_IDLE = 524295, // 0x00080007
      USERID_ANIMATION_CONTROLLER_FAITH_JUMP_PREJUMP = 524296, // 0x00080008
      USERID_ANIMATION_CONTROLLER_FAITH_JUMP_RISING = 524297, // 0x00080009
      USERID_ANIMATION_CONTROLLER_FAITH_JUMP_FALLING = 524298, // 0x0008000A
      USERID_ANIMATION_CONTROLLER_FAITH_JUMP_LANDING = 524299, // 0x0008000B
      USERID_ANIMATION_CONTROLLER_FAITH_JUMP_FROM_CLAMBER = 524300, // 0x0008000C
      USERID_ANIMATION_CONTROLLER_FAITH_VERTICAL_RUN = 524301, // 0x0008000D
      USERID_ANIMATION_CONTROLLER_FAITH_WALLRUN_RIGHT_START = 524302, // 0x0008000E
      USERID_ANIMATION_CONTROLLER_FAITH_WALLRUN_RIGHT_LOOP = 524303, // 0x0008000F
      USERID_ANIMATION_CONTROLLER_FAITH_WALLRUN_RIGHT_END = 524304, // 0x00080010
      USERID_ANIMATION_CONTROLLER_FAITH_WALLRUN_LEFT_START = 524305, // 0x00080011
      USERID_ANIMATION_CONTROLLER_FAITH_WALLRUN_LEFT_LOOP = 524306, // 0x00080012
      USERID_ANIMATION_CONTROLLER_FAITH_WALLRUN_LEFT_END = 524307, // 0x00080013
      USERID_ANIMATION_CONTROLLER_FAITH_SLIDE_START = 524308, // 0x00080014
      USERID_ANIMATION_CONTROLLER_FAITH_SLIDE_LOOP = 524309, // 0x00080015
      USERID_ANIMATION_CONTROLLER_FAITH_SLIDE_END = 524310, // 0x00080016
      USERID_ANIMATION_CONTROLLER_FAITH_EDGE_VAULT = 524311, // 0x00080017
      USERID_ANIMATION_CONTROLLER_FAITH_CLAMBER_HALF_METRE = 524312, // 0x00080018
      USERID_ANIMATION_CONTROLLER_FAITH_CLAMBER_ONE_METRE = 524313, // 0x00080019
      USERID_ANIMATION_CONTROLLER_FAITH_CLAMBER_FROM_HANG = 524314, // 0x0008001A
      USERID_ANIMATION_CONTROLLER_FAITH_CLAMBER_HALF_METRE_FROM_STATIC = 524315, // 0x0008001B
      USERID_ANIMATION_CONTROLLER_FAITH_CLAMBER_ONE_METRE_FROM_STATIC = 524316, // 0x0008001C
      USERID_ANIMATION_CONTROLLER_FAITH_VERTICLE_WALL_SLIDE = 524317, // 0x0008001D
      USERID_ANIMATION_CONTROLLER_FAITH_VERTICLE_WALL_JUMP = 524318, // 0x0008001E
      USERID_ANIMATION_CONTROLLER_FAITH_WALL_SLIDE_OPTIMAL = 524319, // 0x0008001F
      USERID_ANIMATION_CONTROLLER_FAITH_WALL_SLIDE_LEFT = 524320, // 0x00080020
      USERID_ANIMATION_CONTROLLER_FAITH_WALL_SLIDE_RIGHT = 524321, // 0x00080021
      USERID_ANIMATION_CONTROLLER_FAITH_ROLL = 524322, // 0x00080022
      USERID_ANIMATION_CONTROLLER_FAITH_LAND_BADLY = 524323, // 0x00080023
      USERID_ANIMATION_CONTROLLER_FAITH_BALANCE_OPTIMAL = 524324, // 0x00080024
      USERID_ANIMATION_CONTROLLER_FAITH_BALANCE_LEFT = 524325, // 0x00080025
      USERID_ANIMATION_CONTROLLER_FAITH_BALANCE_RIGHT = 524326, // 0x00080026
      USERID_ANIMATION_CONTROLLER_FAITH_SWING_IDLE = 524327, // 0x00080027
      USERID_ANIMATION_CONTROLLER_FAITH_SWING_FORWARD = 524328, // 0x00080028
      USERID_ANIMATION_CONTROLLER_FAITH_SWING_BACK = 524329, // 0x00080029
      USERID_ANIMATION_CONTROLLER_FAITH_ZIPLINE_OPTIMAL = 524330, // 0x0008002A
      USERID_ANIMATION_CONTROLLER_FAITH_ZIPLINE_FORWARD = 524331, // 0x0008002B
      USERID_ANIMATION_CONTROLLER_FAITH_ZIPLINE_BACK = 524332, // 0x0008002C
      USERID_ANIMATION_CONTROLLER_FAITH_ZIPLINE_FAIL = 524333, // 0x0008002D
      USERID_ANIMATION_CONTROLLER_FAITH_HANG = 524334, // 0x0008002E
      USERID_ANIMATION_CONTROLLER_FAITH_HANG_TO_WALL_SLIDE = 524335, // 0x0008002F
      USERID_ANIMATION_CONTROLLER_FAITH_DYNAMIC_CLIMB = 524336, // 0x00080030
      USERID_ANIMATION_CONTROLLER_FAITH_DYNAMIC_CLIMB_REVERSE = 524337, // 0x00080031
      USERID_ANIMATION_CONTROLLER_FAITH_DYNAMIC_CLIMB_REVERSE_HALFSTEP = 524338, // 0x00080032
      USERID_ANIMATION_CONTROLLER_FAITH_STATIC_CLIMB_REVERSE = 524339, // 0x00080033
      USERID_ANIMATION_CONTROLLER_FAITH_STATIC_CLIMB = 524340, // 0x00080034
      USERID_ANIMATION_CONTROLLER_FAITH_POWERJUMP_JUMP = 524341, // 0x00080035
      USERID_ANIMATION_CONTROLLER_FAITH_POWERJUMP_TRANSITION = 524342, // 0x00080036
      USERID_ANIMATION_CONTROLLER_FAITH_POWERJUMP_LOOP = 524343, // 0x00080037
      USERID_ANIMATION_CONTROLLER_FAITH_BALANCE_FALL = 524344, // 0x00080038
      USERID_ANIMATION_CONTROLLER_FAITH_BALANCE_FALLEN = 524345, // 0x00080039
      USERID_ANIMATION_CONTROLLER_FAITH_BALANCE_RECLIMB = 524346, // 0x0008003A
      USERID_ANIMATION_CONTROLLER_FAITH_MELEE_SUCCESS = 524347, // 0x0008003B
      USERID_ANIMATION_CONTROLLER_FAITH_MELEE_FAIL = 524348, // 0x0008003C
      USERID_ANIMATION_CONTROLLER_FAITH_MELEE_GETUP = 524349, // 0x0008003D
      USERID_ANIMATION_CONTROLLER_FAITH_PRONE_IDLE = 524350, // 0x0008003E
      USERID_ANIMATION_CONTROLLER_FAITH_PRONE_RECOVER = 524351, // 0x0008003F
      USERID_ANIMATION_CONTROLLER_FAITH_FLYING_KICK = 524352, // 0x00080040
      USERID_ANIMATION_CONTROLLER_FAITH_FLYING_KICK_LOOP = 524353, // 0x00080041
      USERID_ANIMATION_CONTROLLER_FAITH_FLYING_KICK_IMPACT = 524354, // 0x00080042
      USERID_ANIMATION_CONTROLLER_FAITH_DEATH_RUNNING = 524355, // 0x00080043
      USERID_ANIMATION_CONTROLLER_FAITH_INTRO_JUMPINGSTRETCHING = 524356, // 0x00080044
      USERID_ANIMATION_CONTROLLER_FAITH_INTRO_LANDING = 524357, // 0x00080045
      USERID_ANIMATION_CONTROLLER_FAITH_INTRO_2_1CAUTIOUS = 524358, // 0x00080046
      USERID_ANIMATION_CONTROLLER_FAITH_INTRO_3_2DAZED = 524359, // 0x00080047
      USERID_ANIMATION_CONTROLLER_POLICE_IDLE = 589824, // 0x00090000
      USERID_ANIMATION_CONTROLLER_POLICE_DRAW_PISTOL = 589825, // 0x00090001
      USERID_ANIMATION_CONTROLLER_POLICE_LOWER_PISTOL = 589826, // 0x00090002
      USERID_ANIMATION_CONTROLLER_POLICE_LOWERED_PISTOL = 589827, // 0x00090003
      USERID_ANIMATION_CONTROLLER_POLICE_AIM_PISTOL = 589828, // 0x00090004
      USERID_ANIMATION_CONTROLLER_POLICE_AIM_HOLD = 589829, // 0x00090005
      USERID_ANIMATION_CONTROLLER_POLICE_FIRE_PISTOL = 589830, // 0x00090006
      USERID_ANIMATION_CONTROLLER_POLICE_HOLSTER_PISTOL = 589831, // 0x00090007
      USERID_ANIMATION_CONTROLLER_POLICE_CROUCH = 589832, // 0x00090008
      USERID_ANIMATION_CONTROLLER_POLICE_CROUCH_LOWER_PISTOL = 589833, // 0x00090009
      USERID_ANIMATION_CONTROLLER_POLICE_CROUCH_LOWERED_PISTOL = 589834, // 0x0009000A
      USERID_ANIMATION_CONTROLLER_POLICE_CROUCH_AIM_PISTOL = 589835, // 0x0009000B
      USERID_ANIMATION_CONTROLLER_POLICE_CROUCH_AIM_HOLD = 589836, // 0x0009000C
      USERID_ANIMATION_CONTROLLER_POLICE_CROUCH_FIRE = 589837, // 0x0009000D
      USERID_ANIMATION_CONTROLLER_POLICE_CROUCH_STAND = 589838, // 0x0009000E
      USERID_ANIMATION_CONTROLLER_POLICE_KNOCKDOWN_FORWARDS_LOW = 589839, // 0x0009000F
      USERID_ANIMATION_CONTROLLER_POLICE_KNOCKDOWN_FORWARDS_MEDIUM = 589840, // 0x00090010
      USERID_ANIMATION_CONTROLLER_POLICE_KNOCKDOWN_FORWARDS_HIGH = 589841, // 0x00090011
      USERID_ANIMATION_CONTROLLER_POLICE_KNOCKDOWN_BACKWARDS_LOW = 589842, // 0x00090012
      USERID_ANIMATION_CONTROLLER_POLICE_KNOCKDOWN_BACKWARDS_MEDIUM = 589843, // 0x00090013
      USERID_ANIMATION_CONTROLLER_POLICE_KNOCKDOWN_BACKWARDS_HIGH = 589844, // 0x00090014
      USERID_ANIMATION_CONTROLLER_POLICE_KNOCKDOWN_IDLE_FORWARDS = 589845, // 0x00090015
      USERID_ANIMATION_CONTROLLER_POLICE_KNOCKDOWN_IDLE_BACKWARDS = 589846, // 0x00090016
      USERID_ANIMATION_CONTROLLER_POLICE_KNOCKDOWN_STAND_FORWARDS = 589847, // 0x00090017
      USERID_ANIMATION_CONTROLLER_POLICE_KNOCKDOWN_STAND_BACKWARDS = 589848, // 0x00090018
      USERID_ANIMATION_CONTROLLER_POLICE_RUN = 589849, // 0x00090019
      USERID_ANIMATION_CONTROLLER_POLICE_WALK = 589850, // 0x0009001A
      USERID_ANIMATION_CONTROLLER_POLICE_MELEE_SUCCESS = 589851, // 0x0009001B
      USERID_ANIMATION_CONTROLLER_POLICE_MELEE_FAIL = 589852, // 0x0009001C
      USERID_ANIMATION_CONTROLLER_POLICE_MELEE_GETUP = 589853, // 0x0009001D
      USERID_ANIMATION_CONTROLLER_POLICE_FALL_BACKWARDS = 589854, // 0x0009001E
      USERID_ANIMATION_CONTROLLER_POLICE_LAND_BACKWARDS = 589855, // 0x0009001F
      USERID_ANIMATION_CONTROLLER_POLICE_FALL_FORWARDS = 589856, // 0x00090020
      USERID_ANIMATION_CONTROLLER_POLICE_LAND_FORWARDS = 589857, // 0x00090021
    }

    public enum UserIDAnimation_Trial
    {
      USERID_ANIMATION_CONTROLLER_FAITH_RUN_SLOW = 524288, // 0x00080000
      USERID_ANIMATION_CONTROLLER_FAITH_RUN_MEDIUM = 524289, // 0x00080001
      USERID_ANIMATION_CONTROLLER_FAITH_RUN_FAST = 524290, // 0x00080002
      USERID_ANIMATION_CONTROLLER_FAITH_RUN_UPHILL = 524291, // 0x00080003
      USERID_ANIMATION_CONTROLLER_FAITH_RUN_STOP_SLIDE = 524292, // 0x00080004
      USERID_ANIMATION_CONTROLLER_FAITH_RUN_180_SLIDE_START = 524293, // 0x00080005
      USERID_ANIMATION_CONTROLLER_FAITH_RUN_180_SLIDE_FINSH = 524294, // 0x00080006
      USERID_ANIMATION_CONTROLLER_FAITH_IDLE = 524295, // 0x00080007
      USERID_ANIMATION_CONTROLLER_FAITH_JUMP_RISING = 524296, // 0x00080008
      USERID_ANIMATION_CONTROLLER_FAITH_JUMP_FALLING = 524297, // 0x00080009
      USERID_ANIMATION_CONTROLLER_FAITH_JUMP_LANDING = 524298, // 0x0008000A
      USERID_ANIMATION_CONTROLLER_FAITH_JUMP_FROM_CLAMBER = 524299, // 0x0008000B
      USERID_ANIMATION_CONTROLLER_FAITH_VERTICAL_RUN = 524300, // 0x0008000C
      USERID_ANIMATION_CONTROLLER_FAITH_WALLRUN_RIGHT_LOOP = 524301, // 0x0008000D
      USERID_ANIMATION_CONTROLLER_FAITH_WALLRUN_LEFT_LOOP = 524302, // 0x0008000E
      USERID_ANIMATION_CONTROLLER_FAITH_SLIDE_START = 524303, // 0x0008000F
      USERID_ANIMATION_CONTROLLER_FAITH_SLIDE_LOOP = 524304, // 0x00080010
      USERID_ANIMATION_CONTROLLER_FAITH_SLIDE_END = 524305, // 0x00080011
      USERID_ANIMATION_CONTROLLER_FAITH_EDGE_VAULT = 524306, // 0x00080012
      USERID_ANIMATION_CONTROLLER_FAITH_CLAMBER_HALF_METRE = 524307, // 0x00080013
      USERID_ANIMATION_CONTROLLER_FAITH_CLAMBER_ONE_METRE = 524308, // 0x00080014
      USERID_ANIMATION_CONTROLLER_FAITH_CLAMBER_FROM_HANG = 524309, // 0x00080015
      USERID_ANIMATION_CONTROLLER_FAITH_CLAMBER_HALF_METRE_FROM_STATIC = 524310, // 0x00080016
      USERID_ANIMATION_CONTROLLER_FAITH_CLAMBER_ONE_METRE_FROM_STATIC = 524311, // 0x00080017
      USERID_ANIMATION_CONTROLLER_FAITH_VERTICLE_WALL_SLIDE = 524312, // 0x00080018
      USERID_ANIMATION_CONTROLLER_FAITH_VERTICLE_WALL_JUMP = 524313, // 0x00080019
      USERID_ANIMATION_CONTROLLER_FAITH_WALL_SLIDE_OPTIMAL = 524314, // 0x0008001A
      USERID_ANIMATION_CONTROLLER_FAITH_WALL_SLIDE_LEFT = 524315, // 0x0008001B
      USERID_ANIMATION_CONTROLLER_FAITH_WALL_SLIDE_RIGHT = 524316, // 0x0008001C
      USERID_ANIMATION_CONTROLLER_FAITH_ROLL = 524317, // 0x0008001D
      USERID_ANIMATION_CONTROLLER_FAITH_LAND_BADLY = 524318, // 0x0008001E
      USERID_ANIMATION_CONTROLLER_FAITH_BALANCE_OPTIMAL = 524319, // 0x0008001F
      USERID_ANIMATION_CONTROLLER_FAITH_BALANCE_LEFT = 524320, // 0x00080020
      USERID_ANIMATION_CONTROLLER_FAITH_BALANCE_RIGHT = 524321, // 0x00080021
      USERID_ANIMATION_CONTROLLER_FAITH_SWING_IDLE = 524322, // 0x00080022
      USERID_ANIMATION_CONTROLLER_FAITH_SWING_FORWARD = 524323, // 0x00080023
      USERID_ANIMATION_CONTROLLER_FAITH_SWING_BACK = 524324, // 0x00080024
      USERID_ANIMATION_CONTROLLER_FAITH_ZIPLINE_OPTIMAL = 524325, // 0x00080025
      USERID_ANIMATION_CONTROLLER_FAITH_ZIPLINE_FORWARD = 524326, // 0x00080026
      USERID_ANIMATION_CONTROLLER_FAITH_ZIPLINE_BACK = 524327, // 0x00080027
      USERID_ANIMATION_CONTROLLER_FAITH_ZIPLINE_FAIL = 524328, // 0x00080028
      USERID_ANIMATION_CONTROLLER_FAITH_HANG = 524329, // 0x00080029
      USERID_ANIMATION_CONTROLLER_FAITH_HANG_TO_WALL_SLIDE = 524330, // 0x0008002A
      USERID_ANIMATION_CONTROLLER_FAITH_DYNAMIC_CLIMB = 524331, // 0x0008002B
      USERID_ANIMATION_CONTROLLER_FAITH_DYNAMIC_CLIMB_REVERSE = 524332, // 0x0008002C
      USERID_ANIMATION_CONTROLLER_FAITH_DYNAMIC_CLIMB_REVERSE_HALFSTEP = 524333, // 0x0008002D
      USERID_ANIMATION_CONTROLLER_FAITH_STATIC_CLIMB_REVERSE = 524334, // 0x0008002E
      USERID_ANIMATION_CONTROLLER_FAITH_STATIC_CLIMB = 524335, // 0x0008002F
      USERID_ANIMATION_CONTROLLER_FAITH_POWERJUMP_TRANSITION = 524336, // 0x00080030
      USERID_ANIMATION_CONTROLLER_FAITH_POWERJUMP_LOOP = 524337, // 0x00080031
      USERID_ANIMATION_CONTROLLER_FAITH_BALANCE_FALL = 524338, // 0x00080032
      USERID_ANIMATION_CONTROLLER_FAITH_BALANCE_FALLEN = 524339, // 0x00080033
      USERID_ANIMATION_CONTROLLER_FAITH_BALANCE_RECLIMB = 524340, // 0x00080034
      USERID_ANIMATION_CONTROLLER_FAITH_PRONE_IDLE = 524341, // 0x00080035
      USERID_ANIMATION_CONTROLLER_FAITH_PRONE_RECOVER = 524342, // 0x00080036
      USERID_ANIMATION_CONTROLLER_FAITH_FLYING_KICK_LOOP = 524343, // 0x00080037
      USERID_ANIMATION_CONTROLLER_FAITH_FLYING_KICK_IMPACT = 524344, // 0x00080038
      USERID_ANIMATION_CONTROLLER_FAITH_DEATH_RUNNING = 524345, // 0x00080039
      USERID_ANIMATION_CONTROLLER_FAITH_INTRO_JUMPINGSTRETCHING = 524346, // 0x0008003A
    }

    public enum ChannelFaith_Full
    {
      CHANNEL_FAITH_RUN_SLOW,
      CHANNEL_FAITH_RUN_MEDIUM,
      CHANNEL_FAITH_RUN_FAST,
      CHANNEL_FAITH_RUN_UPHILL,
      CHANNEL_FAITH_RUN_STOP_SLIDE,
      CHANNEL_FAITH_RUN_180_SLIDE_START,
      CHANNEL_FAITH_RUN_180_SLIDE_FINSH,
      CHANNEL_FAITH_IDLE,
      CHANNEL_FAITH_JUMP_PREJUMP,
      CHANNEL_FAITH_JUMP_RISING,
      CHANNEL_FAITH_JUMP_FALLING,
      CHANNEL_FAITH_JUMP_LANDING,
      CHANNEL_FAITH_JUMP_FROM_CLAMBER,
      CHANNEL_FAITH_VERTICAL_RUN,
      CHANNEL_FAITH_WALLRUN_RIGHT_START,
      CHANNEL_FAITH_WALLRUN_RIGHT_LOOP,
      CHANNEL_FAITH_WALLRUN_RIGHT_END,
      CHANNEL_FAITH_WALLRUN_LEFT_START,
      CHANNEL_FAITH_WALLRUN_LEFT_LOOP,
      CHANNEL_FAITH_WALLRUN_LEFT_END,
      CHANNEL_FAITH_SLIDE_START,
      CHANNEL_FAITH_SLIDE_LOOP,
      CHANNEL_FAITH_SLIDE_END,
      CHANNEL_FAITH_EDGE_VAULT,
      CHANNEL_FAITH_CLAMBER_HALF_METRE,
      CHANNEL_FAITH_CLAMBER_ONE_METRE,
      CHANNEL_FAITH_CLAMBER_FROM_HANG,
      CHANNEL_FAITH_CLAMBER_HALF_METRE_FROM_STATIC,
      CHANNEL_FAITH_CLAMBER_ONE_METRE_FROM_STATIC,
      CHANNEL_FAITH_WALLSLIDE_VERTICAL,
      CHANNEL_FAITH_WALLJUMP_VERTICAL,
      CHANNEL_FAITH_WALLSLIDE_OPTIMAL,
      CHANNEL_FAITH_WALLSLIDE_LEFT,
      CHANNEL_FAITH_WALLSLIDE_RIGHT,
      CHANNEL_FAITH_ROLL,
      CHANNEL_FAITH_LAND_BADLY,
      CHANNEL_FAITH_BALANCE_OPTIMAL,
      CHANNEL_FAITH_BALANCE_LEFT,
      CHANNEL_FAITH_BALANCE_RIGHT,
      CHANNEL_FAITH_SWING_IDLE,
      CHANNEL_FAITH_SWING_FORWARD,
      CHANNEL_FAITH_SWING_BACK,
      CHANNEL_FAITH_ZIPLINE_OPTIMAL,
      CHANNEL_FAITH_ZIPLINE_FORWARD,
      CHANNEL_FAITH_ZIPLINE_BACK,
      CHANNEL_FAITH_ZIPLINE_FAIL,
      CHANNEL_FAITH_HANG,
      CHANNEL_FAITH_HANG_TO_WALL_SLIDE,
      CHANNEL_FAITH_DYNAMIC_CLIMB,
      CHANNEL_FAITH_DYNAMIC_CLIMB_REVERSE,
      CHANNEL_FAITH_DYNAMIC_CLIMB_REVERSE_HALFSTEP,
      CHANNEL_FAITH_STATIC_CLIMB_REVERSE,
      CHANNEL_FAITH_STATIC_CLIMB,
      CHANNEL_FAITH_POWERJUMP_JUMP,
      CHANNEL_FAITH_POWERJUMP_TRANSITION,
      CHANNEL_FAITH_POWERJUMP_LOOP,
      CHANNEL_FAITH_BALANCE_FALL,
      CHANNEL_FAITH_BALANCE_FALLEN,
      CHANNEL_FAITH_BALANCE_RECLIMB,
      CHANNEL_FAITH_MELEE_SUCCESS,
      CHANNEL_FAITH_MELEE_FAIL,
      CHANNEL_FAITH_MELEE_GETUP,
      CHANNEL_FAITH_PRONE_IDLE,
      CHANNEL_FAITH_PRONE_RECOVER,
      CHANNEL_FAITH_FLYING_KICK,
      CHANNEL_FAITH_FLYING_KICK_LOOP,
      CHANNEL_FAITH_FLYING_KICK_IMPACT,
      CHANNEL_FAITH_DEATH_RUNNING,
      CHANNEL_FAITH_INTRO_JUMPSTRETCHING,
      CHANNEL_FAITH_INTRO_LANDING,
      CHANNEL_FAITH_INTRO_2_1CAUTIOUS,
      CHANNEL_FAITH_INTRO_3_2DAZED,
    }

    public enum ChannelFaith_Trial
    {
      CHANNEL_FAITH_RUN_SLOW,
      CHANNEL_FAITH_RUN_MEDIUM,
      CHANNEL_FAITH_RUN_FAST,
      CHANNEL_FAITH_RUN_UPHILL,
      CHANNEL_FAITH_RUN_STOP_SLIDE,
      CHANNEL_FAITH_RUN_180_SLIDE_START,
      CHANNEL_FAITH_RUN_180_SLIDE_FINSH,
      CHANNEL_FAITH_IDLE,
      CHANNEL_FAITH_JUMP_RISING,
      CHANNEL_FAITH_JUMP_FALLING,
      CHANNEL_FAITH_JUMP_LANDING,
      CHANNEL_FAITH_JUMP_FROM_CLAMBER,
      CHANNEL_FAITH_VERTICAL_RUN,
      CHANNEL_FAITH_WALLRUN_RIGHT_LOOP,
      CHANNEL_FAITH_WALLRUN_LEFT_LOOP,
      CHANNEL_FAITH_SLIDE_START,
      CHANNEL_FAITH_SLIDE_LOOP,
      CHANNEL_FAITH_SLIDE_END,
      CHANNEL_FAITH_EDGE_VAULT,
      CHANNEL_FAITH_CLAMBER_HALF_METRE,
      CHANNEL_FAITH_CLAMBER_ONE_METRE,
      CHANNEL_FAITH_CLAMBER_FROM_HANG,
      CHANNEL_FAITH_CLAMBER_HALF_METRE_FROM_STATIC,
      CHANNEL_FAITH_CLAMBER_ONE_METRE_FROM_STATIC,
      CHANNEL_FAITH_WALLSLIDE_VERTICAL,
      CHANNEL_FAITH_WALLJUMP_VERTICAL,
      CHANNEL_FAITH_WALLSLIDE_OPTIMAL,
      CHANNEL_FAITH_WALLSLIDE_LEFT,
      CHANNEL_FAITH_WALLSLIDE_RIGHT,
      CHANNEL_FAITH_ROLL,
      CHANNEL_FAITH_LAND_BADLY,
      CHANNEL_FAITH_BALANCE_OPTIMAL,
      CHANNEL_FAITH_BALANCE_LEFT,
      CHANNEL_FAITH_BALANCE_RIGHT,
      CHANNEL_FAITH_SWING_IDLE,
      CHANNEL_FAITH_SWING_FORWARD,
      CHANNEL_FAITH_SWING_BACK,
      CHANNEL_FAITH_ZIPLINE_OPTIMAL,
      CHANNEL_FAITH_ZIPLINE_FORWARD,
      CHANNEL_FAITH_ZIPLINE_BACK,
      CHANNEL_FAITH_ZIPLINE_FAIL,
      CHANNEL_FAITH_HANG,
      CHANNEL_FAITH_HANG_TO_WALL_SLIDE,
      CHANNEL_FAITH_DYNAMIC_CLIMB,
      CHANNEL_FAITH_DYNAMIC_CLIMB_REVERSE,
      CHANNEL_FAITH_DYNAMIC_CLIMB_REVERSE_HALFSTEP,
      CHANNEL_FAITH_STATIC_CLIMB_REVERSE,
      CHANNEL_FAITH_STATIC_CLIMB,
      CHANNEL_FAITH_POWERJUMP_TRANSITION,
      CHANNEL_FAITH_POWERJUMP_LOOP,
      CHANNEL_FAITH_BALANCE_FALL,
      CHANNEL_FAITH_BALANCE_FALLEN,
      CHANNEL_FAITH_BALANCE_RECLIMB,
      CHANNEL_FAITH_PRONE_IDLE,
      CHANNEL_FAITH_PRONE_RECOVER,
      CHANNEL_FAITH_FLYING_KICK_LOOP,
      CHANNEL_FAITH_FLYING_KICK_IMPACT,
      CHANNEL_FAITH_DEATH_RUNNING,
      CHANNEL_FAITH_INTRO_JUMPSTRETCHING,
    }

    public enum ChannelPolice_Full
    {
      CHANNEL_POLICE_IDLE,
      CHANNEL_POLICE_DRAW_PISTOL,
      CHANNEL_POLICE_LOWER_PISTOL,
      CHANNEL_POLICE_LOWERED_PISTOL,
      CHANNEL_POLICE_AIM_PISTOL,
      CHANNEL_POLICE_AIM_HOLD,
      CHANNEL_POLICE_FIRE_PISTOL,
      CHANNEL_POLICE_HOLSTER_PISTOL,
      CHANNEL_POLICE_CROUCH,
      CHANNEL_POLICE_CROUCH_LOWER_PISTOL,
      CHANNEL_POLICE_CROUCH_LOWERED_PISTOL,
      CHANNEL_POLICE_CROUCH_AIM_PISTOL,
      CHANNEL_POLICE_CROUCH_AIM_HOLD,
      CHANNEL_POLICE_CROUCH_FIRE,
      CHANNEL_POLICE_CROUCH_STAND,
      CHANNEL_POLICE_KNOCKDOWN_FORWARDS_LOW,
      CHANNEL_POLICE_KNOCKDOWN_FORWARDS_MEDIUM,
      CHANNEL_POLICE_KNOCKDOWN_FORWARDS_HIGH,
      CHANNEL_POLICE_KNOCKDOWN_BACKWARDS_LOW,
      CHANNEL_POLICE_KNOCKDOWN_BACKWARDS_MEDIUM,
      CHANNEL_POLICE_KNOCKDOWN_BACKWARDS_HIGH,
      CHANNEL_POLICE_KNOCKDOWN_IDLE_FORWARDS,
      CHANNEL_POLICE_KNOCKDOWN_IDLE_BACKWARDS,
      CHANNEL_POLICE_KNOCKDOWN_STAND_FORWARDS,
      CHANNEL_POLICE_KNOCKDOWN_STAND_BACKWARDS,
      CHANNEL_POLICE_RUN,
      CHANNEL_POLICE_WALK,
      CHANNEL_POLICE_MELEE_SUCCESS,
      CHANNEL_POLICE_MELEE_FAIL,
      CHANNEL_POLICE_MELEE_GETUP,
      CHANNEL_POLICE_FALL_BACKWARDS,
      CHANNEL_POLICE_LAND_BACKWARDS,
      CHANNEL_POLICE_FALL_FORWARDS,
      CHANNEL_POLICE_LAND_FORWARDS,
    }
  }
}
