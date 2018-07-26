using S7.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Threading;
using XuatThuy.Utils;

/// <summary>
/// Summary description for PLC
/// </summary>

namespace XuatThuy.PLCs
{
    public class PLC
    {
        private static PLC _Instance;
        public static PLC GetInstance(XuatHangDataContext db)
        {
            if (_Instance == null)
            {
                _Instance = new PLC(db);
            }

            return _Instance;
        }

        private System.Timers.Timer Timer_DocGhi;
        //XuatHangDataContext db = new XuatHangDataContext();
        public void set_timer_tic(double time)
        {
            while (true)
            {
                if (Timer_DocGhi.Enabled == true)
                {
                    Timer_DocGhi.Enabled = false;
                    Timer_DocGhi.Interval = time;
                    Timer_DocGhi.Enabled = true;
                    break;
                }
            }
        }
        /// <summary>
        /// Hàm tạo Class PLC
        /// </summary>
        public PLC(XuatHangDataContext db)
        {
            try
            {
                var listPLC = db.PLC_PLCLay().ToList();

                if (listPLC.Count > 0)
                {
                    cpuIP = listPLC[0].IP.ToString();
                    cpurack = (short)listPLC[0].Rack;
                    cpuSlot = (short)listPLC[0].Slot;
                    PLC1 = new Plc(MyCpuType, MyCpuIp, MyCpuRack, MyCpuSlot);

                    //Debug.WriteLine("_plcModel.IpAddress == " + cpuIP);
                    //Debug.WriteLine("_plcModel.CpuType == " + MyCpuType.ToString());
                    //Debug.WriteLine("_plcModel.Rack == " + cpurack.ToString());
                    //Debug.WriteLine("_plcModel.Slot == " + cpuSlot.ToString());

                    khoitaodanhsachDB(db);
                    KhoiTaoDataTableGhi(db);
                }
            }
            catch (Exception ex)
            {
                LogUtils.PrintLog("ERROR: Không khởi tạo được PLC");
                LogUtils.PrintLog("ERROR MESSAGE: " + ex.Message);
                throw;
            }

            if (Table_ListDB.Rows.Count > 0)
            {
                Timer_DocGhi = new System.Timers.Timer();
                Timer_DocGhi.Interval = 10;
                Timer_DocGhi.Elapsed += new System.Timers.ElapsedEventHandler(Docghi);
                Timer_DocGhi.Start();

            }
        }

