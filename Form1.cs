using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms.Design;
using TextBox = System.Windows.Forms.TextBox;

namespace CrudOperations
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbOgrenciDataSet.Student' table. You can move, or remove it, as needed.
            this.studentTableAdapter.Fill(this.dbOgrenciDataSet.Student);
            BindData();



        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
       
          SqlConnection sqlConnection = new SqlConnection("Data Source=DESKTOP-ARML8T9\\SQLEXPRESS;Initial Catalog=DbOgrenci;Integrated Security=True");
            sqlConnection.Open();   
            //SQL Server veritabanına bağlantıyı temsil eder. Bu sınıf devralınamaz.
          SqlCommand cmd = new SqlCommand("Insert Into Student values(@StudentName,@StudentAdress,@City,@Number)",sqlConnection);
            //SqlCommand bizim veritabanı üzerinde yapmak istediğimiz işlemlerin ADO tarafında belirtilmesini sağlayan sınıftır.
            cmd.Parameters.AddWithValue("@StudentName", txtAd.Text);
            cmd.Parameters.AddWithValue("@StudentAdress",txtAdress.Text);
            cmd.Parameters.AddWithValue("@City",txtCity.Text);
            cmd.Parameters.AddWithValue("@Number",txtNumber.Text);
            cmd.ExecuteNonQuery();
            //etkilenen satır sayısını gosterir

            DataTable dataTable = new DataTable();   
            
            sqlConnection.Close();
            MessageBox.Show("Successful");
            BindData();
            foreach (var item in Controls)
            {
                var textboxes = item as TextBox;
                if (textboxes != null)
                {
                    textboxes.Clear();
                }
            }
            // AddWithValue Kullandığınızda parametreye atadığınız değer SQL Server tarafından uygun tipe dönüştürülür.
            // hiçbir satır döndürmese ExecuteNonQuery de, parametrelere eşlenen tüm çıkış parametreleri veya dönüş değerleri verilerle doldurulur.




        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        void BindData()
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-ARML8T9\\SQLEXPRESS;Initial Catalog=DbOgrenci;Integrated Security=True");
            SqlCommand sqlCommand = new SqlCommand("Select * From Student", connection);
            SqlDataAdapter sqlDataAdapter=new SqlDataAdapter(sqlCommand);
            //bir veri komutlarını ve SQL Server veritabanını doldurmak DataSet ve güncelleştirmek için kullanılan veritabanı bağlantısını temsil eder.
            DataTable dataTable =new DataTable();
            sqlDataAdapter.Fill(dataTable);
            dataGridViewStudent.DataSource = dataTable;

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-ARML8T9\\SQLEXPRESS;Initial Catalog=DbOgrenci;Integrated Security=True");
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand("Delete From Student Where StudentId='"+int.Parse(txtId.Text)+"'", connection);
            sqlCommand.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("deleted");
            BindData();
            foreach (var item in Controls)
            {
                var textboxes = item as TextBox;
                if (textboxes != null)
                {
                    textboxes.Clear();
                }
            }

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-ARML8T9\\SQLEXPRESS;Initial Catalog=DbOgrenci;Integrated Security=True");
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand("update Student set StudentName='" + txtAd.Text + "',StudentAdress='" + txtAdress.Text + "',City='" + txtCity.Text + "',Number='" + txtNumber.Text+"'Where StudentId='"+int.Parse(txtId.Text)+ "'" , connection);
            sqlCommand.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Guncellendi");
            BindData();
            foreach (var item in Controls)
            {
                var textboxes = item as TextBox;
                if (textboxes != null)
                {
                    textboxes.Clear();
                }
            }



        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
