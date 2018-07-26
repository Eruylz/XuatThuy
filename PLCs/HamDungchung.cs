﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

/// <summary>
/// Summary description for HamDungchung
/// </summary>
/// 
namespace XuatThuy
{
    public class HamDungchung
    {
        XuatHangDataContext db = new XuatHangDataContext();
        public HamDungchung()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string GetPropertyName<T>(Expression<Func<T>> propertyLambda)
        {
            var me = propertyLambda.Body as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }

            return me.Member.Name;
        }

        public enum Enum_DB100
        {
            HMI_Tam_dung,
            HMI_Nhap_phieu,
            YC_bat_in,
            YC_Tat_in,
            YC_in_them,
            DB_IN1,
            DB_IN2,
            DB_IN3,
            Bang_san_sang,
            NguonOK,
            Bang_dang_chay,
            Dang_In,
            Cho_phep_tat_in,
            OUT1,
            OUT2,
            OUT3,
            Luong_Dat_S1,
            Luong_Dat_S2,
            Luong_Dat_S,
            So_dem1,
            So_dem2,
            Bao_qua_dd,
            So_loi,
            So_phieu,
            TGian_C1,
            TGian_C2,
            Tong_bao,
            Bao_dong,
            IN_So_batdau,
            Luong_in_them

        }
        public enum Enum_DB101
        {
            HMI_Tam_dung,
            HMI_Nhap_phieu,
            YC_bat_in,
            YC_Tat_in,
            YC_in_them,
            DB_IN1,
            DB_IN2,
            DB_IN3,
            Bang_san_sang,
            NguonOK,
            Bang_dang_chay,
            Dang_In,
            Cho_phep_tat_in,
            OUT1,
            OUT2,
            OUT3,
            Luong_Dat_S1,
            Luong_Dat_S2,
            Luong_Dat_S,
            So_dem1,
            So_dem2,
            Bao_qua_dd,
            So_loi,
            So_phieu,
            TGian_C1,
            TGian_C2,
            Tong_bao,
            Bao_dong,
            IN_So_batdau,
            Luong_in_them

        }
        public enum Enum_DB102
        {
            HMI_Tam_dung,
            HMI_Nhap_phieu,
            YC_bat_in,
            YC_Tat_in,
            YC_in_them,
            DB_IN1,
            DB_IN2,
            DB_IN3,
            Bang_san_sang,
            NguonOK,
            Bang_dang_chay,
            Dang_In,
            Cho_phep_tat_in,
            OUT1,
            OUT2,
            OUT3,
            Luong_Dat_S1,
            Luong_Dat_S2,
            Luong_Dat_S,
            So_dem1,
            So_dem2,
            Bao_qua_dd,
            So_loi,
            So_phieu,
            TGian_C1,
            TGian_C2,
            Tong_bao,
            Bao_dong,
            IN_So_batdau,
            Luong_in_them

        }
        public enum Enum_DB103
        {
            HMI_Tam_dung,
            HMI_Nhap_phieu,
            YC_bat_in,
            YC_Tat_in,
            YC_in_them,
            DB_IN1,
            DB_IN2,
            DB_IN3,
            Bang_san_sang,
            NguonOK,
            Bang_dang_chay,
            Dang_In,
            Cho_phep_tat_in,
            OUT1,
            OUT2,
            OUT3,
            Luong_Dat_S1,
            Luong_Dat_S2,
            Luong_Dat_S,
            So_dem1,
            So_dem2,
            Bao_qua_dd,
            So_loi,
            So_phieu,
            TGian_C1,
            TGian_C2,
            Tong_bao,
            Bao_dong,
            IN_So_batdau,
            Luong_in_them

        }
        public enum Enum_DB104
        {
            HMI_Tam_dung,
            HMI_Nhap_phieu,
            YC_bat_in,
            YC_Tat_in,
            YC_in_them,
            DB_IN1,
            DB_IN2,
            DB_IN3,
            Bang_san_sang,
            NguonOK,
            Bang_dang_chay,
            Dang_In,
            Cho_phep_tat_in,
            OUT1,
            OUT2,
            OUT3,
            Luong_Dat_S1,
            Luong_Dat_S2,
            Luong_Dat_S,
            So_dem1,
            So_dem2,
            Bao_qua_dd,
            So_loi,
            So_phieu,
            TGian_C1,
            TGian_C2,
            Tong_bao,
            Bao_dong,
            IN_So_batdau,
            Luong_in_them

        }
        public enum Enum_DB105
        {
            HMI_Tam_dung,
            HMI_Nhap_phieu,
            YC_bat_in,
            YC_Tat_in,
            YC_in_them,
            DB_IN1,
            DB_IN2,
            DB_IN3,
            Bang_san_sang,
            NguonOK,
            Bang_dang_chay,
            Dang_In,
            Cho_phep_tat_in,
            OUT1,
            OUT2,
            OUT3,
            Luong_Dat_S1,
            Luong_Dat_S2,
            Luong_Dat_S,
            So_dem1,
            So_dem2,
            Bao_qua_dd,
            So_loi,
            So_phieu,
            TGian_C1,
            TGian_C2,
            Tong_bao,
            Bao_dong,
            IN_So_batdau,
            Luong_in_them

        }
        public enum Enum_DB106
        {
            HMI_Tam_dung,
            HMI_Nhap_phieu,
            YC_bat_in,
            YC_Tat_in,
            YC_in_them,
            DB_IN1,
            DB_IN2,
            DB_IN3,
            Bang_san_sang,
            NguonOK,
            Bang_dang_chay,
            Dang_In,
            Cho_phep_tat_in,
            OUT1,
            OUT2,
            OUT3,
            Luong_Dat_S1,
            Luong_Dat_S2,
            Luong_Dat_S,
            So_dem1,
            So_dem2,
            Bao_qua_dd,
            So_loi,
            So_phieu,
            TGian_C1,
            TGian_C2,
            Tong_bao,
            Bao_dong,
            IN_So_batdau,
            Luong_in_them

        }
        public enum Enum_DB107
        {
            HMI_Tam_dung,
            HMI_Nhap_phieu,
            YC_bat_in,
            YC_Tat_in,
            YC_in_them,
            DB_IN1,
            DB_IN2,
            DB_IN3,
            Bang_san_sang,
            NguonOK,
            Bang_dang_chay,
            Dang_In,
            Cho_phep_tat_in,
            OUT1,
            OUT2,
            OUT3,
            Luong_Dat_S1,
            Luong_Dat_S2,
            Luong_Dat_S,
            So_dem1,
            So_dem2,
            Bao_qua_dd,
            So_loi,
            So_phieu,
            TGian_C1,
            TGian_C2,
            Tong_bao,
            Bao_dong,
            IN_So_batdau,
            Luong_in_them

        }
        public enum Enum_DB108
        {
            HMI_Tam_dung,
            HMI_Nhap_phieu,
            YC_bat_in,
            YC_Tat_in,
            YC_in_them,
            DB_IN1,
            DB_IN2,
            DB_IN3,
            Bang_san_sang,
            NguonOK,
            Bang_dang_chay,
            Dang_In,
            Cho_phep_tat_in,
            OUT1,
            OUT2,
            OUT3,
            Luong_Dat_S1,
            Luong_Dat_S2,
            Luong_Dat_S,
            So_dem1,
            So_dem2,
            Bao_qua_dd,
            So_loi,
            So_phieu,
            TGian_C1,
            TGian_C2,
            Tong_bao,
            Bao_dong,
            IN_So_batdau,
            Luong_in_them

        }
        public enum Enum_DB109
        {
            HMI_Tam_dung,
            HMI_Nhap_phieu,
            YC_bat_in,
            YC_Tat_in,
            YC_in_them,
            DB_IN1,
            DB_IN2,
            DB_IN3,
            Bang_san_sang,
            NguonOK,
            Bang_dang_chay,
            Dang_In,
            Cho_phep_tat_in,
            OUT1,
            OUT2,
            OUT3,
            Luong_Dat_S1,
            Luong_Dat_S2,
            Luong_Dat_S,
            So_dem1,
            So_dem2,
            Bao_qua_dd,
            So_loi,
            So_phieu,
            TGian_C1,
            TGian_C2,
            Tong_bao,
            Bao_dong,
            IN_So_batdau,
            Luong_in_them

        }
        public enum Enum_DB110
        {
            HMI_Tam_dung,
            HMI_Nhap_phieu,
            YC_bat_in,
            YC_Tat_in,
            YC_in_them,
            DB_IN1,
            DB_IN2,
            DB_IN3,
            Bang_san_sang,
            NguonOK,
            Bang_dang_chay,
            Dang_In,
            Cho_phep_tat_in,
            OUT1,
            OUT2,
            OUT3,
            Luong_Dat_S1,
            Luong_Dat_S2,
            Luong_Dat_S,
            So_dem1,
            So_dem2,
            Bao_qua_dd,
            So_loi,
            So_phieu,
            TGian_C1,
            TGian_C2,
            Tong_bao,
            Bao_dong,
            IN_So_batdau,
            Luong_in_them

        }
        public enum Enum_DB111
        {
            HMI_Tam_dung,
            HMI_Nhap_phieu,
            YC_bat_in,
            YC_Tat_in,
            YC_in_them,
            DB_IN1,
            DB_IN2,
            DB_IN3,
            Bang_san_sang,
            NguonOK,
            Bang_dang_chay,
            Dang_In,
            Cho_phep_tat_in,
            OUT1,
            OUT2,
            OUT3,
            Luong_Dat_S1,
            Luong_Dat_S2,
            Luong_Dat_S,
            So_dem1,
            So_dem2,
            Bao_qua_dd,
            So_loi,
            So_phieu,
            TGian_C1,
            TGian_C2,
            Tong_bao,
            Bao_dong,
            IN_So_batdau,
            Luong_in_them

        }
        public enum Enum_DB112
        {
            HMI_Tam_dung,
            HMI_Nhap_phieu,
            YC_bat_in,
            YC_Tat_in,
            YC_in_them,
            DB_IN1,
            DB_IN2,
            DB_IN3,
            Bang_san_sang,
            NguonOK,
            Bang_dang_chay,
            Dang_In,
            Cho_phep_tat_in,
            OUT1,
            OUT2,
            OUT3,
            Luong_Dat_S1,
            Luong_Dat_S2,
            Luong_Dat_S,
            So_dem1,
            So_dem2,
            Bao_qua_dd,
            So_loi,
            So_phieu,
            TGian_C1,
            TGian_C2,
            Tong_bao,
            Bao_dong,
            IN_So_batdau,
            Luong_in_them

        }
        public enum Enum_DB113
        {
            HMI_Tam_dung,
            HMI_Nhap_phieu,
            YC_bat_in,
            YC_Tat_in,
            YC_in_them,
            DB_IN1,
            DB_IN2,
            DB_IN3,
            Bang_san_sang,
            NguonOK,
            Bang_dang_chay,
            Dang_In,
            Cho_phep_tat_in,
            OUT1,
            OUT2,
            OUT3,
            Luong_Dat_S1,
            Luong_Dat_S2,
            Luong_Dat_S,
            So_dem1,
            So_dem2,
            Bao_qua_dd,
            So_loi,
            So_phieu,
            TGian_C1,
            TGian_C2,
            Tong_bao,
            Bao_dong,
            IN_So_batdau,
            Luong_in_them

        }
        public enum Enum_DB300
        {
            Sophieu,                    //So phieu
            Setpoint,                    //Luongdat Kg cho phuong tien
            KLTongPhieu,                    //Thuc giao
            tong_can,                    //tong tich luy kg
            KLCan1,                    //Ket1_chi thi KL
            KLTong1,                    //Ket1_Tong xuat 1 ket trong phieu
            Min_ket1,                    //Ket1_nguong cat duoi
            Max_ket1,                    //Ket1_Nguong cat tren
            Ncham_ket1,                    //Ket1_Nguong cham
            Buoc_chay_1,                    //Ket1_buoc tu 1->25
            Trangthai_1,                    //=1 cap lieu ket,=2 dung de can,=3 dang xa lieu ,= 0
            KLCan2,                    //Ket2_chi thi KL
            KLTong2,                    //Ket2_Tong xuat 1 ket trong phieu
            Min_ket2,                    //Ket2_nguong cat duoi
            Max_ket2,                    //Ket2_Nguong cat tren
            Ncham_ket2,                    //Ket2_Nguong cham
            Buoc_chay_2,                    //Ket2_buoc tu 1->25
            Trangthai_2,                    //Ket2_=1 cap lieu ket,=2 dung de can,=3 dang xa lieu ,= 0
            HMI_Nhapphieu,                    //Nhâp phieu giam sat=1 ket thuc phieu=0
            HMI_Modechay,                    //Auto=1 local =1
            HMI_Tamdung,                    //tam dung =0
            HMI_chay_rut_lieu,                    //Chay rut lieu cuoi =1
            Caplieuchay,                    //chay=1 dung =0
            Thaolieuchay,                    //chay=1 dung =0
            Baodongchung,                    //=1 bao dong
            Yeu_cau_luuhang1,                    //=1 y/c luu du lieu ket 1
            Yeu_cau_luuhang2,                    //=1 y/c luu du lieu ket 2
            Chophepketthuc,                    //
            Ket1_Caplieu,                    //
            Ket1_thaolieu,                    //
            Ket2_Caplieu,                    //
            Ket2_thaolieu,                    //
            Yeu_cau_luubi1,                    //
            Yeu_cau_luubi2,                    //
            LocbuiP40_Run,                    //
            LocbuiP41_Run,                    //
            STAT12,
            Tongbd,                             //
            Tongkt,
            Yc_caplieuK1,
            Yc_caplieuK2,
            Yc_xalieuK1,
            Yc_xalieuK2,
            DuPhong1,
            DuPhong2,
            DuPhong3,
            DuPhong4

        }
        public enum Enum_DB304
        {
            KL_can,
            KLTong1,
            Luu_xong_hang,
            Luu_xong_bi,
            Resset_xong,
            Loi_chot_KL,
            Buoc_can,
            Reset,
            CMD_Resset,
            CMD_chotKL,
            YC_luubi,
            YC_luuhang,
            KLbi1,
            KLbi2,
            KLbi3,
            KLbi4,
            KLbi5,
            KLbi6,
            KLbi7,
            KLbi8,
            KLbi9,
            KLbi10,
            KLbi11,
            KLbi12,
            KLbi13,
            KLbi14,
            KLbi15,
            KLbi16,
            KLbi17,
            KLbi18,
            KLbi19,
            KLbi20,
            KLbi21,
            KLbi22,
            KLbi23,
            KLbi24,
            KLbi25,
            KLbi26,
            KLbi27,
            KLbi28,
            KLbi29,
            KLbi30,
            KLbi31,
            KLbi32,
            KLbi33,
            KLbi34,
            KLbi35,
            KLbi36,
            KLbi37,
            KLbi38,
            KLbi39,
            KLbi40,
            KLbi41,
            KLbi42,
            KLbi43,
            KLbi44,
            KLbi45,
            KLbi46,
            KLbi47,
            KLbi48,
            KLbi49,
            KLbi50,
            KLhang1,
            KLhang2,
            KLhang3,
            KLhang4,
            KLhang5,
            KLhang6,
            KLhang7,
            KLhang8,
            KLhang9,
            KLhang10,
            KLhang11,
            KLhang12,
            KLhang13,
            KLhang14,
            KLhang15,
            KLhang16,
            KLhang17,
            KLhang18,
            KLhang19,
            KLhang20,
            KLhang21,
            KLhang22,
            KLhang23,
            KLhang24,
            KLhang25,
            KLhang26,
            KLhang27,
            KLhang28,
            KLhang29,
            KLhang30,
            KLhang31,
            KLhang32,
            KLhang33,
            KLhang34,
            KLhang35,
            KLhang36,
            KLhang37,
            KLhang38,
            KLhang39,
            KLhang40,
            KLhang41,
            KLhang42,
            KLhang43,
            KLhang44,
            KLhang45,
            KLhang46,
            KLhang47,
            KLhang48,
            KLhang49,
            KLhang50,