        /// <summary>
        /// thao tác đọc và ghi 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Docghi(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Timer_DocGhi.Enabled = false;
                TimeMerThaoTacDocDuLieu();
                Timer_DocGhi.Enabled = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception message ==== " + ex.Message);
            }
        }

        /// <summary>
        /// Khai báo CPURack
        /// </summary>
        private static short MyCpuRack;
        public short cpurack
        {
            get { return MyCpuRack; }
            set
            {
                MyCpuRack = value;
            }
        }
        /// <summary>
        /// Khai Báo CPUSlot
        /// </summary>
        private static short MyCpuSlot;
        public short cpuSlot
        {
            get { return MyCpuSlot; }
            set { MyCpuSlot = value; }
        }
        private static string MyCpuIp;
        /// <summary>
        /// Khai Báo CPUIP
        /// </summary>
        public string cpuIP
        {
            get { return MyCpuIp; }
            set
            {
                MyCpuIp = value;
            }
        }
        /// <summary>
        /// Khai báo CPU type
        /// </summary>
        private static CpuType MyCpuType = S7.Net.CpuType.S7300;

        /// <summary>
        ///  Khai Báo PLC và kết nối
        /// </summary>
        static S7.Net.Plc PLC1 = new Plc(MyCpuType, MyCpuIp, MyCpuRack, MyCpuSlot);
        /// <summary>
        /// khai báo đối tượng thuộc các lớp cơ sở
        /// </summary>
        public static DanhSachTagdb100 DS_TagDB100 = new DanhSachTagdb100();
        public static DanhSachTagdb100 DS_TagDB101 = new DanhSachTagdb100();
        public static DanhSachTagdb100 DS_TagDB102 = new DanhSachTagdb100();
        public static DanhSachTagdb100 DS_TagDB103 = new DanhSachTagdb100();
        public static DanhSachTagdb100 DS_TagDB104 = new DanhSachTagdb100();
        public static DanhSachTagdb100 DS_TagDB105 = new DanhSachTagdb100();
        public static DanhSachTagdb100 DS_TagDB106 = new DanhSachTagdb100();
        public static DanhSachTagdb100 DS_TagDB107 = new DanhSachTagdb100();
        public static DanhSachTagdb100 DS_TagDB108 = new DanhSachTagdb100();
        public static DanhSachTagdb100 DS_TagDB109 = new DanhSachTagdb100();
        public static DanhSachTagdb100 DS_TagDB110 = new DanhSachTagdb100();
        public static DanhSachTagdb100 DS_TagDB111 = new DanhSachTagdb100();
        public static DanhSachTagdb100 DS_TagDB112 = new DanhSachTagdb100();
        public static DanhSachTagdb100 DS_TagDB113 = new DanhSachTagdb100();
        public static DanhSachTagDB300 DS_Tag_DB300 = new DanhSachTagDB300();
        public static DanhSachTagDB304 DS_Tag_DB304 = new DanhSachTagDB304();
        public static DanhSachTagDB304 DS_Tag_DB305 = new DanhSachTagDB304();

        /// <summary>
        ///  mở kết nối đến PLC
        /// </summary>
        ErrorCode connectionResult = PLC1.Open();
        //ErrorCode   connectionResult=(ErrorCode) Application["connectionResult"] ;
        //Khai Báo DataTable để chứa toàn bộ dũ liệu từ PLC đẩy ra. 
        //  public    List<int> ListDB = new List<int>();
        public System.Data.DataTable Table_ListDB = new DataTable();
        public static System.Data.DataTable Table_ListTag_Ghi = new DataTable();

        // public    Dictionary<int, int> ListDB = new Dictionary<int, int>();
        /// <summary>
        /// Danh sách các tag trong  luuw trong 1 DB
        /// </summary> 

        public static bool PLCConnected;
        public static bool ReadSuccess;
        /// <summary>
        /// đẩy dữ liệu từ class ra DataTable   doc=1 la doc con doc=0 la ghi
        /// </summary>
        /// <param name="DB"></param>
        public void TimeMerThaoTacDocDuLieu()
        {     // nếu connect được thì thực hiện đọc ghi dữ liệu
            //Debug.WriteLine("connectionResult == " + connectionResult.ToString());
            PLCConnected = PLC1.IsConnected && PLC1.IsAvailable;
            if (PLC1.IsAvailable)
            {    // nếu connect được thì thực hiện đọc ghi dữ liệu

                if (connectionResult.Equals(ErrorCode.NoError))
                {   /// chỗ này cần code thêm để thực hiện 2 việc song song nhau tránh gây chậm

                    DocDuLieuTuPLCRaClassTuongUng();
                    ReadSuccess = true;
                }
                // nếu không connect được thì thực hiện đóng kết nối và mở một kết nối khác
                else
                {
                    PLC1.Close();
                    connectionResult = PLC1.Open();
                }
            }
            // nếu không connect được thì thực hiện đóng kết nối và mở một kết nối khác
            else
            {
                PLC1.Close();
                ReadSuccess = false;
                connectionResult = PLC1.Open();
            }
        }
        public void KhoiTaoDataTableGhi(XuatHangDataContext db)
        {
            Table_ListTag_Ghi.Clear();
            DataRow rowWrite;
            //System.Type typeInt32 = System.Type.GetType("System.Int32");
            DataColumn DBID_Write = new DataColumn("DBID_Write", System.Type.GetType("System.Int32"));
            DataColumn TenTag_Write = new DataColumn("TenTag_Write", System.Type.GetType("System.String"));
            DataColumn GiaTri_Write = new DataColumn("GiaTri_Write", System.Type.GetType("System.String"));
            DataColumn Flag_OK_Write = new DataColumn("Flag_OK_Write", System.Type.GetType("System.Int32"));
            DataColumn DiaChi = new DataColumn("DiaChi", System.Type.GetType("System.String"));
            DataColumn KieuDuLieu = new DataColumn("KieuDuLieu", System.Type.GetType("System.String"));
            DataColumn ChuoiDocGiaTri = new DataColumn("ChuoiDocGiaTri", System.Type.GetType("System.String"));
            Table_ListTag_Ghi.Columns.Add(DBID_Write);
            Table_ListTag_Ghi.Columns.Add(TenTag_Write);
            Table_ListTag_Ghi.Columns.Add(GiaTri_Write);
            Table_ListTag_Ghi.Columns.Add(Flag_OK_Write);
            Table_ListTag_Ghi.Columns.Add(DiaChi);
            Table_ListTag_Ghi.Columns.Add(KieuDuLieu);
            Table_ListTag_Ghi.Columns.Add(ChuoiDocGiaTri);
            var listtag_Write = db.p_DanhSachTag_Ghi().ToList();
            foreach (var item in listtag_Write)
            {
                rowWrite = Table_ListTag_Ghi.NewRow();
                rowWrite["DBID_Write"] = item.DBID;
                rowWrite["TenTag_Write"] = item.TenTrenPLC;
                rowWrite["GiaTri_Write"] = item.GiaTri;
                rowWrite["Flag_OK_Write"] = item.Flag_OK;
                rowWrite["DiaChi"] = item.DiaChi;
                rowWrite["KieuDuLieu"] = item.KieuDuLieu;
                rowWrite["ChuoiDocGiaTri"] = item.ChuoiDocGiaTri;
                Table_ListTag_Ghi.Rows.Add(rowWrite);
            }
        }
        /// <summary>
        /// Thao tác đọc tất cả dữ liệu từ PLC ra DataTable
        /// </summary>
        private void DocDuLieuTuPLCRaClassTuongUng()
        {
            // đẩy dữ liệu từ class x sang datatable;
            for (int i = 0; i < Table_ListDB.Rows.Count; i++)
            {
                string x; DateTime dt;
                int loaiDB = Table_ListDB.Rows[i].Field<int>("LoaiDB");
                int DB = Table_ListDB.Rows[i].Field<int>("BDID");
                dt = DateTime.Now;
                switch (DB)
                {
                    case 100:

                        PLC1.ReadClass(DS_TagDB100, DB);
                        break;
                    case 101:
                        PLC1.ReadClass(DS_TagDB101, DB);
                        break;

                    case 102:
                        PLC1.ReadClass(DS_TagDB102, DB);
                        break;
                    case 103:
                        PLC1.ReadClass(DS_TagDB103, DB);
                        break;
                    case 104:
                        PLC1.ReadClass(DS_TagDB104, DB);
                        break;
                    case 105:
                        PLC1.ReadClass(DS_TagDB105, DB);
                        break;
                    case 106:
                        PLC1.ReadClass(DS_TagDB106, DB);
                        break;
                    case 107:
                        PLC1.ReadClass(DS_TagDB107, DB);
                        break;
                    case 108:
                        PLC1.ReadClass(DS_TagDB108, DB);
                        break;
                    case 109:
                        PLC1.ReadClass(DS_TagDB109, DB);
                        break;
                    case 110:
                        PLC1.ReadClass(DS_TagDB110, DB);
                        break;
                    case 111:
                        PLC1.ReadClass(DS_TagDB111, DB);
                        break;
                    case 112:
                        PLC1.ReadClass(DS_TagDB112, DB);
                        break;
                    case 113:
                        PLC1.ReadClass(DS_TagDB113, DB);
                        break;
                    case 300:
                        PLC1.ReadClass(DS_Tag_DB300, DB);
                        break;

                    case 304:
                        PLC1.ReadClass(DS_Tag_DB304, DB);
                        break;
                    case 305:
                        PLC1.ReadClass(DS_Tag_DB305, DB);
                        break;

                    default:
                        break;

                }

                if (thongbao != null)
                {
                    x = (DateTime.Now - dt).TotalMilliseconds.ToString();
                    thongbao(x);
                }
            }

        }
        public delegate void TB(string msg);
        public event TB thongbao;


        public static ErrorCode Write_DB300_Sophieu(int GiaTri) //DINT
        {
            var results = from myRow in Table_ListTag_Ghi.AsEnumerable()
                          where myRow.Field<int>("DBID_Write") == 300 && myRow.Field<string>("TenTag_Write") == Enum.GetName(typeof(HamDungchung.Enum_DB300), HamDungchung.Enum_DB300.Sophieu)
                          select myRow.Field<string>("ChuoiDocGiaTri");
            var chuoidocghi = results.FirstOrDefault();
            ErrorCode kq = PLC1.Write(chuoidocghi, GiaTri);
            return kq;
        }
        public static ErrorCode Write_DB300_Setpoint(int GiaTri) //DINT
        {
            var results = from myRow in Table_ListTag_Ghi.AsEnumerable()
                          where myRow.Field<int>("DBID_Write") == 300 && myRow.Field<string>("TenTag_Write") == Enum.GetName(typeof(HamDungchung.Enum_DB300), HamDungchung.Enum_DB300.Setpoint)
                          select myRow.Field<string>("ChuoiDocGiaTri");
            var chuoidocghi = results.FirstOrDefault();
            ErrorCode kq = PLC1.Write(chuoidocghi, GiaTri);
            return kq;
        }
        public static ErrorCode Write_DB300_Min_ket1(int GiaTri) //DINT
        {
            var results = from myRow in Table_ListTag_Ghi.AsEnumerable()
                          where myRow.Field<int>("DBID_Write") == 300 && myRow.Field<string>("TenTag_Write") == Enum.GetName(typeof(HamDungchung.Enum_DB300), HamDungchung.Enum_DB300.Min_ket1)
                          select myRow.Field<string>("ChuoiDocGiaTri");
            var chuoidocghi = results.FirstOrDefault();
            ErrorCode kq = PLC1.Write(chuoidocghi, GiaTri);
            return kq;
        }
        public static ErrorCode Write_DB300_Max_ket1(int GiaTri) //DINT
        {
            var results = from myRow in Table_ListTag_Ghi.AsEnumerable()
                          where myRow.Field<int>("DBID_Write") == 300 && myRow.Field<string>("TenTag_Write") == Enum.GetName(typeof(HamDungchung.Enum_DB300), HamDungchung.Enum_DB300.Max_ket1)
                          select myRow.Field<string>("ChuoiDocGiaTri");
            var chuoidocghi = results.FirstOrDefault();
            ErrorCode kq = PLC1.Write(chuoidocghi, GiaTri);
            return kq;
        }
        public static ErrorCode Write_DB300_Ncham_ket1(int GiaTri) //DINT
        {
            var results = from myRow in Table_ListTag_Ghi.AsEnumerable()
                          where myRow.Field<int>("DBID_Write") == 300 && myRow.Field<string>("TenTag_Write") == Enum.GetName(typeof(HamDungchung.Enum_DB300), HamDungchung.Enum_DB300.Ncham_ket1)
                          select myRow.Field<string>("ChuoiDocGiaTri");
            var chuoidocghi = results.FirstOrDefault();
            ErrorCode kq = PLC1.Write(chuoidocghi, GiaTri);
            return kq;
        }
        public static ErrorCode Write_DB300_Min_ket2(int GiaTri) //DINT
        {
            var results = from myRow in Table_ListTag_Ghi.AsEnumerable()
                          where myRow.Field<int>("DBID_Write") == 300 && myRow.Field<string>("TenTag_Write") == Enum.GetName(typeof(HamDungchung.Enum_DB300), HamDungchung.Enum_DB300.Min_ket2)
                          select myRow.Field<string>("ChuoiDocGiaTri");
            var chuoidocghi = results.FirstOrDefault();
            ErrorCode kq = PLC1.Write(chuoidocghi, GiaTri);
            return kq;
        }
        public static ErrorCode Write_DB300_Max_ket2(int GiaTri) //DINT
        {
            var results = from myRow in Table_ListTag_Ghi.AsEnumerable()
                          where myRow.Field<int>("DBID_Write") == 300 && myRow.Field<string>("TenTag_Write") == Enum.GetName(typeof(HamDungchung.Enum_DB300), HamDungchung.Enum_DB300.Max_ket2)
                          select myRow.Field<string>("ChuoiDocGiaTri");
            var chuoidocghi = results.FirstOrDefault();
            ErrorCode kq = PLC1.Write(chuoidocghi, GiaTri);
            return kq;
        }
        public static ErrorCode Write_DB300_Ncham_ket2(int GiaTri) //DINT
        {
            var results = from myRow in Table_ListTag_Ghi.AsEnumerable()
                          where myRow.Field<int>("DBID_Write") == 300 && myRow.Field<string>("TenTag_Write") == Enum.GetName(typeof(HamDungchung.Enum_DB300), HamDungchung.Enum_DB300.Ncham_ket2)
                          select myRow.Field<string>("ChuoiDocGiaTri");
            var chuoidocghi = results.FirstOrDefault();
            ErrorCode kq = PLC1.Write(chuoidocghi, GiaTri);
            return kq;
        }
        public static ErrorCode Write_DB300_HMI_Nhapphieu(bool GiaTri) //BOOL
        {
            var results = from myRow in Table_ListTag_Ghi.AsEnumerable()
                          where myRow.Field<int>("DBID_Write") == 300 && myRow.Field<string>("TenTag_Write") == Enum.GetName(typeof(HamDungchung.Enum_DB300), HamDungchung.Enum_DB300.HMI_Nhapphieu)
                          select myRow.Field<string>("ChuoiDocGiaTri");
            var chuoidocghi = results.FirstOrDefault();
            ErrorCode kq = PLC1.Write(chuoidocghi, GiaTri);
            return kq;
        }
        public static ErrorCode Write_DB300_HMI_Modechay(bool GiaTri) //BOOL
        {
            var results = from myRow in Table_ListTag_Ghi.AsEnumerable()
                          where myRow.Field<int>("DBID_Write") == 300 && myRow.Field<string>("TenTag_Write") == Enum.GetName(typeof(HamDungchung.Enum_DB300), HamDungchung.Enum_DB300.HMI_Modechay)
                          select myRow.Field<string>("ChuoiDocGiaTri");
            var chuoidocghi = results.FirstOrDefault();
            ErrorCode kq = PLC1.Write(chuoidocghi, GiaTri);
            return kq;
        }
        public static ErrorCode Write_DB300_HMI_Tamdung(bool GiaTri) //BOOL
        {
            var results = from myRow in Table_ListTag_Ghi.AsEnumerable()
                          where myRow.Field<int>("DBID_Write") == 300 && myRow.Field<string>("TenTag_Write") == Enum.GetName(typeof(HamDungchung.Enum_DB300), HamDungchung.Enum_DB300.HMI_Tamdung)
                          select myRow.Field<string>("ChuoiDocGiaTri");
            var chuoidocghi = results.FirstOrDefault();
            ErrorCode kq = PLC1.Write(chuoidocghi, GiaTri);
            return kq;
        }
        public static ErrorCode Write_DB300_HMI_chay_rut_lieu(bool GiaTri) //BOOL
        {
            var results = from myRow in Table_ListTag_Ghi.AsEnumerable()
                          where myRow.Field<int>("DBID_Write") == 300 && myRow.Field<string>("TenTag_Write") == Enum.GetName(typeof(HamDungchung.Enum_DB300), HamDungchung.Enum_DB300.HMI_chay_rut_lieu)
                          select myRow.Field<string>("ChuoiDocGiaTri");
            var chuoidocghi = results.FirstOrDefault();
            ErrorCode kq = PLC1.Write(chuoidocghi, GiaTri);
            return kq;
        }
        public static ErrorCode Write_DB300_Yeu_cau_luuhang1(bool GiaTri) //BOOL
        {
            var results = from myRow in Table_ListTag_Ghi.AsEnumerable()
                          where myRow.Field<int>("DBID_Write") == 300 && myRow.Field<string>("TenTag_Write") == Enum.GetName(typeof(HamDungchung.Enum_DB300), HamDungchung.Enum_DB300.Yeu_cau_luuhang1)
                          select myRow.Field<string>("ChuoiDocGiaTri");
            var chuoidocghi = results.FirstOrDefault();
            ErrorCode kq = PLC1.Write(chuoidocghi, GiaTri);
            return kq;
        }
        public static ErrorCode Write_DB300_Yeu_cau_luuhang2(bool GiaTri) //BOOL
        {
            var results = from myRow in Table_ListTag_Ghi.AsEnumerable()
                          where myRow.Field<int>("DBID_Write") == 300 && myRow.Field<string>("TenTag_Write") == Enum.GetName(typeof(HamDungchung.Enum_DB300), HamDungchung.Enum_DB300.Yeu_cau_luuhang2)
                          select myRow.Field<string>("ChuoiDocGiaTri");
            var chuoidocghi = results.FirstOrDefault();
            ErrorCode kq = PLC1.Write(chuoidocghi, GiaTri);
            return kq;

        }
        public static ErrorCode Write_DB300_Yeu_cau_luubi1(bool GiaTri) //BOOL
        {
            var results = from myRow in Table_ListTag_Ghi.AsEnumerable()
                          where myRow.Field<int>("DBID_Write") == 300 && myRow.Field<string>("TenTag_Write") == Enum.GetName(typeof(HamDungchung.Enum_DB300), HamDungchung.Enum_DB300.Yeu_cau_luubi1)
                          select myRow.Field<string>("ChuoiDocGiaTri");
            var chuoidocghi = results.FirstOrDefault();
            ErrorCode kq = PLC1.Write(chuoidocghi, GiaTri);
            return kq;

        }
        public static ErrorCode Write_DB300_Yeu_cau_luubi2(bool GiaTri) //BOOL
        {
            var results = from myRow in Table_ListTag_Ghi.AsEnumerable()
                          where myRow.Field<int>("DBID_Write") == 300 && myRow.Field<string>("TenTag_Write") == Enum.GetName(typeof(HamDungchung.Enum_DB300), HamDungchung.Enum_DB300.Yeu_cau_luubi2)
                          select myRow.Field<string>("ChuoiDocGiaTri");
            var chuoidocghi = results.FirstOrDefault();
            ErrorCode kq = PLC1.Write(chuoidocghi, GiaTri);
            return kq;

        }



        /// <summary>
        /// Thao tác ghi tất cả dữ liệu từ Datatable ghi vào PLC
        /// </summary>             
        private void GhiDuLieuVaoPLC()
        {
            if (PLC1.IsAvailable)
            {    // nếu connect được thì thực hiện đọc ghi dữ liệu
                if (connectionResult.Equals(ErrorCode.NoError))
                {   /// chỗ này cần code thêm để thực hiện 2 việc song song nhau tránh gây chậm
                    DocDuLieuTuPLCRaClassTuongUng();
                }
                // nếu không connect được thì thực hiện đóng kết nối và mở một kết nối khác
                else
                {
                    PLC1.Close();
                    connectionResult = PLC1.Open();
                }
            }
            // nếu không connect được thì thực hiện đóng kết nối và mở một kết nối khác
            else
            {
                PLC1.Close();
                connectionResult = PLC1.Open();
            }
        }
        /// <summary>
        /// Khởi tạo danh sách DB tương ứng với Class lấy từ Database
        /// </summary>
        void khoitaodanhsachDB(XuatHangDataContext db)
        {
            Table_ListDB.Clear();
            //Table_ListTag.Rows.Clear();
            //Table_ListTag.Columns.Clear();
            DataRow row;
            DataColumn DBID = new DataColumn("BDID", System.Type.GetType("System.Int32"));
            DataColumn LoaiDB = new DataColumn("LoaiDB", System.Type.GetType("System.Int32"));
            Table_ListDB.Columns.Add(DBID);
            Table_ListDB.Columns.Add(LoaiDB);
            var ListDB = db.PLC_DanhSachDB().ToList();
            foreach (var item in ListDB)
            {
                row = Table_ListDB.NewRow();
                row["BDID"] = item.ID;
                row["LoaiDB"] = item.LoaiDB;

                Table_ListDB.Rows.Add(row);
            }
        }


        public void CloseconectPLC()
        {
            PLC1.Close();
        }

    }
    public class DanhSachTagdb100
    {
        public bool HMI_Tam_dung { get; set; }  // =0 tam dung, =1 ket thuc
        public bool HMI_Nhap_phieu { get; set; }  // =0 ket thuc phieu, =1 nhap phieu de giam sat san sang xuat
        public bool YC_bat_in { get; set; }   // =1 sau khi nhap phieu 2s và yêu cau PM máy in phun hoat dong
        public bool YC_Tat_in { get; set; }   // =1 khi ket thuc phieu, yeu cau may in tat
        public bool YC_in_them { get; set; }    // =1 khi yeu cau san sang lai may in
        public bool DB_IN1 { get; set; } // du phong1
        public bool DB_IN2 { get; set; }   // du phong2
        public bool DB_IN3 { get; set; }  // du phong3
        public bool Bang_san_sang { get; set; }   // =0 mat san sang cho phep bang chay, =1 san sang cho phep bang chay
        public bool NguonOK { get; set; }   // Nguon24V ok
        public bool Bang_dang_chay { get; set; }    // =1 khi bang dang chay, =0 khi bang dung
        public bool Dang_In { get; set; }  // =1 Báo máy in dang chay
        public bool Cho_phep_tat_in { get; set; }   // =1 Cho phep may in tat khi du 
        public bool OUT1 { get; set; }   // du phong out1
        public bool OUT2 { get; set; } // du phong out2
        public bool OUT3 { get; set; }   // du phong out3
        public ushort Luong_Dat_S1 { get; set; }   // Luong bao dat lan 1 truoc khi nhap phieu
        public ushort Luong_Dat_S2 { get; set; }   // Luong bao dat lan 2 sau khi nhap phieu dung de nhap xuat bo xung bao thieu
        public ushort Luong_Dat_S { get; set; }   // Luong_Dat_S=Luong_Dat_S1+Luong_Dat_S2
        public ushort So_dem1 { get; set; }  // So dem cua dau dem 1( trong 1 phieu)
        public ushort So_dem2 { get; set; }  // So dem cua dau dem 2( trong 1 phieu)
        public ushort Bao_qua_dd { get; set; }  // So bao cau xuong de tru luong dat xuat
        public ushort So_loi { get; set; }   // So loi bao dong trong 1 phieu.
        public int So_phieu { get; set; }  // So phieu tang dan trong 1 mang.
        public int TGian_C1 { get; set; }    // Thoi gian ms- 1 bao qua dau dem1
        public int TGian_C2 { get; set; }   // Thoi gian ms- 1 bao qua dau dem2
        public int Tong_bao { get; set; }  // Tong so bao tich luy
        public byte Bao_dong { get; set; }  // =1 dinh bao dd1,=2 dinh bao dd2,=4 mat nguon,=8 lech bao,=16 qua so loi quy din
        public ushort IN_So_batdau { get; set; }   // So bao da qua dau in xuong phuong tien khi bat dau chay bang
        public ushort Luong_in_them { get; set; }
    }
    public class DanhSachTagdb101
    {
        public bool HMI_Tam_dung { get; set; }  // =0 tam dung, =1 ket thuc
        public bool HMI_Nhap_phieu { get; set; }  // =0 ket thuc phieu, =1 nhap phieu de giam sat san sang xuat
        public bool YC_bat_in { get; set; }   // =1 sau khi nhap phieu 2s và yêu cau PM máy in phun hoat dong
        public bool YC_Tat_in { get; set; }   // =1 khi ket thuc phieu, yeu cau may in tat
        public bool YC_in_them { get; set; }    // =1 khi yeu cau san sang lai may in
        public bool DB_IN1 { get; set; } // du phong1
        public bool DB_IN2 { get; set; }   // du phong2
        public bool DB_IN3 { get; set; }  // du phong3
        public bool Bang_san_sang { get; set; }   // =0 mat san sang cho phep bang chay, =1 san sang cho phep bang chay
        public bool NguonOK { get; set; }   // Nguon24V ok
        public bool Bang_dang_chay { get; set; }    // =1 khi bang dang chay, =0 khi bang dung
        public bool Dang_In { get; set; }  // =1 Báo máy in dang chay
        public bool Cho_phep_tat_in { get; set; }   // =1 Cho phep may in tat khi du 
        public bool OUT1 { get; set; }   // du phong out1
        public bool OUT2 { get; set; } // du phong out2
        public bool OUT3 { get; set; }   // du phong out3
        public ushort Luong_Dat_S1 { get; set; }   // Luong bao dat lan 1 truoc khi nhap phieu
        public ushort Luong_Dat_S2 { get; set; }   // Luong bao dat lan 2 sau khi nhap phieu dung de nhap xuat bo xung bao thieu
        public ushort Luong_Dat_S { get; set; }   // Luong_Dat_S=Luong_Dat_S1+Luong_Dat_S2
        public ushort So_dem1 { get; set; }  // So dem cua dau dem 1( trong 1 phieu)
        public ushort So_dem2 { get; set; }  // So dem cua dau dem 2( trong 1 phieu)
        public ushort Bao_qua_dd { get; set; }  // So bao cau xuong de tru luong dat xuat
        public ushort So_loi { get; set; }   // So loi bao dong trong 1 phieu.
        public int So_phieu { get; set; }  // So phieu tang dan trong 1 mang.
        public int TGian_C1 { get; set; }    // Thoi gian ms- 1 bao qua dau dem1
        public int TGian_C2 { get; set; }   // Thoi gian ms- 1 bao qua dau dem2
        public int Tong_bao { get; set; }  // Tong so bao tich luy
        public byte Bao_dong { get; set; }  // =1 dinh bao dd1,=2 dinh bao dd2,=4 mat nguon,=8 lech bao,=16 qua so loi quy din
        public ushort IN_So_batdau { get; set; }   // So bao da qua dau in xuong phuong tien khi bat dau chay bang
        public ushort Luong_in_them { get; set; }
    }
    public class DanhSachTagdb102
    {
        public bool HMI_Tam_dung { get; set; }  // =0 tam dung, =1 ket thuc
        public bool HMI_Nhap_phieu { get; set; }  // =0 ket thuc phieu, =1 nhap phieu de giam sat san sang xuat
        public bool YC_bat_in { get; set; }   // =1 sau khi nhap phieu 2s và yêu cau PM máy in phun hoat dong
        public bool YC_Tat_in { get; set; }   // =1 khi ket thuc phieu, yeu cau may in tat
        public bool YC_in_them { get; set; }    // =1 khi yeu cau san sang lai may in
        public bool DB_IN1 { get; set; } // du phong1
        public bool DB_IN2 { get; set; }   // du phong2
        public bool DB_IN3 { get; set; }  // du phong3
        public bool Bang_san_sang { get; set; }   // =0 mat san sang cho phep bang chay, =1 san sang cho phep bang chay
        public bool NguonOK { get; set; }   // Nguon24V ok
        public bool Bang_dang_chay { get; set; }    // =1 khi bang dang chay, =0 khi bang dung
        public bool Dang_In { get; set; }  // =1 Báo máy in dang chay
        public bool Cho_phep_tat_in { get; set; }   // =1 Cho phep may in tat khi du 
        public bool OUT1 { get; set; }   // du phong out1
        public bool OUT2 { get; set; } // du phong out2
        public bool OUT3 { get; set; }   // du phong out3
        public ushort Luong_Dat_S1 { get; set; }   // Luong bao dat lan 1 truoc khi nhap phieu
        public ushort Luong_Dat_S2 { get; set; }   // Luong bao dat lan 2 sau khi nhap phieu dung de nhap xuat bo xung bao thieu
        public ushort Luong_Dat_S { get; set; }   // Luong_Dat_S=Luong_Dat_S1+Luong_Dat_S2
        public ushort So_dem1 { get; set; }  // So dem cua dau dem 1( trong 1 phieu)
        public ushort So_dem2 { get; set; }  // So dem cua dau dem 2( trong 1 phieu)
        public ushort Bao_qua_dd { get; set; }  // So bao cau xuong de tru luong dat xuat
        public ushort So_loi { get; set; }   // So loi bao dong trong 1 phieu.
        public int So_phieu { get; set; }  // So phieu tang dan trong 1 mang.
        public int TGian_C1 { get; set; }    // Thoi gian ms- 1 bao qua dau dem1
        public int TGian_C2 { get; set; }   // Thoi gian ms- 1 bao qua dau dem2
        public int Tong_bao { get; set; }  // Tong so bao tich luy
        public byte Bao_dong { get; set; }  // =1 dinh bao dd1,=2 dinh bao dd2,=4 mat nguon,=8 lech bao,=16 qua so loi quy din
        public ushort IN_So_batdau { get; set; }   // So bao da qua dau in xuong phuong tien khi bat dau chay bang
        public ushort Luong_in_them { get; set; }
    }
    public class DanhSachTagdb103
    {
        public bool HMI_Tam_dung { get; set; }  // =0 tam dung, =1 ket thuc
        public bool HMI_Nhap_phieu { get; set; }  // =0 ket thuc phieu, =1 nhap phieu de giam sat san sang xuat
        public bool YC_bat_in { get; set; }   // =1 sau khi nhap phieu 2s và yêu cau PM máy in phun hoat dong
        public bool YC_Tat_in { get; set; }   // =1 khi ket thuc phieu, yeu cau may in tat
        public bool YC_in_them { get; set; }    // =1 khi yeu cau san sang lai may in
        public bool DB_IN1 { get; set; } // du phong1
        public bool DB_IN2 { get; set; }   // du phong2
        public bool DB_IN3 { get; set; }  // du phong3
        public bool Bang_san_sang { get; set; }   // =0 mat san sang cho phep bang chay, =1 san sang cho phep bang chay
        public bool NguonOK { get; set; }   // Nguon24V ok
        public bool Bang_dang_chay { get; set; }    // =1 khi bang dang chay, =0 khi bang dung
        public bool Dang_In { get; set; }  // =1 Báo máy in dang chay
        public bool Cho_phep_tat_in { get; set; }   // =1 Cho phep may in tat khi du 
        public bool OUT1 { get; set; }   // du phong out1
        public bool OUT2 { get; set; } // du phong out2
        public bool OUT3 { get; set; }   // du phong out3
        public ushort Luong_Dat_S1 { get; set; }   // Luong bao dat lan 1 truoc khi nhap phieu
        public ushort Luong_Dat_S2 { get; set; }   // Luong bao dat lan 2 sau khi nhap phieu dung de nhap xuat bo xung bao thieu
        public ushort Luong_Dat_S { get; set; }   // Luong_Dat_S=Luong_Dat_S1+Luong_Dat_S2
        public ushort So_dem1 { get; set; }  // So dem cua dau dem 1( trong 1 phieu)
        public ushort So_dem2 { get; set; }  // So dem cua dau dem 2( trong 1 phieu)
        public ushort Bao_qua_dd { get; set; }  // So bao cau xuong de tru luong dat xuat
        public ushort So_loi { get; set; }   // So loi bao dong trong 1 phieu.
        public int So_phieu { get; set; }  // So phieu tang dan trong 1 mang.
        public int TGian_C1 { get; set; }    // Thoi gian ms- 1 bao qua dau dem1
        public int TGian_C2 { get; set; }   // Thoi gian ms- 1 bao qua dau dem2
        public int Tong_bao { get; set; }  // Tong so bao tich luy
        public byte Bao_dong { get; set; }  // =1 dinh bao dd1,=2 dinh bao dd2,=4 mat nguon,=8 lech bao,=16 qua so loi quy din
        public ushort IN_So_batdau { get; set; }   // So bao da qua dau in xuong phuong tien khi bat dau chay bang
        public ushort Luong_in_them { get; set; }
    }
    public class DanhSachTagdb104
    {
        public bool HMI_Tam_dung { get; set; }  // =0 tam dung, =1 ket thuc
        public bool HMI_Nhap_phieu { get; set; }  // =0 ket thuc phieu, =1 nhap phieu de giam sat san sang xuat
        public bool YC_bat_in { get; set; }   // =1 sau khi nhap phieu 2s và yêu cau PM máy in phun hoat dong
        public bool YC_Tat_in { get; set; }   // =1 khi ket thuc phieu, yeu cau may in tat
        public bool YC_in_them { get; set; }    // =1 khi yeu cau san sang lai may in
        public bool DB_IN1 { get; set; } // du phong1
        public bool DB_IN2 { get; set; }   // du phong2
        public bool DB_IN3 { get; set; }  // du phong3
        public bool Bang_san_sang { get; set; }   // =0 mat san sang cho phep bang chay, =1 san sang cho phep bang chay
        public bool NguonOK { get; set; }   // Nguon24V ok
        public bool Bang_dang_chay { get; set; }    // =1 khi bang dang chay, =0 khi bang dung
        public bool Dang_In { get; set; }  // =1 Báo máy in dang chay
        public bool Cho_phep_tat_in { get; set; }   // =1 Cho phep may in tat khi du 
        public bool OUT1 { get; set; }   // du phong out1
        public bool OUT2 { get; set; } // du phong out2
        public bool OUT3 { get; set; }   // du phong out3
        public ushort Luong_Dat_S1 { get; set; }   // Luong bao dat lan 1 truoc khi nhap phieu
        public ushort Luong_Dat_S2 { get; set; }   // Luong bao dat lan 2 sau khi nhap phieu dung de nhap xuat bo xung bao thieu
        public ushort Luong_Dat_S { get; set; }   // Luong_Dat_S=Luong_Dat_S1+Luong_Dat_S2
        public ushort So_dem1 { get; set; }  // So dem cua dau dem 1( trong 1 phieu)
        public ushort So_dem2 { get; set; }  // So dem cua dau dem 2( trong 1 phieu)
        public ushort Bao_qua_dd { get; set; }  // So bao cau xuong de tru luong dat xuat
        public ushort So_loi { get; set; }   // So loi bao dong trong 1 phieu.
        public int So_phieu { get; set; }  // So phieu tang dan trong 1 mang.
        public int TGian_C1 { get; set; }    // Thoi gian ms- 1 bao qua dau dem1
        public int TGian_C2 { get; set; }   // Thoi gian ms- 1 bao qua dau dem2
        public int Tong_bao { get; set; }  // Tong so bao tich luy
        public byte Bao_dong { get; set; }  // =1 dinh bao dd1,=2 dinh bao dd2,=4 mat nguon,=8 lech bao,=16 qua so loi quy din
        public ushort IN_So_batdau { get; set; }   // So bao da qua dau in xuong phuong tien khi bat dau chay bang
        public ushort Luong_in_them { get; set; }
    }
    public class DanhSachTagdb105
    {
        public bool HMI_Tam_dung { get; set; }  // =0 tam dung, =1 ket thuc
        public bool HMI_Nhap_phieu { get; set; }  // =0 ket thuc phieu, =1 nhap phieu de giam sat san sang xuat
        public bool YC_bat_in { get; set; }   // =1 sau khi nhap phieu 2s và yêu cau PM máy in phun hoat dong
        public bool YC_Tat_in { get; set; }   // =1 khi ket thuc phieu, yeu cau may in tat
        public bool YC_in_them { get; set; }    // =1 khi yeu cau san sang lai may in
        public bool DB_IN1 { get; set; } // du phong1
        public bool DB_IN2 { get; set; }   // du phong2
        public bool DB_IN3 { get; set; }  // du phong3
        public bool Bang_san_sang { get; set; }   // =0 mat san sang cho phep bang chay, =1 san sang cho phep bang chay
        public bool NguonOK { get; set; }   // Nguon24V ok
        public bool Bang_dang_chay { get; set; }    // =1 khi bang dang chay, =0 khi bang dung
        public bool Dang_In { get; set; }  // =1 Báo máy in dang chay
        public bool Cho_phep_tat_in { get; set; }   // =1 Cho phep may in tat khi du 
        public bool OUT1 { get; set; }   // du phong out1
        public bool OUT2 { get; set; } // du phong out2
        public bool OUT3 { get; set; }   // du phong out3
        public ushort Luong_Dat_S1 { get; set; }   // Luong bao dat lan 1 truoc khi nhap phieu
        public ushort Luong_Dat_S2 { get; set; }   // Luong bao dat lan 2 sau khi nhap phieu dung de nhap xuat bo xung bao thieu
        public ushort Luong_Dat_S { get; set; }   // Luong_Dat_S=Luong_Dat_S1+Luong_Dat_S2
        public ushort So_dem1 { get; set; }  // So dem cua dau dem 1( trong 1 phieu)
        public ushort So_dem2 { get; set; }  // So dem cua dau dem 2( trong 1 phieu)
        public ushort Bao_qua_dd { get; set; }  // So bao cau xuong de tru luong dat xuat
        public ushort So_loi { get; set; }   // So loi bao dong trong 1 phieu.
        public int So_phieu { get; set; }  // So phieu tang dan trong 1 mang.
        public int TGian_C1 { get; set; }    // Thoi gian ms- 1 bao qua dau dem1
        public int TGian_C2 { get; set; }   // Thoi gian ms- 1 bao qua dau dem2
        public int Tong_bao { get; set; }  // Tong so bao tich luy
        public byte Bao_dong { get; set; }  // =1 dinh bao dd1,=2 dinh bao dd2,=4 mat nguon,=8 lech bao,=16 qua so loi quy din
        public ushort IN_So_batdau { get; set; }   // So bao da qua dau in xuong phuong tien khi bat dau chay bang
        public ushort Luong_in_them { get; set; }
    }
    public class DanhSachTagdb106
    {
        public bool HMI_Tam_dung { get; set; }  // =0 tam dung, =1 ket thuc
        public bool HMI_Nhap_phieu { get; set; }  // =0 ket thuc phieu, =1 nhap phieu de giam sat san sang xuat
        public bool YC_bat_in { get; set; }   // =1 sau khi nhap phieu 2s và yêu cau PM máy in phun hoat dong
        public bool YC_Tat_in { get; set; }   // =1 khi ket thuc phieu, yeu cau may in tat
        public bool YC_in_them { get; set; }    // =1 khi yeu cau san sang lai may in
        public bool DB_IN1 { get; set; } // du phong1
        public bool DB_IN2 { get; set; }   // du phong2
        public bool DB_IN3 { get; set; }  // du phong3
        public bool Bang_san_sang { get; set; }   // =0 mat san sang cho phep bang chay, =1 san sang cho phep bang chay
        public bool NguonOK { get; set; }   // Nguon24V ok
        public bool Bang_dang_chay { get; set; }    // =1 khi bang dang chay, =0 khi bang dung
        public bool Dang_In { get; set; }  // =1 Báo máy in dang chay
        public bool Cho_phep_tat_in { get; set; }   // =1 Cho phep may in tat khi du 
        public bool OUT1 { get; set; }   // du phong out1
        public bool OUT2 { get; set; } // du phong out2
        public bool OUT3 { get; set; }   // du phong out3
        public ushort Luong_Dat_S1 { get; set; }   // Luong bao dat lan 1 truoc khi nhap phieu
        public ushort Luong_Dat_S2 { get; set; }   // Luong bao dat lan 2 sau khi nhap phieu dung de nhap xuat bo xung bao thieu
        public ushort Luong_Dat_S { get; set; }   // Luong_Dat_S=Luong_Dat_S1+Luong_Dat_S2
        public ushort So_dem1 { get; set; }  // So dem cua dau dem 1( trong 1 phieu)
        public ushort So_dem2 { get; set; }  // So dem cua dau dem 2( trong 1 phieu)
        public ushort Bao_qua_dd { get; set; }  // So bao cau xuong de tru luong dat xuat
        public ushort So_loi { get; set; }   // So loi bao dong trong 1 phieu.
        public int So_phieu { get; set; }  // So phieu tang dan trong 1 mang.
        public int TGian_C1 { get; set; }    // Thoi gian ms- 1 bao qua dau dem1
        public int TGian_C2 { get; set; }   // Thoi gian ms- 1 bao qua dau dem2
        public int Tong_bao { get; set; }  // Tong so bao tich luy
        public byte Bao_dong { get; set; }  // =1 dinh bao dd1,=2 dinh bao dd2,=4 mat nguon,=8 lech bao,=16 qua so loi quy din
        public ushort IN_So_batdau { get; set; }   // So bao da qua dau in xuong phuong tien khi bat dau chay bang
        public ushort Luong_in_them { get; set; }
    }
    public class DanhSachTagdb107
    {
        public bool HMI_Tam_dung { get; set; }  // =0 tam dung, =1 ket thuc
        public bool HMI_Nhap_phieu { get; set; }  // =0 ket thuc phieu, =1 nhap phieu de giam sat san sang xuat
        public bool YC_bat_in { get; set; }   // =1 sau khi nhap phieu 2s và yêu cau PM máy in phun hoat dong
        public bool YC_Tat_in { get; set; }   // =1 khi ket thuc phieu, yeu cau may in tat
        public bool YC_in_them { get; set; }    // =1 khi yeu cau san sang lai may in
        public bool DB_IN1 { get; set; } // du phong1
        public bool DB_IN2 { get; set; }   // du phong2
        public bool DB_IN3 { get; set; }  // du phong3
        public bool Bang_san_sang { get; set; }   // =0 mat san sang cho phep bang chay, =1 san sang cho phep bang chay
        public bool NguonOK { get; set; }   // Nguon24V ok
        public bool Bang_dang_chay { get; set; }    // =1 khi bang dang chay, =0 khi bang dung
        public bool Dang_In { get; set; }  // =1 Báo máy in dang chay
        public bool Cho_phep_tat_in { get; set; }   // =1 Cho phep may in tat khi du 
        public bool OUT1 { get; set; }   // du phong out1
        public bool OUT2 { get; set; } // du phong out2
        public bool OUT3 { get; set; }   // du phong out3
        public ushort Luong_Dat_S1 { get; set; }   // Luong bao dat lan 1 truoc khi nhap phieu
        public ushort Luong_Dat_S2 { get; set; }   // Luong bao dat lan 2 sau khi nhap phieu dung de nhap xuat bo xung bao thieu
        public ushort Luong_Dat_S { get; set; }   // Luong_Dat_S=Luong_Dat_S1+Luong_Dat_S2
        public ushort So_dem1 { get; set; }  // So dem cua dau dem 1( trong 1 phieu)
        public ushort So_dem2 { get; set; }  // So dem cua dau dem 2( trong 1 phieu)
        public ushort Bao_qua_dd { get; set; }  // So bao cau xuong de tru luong dat xuat
        public ushort So_loi { get; set; }   // So loi bao dong trong 1 phieu.
        public int So_phieu { get; set; }  // So phieu tang dan trong 1 mang.
        public int TGian_C1 { get; set; }    // Thoi gian ms- 1 bao qua dau dem1
        public int TGian_C2 { get; set; }   // Thoi gian ms- 1 bao qua dau dem2
        public int Tong_bao { get; set; }  // Tong so bao tich luy
        public byte Bao_dong { get; set; }  // =1 dinh bao dd1,=2 dinh bao dd2,=4 mat nguon,=8 lech bao,=16 qua so loi quy din
        public ushort IN_So_batdau { get; set; }   // So bao da qua dau in xuong phuong tien khi bat dau chay bang
        public ushort Luong_in_them { get; set; }
    }
    public class DanhSachTagdb108
    {
        public bool HMI_Tam_dung { get; set; }  // =0 tam dung, =1 ket thuc
        public bool HMI_Nhap_phieu { get; set; }  // =0 ket thuc phieu, =1 nhap phieu de giam sat san sang xuat
        public bool YC_bat_in { get; set; }   // =1 sau khi nhap phieu 2s và yêu cau PM máy in phun hoat dong
        public bool YC_Tat_in { get; set; }   // =1 khi ket thuc phieu, yeu cau may in tat
        public bool YC_in_them { get; set; }    // =1 khi yeu cau san sang lai may in
        public bool DB_IN1 { get; set; } // du phong1
        public bool DB_IN2 { get; set; }   // du phong2
        public bool DB_IN3 { get; set; }  // du phong3
        public bool Bang_san_sang { get; set; }   // =0 mat san sang cho phep bang chay, =1 san sang cho phep bang chay
        public bool NguonOK { get; set; }   // Nguon24V ok
        public bool Bang_dang_chay { get; set; }    // =1 khi bang dang chay, =0 khi bang dung
        public bool Dang_In { get; set; }  // =1 Báo máy in dang chay
        public bool Cho_phep_tat_in { get; set; }   // =1 Cho phep may in tat khi du 
        public bool OUT1 { get; set; }   // du phong out1
        public bool OUT2 { get; set; } // du phong out2
        public bool OUT3 { get; set; }   // du phong out3
        public ushort Luong_Dat_S1 { get; set; }   // Luong bao dat lan 1 truoc khi nhap phieu
        public ushort Luong_Dat_S2 { get; set; }   // Luong bao dat lan 2 sau khi nhap phieu dung de nhap xuat bo xung bao thieu
        public ushort Luong_Dat_S { get; set; }   // Luong_Dat_S=Luong_Dat_S1+Luong_Dat_S2
        public ushort So_dem1 { get; set; }  // So dem cua dau dem 1( trong 1 phieu)
        public ushort So_dem2 { get; set; }  // So dem cua dau dem 2( trong 1 phieu)
        public ushort Bao_qua_dd { get; set; }  // So bao cau xuong de tru luong dat xuat
        public ushort So_loi { get; set; }   // So loi bao dong trong 1 phieu.
        public int So_phieu { get; set; }  // So phieu tang dan trong 1 mang.
        public int TGian_C1 { get; set; }    // Thoi gian ms- 1 bao qua dau dem1
        public int TGian_C2 { get; set; }   // Thoi gian ms- 1 bao qua dau dem2
        public int Tong_bao { get; set; }  // Tong so bao tich luy
        public byte Bao_dong { get; set; }  // =1 dinh bao dd1,=2 dinh bao dd2,=4 mat nguon,=8 lech bao,=16 qua so loi quy din
        public ushort IN_So_batdau { get; set; }   // So bao da qua dau in xuong phuong tien khi bat dau chay bang
        public ushort Luong_in_them { get; set; }
    }
    public class DanhSachTagdb109
    {
        public bool HMI_Tam_dung { get; set; }  // =0 tam dung, =1 ket thuc
        public bool HMI_Nhap_phieu { get; set; }  // =0 ket thuc phieu, =1 nhap phieu de giam sat san sang xuat
        public bool YC_bat_in { get; set; }   // =1 sau khi nhap phieu 2s và yêu cau PM máy in phun hoat dong
        public bool YC_Tat_in { get; set; }   // =1 khi ket thuc phieu, yeu cau may in tat
        public bool YC_in_them { get; set; }    // =1 khi yeu cau san sang lai may in
        public bool DB_IN1 { get; set; } // du phong1
        public bool DB_IN2 { get; set; }   // du phong2
        public bool DB_IN3 { get; set; }  // du phong3
        public bool Bang_san_sang { get; set; }   // =0 mat san sang cho phep bang chay, =1 san sang cho phep bang chay
        public bool NguonOK { get; set; }   // Nguon24V ok
        public bool Bang_dang_chay { get; set; }    // =1 khi bang dang chay, =0 khi bang dung
        public bool Dang_In { get; set; }  // =1 Báo máy in dang chay
        public bool Cho_phep_tat_in { get; set; }   // =1 Cho phep may in tat khi du 
        public bool OUT1 { get; set; }   // du phong out1
        public bool OUT2 { get; set; } // du phong out2
        public bool OUT3 { get; set; }   // du phong out3
        public ushort Luong_Dat_S1 { get; set; }   // Luong bao dat lan 1 truoc khi nhap phieu
        public ushort Luong_Dat_S2 { get; set; }   // Luong bao dat lan 2 sau khi nhap phieu dung de nhap xuat bo xung bao thieu
        public ushort Luong_Dat_S { get; set; }   // Luong_Dat_S=Luong_Dat_S1+Luong_Dat_S2
        public ushort So_dem1 { get; set; }  // So dem cua dau dem 1( trong 1 phieu)
        public ushort So_dem2 { get; set; }  // So dem cua dau dem 2( trong 1 phieu)
        public ushort Bao_qua_dd { get; set; }  // So bao cau xuong de tru luong dat xuat
        public ushort So_loi { get; set; }   // So loi bao dong trong 1 phieu.
        public int So_phieu { get; set; }  // So phieu tang dan trong 1 mang.
        public int TGian_C1 { get; set; }    // Thoi gian ms- 1 bao qua dau dem1
        public int TGian_C2 { get; set; }   // Thoi gian ms- 1 bao qua dau dem2
        public int Tong_bao { get; set; }  // Tong so bao tich luy
        public byte Bao_dong { get; set; }  // =1 dinh bao dd1,=2 dinh bao dd2,=4 mat nguon,=8 lech bao,=16 qua so loi quy din
        public ushort IN_So_batdau { get; set; }   // So bao da qua dau in xuong phuong tien khi bat dau chay bang
        public ushort Luong_in_them { get; set; }
    }
    public class DanhSachTagdb110
    {
        public bool HMI_Tam_dung { get; set; }  // =0 tam dung, =1 ket thuc
        public bool HMI_Nhap_phieu { get; set; }  // =0 ket thuc phieu, =1 nhap phieu de giam sat san sang xuat
        public bool YC_bat_in { get; set; }   // =1 sau khi nhap phieu 2s và yêu cau PM máy in phun hoat dong
        public bool YC_Tat_in { get; set; }   // =1 khi ket thuc phieu, yeu cau may in tat
        public bool YC_in_them { get; set; }    // =1 khi yeu cau san sang lai may in
        public bool DB_IN1 { get; set; } // du phong1
        public bool DB_IN2 { get; set; }   // du phong2
        public bool DB_IN3 { get; set; }  // du phong3
        public bool Bang_san_sang { get; set; }   // =0 mat san sang cho phep bang chay, =1 san sang cho phep bang chay
        public bool NguonOK { get; set; }   // Nguon24V ok
        public bool Bang_dang_chay { get; set; }    // =1 khi bang dang chay, =0 khi bang dung
        public bool Dang_In { get; set; }  // =1 Báo máy in dang chay
        public bool Cho_phep_tat_in { get; set; }   // =1 Cho phep may in tat khi du 
        public bool OUT1 { get; set; }   // du phong out1
        public bool OUT2 { get; set; } // du phong out2
        public bool OUT3 { get; set; }   // du phong out3
        public ushort Luong_Dat_S1 { get; set; }   // Luong bao dat lan 1 truoc khi nhap phieu
        public ushort Luong_Dat_S2 { get; set; }   // Luong bao dat lan 2 sau khi nhap phieu dung de nhap xuat bo xung bao thieu
        public ushort Luong_Dat_S { get; set; }   // Luong_Dat_S=Luong_Dat_S1+Luong_Dat_S2
        public ushort So_dem1 { get; set; }  // So dem cua dau dem 1( trong 1 phieu)
        public ushort So_dem2 { get; set; }  // So dem cua dau dem 2( trong 1 phieu)
        public ushort Bao_qua_dd { get; set; }  // So bao cau xuong de tru luong dat xuat
        public ushort So_loi { get; set; }   // So loi bao dong trong 1 phieu.
        public int So_phieu { get; set; }  // So phieu tang dan trong 1 mang.
        public int TGian_C1 { get; set; }    // Thoi gian ms- 1 bao qua dau dem1
        public int TGian_C2 { get; set; }   // Thoi gian ms- 1 bao qua dau dem2
        public int Tong_bao { get; set; }  // Tong so bao tich luy
        public byte Bao_dong { get; set; }  // =1 dinh bao dd1,=2 dinh bao dd2,=4 mat nguon,=8 lech bao,=16 qua so loi quy din
        public ushort IN_So_batdau { get; set; }   // So bao da qua dau in xuong phuong tien khi bat dau chay bang
        public ushort Luong_in_them { get; set; }
    }
    public class DanhSachTagdb111
    {
        public bool HMI_Tam_dung { get; set; }  // =0 tam dung, =1 ket thuc
        public bool HMI_Nhap_phieu { get; set; }  // =0 ket thuc phieu, =1 nhap phieu de giam sat san sang xuat
        public bool YC_bat_in { get; set; }   // =1 sau khi nhap phieu 2s và yêu cau PM máy in phun hoat dong
        public bool YC_Tat_in { get; set; }   // =1 khi ket thuc phieu, yeu cau may in tat
        public bool YC_in_them { get; set; }    // =1 khi yeu cau san sang lai may in
        public bool DB_IN1 { get; set; } // du phong1
        public bool DB_IN2 { get; set; }   // du phong2
        public bool DB_IN3 { get; set; }  // du phong3
        public bool Bang_san_sang { get; set; }   // =0 mat san sang cho phep bang chay, =1 san sang cho phep bang chay
        public bool NguonOK { get; set; }   // Nguon24V ok
        public bool Bang_dang_chay { get; set; }    // =1 khi bang dang chay, =0 khi bang dung
        public bool Dang_In { get; set; }  // =1 Báo máy in dang chay
        public bool Cho_phep_tat_in { get; set; }   // =1 Cho phep may in tat khi du 
        public bool OUT1 { get; set; }   // du phong out1
        public bool OUT2 { get; set; } // du phong out2
        public bool OUT3 { get; set; }   // du phong out3
        public ushort Luong_Dat_S1 { get; set; }   // Luong bao dat lan 1 truoc khi nhap phieu
        public ushort Luong_Dat_S2 { get; set; }   // Luong bao dat lan 2 sau khi nhap phieu dung de nhap xuat bo xung bao thieu
        public ushort Luong_Dat_S { get; set; }   // Luong_Dat_S=Luong_Dat_S1+Luong_Dat_S2
        public ushort So_dem1 { get; set; }  // So dem cua dau dem 1( trong 1 phieu)
        public ushort So_dem2 { get; set; }  // So dem cua dau dem 2( trong 1 phieu)
        public ushort Bao_qua_dd { get; set; }  // So bao cau xuong de tru luong dat xuat
        public ushort So_loi { get; set; }   // So loi bao dong trong 1 phieu.
        public int So_phieu { get; set; }  // So phieu tang dan trong 1 mang.
        public int TGian_C1 { get; set; }    // Thoi gian ms- 1 bao qua dau dem1
        public int TGian_C2 { get; set; }   // Thoi gian ms- 1 bao qua dau dem2
        public int Tong_bao { get; set; }  // Tong so bao tich luy
        public byte Bao_dong { get; set; }  // =1 dinh bao dd1,=2 dinh bao dd2,=4 mat nguon,=8 lech bao,=16 qua so loi quy din
        public ushort IN_So_batdau { get; set; }   // So bao da qua dau in xuong phuong tien khi bat dau chay bang
        public ushort Luong_in_them { get; set; }
    }
    public class DanhSachTagdb112
    {
        public bool HMI_Tam_dung { get; set; }  // =0 tam dung, =1 ket thuc
        public bool HMI_Nhap_phieu { get; set; }  // =0 ket thuc phieu, =1 nhap phieu de giam sat san sang xuat
        public bool YC_bat_in { get; set; }   // =1 sau khi nhap phieu 2s và yêu cau PM máy in phun hoat dong
        public bool YC_Tat_in { get; set; }   // =1 khi ket thuc phieu, yeu cau may in tat
        public bool YC_in_them { get; set; }    // =1 khi yeu cau san sang lai may in
        public bool DB_IN1 { get; set; } // du phong1
        public bool DB_IN2 { get; set; }   // du phong2
        public bool DB_IN3 { get; set; }  // du phong3
        public bool Bang_san_sang { get; set; }   // =0 mat san sang cho phep bang chay, =1 san sang cho phep bang chay
        public bool NguonOK { get; set; }   // Nguon24V ok
        public bool Bang_dang_chay { get; set; }    // =1 khi bang dang chay, =0 khi bang dung
        public bool Dang_In { get; set; }  // =1 Báo máy in dang chay
        public bool Cho_phep_tat_in { get; set; }   // =1 Cho phep may in tat khi du 
        public bool OUT1 { get; set; }   // du phong out1
        public bool OUT2 { get; set; } // du phong out2
        public bool OUT3 { get; set; }   // du phong out3
        public ushort Luong_Dat_S1 { get; set; }   // Luong bao dat lan 1 truoc khi nhap phieu
        public ushort Luong_Dat_S2 { get; set; }   // Luong bao dat lan 2 sau khi nhap phieu dung de nhap xuat bo xung bao thieu
        public ushort Luong_Dat_S { get; set; }   // Luong_Dat_S=Luong_Dat_S1+Luong_Dat_S2
        public ushort So_dem1 { get; set; }  // So dem cua dau dem 1( trong 1 phieu)
        public ushort So_dem2 { get; set; }  // So dem cua dau dem 2( trong 1 phieu)
        public ushort Bao_qua_dd { get; set; }  // So bao cau xuong de tru luong dat xuat
        public ushort So_loi { get; set; }   // So loi bao dong trong 1 phieu.
        public int So_phieu { get; set; }  // So phieu tang dan trong 1 mang.
        public int TGian_C1 { get; set; }    // Thoi gian ms- 1 bao qua dau dem1
        public int TGian_C2 { get; set; }   // Thoi gian ms- 1 bao qua dau dem2
        public int Tong_bao { get; set; }  // Tong so bao tich luy
        public byte Bao_dong { get; set; }  // =1 dinh bao dd1,=2 dinh bao dd2,=4 mat nguon,=8 lech bao,=16 qua so loi quy din
        public ushort IN_So_batdau { get; set; }   // So bao da qua dau in xuong phuong tien khi bat dau chay bang
        public ushort Luong_in_them { get; set; }
    }
    public class DanhSachTagdb113
    {
        public bool HMI_Tam_dung { get; set; }  // =0 tam dung, =1 ket thuc
        public bool HMI_Nhap_phieu { get; set; }  // =0 ket thuc phieu, =1 nhap phieu de giam sat san sang xuat
        public bool YC_bat_in { get; set; }   // =1 sau khi nhap phieu 2s và yêu cau PM máy in phun hoat dong
        public bool YC_Tat_in { get; set; }   // =1 khi ket thuc phieu, yeu cau may in tat
        public bool YC_in_them { get; set; }    // =1 khi yeu cau san sang lai may in
        public bool DB_IN1 { get; set; } // du phong1
        public bool DB_IN2 { get; set; }   // du phong2
        public bool DB_IN3 { get; set; }  // du phong3
        public bool Bang_san_sang { get; set; }   // =0 mat san sang cho phep bang chay, =1 san sang cho phep bang chay
        public bool NguonOK { get; set; }   // Nguon24V ok
        public bool Bang_dang_chay { get; set; }    // =1 khi bang dang chay, =0 khi bang dung
        public bool Dang_In { get; set; }  // =1 Báo máy in dang chay
        public bool Cho_phep_tat_in { get; set; }   // =1 Cho phep may in tat khi du 
        public bool OUT1 { get; set; }   // du phong out1
        public bool OUT2 { get; set; } // du phong out2
        public bool OUT3 { get; set; }   // du phong out3
        public ushort Luong_Dat_S1 { get; set; }   // Luong bao dat lan 1 truoc khi nhap phieu
        public ushort Luong_Dat_S2 { get; set; }   // Luong bao dat lan 2 sau khi nhap phieu dung de nhap xuat bo xung bao thieu
        public ushort Luong_Dat_S { get; set; }   // Luong_Dat_S=Luong_Dat_S1+Luong_Dat_S2
        public ushort So_dem1 { get; set; }  // So dem cua dau dem 1( trong 1 phieu)
        public ushort So_dem2 { get; set; }  // So dem cua dau dem 2( trong 1 phieu)
        public ushort Bao_qua_dd { get; set; }  // So bao cau xuong de tru luong dat xuat
        public ushort So_loi { get; set; }   // So loi bao dong trong 1 phieu.
        public int So_phieu { get; set; }  // So phieu tang dan trong 1 mang.
        public int TGian_C1 { get; set; }    // Thoi gian ms- 1 bao qua dau dem1
        public int TGian_C2 { get; set; }   // Thoi gian ms- 1 bao qua dau dem2
        public int Tong_bao { get; set; }  // Tong so bao tich luy
        public byte Bao_dong { get; set; }  // =1 dinh bao dd1,=2 dinh bao dd2,=4 mat nguon,=8 lech bao,=16 qua so loi quy din
        public ushort IN_So_batdau { get; set; }   // So bao da qua dau in xuong phuong tien khi bat dau chay bang
        public ushort Luong_in_them { get; set; }
    }

    public class DanhSachTagDB300
    {
        public int Sophieu { get; set; }
        public int Setpoint { get; set; }
        public int KLTongPhieu { get; set; }
        public int tong_can { get; set; }
        public int KLCan1 { get; set; }
        public int KLTong1 { get; set; }
        public int Min_ket1 { get; set; }
        public int Max_ket1 { get; set; }
        public int Ncham_ket1 { get; set; }
        public short Buoc_chay_1 { get; set; }
        public byte Trangthai_1 { get; set; }
        public int KLCan2 { get; set; }
        public int KLTong2 { get; set; }
        public int Min_ket2 { get; set; }
        public int Max_ket2 { get; set; }
        public int Ncham_ket2 { get; set; }
        public short Buoc_chay_2 { get; set; }
        public byte Trangthai_2 { get; set; }
        public bool HMI_Nhapphieu { get; set; }
        public bool HMI_Modechay { get; set; }
        public bool HMI_Tamdung { get; set; }
        public bool HMI_chay_rut_lieu { get; set; }
        public bool Caplieuchay { get; set; }
        public bool Thaolieuchay { get; set; }
        public bool Baodongchung { get; set; }
        public bool Yeu_cau_luuhang1 { get; set; }
        public bool Yeu_cau_luuhang2 { get; set; }
        public bool Chophepketthuc { get; set; }
        public bool Ket1_Caplieu { get; set; }
        public bool Ket1_thaolieu { get; set; }
        public bool Ket2_Caplieu { get; set; }
        public bool Ket2_thaolieu { get; set; }
        public bool Yeu_cau_luubi1 { get; set; }
        public bool Yeu_cau_luubi2 { get; set; }
        public bool LocbuiP40_Run { get; set; }
        public bool LocbuiP41_Run { get; set; }
        public bool STAT12 { get; set; }
        public int Tongbd { get; set; }
        public int Tongkt { get; set; }
        public bool Yc_caplieuK1 { get; set; }
        public bool Yc_caplieuK2 { get; set; }
        public bool Yc_xalieuK1 { get; set; }
        public bool Yc_xalieuK2 { get; set; }
        public bool DuPhong1 { get; set; }
        public bool DuPhong2 { get; set; }
        public bool DuPhong3 { get; set; }
        public bool DuPhong4 { get; set; }
    }

    public class DanhSachTagDB304
    {
        public int KL_can { get; set; }
        public int KLTong1 { get; set; }
        public bool Luu_xong_hang { get; set; }
        public bool Luu_xong_bi { get; set; }
        public bool Resset_xong { get; set; }
        public bool Loi_chot_KL { get; set; }
        public short Buoc_can { get; set; }
        public bool Reset { get; set; }
        public bool CMD_Resset { get; set; }
        public bool CMD_chotKL { get; set; }
        public bool YC_luubi { get; set; }
        public bool YC_luuhang { get; set; }
        public int KLbi1 { get; set; }
        public int KLbi2 { get; set; }
        public int KLbi3 { get; set; }
        public int KLbi4 { get; set; }
        public int KLbi5 { get; set; }
        public int KLbi6 { get; set; }
        public int KLbi7 { get; set; }
        public int KLbi8 { get; set; }
        public int KLbi9 { get; set; }
        public int KLbi10 { get; set; }
        public int KLbi11 { get; set; }
        public int KLbi12 { get; set; }
        public int KLbi13 { get; set; }
        public int KLbi14 { get; set; }
        public int KLbi15 { get; set; }
        public int KLbi16 { get; set; }
        public int KLbi17 { get; set; }
        public int KLbi18 { get; set; }
        public int KLbi19 { get; set; }
        public int KLbi20 { get; set; }
        public int KLbi21 { get; set; }
        public int KLbi22 { get; set; }
        public int KLbi23 { get; set; }
        public int KLbi24 { get; set; }
        public int KLbi25 { get; set; }
        public int KLbi26 { get; set; }
        public int KLbi27 { get; set; }
        public int KLbi28 { get; set; }
        public int KLbi29 { get; set; }
        public int KLbi30 { get; set; }
        public int KLbi31 { get; set; }
        public int KLbi32 { get; set; }
        public int KLbi33 { get; set; }
        public int KLbi34 { get; set; }
        public int KLbi35 { get; set; }
        public int KLbi36 { get; set; }
        public int KLbi37 { get; set; }
        public int KLbi38 { get; set; }
        public int KLbi39 { get; set; }
        public int KLbi40 { get; set; }
        public int KLbi41 { get; set; }
        public int KLbi42 { get; set; }
        public int KLbi43 { get; set; }
        public int KLbi44 { get; set; }
        public int KLbi45 { get; set; }
        public int KLbi46 { get; set; }
        public int KLbi47 { get; set; }
        public int KLbi48 { get; set; }
        public int KLbi49 { get; set; }
        public int KLbi50 { get; set; }
        public int KLhang1 { get; set; }
        public int KLhang2 { get; set; }
        public int KLhang3 { get; set; }
        public int KLhang4 { get; set; }
        public int KLhang5 { get; set; }
        public int KLhang6 { get; set; }
        public int KLhang7 { get; set; }
        public int KLhang8 { get; set; }
        public int KLhang9 { get; set; }
        public int KLhang10 { get; set; }
        public int KLhang11 { get; set; }
        public int KLhang12 { get; set; }
        public int KLhang13 { get; set; }
        public int KLhang14 { get; set; }
        public int KLhang15 { get; set; }
        public int KLhang16 { get; set; }
        public int KLhang17 { get; set; }
        public int KLhang18 { get; set; }
        public int KLhang19 { get; set; }
        public int KLhang20 { get; set; }
        public int KLhang21 { get; set; }
        public int KLhang22 { get; set; }
        public int KLhang23 { get; set; }
        public int KLhang24 { get; set; }
        public int KLhang25 { get; set; }
        public int KLhang26 { get; set; }
        public int KLhang27 { get; set; }
        public int KLhang28 { get; set; }
        public int KLhang29 { get; set; }
        public int KLhang30 { get; set; }
        public int KLhang31 { get; set; }
        public int KLhang32 { get; set; }
        public int KLhang33 { get; set; }
        public int KLhang34 { get; set; }
        public int KLhang35 { get; set; }
        public int KLhang36 { get; set; }
        public int KLhang37 { get; set; }
        public int KLhang38 { get; set; }
        public int KLhang39 { get; set; }
        public int KLhang40 { get; set; }
        public int KLhang41 { get; set; }
        public int KLhang42 { get; set; }
        public int KLhang43 { get; set; }
        public int KLhang44 { get; set; }
        public int KLhang45 { get; set; }
        public int KLhang46 { get; set; }
        public int KLhang47 { get; set; }
        public int KLhang48 { get; set; }
        public int KLhang49 { get; set; }
        public int KLhang50 { get; set; }
        public int KLGiao1 { get; set; }
        public int KLGiao2 { get; set; }
        public int KLGiao3 { get; set; }
        public int KLGiao4 { get; set; }
        public int KLGiao5 { get; set; }
        public int KLGiao6 { get; set; }
        public int KLGiao7 { get; set; }
        public int KLGiao8 { get; set; }
        public int KLGiao9 { get; set; }
        public int KLGiao10 { get; set; }
        public int KLGiao11 { get; set; }
        public int KLGiao12 { get; set; }
        public int KLGiao13 { get; set; }
        public int KLGiao14 { get; set; }
        public int KLGiao15 { get; set; }
        public int KLGiao16 { get; set; }
        public int KLGiao17 { get; set; }
        public int KLGiao18 { get; set; }
        public int KLGiao19 { get; set; }
        public int KLGiao20 { get; set; }
        public int KLGiao21 { get; set; }
        public int KLGiao22 { get; set; }
        public int KLGiao23 { get; set; }
        public int KLGiao24 { get; set; }
        public int KLGiao25 { get; set; }
        public int KLGiao26 { get; set; }
        public int KLGiao27 { get; set; }
        public int KLGiao28 { get; set; }
        public int KLGiao29 { get; set; }
        public int KLGiao30 { get; set; }
        public int KLGiao31 { get; set; }
        public int KLGiao32 { get; set; }
        public int KLGiao33 { get; set; }
        public int KLGiao34 { get; set; }
        public int KLGiao35 { get; set; }
        public int KLGiao36 { get; set; }
        public int KLGiao37 { get; set; }
        public int KLGiao38 { get; set; }
        public int KLGiao39 { get; set; }
        public int KLGiao40 { get; set; }
        public int KLGiao41 { get; set; }
        public int KLGiao42 { get; set; }
        public int KLGiao43 { get; set; }
        public int KLGiao44 { get; set; }
        public int KLGiao45 { get; set; }
        public int KLGiao46 { get; set; }
        public int KLGiao47 { get; set; }
        public int KLGiao48 { get; set; }
        public int KLGiao49 { get; set; }
        public int KLGiao50 { get; set; }

        public short Check_step { get; set; }
        public int KLTong { get; set; }
    }
    public class DanhSachTagDB305
    {
        public int KL_can { get; set; }
        public int KLTong1 { get; set; }
        public bool Luu_xong_hang { get; set; }
        public bool Luu_xong_bi { get; set; }
        public bool Resset_xong { get; set; }
        public bool Loi_chot_KL { get; set; }
        public short Buoc_can { get; set; }
        public bool Reset { get; set; }
        public bool CMD_Resset { get; set; }
        public bool CMD_chotKL { get; set; }
        public bool YC_luubi { get; set; }
        public bool YC_luuhang { get; set; }
        public int KLbi1 { get; set; }
        public int KLbi2 { get; set; }
        public int KLbi3 { get; set; }
        public int KLbi4 { get; set; }
        public int KLbi5 { get; set; }
        public int KLbi6 { get; set; }
        public int KLbi7 { get; set; }
        public int KLbi8 { get; set; }
        public int KLbi9 { get; set; }
        public int KLbi10 { get; set; }
        public int KLbi11 { get; set; }
        public int KLbi12 { get; set; }
        public int KLbi13 { get; set; }
        public int KLbi14 { get; set; }
        public int KLbi15 { get; set; }
        public int KLbi16 { get; set; }
        public int KLbi17 { get; set; }
        public int KLbi18 { get; set; }
        public int KLbi19 { get; set; }
        public int KLbi20 { get; set; }
        public int KLbi21 { get; set; }
        public int KLbi22 { get; set; }
        public int KLbi23 { get; set; }
        public int KLbi24 { get; set; }
        public int KLbi25 { get; set; }
        public int KLbi26 { get; set; }
        public int KLbi27 { get; set; }
        public int KLbi28 { get; set; }
        public int KLbi29 { get; set; }
        public int KLbi30 { get; set; }
        public int KLbi31 { get; set; }
        public int KLbi32 { get; set; }
        public int KLbi33 { get; set; }
        public int KLbi34 { get; set; }
        public int KLbi35 { get; set; }
        public int KLbi36 { get; set; }
        public int KLbi37 { get; set; }
        public int KLbi38 { get; set; }
        public int KLbi39 { get; set; }
        public int KLbi40 { get; set; }
        public int KLbi41 { get; set; }
        public int KLbi42 { get; set; }
        public int KLbi43 { get; set; }
        public int KLbi44 { get; set; }
        public int KLbi45 { get; set; }
        public int KLbi46 { get; set; }
        public int KLbi47 { get; set; }
        public int KLbi48 { get; set; }
        public int KLbi49 { get; set; }
        public int KLbi50 { get; set; }
        public int KLhang1 { get; set; }
        public int KLhang2 { get; set; }
        public int KLhang3 { get; set; }
        public int KLhang4 { get; set; }
        public int KLhang5 { get; set; }
        public int KLhang6 { get; set; }
        public int KLhang7 { get; set; }
        public int KLhang8 { get; set; }
        public int KLhang9 { get; set; }
        public int KLhang10 { get; set; }
        public int KLhang11 { get; set; }
        public int KLhang12 { get; set; }
        public int KLhang13 { get; set; }
        public int KLhang14 { get; set; }
        public int KLhang15 { get; set; }
        public int KLhang16 { get; set; }
        public int KLhang17 { get; set; }
        public int KLhang18 { get; set; }
        public int KLhang19 { get; set; }
        public int KLhang20 { get; set; }
        public int KLhang21 { get; set; }
        public int KLhang22 { get; set; }
        public int KLhang23 { get; set; }
        public int KLhang24 { get; set; }
        public int KLhang25 { get; set; }
        public int KLhang26 { get; set; }
        public int KLhang27 { get; set; }
        public int KLhang28 { get; set; }
        public int KLhang29 { get; set; }
        public int KLhang30 { get; set; }
        public int KLhang31 { get; set; }
        public int KLhang32 { get; set; }
        public int KLhang33 { get; set; }
        public int KLhang34 { get; set; }
        public int KLhang35 { get; set; }
        public int KLhang36 { get; set; }
        public int KLhang37 { get; set; }
        public int KLhang38 { get; set; }
        public int KLhang39 { get; set; }
        public int KLhang40 { get; set; }
        public int KLhang41 { get; set; }
        public int KLhang42 { get; set; }
        public int KLhang43 { get; set; }
        public int KLhang44 { get; set; }
        public int KLhang45 { get; set; }
        public int KLhang46 { get; set; }
        public int KLhang47 { get; set; }
        public int KLhang48 { get; set; }
        public int KLhang49 { get; set; }
        public int KLhang50 { get; set; }
        public int KLGiao1 { get; set; }
        public int KLGiao2 { get; set; }
        public int KLGiao3 { get; set; }
        public int KLGiao4 { get; set; }
        public int KLGiao5 { get; set; }
        public int KLGiao6 { get; set; }
        public int KLGiao7 { get; set; }
        public int KLGiao8 { get; set; }
        public int KLGiao9 { get; set; }
        public int KLGiao10 { get; set; }
        public int KLGiao11 { get; set; }
        public int KLGiao12 { get; set; }
        public int KLGiao13 { get; set; }
        public int KLGiao14 { get; set; }
        public int KLGiao15 { get; set; }
        public int KLGiao16 { get; set; }
        public int KLGiao17 { get; set; }
        public int KLGiao18 { get; set; }
        public int KLGiao19 { get; set; }
        public int KLGiao20 { get; set; }
        public int KLGiao21 { get; set; }
        public int KLGiao22 { get; set; }
        public int KLGiao23 { get; set; }
        public int KLGiao24 { get; set; }
        public int KLGiao25 { get; set; }
        public int KLGiao26 { get; set; }
        public int KLGiao27 { get; set; }
        public int KLGiao28 { get; set; }
        public int KLGiao29 { get; set; }
        public int KLGiao30 { get; set; }
        public int KLGiao31 { get; set; }
        public int KLGiao32 { get; set; }
        public int KLGiao33 { get; set; }
        public int KLGiao34 { get; set; }
        public int KLGiao35 { get; set; }
        public int KLGiao36 { get; set; }
        public int KLGiao37 { get; set; }
        public int KLGiao38 { get; set; }
        public int KLGiao39 { get; set; }
        public int KLGiao40 { get; set; }
        public int KLGiao41 { get; set; }
        public int KLGiao42 { get; set; }
        public int KLGiao43 { get; set; }
        public int KLGiao44 { get; set; }
        public int KLGiao45 { get; set; }
        public int KLGiao46 { get; set; }
        public int KLGiao47 { get; set; }
        public int KLGiao48 { get; set; }
        public int KLGiao49 { get; set; }
        public int KLGiao50 { get; set; }    
        public short Check_step { get; set; }
        public int KLTong { get; set; }

    }

}

