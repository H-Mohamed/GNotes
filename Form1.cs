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

namespace DB_SAIR_NOTES
{
    public partial class Form1 : Form
    {
        BindingManagerBase B; 
         SqlConnection Cnx = new SqlConnection(@"Data Source=HARIRMOHAMED\TDI201_2018;Initial Catalog=DB_SAIR_Notes;Integrated Security=True");
         SqlCommand cmd = new SqlCommand();
         SqlDataAdapter da = new SqlDataAdapter();
         SqlDataReader dr;
         DataSet ds;
        public void init(string cmmd, int type)
        {
            if (type == 1) cmd.CommandType = CommandType.StoredProcedure;
            if (type == 2) cmd.CommandType = CommandType.Text;
            cmd.Connection = Cnx;
            cmd.CommandText = cmmd;
        }

        #region Data & Initial Predefined Behaviour
        //tabs
        // default localtion=new Point(215, 12);
        // default Size = new Size(787, 462); 

        //Buttons  https://flatuicolors.com/palette/fr
        //active     Good SAMARITAN      Color.FromArgb(1,60, 99, 130) rgba(60, 99, 130,1.0);
        //default    Forest Blues        Color.FromArgb(1,10, 61, 98) rgba(10, 61, 98,1.0);
        //Hover      Dupan               Color.FromArgb(1,96, 163, 188) rgba(96, 163, 188,1.0);
        public Form1()
        {
            InitializeComponent();
            setbtn_Behavior();
        }
        #region Set Controls Behaviour
        /*
         
             */
        private void Form1_Load(object sender, EventArgs e)
        {
            HideTabs();
        }
        public void tabtoshow(TabControl T)
        {
            HideTabs();
            T.Show();
        }
        public void ButtonClick(Button b)
        {
            switch (b.Name)
            {
                case "button1": tabtoshow(Proftab); break;
                case "button2": tabtoshow(Etudtab); break;
                case "button3": tabtoshow(ModuleTab); break;
                case "button4": tabtoshow(NotesTab); break; 
            }
        }
        private void HideTabs()
        {
            foreach (Control c in this.Controls)
            {
                if ((c is TabControl)) { c.Hide(); c.Location = new Point(215, 12); c.Size = new Size(787, 462); }
            }
        }
        //Panel1 Buttons
        public void setbtn_Behavior()
        {
            foreach (Control c in panel1.Controls)
            {
                if(c is Button)
                {
                    BtnStatus((c as Button), 3);
                    c.Leave += (Object sender, EventArgs e) => {
                        BtnStatus((c as Button), 2);
                    };
                    c.Click += (Object sender, EventArgs e) => {
                        ButtonClick((c as Button));
                    };
                }
            }
        }
        private void BtnStatus(Button b, int status)
        {
            switch(status)
            {   
                //Active
                case 1:b.BackColor = Color.FromArgb(1, 60, 99, 130); break;
                //in
                case 2:b.BackColor= Color.FromArgb(1, 96, 163, 188); break;
                //out     
                case 3: b.BackColor = Color.FromArgb(1, 10, 61, 98); break;
                default: b.BackColor= Color.FromArgb(1, 10, 61, 98); break;
            } 
        }
        #endregion
        private void button14_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion
        
        #region Bindings & data processing
        

        #endregion
    }
}
