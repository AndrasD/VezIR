using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;
using System.Data;
using System.Threading;
using System.Drawing;
using FakPlusz;

namespace FakPlusz.Alapfunkciok
{
    /// <summary>
    /// Comboinfok gyujtemenye
    /// </summary>
    public class ComboCollection
    {
        /// <summary>
        /// Informacio minden olyan tablainformaciorol, mely a tervezes szerint szerepelhet comboban a tabla leirasakor
        /// </summary>
        public KulonlegesComboinfok ComboazontipCombok = new KulonlegesComboinfok();
        /// <summary>
        /// Informacio minden olyan tablainformaciorol, mely a tervezes szerint lehet osszefugges 
        /// </summary>
        public KulonlegesComboinfok LehetOsszefCombok = new KulonlegesComboinfok();
        /// <summary>
        /// Informacio minden olyan tablainformaciorol, mely a tervezes szerint lehet csoportmeghatarozas
        /// </summary>
        public KulonlegesComboinfok LehetCsoportCombok = new KulonlegesComboinfok();
        /// <summary>
        /// Minden olyan kodtabla vagy kodtablajellegu tabla aktualis tartalma, mely Combo-ban szerepelhet 
        /// </summary>
        public ArrayList AlapCombok = new ArrayList();
        private Tablainfo _kiajanlinfo = null;
        /// <summary>
        /// Kiajanlasok Tablainfoja, ha van
        /// </summary>
        public Tablainfo Kiajanlinfo
        {
            get { return _kiajanlinfo; }
            set
            {
                _kiajanlinfo = value;
                for (int i = 0; i < AlapCombok.Count; i++)
                {
                    Comboinfok egyinf = (Comboinfok)AlapCombok[i];
                    string azontip = egyinf.Combotag.Azonositok.Azontip;
                    string[] van = _kiajanlinfo.Fak.GetTartal(_kiajanlinfo, "AZONTIP", "AZONTIP", azontip);
                    if(van!=null ) //|| van.Length==0)
//                    if(egyinf.Combotag.Azonositok.Adatfajta=="S")
                        AlapComboAdd(egyinf.Combotag);
                }
            }
        }
            /// <summary>
            /// Comboinformaciok gyujtemenye
            /// </summary>
        public ComboCollection()
        {
            ComboazontipCombok.Infoba();
        }
        /// <summary>
        /// hozzaadas AlapCombok-hoz
        /// </summary>
        /// <param name="tag">
        /// A tabla TablainfoTag-ja
        /// </param>
        public void AlapComboAdd(TablainfoTag tag)
        {
            bool megvan = false;
            foreach (Comboinfok egycomboinf in AlapCombok)
            {
                if (egycomboinf.Combotag.Azonositok.Azontip == tag.Azonositok.Azontip)
                {
                    egycomboinf.Infoba(tag);
                    egycomboinf.SetDefertekek();
                    megvan = true;
                    break;
                }
            }
            if (!megvan)
            {
                Comboinfok egycomboinf = new Comboinfok();
                egycomboinf.Infoba(tag);
                AlapCombok.Add(egycomboinf);
                egycomboinf.ComboCollection = this;
                egycomboinf.SetDefertekek();
            }
            
        }
        /// <summary>
        /// AlapCombok-ban tag alapjan keresi a megfelelot
        /// </summary>
        /// <param name="tag">
        /// ezt keresi
        /// </param>
        /// <returns></returns>
        public Comboinfok AlapComboFind(TablainfoTag tag)
        {
            foreach (Comboinfok egycomboinf in AlapCombok)
                for (int i = 0; i < AlapCombok.Count; i++)
                {
                    if (egycomboinf.Combotag.Azonositok.Azontip == tag.Azonositok.Azontip
                        || egycomboinf.Combotag.Azonositok.Azontip == tag.Azonositok.Szrmazontip)
                        return egycomboinf;
                }
            return null;
        }
        /// <summary>
        /// Kereses teljes azonosito alapjan
        /// </summary>
        /// <param name="azontip">
        /// kivant azonosito
        /// </param>
        /// <returns>
        /// a megfelelo Comboinfo vagy null
        /// </returns>
        public Comboinfok ComboinfoKeres(string azontip)
        {
            return ComboinfoKeres(azontip, null,null, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="azontip"></param>
        /// <param name="tabinfo"></param>
        /// <param name="egycol"></param>
        /// <param name="combo"></param>
        /// <returns></returns>
        public Comboinfok ComboinfoKeres(string azontip, Tablainfo tabinfo, Cols egycol, ComboBox combo)
        {
            Comboinfok egycomboinf = null;
            foreach (Comboinfok egycombo in AlapCombok)
            {
                if (egycombo.Combotag.Azonositok.Azontip == azontip)
                {
                    egycomboinf = egycombo;
                    break;
                }
            }
            if (egycomboinf == null)
                return null;
            else if (tabinfo != null)
            {
                if (tabinfo.Tablanev == "KIAJANL")
                {
//                    Kiajanlinfo = tabinfo;
                    egycol.Combo_Info = egycomboinf;
                    if (tabinfo.ViewSorindex == -1)
                    {
                        egycol.Tartalom = "0";
                        tabinfo.InputColumns[egycol.ColumnName].Tartalom = "0";
                        egycol.ComboAktFileba = egycomboinf.ComboFileinfo[0].ToString();
                        egycol.ComboAktSzoveg = egycomboinf.ComboInfo[0].ToString();
                        egycol.Kiegcol.Combo_Info = egycomboinf;
                        egycol.Kiegcol.ComboAktFileba = egycol.ComboAktFileba;
                        egycol.Kiegcol.ComboAktSzoveg = egycol.ComboAktSzoveg;
                    }
                    else
                    {
                        DataRow row = tabinfo.AktualViewRow;
                        egycol.Tartalom = row["RSORSZAM"].ToString();
                        tabinfo.InputColumns[egycol.ColumnName].Tartalom = egycol.Tartalom;
                        egycol.ComboAktFileba = egycol.Tartalom;
                        int i = egycomboinf.ComboFileinfo.IndexOf(egycol.Tartalom);
                        if (i == -1)
                            egycol.ComboAktSzoveg = "";
                        else
                            egycol.ComboAktSzoveg = egycomboinf.ComboInfo[i].ToString();
                        egycol.Kiegcol.Combo_Info = egycomboinf;
                        egycol.Kiegcol.ComboAktFileba = egycol.ComboAktFileba;
                        egycol.Kiegcol.ComboAktSzoveg = egycol.ComboAktSzoveg;
                        if (combo != null)
                        {
                            combo.Items.Clear();
                            combo.Items.AddRange(egycomboinf.ComboSzovinfoAll());
                        }
                    }
                }
            }
            //else if (Kiajanlinfo != null)
            //{
            //    string azontipus = egycomboinf.Combotag.Azonositok.Azontip;
            //    Kiajanlinfo.DataView.RowFilter = "AZONTIP = '" + azontipus + "'";
            //    if (Kiajanlinfo.DataView.Count == 0)
            //        DefFileba = egycomboinf.ComboFileinfoAll()[0];
            //    else
            //        DefFileba = Kiajanlinfo.DataView[0].Row["RSORSZAM"].ToString();
            //}

            return egycomboinf;
        }
    }
    /// <summary>
    /// Kulonleges Comboinfok, csak a Tervezonek kell
    /// </summary>
    public class KulonlegesComboinfok
    {
        /// <summary>
        /// A comboinfok TablainfoTag-jai
        /// </summary>
        public ArrayList Tagok = new ArrayList();
        /// <summary>
        /// A hivatkozo tablainformacio objectumok
        /// </summary>
        public TablainfoCollection Tabinfok = new TablainfoCollection();
        /// <summary>
        /// Rogziteskor ezek kozul kerulnek a tablaba
        /// </summary>
        public ArrayList ComboFileinfo = new ArrayList();
        /// <summary>
        /// A szoveges valasztek
        /// </summary>
        public ArrayList ComboInfo = new ArrayList();
        /// <summary>
        /// Combofileinfo tomb-kent
        /// </summary>
        public string[] Fileba
        {
            get { return (string[])ComboFileinfo.ToArray(typeof(string)); }
        }
        /// <summary>
        /// ComboInfo tombkent
        /// </summary>
        public string[] Szovegbe
        {

            get
            {
                ArrayList ar = new ArrayList();
                ar.AddRange(ComboInfo);
                ar.Sort();
                return (string[])ar.ToArray(typeof(string));
            }
        }
        /// <summary>
        /// Kiegeszito (regebbi verziobol, azota mar torolt) rogzitett
        /// </summary>
        public ArrayList KiegFileinfo = new ArrayList();
        /// <summary>
        /// Kiegeszito megjelenitendo
        /// </summary>
        public ArrayList KiegInfo = new ArrayList();
        /// <summary>
        /// A kiegeszitok identity-je
        /// </summary>
        public ArrayList KiegId = new ArrayList();
        /// <summary>
        /// KiegFileinfo tombkent
        /// </summary>
        public string[] KiegFileba
        {
            get { return (string[])KiegFileinfo.ToArray(typeof(string)); }
        }
        /// <summary>
        /// KiegInfo tombkent
        /// </summary>
        public string[] KiegSzovegbe
        {
            get
            {
                ArrayList ar = new ArrayList();
                ar.AddRange(KiegInfo);
                ar.Sort();
                return (string[])ar.ToArray(typeof(string));
            }
        }
        /// <summary>
        /// KiegId tombkent
        /// </summary>
        public string[] KiegIdk
        {
            get { return (string[])KiegId.ToArray(typeof(string)); }
        }
        /// <summary>
        /// A ComboInfo elemek maximalis hossza 
        /// </summary>
        public int Maxhossz = 0;
        /// <summary>
        /// 
        /// </summary>
        public string[] AzonositoComboSzovegbe = null;
        /// <summary>
        /// objectum eloallitasa
        /// </summary>
        public KulonlegesComboinfok()
        {
        }
        /// <summary>
        /// Valasztek elso elemenek hozzaadasa
        /// </summary>
        public void Infoba()
        {
            ComboInfo.Add("Nem Combo");
            ComboFileinfo.Add(" ");
        }
        /// <summary>
        /// Uj elem hozzaadasa, ha meg nincs meg
        /// </summary>
        /// <param name="tag">
        /// Az elem TablainfoTag-ja
        /// </param>
        public void Infoba(TablainfoTag tag)
        {
            bool megvan = false;
            foreach (TablainfoTag egytag in Tagok)
            {
                if (egytag.Azonositok.Azontip == tag.Azonositok.Azontip)
                {
                    megvan = true;
                    break;
                }
            }
            if (!megvan)
            {
                Tagok.Add(tag);
                string szov = tag.Azonositok.Szoveg;
                ComboInfo.Add(szov);
                if (szov.Length > Maxhossz)
                    Maxhossz = szov.Length;
                ComboFileinfo.Add(tag.Azonositok.Azontip);
            }
        }
        /// <summary>
        /// index megallapitas TablainfoTag alapjan
        /// </summary>
        /// <param name="tag">
        /// a TablainfoTag
        /// </param>
        /// <returns>
        /// index vagy -1
        /// </returns>
        public int Find(TablainfoTag tag)
        {
            for (int i = 0; i < Tagok.Count; i++)
            {
                if (((TablainfoTag)Tagok[i]).Azonositok.Azontip == tag.Azonositok.Azontip)
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// Torles index alapjan
        /// </summary>
        /// <param name="i">
        /// torlendo elem indexe
        /// </param>
        public void Deleteinfo(int i)
        {
            Tagok.Remove(i);
            ComboFileinfo.Remove(i);
            ComboInfo.Remove(i);
        }
        /// <summary>
        /// index megallapitas tarolt info alapjan
        /// </summary>
        /// <param name="fileinfo">
        /// a tarolt info
        /// </param>
        /// <returns>
        /// index vagy -1
        /// </returns>
        public int Find(string fileinfo)
        {
            return ComboFileinfo.IndexOf(fileinfo);
        }
        /// <summary>
        /// oszlopinformacioban tarolt tartalomhoz tartozo szoveges informacio elhelyezese az oszlopinformacioban
        /// </summary>
        /// <param name="egycol">
        /// oszlopinformacio
        /// </param>
        public void SetComboAktszoveg(Cols egycol)
        {
            Cols egyinp = egycol;
            if (egyinp != null)
            {
                int i = ComboFileinfo.IndexOf(egyinp.Tartalom);
                if (i == -1)
                    i = KiegFileinfo.IndexOf(egyinp.Tartalom);
                if (i != -1)
                    egyinp.ComboAktSzoveg = ComboInfo[i].ToString();
            }
        }
        /// <summary>
        /// Kivalasztott szoveges informaciohoz tartozo tarolando tartalom megallapitasa
        /// </summary>
        /// <param name="aktszoveg">
        /// szoveges info
        /// </param>
        /// <returns>
        /// tarolando tartalom string alakban vagy "0"
        /// </returns>
        public string GetComboAktfileba(string aktszoveg)
        {
            int i = ComboInfo.IndexOf(aktszoveg);
            if (i != -1)
                return Fileba[i];
            i = KiegInfo.IndexOf(aktszoveg);
            if (i == -1)
                return "0";
            else
                return KiegFileba[i];
        }
        /// <summary>
        /// Oszlopinformacio alapjan a hozzatartozo tablainformacio tarolasa a hivatkozo tablainformaciok kozott
        /// </summary>
        /// <param name="egycol">
        /// az oszlopinformacio
        /// </param>
        public void AttachToComboinfok(Cols egycol)
        {
            egycol.ComboAzontipCombo = this;
            Tablainfo tabinfo = egycol.Tablainfo;
            bool megvan = false;
            if (!tabinfo.Leiroe || tabinfo.Leiroe && tabinfo.Azon != "LEIR")
            {
                if (tabinfo.Leiroe)
                    tabinfo = tabinfo.LeiroTablainfo;
                foreach (Tablainfo egytabinfo in Tabinfok)
                {
                    if (egytabinfo.Leiroe == tabinfo.Leiroe && egytabinfo.Azontip == tabinfo.Azontip)
                    {
                        megvan = true;
                        break;
                    }
                }
                if (!megvan)
                {
                    Tabinfok.Csakbase = true;
                    Tabinfok.Add(tabinfo);
                    Tabinfok.Csakbase = false;
                }
            }
        }
        /// <summary>
        /// ComboItemek a ComboBox-ba, a kezdo itemindex beallitasa az aktualis rogzitett vagy default tartalom alapjan
        /// </summary>
        /// <param name="combo">
        /// ComboBox
        /// </param>
        /// <param name="egycol">
        /// oszlopinformacio
        /// </param>
        /// <param name="tartal">
        /// rogzitett vagy default tartalom
        /// </param>
        public void SetComboItems(ComboBox combo, Cols egycol, string tartal)
        {
            Cols egyinp = egycol.Kiegcol;
            if (egyinp == null)
                egyinp = egycol;
            if (egyinp != null && combo != null)
            {
                combo.Items.Clear();
                combo.Items.AddRange(Szovegbe);
            }
            int i = 0;
            if (ComboInfo.Count == 0)
                i = -1;
            if (tartal != egycol.DefaultValue.ToString())
            {
                i = ComboInfo.IndexOf(tartal);
                if (i == -1)
                    i = ComboFileinfo.IndexOf(tartal);
                if (i == -1 && tartal == egycol.OrigTartalom && ComboInfo.Count != 0)
                    i = 0;
            }
            else if (egycol.Lehetures)
                i = -1;
            if (i != -1)
            {
                egyinp.DefaultValue = ComboInfo[i].ToString();
                egyinp.ComboAktSzoveg = ComboInfo[i].ToString();
                egyinp.ComboAktFileba = ComboFileinfo[i].ToString();

                if (combo != null)
                {
                    combo.Text = egyinp.ComboAktSzoveg;
                    combo.SelectedIndex = combo.Items.IndexOf(combo.Text);
                }
            }
            else
            {
                egyinp.DefaultValue = egycol.DefaultValue.ToString();
                egyinp.ComboAktSzoveg = "";
                egyinp.ComboAktFileba = egycol.DefaultValue.ToString();
                if (combo != null)
                    combo.Text = "";
            }
        }
        /// <summary>
        /// oszlopinformacio comboinformacio megallapitasa megadott tartalom alapjan
        /// </summary>
        /// <param name="egyinfo">
        /// oszlopinformacio
        /// </param>
        /// <param name="tartal">
        /// tartalom
        /// </param>
        /// <returns>
        /// szoveges informacio
        /// </returns>
        public string Combotolt(Cols egyinfo, string tartal)
        {
            int i = ComboFileinfo.IndexOf(tartal);
            if (i == -1)
                i = 0;
            egyinfo.ComboAktFileba = ComboFileinfo[i].ToString();
            egyinfo.ComboAktSzoveg = ComboInfo[i].ToString();
            return egyinfo.ComboAktSzoveg;
        }
        /// <summary>
        /// oszlopinfo combinformaciok megallapitasa index alapjan
        /// </summary>
        /// <param name="selind">
        /// index
        /// </param>
        /// <param name="egycol">
        /// oszlopinformacio
        /// </param>
        public void SetSelectedComboInfo(int selind, Cols egycol)
        {
            Cols egyinp = egycol;
            egyinp.ComboAktSzoveg = Szovegbe[selind].ToString();
            int i = ComboInfo.IndexOf(egyinp.ComboAktSzoveg);
            egyinp.ComboAktFileba = ComboFileinfo[i].ToString();
            egyinp.Tartalom = egyinp.ComboAktFileba;
            egycol.Tartalom = egyinp.ComboAktFileba;
        }

    }
    /// <summary>
    /// hasonlit a KulonlegesComboinfok-hoz, most nincs kedvem kommentalni
    /// </summary>
    public class Comboinfok
    {
        /// <summary>
        /// tablainformaciok kollekcioja
        /// </summary>
        public TablainfoCollection Tabinfok = new TablainfoCollection();
        /// <summary>
        /// fileinformaciok listaja: file-ba ezek kozul kell rogziteni
        /// </summary>
        public ArrayList ComboFileinfo = new ArrayList();
        /// <summary>
        /// fileinformaciok tombje
        /// </summary>
        public string[] Fileba
        {
            get { return (string[])ComboFileinfo.ToArray(typeof(string)); }
        }
        /// <summary>
        /// combo-itemek listaja
        /// </summary>
        public ArrayList ComboInfo = new ArrayList();
        /// <summary>
        /// combo-itemek tombje
        /// </summary>
        public string[] Szovegbe
        {
            get { return (string[])ComboInfo.ToArray(typeof(string)); }
        }
        /// <summary>
        /// combo adattablabol id-k listaja
        /// </summary>
        public ArrayList ComboId = new ArrayList();
        /// <summary>
        /// combo adattablabol id-k tombje
        /// </summary>
        public string[] ComboIdk
        {
            get { return (string[])ComboId.ToArray(typeof(string)); }
        }
        /// <summary>
        /// combo adattabla kiegfileinfo listaja
        /// </summary>
        public ArrayList KiegFileinfo = new ArrayList();
        /// <summary>
        /// combo adattabla kiegfileinfo tombje
        /// </summary>
        public string[] KiegFileba
        {
            get { return (string[])KiegFileinfo.ToArray(typeof(string)); }
        }
        /// <summary>
        /// combo adattabla kiegeszito szovegek listaja (itemek)
        /// </summary>
        public ArrayList KiegInfo = new ArrayList();
        /// <summary>
        /// combo adattabla kiegeszito szovegek tombje (itemek)
        /// </summary>
        public string[] KiegSzovegbe
        {
            get { return (string[])KiegInfo.ToArray(typeof(string)); }
        }
        /// <summary>
        /// kiegeszito id-k listaja
        /// </summary>
        public ArrayList KiegId = new ArrayList();
        /// <summary>
        /// kiegeszito id-k tombje
        /// </summary>
        public string[] KiegIdk
        {
            get { return (string[])KiegId.ToArray(typeof(string)); }
        }
        /// <summary>
        /// 
        /// </summary>
        public ComboCollection ComboCollection;
        /// <summary>
        /// maximalis szoveghossz
        /// </summary>
        public int Maxhossz = 0;
        public int Minhossz = 0;
        /// <summary>
        /// maximalis hosszu szoveg
        /// </summary>
        public string Maxszov = "";
        public string  Minszov = "";
        /// <summary>
        /// fakuserinterface
        /// </summary>
        public FakUserInterface Fak;
        /// <summary>
        /// combo tabla osszefoglalo infoja
        /// </summary>
        public TablainfoTag Combotag = null;
        /// <summary>
        /// azon comboboxok listaja, melyek ezt a a comboinformaciot hasznaljak
        /// </summary>
        public ArrayList ComboArray = new ArrayList();
        /// <summary>
        /// Kezdoertek fileba
        /// </summary>
        public string DefFileba = "";
        /// <summary>
        /// kezdoertek szovegbe
        /// </summary>
        public string DefSzovegbe = "";
        /// <summary>
        /// objectum letrehozasa
        /// </summary>
        public Comboinfok()
        {
        }
        /// <summary>
        /// adott osszefoglalo info beillesztese
        /// </summary>
        /// <param name="tag">
        /// osszefoglalo info
        /// </param>
        public void Infoba(TablainfoTag tag)
        {
            if (Combotag == null)
            {
                Combotag = tag;
                Fak = tag.Fak;
            }
            else
            {
                ComboFileinfo.Clear();
                ComboInfo.Clear();
                ComboId.Clear();
                KiegFileinfo.Clear();
                KiegInfo.Clear();
                KiegId.Clear();
            }
            Tablainfo tabinfo = tag.Tablainfo;
            int cfilcol = -1;
            if (tabinfo.Tablanev == "BANKOK")
            {
                cfilcol = -1;
            }
            string cfilnev = tag.Azonositok.Combofileba;
//            int cfilcol = -1;
            DataTable datt;
            DataView dv = new DataView();
            DataTable datt1 = null;
            DataView dv1 = null;
            bool kellkieg = tabinfo.AktVerzioId > 1;
            if(kellkieg)
            {
                datt1 = new DataTable(tabinfo.Tablanev);
                dv1 = new DataView();
                string elozosel  = " where verzio_id = "+ (tabinfo.AktVerzioId - 1).ToString();
                Fak.Select(datt1, Fak.AktualCegconn, tabinfo.Tablanev, elozosel, "", false);
                dv1.Table = datt1;
            }
            string[] cnevek = tag.Azonositok.Comboszovegbe;
            ArrayList cnevekar = new ArrayList(cnevek);
            int[] cnevekcol;
            Cols[] cnevekegycol;
            cnevekcol = new int[cnevek.Length];
            cnevekegycol = new Cols[cnevek.Length];
            string cfil;
            string cszov;
            string cid;
            string cidnev = tabinfo.IdentityColumnName;
            if (tabinfo.TermSzarm != "SZ" && tabinfo.Adattabla.LastSel != "" && tabinfo.Adattabla.TableName != "LEIRO")
            {
                datt = new DataTable(tabinfo.Tablanev);
                Fak.Select(datt, tabinfo.Adattabla.Connection, tabinfo.Tablanev, "", "", false);
            }
            else
                datt = tabinfo.Adattabla;
            dv.Table = datt;
            if (tabinfo.Tablanev == "BASE")
            {
                dv.RowFilter = "SUBSTRING(AZON,1,1)='T' AND SZOVEG <> 'Természetes' AND TABLANEV=''";
                cidnev = "SORREND";
            }
            dv.Sort = tabinfo.ComboSort;
            if (cfilnev == "")
                cfilcol = -1;
            else
                cfilcol = datt.Columns.IndexOf(cfilnev);
            if (cfilcol != -1)
            {
                for (int i = 0; i < cnevek.Length; i++)
                {
                    bool comboe = false;
                    string egynev = cnevek[i];
                    string combonev = egynev;
                    if (egynev.Contains("_K"))
                    {
                        comboe = true;
                        egynev = egynev.Substring(0, egynev.Length - 2);
                    }
                    Cols tabinfocol = tabinfo.TablaColumns[egynev];
                    if (comboe)
                        tabinfocol = tabinfocol.Kiegcol;
                    cnevekcol[i] = datt.Columns.IndexOf(tabinfocol.ColumnName);
                    cnevekegycol[i] = tabinfocol;
                }
            }
            Minhossz = 0;
            if (dv.Count == 0)
            {
                for (int i = 0; i < cnevek.Length; i++)
                    Maxhossz += cnevekegycol[i].InputMaxLength;
            }
            else
            {
                for (int j = 0; j < dv.Count; j++)
                {
                    cszov = "";
                    DataRow dr = dv[j].Row;
                    cfil = dr[cfilcol].ToString().Trim();
                    cid = dr[cidnev].ToString();
                    for (int k = 0; k < cnevekcol.Length; k++)
                    {
                        if (cszov != "")
                            cszov += " ";
                        string st = dr[cnevekcol[k]].ToString().Trim();
                        if (cnevekegycol[k].DataType == typeof(DateTime))
                            st = Convert.ToDateTime(dr[cnevekcol[k]].ToString()).ToShortDateString();
                        string form = cnevekegycol[k].Format;
                        if (st.Length != 0)
                        {
                            for (int i = 0; i < st.Length; i++)
                            {
                                if (form.Length > i)
                                {
                                    string egykar = form.Substring(i, 1);
                                    if (egykar != "#")
                                        st = st.Insert(i, egykar);
                                }
                            }
                        }
                        cszov += st;
                    }
                    if (cszov != "")
                    {
                        ComboFileinfo.Add(cfil);
                        ComboInfo.Add(cszov);
                        if (cszov.Length > Maxhossz)
                        {
                            Maxhossz = cszov.Length;
                            Maxszov = cszov;
                        }
                        if (Minhossz == 0 || Minhossz > cszov.Length)
                        {
                            Minhossz = cszov.Length;
                            Minszov = cszov;
                        }
                        ComboId.Add(cid);
                    }
                }
                if (kellkieg)
                {
                    for (int j = 0; j < dv1.Count; j++)
                    {
                        cszov = "";
                        DataRow dr = dv1[j].Row;
                        cfil = dr[cfilcol].ToString().Trim();
                        cid = dr[cidnev].ToString();
                        for (int k = 0; k < cnevekcol.Length; k++)
                        {
                            if (cszov != "")
                                cszov += " ";
                            string st = dr[cnevekcol[k]].ToString().Trim();
                            if (cnevekegycol[k].DataType == typeof(DateTime))
                                st = Convert.ToDateTime(dr[cnevekcol[k]].ToString()).ToShortDateString();
                            string form = cnevekegycol[k].Format;
                            if (st.Length != 0)
                            {
                                for (int i = 0; i < st.Length; i++)
                                {
                                    if (form.Length > i)
                                    {
                                        string egykar = form.Substring(i, 1);
                                        if (egykar != "#")
                                            st = st.Insert(i, egykar);
                                    }
                                }
                            }
                            cszov += st;
                        }
                        if (cszov != "")
                        {
                            KiegFileinfo.Add(cfil);
                            KiegInfo.Add(cszov);
                            if (cszov.Length > Maxhossz)
                            {
                                Maxhossz = cszov.Length;
                                Maxszov = cszov;
                            }
                            if (Minhossz == 0 || Minhossz > cszov.Length)
                            {
                                Minhossz = cszov.Length;
                                Minszov = cszov;
                            }
                            KiegId.Add(cid);
                        }
                    }
                }
            }
            if (dv.Count != 0)
                SetDefertekek();
           dv.RowFilter = "";
        }
        /// <summary>
        /// Kezdoertekek beallitasa
        /// </summary>
        public void SetDefertekek()
        {
            Tablainfo tabinfo = Combotag.Tablainfo;
            if(Combotag.Azonositok.Leiroe)
                tabinfo=Combotag.LeiroTablainfo;
            Cols egycol = tabinfo.TablaColumns[Combotag.Azonositok.Combofileba];
            if (ComboFileinfo.Count == 0 || egycol.Lehetures)
            {
                DefFileba = "";
                if(egycol.Numeric(egycol.DataType))
                    DefFileba="0";
                DefSzovegbe = "";
                if(ComboFileinfo.Count == 0)
                    return;
            }
            else
            {
                DefFileba = ComboFileinfoAll()[0];
                DefSzovegbe = ComboSzovinfoAll()[0];
            }
            if (ComboCollection != null)
            {
                Tablainfo kiajanlinfo = ComboCollection.Kiajanlinfo;
                if (kiajanlinfo == null)
                    return;
                string azontip = Combotag.Azonositok.Azontip;
                string filter = kiajanlinfo.DataView.RowFilter;
                kiajanlinfo.DataView.RowFilter = "AZONTIP='" + azontip+"'";
                bool megvan = kiajanlinfo.DataView.Count != 0;
                if (megvan)
                {
                    string rsorszam = kiajanlinfo.DataView[0].Row["RSORSZAM"].ToString();
                    int i = ComboFileinfo.IndexOf(rsorszam);
                    if (i != -1)
                    {
                        DefFileba = ComboFileinfoAll()[i];
                        DefSzovegbe = ComboSzovinfoAll()[i];
                    }
                }
                kiajanlinfo.DataView.RowFilter = filter;
            }
        }
        
        /// <summary>
        /// adott tablainformacio keresese
        /// </summary>
        /// <param name="tabinfo">
        /// a tablainformacio
        /// </param>
        /// <returns>
        /// tablainformacio indexe vagy -1
        /// </returns>
        public int Find(Tablainfo tabinfo)
        {
            for (int i = 0; i < Tabinfok.Count; i++)
            {
                Tablainfo egyinfo = Tabinfok[i];
                if (tabinfo.Leiroe == egyinfo.Leiroe && tabinfo.Azontip == egyinfo.Azontip)
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// adott indexu info torlese
        /// </summary>
        /// <param name="i">
        /// az index
        /// </param>
        public void Deleteinfo(int i)
        {
            Tabinfok.Csakbase = true;
            Tabinfok.Remove(i);
            Tabinfok.Csakbase = false;
            ComboFileinfo.Remove(i);
            ComboInfo.Remove(i);
        }
        /// <summary>
        /// Az oszlopinformaciohoz hozzarendeli a comboinformaciot, a comboinformacioba eltarolja az oszlopinformacio tablainformaciojat
        /// </summary>
        /// <param name="egycol">
        /// oszlopinformacio objectuma
        /// </param>
        public void AttachToComboinfok(Cols egycol)
        {
            egycol.Combo_Info = this;
            Tablainfo tabinfo = egycol.Tablainfo;
            bool megvan = false;
            foreach (Tablainfo egyinfo in Tabinfok)
            {
                if (egyinfo.Leiroe == tabinfo.Leiroe && egyinfo.Azontip == tabinfo.Azontip)
                {
                    megvan = true;
                    break;
                }
            }
            if (!megvan)
            {
                Tabinfok.Csakbase = true;
                Tabinfok.Add(tabinfo);
                Tabinfok.Csakbase = false;
            }
        }
        /// <summary>
        /// oszlopinformacioban levo tartalomhoz tartozo szoveges informacio elhelyezese az oszlopinformacioban
        /// </summary>
        /// <param name="egycol">
        /// oszlopinformacio
        /// </param>
        public void SetComboAktszoveg(Cols egycol)
        {
            Cols egyinp = egycol;
            if (egyinp != null)
            {
                egyinp.ComboAktSzoveg = "";
                int i = ComboFileinfo.IndexOf(egyinp.Tartalom);
                if (i != -1)
                    egyinp.ComboAktSzoveg = ComboInfo[i].ToString();
                else
                {
                    i = KiegFileinfo.IndexOf(egyinp.Tartalom);
                    if (i != -1)
                        egyinp.ComboAktSzoveg = KiegInfo[i].ToString();
                    else if (!egyinp.Lehetures)
                    {
                        if (egyinp.DefaultValue.ToString() != "")
                            egyinp.Tartalom = egyinp.DefaultValue.ToString();
                        else
                            egyinp.Tartalom = DefFileba;
                        i = ComboFileinfo.IndexOf(egyinp.Tartalom);
                        if (i != -1)
                            egyinp.ComboAktSzoveg = ComboInfo[i].ToString();
                        else
                            egyinp.ComboAktSzoveg = DefSzovegbe;
                    }
                }
            }
        }
        /// <summary>
        /// Kivalasztott szoveges informaciohoz tartozo tarolando tartalom megallapitasa
        /// </summary>
        /// <param name="aktszoveg">
        /// szoveges info
        /// </param>
        /// <returns>
        /// tarolando tartalom string alakban vagy "0"
        /// </returns>
        public string GetComboAktfileba(string aktszoveg)
        {
            int i = ComboInfo.IndexOf(aktszoveg);
            if (i != -1)
                return ComboFileinfo[i].ToString();
            else
            {
                i = KiegInfo.IndexOf(aktszoveg);
                if (i == -1)
                {
                    try
                    {
                        Convert.ToInt64(ComboFileinfo[0].ToString());
                        return "0";
                    }
                    catch
                    {
                        return "";
                    }
                }
                else
                    return KiegFileinfo[i].ToString();
            }
        }
        /// <summary>
        /// ??? 
        /// </summary>
        /// <param name="combo"></param>
        public void RemoveKiegComboItems(ComboBox combo)
        {
            if (KiegInfo.Count != 0)
            {
                string comboszov = combo.Items[0].ToString().Trim();
                int i = KiegInfo.IndexOf(comboszov);
                if (i != -1)
                    combo.Items.Remove(comboszov);
            }
        }
        /// <summary>
        /// visszaadja az osszes fileban tarolando comboerteket, elol a kiegeszitokkel
        /// </summary>
        /// <returns>
        /// string tomb
        /// </returns>
        public string[] ComboFileinfoAll()
        {
            ArrayList all = new ArrayList();
            if (KiegFileinfo.Count != 0)
                all.AddRange(KiegFileinfo);
            all.AddRange(ComboFileinfo);
            return (string[])all.ToArray(typeof(string));
        }
        /// <summary>
        /// visszaadja az osszes szoveges comboerteket, elol a kiegeszitok
        /// </summary>
        /// <returns>
        /// string tomb
        /// </returns>
        public string[] ComboSzovinfoAll()
        {
            ArrayList all = new ArrayList();
            if (KiegInfo.Count != 0)
                all.AddRange(KiegInfo);
            all.AddRange(ComboInfo);
            return (string[])all.ToArray(typeof(string));
        }
        /// <summary>
        /// ComboItemek a ComboBox-ba, a kezdo itemindex beallitasa az aktualis rogzitett vagy default tartalom alapjan
        /// </summary>
        /// <param name="combo">
        /// ComboBox
        /// </param>
        /// <param name="egycol">
        /// oszlopinformacio
        /// </param>
        /// <param name="tartal">
        /// rogzitett vagy default tartalom
        /// </param>
        public void SetComboItems(ComboBox combo, Cols egycol, string tartal)
        {
            if (combo != null && ComboArray.IndexOf(combo) == -1)
                ComboArray.Add(combo);
            Cols egyinp = egycol.Kiegcol;
            if (egyinp == null)
                egyinp = egycol;
            if (egyinp != null && combo != null)
            {
                combo.Items.Clear();
                combo.Items.AddRange(Szovegbe);
            }
            int i = 0;
            if (ComboInfo.Count == 0)
                i = -1;
            if (tartal != egycol.DefaultValue.ToString())
            {
                i = ComboInfo.IndexOf(tartal);
                if (i == -1)
                    i = ComboFileinfo.IndexOf(tartal);
                if (i == -1 && tartal == egycol.OrigTartalom && ComboInfo.Count != 0)
                    i = 0;
            }
            else if (egycol.Lehetures)
                i = -1;
            if (i != -1)
            {
                egyinp.DefaultValue = ComboInfo[i].ToString();
                egyinp.ComboAktSzoveg = ComboInfo[i].ToString();
                egyinp.ComboAktFileba = ComboFileinfo[i].ToString();

                if (combo != null)
                {
                    combo.Text = egyinp.ComboAktSzoveg;
                    combo.SelectedIndex = combo.Items.IndexOf(combo.Text);
                }
            }
            else
            {
                egyinp.DefaultValue = egycol.DefaultValue;
                egyinp.ComboAktSzoveg = "";
                egyinp.ComboAktFileba = egycol.DefaultValue.ToString();
                if (combo != null)
                    combo.Text = "";
            }
        }
        /// <summary>
        /// oszlopinfo combinformaciok megallapitasa index alapjan
        /// </summary>
        /// <param name="selind">
        /// index
        /// </param>
        /// <param name="egycol">
        /// oszlopinformacio
        /// </param>
        public void SetSelectedComboInfo(int selind, Cols egycol)
        {
            Cols egyinp = egycol;
            egyinp.ComboAktSzoveg = ComboInfo[selind].ToString();
            egyinp.ComboAktFileba = ComboFileinfo[selind].ToString();
            egyinp.Tartalom = egyinp.ComboAktFileba;
            egycol.Tartalom = egyinp.ComboAktFileba;
        }
        /// <summary>
        /// fileban rogzitendo tartalom alapjan az oszlopinformacioba kitolti az aktualis file ill. szovegtartalmat
        /// </summary>
        /// <param name="egyinfo">
        /// oszlopinformacio
        /// </param>
        /// <param name="tartal">
        /// fileban rogzitendo tartalom
        /// </param>
        /// <returns>
        /// a megtalalt szoveg vagy / ha LehetUres "", egyebkent az elso
        /// </returns>
        public string Combotolt(Cols egyinfo, string tartal)
        {
            if (ComboFileinfo.Count == 0)
            {
                egyinfo.ComboAktFileba = "";
                egyinfo.ComboAktSzoveg = "";
                return egyinfo.ComboAktSzoveg;
            }
            int i = ComboFileinfo.IndexOf(tartal);
            if (i != -1)
            {
                egyinfo.ComboAktFileba = ComboFileinfo[i].ToString();
                egyinfo.ComboAktSzoveg = ComboInfo[i].ToString();
                return egyinfo.ComboAktSzoveg;
            }
            i = KiegFileinfo.IndexOf(tartal);
            if (i != -1)
            {
                egyinfo.ComboAktFileba = KiegFileinfo[i].ToString();
                egyinfo.ComboAktSzoveg = KiegInfo[i].ToString();
                return egyinfo.ComboAktSzoveg;
            }
            if (egyinfo.Lehetures)
            {
                egyinfo.ComboAktFileba = "";
                egyinfo.ComboAktSzoveg = "";
            }
            else if (DefSzovegbe != "")
            {
                egyinfo.ComboAktFileba = DefFileba;//.ToString();
                egyinfo.ComboAktSzoveg = DefSzovegbe;//. ComboInfo[0].ToString();
            }
            else
            {
                egyinfo.ComboAktFileba = ComboFileinfo[0].ToString();
                egyinfo.ComboAktSzoveg = ComboInfo[0].ToString();
            }


            return egyinfo.ComboAktSzoveg;
        }
    }
}