            KLGiao1,
            KLGiao2,
            KLGiao3,
            KLGiao4,
            KLGiao5,
            KLGiao6,
            KLGiao7,
            KLGiao8,
            KLGiao9,
            KLGiao10,
            KLGiao11,
            KLGiao12,
            KLGiao13,
            KLGiao14,
            KLGiao15,
            KLGiao16,
            KLGiao17,
            KLGiao18,
            KLGiao19,
            KLGiao20,
            KLGiao21,
            KLGiao22,
            KLGiao23,
            KLGiao24,
            KLGiao25,
            KLGiao26,
            KLGiao27,
            KLGiao28,
            KLGiao29,
            KLGiao30,
            KLGiao31,
            KLGiao32,
            KLGiao33,
            KLGiao34,
            KLGiao35,
            KLGiao36,
            KLGiao37,
            KLGiao38,
            KLGiao39,
            KLGiao40,
            KLGiao41,
            KLGiao42,
            KLGiao43,
            KLGiao44,
            KLGiao45,
            KLGiao46,
            KLGiao47,
            KLGiao48,
            KLGiao49,
            KLGiao50,

            Check_step,
            KLTong,

        }
        public enum Enum_DB305
        {
            KL_can,
            KLTong1,
            Luu_xong_hang,
            Luu_xong_bi,
            Resset_xong,
            Loi_chot_KL,
            Buoc_can,
            Reset,
            CMD_Resset,
            CMD_chotKL,
            YC_luubi,
            YC_luuhang,
            KLbi1,
            KLbi2,
            KLbi3,
            KLbi4,
            KLbi5,
            KLbi6,
            KLbi7,
            KLbi8,
            KLbi9,
            KLbi10,
            KLbi11,
            KLbi12,
            KLbi13,
            KLbi14,
            KLbi15,
            KLbi16,
            KLbi17,
            KLbi18,
            KLbi19,
            KLbi20,
            KLbi21,
            KLbi22,
            KLbi23,
            KLbi24,
            KLbi25,
            KLbi26,
            KLbi27,
            KLbi28,
            KLbi29,
            KLbi30,
            KLbi31,
            KLbi32,
            KLbi33,
            KLbi34,
            KLbi35,
            KLbi36,
            KLbi37,
            KLbi38,
            KLbi39,
            KLbi40,
            KLbi41,
            KLbi42,
            KLbi43,
            KLbi44,
            KLbi45,
            KLbi46,
            KLbi47,
            KLbi48,
            KLbi49,
            KLbi50,
            KLhang1,
            KLhang2,
            KLhang3,
            KLhang4,
            KLhang5,
            KLhang6,
            KLhang7,
            KLhang8,
            KLhang9,
            KLhang10,
            KLhang11,
            KLhang12,
            KLhang13,
            KLhang14,
            KLhang15,
            KLhang16,
            KLhang17,
            KLhang18,
            KLhang19,
            KLhang20,
            KLhang21,
            KLhang22,
            KLhang23,
            KLhang24,
            KLhang25,
            KLhang26,
            KLhang27,
            KLhang28,
            KLhang29,
            KLhang30,
            KLhang31,
            KLhang32,
            KLhang33,
            KLhang34,
            KLhang35,
            KLhang36,
            KLhang37,
            KLhang38,
            KLhang39,
            KLhang40,
            KLhang41,
            KLhang42,
            KLhang43,
            KLhang44,
            KLhang45,
            KLhang46,
            KLhang47,
            KLhang48,
            KLhang49,
            KLhang50,

