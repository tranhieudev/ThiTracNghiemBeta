﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThiTracNghiemBetta.form.student
{
    public partial class frmInputStudent : Form
    {
        private string message = "";
        public frmInputStudent()
        {
            InitializeComponent();
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void kHOABindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.tableAdapterManager.UpdateAll(this.ds);

        }

        private void frmInputStudent_Load(object sender, EventArgs e)
        {
            ds.EnforceConstraints = false;
            // TODO: This line of code loads data into the 'ds.SINHVIEN' table. You can move, or remove it, as needed.
            this.sINHVIENTableAdapter.Connection.ConnectionString = Program.connstr;
            this.sINHVIENTableAdapter.Fill(this.ds.SINHVIEN);
            // TODO: This line of code loads data into the 'tN_CSDLPTDataSet.LOP' table. You can move, or remove it, as needed.
            this.adapterLop.Connection.ConnectionString = Program.connstr;
            this.adapterLop.Fill(this.ds.LOP);
            // TODO: This line of code loads data into the 'tN_CSDLPTDataSet.KHOA' table. You can move, or remove it, as needed.
            ds.EnforceConstraints = true;

        }

        private void bar_btn_deleteLop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
         
        }
        private bool checkValidateLop()
        {
            if (bds_sv.Count > 0)
            {
                MessageBox.Show("Lớp đã tồn tại sinh viên. Không thể xóa lớp này", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void gv_Lop_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            };
            var rowH = gv_Lop.FocusedRowHandle;
            var focusRowView = (DataRowView)gv_Lop.GetFocusedRow();
            if (focusRowView == null || focusRowView.IsNew) return;

            if (rowH >= 0)
            {
                popupMenuLop.ShowPopup(barManager1, new Point(MousePosition.X, MousePosition.Y));
               
            }
            else popupMenuLop.HidePopup();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_addClass_Click(object sender, EventArgs e)
        {
            /*
             * show diaglog
             */
            addClass f = new addClass(this);
            f.ShowDialog();
           
        }


      
        private void sINHVIENDataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
              
            }
    }

        private void btn_edit_lop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmEditClass f = new frmEditClass();
            f.txtMaLop.Enabled = false;
            f.ShowDialog();
        }

        private void btn_delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (checkValidateLop() == true)
            {
                var row = gv_Lop.FocusedRowHandle;
                gv_Lop.DeleteRow(row);
                bds_lop.EndEdit();
                this.adapterLop.Update(this.ds.LOP);
            }
        }

        private void btn_add_sv_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*TO DO
             *  show form để nhận data 
             *  get data từ form;
             *  add data vao gridview
             */

           /* addSV f = new addSV();
            f.txt_malop.Text= gv_Lop.GetFocusedRowCellValue("MALOP").ToString();
            f.ShowDialog();

            //get data add data
            if (f.state == true)
            {     
                DataRow row = this.ds.SINHVIEN.NewRow();
                row[0] = f.txt_ma_sv.Text.Trim().ToUpper();
                row[1] = utils.utils.chuanHoaTen(f.txt_ho_sv.Text.Trim());
                row[2] = utils.utils.chuanHoaTen(f.txt_ten_sv.Text.Trim());
                row[3] = new DateTime(f.dt_picker.Value.Year, f.dt_picker.Value.Month, f.dt_picker.Value.Day) ;
                row[4] = utils.utils.chuanHoaTen(f.txt_dia_chi.Text.Trim());
                row[5] = f.txt_malop.Text;
                this.ds.SINHVIEN.Rows.Add(row);
                bds_sv.EndEdit();
                sINHVIENTableAdapter.Update(this.ds.SINHVIEN);
                // tao tai khoan 
                if (f.txt_ma_sv.Enabled == true) // chuc nang add thi moi tao
                {
                    if (taoTaiKhoan(f.txt_ma_sv.Text) == false)
                    {
                        MessageBox.Show("Lỗi không thể tạo tài khoản!", "Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
            }*/
           
        }

        private void GV_SV_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
               
                return;
            }
            var rowH = gv_Lop.FocusedRowHandle;
            var focusRowView = (DataRowView)gv_Lop.GetFocusedRow();
            if (focusRowView == null || focusRowView.IsNew)
            {
                return;
            }

            if (rowH >= 0)
            {
                popupSinhVien.ShowPopup(barManager1, new Point(MousePosition.X, MousePosition.Y));
                txtTenLop.Text = gv_Lop.GetFocusedRowCellValue("MALOP").ToString();
            }
            else popupSinhVien.HidePopup();
        }

        private void GV_SV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_edit_sv_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*TO DO
             * tao form
             * gui thong tin sang form
             * thuc hien chinh sua
             * Update du lieu;
             */
            addSV f = new addSV();
            //gui du lieu
            f.txt_malop.Text = gv_Lop.GetFocusedRowCellValue("MALOP").ToString();
            f.txt_ma_sv.Text = GV_SV.CurrentRow.Cells[0].Value.ToString();
            f.txt_ho_sv.Text = GV_SV.CurrentRow.Cells[1].Value.ToString();
            f.txt_ten_sv.Text = GV_SV.CurrentRow.Cells[2].Value.ToString();
            f.dt_picker.Text = GV_SV.CurrentRow.Cells[3].Value.ToString();
            f.txt_dia_chi.Text = GV_SV.CurrentRow.Cells[4].Value.ToString();
            f.txt_ma_sv.Enabled = false;
           
            f.ShowDialog();

            //get data
            if (f.state == true)
            {
                GV_SV.CurrentRow.Cells[1].Value=f.txt_ho_sv.Text.Trim();
                GV_SV.CurrentRow.Cells[2].Value=f.txt_ten_sv.Text.Trim();
                GV_SV.CurrentRow.Cells[3].Value = new DateTime(f.dt_picker.Value.Year, f.dt_picker.Value.Month, f.dt_picker.Value.Day);
                GV_SV.CurrentRow.Cells[4].Value= f.txt_dia_chi.Text.Trim();
                bds_sv.EndEdit();
                sINHVIENTableAdapter.Update(this.ds.SINHVIEN);

               
            }

        }
        private bool taoTaiKhoan(string masv) // tra ve true neu ton tai - false neu khong ton tai
        {
            int kn = Program.KetNoi();
            if (kn == 0)
            {
                MessageBox.Show("Lỗi kết nối! ");
                return true;
            }
            SqlCommand sqlCommand = new SqlCommand("SP_TAOLOGIN", Program.conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@LGNAME", masv);
            sqlCommand.Parameters.AddWithValue("@PASS", "123");
            sqlCommand.Parameters.AddWithValue("@USERNAME", masv);
            sqlCommand.Parameters.AddWithValue("@ROLE", "SINHVIEN");
            int kq = Program.execStoreProcedureWithReturnValue(sqlCommand);
            if (kq == 0)
            {
                Program.conn.Close();
                return true;
            }
            Program.conn.Close();
            return false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            
        }

        private void btn_delete_sv_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GV_SV.Rows.Remove(GV_SV.CurrentRow);
            bds_sv.EndEdit();
            sINHVIENTableAdapter.Update(this.ds.SINHVIEN);
        }

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát không?", "Exit", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (Program.mNhom == "GIANGVIEN") Program.frmMain.btKHOA.Enabled = Program.frmMain.btGiaoVien.Enabled = Program.frmMain.btLOP.Enabled = Program.frmMain.btMonHoc.Enabled = Program.frmMain.btlogin.Enabled = false;
                else
                {
                    Program.frmMain.btBD.Enabled = Program.frmMain.btCancel.Enabled = Program.frmMain.btDSDK.Enabled = Program.frmMain.btKHOA.Enabled = Program.frmMain.btlogin.Enabled = Program.frmMain.btLOP.Enabled = Program.frmMain.btMonHoc.Enabled = true;
                    Program.frmMain.btnXEMBAITHI.Enabled = Program.frmMain.btnXEMBANGDIEM.Enabled = Program.frmMain.btnXEMDSDANGKY.Enabled = true;
                }
                this.Close();
            }
        }

        private void barbtnaddClass_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            txtTenLop.Text = gv_Lop.GetFocusedRowCellValue("MALOP").ToString();
            //bds_lop.AddNew();
            txtMaLop.Focus();
            gb_lop.Enabled = true;
            
           
        }

        private void tENLOPLabel_Click(object sender, EventArgs e)
        {

        }
        private bool checkValidateAddClass()
        {
            /*
             * Kiểm tra mã lớp rỗng ko?
             * Kiểm tra các ký tự đặc biệt của mã lớp
             * kiểm tra mã lớp có tồn tại chưa
             * Kiểm tra lên lớp có rỗng không
             * kiểm tên lớp có tồn tại chưa (tên lớp khóa unique)
             */
            string malop = txtMaLop.Text;
            string tenLop = txtTenLop.Text;

            if (malop.Length == 0)
            {
                message = "Mã lớp không được để trống!";
                return false;
            }
            Regex regex = new Regex("^[a-zA-Z0-9]*$");

            if (regex.IsMatch(malop) == false)
            {
                message = "Tên lớp không được chứa khoảng trắng hoặc ký tự đặc biệt";
                return false;
            }

            if (tenLop.Length == 0)
            {
                message = "Tên lớp không được để trống!";
                return false;
            }
            if (checkExistMaLop(txtMaLop.Text.Trim()))
            {
                message = "Mã lớp đã tồn tại!!";
                return false;
            }
            if (checkExistTenLop(txtTenLop.Text.Trim()))
            {
                message = "Tên lớp đã tồn tại !";
                return false;
            }
            return true;
        }
        private bool checkExistMaLop(string strLenh)
        {
            try
            {
                int kn = Program.KetNoi();
                if (kn == 0)
                {
                    MessageBox.Show("Sự cố kết nối!", "", MessageBoxButtons.OK);
                    return true;
                }

                String strlenh = "EXEC dbo.SP_TIMKIEM_LOP '" + strLenh + "'";
                Program.myReader = Program.ExecSqlDataReader(strlenh);
                if (Program.myReader.Read() != false)
                {
                    //MessageBox.Show("Mã lớp đã tồn tại", "", MessageBoxButtons.OK);
                    message = "Mã lớp đã tồn tại";
                    Program.conn.Close();
                    return true;
                }

                else
                {
                    Program.conn.Close();
                    return false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return true;
        }
        private bool checkExistTenLop(string tenlop)
        {
            int kn = Program.KetNoi();
            SqlCommand sqlCommand = new SqlCommand("SP_KIEM_TRA_TON_TAI_TEN_LOP", Program.conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@TENLOP", txtTenLop.Text);
            int kq = Program.execStoreProcedureWithReturnValue(sqlCommand);
            if (kq != 0) // ton tai
            {
                return true;
            }
            return false;
        }

        private void btnLuuLop_Click(object sender, EventArgs e)
        {
            bool check=checkValidateAddClass();
            if (check == false)
            {
                MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {

            }
        }

        private void gc_lop_Click(object sender, EventArgs e)
        {
          
        }

        private void gc_lop_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void gv_Lop_Click(object sender, EventArgs e)
        {
            txtTenLop.Text = "afsdfa";
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
    
}
