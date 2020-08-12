using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace QUANLYNHATRO1
{
    /// <summary>
    /// Summary description for QUANLYNHATRO1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class QUANLYNHATRO1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        #region Chức năng đăng nhập và đăng ký
        //Thêm chủ nhà trọ
        [WebMethod]
        public bool ThemChuNhaTro(string hoten, string cmnd, string sdt, string diachi, string email)
        {
            try
            {
                ChuNhaTro cnt = new ChuNhaTro();
                cnt.HoTen = hoten;
                cnt.CMND = cmnd;
                cnt.SDT = sdt;
                cnt.DiaChi = diachi;
                cnt.Email = email;
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                context.ChuNhaTros.InsertOnSubmit(cnt);
                context.SubmitChanges();
                return true;
            }
            catch
            {

            }
            return false;
        }
        //Thêm tài khoản
        [WebMethod]
        public bool ThemTaiKhoan(int machunhatro, string taikhoan, string matkhau)
        {
            try
            {
                DangNhap dn = new DangNhap();
                dn.MaCNT = machunhatro;
                dn.TaiKhoan = taikhoan;
                dn.MatKhau = matkhau;

                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                context.DangNhaps.InsertOnSubmit(dn);
                context.SubmitChanges();
                return true;
            }
            catch
            {

            }
            return false;
        }
        //Hiển thị danh sách tài khoản cho quản lý admin
        [WebMethod]
        public List<DangNhap> DanhSachTaiKhoan()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<DangNhap> dsdn = context.DangNhaps.ToList();
            foreach (DangNhap dn in dsdn)
            {
                dn.ChuNhaTro = null;
            }
            return dsdn;
        }
        //Kiểm tra đăng nhập
        [WebMethod]
        public int DangNhap(string taikhoan, string matkhau)
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            return context.DangNhaps.Where(x => x.TaiKhoan == taikhoan).Where(x => x.MatKhau == matkhau).Count();
        }
        //Đăng nhập user
        [WebMethod]
        public int DangNhapNguoiDung(string email, string matkhau)
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            return context.ChuNhaTros.Where(x => x.Email == email).Where(x => x.MatKhau == matkhau).Count();
        }
        #endregion
        #region Tương tác với bảng quy định
        //Hien thi quy dinh
        [WebMethod]
        public List<QuyDinh> DanhSachQuyDinh()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<QuyDinh> dsquydinh = context.QuyDinhs.ToList();
            return dsquydinh;
        }
        //Them Quy Dinh
        [WebMethod]
        public bool ThemQuyDinh(string tenquydinh, string noidung)
        {
            try
            {
                QuyDinh qd = new QuyDinh();
                qd.TenQuyDinh = tenquydinh;
                qd.NoiDung = noidung;
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                context.QuyDinhs.InsertOnSubmit(qd);
                context.SubmitChanges();
                return true;
            }
            catch
            {

            }
            return false;
        }
        //Sua Quy Dinh
        [WebMethod]
        public bool SuaQuyDinh(int maquydinh, string tenquydinh, string noidung)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                QuyDinh qd = context.QuyDinhs.FirstOrDefault(x => x.MaQuyDinh == maquydinh);
                if (qd != null)
                {
                    qd.TenQuyDinh = tenquydinh;
                    qd.NoiDung = noidung;
                    context.SubmitChanges();
                    return true;
                }


            }
            catch
            {

            }
            return false;
        }
        //Xoa Quy Dinh
        [WebMethod]
        public bool XoaQuyDinh(int maquydinh)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                QuyDinh qd = context.QuyDinhs.FirstOrDefault(x => x.MaQuyDinh == maquydinh);
                if (qd != null)
                {
                    context.QuyDinhs.DeleteOnSubmit(qd);
                    context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch
            {

            }
            return false;
        }
        #endregion
        #region Tương tác với bản khách hàng
        //Hiển thị danh sách khách hàng
        [WebMethod]
        public List<KhachHang> DanhSachKhachHang()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<KhachHang> dskh = context.KhachHangs.ToList();
            foreach (KhachHang kh in dskh)
                kh.HopDongs.Clear();
            return dskh;
        }
        //Thêm khách hàng
        [WebMethod]
        public bool ThemKhachHang(string hoten, string cmnd, string sdt, string diachi, string nghenghiep, string email)
        {
            try
            {
                KhachHang kh = new KhachHang();
                kh.HoTen = hoten;
                kh.CMND = cmnd;
                kh.SDT = sdt;
                kh.DiaChi = diachi;
                kh.NgheNghiep = nghenghiep;
                kh.Email = email;
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                context.KhachHangs.InsertOnSubmit(kh);
                context.SubmitChanges();
                return true;

            }
            catch
            {

            }
            return false;
        }
        //Sửa khách hàng
        [WebMethod]
        public bool SuaKhachHang(int makh, string hoten, string cmnd, string sdt, string diachi, string nghenghiep, string email)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                KhachHang kh = context.KhachHangs.FirstOrDefault(x => x.MaKH == makh);
                if (kh != null)
                {

                    kh.HoTen = hoten;
                    kh.CMND = cmnd;
                    kh.SDT = sdt;
                    kh.DiaChi = diachi;
                    kh.NgheNghiep = nghenghiep;
                    kh.Email = email;
                    context.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch
            {

            }
            return false;
        }
        //Xóa khách hàng
        [WebMethod]
        public bool XoaKhachHang(int makh)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                KhachHang kh = context.KhachHangs.FirstOrDefault(x => x.MaKH == makh);
                if (kh.HopDongs.Count > 0)
                {
                    return false;
                }
                else
                {
                    context.KhachHangs.DeleteOnSubmit(kh);
                    context.SubmitChanges();
                    return true;
                }

            }
            catch
            {

            }
            return true;
        }
        #endregion
        #region Tương tác với bảng thiết bị
        //Hiển thị danh sách các thiết bị
        [WebMethod]
        public List<ThietBi> DanhSachThietBi()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<ThietBi> dstb = context.ThietBis.ToList();
            foreach (ThietBi tb in dstb)
            {
                tb.PhongTro = null;
            }
            return dstb;

        }
        //Thêm thiết bị
        [WebMethod]
        public bool ThemThietBi(int maphong, string tenthietbi, int soluong, string tinhtrang)
        {
            try
            {
                ThietBi tb = new ThietBi();
                tb.MaPhong = maphong;
                tb.TenThietBi = tenthietbi;
                tb.SoLuong = soluong;
                tb.TinhTrang = tinhtrang;
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                context.ThietBis.InsertOnSubmit(tb);
                context.SubmitChanges();
                return true;
            }
            catch
            {

            }
            return false;
        }
        [WebMethod]
        public bool SuaThietBi(string tenthietbi, int soluong, string tinhtrang, int mathietbi)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                ThietBi tb = context.ThietBis.FirstOrDefault(x => x.MaTB == mathietbi);
                if (tb != null)
                {

                    tb.TenThietBi = tenthietbi;
                    tb.TinhTrang = tinhtrang;
                    tb.SoLuong = soluong;
                    context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch
            {

            }
            return false;
        }
        //Xóa Thiết bị
        [WebMethod]
        public bool XoaThietBi(int mathietbi)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                ThietBi tb = context.ThietBis.FirstOrDefault(x => x.MaTB == mathietbi);
                if (tb != null)
                {
                    context.ThietBis.DeleteOnSubmit(tb);
                    context.SubmitChanges();
                    return true;
                }
                return false;


            }
            catch
            {

            }
            return false;
        }

        #endregion
        #region Tương tác với bảng chủ nhà trọ
        //Hiển thị danh sách chủ nhà trọ
        [WebMethod]
        public List<ChuNhaTro> DanhSachChuNhaTro()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<ChuNhaTro> dscnt = context.ChuNhaTros.ToList();
            foreach (ChuNhaTro cnt in dscnt)
            {
                cnt.DangNhaps.Clear();
                cnt.HopDongs.Clear();
            }
            return dscnt;
        }
        [WebMethod]
        public bool ThemMoiChuNhaTro(string hoten, string cmnd, string diachi, string sdt, string email, int gioitinh, string matkhau)
        {
            try
            {
                ChuNhaTro cnt = new ChuNhaTro();
                cnt.HoTen = hoten;
                cnt.CMND = cmnd;
                cnt.DiaChi = diachi;
                cnt.SDT = sdt;
                cnt.Email = email;
                cnt.GioiTinh = gioitinh;
                cnt.MatKhau = matkhau;
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                context.ChuNhaTros.InsertOnSubmit(cnt);
                context.SubmitChanges();
                return true;
            }
            catch { }
            return false;
        }

        [WebMethod]
        public bool SuaThongTinChuNhaTro(int machunhatro, string hoten, string cmnd, string sdt, string diachi, string email)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                ChuNhaTro cnt = context.ChuNhaTros.FirstOrDefault(x => x.MaCNT == machunhatro);
                if (cnt != null)
                {
                    cnt.HoTen = hoten;
                    cnt.CMND = cmnd;
                    cnt.SDT = sdt;
                    cnt.DiaChi = diachi;
                    cnt.Email = email;
                    context.SubmitChanges();
                    return true;
                }
                return false;


            }
            catch
            {

            }
            return false;
        }
        #endregion
        #region Tương tác với khu vực phòng trọ
        //Tương tác với bảng khu vực phòng
        //Hiển thị khu vực phòng
        [WebMethod]
        public List<KhuVucPhong> DanhSachKhuVuc()
        {

            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<KhuVucPhong> dskvp = context.KhuVucPhongs.ToList();
            foreach (KhuVucPhong kvp in dskvp)
                kvp.PhongTros.Clear();
            return dskvp;
        }
        //Thêm khu vực phòng
        [WebMethod]
        public bool ThemKhuVucPhong(string tenkv, string tang, string tenday, int soluongphong, string tinhtrang, string diachikhuvuc)
        {
            try
            {
                KhuVucPhong kvp = new KhuVucPhong();
                kvp.TenKV = tenkv;
                kvp.Tang = tang;
                kvp.TenDayPhong = tenday;
                kvp.SoLuongPhong = soluongphong;
                kvp.TinhTrang = tinhtrang;
                kvp.DiaChiKhuVuc = diachikhuvuc;
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                context.KhuVucPhongs.InsertOnSubmit(kvp);
                context.SubmitChanges();
                return true;
            }
            catch
            {

            }
            return false;
        }
        //Sửa Khu Vực Phòng
        [WebMethod]
        public bool SuaKhuVucPhong(int makvp, string tenkv, string tang, string tenday, int soluongphong, string tinhtrang, string diachikhuvuc)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                KhuVucPhong kvp = context.KhuVucPhongs.FirstOrDefault(x => x.MaKV == makvp);
                if (kvp != null)
                {
                    kvp.TenKV = tenkv;
                    kvp.Tang = tang;
                    kvp.TenDayPhong = tenday;
                    kvp.SoLuongPhong = soluongphong;
                    kvp.TinhTrang = tinhtrang;
                    kvp.DiaChiKhuVuc = diachikhuvuc;
                    context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch
            {

            }
            return false;
        }
        //Xóa khu vực phòng
        [WebMethod]
        public bool XoaKhuVucPhong(int makv)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                KhuVucPhong kvp = context.KhuVucPhongs.FirstOrDefault(x => x.MaKV == makv);
                if (kvp.PhongTros.Count > 0)
                {
                    return false;
                }
                else
                {
                    context.KhuVucPhongs.DeleteOnSubmit(kvp);
                    context.SubmitChanges();
                    return true;
                }
            }
            catch
            {

            }
            return true;
        }




        #endregion
        #region Tương tác với phòng trọ
        //Hiển thị danh sách phòng trọ
        [WebMethod]
        public List<PhongTro> DanhSachPhongTro()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<PhongTro> dspt = context.PhongTros.ToList();
            foreach (PhongTro pt in dspt)
            {
                pt.DangKyDichVus.Clear();
                pt.HopDongs.Clear();
                pt.KhuVucPhong = null;
                pt.ThietBis.Clear();
            }
            return dspt;
        }
        //Hiển thị danh sách phòng trọ theo mã khu vực phòng
        [WebMethod]
        public List<PhongTro> DanhSachPhongTheoKhuVuc(int makhuvuc)
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<PhongTro> dspt = context.PhongTros.Where(x => x.MaKV == makhuvuc).ToList();
            foreach (PhongTro pt in dspt)
            {
                pt.DangKyDichVus.Clear();
                pt.HopDongs.Clear();
                pt.KhuVucPhong = null;
                pt.ThietBis.Clear();
            }
            return dspt;
        }
        //Thêm phòng trọ
        [WebMethod]
        public bool ThemPhongTro(int makv, string tenphong, int giaphong, int songuoitoida, string tinhtrang)
        {
            try
            {
                PhongTro pt = new PhongTro();
                pt.MaKV = makv;
                pt.TenPhong = tenphong;
                pt.GiaPhong = giaphong;
                pt.SoNguoiToiDa = songuoitoida;
                pt.TinhTrang = tinhtrang;
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                context.PhongTros.InsertOnSubmit(pt);
                context.SubmitChanges();
                return true;

            }
            catch
            {

            }
            return false;
        }
        //Sửa phòng trọ
        [WebMethod]
        public bool SuaPhongTro(int maphong, string tenphong, int giaphong, int songuoitoida, string tinhtrang)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                PhongTro pt = context.PhongTros.FirstOrDefault(x => x.MaPhong == maphong);
                if (pt != null)
                {
                    pt.TenPhong = tenphong;
                    pt.GiaPhong = giaphong;
                    pt.SoNguoiToiDa = songuoitoida;
                    pt.TinhTrang = tinhtrang;
                    context.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch
            {

            }
            return false;
        }
        //Xóa Phòng Trọ
        [WebMethod]
        public bool XoaPhongtro(int maphongtro)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                PhongTro pt = context.PhongTros.FirstOrDefault(x => x.MaPhong == maphongtro);

                if ((pt.DangKyDichVus.Count > 0) || (pt.HopDongs.Count > 0) || (pt.ThietBis.Count > 0))
                {
                    return false;
                }
                else
                {
                    context.PhongTros.DeleteOnSubmit(pt);
                    context.SubmitChanges();
                    return true;
                }
            }
            catch
            {
            }
            return true;
        }
        #endregion
        #region Tương tác với dịch vụ
        //Hiển thị, thêm, sửa, xóa dịch vụ
        //Hiển thị  dịch vụ
        [WebMethod]
        public List<DichVu> DanhSachDichVu()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<DichVu> dsdv = context.DichVus.ToList();
            foreach (DichVu dv in dsdv)
                dv.DangKyDichVus.Clear();
            return dsdv;
        }
        //Thêm dịch vụ
        [WebMethod]
        public bool ThemDichVu(string tendv, int dongiadv, string ghichu)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                DichVu dv = new DichVu();
                dv.TenDV = tendv;
                dv.DonGiaDV = dongiadv;
                dv.GhiChu = ghichu;
                context.DichVus.InsertOnSubmit(dv);
                context.SubmitChanges();
                return true;

            }
            catch
            {

            }
            return false;
        }
        //Sửa Dịch Vụ
        [WebMethod]
        public bool SuaDichVu(int madv, string tendv, int dongiadv, string ghichu)
        {
            try {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                DichVu dv = context.DichVus.FirstOrDefault(x => x.MaDV == madv);
                if (dv != null)
                {
                    dv.TenDV = tendv;
                    dv.DonGiaDV = dongiadv;
                    dv.GhiChu = ghichu;
                    context.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch
            { }
            return false;
        }
        //Xóa Dịch Vụ
        [WebMethod]
        public bool XoaDichVu(int madv)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                DichVu dv = context.DichVus.FirstOrDefault(x => x.MaDV == madv);
                if (dv.DangKyDichVus.Count > 0)
                {
                    return false;
                }
                else
                {
                    context.DichVus.DeleteOnSubmit(dv);
                    context.SubmitChanges();
                    return true;
                }


            }
            catch
            {

            }
            return true;
        }
        #endregion
        #region Tương tác với đăng ký dịch vụ
        //Hiển thị danh sách đăng ký dịch vụ
        [WebMethod]
        public List<DangKyDichVu> DanhSachDangKy()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<DangKyDichVu> dsdkdv = context.DangKyDichVus.ToList();
            foreach (DangKyDichVu dkdv in dsdkdv)
            {
                dkdv.HoaDonDichVus.Clear();
                dkdv.DichVu = null;
                dkdv.PhongTro = null;
            }
            return dsdkdv;

        }
        //Thêm đăng ký dịch vụ
        [WebMethod]
        public bool ThemDangKy(int maphong, int madv, int soluong, DateTime thoigian)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                DangKyDichVu dkdv = new DangKyDichVu();
                dkdv.MaPhong = maphong;
                dkdv.MaDV = madv;
                dkdv.SoLuong = soluong;
                dkdv.ThoiGian = thoigian;
                context.DangKyDichVus.InsertOnSubmit(dkdv);
                context.SubmitChanges();
                return true;

            }
            catch
            {

            }
            return false;
        }
        //Sửa dịch vụ
        [WebMethod]
        public bool SuaDangKy(int madkdv, int soluong, DateTime thoigian)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                DangKyDichVu dkdv = context.DangKyDichVus.FirstOrDefault(x => x.MaDKDV == madkdv);
                if (dkdv != null)
                {
                    dkdv.SoLuong = soluong;
                    dkdv.ThoiGian = thoigian;
                    context.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch
            { }
            return false;
        }
        //Xóa đăng ký
        [WebMethod]
        public bool XoaDangKy(int madkdv)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                DangKyDichVu dkdv = context.DangKyDichVus.FirstOrDefault(x => x.MaDKDV == madkdv);
                //Con thieu can nghien cuu them
                if (dkdv.HoaDonDichVus.Count > 0)
                {
                    return false;
                }
                else
                {
                    context.DangKyDichVus.DeleteOnSubmit(dkdv);
                    context.SubmitChanges();
                    return true;

                }

            }
            catch
            { }
            return false;

        }
        #endregion
        #region Hóa đơn dịch vụ
        //Hóa đơn dịch vụ chưa thanh toán
        [WebMethod]
        public List<HoaDonDichVu> HoaDonDichVuChuaThanhToan()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<HoaDonDichVu> dshddv = context.HoaDonDichVus.Where(x => x.ThoiGianThanhToan == null).ToList();
            foreach (HoaDonDichVu hddv in dshddv)
            {
                hddv.DangKyDichVu = null;
            }
            return dshddv;
        }
        //Hóa đơn dịch vụ đã thanh toán
        [WebMethod]
        public List<HoaDonDichVu> HoaDonDichVuDaThanhToan()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<HoaDonDichVu> dshddv = context.HoaDonDichVus.Where(x => x.ThoiGianThanhToan != null).ToList();
            foreach (HoaDonDichVu hddv in dshddv)
            {
                hddv.DangKyDichVu = null;
            }
            return dshddv;
        }
        //Toàn bộ hóa đơn dịch vụ
        [WebMethod]
        public List<HoaDonDichVu> DanhSachHoaDonDichVu()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<HoaDonDichVu> dshddv = context.HoaDonDichVus.ToList();
            foreach (HoaDonDichVu hddv in dshddv)
            {
                hddv.DangKyDichVu = null;
            }
            return dshddv;
        }
        //Thêm hóa đơn dịch vụ
        [WebMethod]
        public bool ThemHoaDonDichVu(int madangkydv, int tongtien, DateTime thoigianlap, DateTime thoigianthanhtoan)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                HoaDonDichVu hddv = new HoaDonDichVu();
                hddv.MaDKDV = madangkydv;
                hddv.TongTien = tongtien;
                hddv.ThoiGianLap = thoigianlap;
                hddv.ThoiGianThanhToan = thoigianthanhtoan;
                context.HoaDonDichVus.InsertOnSubmit(hddv);
                context.SubmitChanges();
                return true;

            }
            catch
            {

            }
            return false;
        }
        //Sửa hóa đơn dịch vụ
        [WebMethod]
        public bool SuaHoaDonDichVu(int mahddv, int tongtien, DateTime thoigianlap, DateTime thoigianthanhtoan)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                HoaDonDichVu hddv = context.HoaDonDichVus.FirstOrDefault(x => x.MaHoaDonDV == mahddv);
                if (hddv != null)
                {
                    hddv.TongTien = tongtien;
                    hddv.ThoiGianLap = thoigianlap;
                    hddv.ThoiGianThanhToan = thoigianthanhtoan;
                    context.SubmitChanges();
                    return true;

                }
                return false;
            }
            catch
            { }
            return false;
        }
        //Xóa hóa đơn dịch vụ
        [WebMethod]
        public bool XoaHoaDonDichVu(int mahddv)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                HoaDonDichVu hddv = context.HoaDonDichVus.FirstOrDefault(x => x.MaHoaDonDV == mahddv);
                if (hddv != null)
                {
                    context.HoaDonDichVus.DeleteOnSubmit(hddv);
                    context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch
            {

            }
            return false;
        }
        #endregion
        #region Tương tác với hóa đơn phòng trọ
        //Hiển thị toàn bộ hóa đơn phòng trọ
        [WebMethod]
        public List<HoaDonPhongTro> ToanBoHoaDonPhongTro()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<HoaDonPhongTro> dshdpt = context.HoaDonPhongTros.ToList();
            foreach (HoaDonPhongTro hdpt in dshdpt)
            {
                hdpt.HopDong = null;

            }
            return dshdpt;
        }
        //Hiển thị hóa đơn phong tro đã thanh toán
        //[WebMethod]
        //public List<HoaDonPhongTro> HoaDonPhongTroDaThanhToan()
        //{
        //    QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
        //    List<HoaDonPhongTro> dshdpt = context.HoaDonPhongTros.Where(x => x.ThoiGianThanhToan != null).ToList();
        //    foreach (HoaDonPhongTro hdpt in dshdpt)
        //    {
        //        hdpt.HopDong = null;
        //    }
        //    return dshdpt;
        //}
        ////Hiển thị hóa đơn phòng trọ chưa thanh toán
        //[WebMethod]
        //public List<HoaDonPhongTro> HoaDonPhongTroChuaThanhToan()
        //{
        //    QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
        //    List<HoaDonPhongTro> dshdpt = context.HoaDonPhongTros.Where(x => x.ThoiGianThanhToan == null).ToList();
        //    foreach (HoaDonPhongTro hdpt in dshdpt)
        //    {
        //        hdpt.HopDong = null;
        //    }
        //    return dshdpt;
        //}
        //Thêm hóa đơn phòng trọ
        [WebMethod]
        public bool ThemHoaDonPhongTro(int mahd, int sotien, DateTime thoigianlap, DateTime thoigianthanhtoan)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                HoaDonPhongTro hdpt = new HoaDonPhongTro();
                hdpt.MaHD = mahd;
                hdpt.SoTien = sotien;
                hdpt.ThoiGianLap = thoigianlap;
                hdpt.ThoiGianThanhToan = thoigianthanhtoan;
                context.HoaDonPhongTros.InsertOnSubmit(hdpt);
                context.SubmitChanges();
                return true;
            }
            catch
            {

            }
            return false;
        }
        //Thêm hóa đơn phòng trọ chưa thanh toán
        [WebMethod]
        public bool ThemHoaDonPhongTroChuaThanhToan(int mahd, int sotien, DateTime thoigianlap)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                HoaDonPhongTro hdpt = new HoaDonPhongTro();
                hdpt.MaHD = mahd;
                hdpt.SoTien = sotien;
                hdpt.ThoiGianLap = thoigianlap;
                context.HoaDonPhongTros.InsertOnSubmit(hdpt);
                context.SubmitChanges();
                return true;

            }
            catch { }
            return false;
        }
        //Sửa hóa đơn phòng trọ
        [WebMethod]
        public bool SuaHoaDonPhongTro(int mahdpt, int sotien, DateTime thoigianlap, DateTime thoigianthanhtoan)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                HoaDonPhongTro hdpt = context.HoaDonPhongTros.FirstOrDefault(x => x.MaHoaDon == mahdpt);
                if (hdpt != null)
                {
                    hdpt.SoTien = sotien;
                    hdpt.ThoiGianLap = thoigianlap;
                    hdpt.ThoiGianThanhToan = thoigianthanhtoan;
                    context.SubmitChanges();
                    return true;

                }
                return false;
            }
            catch
            {

            }
            return false;
        }
        //Xóa hóa đơn phòng trọ
        [WebMethod]
        public bool XoaHoaDonPhongTro(int mahoadonpt)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                HoaDonPhongTro hdpt = context.HoaDonPhongTros.FirstOrDefault(x => x.MaHoaDon == mahoadonpt);
                if (hdpt != null)
                {
                    context.HoaDonPhongTros.DeleteOnSubmit(hdpt);
                    context.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch
            {

            }
            return false;

        }
        #endregion
        #region Tương tác với bảng hợp đồng
        //Hiển thị danh sách hợp đồng
        [WebMethod]
        public List<HopDong> DanhSachHopDong()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<HopDong> dshd = context.HopDongs.ToList();
            foreach (HopDong hd in dshd)
            {
                hd.ChuNhaTro = null;
                hd.KhachHang = null;
                hd.PhongTro = null;
                hd.HoaDonPhongTros.Clear();
            }
            return dshd;
        }
        //Thêm hợp đồng
        [WebMethod]
        public bool ThemHopDong(int macnt, int makh, int maphong, int tiendatcoc, DateTime ngaylap, DateTime ngaythue, DateTime ngayketthuc)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                HopDong hd = new HopDong();
                hd.MaCNT = macnt;
                hd.MaKH = makh;
                hd.TienDatCoc = tiendatcoc;
                hd.NgayLap = ngaylap;
                hd.NgayThue = ngaythue;
                hd.NgayKetThuc = ngayketthuc;
                hd.MaPhong = maphong;
                context.HopDongs.InsertOnSubmit(hd);
                context.SubmitChanges();
                return true;

            }
            catch
            {

            }
            return false;
        }
        //Sửa hợp đồng
        [WebMethod]
        public bool SuaHopDong(int mahd, int tiendatcoc, DateTime ngaylap, DateTime ngaythue, DateTime ngayketthuc)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                HopDong hd = context.HopDongs.FirstOrDefault(x => x.MaHD == mahd);

                if (hd != null)
                {
                    hd.TienDatCoc = tiendatcoc;
                    hd.NgayLap = ngaylap;
                    hd.NgayKetThuc = ngayketthuc;
                    hd.NgayThue = ngaythue;
                    context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch
            {

            }
            return false;
        }
        //Xóa hợp đồng
        [WebMethod]
        public bool XoaHopDong(int mahd)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                HopDong hd = context.HopDongs.FirstOrDefault(x => x.MaHD == mahd);
                if (hd.HoaDonPhongTros.Count > 0)
                {
                    return false;
                }
                else
                {
                    context.HopDongs.DeleteOnSubmit(hd);
                    context.SubmitChanges();
                    return true;
                }
            }
            catch
            {

            }
            return true;
        }
        #endregion
        #region Tương tác join
        //Join bảng phòng trọ và khu vực phòng
        //Liệt kê các phòng trọ thuộc khu vực phòng nào
        // QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
        //public string KVP()
        //{
        //    QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
        //    var query = context.PhongTros.Join(context.KhuVucPhongs,
        //        x => x.MaKV,
        //        y => y.MaKV,
        //        (x, y) => new { KhuVucPhong = x, PhongTro = y });
        //    return View(query.ToList());
        //}
        [WebMethod]
        public List<uspPhongTroListResult> KVP()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<uspPhongTroListResult> dsphongtro = context.uspPhongTroList().ToList();
            return dsphongtro;
        }
        [WebMethod]
        public List<usp_KH_HD_PResult> KHP(String tenKH)
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<usp_KH_HD_PResult> dsKHP = context.usp_KH_HD_P(tenKH).ToList();
            return dsKHP;
        }
        //Tìm kiếm khách hàng
        [WebMethod]
        public List<usp_Search_KHResult> SearchKH(String tenKH)
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<usp_Search_KHResult> dsKH = context.usp_Search_KH(tenKH).ToList();
            return dsKH;
        }
        //Hiển thị danh sách phòng trọ và khu vực hiện có:
        [WebMethod]
        public List<uspPTKVPResult> PTKVP()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<uspPTKVPResult> dsPTKVP = context.uspPTKVP().ToList();
            return dsPTKVP;
        }
        //Sửa thiết bị
        [WebMethod]
        public bool SuaDanhSachThietBi(int maphong, string tenthietbi, int soluong, string tinhtrang, int mathietbi)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                ThietBi tb = context.ThietBis.FirstOrDefault(x => x.MaTB == mathietbi);
                if (tb != null)
                {
                    tb.MaPhong = maphong;
                    tb.TenThietBi = tenthietbi;
                    tb.TinhTrang = tinhtrang;
                    tb.SoLuong = soluong;
                    context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch
            {

            }
            return false;
        }
        //Hhien thi danh sach phong tro va so nguoi hien co
        [WebMethod]
        public List<uspChiTietPhongTroResult> DanhSachPhongTroVaSoNguoiHienCo()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<uspChiTietPhongTroResult> dsctpt = context.uspChiTietPhongTro().ToList();
            return dsctpt;
        }
        //Hien thi danh sach phong tro
        [WebMethod]
        public List<uspDanhSachPhongTroResult> DSPT()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<uspDanhSachPhongTroResult> dspt = context.uspDanhSachPhongTro().ToList();
            return dspt;
        }
        //Danh sách đăng ký dịch vụ 1
        [WebMethod]
        public List<uspDangKyDichVuResult> DSDKDV()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<uspDangKyDichVuResult> dsdkdv = context.uspDangKyDichVu().ToList();
            return dsdkdv;
        }
        //Danh sách đăng ký dịch vụ 2
        [WebMethod]
        public List<uspDSDKDVResult> DSDKDVR()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<uspDSDKDVResult> dsdkdv = context.uspDSDKDV().ToList();
            return dsdkdv;
        }
        // Danh sách hóa đơn dịch vụ
        [WebMethod]
        public List<uspHoaDonDichVuResult> DSHDDV()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<uspHoaDonDichVuResult> dshddv = context.uspHoaDonDichVu().ToList();
            return dshddv;

        }
        //Danh sách hóa đơn dịch vụ đã thanh toán
        [WebMethod]
        public List<uspHoaDonDichVuDaThanhToanResult> DanhSachHoaDonDichVuDaThanhToan()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<uspHoaDonDichVuDaThanhToanResult> dshddvdtt = context.uspHoaDonDichVuDaThanhToan().ToList();
            return dshddvdtt;
        }
        //Danh sách hóa đơn dịch vụ chưa thanh toán
        [WebMethod]
        public List<uspHoaDonDichVuChuaThanhToanResult> DanhSachHoaDonDichVuChuaThanhToan()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<uspHoaDonDichVuChuaThanhToanResult> dshddvctt = context.uspHoaDonDichVuChuaThanhToan().ToList();
            return dshddvctt;
        }
        //Tạo load dữ liệu lên spinner đăng ký dịch vụ để lập hóa đơn
        [WebMethod]
        public List<uspSpDangKyDichVuResult> SpinnerDangKyDichVu()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<uspSpDangKyDichVuResult> spdkdv = context.uspSpDangKyDichVu().ToList();
            return spdkdv;
        }
        //Thêm hóa đơn nếu chưa được thanh toán
        [WebMethod]
        public bool ThemHoaDonDichVuChuaThanhToan(int madangkydichvu, int tongtien, DateTime thoigianlap)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                HoaDonDichVu hddv = new HoaDonDichVu();
                hddv.MaDKDV = madangkydichvu;
                hddv.TongTien = tongtien;
                hddv.ThoiGianLap = thoigianlap;
                context.HoaDonDichVus.InsertOnSubmit(hddv);
                context.SubmitChanges();
                return true;
            }
            catch { }
            return false;
        }
        //Sửa hóa đơn dịch vụ chưa thanh toán
        [WebMethod]
        public bool SuaHoaDonDichVuChuaThanhToan(int mahddv, DateTime thoigianthanhtoan)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                HoaDonDichVu hddv = context.HoaDonDichVus.FirstOrDefault(x => x.MaHoaDonDV == mahddv);
                if (hddv != null)
                {
                    hddv.ThoiGianThanhToan = thoigianthanhtoan;
                    context.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch
            { }
            return false;
        }
        [WebMethod]
        public List<uspThietBiPhongKhuVucResult> DanhSachThietBiPhong()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<uspThietBiPhongKhuVucResult> dstbp = context.uspThietBiPhongKhuVuc().ToList();
            return dstbp;
        }
        //Hiển thị danh sach hop dong
        [WebMethod]
        public List<uspHopDongResult> ChiTietDanhSachHopDong()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<uspHopDongResult> dshd = context.uspHopDong().ToList();
            return dshd;
        }

        //Dang nhap voi user
        [WebMethod]
        public List<uspDangNhapUserResult> DangNhapUser(String email, String pws)
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<uspDangNhapUserResult> dn = context.uspDangNhapUser(email, pws).ToList();
            return dn;
        }
        //Danh sách hóa đơn phòng trọ
        [WebMethod]
        public List<uspHoaDonPhongTroResult> HoaDonPhongTro()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<uspHoaDonPhongTroResult> hdpt = context.uspHoaDonPhongTro().ToList();
            return hdpt;
        }
        //Them Hoa Don Phong Tro Chua Thanh Toan
        public bool ThemHoaDonPhongTroChuaTT(int mahd, int sotien, DateTime thoigianlap)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                HoaDonPhongTro hdpt = new HoaDonPhongTro();
                hdpt.MaHD = mahd;
                hdpt.SoTien = sotien;
                hdpt.ThoiGianLap = thoigianlap;
                context.HoaDonPhongTros.InsertOnSubmit(hdpt);
                context.SubmitChanges();
                return true;

            }
            catch { }
            return false;
        }
        //Hóa đơn phòng trọ đã thanh toán
        [WebMethod]
        public List<uspHoaDonPhongTroThanhToanResult> HoaDonPhongTroDaThanhToan()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<uspHoaDonPhongTroThanhToanResult> hdpttt = context.uspHoaDonPhongTroThanhToan().ToList();
            return hdpttt;
        }
        //Hóa đơn phòng trọ chưa thanh toán
        [WebMethod]
        public List<uspHoaDonPhongTroChuaThanhToanResult> HoaDonPhongTroChuaThanhToan()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<uspHoaDonPhongTroChuaThanhToanResult> hdptctt = context.uspHoaDonPhongTroChuaThanhToan().ToList();
            return hdptctt;
        }
        //Sửa hóa đơn phòng trọ chưa thanh toán
        [WebMethod]
        public bool SuaHoaDonPhongTroChuaThanhToan(int mahoadon, DateTime thoigianthanhtoan)
        {
            try
            {
                QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
                HoaDonPhongTro hdpt = context.HoaDonPhongTros.FirstOrDefault(x => x.MaHoaDon == mahoadon);
                if (hdpt != null)
                {
                    hdpt.ThoiGianThanhToan = thoigianthanhtoan;
                    context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch { }
            return false;
        }

        //Tổng tiền hóa đơn dịch vụ
        [WebMethod]
        public double tongtienhoadondichvu(DateTime thoigian1, DateTime thoigian2)
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            return context.HoaDonDichVus.Where(y=>y.ThoiGianThanhToan>=thoigian1&&y.ThoiGianThanhToan<=thoigian2).Sum(x => x.TongTien).Value;
        }
        //Tổng tiền hóa đơn phòng trọ
        [WebMethod]
        public double tongtienhoadonphongtro(DateTime thoigian1,DateTime thoigian2)
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            return context.HoaDonPhongTros.Where(y => y.ThoiGianThanhToan >= thoigian1 && y.ThoiGianThanhToan <= thoigian2).Sum(x => x.SoTien).Value;
        }
        //Hiển thị danh sách số người hiện có
        [WebMethod]
        public List<uspSoNguoiHienCoResult> SoNguoiHienCo()
        {
            QUANLYNHATRO1DataContext context = new QUANLYNHATRO1DataContext();
            List<uspSoNguoiHienCoResult> snhc = context.uspSoNguoiHienCo().ToList();
            return snhc;
        }

        #endregion


    }
}