            KLGiao1,
            KLGiao2,
            KLGiao3,
            KLGiao4,
            KLGiao5,
            KLGiao6,
            KLGiao7,
            KLGiao8,
            KLGiao9,
            KLGiao10,
            KLGiao11,
            KLGiao12,
            KLGiao13,
            KLGiao14,
            KLGiao15,
            KLGiao16,
            KLGiao17,
            KLGiao18,
            KLGiao19,
            KLGiao20,
            KLGiao21,
            KLGiao22,
            KLGiao23,
            KLGiao24,
            KLGiao25,
            KLGiao26,
            KLGiao27,
            KLGiao28,
            KLGiao29,
            KLGiao30,
            KLGiao31,
            KLGiao32,
            KLGiao33,
            KLGiao34,
            KLGiao35,
            KLGiao36,
            KLGiao37,
            KLGiao38,
            KLGiao39,
            KLGiao40,
            KLGiao41,
            KLGiao42,
            KLGiao43,
            KLGiao44,
            KLGiao45,
            KLGiao46,
            KLGiao47,
            KLGiao48,
            KLGiao49,
            KLGiao50,

            Check_step,
            KLTong,
        }
        public enum ENUM_DanhMucDB
        {
            P1U53_DB = 100,  // điều khiển tay gạt
            P1U64_DB = 101,  //máng  1
            P1U65_DB = 102,   //máng  2
            P1U54_DB = 103,   //máng 3
            P1U55_DB = 104,   //máng  4
            P1U34_DB = 105,    //máng 5
            P1U45_DB = 106,   //máng  6
            P1U63_DB = 107,  //điều khiển tay gạt
            P1U46_DB = 108,   //máng 8
            P1U94_DB = 109, //máng  9
            P1U33_DB = 110,  //máng đường sắt thượng
            P1U43_DB = 111,   //máng   đường sắt hạ
            P1U451_DB = 112, // điều khiển tay gạt
            P1U452_DB = 113,  // điều khiển tay gạt chỉ đọc trạng thái ss băng, xung đầu đếm ,số bao qua đầu đếm
            XiRoiThuyChung_DB = 300,
            XiRoiThuyKet1_DB = 304,
            XiRoiThuyKet2_DB = 305,
        }
        public enum ErrorCode
        {
            NoError = 0,
            WrongCPU_Type = 1,
            ConnectionError = 2,
            IPAddressNotAvailable,
            WrongVarFormat = 10,
            WrongNumberReceivedBytes = 11,
            SendData = 20,
            ReadData = 30,
            WriteData = 50
        }
    }
}
