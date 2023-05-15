using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace File_cabinet_app
{
    public partial class App_interface : Form
    {
        public App_interface()
        {
            InitializeComponent();
        }

        BindingList<Auditorium> data1 = new BindingList<Auditorium>();
        BindingList<Groups_of_students> data2 = new BindingList<Groups_of_students>();

        public void DGRefresh1()
        {
            data1.Clear();
            using (ApplicationContext db = new ApplicationContext())
            {
                var infolist = db.auditorium.FromSqlRaw("SELECT * FROM auditorium").ToList();
                for (int i = 0; i < infolist.Count(); i++)
                {
                    data1.Add(infolist[i]);
                    dataGridView1.DataSource = data1;
                }
            }
        }
        public void DGRefresh2()
        {
            data2.Clear();
            using (ApplicationContext db = new ApplicationContext())
            {
                var infolist = db.groups_of_students.FromSqlRaw("SELECT * FROM groups_of_students").ToList();
                for (int i = 0; i < infolist.Count(); i++)
                {
                    data2.Add(infolist[i]);
                    dataGridView2.DataSource = data2;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = data1;
            dataGridView2.DataSource = data2;

            DGRefresh1();
        }


        private void App_interface_Load(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                var ap = db.Document.ToList();
                foreach (var item in ap)
                {
                    comboBox1.Items.Add(item.naimenovaniye);

                }

                var dol = db.Dolzhnosti.ToList();
                foreach (var item in dol)
                {
                    comboBox2.Items.Add(item.nazvaniye);

                }

                var sotrudniki = db.Sotrudniki.ToList();
                dataGridView1.RowCount = sotrudniki.Count;

                int index = 0;
                foreach (var item in sotrudniki)
                {
                    dataGridView1.Rows[index].Cells[0].Value = item.nomer_sotrudnika;

                    dataGridView1.Rows[index].Cells[1].Value = item.nomer_punkta;
                    dataGridView1.Rows[index].Cells[2].Value = item.nomer_dolzhnosti;
                    dataGridView1.Rows[index].Cells[3].Value = item.fio;
                    dataGridView1.Rows[index].Cells[4].Value = item.data_rozhdeniya;
                    dataGridView1.Rows[index].Cells[5].Value = item.telefon;
                    dataGridView1.Rows[index].Cells[6].Value = item.adres;
                    foreach (var item1 in ap)
                    {
                        if (item.nomer_punkta == item1.nomer_punkta)
                        {
                            dataGridView1.Rows[index].Cells[1].Value = item1.naimenovaniye;

                        }
                    }
                    foreach (var item2 in dol)
                    {
                        if (item.nomer_dolzhnosti == item2.nomer_dolzhnosti)
                        {
                            dataGridView1.Rows[index].Cells[2].Value = item2.nazvaniye;

                        }
                    }
                    index++;
                }
            }
        }

        public class Document
        {
            [Key]
            public string document_name { get; set; }
            public string detailed_description { get; set; }
            public int addressee_id { get; set; }
            public int document_id { get; set; }
            public Document(string document_name, string detailed_description, int addressee_id, int document_id)
            {
                this.document_name = document_name;
                this.detailed_description = detailed_description;
                this.addressee_id = addressee_id;
                this.document_id = document_id;
            }
        }
    }
}
}
