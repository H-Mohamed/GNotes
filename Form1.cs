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
        #region DB_VARIABLES
        //   BindingManagerBase B_Prof;
        BindingSource _BSprof = new BindingSource();
        //  BindingManagerBase B_Etudiant;
        BindingSource _BSEtudiant = new BindingSource();
        //   BindingManagerBase B_module;
        BindingSource _BsModule = new BindingSource();
        //   BindingManagerBase B_Notes; 
        BindingSource _BSNotes = new BindingSource();
        SqlConnection Cnx = new SqlConnection(@"Data Source=HARIRMOHAMED\TDI201_2018;Initial Catalog=DB_SAIR_Notes;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        //  SqlDataReader dr;
        DataSet ds = new DataSet();
        #endregion
        #region Form Load & Controls Initial Predefined Behaviour
        //tabs
        // default localtion=new Point(215, 48);
        // default Size = new Size(787, 462); 

        //Buttons  https://flatuicolors.com/palette/fr
        //active     Good SAMARITAN      Color.FromArgb(1,60, 99, 130) rgba(60, 99, 130,1.0);
        //default    Forest Blues        Color.FromArgb(1,10, 61, 98) rgba(10, 61, 98,1.0);
        //Hover      Dupan               Color.FromArgb(1,96, 163, 188) rgba(96, 163, 188,1.0);
        public Form1()
        {
            InitializeComponent();
            set_panel_btn_Behavior();
            set_click_btn_behaviour();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dB_SAIR_NotesDataSet9.Professeur' table. You can move, or remove it, as needed.
            this.professeurTableAdapter.Fill(this.dB_SAIR_NotesDataSet9.Professeur);
            // TODO: This line of code loads data into the 'dB_SAIR_NotesDataSet8.R2' table. You can move, or remove it, as needed.
            this.r2TableAdapter.Fill(this.dB_SAIR_NotesDataSet8.R2);
            // TODO: This line of code loads data into the 'dB_SAIR_NotesDataSet7.R3' table. You can move, or remove it, as needed.
            this.r3TableAdapter.Fill(this.dB_SAIR_NotesDataSet7.R3);
            // TODO: This line of code loads data into the 'dB_SAIR_NotesDataSet6.R4' table. You can move, or remove it, as needed.
            this.r4TableAdapter.Fill(this.dB_SAIR_NotesDataSet6.R4);
            // TODO: This line of code loads data into the 'dB_SAIR_NotesDataSet5.R5' table. You can move, or remove it, as needed.
            this.r5TableAdapter.Fill(this.dB_SAIR_NotesDataSet5.R5);
            // TODO: This line of code loads data into the 'dB_SAIR_NotesDataSet4.R6' table. You can move, or remove it, as needed.
            this.r6TableAdapter.Fill(this.dB_SAIR_NotesDataSet4.R6);
            // TODO: This line of code loads data into the 'dB_SAIR_NotesDataSet3.R7' table. You can move, or remove it, as needed.
            this.r7TableAdapter.Fill(this.dB_SAIR_NotesDataSet3.R7);
            // TODO: This line of code loads data into the 'dB_SAIR_NotesDataSet2.R8' table. You can move, or remove it, as needed.
            this.r8TableAdapter.Fill(this.dB_SAIR_NotesDataSet2.R8);
            // TODO: This line of code loads data into the 'dB_SAIR_NotesDataSet.Etudiant' table. You can move, or remove it, as needed.
            this.etudiantTableAdapter1.Fill(this.dB_SAIR_NotesDataSet.Etudiant);
            // TODO: This line of code loads data into the 'iDETUDIANTS.Etudiant' table. You can move, or remove it, as needed.
            this.etudiantTableAdapter.Fill(this.iDETUDIANTS.Etudiant);
            HideTabs();
            loadData();
            foreach (Control c in Controls)
            {
                if (c is DataGridView)
                    (c as DataGridView).AutoSizeColumnsMode
                        = DataGridViewAutoSizeColumnsMode.Fill;
                // AutoSizeColumnsMode.Fill:
            }
            try
            {
                foreach (Control c in Controls)
                {
                    foreach (Control t in c.Controls)
                    {
                        if (c is DataGridView)
                            (c as DataGridView).AutoSizeColumnsMode
                                = DataGridViewAutoSizeColumnsMode.Fill;
                        // AutoSizeColumnsMode.Fill:
                    }
                }
            }
            catch
            {

            }
        }

        #region Set Controls Behaviour 
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
                case "button14":
                    tabtoshow(TabAutres);
                    loadBulletin(1); break;
                    //TabAutres
            }
        }
        private void HideTabs()
        {
            foreach (Control c in this.Controls)
            {
                if ((c is TabControl)) { c.Hide(); c.Location = new Point(215, 48); c.Size = new Size(787, 462); }
            }
        }
        //Panel1 Buttons
        public void set_panel_btn_Behavior()
        {
            foreach (Control c in panel1.Controls)
            {
                if (c is Button)
                {
                    BtnStatus((c as Button), 1);
                    c.Leave += (Object sender, EventArgs e) => {
                        BtnStatus((c as Button), 2);
                    };
                    c.Click += (Object sender, EventArgs e) => {
                        ButtonClick((c as Button));
                    };
                }
            }
        }
        //Onclick Load assoc GDV
        private void loadData()
        {
            Navigation_init();
            try
            {
                HideTabs();
                load_DGV(ProfDGV, PROC_DS("getProf").Tables[0]);
                load_DGV(ETUDGV, PROC_DS("getEtud").Tables[0]);
                load_DGV(ModDGV, PROC_DS("getModu").Tables[0]);
                load_DGV(NotesDGV, PROC_DS("getNotes").Tables[0]);
                button1.Click += (object f, EventArgs j) =>
                {
                    load_DGV(ProfDGV, PROC_DS("getProf").Tables[0]);
                }; button2.Click += (object f, EventArgs j) =>
                {
                    load_DGV(ETUDGV, PROC_DS("getEtud").Tables[0]);
                }; button3.Click += (object f, EventArgs j) =>
                {
                    load_DGV(ModDGV, PROC_DS("getModu").Tables[0]);
                }; button4.Click += (object f, EventArgs j) =>
                {
                    load_DGV(NotesDGV, PROC_DS("getNotes").Tables[0]);
                };
            }
            catch (Exception r)
            {
                HideTabs();
                load_DGV(ProfDGV, PROC_DS("getProf").Tables[0]);
                load_DGV(ETUDGV, PROC_DS("getEtud").Tables[0]);
                load_DGV(ModDGV, PROC_DS("getModu").Tables[0]);
                load_DGV(NotesDGV, PROC_DS("getNotes").Tables[0]);
            }
        }
        //NAVIGATION
        private void Navigation_init()
        {
            try
            {
                //init BINDING SOURCES
                _BSEtudiant.DataSource = PROC_DS("getEtud").Tables[0];
                _BSprof.DataSource = PROC_DS("getProf").Tables[0];
                _BsModule.DataSource = PROC_DS("getModu").Tables[0];
                _BSNotes.DataSource = PROC_DS("getNotes").Tables[0];
                //bind textboxes
                //prof
                profcode.DataBindings.Add("text", _BSprof, "codeP");
                profnom.DataBindings.Add("text", _BSprof, "nomP");
                profprenom.DataBindings.Add("text", _BSprof, "prenomP");
                profdipl.DataBindings.Add("text", _BSprof, "diplome");
                profemail.DataBindings.Add("text", _BSprof, "Contact");
                //etudiant
                etucode.DataBindings.Add("text", _BSEtudiant, "codeE");
                etunom.DataBindings.Add("text", _BSEtudiant, "nom");
                etuprenom.DataBindings.Add("text", _BSEtudiant, "prenom");
                etudateN.DataBindings.Add("text", _BSEtudiant, "dateNaissance");
                etuemail.DataBindings.Add("text", _BSEtudiant, "email");
                //modules
                modCode.DataBindings.Add("text", _BsModule, "codeM");
                modNom.DataBindings.Add("text", _BsModule, "nomM");
                modCodeProf.DataBindings.Add("text", _BsModule, "codeP");
                modDPrevue.DataBindings.Add("text", _BsModule, "dateP");
                modDpassasion.DataBindings.Add("text", _BsModule, "dateR");
                //notes
                NotecodeEtudiant.DataBindings.Add("text", _BSNotes, "codeEtudiant");
                NotecodeModule.DataBindings.Add("text", _BSNotes, "codeModule");
                NoteValeur.DataBindings.Add("text", _BSNotes, "note");
            }
            catch (Exception m)
            {

            }
            ////Navigation btn set
        }
        private void BtnStatus(Button b, int status)
        {
            switch (status)
            {
                //Active
                case 1: b.BackColor = Color.FromArgb(1, 60, 99, 130); break;
                //in
                case 2: b.BackColor = Color.FromArgb(1, 96, 163, 188); break;
                //out     
                case 3: b.BackColor = Color.FromArgb(1, 10, 61, 98); break;
                default: b.BackColor = Color.FromArgb(1, 10, 61, 98); break;
            }
        }
        //EXIT btn
        private void button14_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #endregion
        #region Bindings & data processing
        public void InitCMD(string command, CommandType T)
        {
            cmd.Parameters.Clear();
            cmd.CommandText = command;
            cmd.CommandType = T;
            cmd.Connection = Cnx;
        }
        public void PROC_NoDS_Params(string Proc, SqlParameter[] Params)
        {
            InitCMD(Proc, CommandType.StoredProcedure);
            cmd.Parameters.Clear();
            cmd.Parameters.AddRange(Params);
            cmd.ExecuteNonQuery();
        }
        public DataSet PROC_DS(string Proc)
        {
            ds = new DataSet();
            InitCMD(Proc, CommandType.StoredProcedure);
            cmd.Parameters.Clear();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public DataSet PROC_DS_Params(string Proc, SqlParameter[] Params)
        {
            try
            {
                InitCMD(Proc, CommandType.StoredProcedure);
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(Params);
                da = new SqlDataAdapter(cmd);
                ds.Clear();
                da.Fill(ds, "T");
                return ds;
            }
            catch
            {
                return new DataSet();
            }
        }

        public void load_DGV(DataGridView D, DataTable Table)//,BindingManagerBase b,string b_datamember)
        {
            try
            {
                // D.SelectionMode = SelectionMode.One;
                D.Columns.Clear();
                D.RowHeadersVisible = false;
                D.DataSource = Table;
                //  DataTable t = Table.Clone();
                //    b = BindingContext[t,b_datamember];
            }
            catch (Exception r)
            {
                error(r);
            }
        }
        #endregion
        #region Reporting 
        private void loadCR()
        {
            CrystalReport1 c = new CrystalReport1();
            c.SetDataSource(PROC_DS("ReportingP1").Tables[0]);
            CRV1.ReportSource = c;
            CRV1.Refresh();
        }
        private void loadBulletin(int idEtudiant)
        {
            QuestionB_Bulletin c = new QuestionB_Bulletin();

            c.SetParameterValue("@idE", idEtudiant);
            //c.SetDataSource(PROC_DS_Params("getDetailsEtudiantB",new SqlParameter[] { new SqlParameter("@idE", idEtudiant) }).Tables[0]);
            CRV2.ReportSource = c;
            CRV2.Refresh();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ComboBox c = comboBox1;

        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                loadBulletin(int.Parse(comboBox4.SelectedValue.ToString()));
            }
            catch
            {
                Refresh();
            }
        }
        #endregion
        #region //GESTION PROFESSEUR

        //Ajouter Professeur
        private void AjouterProf()
        {
            try
            {
                load_DGV(ProfDGV,
           (PROC_DS_Params("ADDPROF", new SqlParameter[]
           { new SqlParameter("@CODEPROF",int.Parse(profcode.Text)), new SqlParameter("@NOMPROF",profnom.Text)
            , new SqlParameter("@PRENOMPROF",profprenom.Text), new SqlParameter("@DIPLOMEPROF",profdipl.Text)
            , new SqlParameter("@EMAILPROF",profemail.Text)
           })).Tables[0]
                 ); 
            }
            catch (Exception r)
            {
                error(r);
            }
        }
        //Suppression
        private void SuppProf()
        {
            try
            {
                load_DGV(
                    ProfDGV,
                      (PROC_DS_Params("DELPROF", new SqlParameter[]
                    { new SqlParameter("@CP",profcode.Text) })).Tables[0]
                 ); 

            }
            catch
            {
                MessageBox.Show(profcode.Text + " > ne correspond à aucun enregistrement ");
            }
        }
        //Modification
        private void ModifProf()
        {
            try
            {
                load_DGV(
                    ProfDGV,
                    (PROC_DS_Params("UPDPROF", new SqlParameter[]
                    {  new SqlParameter("@CODEPROF",profcode.Text), new SqlParameter("@NOMPROF",profnom.Text)
                     , new SqlParameter("@PRENOMPROF",profnom.Text), new SqlParameter("@DIPLOMEPROF",profnom.Text)
                     , new SqlParameter("@EMAILPROF",profnom.Text)
                     })).Tables[0]
                    ); 

            }
            catch
            {
                MessageBox.Show(profcode.Text + " > ne correspond à aucun enregistrement ");
            }
        }         
        #endregion
        #region //GESTION ETUDIANT

        //Ajouter Etudiant 
        private void AjouterETD()
        {
            load_DGV(ETUDGV,
            (PROC_DS_Params("ADDETUD", new SqlParameter[]
            { new SqlParameter("@CODEET",etucode.Text), new SqlParameter("@NOM",etunom.Text)
            , new SqlParameter("@PRENOM",etuprenom.Text), new SqlParameter("@DATEN",DateTime.Parse(etudateN.Value.ToShortDateString().ToString()))
            , new SqlParameter("@EMAIL",etuemail.Text)
            })).Tables[0]
                     );
        }
        //Suppression
        private void SuppETD()
        {
            try
            {
                load_DGV(
                    ProfDGV,
                      (PROC_DS_Params("DELETD", new SqlParameter[]
                    { new SqlParameter("@CE",etucode.Text) })).Tables[0]
                     ); 
            }
            catch
            {
                MessageBox.Show(profcode.Text + " > ne correspond à aucun enregistrement ");
            }
        }
        //Modification
        private void ModifETD()
        {
            try
            {
                load_DGV(ETUDGV,
           (PROC_DS_Params("DELETD", new SqlParameter[]
           { new SqlParameter("@CODEET",etucode.Text), new SqlParameter("@NOM",etunom.Text)
            , new SqlParameter("@PRENOM",etuprenom.Text), new SqlParameter("@DATEN",DateTime.Parse(etudateN.Value.ToShortDateString().ToString()))
            , new SqlParameter("@EMAIL",etuemail.Text)
           })).Tables[0]
                    ); ;
            }
            catch
            {
                MessageBox.Show(profcode.Text + " > ne correspond à aucun enregistrement ");
            }
        }

        #endregion
        #region //GESTION Modules

        //Ajouter Module 
        private void AjouterMOD()
        {
            load_DGV(ModDGV,
            (PROC_DS_Params("ADDMOD", new SqlParameter[]
            { new SqlParameter("@CM",int.Parse(modCode.Text)), new SqlParameter("@NOMM",modNom.Text)
            , new SqlParameter("@CP",int.Parse(modCodeProf.Text)),
                new SqlParameter("@DATEPREVUE",DateTime.Parse(modDPrevue.Text.ToString()))
            , new SqlParameter("@DATEREEL",DateTime.Parse(modDpassasion.Text.ToString()))
            })).Tables[0]
                     ); 
        }
        //Suppression
        private void SuppMOD()
        {
            try
            {
                load_DGV(ModDGV,
                    ( PROC_DS_Params("DELMOD", new SqlParameter[]
                    { new SqlParameter("@CM",int.Parse(modCode.Text)) })).Tables[0]
                     );
            }
            catch
            {
                MessageBox.Show(profcode.Text + " > ne correspond à aucun enregistrement ");
            }
        }
        //Modification
        private void ModifMOD()
        {
            try
            {
                load_DGV(ModDGV,
                (PROC_DS_Params("UPDMOD", new SqlParameter[]
                { new SqlParameter("@CM",int.Parse(modCode.Text)), new SqlParameter("@NOMM",modNom.Text)
            , new SqlParameter("@CP",int.Parse(modCodeProf.Text)),
                new SqlParameter("@DATEPREVUE",DateTime.Parse(modDPrevue.Text.ToString()))
            , new SqlParameter("@DATEREEL",DateTime.Parse(modDpassasion.Text.ToString()))
                })).Tables[0]
                         );

            }
            catch
            {
                MessageBox.Show(profcode.Text + " > ne correspond à aucun enregistrement ");
            }
        } 
        #endregion
        #region //GESTION Notes
        //BINDINGMANAGERBASE NOTES

        //Ajouter Note 
        private void AjouterNOT()
        {
            load_DGV(NotesDGV,
            (PROC_DS_Params("ADDNOTE", new SqlParameter[]
            { new SqlParameter("@CODEE",NotecodeEtudiant.Text)
            , new SqlParameter("@CODEM",NotecodeModule.Text)
            , new SqlParameter("@Note",NoteValeur.Text)
            })).Tables[0]
                     );
        }
        //Suppression
        private void SuppNOT()
        {
            try
            {
                load_DGV(
                    NotesDGV,
                      (PROC_DS_Params("DELNOTE", new SqlParameter[]
                    { new SqlParameter("@CE",NotecodeEtudiant.Text), new SqlParameter("@CM",NotecodeModule.Text) })).Tables[0]
                     ); 
            }
            catch
            {
                MessageBox.Show(" ERROR > ne correspond à aucun enregistrement !! ");
            }
        }
        //Modification
        private void ModifNOT()
        {
            try
            {
                load_DGV(
                    NotesDGV,
                    (PROC_DS_Params("UPDNOTE", new SqlParameter[]
                    {  new SqlParameter("@CE",NotecodeEtudiant.Text)
                     , new SqlParameter("@CM",NotecodeModule.Text)
                     , new SqlParameter("@NOTE",NoteValeur.Text)
                     })).Tables[0]
                ); 
                  
            }
            catch
            {
                MessageBox.Show(profcode.Text + " > ne correspond à aucun enregistrement ");
            }
        }

        

        

        #endregion
        #region Data Process Behaviour // adding events 
        public void set_click_btn_behaviour()
        {
            //btns Prof
            DELPROF.Click += (object sender, EventArgs e) =>
            {
                SuppProf();
            };
            ADDPROF.Click += (object sender, EventArgs e) =>
            {
                AjouterProf();
            };
            UPDPROF.Click += (object sender, EventArgs e) =>
            {
                ModifProf();
            };

            //btns Etudiant

            DELETD.Click += (object sender, EventArgs e) =>
            {
                SuppETD();
            };
            ADDETD.Click += (object sender, EventArgs e) =>
            {
                AjouterETD();
            };
            UPDETD.Click += (object sender, EventArgs e) =>
            {
                ModifETD();
            };

            //btns Module

            DELMOD.Click += (object sender, EventArgs e) =>
            {
                SuppMOD();
            };
            ADDMOD.Click += (object sender, EventArgs e) =>
            {
                AjouterMOD();
            };
            UPDMOD.Click += (object sender, EventArgs e) =>
            {
                ModifMOD();
            };

            //btns Note

            DELNOTE.Click += (object sender, EventArgs e) =>
            {
                SuppNOT();
            };
            ADDNOTE.Click += (object sender, EventArgs e) =>
            {
                AjouterNOT();
            };
            UPDNOTE.Click += (object sender, EventArgs e) =>
            {
                ModifNOT();
            };
            //Navigations
            Navigation_btn_set();
        }
        private void Navigation_btn_set()
        {
            //prof
            ProfNext.Click += (object s, EventArgs e) =>
            {
                _BSprof.MoveNext();
            };
            Profbefore.Click += (object s, EventArgs e) =>
            {
                _BSprof.MovePrevious();
            };
            ProfFisrt.Click += (object s, EventArgs e) =>
            {
                _BSprof.MoveFirst();
            };
            ProfLast.Click += (object s, EventArgs e) =>
            {
                _BSprof.MoveLast();
            };
            //etudiant
            EtudiantNext.Click += (object s, EventArgs e) =>
            {
                _BSEtudiant.MoveNext();
            };
            Etudiantbefore.Click += (object s, EventArgs e) =>
            {
                _BSEtudiant.MovePrevious();
            };
            EtudiantFirst.Click += (object s, EventArgs e) =>
            {
                _BSEtudiant.MoveFirst();
            };
            EtudiantLast.Click += (object s, EventArgs e) =>
            {
                _BSEtudiant.MoveLast();
            };
            //module
            ModuleFisrt.Click += (object s, EventArgs e) =>
            {
                _BsModule.MoveFirst();
            };
            ModuleLast.Click += (object s, EventArgs e) =>
            {
                _BsModule.MoveLast();
            };
            ModuleNext.Click += (object s, EventArgs e) =>
            {
                _BsModule.MoveNext();
            };
            Modulebefore.Click += (object s, EventArgs e) =>
            {
                _BsModule.MovePrevious();
            };
            //note
            NoteFirst.Click += (object s, EventArgs e) =>
            {
                _BSNotes.MoveFirst();
            };
            Notelast.Click += (object s, EventArgs e) =>
            {
                _BSNotes.MoveLast();
            };
            Notenext.Click += (object s, EventArgs e) =>
            {
                _BSNotes.MoveNext();
            };
            Notebefore.Click += (object s, EventArgs e) =>
            {
                _BSNotes.MovePrevious();
            };

        }

        #endregion
        #region APPLI OTHER FCT 
        private void button42_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void error(Exception t)
        {
            MessageBox.Show(t.Message + "\nDetails:" + t.TargetSite, "Erreur | " + t.TargetSite, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            loadData();
            MessageBox.Show(_BSEtudiant.Count + " Etudiant <> " +
                            _BsModule.Count + " Module <> " +
                            _BSNotes.Count + " Note <> " +
                            _BSprof.Count + " Profs <> ");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CountETD.Text = PROC_DS("R1").Tables[0].Rows[0][0].ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DGVR2.DataSource = PROC_DS("R2").Tables[0];
            DGVR2.Refresh();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataGridView1.Refresh();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            dataGridView2.Refresh();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            dataGridView3.Refresh();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            dataGridView4.Refresh();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            dataGridView5.Refresh();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            dataGridView6.Refresh();
        }

        #endregion 
    }
}
