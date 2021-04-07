using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SGBD_Lab1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            SqlDataAdapter adapter;
            DataSet dset;
            string connstring = "Data Source=DESKTOP-TKFHRGS\\SQLEXPRESS;Initial Catalog=Airport_Management;"
            + "Integrated Security=true";
            SqlConnection connection = new SqlConnection(connstring);
            try
            {
                connection.Open();
                dset = new System.Data.DataSet();
                adapter = new SqlDataAdapter("select * from Companies",
                connstring);
                adapter.Fill(dset, "Companies");
                adapter = new SqlDataAdapter("select * from FlightStaff",
                connstring);
                adapter.Fill(dset, "FlightStaff");
                dset.Relations.Add("Company_FlightStaff", dset.Tables["Companies"].Columns["CompanyID"],
                dset.Tables["FlightStaff"].Columns["HiringCompany"]);
                connection.Close();
                dataGridView1.DataSource = dset.Tables["Companies"].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                connection.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = this.dataGridView1.CurrentCell.RowIndex;
            object cell_value = this.dataGridView1[0, idx].Value;
            string idString = cell_value.ToString();
            int companyID = Int32.Parse(idString);
            string connstring = "Data Source=DESKTOP-TKFHRGS\\SQLEXPRESS;Initial Catalog=Airport_Management;"
            + "Integrated Security=true";
            SqlConnection connection = new SqlConnection(connstring);
            string childQuery = "select * from FlightStaff where HiringCompany = @companyID";
            SqlCommand command = new SqlCommand(childQuery, connection);
            command.Parameters.Add("@companyId", SqlDbType.Int);
            command.Parameters["@companyId"].Value = companyID;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                dataGridView2.DataSource = dataTable;
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                connection.Close();
            }
        }

        private void button1_MouseCaptureChanged(object sender, EventArgs e)
        {
            //delete button

            int idxRow1 = this.dataGridView1.CurrentCell.RowIndex;
            object cell_value = this.dataGridView1[0, idxRow1].Value;
            string idString = cell_value.ToString();
            int companyID = Int32.Parse(idString);

            int idxRow2 = this.dataGridView2.CurrentCell.RowIndex;
            string staffID = this.dataGridView2[0, idxRow2].Value.ToString();

            string connstring = "Data Source=DESKTOP-TKFHRGS\\SQLEXPRESS;Initial Catalog=Airport_Management;"
            + "Integrated Security=true";
            SqlConnection connection = new SqlConnection(connstring);
            string childQuery = "delete from FlightStaff where FlightStaffID = @staffID " +
                "select * from FlightStaff where HiringCompany = @companyID";
            SqlCommand command = new SqlCommand(childQuery, connection);
            command.Parameters.Add("@staffID", SqlDbType.VarChar);
            command.Parameters["@staffID"].Value = staffID;
            command.Parameters.Add("@companyId", SqlDbType.Int);
            command.Parameters["@companyId"].Value = companyID;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                dataGridView2.DataSource = dataTable;
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                connection.Close();
            }
        }

        private void button2_MouseCaptureChanged(object sender, EventArgs e)
        {
            //update button

            int idxRow1 = this.dataGridView1.CurrentCell.RowIndex;
            object cell_value = this.dataGridView1[0, idxRow1].Value;
            string idString = cell_value.ToString();
            int companyID = Int32.Parse(idString);

            int idxCol = this.dataGridView2.CurrentCell.ColumnIndex;
            string column_name = this.dataGridView2.Columns[idxCol].Name;

            int idxRow2 = this.dataGridView2.CurrentCell.RowIndex;
            string staffID = this.dataGridView2[0, idxRow2].Value.ToString();
            string newValue = this.textBox1.Text;
            string connstring = "Data Source=DESKTOP-TKFHRGS\\SQLEXPRESS;Initial Catalog=Airport_Management;"
            + "Integrated Security=true";
            SqlConnection connection = new SqlConnection(connstring);
            string childQuery = "update FlightStaff set " + column_name + " = @val where FlightStaffID = @staffID " +
                "select * from FlightStaff where HiringCompany = @companyID";
            SqlCommand command = new SqlCommand(childQuery, connection);
            command.Parameters.Add("@staffID", SqlDbType.VarChar);
            command.Parameters["@staffID"].Value = staffID;
            command.Parameters.Add("@val", SqlDbType.VarChar);
            command.Parameters["@val"].Value = newValue;
            command.Parameters.Add("@companyId", SqlDbType.Int);
            command.Parameters["@companyId"].Value = companyID;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                dataGridView2.DataSource = dataTable;
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hey King! I don't know if you got the message, but that's...the wrong value ^-^");
                connection.Close();
            }
        }

        private void button4_MouseCaptureChanged(object sender, EventArgs e)
        {
            //display button

            string connstring = "Data Source=DESKTOP-TKFHRGS\\SQLEXPRESS;Initial Catalog=Airport_Management;"
            + "Integrated Security=true";
            SqlConnection connection = new SqlConnection(connstring);
            string childQuery = "select * from FlightStaff";
            SqlCommand command = new SqlCommand(childQuery, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(); //face select-ul
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                dataGridView2.DataSource = dataTable;
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                connection.Close();
            }
        }

        private void button3_MouseCaptureChanged(object sender, EventArgs e)
        {
            //insert button

            int idxRow1 = this.dataGridView1.CurrentCell.RowIndex;
            object cell_value = this.dataGridView1[0, idxRow1].Value;
            string idString = cell_value.ToString();
            int companyID = Int32.Parse(idString);

            string connstring = "Data Source=DESKTOP-TKFHRGS\\SQLEXPRESS;Initial Catalog=Airport_Management;"
            + "Integrated Security=true";
            SqlConnection connection = new SqlConnection(connstring);
            string childQuery = "insert into FlightStaff values (@staffID, @firstName, @lastName, @birthDate, @occupation,"  +
                "@companyID, @hiringDate, @salary, @bonus, @rating) " +
                "select * from FlightStaff where HiringCompany = @companyID";
            SqlCommand command = new SqlCommand(childQuery, connection);

            command.Parameters.Add("@staffID", SqlDbType.VarChar);
            command.Parameters["@staffID"].Value = this.textBox2.Text;

            command.Parameters.Add("@firstName", SqlDbType.VarChar);
            command.Parameters["@firstName"].Value = this.textBox3.Text;

            command.Parameters.Add("@lastName", SqlDbType.VarChar);
            command.Parameters["@lastName"].Value = this.textBox4.Text;

            command.Parameters.Add("@birthDate", SqlDbType.VarChar);
            command.Parameters["@birthDate"].Value = this.textBox5.Text;

            command.Parameters.Add("@occupation", SqlDbType.VarChar);
            command.Parameters["@occupation"].Value = this.textBox6.Text;

            command.Parameters.Add("@companyId", SqlDbType.Int);
            command.Parameters["@companyId"].Value = companyID;

            command.Parameters.Add("@hiringDate", SqlDbType.VarChar);
            command.Parameters["@hiringDate"].Value = this.textBox7.Text;

            command.Parameters.Add("@salary", SqlDbType.VarChar);
            command.Parameters["@salary"].Value = this.textBox8.Text;

            command.Parameters.Add("@bonus", SqlDbType.VarChar);
            command.Parameters["@bonus"].Value = this.textBox9.Text;

            command.Parameters.Add("@rating", SqlDbType.VarChar);
            command.Parameters["@rating"].Value = this.textBox10.Text;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                dataGridView2.DataSource = dataTable;
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                connection.Close();
            }
        }
    }
}