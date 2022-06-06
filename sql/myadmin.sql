/*
 Navicat Premium Data Transfer

 Source Server         : 127.0.0.1
 Source Server Type    : MySQL
 Source Server Version : 50727
 Source Host           : localhost:3306
 Source Schema         : myadmin

 Target Server Type    : MySQL
 Target Server Version : 50727
 File Encoding         : 65001

 Date: 06/06/2022 16:14:16
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for t_api
-- ----------------------------
DROP TABLE IF EXISTS `t_api`;
CREATE TABLE `t_api`  (
  `FId` bigint(20) NOT NULL COMMENT '主键编码',
  `FParentId` bigint(20) NULL DEFAULT NULL COMMENT '父级节点主键编码',
  `FTitle` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '标题',
  `FPath` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '地址',
  `FAction` varchar(16) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '请求类型',
  `FCreatedUserId` bigint(20) NULL DEFAULT NULL COMMENT '创建者Id',
  `FCreatedUserName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '创建者',
  `FCreatedTime` datetime(3) NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `FModifiedUserId` bigint(20) NULL DEFAULT NULL COMMENT '修改者Id',
  `FModifiedUserName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '修改者',
  `FModifiedTime` datetime(3) NULL DEFAULT CURRENT_TIMESTAMP(3) ON UPDATE CURRENT_TIMESTAMP(3) COMMENT '修改时间',
  PRIMARY KEY (`FId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for t_casbin_rule
-- ----------------------------
DROP TABLE IF EXISTS `t_casbin_rule`;
CREATE TABLE `t_casbin_rule`  (
  `FId` int(11) NOT NULL AUTO_INCREMENT,
  `FPtype` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `FV0` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `FV1` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `FV2` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `FV3` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `FV4` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `FV5` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `FCreatedUserId` bigint(20) NULL DEFAULT NULL COMMENT '创建者Id',
  `FCreatedUserName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '创建者',
  `FCreatedTime` datetime(3) NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  PRIMARY KEY (`FId`) USING BTREE,
  UNIQUE INDEX `idx_t_casbin_rule_01`(`FPtype`, `FV0`, `FV1`, `FV2`, `FV3`, `FV4`, `FV5`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 117 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for t_menu
-- ----------------------------
DROP TABLE IF EXISTS `t_menu`;
CREATE TABLE `t_menu`  (
  `FId` bigint(20) NOT NULL COMMENT '主键Id',
  `FParentId` bigint(20) NOT NULL COMMENT '父级节点',
  `FLabel` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '权限名称',
  `FIcon` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '图标',
  `FPermisson` varchar(550) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '权限编码(比如admin:sysRole:list',
  `FType` int(11) NOT NULL COMMENT '权限类型(M,C,F)',
  `FPath` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '菜单主地址(/admin/sys-role)',
  `FIdPath` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '父子级主键ID拼接(/0/2/52',
  `FComponent` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '菜单访问的实际页面地址(/admin/sys-role/index)',
  `FAction` varchar(16) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '请求类型(POST,GET)',
  `FHidden` bit(1) NOT NULL COMMENT '隐藏',
  `FEnabled` bit(1) NOT NULL COMMENT '启用',
  `FClosable` bit(1) NULL DEFAULT NULL COMMENT '可关闭',
  `FOpened` bit(1) NULL DEFAULT NULL COMMENT '打开组',
  `FNewWindow` bit(1) NULL DEFAULT NULL COMMENT '打开新窗口',
  `FExternal` bit(1) NULL DEFAULT NULL COMMENT '链接外显',
  `FSort` int(11) NULL DEFAULT NULL COMMENT '排序',
  `FDescription` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '描述',
  `FIsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  `FCreatedUserId` bigint(20) NULL DEFAULT NULL COMMENT '创建者Id',
  `FCreatedUserName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '创建者',
  `FCreatedTime` datetime(3) NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `FModifiedUserId` bigint(20) NULL DEFAULT NULL COMMENT '修改者Id',
  `FModifiedUserName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '修改者',
  `FModifiedTime` datetime(3) NULL DEFAULT CURRENT_TIMESTAMP(3) ON UPDATE CURRENT_TIMESTAMP(3) COMMENT '修改时间',
  PRIMARY KEY (`FId`) USING BTREE,
  UNIQUE INDEX `idx_t_menu_01`(`FParentId`, `FLabel`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '权限' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for t_menu_api_rule
-- ----------------------------
DROP TABLE IF EXISTS `t_menu_api_rule`;
CREATE TABLE `t_menu_api_rule`  (
  `FMenuId` bigint(20) NOT NULL,
  `FApiId` bigint(20) NOT NULL COMMENT '主键编码',
  `FCreatedUserId` bigint(20) NULL DEFAULT NULL COMMENT '创建者Id',
  `FCreatedUserName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '创建者',
  `FCreatedTime` datetime(3) NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  PRIMARY KEY (`FMenuId`, `FApiId`) USING BTREE,
  INDEX `idx_t_menu_api_rule_01`(`FMenuId`, `FApiId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for t_organization
-- ----------------------------
DROP TABLE IF EXISTS `t_organization`;
CREATE TABLE `t_organization`  (
  `FId` bigint(20) NOT NULL COMMENT '主键Id',
  `FParentId` bigint(20) NOT NULL COMMENT '父级',
  `FName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '名称',
  `FCode` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '编码',
  `FValue` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '值',
  `FPrimaryEmployeeId` bigint(20) NULL DEFAULT NULL COMMENT '主管Id',
  `FEmployeeCount` int(11) NOT NULL COMMENT '员工人数',
  `FDescription` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '描述',
  `FEnabled` bit(1) NOT NULL COMMENT '启用',
  `FSort` int(11) NOT NULL COMMENT '排序',
  `FIsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  `FCreatedUserId` bigint(20) NULL DEFAULT NULL COMMENT '创建者Id',
  `FCreatedUserName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '创建者',
  `FCreatedTime` datetime(3) NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `FModifiedUserId` bigint(20) NULL DEFAULT NULL COMMENT '修改者Id',
  `FModifiedUserName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '修改者',
  `FModifiedTime` datetime(3) NULL DEFAULT CURRENT_TIMESTAMP(3) ON UPDATE CURRENT_TIMESTAMP(3) COMMENT '修改时间',
  PRIMARY KEY (`FId`) USING BTREE,
  UNIQUE INDEX `idx_t_organization_01`(`FParentId`, `FName`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '组织架构' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for t_role
-- ----------------------------
DROP TABLE IF EXISTS `t_role`;
CREATE TABLE `t_role`  (
  `FId` bigint(20) NOT NULL COMMENT '主键Id',
  `FName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '名称',
  `FCode` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '编码',
  `FDescription` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '说明',
  `FEnabled` bit(1) NOT NULL COMMENT '启用',
  `FDataScope` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '数据范围',
  `FSort` int(11) NOT NULL COMMENT '排序',
  `FIsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  `FCreatedUserId` bigint(20) NULL DEFAULT NULL COMMENT '创建者Id',
  `FCreatedUserName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '创建者',
  `FCreatedTime` datetime(3) NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `FModifiedUserId` bigint(20) NULL DEFAULT NULL COMMENT '修改者Id',
  `FModifiedUserName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '修改者',
  `FModifiedTime` datetime(3) NULL DEFAULT CURRENT_TIMESTAMP(3) ON UPDATE CURRENT_TIMESTAMP(3) COMMENT '修改时间',
  PRIMARY KEY (`FId`) USING BTREE,
  UNIQUE INDEX `idx_t_role_01`(`FName`) USING BTREE,
  UNIQUE INDEX `idx_t_role_02`(`FCode`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '角色' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for t_role_menu
-- ----------------------------
DROP TABLE IF EXISTS `t_role_menu`;
CREATE TABLE `t_role_menu`  (
  `FRoleId` bigint(20) NOT NULL,
  `FMenuId` bigint(20) NOT NULL,
  `CreatedUserId` bigint(20) NULL DEFAULT NULL COMMENT '创建者Id',
  `CreatedUserName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '创建者',
  `CreatedTime` datetime(3) NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  PRIMARY KEY (`FRoleId`, `FMenuId`) USING BTREE,
  INDEX `idx_t_role_menu_01`(`FMenuId`, `FRoleId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for t_user
-- ----------------------------
DROP TABLE IF EXISTS `t_user`;
CREATE TABLE `t_user`  (
  `FId` bigint(20) NOT NULL COMMENT '主键Id',
  `FUserName` varchar(60) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '账号',
  `FPassword` varchar(60) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '密码',
  `FNickName` varchar(60) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '昵称',
  `FAvatar` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '头像',
  `FStatus` int(11) NOT NULL COMMENT '状态',
  `FRemark` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '备注',
  `FIsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  `OrganizationId` bigint(20) NOT NULL COMMENT '主属部门Id',
  `FCreatedUserId` bigint(20) NULL DEFAULT NULL COMMENT '创建者Id',
  `FCreatedUserName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '创建者',
  `FCreatedTime` datetime(3) NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `FModifiedUserId` bigint(20) NULL DEFAULT NULL COMMENT '修改者Id',
  `FModifiedUserName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '修改者',
  `FModifiedTime` datetime(3) NULL DEFAULT CURRENT_TIMESTAMP(3) ON UPDATE CURRENT_TIMESTAMP(3) COMMENT '修改时间',
  PRIMARY KEY (`FId`) USING BTREE,
  UNIQUE INDEX `idx_t_user_01`(`FUserName`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '用户' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for t_user_role
-- ----------------------------
DROP TABLE IF EXISTS `t_user_role`;
CREATE TABLE `t_user_role`  (
  `FId` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键Id',
  `FUserId` bigint(20) NOT NULL COMMENT '用户Id',
  `FRoleId` bigint(20) NOT NULL COMMENT '角色Id',
  `FCreatedUserId` bigint(20) NULL DEFAULT NULL COMMENT '创建者Id',
  `FCreatedUserName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '创建者',
  `FCreatedTime` datetime(3) NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  PRIMARY KEY (`FId`) USING BTREE,
  UNIQUE INDEX `idx_t_user_role_01`(`FUserId`, `FRoleId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '用户角色' ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
