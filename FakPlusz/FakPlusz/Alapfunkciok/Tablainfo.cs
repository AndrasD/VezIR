using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;
using System.Data;
using System.Threading;
using FakPlusz;
using FakPlusz.SzerkesztettListak;
using FakPlusz.Alapcontrolok;
namespace FakPlusz.Alapfunkciok
{
    /// <summary>
    /// Tablaszintu informaciok gyujtemenye
    /// </summary>
    public class TablainfoCollection : ArrayList
    {
        private ArrayList tablanevek = new ArrayList();
        private ArrayList szintek = new ArrayList();
        private ArrayList azonok = new ArrayList();
        private ArrayList azontipek = new ArrayList();
        private ArrayList azontip1ek = new ArrayList();
        private ArrayList azontip2ok = new ArrayList();
        private ArrayList termszarmok = new ArrayList();
        private ArrayList identitynevek = new ArrayList();
        private bool csakbase = false;
        /// <summary>
        /// ha csak az ArrayList Add funkciojat akarjuk
        /// </summary>
        public bool Csakbase
        {
            get { return csakbase; }
            set { csakbase = value; }
        }
        /// <summary>
        /// objectum letrehozasa
        /// </summary>
        public TablainfoCollection()
        {
        }
        /// <summary>
        /// kereses azonosito alapjan
        /// </summary>
        /// <param name="azon">
        /// kert azonosito
        /// </param>
        /// <returns>
        /// a kert info indexe
        /// </returns>
        public int IndexOf(string azon)
        {
            return azonok.IndexOf(azon);
        }
        /// <summary>
        /// kereses szint es tablanev alapjan
        /// </summary>
        /// <param name="szint"></param>
        /// <param name="tablanev"></param>
        /// <returns></returns>
        public int IndexOf(string szint, string tablanev)
        {
            for (int i = 0; i < tablanevek.Count; i++)
            {
                if (tablanevek[i].ToString() == tablanev && szintek[i].ToString() == szint)
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// kereses index alapjan
        /// </summary>
        /// <param name="index">
        /// kivant index
        /// </param>
        /// <returns>
        /// Tablainformacio vagy null
        /// </returns>
        public new Tablainfo this[int index]
        {
            get 
            {
                if (index < 0 || index > this.Count - 1)
                    return null;
                else
                    return (Tablainfo)base[index]; 
            }
            set { base[index] = value;}
        }
        /// <summary>
        /// kereses tablanev alapjan
        /// </summary>
        /// <param name="name">
        /// kivant tablanev
        /// </param>
        /// <returns>
        /// Tablainformacio vagy null
        /// </returns>
        public Tablainfo this[string name]
        {
            get
            {
                int i = tablanevek.IndexOf(name);
                if (i == -1)
                    return null;
                else
                    return (Tablainfo)this[i];
            }
        }
        /// <summary>
        /// Uj tablainformacio hozzaadas
        /// </summary>
        /// <param name="value">
        /// Uj tablainformacio
        /// </param>
        /// <returns>
        /// Uj informacio indexe
        /// </returns>
        public override int Add(object value)
        {
            Tablainfo egyinfo = (Tablainfo)value;
            if (value == null)
                return -1;
            string azontip = egyinfo.Azontip;
            if (azontipek.IndexOf(azontip) == -1)
            {
                if (csakbase)
                    return base.Add(value);
                tablanevek.Add(egyinfo.Tablanev);
                szintek.Add(egyinfo.Szint);
                azonok.Add(egyinfo.Azon);
                azontipek.Add(egyinfo.Azontip);
                azontip1ek.Add(egyinfo.Azontip1);
                azontip2ok.Add(egyinfo.Azontip2);
                termszarmok.Add(egyinfo.TermSzarm);
                identitynevek.Add(egyinfo.IdentityColumnName);
                return base.Add(value);
            }
            else if (egyinfo.Tablanev == "LEIRO" && egyinfo.Azon.Substring(0, 1) == "T")
            {
                string szrmazontip = "SZRM" + egyinfo.Azon.Substring(2, 1) + egyinfo.Azontip.Substring(4);
                tablanevek.Add(egyinfo.Tablanev);
                szintek.Add(egyinfo.Szint);
                azonok.Add(egyinfo.Azon);
                azontipek.Add(szrmazontip);
                azontip1ek.Add(egyinfo.Azontip1);
                azontip2ok.Add(egyinfo.Azontip2);
                termszarmok.Add(egyinfo.TermSzarm);
                identitynevek.Add(egyinfo.IdentityColumnName);
                return base.Add(value);

            }
            return -1;
        }
        /// <summary>
        /// Adott tablainformacio torlese
        /// </summary>
        /// <param name="obj">
        /// A torolni kivant tablainformacio
        /// </param>
        public override void Remove(object obj)
        {
            Tablainfo info = (Tablainfo)obj;
            int i=this.IndexOf(info);
            if (i != -1)
            {
                base.RemoveAt(i);
                tablanevek.RemoveAt(i);
                szintek.RemoveAt(i);
                azonok.RemoveAt(i);
                azontipek.RemoveAt(i);
                azontip1ek.RemoveAt(i);
                azontip2ok.RemoveAt(i);
                termszarmok.RemoveAt(i);
                identitynevek.RemoveAt(i);
            }
        }
        /// <summary>
        /// kerese tablainformacio alapjan
        /// </summary>
        /// <param name="tabinfo">
        /// a kert tablainfo
        /// </param>
        /// <returns>
        /// a kert info indexe
        /// </returns>
        public int IndexOf(Tablainfo tabinfo)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i] == tabinfo)
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// FakUserInterfacenel leirva
        /// </summary>
        /// <param name="szint"></param>
        /// <returns></returns>
        public TablainfoCollection GetBySzint(string szint)
        {
            csakbase = true;
            TablainfoCollection ar = new TablainfoCollection();
            for (int i = 0; i < this.Count; i++)
            {
                if (szint.Contains(szintek[i].ToString()))
                    ar.Add(this[i]);
            }
            csakbase = false;
            return ar;
        }
        /// <summary>
        /// FakUserInterfacenel leirva
        /// </summary>
        /// <param name="tablanev"></param>
        /// <returns></returns>
        public TablainfoCollection GetByTablanev(string tablanev)
        {
            csakbase = true;
            TablainfoCollection ar = new TablainfoCollection();
            for (int i = 0; i < this.Count; i++)
            {
                if (tablanev == tablanevek[i].ToString())
                    ar.Add(this[i]);
            }
            csakbase = false;
            return ar;
        }
        /// <summary>
        /// FakUserInterfacenel leirva
        /// </summary>
        /// <param name="szint"></param>
        /// <param name="tablanev"></param>
        /// <returns></returns>
        public Tablainfo GetBySzintPluszTablanev(string szint, string tablanev)
        {
            for (int i = 0; i < tablanevek.Count; i++)
            {
                if (szintek[i].ToString() == szint && tablanevek[i].ToString() == tablanev)
                    return this[i];
            }
            return null;
        }
        /// <summary>
        /// FakUserInterfacenel leirva
        /// </summary>
        /// <param name="azontip"></param>
        /// <returns></returns>
        public Tablainfo GetByAzontip(string azontip)
        {
            int i = azontipek.IndexOf(azontip);
            if (i == -1)
                return null;
            return this[i];
        }
        /// <summary>
        /// FakUserInterfacenel leirva
        /// </summary>
        /// <param name="azontip1"></param>
        /// <returns></returns>
        public Tablainfo[] GetByAzontip1(string azontip1)
        {
            if (azontip1 == "")
                return null;
            ArrayList ar = new ArrayList();
            for (int i = 0; i < azontip1ek.Count; i++)
            {
                if (azontip1ek[i].ToString() == azontip1)
                    ar.Add(i);
            }
            if (ar.Count == 0)
                return null;
            int[] indexek = (int[])ar.ToArray(typeof(int));
            Tablainfo[] infok = new Tablainfo[indexek.Length];
            for (int i = 0; i < infok.Length; i++)
                infok[i] = this[indexek[i]];
            return infok;
        }
        /// <summary>
        /// FakUserInterfacenel leirva
        /// </summary>
        /// <param name="azontip2"></param>
        /// <returns></returns>
        public Tablainfo[] GetByAzontip2(string azontip2)
        {
            if (azontip2 == "")
                return null;
            ArrayList ar = new ArrayList();
            for (int i = 0; i < azontip2ok.Count; i++)
            {
                if (azontip2ok[i].ToString() == azontip2)
                    ar.Add(i);
            }
            if (ar.Count == 0)
                return null;
            int[] indexek = (int[])ar.ToArray(typeof(int));
            Tablainfo[] infok = new Tablainfo[indexek.Length];
            for (int i = 0; i < infok.Length; i++)
                infok[i] = this[indexek[i]];
            return infok;
        }
        /// <summary>
        /// kereses identity nev alapjan
        /// </summary>
        /// <param name="ident"></param>
        /// <returns></returns>
        public Tablainfo GetByIdentityName(string ident)
        {
            int i = identitynevek.IndexOf(ident);
            if (i == -1)
                return null;
            else
                return this[i];
        }
        /// <summary>
        /// FakUserInterfacenel leirva
        /// </summary>
        /// <param name="termszarm"></param>
        /// <returns></returns>
        public Tablainfo[] GetByTermszarm(string termszarm)
        {
            return GetByTermszarmPluszSzint(termszarm, "");
        }
        /// <summary>
        /// FakUserInterfacenel leirva
        /// </summary>
        /// <param name="termszarm"></param>
        /// <param name="szint"></param>
        /// <returns></returns>
        public Tablainfo[] GetByTermszarmPluszSzint(string termszarm, string szint)
        {
            csakbase = true;
            ArrayList ar = new ArrayList();
            for (int i = 0; i < termszarmok.Count; i++)
            {
                if (termszarmok[i].ToString() == termszarm && (szint == "" || szint.Contains(szintek[i].ToString())))
                    ar.Add(this[i]);
            }
            csakbase = false;
            if (ar.Count == 0)
                return null;
            return (Tablainfo[])ar.ToArray(typeof(Tablainfo));
        }
        /// <summary>
        /// FakUserInterfacenel leirva
        /// </summary>
        /// <returns></returns>
        public TablainfoCollection GetCegPluszCegalattiTermTablaInfok() 
        {
            csakbase = true;
            Tablainfo egyinfo;
            ArrayList ar = new ArrayList();
            for (int i = 0; i < termszarmok.Count; i++)
            {
                if (termszarmok[i].ToString().Trim() == "T" && tablanevek[i].ToString() != "TARTAL")
                {
                    egyinfo = this[i];
                    if (("C" + egyinfo.Fak.Szintstring).Contains(szintek[i].ToString()) && egyinfo.TermParentTabinfo == null)
                        ar = AddChildren(ar,egyinfo);
                }
            }
            TablainfoCollection tc = new TablainfoCollection();
            for (int i = 0; i < ar.Count; i++)
            {
                if (tc.IndexOf((Tablainfo)ar[i]) == -1)
                    tc.Add(ar[i]);
            }
            csakbase = false;
            return tc;
        }
        private ArrayList AddChildren(ArrayList ar,Tablainfo info)
        {
            ar.Add(info);
            foreach(Tablainfo tabinfo in info.TermChildTabinfo)
                ar = AddChildren(ar,tabinfo);
            return ar;
        }
        /// <summary>
        /// Azon osszetett(csoport,osszefugges) tablainfok kerese, melyek valamelyik eleme azonos a kerttel es verzioja egyezik a kerttel
        /// </summary>
        /// <param name="azontip">
        /// vizsgalando teljes azonositoja
        /// </param>
        /// <param name="aktverzioid">
        /// vizsgalando verzio
        /// </param>
        /// <returns>
        /// megfelelo infok tombje vagy null
        /// </returns>
        public Tablainfo[] GetByAzontipPluszVerzioid(string azontip, int aktverzioid)
        {
            csakbase = true;
            ArrayList ar = new ArrayList();
            for (int i = 0; i < azontipek.Count; i++)
            {
                Tablainfo info = this[i];
                if (info.KellVerzio && info.LastVersionId == aktverzioid && (info.Azontip1 == azontip || info.Azontip2 == azontip))
                    ar.Add(this[i]);
            }
            csakbase = false;
            if (ar.Count == 0)
                return null;
            return (Tablainfo[])ar.ToArray(typeof(Tablainfo));
        }
        public TablainfoCollection GetBySzintPluszVerzioid(string szint, int aktverzioid)
        {
            csakbase = true;
            TablainfoCollection coll = new TablainfoCollection();
            for (int i = 0; i < azontipek.Count; i++)
            {
                Tablainfo info = this[i];
                if (info.KellVerzio && info.LastVersionId == aktverzioid && info.Szint == szint)
                    coll.Add(info);
            }
            return coll;
        }
    }
    /// <summary>
    /// Tablaszintu informaciok osztalya
    /// </summary>
    public class Tablainfo
    {
        private FakUserInterface _fak;
        /// <summary>
        /// fakuserinterface
        /// </summary>
        public FakUserInterface Fak
        {
            get { return _fak; }
        }
        private Azonositok _azonositok = null;
        /// <summary>
        /// tablaazonositok objectuma
        /// </summary>
        public Azonositok Azonositok
        {
            get { return _azonositok; }
        }
        private Base.HozferJogosultsag _hozferjog;
        private Base.HozferJogosultsag _akthozferjog;
        /// <summary>
        /// Aktualis hozzaferesi jog
        /// </summary>
        public Base.HozferJogosultsag HozferJog
        {
            get { return _akthozferjog; }
            set { _akthozferjog = value; }
        }
        private string _aktusername="";
        private Base.KezSzint _aktkezeloszint;
        private string _ownernev = "";
        /// <summary>
        /// Owner column neve
        /// </summary>
        public string Ownernev
        {
            get { return _ownernev; }
        }
        private Base _hivo = null;
        /// <summary>
        /// hivo UserControl
        /// </summary>
        public Base Hivo
        {
            get { return _hivo; }
            set { _hivo = value; }
        }
        private ArrayList _torolttagok = new ArrayList();
        /// <summary>
        /// Ha a tartalomjegyzekbol torlunk, a torolt infok osszefogo informacioja
        /// </summary>
        public ArrayList ToroltTagok
        {
            get { return _torolttagok; }
            set { _torolttagok = value; }
        }
        private Tablainfo _leirotablainfo = null;
        /// <summary>
        /// A tabla leirotablainformacioja
        /// </summary>
        public Tablainfo LeiroTablainfo
        {
            get { return _leirotablainfo; }
            set { _leirotablainfo = value; }
        }
        private Tablainfo _naplotablainfo = null;
        /// <summary>
        /// A tablainformacio naplotablainformacioja, ahova a valtoztatasok naplozasa kerul
        /// </summary>
        public Tablainfo NaploTablainfo
        {
            get { return _naplotablainfo; }
            set
            {
                _naplotablainfo = value;
            }
        }
        private DataTable _naplotabla;
        /// <summary>
        /// a tablainformacio naplotablaja, ide kerul a valtozasok naplozasa
        /// </summary>
        public DataTable NaploTabla
        {
            get { return _naplotabla; }
            set { _naplotabla = value; }
        }
        private ArrayList _kiegdatacolumns = new ArrayList();
        /// <summary>
        /// az adattablat kiegeszito DataColumn-ok
        /// </summary>
        public ArrayList KiegDataColumns
        {
            get { return _kiegdatacolumns; }
        }
        private int _nextparent = 0;
        /// <summary>
        /// ??? nem tudom, hogy kell-e
        /// </summary>
        public int NextParent
        {
            get { return _nextparent; }
        }
        private ColCollection _tablacolumns = new ColCollection();
        /// <summary>
        /// a tabla mezoleirasai
        /// </summary>
        public ColCollection TablaColumns
        {
            get { return _tablacolumns; }
        }
        private ColCollection _kiegcolumns = new ColCollection();
        /// <summary>
        /// a mezok leirasat kiegeszito mezoleirasok
        /// </summary>
        public ColCollection KiegColumns
        {
            get { return _kiegcolumns; }
        }
        private ColCollection _combocolumns = new ColCollection();
        /// <summary>
        /// a mezoleirasokbol a Combo mezoleirasok
        /// </summary>
        public ColCollection ComboColumns
        {
            get { return _combocolumns; }
        }
        private ColCollection _inputcolumns = new ColCollection();
        /// <summary>
        /// Azon mezok leirasa, melyekbe adatbevitel lehetseges
        /// </summary>
        public ColCollection InputColumns
        {
            get { return _inputcolumns; }
        }
        private AdatTabla _inputtable = null;
        /// <summary>
        /// A TERVEZO hasznalja
        /// </summary>
        public AdatTabla Inputtabla
        {
            get { return _inputtable; }
        }
        private Osszefinfo _osszefinfo = null;
        /// <summary>
        /// Osszefugges jellegu tablainfo-k osszefugges objectuma (Osszefugges, Csoport, Szukitett kodtabla) vagy null
        /// </summary>
        public Osszefinfo Osszefinfo
        {
            get { return _osszefinfo; }
        }
        private TablainfoTag _parenttag = null;
        /// <summary>
        /// A tablainformacio Parent osszefogo informacioja , vagy null
        /// </summary>
        public TablainfoTag ParentTag
        {
            get { return _parenttag; }
        }
        private TablainfoTag _tablatag = null;
        /// <summary>
        /// A tablainformacio sajat osszefogo informacioja
        /// </summary>
        public TablainfoTag TablaTag
        {
            get { return _tablatag; }
        }
        private TablainfoTag _fordtag = null;
        /// <summary>
        /// A tablainfo forditott osszefogo informacioja Csoport vagy Osszefugges eseten, egyebkent null
        /// </summary>
        public TablainfoTag FordTablaTag
        {
            get { return _fordtag; }
        }
        private AdatTabla _adattabla;
        /// <summary>
        /// Felturbozott DataTable
        /// </summary>
        public AdatTabla Adattabla
        {
            get { return _adattabla; }
            set { _adattabla = value; }
        }
        private bool _readonly = false;
        public bool ReadOnly
        {
            get { return _readonly; }
            set { _readonly = value; }
        }

        private bool _leiroe = false;
        /// <summary>
        /// Leirotablainformacio-e
        /// </summary>
        public bool Leiroe
        {
            get { return _leiroe; }
        }
        private ArrayList _verzioterkeparr = new ArrayList();
        /// <summary>
        /// Verzioinformaciok
        /// </summary>
        public ArrayList VerzioTerkepArray
        {
            get { return _verzioterkeparr; }
        }
        private int _aktverzioid = 1;
        /// <summary>
        /// Aktualis (adattablaban allomasozo verzio)
        /// </summary>
        public int AktVerzioId
        {
            get { return _aktverzioid; }
            set { _aktverzioid = value; }
        }
        private MezoControlCollection _egycontinfok = new MezoControlCollection();
        /// <summary>
        /// Az erre a tablara hivatkozott UserControlok mezocontrolinformacioi
        /// </summary>
        public MezoControlCollection ControlInfok
        {
            get { return _egycontinfok; }
            set { _egycontinfok = value; }
        }
        private bool _kellbarmilyenvaltozas = false;
        /// <summary>
        /// kell-e barmilyen valtozas
        /// </summary>
        public bool KellBarmilyenValtozas
        {
            get { return _kellbarmilyenvaltozas; }
            set { _kellbarmilyenvaltozas = value; }
        }
        private DateTime _lastupdate = DateTime.MinValue;
        /// <summary>
        /// a tabla utolso modositasanak datuma
        /// </summary>
        public DateTime LastUpdate
        {
            get { return _lastupdate; }
            set
            {
                _lastupdate = value;
                int conind = _fak.ConnectionStringArray.IndexOf(this.Adattabla.Connection);
                if (_fak.LastUpdateDateTime[conind].CompareTo(value) < 0)
                    _fak.LastUpdateDateTime[conind] = value;
            }
        }
    
        private MezoControlInfo _aktcontinfo = null;
        /// <summary>
        /// Az erre a tablara hivatkozott UserControlok kozul az aktualis UserControl mezocontrolinformacioi
        /// </summary>
        public MezoControlInfo AktualControlInfo
        {
            get { return _aktcontinfo; }
            set { _aktcontinfo = value; }
        }
        private UserControlCollection _usercontrolok = new UserControlCollection();
        /// <summary>
        /// Az erre a tablara hivatkozott UserControlok UserControlinformacioi
        /// </summary>
        public UserControlCollection UserControlok
        {
            get { return _usercontrolok; }
        }
        public Cols AlkalmazasIdColumn
        {
            get
            {
                Cols alkidcol = _tablacolumns["ALKALMAZAS_ID"];
                if (alkidcol == null || !alkidcol.ReadOnly )
                    return null;
                else
                    return alkidcol;
            }
        }
        private Cols _sorrendcolumn = null;
        /// <summary>
        /// SORREND vagy SORREND_K nevu mezo mezoinformacioi
        /// </summary>
        public Cols SorrendColumn
        {
            get
            {
                if (_sorrendcolumn == null)
                    return _kiegsorrendcolumn;
                else
                    return _sorrendcolumn;
            }
            set { _sorrendcolumn = value; }
        }
        private string _sorrendmezo = "";
        /// <summary>
        /// TERVEZOVEL Sorrendmezokent megadott mezo neve
        /// </summary>
        public string Sorrendmezo
        {
            get { return _sorrendmezo; }
        }
        private Cols _kiegsorrendcolumn = null;
        private bool _beszurhat = false;
        /// <summary>
        /// Beszurhat sort?
        /// </summary>
        public bool Beszurhat
        {
            get
            { 
                return _beszurhat; 
            }
        }
        private bool _torolhet = false;
        /// <summary>
        /// Torolhet sort?
        /// </summary>
        public bool Torolhet
        {
            get { return _torolhet; }
        }
        private bool _modosithat = false;
        /// <summary>
        /// Modosithat sort?
        /// </summary>
        public bool Modosithat
        {
            get { return _modosithat; }
            set { _modosithat = value; }
        }
        private long _aktidentity = -1;
        /// <summary>
        /// Az aktualis DataView sor identity-je
        /// </summary>
        public long AktIdentity
        {
            get { return _aktidentity; }
            set { _aktidentity = value; }
        }
        private int _identitycolindex = -1;
        /// <summary>
        /// A tabla Identity mezo-jenek indexe
        /// </summary>
        public int IdentityColumnIndex
        {
            get { return _identitycolindex; }
            set { _identitycolindex = value; }
        }
        /// <summary>
        /// KOD nevu mezo indexe
        /// </summary>
        public int Kodcol
        {
            get { return _adattabla.Columns.IndexOf("KOD"); }
        }
        private int _sorszam1col = -1;
        /// <summary>
        /// SORSZAM1 nevu mezo indexe (osszefugg jellegu)
        /// </summary>
        public int Sorszam1col
        {
            get { return _sorszam1col; }
        }
        private int _sorszam2col = -1;
        /// <summary>
        /// SORSZAM2 nevu mezo indexe(osszefugg jellegu)
        /// </summary>
        public int Sorszam2col
        {
            get { return _sorszam2col; }
        }
        private int _verzioidcol = -1;
        /// <summary>
        /// VERZIO_ID nevu mezo indexe
        /// </summary>
        public int VerzioIdcol
        {
            get { return _verzioidcol; }
        }
        private int _verzioid1col = -1;
        /// <summary>
        /// VERZIO_ID1 nevu mezo indexe (osszefugg jellegu)
        /// </summary>
        public int VerzioId1col
        {
            get { return _verzioid1col; }
        }
        private int _verzioid2col = -1;
        /// <summary>
        /// VERZIO_ID2 nevu mezo indexe (osszefugg jellegu)
        /// </summary>
        public int VerzioId2col
        {
            get { return _verzioid2col; }
        }
        private string _identity = "";
        /// <summary>
        /// A tabla Identity mezo-jenek neve
        /// </summary>
        public string IdentityColumnName
        {
            get { return _identity; }
            set { _identity = value; }
        }
        private int _datumtolcol = -1;
        /// <summary>
        /// DATUMTOL nevu mezo indexe
        /// </summary>
        public int DatumtolColumnIndex
        {
            get { return _datumtolcol; }
        }
        private bool _datumtollehetures = true;
        /// <summary>
        /// DATUMTOL lehet ures?
        /// </summary>
        public bool DatumtolLehetUres
        {
            get { return _datumtollehetures; }
        }
        private int _datumigcol = -1;
        /// <summary>
        /// DATUMIG nevu mezo indexe
        /// </summary>
        public int DatumigColumnIndex
        {
            get { return _datumigcol; }
        }
        private bool _datumiglehetures = true;
        /// <summary>
        /// DATUMIG lehet ures?
        /// </summary>
        public bool DatumigLehetUres
        {
            get { return _datumiglehetures; }
        }
        private string _mindattime = "";
        /// <summary>
        /// Legkisebb adhato -tol datum ShortDateString alakban
        /// </summary>
        public string MinDateTime
        {
            get { return _mindattime; }
            set { _mindattime = value; }
        }
        private bool _newversion = false;
        /// <summary>
        /// true, ha uj verziot allitottunk elo
        /// </summary>
        public bool NewVersionCreated
        {
            get { return _newversion; }
            set { _newversion = value; }
        }
        private bool _modositott = false;
        /// <summary>
        /// true, ha modositottunk es ok-ztunk
        /// </summary>
        public bool Modositott
        {
            get { return _modositott; }
            set
            {
                MezoControlInfo egyinfo = _aktcontinfo;
                if (_modositott != value)
                {
                    if (_modositott && !value)
                    {
                    }
                    _modositott = value;
                    if (egyinfo != null && egyinfo.Hivo != null)
                    {
                        if (value == false)
                            egyinfo.ClearChanged();
                        else if(!Fak.EventTilt)
                            egyinfo.SetChanged();
                    }
                }
            }
        }
        private bool _rogzitett = false;
        /// <summary>
        /// Rogzitett-e a tabla tartalma
        /// </summary>
        public bool Rogzitett
        {
            get { return _rogzitett; }
            set { _rogzitett = value; }
        }
        private bool _changed = false;
        /// <summary>
        /// true, ha valtoztattunk 
        /// </summary>
        public bool Changed
        {
            get { return _changed; }
            set
            {
                MezoControlInfo egyinfo = _aktcontinfo;
                if (_changed != value || !_changed)
                {
                    _changed = value;
                    if (egyinfo != null && egyinfo.Hivo != null)
                    {
                        if (value == false)
                            egyinfo.ClearChanged();
                        else
                            egyinfo.SetChanged();
                    }
                }
            }
        }
        private bool _modositasihiba = false;
        /// <summary>
        /// true, ha a valtoztatasban hiba volt
        /// </summary>
        public bool ModositasiHiba
        {
            get { return _modositasihiba; }
            set
            {
                MezoControlInfo egyinfo = _aktcontinfo;
                if (egyinfo != null && egyinfo.Hivo != null)
                {
                    //if (_modositasihiba != value || value)
                    //{
                        _modositasihiba = value;
                        if (!value)
                            egyinfo.ClearError();
                        else
                            egyinfo.SetError();
                    //}
                }
                else
                    _modositasihiba = value;
            }
        }
        private bool _ures = true;
        /// <summary>
        /// true, ha ures a tabla
        /// </summary>
        public bool Ures
        {
            get { return _ures; }
            set { _ures = value; }
        }
        private DateTime[] _aktintervallum = null;
        /// <summary>
        /// Aktualis datumtartomany
        /// </summary>
        public DateTime[] AktIntervallum
        {
            get { return _aktintervallum; }
        }
        private DateTime _mindatumkezd = DateTime.MinValue;
        private DateTime _mindatumveg = DateTime.MinValue;
        private DateTime _maxdatumkezd = DateTime.MinValue;
        private DateTime _maxdatumveg = DateTime.MinValue;
        private Tablainfo _firsttermparent = null;
        private ArrayList _specdatumnevekarray = new ArrayList();
        /// <summary>
        /// Definialt specialis datumnevek listaja
        /// </summary>
        public ArrayList SpecDatumNevekArray
        {
            get { return _specdatumnevekarray; }
        }
        private string[] _specdatumnevek = null;
        /// <summary>
        /// A Specialis datumnevek kodtablaban definialt sorok SZOVEG-ei
        /// </summary>
        public string[] SpecDatumNevek
        {
            get {return _specdatumnevek;}
        }
        /// <summary>
        /// Ha vannak specialis datumnevek, a listazas tolti ki
        /// </summary>
        public bool[] SpecDatumNevSzerepel = null;
        /// <summary>
        /// A termeszetes, fastrukturaba rendezett tablaknal a tabla legmagasabb szulotabla info-ja
        /// </summary>
        public Tablainfo FirstTermParentTabinfo
        {
            get { return _firsttermparent; }
            set { _firsttermparent = value; }
        }
        private TablainfoCollection _gyokerek = new TablainfoCollection();
        /// <summary>
        /// gyokertablainfok 
        /// </summary>
        public TablainfoCollection Gyokerek
        {
            get { return _gyokerek; }
        }
        private TablainfoCollection _termparentchain = new TablainfoCollection();
        /// <summary>
        /// A termeszetes, fastrukturaba rendezett tablaknal a tabla osszes szulotablainfo-ja sorban
        /// </summary>
        public TablainfoCollection TermParentTabinfoChain
        {
            get { return _termparentchain; }
        }
        private Tablainfo _termparentinfo = null;
        /// <summary>
        /// termeszetes tablainfo parent tablainfo-ja, vagy null
        /// </summary>
        public Tablainfo TermParentTabinfo
        {
            get { return _termparentinfo; }
            set { _termparentinfo = value; }
        }
        private TablainfoCollection _termchildinfo = new TablainfoCollection();
        /// <summary>
        /// termeszetes tablainfo child infoi 
        /// </summary>
        public TablainfoCollection TermChildTabinfo
        {
            get { return _termchildinfo; }
            set { _termchildinfo = value; }
        }
        private TablainfoCollection _combohasznalok = new TablainfoCollection();
        public TablainfoCollection ComboHasznalok
        {
            get { return _combohasznalok; }
            set { _combohasznalok = value; }
        }
        private string _selectstring = "";
        /// <summary>
        /// SELECT-hez "WHERE ...." vagy ures
        /// </summary>
        public string SelectString
        {
            get { return _selectstring; }
            set { _selectstring = value; }
        }
        private bool _deletelast = false;
        /// <summary>
        /// true, ha toroltuk az utolso verziot
        /// </summary>
        public bool LastVersionDeleted
        {
            get { return _deletelast; }
            set { _deletelast = value; }
        }
        private string _deleteversionid = "0";
        /// <summary>
        /// torolt verzio azonositoja
        /// </summary>
        public string DeletedVersionId
        {
            get { return _deleteversionid; }
            set { _deleteversionid = value; }
        }
        private int _tartalommaxlength = 0;
        /// <summary>
        /// TERVEZO hasznalja
        /// </summary>
        public int TartalomMaxLength
        {
            get { return _tartalommaxlength; }
        }
        private int _szovegmaxlength = 0;
        /// <summary>
        /// TERVEZO hasznalja
        /// </summary>
        public int SzovegMaxLength
        {
            get { return _szovegmaxlength; }
        }
        private bool _leirorendben = true;
        private DataTable _schematabla = null;
        private bool _kelloszlszov = true;
        /// <summary>
        /// TERVEZO-nek kell
        /// </summary>
        public bool KellOszlopSzov
        {
            get { return _kelloszlszov; }
        }
        private string _leirohibaszov = "";
        /// <summary>
        /// Ha a leirotabla ellenorzesekor javithatatlan hibat talal, annak hibaszovege
        /// </summary>
        public string LeiroHibaszov
        {
            get { return _leirohibaszov; }
        }

        // Altalanos listazohoz
        private ArrayList _beallitottidertekek = new ArrayList();
        /// <summary>
        /// Adattablahoz kapcsolt DataView
        /// </summary>
        public DataView DataView
        {
            get { return _adattabla.DataView; }
        }
        /// <summary>
        /// Ha tartozik hozza DataGridView
        /// </summary>
        public DataGridView DataGridView
        {
            get
            {
                if (_aktcontinfo == null || _aktcontinfo.DataGridView == null)
                    return null;
                else
                    return _aktcontinfo.DataGridView;
            }
        }
        /// <summary>
        /// A TERVEZO hasznalja
        /// </summary>
        public DataView InputDataView
        {
            get
            {
                if (_inputtable == null)
                    return null;
                else
                    return _inputtable.DataView;
            }
        }
        /// <summary>
        /// A TERVEZO hasznalja
        /// </summary>
        public DataGridView InputDataGridView
        {
            get
            {
                if (_inputtable == null)
                    return null;
                else
                    return _inputtable.GridView;
            }
            set
            {
                if (_inputtable != null)
                    _inputtable.GridView = value;
            }
        }
        /// <summary>
        /// A tabla neve
        /// </summary>
        public string Tablanev
        {
            get { return _adattabla.TableName; }
        }
        /// <summary>
        /// TABLANEV nevu mezo indexe
        /// </summary>
        public int Tablanevcol
        {
            get { return _adattabla.Columns.IndexOf("TABLANEV"); }
        }
        /// <summary>
        /// SZOVEG nevu mezo indexe
        /// </summary>
        public int Szovegcol
        {
            get { return _adattabla.Columns.IndexOf("SZOVEG"); }
        }
        /// <summary>
        /// SZOVEG nevu mezo tartalma vagy ""
        /// </summary>
        public string Szoveg
        {
            get { return _azonositok.Szoveg; }
        }
        /// <summary>
        /// Termeszetes vagy Szarmazekos ("T "/"SZ") tabla
        /// </summary>
        public string TermSzarm
        {
            get { return _azonositok.Termszarm; }
        }
        /// <summary>
        /// Tabla szintje (R/U/C a tobbi alkalmazasfuggo)
        /// </summary>
        public string Szint
        {
            get { return _azonositok.Szint; }
        }
        /// <summary>
        /// Tabla adatfajta ("T"ablazat/"K"odtabla/"C"soport/"O"sszefugges/"F"ogalom/"S"zukitett kodtabla/"M"ezonevek)
        /// </summary>
        public string Adatfajta
        {
            get { return _azonositok.Adatfajta; }
        }

        /// <summary>
        /// Tabla azonositoja = Termszarm+Szint+Adatfajta
        /// </summary>
        public string Azon
        {
            get { return _azonositok.Azon; }
        }
        /// <summary>
        /// KODTIPUS nevu mezo indexe
        /// </summary>
        public int Kodtipuscol
        {
            get { return _adattabla.Columns.IndexOf("KODTIPUS"); }
        }
        /// <summary>
        /// KODTIPUS nevu mezo tartalma vagy ""
        /// </summary>
        public string Kodtipus
        {
            get { return _azonositok.Kodtipus; }
        }
        /// <summary>
        /// AZONTIP nevu mezo indexe 
        /// </summary>
        public int Azontipcol
        {
            get { return _adattabla.Columns.IndexOf("AZONTIP"); }
        }
        /// <summary>
        /// Teljes tablaazonosito AZON+TABLANEV illetve AZON+KODTIPUS
        /// </summary>
        public string Azontip
        {
            get
            {
                if (_leiroe || _azonositok.Azon == "LEIR")
                    return _azonositok.Azon + _azonositok.Tablanev;
                else
                    return _azonositok.Azontip;
            }
        }
        /// <summary>
        /// AZONTIP1 nevu mezo indexe
        /// </summary>
        public int Azontip1col
        {
            get { return _adattabla.Columns.IndexOf("AZONTIP1"); }
        }
        /// <summary>
        /// AZONTIP1 nevu mezo tartalma vagy ""
        /// </summary>
        public string Azontip1
        {
            get { return _azonositok.Azontip1; }
        }
        /// <summary>
        /// SZOVEG1 nevu mezo indexe
        /// </summary>
        public int Szoveg1col
        {
            get { return _adattabla.Columns.IndexOf("SZOVEG1"); }
        }
        /// <summary>
        /// SZOVEG1 nevu mezo tartalma vagy ""
        /// </summary>
        public string Szoveg1
        {
            get { return _azonositok.Szoveg1; }
        }
        /// <summary>
        /// AZONTIP2 nevu mezo indexe
        /// </summary>
        public int Azontip2col
        {
            get { return _adattabla.Columns.IndexOf("AZONTIP2"); }
        }
        /// <summary>
        /// AZONTIP2 nevu mezo tartalma vagy ""
        /// </summary>
        public string Azontip2
        {
            get { return _azonositok.Azontip2; }
        }
        /// <summary>
        /// SZOVEG2 nevu mezo indexe
        /// </summary>
        public int Szoveg2col
        {
            get { return _adattabla.Columns.IndexOf("SZOVEG2"); }
        }
        /// <summary>
        /// SZOVEG2 nevu mezo tartalma vagy ""
        /// </summary>
        public string Szoveg2
        {
            get { return _azonositok.Szoveg2; }
        }
        /// <summary>
        /// TERVEZO hasznalja
        /// </summary>
        public string SzovegColName
        {
            get
            {
                string nev = "";
                if (_azonositok.Szovegcolname != "" && _adattabla.Columns.IndexOf(_azonositok.Szovegcolname) != -1)
                    nev = _azonositok.Szovegcolname;
                else
                    nev = _azonositok.Sorazonositomezo;
                if (nev == "")
                    return nev;
                else
                {
                    Cols egycol = _combocolumns[nev];
                    if (egycol == null)
                        return nev;
                    else
                        return nev + "_K";
                }
            }
        }
        /// <summary>
        /// KOD nevu mezo max. hossza
        /// </summary>
        public int KodHossz
        {
            get { return _azonositok.Kodhossz; }
        }
        /// <summary>
        /// SZOVEG nevu mezo max.hossza
        /// </summary>
        public int SzovegHossz
        {
            get { return _azonositok.Szoveghossz; }
        }
        /// <summary>
        /// "ORDER BY..." vagy ures
        /// </summary>
        public string OrderString
        {
            get { return _azonositok.Orderstring; }
            set { _azonositok.Orderstring = value; }
        }
        /// <summary>
        /// A tartalomjegyzek azon sora, mely a tablainformaciot definialja
        /// </summary>
        public DataRow TartalSor
        {
            get { return _azonositok.Adatsor; }
        }
        /// <summary>
        /// Adattabla sorai
        /// </summary>
        public DataRowCollection Rows
        {
            get { return ((DataTable)_adattabla).Rows; }
        }
        /// <summary>
        /// Adattabla Column-jai
        /// </summary>
        public DataColumnCollection Columns
        {
            get { return ((DataTable)_adattabla).Columns; }
        }
        /// <summary>
        /// adattabla aktualis soranak indexe
        /// </summary>
        public int Sorindex
        {
            get { return _adattabla.Rowindex; }
        }
        /// <summary>
        /// adattabla aktualis sora
        /// </summary>
        public DataRow AktualRow
        {
            get 
            {
                if (_adattabla.Rowindex == -1)
                    return null;
                return ((DataTable)_adattabla).Rows[_adattabla.Rowindex]; 
            }
        }
        /// <summary>
        /// Adattabla DataView indexe
        /// </summary>
        public int ViewSorindex
        {
            get { return _adattabla.Viewindex; }
            set
            {
                if (value == -1)
                {
                    int i = 0;
                    i++;
                }
                _adattabla.Viewindex = value;
                if (_hivo != null && _hivo.Name == "Formvezerles")
                    _akthozferjog = SetAktsorHozfer();
                else
                    _akthozferjog = _hozferjog;
            }
        }
        /// <summary>
        /// Adattabla DataView aktualis sora
        /// </summary>
        public DataRow AktualViewRow
        {
            get 
            {
                if (_adattabla.Viewindex == -1)
                    return null;
                return _adattabla.DataView[_adattabla.Viewindex].Row; 
            }
        }
        /// <summary>
        /// A tablainformacio legmagasabb verzioja
        /// </summary>
        public int LastVersionId
        {
            get
            {
                if (_verzioterkeparr.Count == 0)
                    return 1;
                else
                    return Convert.ToInt32(_verzioterkeparr[_verzioterkeparr.Count - 1].ToString());
            }
        }
        /// <summary>
        /// Legkisebb verzioid, ha nincs, 1
        /// </summary>
        public int FirstVersionId
        {
            get
            {
                if(_verzioterkeparr.Count==0)
                    return 1;
                else
                    return Convert.ToInt32(_verzioterkeparr[0].ToString());
            }
        }
        /// <summary>
        /// A szintnek megfelelo verzioinfok
        /// </summary>
        public Verzioinfok Verzioinfok
        {
            get { return _azonositok.Verzioinfok; }
        }
        /// <summary>
        /// Lezart-e az aktualis verzio
        /// </summary>
        public bool LezartVersion
        {
            get
            {
                Verzioinfok verinfo = _azonositok.Verzioinfok;
                return verinfo.AktVerzioLezart(_aktverzioid);
            }
        }
        /// <summary>
        /// Lezart-e az utolso verzio
        /// </summary>
        public bool LastVersionLezart
        {
            get { return _azonositok.Verzioinfok.LastVerzioLezart(); }
        }
        /// <summary>
        /// Ha kell verzio, az aktualis verzio ervenyessegenek kezdet, egyebkent mindate
        /// </summary>
        public DateTime AktDatumKezd
        {
            get
            {
                DateTime dat = Fak.Mindatum;
                if (_azonositok.Kellverzio)
                    dat = _aktintervallum[0];
                return dat;
            }
        }
        /// <summary>
        /// Ha kell verzio, az aktualis verzio ervenyessegenek vege, egyebkent mindate
        /// </summary>
        public DateTime AktDatumVeg
        {
            get
            {
                DateTime dat = Fak.Mindatum;
                if (_azonositok.Kellverzio)
                    dat = _aktintervallum[1];
                return dat;
            }
        }
        /// <summary>
        /// Az utolso verzio ervenyessegi hatarai
        /// </summary>
        public DateTime[] LastVerzioIntervallum
        {
            get
            { 
                Verzioinfok verinfo = _azonositok.Verzioinfok;
                return (DateTime[])verinfo.VerzioDatumTerkep[verinfo.VerzioDatumTerkep.Count-1];
            }
        }
        /// <summary>
        /// Szerepelhet-e comboban
        /// </summary>
        public bool LehetCombo
        {
            get { return _azonositok.Lehetcombo; }
        }
        /// <summary>
        /// Lehet-e csoportinformacio eleme
        /// </summary>
        public bool LehetCsoport
        {
            get { return _azonositok.Lehetcsoport; }
        }
        /// <summary>
        /// Lehet-e osszefugges eleme
        /// </summary>
        public bool LehetOsszef
        {
            get { return _azonositok.Lehetosszef; }
        }
        /// <summary>
        /// DataView SORT
        /// </summary>
        public string InitialSort
        {
            get { return _azonositok.Sort; }
        }
        /// <summary>
        /// Melyik mezo tartalma keruljon Combo eseten az adatbazis tablaba
        /// </summary>
        public string ComboFileba
        {
            get { return _azonositok.Combofileba; }
        }
        /// <summary>
        /// Mely mezo(k) tartalma keruljon a Combo itemek-be
        /// </summary>
        public string[] ComboSzovegbe
        {
            get { return _azonositok.Comboszovegbe; }
        }
        /// <summary>
        /// Combo itemek SORTja
        /// </summary>
        public string ComboSort
        {
            get { return _azonositok.Combosort; }
        }
        /// <summary>
        /// TERVEZOVEL sorazonositonak megadott mezo neve
        /// </summary>
        public string Sorazonositomezo
        {
            get { return _azonositok.Sorazonositomezo; }
        }
        /// <summary>
        /// ha adtunk sorazonositomezot, annak az indexe, egyebkent az identity mezo indexe
        /// </summary>
        public int Azonositocol
        {
            get
            {
                if (_azonositok.Sorazonositomezo != "")
                    return _adattabla.Columns.IndexOf(_azonositok.Sorazonositomezo);
                else
                    return _identitycolindex;
            }
        }
        /// <summary>
        /// PREV_ID nevu mezo indexe
        /// </summary>
        public int PrevIdcol
        {
            get { return _adattabla.Columns.IndexOf("PREV_ID"); }
        }
        /// <summary>
        /// PREV_ID1 nevu mezo indexe (osszefugg jellegu)
        /// </summary>
        public int PrevId1col
        {
            get { return _adattabla.Columns.IndexOf("PREV_ID1"); }
        }
        /// <summary>
        /// PREV_ID2 nevu mezo indexe (osszefugg jellegu)
        /// </summary>
        public int PrevId2col
        {
            get { return _adattabla.Columns.IndexOf("PREV_ID2"); }
        }
        /// <summary>
        /// A tablahoz kell verziot nyilvantartani?
        /// </summary>
        public bool KellVerzio
        {
            get { return _azonositok.Kellverzio; }
        }
        /// <summary>
        /// Valtozott-e a tabla tartalma
        /// </summary>
        public bool Valtozott
        {
            get { return HozferJog == Base.HozferJogosultsag.Irolvas && ( _modositott || _changed || _modositasihiba); }
            set
            {
                Modositott = value;
                ModositasiHiba = value;
                Changed = value;
            }
        }
        /// <summary>
        /// lehet ures tabla? (termeszetes-eknel),kiindulaskent a parameterezesnel beallitott ertek
        /// userprogram valtoztathatja
        /// </summary>
        public bool LehetUres
        {
            get { return _azonositok.Lehetures; }
            set { _azonositok.Lehetures = value; }
        }
        /// <summary>
        /// lehet ures tabla? (termeszetes-eknel), a parameterezes allitja be
        /// </summary>
        public bool EredetiLehetUres
        {
            get { return _azonositok.EredetiLehetures; }
        }
        /// <summary>
        /// A felhasznaloi program allithatja, azt jelenti, hogy egy adott tablara szukseg van-e
        /// </summary>
        public bool NemKell
        {
            get { return _azonositok.Nemkell; }
            set { _azonositok.Nemkell = value; }
        }
        /// <summary>
        /// A termeszetes, fastrukturaba rendezett tablaknal a tabla osszes szulotablainfo-ja tombben
        /// </summary>

        public Tablainfo[] TermParentTablainfok
        {
            get { return (Tablainfo[])_termparentchain.ToArray(typeof(Tablainfo)); }
        }
        private Comboinfok[] _comboinfok = null;
        /// <summary>
        /// Kiajanlasoknal kell
        /// </summary>
        public Comboinfok[] ComboInfok
        {
            get { return _comboinfok; }
            set { _comboinfok = value; }
        }
        /// <summary>
        /// A LEIRO tabla adattablainformacio letrehozasa
        /// </summary>
        /// <param name="tag">
        /// A tablainformaciot es a hozza tartozo leirotablainformaciot osszefogo objectum
        /// </param>
        /// <param name="tagazon">
        /// a tablainformacio azonositoi
        /// </param>
        /// <param name="fak">
        /// fak
        /// </param>
        public Tablainfo(TablainfoTag tag, Azonositok tagazon, FakUserInterface fak)
        {
            NewTablainfo(null, -1, tag, tagazon, fak, false);
        }
        /// <summary>
        /// Adattablainformacio objectum letrehozas
        /// </summary>
        /// <param name="dt">
        /// Adattabla az informacio letrehozasahoz
        /// </param>
        /// <param name="sorindex">
        /// A kivant informaciot tartalmazo sor indexe
        /// </param>
        /// <param name="tag">
        /// Osszefogo objectum
        /// </param>
        /// <param name="tagazon">
        /// Tablaazonositok
        /// </param>
        /// <param name="fak">
        /// fakuserinterface
        /// </param>
        public Tablainfo(AdatTabla dt, int sorindex, TablainfoTag tag, Azonositok tagazon, FakUserInterface fak)
        {
            NewTablainfo(dt, sorindex, tag, tagazon, fak, false);
        }
        /// <summary>
        /// mint az elozo, de a leiroe parameter tartalma szerint adattabla vagy az adattabla leirotablajanak tablainfojat
        /// hozza letre
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sorindex"></param>
        /// <param name="tag"></param>
        /// <param name="tagazon"></param>
        /// <param name="fak"></param>
        /// <param name="leiroe">
        /// true: leirotabla
        /// false: adattabla
        /// </param>
        public Tablainfo(AdatTabla dt, int sorindex, TablainfoTag tag, Azonositok tagazon, FakUserInterface fak, bool leiroe)
        {
            NewTablainfo(dt, sorindex, tag, tagazon, fak, leiroe);
        }
        private void NewTablainfo(AdatTabla dt, int sorindex, TablainfoTag tag, Azonositok tagazon, FakUserInterface fak, bool leiroe)
        {
            _leiroe = leiroe;
            _fak = fak;
            _tablatag = tag;
            _parenttag = tag.ParentTag;
            _azonositok = tagazon;
            if (dt != null)
                _adattabla = dt;
        }

        /// <summary>
        /// Tablainformacio inicializalasa
        /// </summary>
        /// <param name="kelloszlszov">
        /// true: Mezotulajdonsagok megadasanal a sor/oszlopszovegeket Combobol kell-e valasztani
        /// </param>
        public void Init(bool kelloszlszov)
        {
            _kelloszlszov = kelloszlszov;
            string tablanev = _adattabla.TableName;
            _selectstring = _azonositok.Selectstring;
            _adattabla.Connection = _azonositok.Verzioinfok.AktualConnection;
            _adattabla.Tablainfo = this;
            DateTime[] nulldat = new DateTime[2] { _fak.Mindatum, _fak.Mindatum };
            Azonositok azon = _azonositok;
            if (_azonositok.Szuloszint != "" && _azonositok.Szulotabla != "")
            {
                string parentazon = "T " + _azonositok.Szuloszint + "T" + _azonositok.Szulotabla;
 //               _termparentinfo = _fak.Tablainfok.GetBySzintPluszTablanev(_azonositok.Szuloszint, _azonositok.Szulotabla);
                _termparentinfo = _fak.Tablainfok.GetByAzontip(parentazon);// GetBySzintPluszTablanev(_azonositok.Szuloszint, _azonositok.Szulotabla); 
                if (_termparentinfo != null)
                {
                    ArrayList ar = new ArrayList();
                    if (_termparentinfo.TermChildTabinfo.IndexOf(this) == -1)
                        _termparentinfo.TermChildTabinfo.Add(this);
                    _firsttermparent = _termparentinfo;
                    ar.Add(_termparentinfo);
                    Tablainfo firstpar = _termparentinfo.TermParentTabinfo;
                    _gyokerek.Add(_firsttermparent);
                    if (firstpar != null)
                    {
                        do
                        {
                            if (firstpar == null)
                                break;
                            ar.Add(firstpar);
                            _firsttermparent = firstpar;
                            firstpar = _firsttermparent.TermParentTabinfo;
                        } while (true);
                    }
                    if (_fak.GyokerTablainfok.IndexOf(_firsttermparent) == -1)
                    {
                        _fak.GyokerTablainfok.Add(_firsttermparent);
                        _fak.gyokerchainek.Add(new TablainfoCollection());
                    }
                    int i = ar.Count - 1;
                    do
                    {
                        _termparentchain.Add((Tablainfo)ar[i]);
                        i--;
                    } while (i >= 0);
                }
            }
            Adattolt(_fak.Aktintervallum, true);
            _datumtolcol = _adattabla.Columns.IndexOf("DATUMTOL");
            _datumigcol = _adattabla.Columns.IndexOf("DATUMIG");
            _sorszam1col = _adattabla.Columns.IndexOf("SORSZAM1");
            _sorszam2col = _adattabla.Columns.IndexOf("SORSZAM2");
            _verzioidcol = _adattabla.Columns.IndexOf("VERZIO_ID");
            _verzioid1col = _adattabla.Columns.IndexOf("VERZIO_ID1");
            _verzioid2col = _adattabla.Columns.IndexOf("VERZIO_ID2");
            Beallit();
        }
        /// <summary>
        /// Adattabla feltoltese aktualis adatokkal
        /// </summary>
        /// <param name="intervallum">
        /// Ervenyesseg -tol/-ig
        /// </param>
        /// <param name="force">
        /// true: mindenkeppen kell tolteni, false: csak, ha az intervallum valtozott
        /// </param>
        public void Adattolt(DateTime[] intervallum, bool force)
        {
            Azonositok azon = _azonositok;
            _adattabla.DataView.RowFilter = "";
            if (!force && intervallum[0].CompareTo(_aktintervallum[1]) != 1 && intervallum[1].CompareTo(_aktintervallum[0]) != -1)
            {
            }
            string datumresz = "";
            string tablanev = Tablanev;
            _leirorendben = true;
            _aktintervallum = intervallum;
            if (azon.Kellverzio)
            {
                
                Verzioinfok verinf = Verzioinfok;
                string aktverinfid = verinf.AktVerzioId.ToString();
                _verzioterkeparr.Clear();
                int[] verzioidk = verinf.VersionArray;
                foreach (Int32 egyverzioid in verzioidk)
                {
                    if (_adattabla.LastSel == "" || _adattabla.LastSel != azon.Selectstring)
                    {
                        if (azon.Selectstring == "")
                            datumresz = "where ";
                        else
                            datumresz = " and ";
                        datumresz += "VERZIO_ID=" + egyverzioid + " ";
                        Sqlinterface.Select((DataTable)_adattabla, _adattabla.Connection, tablanev, azon.Selectstring + datumresz, azon.Orderstring, true);
                        if (_adattabla.Rows.Count != 0 || egyverzioid!=verinf.LastVersionId)
                            _verzioterkeparr.Add(egyverzioid);
                    }
                }
                if (_verzioterkeparr.Count == 0)
                {
                    _aktverzioid = verinf.LastVersionId;
                    _aktintervallum = (DateTime[])verinf.VerzioDatumTerkep[verinf.VerzioDatumTerkep.Count - 1];
                    if (!"COS".Contains(_azonositok.Adatfajta) || _leiroe)
                        _verzioterkeparr.Add(_aktverzioid);
                }
                else
                {
                    int i = _verzioterkeparr.IndexOf(Convert.ToInt32(aktverinfid));
                    if (i != -1)
                    {
                        _aktverzioid = verinf.AktVerzioId;
                        _aktintervallum = verinf.AktIntervallum;
                    }
                    else
                    {
                        _aktverzioid = Convert.ToInt32(_verzioterkeparr[_verzioterkeparr.Count - 1].ToString());
                        i = verinf.VersionIdCollection.IndexOf(_aktverzioid);
                        _aktintervallum = (DateTime[])verinf.VerzioDatumTerkep[i];
                    }
                }
            }
            _adattabla.Select();
            if (_adattabla.Columns.IndexOf("LAST_MOD") != -1 && _adattabla.DataView.Count != 0)
            {
                _adattabla.DataView.Sort = "LAST_MOD";
                string upds = _adattabla.DataView[_adattabla.DataView.Count - 1].Row["LAST_MOD"].ToString().Trim();
                if (upds != "")
                    LastUpdate = Convert.ToDateTime(upds);
            }
            _adattabla.DataView.Sort = azon.Sort;
            if (_adattabla.TableName == "VALTOZASNAPLO" || _adattabla.TableName == "USERLOG")
                _adattabla.Rows.Clear();
        }
        /// <summary>
        /// Az adattabla kiegeszito Column-jainak feltoltese
        /// </summary>
        /// <param name="init">
        /// true: sorszamoz
        /// </param>
        public void Tartalmaktolt(bool init)
        {
            Tartalmaktoltkozos(init);
        }
        /// <summary>
        /// Tartalmaktolt(false) hivas, nem sorszamoz
        /// </summary>
        public void Tartalmaktolt()
        {
            Tartalmaktoltkozos(false);
        }
        private void Tartalmaktoltkozos(bool init)
        {
            int j = 100;
            Cols egycol;
            DataRow dr;
            if (_adattabla.DataView.Count == 0)
                _ures = true;
            else
                _ures = false;
            for (int i = 0; i < _adattabla.DataView.Count; i++)
            {
                dr = _adattabla.DataView[i].Row;
                if (init)
                {
                    if (_sorrendcolumn == null && _kiegsorrendcolumn != null)
                    {
                        dr[_kiegsorrendcolumn.ColumnName] = j;
                        j = j + 100;
                    }
                }
                for (int l = 0; l < _tablacolumns.Count; l++)
                {
                    egycol = (Cols)_tablacolumns[l];
                    egycol.OrigTartalom = dr[egycol.ColumnName].ToString();
                    if (egycol.SzamitasiEljaras != "")
                    {
                        string[] paramnevek = egycol.SzamitasiParameterek;
                        if (paramnevek != null)
                        {
                            UserSzamitasok.UserSzamitasok.Szamit(dr,paramnevek,egycol.ColumnName,egycol.SzamitasiEljaras);
                        }
                    }
                    if (egycol.OrigTartalom.Trim() == "")
                        egycol.OrigTartalom = "";
                    if (!egycol.Csakolvas)
                    {
                        if (egycol.OrigTartalom != "" && egycol.DataType.ToString() == "System.DateTime" && egycol.ColumnName != "LAST_MOD")
                            egycol.OrigTartalom = Convert.ToDateTime(dr[egycol.ColumnName]).ToShortDateString();
                    }
                }
                KiegColumnsTolt(dr);
            }
        }
        /// <summary>
        /// DataView kivant sora alapjan  a mezoinformaciok/kiegeszito mezoinformaciok eredeti tartalommal feltoltese
        /// </summary>
        /// <param name="viewindex">
        /// a kivant sorindex. Ha -1, a feltoltes a mezoinfo-k default erteke alapjan
        /// </param>
        public void Tartalmaktolt(int viewindex)
        {
            Cols egycol;
            DataRow dr = null;
            DataRow dr2 = null;
            DataView dataview = _adattabla.DataView;
            if (viewindex == -1 || _identitycolindex == -1 || dataview[viewindex].Row.RowState == DataRowState.Added || dataview[viewindex].Row.RowState == DataRowState.Detached)
                _aktidentity = -1;
            else
                _aktidentity = Convert.ToInt64(dataview[viewindex].Row[_identitycolindex].ToString());
            if (_datumtolcol != -1)
            {
                int k;
                int j;
                if (viewindex == -1)
                {
                    j = dataview.Count - 1;
                    k = j;
                }
                else
                {
                    j = viewindex;
                    k = viewindex - 1;
                }
                if (j != -1)
                    dr = dataview[j].Row;
                if (k != -1)
                    dr2 = dataview[k].Row;
                if (dr == null)
                    _mindattime = "";
                else
                {
                    string datt = dr["DATUMTOL"].ToString();
                    if (datt != "")
                    {
                        _mindattime = Convert.ToDateTime(datt).ToShortDateString();
                    }
                    else
                        _mindattime = "";
                }
            }
            else if (viewindex != -1)
                dr = dataview[viewindex].Row;
            for (int i = 0; i < _tablacolumns.Count; i++)
            {
                egycol = _tablacolumns[i];
                if (dr != null)
                {
                    egycol.Tartalom = dr[i].ToString().Trim();
                    if (egycol.Tartalom != "" && egycol.DataType.ToString() == "System.DateTime" && egycol.ColumnName != "LAST_MOD")
                        egycol.Tartalom = Convert.ToDateTime(dr[i]).ToShortDateString();
                    if (_datumtolcol != -1 && egycol.ColumnName == "DATUMTOL" && viewindex == -1 && _mindattime != "")
                        egycol.Tartalom = Convert.ToDateTime(_mindattime).AddDays(1).ToShortDateString();
                    egycol.OrigTartalom = egycol.Tartalom;
                    egycol.ComboAktFileba = "";
                    egycol.ComboAktSzoveg = "";
                    if (egycol.Kiegcol != null && !egycol.Comboe)
                    {
                        egycol.Kiegcol.ComboAktFileba = "";
                        egycol.Kiegcol.ComboAktSzoveg = "";
                        egycol.Kiegcol.Tartalom = "";
                        egycol.Kiegcol.OrigTartalom = "";
                    }

                }
                else
                {
                    egycol.OrigTartalom = egycol.DefaultValue.ToString();
                    if (egycol.Lehetures)
                        egycol.Tartalom = "";
                    else
                        egycol.Tartalom = egycol.OrigTartalom;
                }
                if (!egycol.Csakolvas && egycol.Comboe)
                {
                    if (egycol.ColumnName != "SORSZOV" && egycol.ColumnName != "OSZLSZOV")
                    {
                        egycol.ComboAktSzoveg = "";
                        if (Tablanev == "KIAJANL")
                        {
                            if (egycol.ColumnName == "RSORSZAM" && _comboinfok != null && dr != null)
                            {
                                string azontip = dr["AZONTIP"].ToString();
                                egycol.Combo_Info = Fak.ComboInfok.ComboinfoKeres(azontip, this, egycol, null);
                                egycol.Tartalom = dr["RSORSZAM"].ToString();
                                Cols egyinp = InputColumns[egycol.ColumnName];
                                egyinp.Tartalom = egycol.Tartalom;
                                egycol.Kiegcol.Combo_Info = egycol.Combo_Info;
                            }
                        }
                        //if (egycol.OrigTartalom != "" && egycol.OrigTartalom != "0")
                        else
                        {
                            int maxlen = 0;
                            if (egycol.ComboAzontipCombo != null)
                            {
                                egycol.ComboAzontipCombo.SetComboAktszoveg(egycol);
                                maxlen = egycol.ComboAzontipCombo.Maxhossz;
                            }
                            else if (egycol.Combo_Info != null)
                            {
                                egycol.Combo_Info.SetComboAktszoveg(egycol);
                                maxlen = egycol.Combo_Info.Maxhossz;
                            }

                            egycol.Kiegcol.OrigTartalom = egycol.ComboAktSzoveg;
                            egycol.Kiegcol.ComboAktSzoveg = egycol.ComboAktSzoveg;
                            if (dr != null)
                            {
                                if (egycol.Kiegcol.MaxLength < maxlen)//egycol.Kiegcol.OrigTartalom.Length)
                                    egycol.Kiegcol.MaxLength = maxlen;// egycol.Kiegcol.OrigTartalom.Length;
                                dr[egycol.Kiegcol.ColumnName] = egycol.Kiegcol.OrigTartalom;
                                if (egycol.Kiegcol.OrigTartalom == "" && !" 0".Contains(egycol.OrigTartalom))
                                    dr[egycol.Kiegcol.ColumnName] = egycol.OrigTartalom;
                            }
                        }
                    }
                    else
                    {
                        egycol.ComboAktSzoveg = egycol.OrigTartalom;
                        egycol.ComboAktFileba = egycol.OrigTartalom;
                    }
                }
                if (egycol.SzamitasiEljaras != "")
                {
                    string[] paramnevek = egycol.SzamitasiParameterek;
                    if (paramnevek != null)
                    {
                        UserSzamitasok.UserSzamitasok.Szamit(dr, paramnevek, egycol.ColumnName, egycol.SzamitasiEljaras);
                    }
                }
            }
            if (dr2 != null)
            {
                _mindattime = dr2["DATUMTOL"].ToString();
                if (_mindattime != "")
                    _mindattime = Convert.ToDateTime(_mindattime).ToShortDateString();
            }
        }
        /// <summary>
        /// Adattablaba uj sor felvetele
        /// </summary>
        /// <returns>
        /// az uj sor
        /// </returns>
        public DataRow Ujsor()
        {
            return _adattabla.Ujsor();
        }
        /// <summary>
        /// TERVEZO: Egy sor inputjat befejezem, OK-t nyomok, nem talaltam hibat, a sort be kell tolteni az adattablaba
        /// </summary>
        /// <param name="viewindex">
        /// aktualis viewindex
        /// </param>
        /// <param name="beszur">
        /// beszuras?
        /// </param>
        /// <param name="sorrend">
        /// mi legyen a sorrendmezo tartalma
        /// </param>
        /// <returns>
        /// a bevitt/modositott sor
        /// </returns>
        public DataRow AdatsortoltInputtablabol(int viewindex, bool beszur, int sorrend)
        {
            return _adattabla.AdatsortoltInputtablabol(viewindex, beszur, sorrend);
        }
        /// <summary>
        /// az adattabla AdatSortolt() eljarasat hivja. Leiras ott
        /// </summary>
        /// <returns></returns>
        public DataRow Adatsortolt()
        {
            return _adattabla.AdatSortolt();
        }
        /// <summary>
        /// Adatsor torlese
        /// </summary>
        /// <param name="viewindex">
        /// A torolni kivant sor viewindexe
        /// </param>
        /// <returns>
        /// DataView a torles utan
        /// </returns>
        public DataView Adatsortorol(int viewindex)
        {
            return _adattabla.Adatsortorol(viewindex);
        }
        /// <summary>
        /// a DataView minden sorat torli
        /// </summary>
        public void TeljesTorles()
        {
            _adattabla.Teljestorles();
        }
        /// <summary>
        /// Uj verzio eloallitasa, az update-et hajtsa vegre
        /// </summary>
        /// <returns>
        /// uj verzio id string
        /// </returns>
        public string CreateNewVersion()
        {
            return CreateNewVersion(true);
        }
        /// <summary>
        /// Torolje a tabla utolso verziojat update-tel
        /// </summary>
        /// <returns>
        /// true:sikeres
        /// </returns>
        public bool DeleteLastVersion()
        {
            return DeleteLastVersion(true);
        }
        /// <summary>
        /// Torolje a tabla megadott verziojat, ha van ilyen (update nelkul)
        /// </summary>
        /// <param name="verz">
        /// kivant verzio id
        /// </param>
        /// <returns>
        /// true: sikeres
        /// </returns>
        public bool DeleteLastVersion(string verz)
        {
            return DeleteLastVersion(verz, false);
        }
        /// <summary>
        /// a DataView minden sorabol az identity-t szervezi tombbe
        /// </summary>
        /// <returns></returns>
        public ArrayList GetIdentityArray()
        {
            DataView view = _adattabla.DataView;
            ArrayList ar = new ArrayList();
            for (int i = 0; i < view.Count; i++)
            {
                DataRow dr = view[i].Row;
                ar.Add(dr[IdentityColumnIndex].ToString());
            }
            return ar;
        }
        /// <summary>
        /// keresse meg azokat a sorokat, melyek a parametertombnek megfelelnek
        /// </summary>
        /// <param name="colnametartalom">
        /// olyan objectumok tombje, melynek minden eleme egy ketelemu objectum
        /// 1 - az oszlop neve
        /// 2 - a keresett tartalom
        /// </param>
        /// <returns>
        /// a megfelelo sorok tombje vagy null
        /// </returns>
        public DataRow[] Find(object[] colnametartalom)
        {
            return _adattabla.Find(colnametartalom);
        }
        /// <summary>
        /// sor kereses, ahol az adott nevu oszlop tartalma az adott tartalom
        /// </summary>
        /// <param name="ColumnName">
        /// keresett oszlop neve
        /// </param>
        /// <param name="tartalom">
        /// keresett tartalom
        /// </param>
        /// <returns>
        /// a sor, vagy null
        /// </returns>
        public DataRow Find(string ColumnName, string tartalom)
        {
            return _adattabla.Find(ColumnName, tartalom);
        }
        /// <summary>
        /// kezeloiszint, viewindex alapjan aktualis hozzaferesi jogosultsag beallitas
        /// </summary>
        /// <param name="kezeloiszint">
        /// kezeloi szint
        /// </param>
        /// <param name="controlnev">
        /// a control neve
        /// </param>
        /// <returns>
        /// Hozzaferesi jogosultsag
        /// </returns>
        public void SetAktHozferJog(Base.KezSzint kezeloiszint, string controlnev)
        {
            _aktkezeloszint=kezeloiszint;
            _aktusername=controlnev;
            _hozferjog = _azonositok.Jogszintek[Convert.ToInt16(kezeloiszint)];
            if (DataView.Count == 0)
                ViewSorindex = -1;
            else if (ViewSorindex == -1)
                ViewSorindex = 0;
            else
                ViewSorindex = _adattabla.Viewindex;

            //else
            //{
            //    int i = ViewSorindex;
            //}
        }
        private Base.HozferJogosultsag SetAktsorHozfer()
        {
            Base.HozferJogosultsag hozfer = _hozferjog;
            if (hozfer != Base.HozferJogosultsag.Irolvas && (_fak.Alkalmazas == "TERVEZO" ||
            _fak.Alkalmazas != "TERVEZO" && TermSzarm.Trim() == "T"))
            //            TermSzarm.Trim() == "T" && _fak.Alkalmazas != "TERVEZO")
            {
                _beszurhat = false;
                _torolhet = false;
                _modosithat = false;
                return hozfer;
            }
            string owner = "";
            _ownernev = "OWNER";
            bool enyem;
            if (Leiroe)
            {
                enyem = TablaTag.Tablainfo.Azonositok.LeiroEnyem;
            }
            else
                enyem = _azonositok.Enyem;

            int i = _tablacolumns.IndexOf(_ownernev);
            if (i == -1)
            {
                _ownernev = "ALKALMAZAS_ID";
                i = _tablacolumns.IndexOf(_ownernev);
            }
            if (i == -1)
            {
                _ownernev = "ALKALM_ID";
                i = _tablacolumns.IndexOf(_ownernev);
                if (i == -1)
                    _ownernev = "";
            }
            int j = _tablacolumns.IndexOf("ENYEM");
            if (ViewSorindex != -1)
            {
                if (j != -1 && Tablanev != "TARTAL" && Tablanev != "LEIRO")
                    enyem = _fak.SetBoolByIgenNem(_adattabla.DataView[_adattabla.Viewindex]["ENYEM"].ToString());
                if (i != -1)
                    owner = _adattabla.DataView[_adattabla.Viewindex][_ownernev].ToString();
            }
            if (_fak.Enyem)
                enyem = false;
            if (hozfer != Base.HozferJogosultsag.Irolvas || ViewSorindex == -1)
            {
            }
            else
            {
                if (_azonositok.Azon == "SZRM")
                    hozfer = Base.HozferJogosultsag.Csakolvas;
                else if (Szint == "R" && _aktkezeloszint != Base.KezSzint.Fejleszto)
                    hozfer = Base.HozferJogosultsag.Csakolvas;
                else if ((Tablanev == "VALTOZASNAPLO" || Tablanev == "USERLOG") && _aktkezeloszint != Base.KezSzint.Fejleszto
                    && _aktkezeloszint != Base.KezSzint.Minden && !_aktkezeloszint.ToString().Contains("Rendszergazda"))
                    hozfer = Base.HozferJogosultsag.Csakolvas;
                else if (Tablanev == "LEIRO" && _aktusername == "Mezonevek")
                    hozfer = Base.HozferJogosultsag.Csakolvas;
                else if ((owner == "" || owner == "0") && enyem && _aktusername != "Tooltipallit" && _fak.Alkalmazas == "TERVEZO")
                    hozfer = Base.HozferJogosultsag.Csakolvas;
            }
            if (owner != "" && owner!="0")
            {
                if (Tablanev == "TARTAL")
                {
                    if (_azonositok.Azon != "SZRM")
                    {
                        _modosithat = true;
                        _torolhet = true;
                        _beszurhat = true;
                    }
                    else
                    {
                        _modosithat = false;
                        _torolhet = false;
                        _beszurhat = false;
                    }
                }
                else
                {
                    _beszurhat = _fak.SetBoolByIgenNem(_azonositok.Beszurhat);
                    _torolhet = _azonositok.Torolheto;
                    _modosithat = _fak.SetBoolByIgenNem(_azonositok.Modosithat);
                }
            }
            else
            {
                if (hozfer == Base.HozferJogosultsag.Irolvas)
                {
                    if (Tablanev == "TARTAL")
                    {
                        _modosithat = true;
                        if (ViewSorindex == -1 || AktualViewRow["TOROLHETO"].ToString() == "I")
                            _torolhet = true;
                        else
                            _torolhet = false;
                        _beszurhat = true;
                    }
                    else if (Tablanev == "BASE")
                    {
                        _beszurhat = _fak.SetBoolByIgenNem(_azonositok.Beszurhat);
                        _torolhet = _azonositok.Torolheto;
                        _modosithat = _fak.SetBoolByIgenNem(_azonositok.Modosithat);
                    }
                    else
                    {
                        _beszurhat = _fak.SetBoolByIgenNem(_azonositok.Beszurhat);
                        _torolhet = _azonositok.Torolheto;
                        _modosithat = _fak.SetBoolByIgenNem(_azonositok.Modosithat);
                        int jj = _tablacolumns.IndexOf("SORENYEM");
                        if (jj != -1 && ViewSorindex != -1)
                        {
                            _torolhet = _azonositok.Torolheto && (AktualViewRow["SORENYEM"].ToString() == "N" || _fak.Enyem);
                            _modosithat = _fak.SetBoolByIgenNem(_azonositok.Modosithat) && (AktualViewRow["SORENYEM"].ToString() == "N" || _fak.Enyem);
                        }
                        else if(!_azonositok.Enyem)
                        {
                            _torolhet = _azonositok.Torolheto && (_fak.Enyem || Fak.Alkalmazas!="TERVEZO");
                            _modosithat = _fak.SetBoolByIgenNem(_azonositok.Modosithat) && (_fak.Enyem || Fak.Alkalmazas != "TERVEZO");
                        }
                    }
                } 
                else
                {
                    _modosithat = false;
                    _torolhet = false;
                    _beszurhat = false;
                }
            }
            return hozfer;
        }
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ListaInfok ListaInfok = null;
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public TablainfoCollection Tablainfok = null;
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public Tablainfo ElsoTabinfo = null;
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public string TablainfoSelect = "";
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public string TablainfoOszlopSelect = "";
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public string TablainfoSorSelect = "";
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList SelectElemek = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList OszlopSelectElemek = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList SorSelectElemek = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList RowFilterek = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList OszlopRowFilterek = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList SorRowFilterek = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public FeltetelinfoCollection Feltetelek = new FeltetelinfoCollection();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public FeltetelinfoCollection OszlopFeltetelek = new FeltetelinfoCollection();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public FeltetelinfoCollection SorFeltetelek = new FeltetelinfoCollection();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public int RowFilterIndex = -1;
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public int OszlopRowFilterIndex = -1;
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public int SorRowFilterIndex = -1;
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public string Sort = "";
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public string SorSort = "";
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public string OszlopSort = "";
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList OsszesBeallitandoId = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList OsszesBeallitottIdErtek = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList OsszesBeallitandoOszlopId = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList OsszesBeallitottOszlopIdErtek = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList OsszesBeallitandoSorId = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList OsszesBeallitottSorIdErtek = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList sorrendazonositok = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList sorrendtartalomoszlopnevek = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList sorrendtartalmak = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList csakosszegsorba = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList sorrendmezoinfok = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList oszlopmezonevek = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList oszloptartalomnevek = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList oszlopszelessegek = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList oszlopnumericek = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList oszlopmezoinfok = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public string DatumString = "";
        ///// <summary>
        ///// Altalanos listazashoz
        ///// </summary>
        //public string OszlopDatumString = "";
        ///// <summary>
        ///// Altalanos listazashoz
        ///// </summary>
        //public string SorDatumString = "";
        ///// <summary>
        ///// Altalanos listazashoz
        ///// </summary>
        public ArrayList SzuksegesIdk = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList SzuksegesOszlopIdk = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList SzuksegesSorIdk = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList BeallitandoIdk = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList BeallitottIdertekek = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList BeallitandoOszlopIdk = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList BeallitottOszlopIdertekek = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList BeallitandoSorIdk = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList BeallitottSorIdertekek = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList ElozoBeallitottIdertekek = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList ElozoBeallitottOszlopIdertekek = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList ElozoBeallitottSorIdertekek = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList BeallitottIndexek = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList BeallitottOszlopIndexek = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList BeallitottSorIndexek = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList SzuksegesIndexek = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList SzuksegesOszlopIndexek = new ArrayList();
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList SzuksegesSorIndexek = new ArrayList();
        public string UserSelect = "";
        /// <summary>
        /// Altalanos listazashoz RowFilter allitasa
        /// </summary>
        public bool SetRowFilter()
        {
            Tablainfo tabinfo = this;
            DataView view = tabinfo.DataView;
            bool retert = false;
            string filtstring = GetRowFilterResz(OsszesBeallitottIdErtek);
            if (RowFilterek.Count == 0)
            {
                if (tabinfo == ElsoTabinfo)
                {
                    if (RowFilterIndex == -1)
                    {
                        RowFilterIndex++;
                        retert = true;
                    }
                    else
                        retert = false;
                }
                else
                {
                    RowFilterIndex = ElsoTabinfo.RowFilterIndex;
                    if (tabinfo.ViewSorindex != -1)
                    {
                        view.RowFilter = filtstring;
                        if (view.Count != 0)
                            retert = true;
                    }
                }
            }
            else
            {
                string[] lastertekek = (string[])ElozoBeallitottIdertekek.ToArray(typeof(string));
                if (lastertekek.Length == 0)
                    retert = true;
                else
                {
                    string[] szuksegesidk = (string[])SzuksegesIdk.ToArray(typeof(string));//(string[])filtinf.SzuksegesIdk.ToArray(typeof(string));
                    for (int i = 0; i < szuksegesidk.Length; i++)
                    {
                        foreach(string egybeallitando in OsszesBeallitandoId)
                        {
                            if (szuksegesidk[i] == egybeallitando.ToString())
                            {
                                if (lastertekek[i] != egybeallitando.ToString())
                                {
                                    retert = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (retert)
                {
                    if (tabinfo == ElsoTabinfo)
                    {
                        if (RowFilterIndex == -1)
                            RowFilterIndex++;
                        else if (RowFilterIndex >= RowFilterek.Count - 1)
                            retert = false;
                        else
                            RowFilterIndex++;
                    }
                    else
                        RowFilterIndex = ElsoTabinfo.RowFilterIndex;
                    if (retert)
                    {
                        string rowfilter = RowFilterek[RowFilterIndex].ToString();
                        if (tabinfo == ElsoTabinfo && rowfilter == "" && view.RowFilter!="")
                        {
                        }
                        else
                        {
                            if (filtstring != "" && rowfilter != "")
                            {
                                filtstring += " AND ";
                                if (rowfilter.Contains(" OR "))
                                    rowfilter = "(" + rowfilter + ")";
                            }
                            view.RowFilter = filtstring + rowfilter;
                        }
                        if (view.Count == 0)
                            retert = false;
                    }
                }
            }
    
            if (retert)
                ViewSorindex = 0;
            else
                ViewSorindex = -1;
            IdErtekBeallitasok(tabinfo.AktualRow, OsszesBeallitottIdErtek);
            return retert;
        }
        /// <summary>
        /// Altalanos listazashoz aktualis sor rowfiltere
        /// </summary>
        public string GetSorRowFilter(int aktindex)
        {
            if (SorRowFilterek.Count == 0)
                return "";
            return SorRowFilterek[aktindex].ToString();
        }
        /// <summary>
        /// Altalanos listazashoz aktualis oszlop rowfiltere
        /// </summary>
        public string GetOszlopRowFilter(int aktindex)
        {
            if (OszlopRowFilterek.Count == 0)
                return "";
            return OszlopRowFilterek[aktindex].ToString();
        }
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        
        public void SorrendInfok(Mezoinfo mezoinfo, MezoinfoCollection mezocoll)
        {
            Cols columninfo = mezoinfo.ColumnInfo;
            Tablainfo tabinfo = columninfo.Tablainfo;
            if (mezocoll.Tablainfok.IndexOf(tabinfo) == -1)
            {
                sorrendazonositok.Clear();
                sorrendtartalmak.Clear();
                sorrendtartalomoszlopnevek.Clear();
                csakosszegsorba.Clear();
                sorrendmezoinfok.Clear();
            }
            if (mezoinfo.SorrendSorszam != "0")
            {
                csakosszegsorba.Add(mezoinfo.CsakOsszegsorba);
                sorrendazonositok.Add(columninfo.ColumnName);
                sorrendtartalomoszlopnevek.Add(mezoinfo.TartalomOszlopNev);
                sorrendtartalmak.Add("");
                sorrendmezoinfok.Add(mezoinfo);
            }
        }
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public bool Sorrendvaltozas(Mezoinfo mezoinfo)
        {
            if (ViewSorindex == -1)
            {
                if (EredetiLehetUres)
                    return true;
                else
                    return false;
            }
            DataRow aktrow = AktualViewRow;
            string tartoszlnev = mezoinfo.TartalomOszlopNev;
            string tart;
            if (mezoinfo.ColumnInfo.DataType.ToString() == "System.DateTime")
                tart = Convert.ToDateTime(aktrow[tartoszlnev].ToString()).ToShortDateString();
            else
                tart = aktrow[tartoszlnev].ToString();
            int i = sorrendmezoinfok.IndexOf(mezoinfo);
            if (sorrendtartalmak[i].ToString() != tart)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public string GetSorrendtartalom(Mezoinfo mezoinfo)
        {
            DataRow aktrow = null;
            string tart="";
            if (ViewSorindex != -1)
                aktrow = AktualViewRow;
            int i = sorrendmezoinfok.IndexOf(mezoinfo);
            if (aktrow != null)
            {
                string tartoszlnev = mezoinfo.TartalomOszlopNev;
                if (mezoinfo.ColumnInfo.DataType.ToString() == "System.DateTime")
                    tart = Convert.ToDateTime(aktrow[tartoszlnev].ToString()).ToShortDateString();
                else
                    tart = aktrow[tartoszlnev].ToString();
            }
            if (sorrendtartalmak[i].ToString() != tart)
            {
                mezoinfo.ElozoSorrendTartalom = tart;
                sorrendtartalmak[i] = tart;
                if (Convert.ToBoolean(csakosszegsorba[i].ToString()))
                    return mezoinfo.FejSzoveg+ " "+ tart;
                else
                    return "";
            }
            else
                tart = "";
            return tart;
        }
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public void OszlopInfok(Mezoinfo mezoinfo, MezoinfoCollection mezocoll)
        {
            Cols columninfo = mezoinfo.ColumnInfo;
            Tablainfo tabinfo = columninfo.Tablainfo;
            if (mezocoll.Tablainfok.IndexOf(tabinfo) == -1)
            {
                oszlopmezonevek.Clear();
                oszloptartalomnevek.Clear();
                oszlopszelessegek.Clear();
                oszlopnumericek.Clear();
                oszlopmezoinfok.Clear();
            }
            if (mezoinfo.OszlopSorszam != "0")
            {
                oszlopmezonevek.Add(columninfo.ColumnName);
                oszloptartalomnevek.Add(mezoinfo.TartalomOszlopNev);
                oszlopszelessegek.Add(mezoinfo.OszlopSzelesseg);
                oszlopnumericek.Add(columninfo.Numeric(columninfo.DataType) && !columninfo.Comboe);
                oszlopmezoinfok.Add(mezoinfo);
            }
        }
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public string GetOszlopTartalom(Mezoinfo mezoinfo)
        {
            DataRow aktrow = AktualViewRow;
            string tart = "";
            int i = oszlopmezoinfok.IndexOf(mezoinfo);
            if (aktrow != null)
            {
                string tartoszlnev = mezoinfo.TartalomOszlopNev;
                tart = aktrow[tartoszlnev].ToString();
            }
            if(tart!="")
            {
                if (mezoinfo.ColumnInfo.DataType.ToString() == "System.DateTime")
                    tart = Convert.ToDateTime(tart).ToShortDateString();
            }
            else if (!mezoinfo.ColumnInfo.Numeric(mezoinfo.ColumnInfo.DataType))
                tart = "";
            else
                tart = "0";
            mezoinfo.AktualisTartalom = tart;
            return tart;
        }
        /// <summary>
        /// szerkesztett listak/statisztikak-hoz 
        /// </summary>
        /// <param name="elsotabinfoid"></param>
        /// <param name="elsotablainfo"></param>
        /// <param name="tabinfok"></param>
        /// <param name="osszesbeallitottid"></param>
        /// <param name="osszesbeallitottertek"></param>
        /// <returns></returns>
        public ArrayList BeallitandoIdkArray(string elsotabinfoid, Tablainfo elsotablainfo,TablainfoCollection tabinfok, ArrayList osszesbeallitottid, ArrayList osszesbeallitottertek)
        {
            string id;
            if (elsotablainfo == this)
            {
                BeallitandoIdk.Add(_identity);
                if (_firsttermparent == null)
                    id = _identity;
                else
                    id = _firsttermparent.IdentityColumnName;
                BeallitandoIdk.Add(id);
                elsotabinfoid = id;
                foreach(string beallid in BeallitandoIdk)
                {
                    int i = osszesbeallitottid.IndexOf(beallid);
                    if (i == -1)
                    {
                        i = osszesbeallitottid.Add(beallid);
                        osszesbeallitottertek.Add(-1);
                    }
                    BeallitottIndexek.Add(i);
                }
            }
            else
                SzuksegesIdkArray(elsotablainfo, elsotabinfoid, tabinfok, osszesbeallitottid);
            foreach(Tablainfo chaininfo in _termparentchain)
            {
                id = chaininfo.IdentityColumnName;
                if (id != elsotabinfoid )
                {
                    int j = osszesbeallitottid.IndexOf(id);
                    if (j == -1)
                    {
                        BeallitandoIdk.Add(id);
                        j = osszesbeallitottid.Add(id);
                        osszesbeallitottertek.Add(-1);
                        BeallitottIndexek.Add(j);
                    }
                }
            }
            if (elsotablainfo != this)
            {
                id = _identity;
                BeallitandoIdk.Add(id);
                int k = osszesbeallitottid.IndexOf(id);
                if (k != -1)
                    BeallitottIndexek.Add(k);
                else 
                {
                    k= osszesbeallitottid.Add(id);
                    BeallitottIndexek.Add(k);
                    osszesbeallitottertek.Add(-1);
                }
            }

            OsszesBeallitandoId = osszesbeallitottid;
            OsszesBeallitottIdErtek = osszesbeallitottertek;
            return osszesbeallitottid;
        }
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public ArrayList SzuksegesIdkArray(Tablainfo elsotablainfo, string elsotablaid, TablainfoCollection tabinfok, ArrayList osszesbeallitottid)
        {
            if (this.TablaColumns.IndexOf(elsotablaid) != -1)
            {
                SzuksegesIdk.Add(elsotablaid);
                SzuksegesIndexek.Add(osszesbeallitottid.IndexOf(elsotablaid));
            }
            foreach(Tablainfo chaininfo in _termparentchain)
            {
                string id = chaininfo.IdentityColumnName;
                if(id!=elsotablaid)
                {
                    int j = osszesbeallitottid.IndexOf(id);
                    if (j != -1)
                    {
                        SzuksegesIndexek.Add(j);
                        SzuksegesIdk.Add(id);
                    }
                }
            }
            for (int i = 0; i < SzuksegesIdk.Count; i++)
                ElozoBeallitottIdertekek.Add("-1");
            return SzuksegesIdk;
        }
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public long[] BeallitottErtekek
        {
            get { return (long[])BeallitottIdertekek.ToArray(typeof(long)); }
            set
            {
                for (int i = 0; i < value.Length; i++)
                {
                    BeallitottIdertekek[i] = value[i];
                }
            }
        }
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public void IdErtekBeallitasok(DataRow dr, ArrayList osszesbeallitott)
        {
            for (int i = 0; i < BeallitandoIdk.Count; i++)
            {
                string id = BeallitandoIdk[i].ToString();
                string tart;
                if (dr != null)
                    tart = dr[id].ToString();
                else
                    tart = "-1";
                osszesbeallitott[Convert.ToInt32(BeallitottIndexek[i].ToString())] = tart;
                OsszesBeallitottIdErtek = osszesbeallitott;
            }
        }
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public string GetRowFilterResz(ArrayList osszesbeallitott)
        {
            string forselect = "";
            string id;
            for (int i = 0; i < SzuksegesIdk.Count; i++)
            {
                id = SzuksegesIdk[i].ToString();
                int idind = OsszesBeallitandoId.IndexOf(id);
                string tart = osszesbeallitott[idind].ToString();
                if (forselect != "")
                    forselect += " AND ";
                forselect += id + " = " + tart;
            }
            return forselect;
        }
        /// <summary>
        /// Altalanos listazashoz
        /// </summary>
        public string GetTablainfoSelect(ArrayList osszesbeallitott)
        {
            string forselect = GetRowFilterResz(osszesbeallitott);
            string usersel = "";
            if (UserSelect != "")
                usersel = " (" + UserSelect + ") ";
            if (TablainfoSelect != "")
            {
                if(usersel!="")
                    usersel += "AND ";
                usersel += " (" + TablainfoSelect + ") ";
            }
            if(DatumString!="")
            {
                if(usersel!="")
                    usersel+=" AND ";
                usersel+=DatumString;
            }
            if (forselect != "")
            {
                if (usersel != "")
                    usersel += " AND ";
                //if (DatumString != "")
                //{
                //    if (forselect != "")
                //        forselect += " AND ";
                //    forselect += DatumString;
//                }
                usersel += forselect;
            }
                //    tabsel += " AND (" + forselect + ")";
                ////tabsel += TablainfoSelect;
                ////if (forselect != "")
                ////    tabsel += ")";
            if (usersel != "")
                return " where " + usersel;
            else
                return "";
        }
        //public string VerzioBeallit()
        //{
        //    string verz = _aktverzioid.ToString();
        //    if (_azonositok._base._kellverzio)
        //    {
        //        if (_verzioterkeparr.Count != 0)
        //        {
        //            if (Convert.ToInt32(verz) >= Convert.ToInt32(_verzioterkeparr[_verzioterkeparr.Count - 1].ToString()))
        //                verz = _verzioterkeparr[_verzioterkeparr.Count - 1].ToString();
        //            else
        //            {
        //                Verzioinfok verinf = _azonositok._base._verzioinfok;
        //                int verindex = -1;
        //                for (int i = 0; i < verinf.VersionArray.Length; i++) //_tablainfo.VerzioInfok.VersionArray.Length; i++)
        //                {
        //                    if (verinf.VersionArray[i].ToString() == verz)
        //                    {
        //                        verindex = i;
        //                        break;
        //                    }
        //                }
        //                for (int i = verindex; i == 0; i--)
        //                {
        //                    string egyver = verinf.VersionArray[i].ToString();
        //                    for (int j = 0; j < _verzioterkeparr.Count - 1; j++)
        //                    {
        //                        if (_verzioterkeparr[j].ToString() == egyver)
        //                        {
        //                            verz = _verzioterkeparr[j].ToString();
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    _aktverzioid = Convert.ToInt32(verz);
        //    _adattabla.Select();
        //    return verz;
        //}
        /// <summary>
        /// Adattabla osszevetese a leirotablaval
        /// </summary>
        public void LeiroVizsg()
        {
            ArrayList torlendok = new ArrayList();
            ArrayList beszurandok = new ArrayList();
            bool kellvaltozas = _fak.KellValtozas;
            Cols egycol;
            bool leirolezart = _leirotablainfo.LezartVersion;
            bool leirolastlezart = _leirotablainfo.LastVersionLezart;
            int leirocolc = _leirotablainfo.TablaColumns.Count;
            int verziodb = _verzioterkeparr.Count;
            int newverid = _fak.VerzioInfok[0].LastVersionId;
            DataRow oldrow;
            DataRow NewRow = null;
            Leirovizsg(torlendok, beszurandok);
            if (torlendok.Count != 0 || beszurandok.Count != 0)
            {
                if (_fak.Alkalmazas != "TERVEZO")
                {
                    _leirorendben = false;
                    _leirohibaszov = _fak.Cegconnectionok[_fak.AktualCegIndex].ToString() + " " + Tablanev + " struktúrája nem lett aktualizálva!";
                }
                else if (torlendok.Count != 0 && verziodb > 1 && _leirotablainfo.Azon != "LEIR")
                {
                    _leirorendben = false;
                    _leirohibaszov = " Uj verzioban mar nem torolheto column";
                }
                else if (leirolastlezart)
                    _leirorendben = false;
                if (!_leirorendben)
                    _fak.BajTablaInfo = this;
                else
                {
                    if (torlendok.Count != 0)
                    {
                        for (int i = 0; i < _leirotablainfo.DataView.Count; i++)
                        {
                            oldrow = _leirotablainfo.DataView[i].Row;
                            string oldadatnev = oldrow["ADATNEV"].ToString();
                            if (torlendok.IndexOf(oldadatnev) != -1)
                            {
                                _leirotablainfo.Adatsortorol(i);
                                i = -1;
                            }
                        }
                    }
                    if (beszurandok.Count != 0)
                        _fak.ValtoztatasFunkcio = "ADD";
                    string adatfajta = _leirotablainfo.TablaTag.Tablainfo.Adatfajta;
                    string azon = _leirotablainfo.TablaTag.Tablainfo.Azon;
                    bool term = adatfajta == "T";
                    foreach (string adatnev in beszurandok)
                    {
                        NewRow = _leirotablainfo.Ujsor();
                        NewRow["ADATNEV"] = adatnev;
                        if (_leirotablainfo.TablaTag.Tablainfo.Tablanev == "TARTAL" && (adatfajta == "T" || azon == "SZRM"))
                        {
                            switch (adatnev)
                            {
                                case "COMBOFILEBA":
                                    NewRow["SORSZOV"] = "Combo fileba adatneve";
                                    NewRow["OSZLSZOV"] = "CombfilAdatnév";
                                    if (term)
                                        NewRow["LATHATO"] = "I";
                                    NewRow["READONLY"] = "N";
                                    NewRow["KELLMEZOELLENORZES"] = "I";
                                    NewRow["TOOLTIP"] = "Melyik adat kerüljön rögzitésre ?";
                                    break;
                                case "COMBOSZOVEGBE":
                                    NewRow["SORSZOV"] = "Combo szövegbe adatneve";
                                    NewRow["OSZLSZOV"] = "CombszövAdatnév";
                                    if (term)
                                        NewRow["LATHATO"] = "I";
                                    NewRow["READONLY"] = "N";
                                    NewRow["KELLMEZOELLENORZES"] = "I";
                                    NewRow["TOOLTIP"] = "Melyik adat (vagy adatok) jelenjen(ek) meg a Combo-ban?";
                                    break;
                                case "COMBOSORT":
                                    NewRow["SORSZOV"] = "Combo sort";
                                    NewRow["OSZLSZOV"] = "ComboSort";
                                    if (term)
                                        NewRow["LATHATO"] = "I";
                                    NewRow["READONLY"] = "N";
                                    NewRow["KELLMEZOELLENORZES"] = "I";
                                    NewRow["TOOLTIP"] = "Milyen sorrendben jelenjenek meg a sorok a Combo-ban?";
                                    break;
                                case "ENYEM":
                                    NewRow["DEFERT"] = "N";
                                    NewRow["COMBOAZONTIP"] = "SZRK9997";
                                    break;
                                case "LEIROENYEM":
                                    NewRow["DEFERT"]="N";
                                    NewRow["COMBOAZONTIP"] = "SZRK9997";
                                    break;
                                case "BESZURHAT":
                                    NewRow["SORSZOV"] = "Beszúrhat/Törölhet";
                                    NewRow["OSZLSZOV"] = "Beszúr/Töröl";
                                    if (term)
                                        NewRow["LATHATO"] = "I";
                                    NewRow["LEHETURES"] = "N";
                                    NewRow["READONLY"] = "N";
                                    NewRow["DEFERT"] = "I";
                                    NewRow["COMBOAZONTIP"] = "SZRK9997";
                                    NewRow["TOOLTIP"] = "Felvehetö-e új sor?/Törölhetök-e sorok?";
                                    break;
                                case "MODOSITHAT":
                                    NewRow["SORSZOV"] = "Módosithat";
                                    NewRow["OSZLSZOV"] = "Módosithat";
                                    if (term)
                                        NewRow["LATHATO"] = "I";
                                    NewRow["LEHETURES"] = "N";
                                    NewRow["READONLY"] = "N";
                                    NewRow["DEFERT"] = "I";
                                    NewRow["COMBOAZONTIP"] = "SZRK9997";
                                    NewRow["TOOLTIP"] = "Módositható tábla?";
                                    break;
                                case "FEJLESZTO":
                                    NewRow["SORSZOV"] = "Fejlesztö jogosultsága";
                                    NewRow["OSZLSZOV"] = "Fejlesztö jog.";
                                    NewRow["LATHATO"] = "I";
                                    NewRow["LEHETURES"] = "N";
                                    NewRow["READONLY"] = "N";
                                    NewRow["DEFERT"] = "0";
                                    NewRow["COMBOAZONTIP"] = "SZRKJogfajta";
                                    NewRow["TOOLTIP"] = "A fejlesztö hozzáférési jogosultsága";
                                    break;
                                case "CEGMINDEN":
                                    NewRow["SORSZOV"] = "Egyedüli cégkezelö";
                                    NewRow["OSZLSZOV"] = "Egyedüli cégkez";
                                    NewRow["LATHATO"] = "I";
                                    NewRow["READONLY"] = "N";
                                    NewRow["LEHETURES"] = "N";
                                    NewRow["DEFERT"] = "0";
                                    NewRow["COMBOAZONTIP"] = "SZRKJogfajta";
                                    NewRow["TOOLTIP"] = "";
                                    break;

                                case "KEZELO":
                                    NewRow["SORSZOV"] = "Kezelö jogosultsága";
                                    NewRow["OSZLSZOV"] = "Kezelö jog.";
                                    NewRow["LATHATO"] = "I";
                                    NewRow["READONLY"] = "N";
                                    NewRow["LEHETURES"] = "N";
                                    NewRow["DEFERT"] = "0";
                                    NewRow["COMBOAZONTIP"] = "SZRKJogfajta";
                                    NewRow["TOOLTIP"] = "Ha az alkalmazásban több kezelö is definiált, kezelö az,\n   aki cégszinten illetve cégszinten belül egy definiált csoport\n   adatait beviheti és sem rendszergazdaként\n   sem az adott cég kiemelt kezelöjeként nincs definiálva";
                                    break;
                                case "KIEMELTKEZELO":
                                    NewRow["SORSZOV"] = "Kiemelt kezelö jogosultsága";
                                    NewRow["OSZLSZOV"] = "Kiem.kez.jog.";
                                    NewRow["LATHATO"] = "I";
                                    NewRow["READONLY"] = "N";
                                    NewRow["LEHETURES"] = "N";
                                    NewRow["DEFERT"] = "1";
                                    NewRow["COMBOAZONTIP"] = "SZRKJogfajta";
                                    NewRow["TOOLTIP"] = "Ha az alkalmazásban egy céghez több kezelö is definiált,\n   kiemelt kezelö az, aki a cég kiemelt kezelöjeként definiált\n  és sem rendszergazdaként sem a cég kezelöjeként nem definiált\n  Adatbevitelre nem jogosult, jogosult viszont a cég egészére vonatkozó\n  olyan feladatok elvégzésére, melyeket a kezelö nem tehet meg";
                                    break;
                                case "KIEMELTPLKEZELO":
                                    NewRow["SORSZOV"] = "Kiemelt kez.+kezelö jogosultsága";
                                    NewRow["OSZLSZOV"] = "Kiem.+kez. jog.";
                                    NewRow["LATHATO"] = "I";
                                    NewRow["READONLY"] = "N";
                                    NewRow["LEHETURES"] = "N";
                                    NewRow["DEFERT"] = "1";
                                    NewRow["COMBOAZONTIP"] = "SZRKJogfajta";
                                    NewRow["TOOLTIP"] = "Kiemelt kezelö + kezelö:\n       kiemelt kezelöként is és ahol kezelöként definiált,\n     ott kezelöként is dolgozhat";
                                    break;
                                case "LEHETCOMBO":
                                    NewRow["SORSZOV"] = "Lehet Combo?";
                                    NewRow["OSZLSZOV"] = "Lehet Combo?";
                                    NewRow["LATHATO"] = "I";
                                    NewRow["READONLY"] = "N";
                                    NewRow["LEHETURES"] = "N";
                                    NewRow["DEFERT"] = "N";
                                    NewRow["COMBOAZONTIP"] = "SZRK9997";
                                    NewRow["TOOLTIP"] = "A definiált tábla sorai szerepelhetnek-e Comboban?";
                                    break;
                                case "LEHETURES":
                                    NewRow["SORSZOV"] = "Lehet üres?";
                                    NewRow["OSZLSZOV"] = "Lehet üres?";
                                    NewRow["LATHATO"] = "I";
                                    NewRow["READONLY"] = "N";
                                    NewRow["LEHETURES"] = "N";
                                    NewRow["DEFERT"] = "N";
                                    NewRow["COMBOAZONTIP"] = "SZRK9997";
                                    NewRow["TOOLTIP"] = "A definiált természetes tábla adatbevitelkor hagyható-e üresen";
                                    break;
                                case "MINDEN":
                                    NewRow["SORSZOV"] = "Kizárólagos kezelö jogosultsága";
                                    NewRow["OSZLSZOV"] = "Kiz.kez.jog.";
                                    NewRow["LATHATO"] = "I";
                                    NewRow["READONLY"] = "N";
                                    NewRow["LEHETURES"] = "N";
                                    NewRow["DEFERT"] = "0";
                                    NewRow["COMBOAZONTIP"] = "SZRKJogfajta";
                                    NewRow["TOOLTIP"] = "Kizárólagos kezelö azt jelenti,\n hogy az alkalmazásban csak egy kezelö definiált";
                                    break;
                                case "OWNER":
                                    NewRow["SORSZOV"] = "Owner alkalmazás";
                                    NewRow["OSZLSZOV"] = "Owner alkalm.";
                                    NewRow["LATHATO"] = "I";
                                    NewRow["READONLY"] = "N";
                                    NewRow["LEHETURES"] = "N";
                                    NewRow["COMBOAZONTIP"] = "SZRKAlkalm";
                                    NewRow["TOOLTIP"] = "A definiálandó tábla tulajdonosa\n    csak az owner módosithat a táblában, ha megfelelö a hozzáférési jogosultsága\nHa üres, az azt jelenti, hogy csak a Tervezö módosithatja";
                                    break;
                                case "RENDSZERGAZDA":
                                    NewRow["SORSZOV"] = "Rendszergazda jogosultsága";
                                    NewRow["OSZLSZOV"] = "Rendszerg.jog.";
                                    NewRow["LATHATO"] = "I";
                                    NewRow["READONLY"] = "N";
                                    NewRow["LEHETURES"] = "N";
                                    NewRow["DEFERT"] = "1";
                                    NewRow["COMBOAZONTIP"] = "SZRKJogfajta";
                                    NewRow["TOOLTIP"] = "Ha az alkalmazásban több kezelö is definiált, rendszergazda az a kezelö,\n   aki rendszergazdaként definiált és cégszinten nem";
                                    break;
                                case "RENDSZERPLKEZELO":
                                    NewRow["SORSZOV"] = "Rendszergazda+kezelö jogosultsága";
                                    NewRow["OSZLSZOV"] = "Rgazd+ kez.jog";
                                    NewRow["LATHATO"] = "I";
                                    NewRow["READONLY"] = "N";
                                    NewRow["LEHETURES"] = "N";
                                    NewRow["DEFERT"] = "0";
                                    NewRow["COMBOAZONTIP"] = "SZRKJogfajta";
                                    NewRow["TOOLTIP"] = "Rendszergazda+kezelö az a kezelö, aki rendszergazdaként és\n   cégszinten kezelöként is definiált";
                                    break;
                                case "RENDSZERPLKIEM":
                                    NewRow["SORSZOV"] = "Rendszergazda+kiem.kez.jogosultsága";
                                    NewRow["OSZLSZOV"] = "Rgazd+kiem.jog.";
                                    NewRow["LATHATO"] = "I";
                                    NewRow["READONLY"] = "N";
                                    NewRow["LEHETURES"] = "N";
                                    NewRow["DEFERT"] = "1";
                                    NewRow["COMBOAZONTIP"] = "SZRKJogfajta";
                                    NewRow["TOOLTIP"] = "Rendszergazda+kiemelt kezelö az a kezelö,\n aki rendszergazdaként és cégszinten kiemelt kezelöként is definiált";
                                    break;
                                case "SELORD":
                                    NewRow["SORSZOV"] = "Order By ...";
                                    NewRow["OSZLSZOV"] = "Order By";
                                    NewRow["LATHATO"] = "I";
                                    NewRow["READONLY"] = "N";
                                    NewRow["KELLMEZOELLENORZES"] = "I";
                                    NewRow["TOOLTIP"] = "Táblák betöltésénél a betöltési sorrend";
                                    break;
                                case "SORT":
                                    NewRow["SORSZOV"] = "DataView Sort";
                                    NewRow["OSZLSZOV"] = "DataView Sort";
                                    NewRow["LATHATO"] = "I";
                                    NewRow["READONLY"] = "N";
                                    NewRow["LEHETURES"] = "N";
                                    NewRow["KELLMEZOELLENORZES"] = "I";
                                    NewRow["TOOLTIP"] = "DataView-ban a kivánt sorrend";
                                    break;
                                case "SZOVEG":
                                    NewRow["SORSZOV"] = "Megnevezés";
                                    NewRow["OSZLSZOV"] = "Megnevezés";
                                    NewRow["LATHATO"] = "I";
                                    NewRow["READONLY"] = "N";
                                    NewRow["LEHETURES"] = "N";
                                    NewRow["ISUNIQUE"] = "I";
                                    NewRow["TOOLTIP"] = "Ezen a néven jelenik meg a TreeView-ban";
                                    break;
                                case "SZULOSZINT":
                                    NewRow["SORSZOV"] = "Szülötábla szintje";
                                    NewRow["OSZLSZOV"] = "Szülötab. szint";
                                    NewRow["LATHATO"] = "N";
                                    NewRow["READONLY"] = "I";
                                    NewRow["TOOLTIP"] = "A szülötábla szintje";
                                    break;
                                case "SZULOTABLA":
                                    NewRow["SORSZOV"] = "Szülötábla neve";
                                    NewRow["OSZLSZOV"] = "Szülötábla neve";
                                    NewRow["LATHATO"] = "I";
                                    NewRow["READONLY"] = "N";
                                    NewRow["KELLSELECT"] = "N";
                                    NewRow["COMBOAZONTIP"] = "BASEBASE";
                                    NewRow["TOOLTIP"] = "A szülötábla neve";
                                    break;
                                case "TABLANEV":
                                    NewRow["SORSZOV"] = "Tábla neve";
                                    NewRow["OSZLSZOV"] = "Táblanév";
                                    NewRow["LATHATO"] = "I";
                                    NewRow["READONLY"] = "N";
                                    NewRow["LEHETURES"] = "N";
                                    NewRow["KELLSELECT"] = "N";
                                    NewRow["COMBOAZONTIP"] = "BASEBASE";
                                    NewRow["TOOLTIP"] = "A definiálni kivánt tábla neve az adatbázisban";
                                    break;
                                case "TOROLHETO":
                                    NewRow["DEFERT"] = "I";
                                    NewRow["COMBOAZONTIP"] = "SZRK9997";
                                    NewRow["LATHATO"] = "N";
                                    break;
                                case "USEREK":
                                    NewRow["SORSZOV"] = "User alkalmazások";
                                    NewRow["OSZLSZOV"] = "User alkalm.-ok";
                                    NewRow["LATHATO"] = "I";
                                    NewRow["READONLY"] = "N";
                                    NewRow["TOOLTIP"] = "Több alkalmazás esetén azon,\n az Owner alkalmazástól eltérö alkalmazásnevek,\n  melyek használják a definiált táblát\nHa az Owner alkalmazás üres, User alkalmazásokat csak akkor kell  felsorolni,\n   ha ezzel valamely alkalmazás(oka)t ki akarunk zárni";
                                    break;
                                case "VEZETO":
                                    NewRow["SORSZOV"] = "Vezetö jogosultsága";
                                    NewRow["OSZLSZOV"] = "Vezetö jog.";
                                    NewRow["LATHATO"] = "I";
                                    NewRow["READONLY"] = "N";
                                    NewRow["LEHETURES"] = "N";
                                    NewRow["DEFERT"] = "1";
                                    NewRow["COMBOAZONTIP"] = "SZRKJogfajta";
                                    NewRow["TOOLTIP"] = "Ha egy cég használatára több kezelö is jogosult,\n   annak lesz vezetöi jogosultsága, aki az adott cégnél\n   kizárólag Vezetöként szerepel";
                                    break;
                            }
                        }
                        if (leirolezart)
                            NewRow["INPUTLATHATO"] = "N";
                        else
                            NewRow["INPUTLATHATO"] = "I";
                        _leirotablainfo.ValtozasNaplozas(NewRow);
                    }
                    if (leirolezart)
                    {
                        string colname = "";
                        string savsort = _leirotablainfo.DataView.Sort;
                        _leirotablainfo.DataView.Sort = "";
                        int oldcount = _leirotablainfo.DataView.Count;
                        _leirotablainfo.NewVersionCreated = true;
                        _leirotablainfo.Adattabla.LastSel = _leirotablainfo.SelectString + " and VERZIO_ID=" + newverid.ToString();
                        _leirotablainfo.VerzioTerkepArray.Add(newverid);
                        _leirotablainfo.AktVerzioId = newverid;
                        for (int i = 0; i < oldcount; i++)
                        {
                            oldrow = _leirotablainfo.DataView[i].Row;
                            NewRow = _leirotablainfo.Ujsor();
                            for (int j = 0; j < leirocolc; j++)
                            {
                                egycol = _leirotablainfo.TablaColumns[j];
                                colname = egycol.ColumnName;
                                if (!egycol.IsIdentity)
                                {
                                    switch (colname)
                                    {
                                        case "VERZIO_ID":
                                            break;
                                        case "PREV_ID":
                                            break;
                                        case "BESZUR_VERZIO_ID":
                                            break;
                                        case "MODOSITOTT":
                                            break;
                                        case "INPUTLATHATO":
                                            NewRow[j] = "I";
                                            break;
                                        default:
                                            NewRow[j] = oldrow[j];
                                            break;
                                    }
                                }
                            }
                            _leirotablainfo.ValtozasNaplozas(NewRow);
                        }
                        _leirotablainfo.DataView.Sort = savsort;
                    }
                    if (kellvaltozas)
                        _fak.KellValtozas = true;
                    Tablainfo[] tabinfok;
                    if (_leirotablainfo.Azon != "LEIR")
                        tabinfok = new Tablainfo[] { _leirotablainfo };
                    else
                        tabinfok = new Tablainfo[] { this };
                    _fak.UpdateTransaction(tabinfok);
                }
            }
        }
        private void Leirovizsg(ArrayList torlendok, ArrayList beszurandok)
        {
            string adatnev;
            DataRow dr;
            Cols egycol;
            Tablainfo adattablainfo = this;
            Tablainfo leirotablainfo = LeiroTablainfo;
            ColCollection adattablaColumns = adattablainfo.TablaColumns;
            DataView LeiroView = leirotablainfo.DataView;
            for (int i = 0; i < LeiroView.Count; i++)
            {
                dr = LeiroView[i].Row;
                adatnev = dr["ADATNEV"].ToString().Trim();
                egycol = adattablaColumns[adatnev];
                if (egycol == null)
                    torlendok.Add(adatnev);
            }
            foreach (Cols adategycol in adattablaColumns)
            {
                bool talalt = false;
                adatnev = adategycol.ColumnName;
                for (int j = 0; j < LeiroView.Count; j++)
                {
                    dr = LeiroView[j].Row;
                    if (adatnev == dr["ADATNEV"].ToString().Trim())
                    {
                        talalt = true;
                        break;
                    }
                }
                if (!talalt)
                    beszurandok.Add(adatnev);
            }
        }
        /// <summary>
        /// Kiegeszito mezoinformaciok feltoltese adattabla egy sora alpajan
        /// </summary>
        /// <param name="dr">
        /// a kivant sor
        /// </param>
        public void KiegColumnsTolt(DataRow dr)
        {
            foreach (Cols egycol in _combocolumns)
            {
                bool ures = egycol.OrigTartalom == "" || egycol.Numeric(egycol.DataType) && egycol.OrigTartalom == "0";
                if (!ures && egycol.Comboe &&(egycol.ComboAzontipCombo != null || egycol.Combo_Info != null))
                {
                    if (egycol.ColumnName != "SORSZOV" && egycol.ColumnName != "OSZLSZOV")
                    {
                        if (Tablanev == "KIAJANL" && _comboinfok != null && dr != null && egycol.ColumnName=="RSORSZAM")
                        {
                            string azontip = dr["AZONTIP"].ToString();
                            egycol.Combo_Info = Fak.ComboInfok.ComboinfoKeres(azontip);
                            egycol.Tartalom = dr["RSORSZAM"].ToString();
                            Cols egyinp = InputColumns[egycol.ColumnName];
                            egyinp.Tartalom = egycol.Tartalom;
                            egycol.Kiegcol.Combo_Info = egycol.Combo_Info;
                        }
                        egycol.Kiegcol.Tartalom = egycol.OrigTartalom;
                        int maxhossz = 0;
                        if (egycol.ComboAzontipCombo != null)
                        {
                            egycol.ComboAzontipCombo.SetComboAktszoveg(egycol.Kiegcol);
                            maxhossz = egycol.ComboAzontipCombo.Maxhossz;
                        }
                        else
                        {
                            egycol.Combo_Info.SetComboAktszoveg(egycol.Kiegcol);
                            maxhossz = egycol.Combo_Info.Maxhossz;
                        }
                        egycol.ComboAktSzoveg = egycol.Kiegcol.ComboAktSzoveg;
                        if (egycol.Kiegcol.MaxLength < maxhossz)
                            egycol.Kiegcol.MaxLength = maxhossz;
                        dr[egycol.Kiegcol.ColumnName] = egycol.ComboAktSzoveg;
                        //if (maxhossz > egycol.Kiegcol.MaxLength)
                        //    egycol.Kiegcol.MaxLength = maxhossz;
                    }
                }
                else if (egycol.Kiegcol != null)
                {
                    dr[egycol.Kiegcol.ColumnName] = "";
                    egycol.ComboAktSzoveg = "";
                }
            }
        }
        /// <summary>
        /// _inputtable inicializalasa
        /// </summary>
        private void Inputtablaini()
        {
            DataRow NewRow;
            Comboinfok egyinfo = _fak.ComboInfok.ComboinfoKeres("SZRK9999");
            if (egyinfo != null)
                _szovegmaxlength = egyinfo.Maxhossz;
            else
                _szovegmaxlength = 50;
            int tarthossz = 0;
            int j = 0;
            if (_inputtable == null)
                _inputtable = new AdatTabla("INPTABLE");
            _inputtable.Clear();
            _inputtable.Columns.Clear();
            _inputtable.Tablainfo = this;
            foreach (Cols egycol in _inputcolumns)
            {
                j = egycol.InputMaxLength;
                if (j > tarthossz)
                    tarthossz = j;
            }
            if (tarthossz <= 50)
                _tartalommaxlength = tarthossz;
            else
                _tartalommaxlength = 50;
            _inputtable.Columns.Add(Ujcol("SZOVEG", "System.String", "Megnevezés", true, _szovegmaxlength));
            _inputtable.Columns.Add(Ujcol("TARTALOM", "System.String", "Tartalom", false, _tartalommaxlength));
            foreach (Cols egycol in _inputcolumns)
            {
                NewRow = _inputtable.NewRow();
                NewRow[0] = egycol.Sorszov;
                NewRow[1] = egycol.DefaultValue;
                _inputtable.Rows.Add(NewRow);
            }
            if (_inputtable.GridView != null)
            {
                _inputtable.GridView.Columns[0].Width = _szovegmaxlength * 9;
                _inputtable.GridView.Columns[1].Width = _tartalommaxlength * 9;
            }

        }
        private void Osszefinfoini()
        {
            if (_osszefinfo == null)
                _osszefinfo = new Osszefinfo(this);
            else
                _osszefinfo.InitKell = true;
        }
        private DataColumn Ujcol(string ColumnName, string DataType, string MappingName, bool ReadOnly, int MaxLength)
        {
            DataColumn col = new DataColumn();
            col.ColumnName = ColumnName;
            col.DataType = System.Type.GetType(DataType);
            col.Caption = MappingName;
            if (DataType == "System.String")
                col.MaxLength = MaxLength;
            col.ReadOnly = ReadOnly;
            return col;
        }
        /// <summary>
        /// TextColumn letrehozasa a DataGridView-hoz
        /// </summary>
        /// <param name="egycol">
        /// mezoinformacio objectum
        /// </param>
        /// <param name="Readonly">
        /// true: a Column ReadOnly
        /// </param>
        /// <returns></returns>
        public DataGridViewColumn Ujtextcolumn(Cols egycol, bool Readonly)
        {
            DataGridViewTextBoxColumn textcol = new DataGridViewTextBoxColumn();
            textcol.DataPropertyName = egycol.ColumnName;
            textcol.Name = egycol.ColumnName;
            textcol.HeaderText = egycol.Caption;
            if (egycol.ToolTip != "")
                textcol.ToolTipText = egycol.ToolTip;
            else if (egycol.Sorszov != "")
                textcol.ToolTipText = egycol.Sorszov;
            else
                textcol.ToolTipText = egycol.Caption;
            int i = egycol.Caption.Length;
            int j = egycol.InputMaxLength;
            if (i < j)
                i = j;
            if (i > 20)
                i = 20;
            textcol.MinimumWidth = egycol.Caption.Length * 9;
            textcol.Width = i * 9;
            textcol.ReadOnly = Readonly;
            textcol.SortMode = DataGridViewColumnSortMode.NotSortable;
            textcol.DefaultCellStyle = new DataGridViewCellStyle();
            if (egycol.Numeric(egycol.DataType))
            {
                textcol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                textcol.DefaultCellStyle.BackColor = _fak.InaktivInputBackColor;
                textcol.DefaultCellStyle.Font = _fak.InaktivInputFont;
                if (egycol.Format != "")
                    textcol.DefaultCellStyle.Format = egycol.Format;
            }
            else
            {
                textcol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                textcol.DefaultCellStyle.BackColor = _fak.InaktivInputBackColor;
                textcol.DefaultCellStyle.Font = _fak.InaktivInputFont;
            }
            return (DataGridViewColumn)textcol;
        }
        public DataGridViewColumn UjImagecolumn(Cols egycol, bool ReadOnly)
        {
            DataGridViewImageColumn textcol = new DataGridViewImageColumn(false);
 //           textcol.DataPropertyName = egycol.ColumnName;
            textcol.Name = egycol.ColumnName;
            textcol.ReadOnly = ReadOnly;
            textcol.ImageLayout = DataGridViewImageCellLayout.Normal;
            textcol.Description = "Normal";
            return textcol;
        }
        /// <summary>
        /// TextColumn letrehozasa DataGridView-hoz
        /// </summary>
        /// <param name="propname">
        /// DataPropertyName
        /// </param>
        /// <param name="text">
        /// Ez legyen a HeaderText
        /// </param>
        /// <param name="Readonly">
        /// true: a Column ReadOnly
        /// </param>
        /// <returns></returns>

        public DataGridViewColumn Ujtextcolumn(string propname, string text, bool Readonly)
        {
            DataGridViewTextBoxColumn textcol = new DataGridViewTextBoxColumn();
            textcol.DataPropertyName = propname;
            textcol.Name = propname;
            textcol.HeaderText = text;
            textcol.ReadOnly = Readonly;
            textcol.SortMode = DataGridViewColumnSortMode.NotSortable;
            return (DataGridViewColumn)textcol;

        }
        public DataGridViewColumn UjCheckboxcolumn(Cols egycol, bool Readonly)
        {
            DataGridViewCheckBoxColumn ccol = new DataGridViewCheckBoxColumn();
            ccol.DataPropertyName = egycol.ColumnName;
            ccol.HeaderText = egycol.Caption;
            if (egycol.ToolTip != "")
                ccol.ToolTipText = egycol.ToolTip;
            ccol.Name = egycol.ColumnName;
            ccol.ReadOnly = Readonly;
            ccol.TrueValue = egycol.Checkyes;
            ccol.FalseValue = egycol.Checkno;
            ccol.SortMode = DataGridViewColumnSortMode.NotSortable;
            ccol.DefaultCellStyle = new DataGridViewCellStyle();
            ccol.DefaultCellStyle.BackColor = _fak.InaktivInputBackColor;
            ccol.DefaultCellStyle.Font = _fak.InaktivInputFont;
            ccol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            return (DataGridViewColumn)ccol;

        }
        private DataGridViewColumn UjComboboxcolumn(Cols egycol)
        {
            Cols kiegcol = egycol.Kiegcol;
            DataGridViewComboBoxColumn combocol = new DataGridViewComboBoxColumn();
            combocol.DataPropertyName = kiegcol.ColumnName;
            combocol.HeaderText = kiegcol.Caption;
            if (egycol.ToolTip != "")
                combocol.ToolTipText = egycol.ToolTip;
            combocol.Name = kiegcol.ColumnName;
            combocol.ReadOnly = false;
            combocol.Items.AddRange(egycol.Combo_Info.Szovegbe);
            combocol.MinimumWidth = egycol.Combo_Info.Minhossz * 9;
            combocol.Width = egycol.Combo_Info.Maxhossz * 9;
            combocol.SortMode = DataGridViewColumnSortMode.NotSortable;
            combocol.DefaultCellStyle = new DataGridViewCellStyle();
            combocol.DefaultCellStyle.BackColor = _fak.InaktivInputBackColor;
            combocol.DefaultCellStyle.Font = _fak.InaktivInputFont;
            return combocol;
        }
        /// <summary>
        /// mezoinformaciok alapjan gridview column-ok tombjet allitja elo
        /// </summary>
        /// <param name="Readonly"></param>
        /// <returns></returns>
        public DataGridViewColumn[] CreateGridViewColumns(bool Readonly)
        {
            ArrayList gridcols = new ArrayList();
            Cols kiegcol;
            foreach (Cols egycol in _tablacolumns)
            {
                kiegcol = egycol.Kiegcol;
                if (kiegcol != null)
                {
                    if (kiegcol.Lathato)
                    {
                        if (egycol.Comboe)
                        {
                            if (Readonly)
                                gridcols.Add(Ujtextcolumn(kiegcol, true));
                            else
                                gridcols.Add(UjComboboxcolumn(egycol));
                        }
                    }
                }
                else
                {
                    if (egycol.Checkboxe)
                        gridcols.Add(UjCheckboxcolumn(egycol, true));
                    else if (egycol.ColumnName == "KEP")
                        gridcols.Add(UjImagecolumn(egycol, true));
                    else if (egycol.Lathato)
                    {
                        int j = gridcols.Add(Ujtextcolumn(egycol, Readonly));
                        if (egycol.DataType.ToString() == "System.DateTime")
                        {
                            string datt = "";
                            if (egycol.ColumnName == "LAST_MOD")
                                datt = _fak.Maxdatum.ToString();
                            else
                                datt = _fak.Maxdatum.ToShortDateString();
                            ((DataGridViewTextBoxColumn)gridcols[j]).Width = datt.Length * 9;
                            ((DataGridViewTextBoxColumn)gridcols[j]).MinimumWidth = egycol.Column.Caption.Length * 9;
                        }
                    }
                }
            }
            return (DataGridViewColumn[])gridcols.ToArray(typeof(DataGridViewColumn));
        }
        /// <summary>
        /// TERVEZO hibavizsgalat resze. Letezik-e valamelyik adatbazisban adott nevu tabla
        /// </summary>
        /// <param name="tablanev">
        /// a keresendo tabla neve
        /// </param>
        /// <returns>
        /// true: letezik
        /// </returns>
        public DataColumnCollection Select(string tablanev)
        {
            string szint = Szint;
            string connstring = _fak.AktualCegconn;
            switch (szint)
            {
                case "R":
                    connstring = _fak.Rendszerconn;
                    break;
                case "U":
                    connstring = _fak.Userconn;
                    break;
            }
            DataTable dt = new DataTable();
            dt = Sqlinterface.Select(dt, connstring, tablanev, "", "", true);
            if (dt != null)
                return dt.Columns;
            return null;
        }
        /// <summary>
        /// Celja: visszaadni egy tabla Column-jait
        /// </summary>
        /// <param name="szint">
        /// a tabla szintje
        /// </param>
        /// <param name="tablanev">
        /// a tabla neve
        /// </param>
        /// <returns>
        /// a Column=ok vagy null
        /// </returns>
        public DataColumnCollection Select(string szint, string tablanev)
        {
            string connstring = _fak.AktualCegconn;
            switch (szint)
            {
                case "R":
                    connstring = _fak.Rendszerconn;
                    break;
                case "U":
                    connstring = _fak.Userconn;
                    break;
            }
            DataTable dt = new DataTable();
            dt = Sqlinterface.Select(dt, connstring, tablanev, "", "", true);
            if (dt != null)
                return dt.Columns;
            return null;
        }
        /// <summary>
        /// Mezoinformacio objectumok eloallitasa, az adattabla column-jainak szukseges kiegeszitese
        /// </summary>
        public void Beallit()
        {
            _sorrendcolumn = null;
            _sorrendmezo = "";
            _kiegsorrendcolumn = null;
            _kiegdatacolumns.Clear();
            _tablacolumns.Clear();
            _kiegcolumns.Clear();
            _combocolumns.Clear();
            _inputcolumns.Clear();
            _tartalommaxlength = 0;
            _szovegmaxlength = 0;
            if (_azonositok.Beszurhat == "I")
                _beszurhat = true;
            if (_azonositok.Modosithat == "I" || _beszurhat)
                _modosithat = true;
            TablainfoTag parent = _tablatag.ParentTag;
            if (_leiroe && _azonositok.Azon != "LEIR")
                _leirotablainfo = _fak.LeiroTag.LeiroTablainfo;
            _schematabla = new DataTable();
            _schematabla = Sqlinterface.GetSchemaTable(_schematabla, _azonositok.Verzioinfok.AktualConnection, _adattabla.TableName);
            if (_schematabla != null)
            {
                DataTable dt1 = _schematabla;
                Cols egycol;
                foreach (DataRow drow in dt1.Rows)
                {
                    egycol = new Cols(drow, this);
                    _tablacolumns.Add(egycol);
                }
                if (_tablacolumns.IndexOf("SORREND") != -1)
                {
                    _sorrendcolumn = _tablacolumns["SORREND"];
                    _sorrendmezo = "SORREND";
                }
                else
                    _sorrendmezo = IdentityColumnName;
                DataView leiroview = null;
                if (_azonositok.Azon == "LEIR")
                    leiroview = _adattabla.DataView;
                //else if (_leiroe)
                //    leiroview = _leirotablainfo.Adattabla.DataView;
                else if (_leirotablainfo != null)
                    leiroview = _leirotablainfo._adattabla.DataView;
//                if (_azonositok.Azon != "LEIR")
//                {
                    for (int i = _tablacolumns.Count; i < _adattabla.Columns.Count; i++)
                    {
                        _adattabla.Columns.RemoveAt(i);
                        i--;
                    }
//                }
                DataRow dr;
                if (leiroview != null)
                {
                    for (int i = 0; i < leiroview.Count; i++)
                    {
                        dr = leiroview[i].Row;
                        string adatnev = dr["ADATNEV"].ToString().Trim();
                        egycol = _tablacolumns[adatnev];
                        if (egycol != null)
                            egycol.Beallitasok(dr, _fak);
                    }
                    for (int i = 0; i < _tablacolumns.Count; i++)
                    {
                        egycol = _tablacolumns[i];
                        if (!egycol.Csakolvas)
                            _inputcolumns.Add(egycol);
                    }
                    if (_tablacolumns.IndexOf("KOD") != -1)
                    {
                        if (parent != null && parent.Tablainfo.Tablanev == "TARTAL" && parent.Tablainfo.DataView.Count != 0)
                        {
                            DataView view = parent.Tablainfo.DataView;
                            string savsort = view.Sort;
                            dr = null;
                            if (Adatfajta == "K")

                                view.Sort = "KODTIPUS";
                            else
                                view.Sort = "TABLANEV";
                            int i = view.Find(Kodtipus);
                            if (i != -1)
                                dr = view[i].Row;
                            if (dr != null)
                            {
                                _tablacolumns["KOD"].InputMaxLength = Convert.ToInt32(dr["KODHOSSZ"].ToString());
                                if (_tablacolumns.IndexOf("SZOVEG") != -1)
                                    _tablacolumns["SZOVEG"].InputMaxLength = Convert.ToInt32(dr["SZOVEGHOSSZ"].ToString());
                            }
                            view.Sort = savsort;
                        }
                    }
                    _kiegdatacolumns.Add(new DataColumn("MODOSITOTT_M", System.Type.GetType("System.Int32")));
                    if (_sorrendcolumn == null && _beszurhat)
                    {
                        _kiegsorrendcolumn = new Cols("SORREND_S", "System.Int32", "Sorrend", 6, false, this, "");
                        _kiegdatacolumns.Add(_kiegsorrendcolumn.Column);
                    }
                    for (int i = 0; i < _kiegcolumns.Count; i++)
                        _kiegdatacolumns.Add(_kiegcolumns[i].Column);
                    for (int i = 0; i < _kiegdatacolumns.Count; i++)
                    {
                        string colname = ((DataColumn)_kiegdatacolumns[i]).ColumnName;
                        if (_adattabla.Columns.IndexOf(colname) != -1)
                            _adattabla.Columns.Remove(colname);
                    }
                    _adattabla.Columns.AddRange((DataColumn[])_kiegdatacolumns.ToArray(typeof(DataColumn)));                   //if (!_leiroe && _azonositok._base._azon != "LEIR" || _leiroe && _azonositok._base._azon == "LEIR")

                }
            }
            if (Tablanev == "BASE")
            {
                IdentityColumnName = "SORREND";
                IdentityColumnIndex = _tablacolumns.IndexOf("SORREND");
            }
        }
        /// <summary>
        /// Combo mezoinformaciok osszekapcsolasa a mezoleirasban definialt Combovalasztas alapjan
        /// </summary>
        public void Combobeallit()
        {
            Tartalmaktolt(false);
            foreach (Cols egycol in _tablacolumns)
            {
                if (egycol.Comboe)
                    egycol.Combobeallit();
                else if (egycol.Lathato && egycol.MaxLength == -1)
                {
                    try
                    {
                        egycol.MaxLength = egycol.InputMaxLength;
                    }
                    catch
                    {
                    }
                }
            }
            Tartalmaktolt(false);
            if (_azonositok.Azon == "LEIR" || _azonositok.Azon == "BASE" || _leiroe || _adattabla.TableName == "TARTAL" || !"COS".Contains(Adatfajta))
                Inputtablaini();
            else
            {
                Osszefinfoini();
            }
            if (_adattabla.TableName == "KODTAB" && Kodtipus == "Specdat")
            {
                _specdatumnevekarray.Clear();
                for (int i = 0; i < DataView.Count; i++)
                    _specdatumnevekarray.Add(DataView[i].Row["SZOVEG"].ToString());
                _fak.SpecDatumNevek = _specdatumnevekarray;
                _specdatumnevek = (string[])_specdatumnevekarray.ToArray(typeof(string));
            }
            else if (_fak.SpecDatumNevek.Count != 0)
            {
                _specdatumnevekarray.Clear();
                foreach (DataColumn egycol in _adattabla.Columns)
                {
                    int i = _fak.SpecDatumNevek.IndexOf(egycol.ColumnName);
                    if (i != -1)
                        _specdatumnevekarray.Add(_fak.SpecDatumNevek[i].ToString());
                }
                _specdatumnevek = (string[])_specdatumnevekarray.ToArray(typeof(string));
                if (_specdatumnevekarray.Count != 0)
                    SpecDatumNevSzerepel = new bool[_specdatumnevek.Length];
            }
        }
        private bool DeleteLastVersion(bool update)
        {
            return DeleteLastVersion(LastVersionId.ToString(), update);
        }
        private bool DeleteLastVersion(string verz, bool update)
        {
            bool kellvaltozas = _fak.KellValtozas;
            string szint = Szint;
            string conns = _adattabla.Connection;
            string tablanev = _adattabla.TableName;
            string selszov = _azonositok.Selectstring;
            int[] verzioinfok = _azonositok.Verzioinfok.VersionArray;
            int i = _verzioterkeparr.IndexOf(Convert.ToInt32(verz));
            if (i == -1)
                return false;
            _deletelast = true;
            if (_adattabla.DataView.Count != 0)
            {
                DataRow dr = _adattabla.DataView[0].Row;
                _fak.ValtoztatasFunkcio = "DELETEVERSION";
                ValtozasNaplozas(dr);
 //               string selszov = _azonositok.Selectstring;
                if (selszov == "")
                    selszov += " where ";
                else
                    selszov += " and ";
                if (_aktverzioid.ToString() != verz)
                {
                    _adattabla.Rows.Clear();
                    Sqlinterface.Select(_adattabla, conns, tablanev, selszov + "VERZIO_ID=" + verz, _azonositok.Orderstring, false);
                }
                _deleteversionid = verz;
                _adattabla.Teljestorles();
                if (kellvaltozas)
                    _fak.KellValtozas = true;
//                int[] verzioinfok = _azonositok.Verzioinfok.VersionArray;
                _aktverzioid = verzioinfok[verzioinfok.Length - 2];
                _verzioterkeparr.RemoveAt(_verzioterkeparr.Count - 1);
                _adattabla.LastSel = selszov + "VERZIO_ID=" + _aktverzioid.ToString();
                if (update)
                    _fak.UpdateTransaction(new Tablainfo[] { this });
            }
            else if (_verzioterkeparr.Count!=0)
            {
                _verzioterkeparr.RemoveAt(_verzioterkeparr.Count - 1);
                _aktverzioid = verzioinfok[verzioinfok.Length - 2];
                _adattabla.LastSel = selszov + "VERZIO_ID=" + _aktverzioid.ToString();
                if (update)
                    _fak.UpdateTransaction(new Tablainfo[] { this });
            }
            return true;
        }
        private string CreateNewVersion(bool update)
        {
            _newversion = true;
            bool kellvaltozas = _fak.KellValtozas;
            Verzioinfok verinf = _azonositok.Verzioinfok;
            int verz = verinf.VersionArray[verinf.VersionArray.Length - 1];
            if(AktVerzioId!=verz)
            {
                DateTime[] interv = null;
                interv = (DateTime[])verinf.VerzioDatumTerkep[verinf.VerzioDatumTerkep.Count -1];
                _fak.Cegadatok(interv);
            }
            if (_osszefinfo!=null)
                _osszefinfo.NewVersionKieg(verz);
            DataTable dt = new DataTable();
            for (int i = 0; i < _tablacolumns.Count; i++)
            {
                DataColumn col = new DataColumn(_adattabla.Columns[i].ColumnName, _adattabla.Columns[i].DataType);
                dt.Columns.Add(col);
            }
            DataRow oldrow = null;
            DataRow newrow;
            DataView view = DataView;

            if (_adattabla.TableName == "OSSZEF" && _azonositok.Adatfajta == "O" && view.Count == 0)
            {
                if (_azonositok.Defert == "1")
                {
                    DataView view1 = _osszefinfo.DataView1;
                    DataView view2 = _osszefinfo.DataView2;
                    DataRow view2row;
                    Verzioinfok verinf1 = _osszefinfo.tabinfo1.Azonositok.Verzioinfok;
                    int verz1 = verinf1.VersionArray[verinf1.VersionArray.Length - 1];
                    Verzioinfok verinf2 = _osszefinfo.tabinfo2.Azonositok.Verzioinfok;
                    int verz2 = verinf2.VersionArray[verinf2.VersionArray.Length - 1];
                    int sorszam1col = _osszefinfo.tabinfo1.IdentityColumnIndex;
                    int sorszam2col = _osszefinfo.tabinfo2.IdentityColumnIndex;
                    int previd1col = _osszefinfo.previd1col;
                    int previd2col = _osszefinfo.previd2col;
                    int sorrend = 0;
                    for (int i = 0; i < view1.Count; i++)
                    {
                        oldrow = view1[i].Row;
                        for (int j = 0; j < view2.Count; j++)
                        {
                            view2row = view2[j].Row;
                            newrow = dt.NewRow();
                            newrow["VERZIO_ID"] = verz;
                            newrow["PREV_ID"] = 0;
                            newrow["KODTIPUS"] = _azonositok.Kodtipus;
                            sorrend += 100;
                            newrow["SORREND"] = sorrend;
                            newrow["SORSZAM1"] = oldrow[sorszam1col];
                            newrow["VERZIO_ID1"] = verz1;
                            newrow["PREV_ID1"] = oldrow[previd1col];
                            newrow["SORSZAM2"] = view2row[sorszam2col];
                            newrow["VERZIO_ID2"] = verz2;
                            newrow["PREV_ID2"] = view2row[previd2col];
                            dt.Rows.Add(newrow);
                        }

                    }
                }
            }
            else
            {
                for (int i = 0; i < view.Count; i++)
                {
                    oldrow = view[i].Row;
                    newrow = dt.NewRow();
                    for (int j = 0; j < _tablacolumns.Count; j++)
                        newrow[j] = oldrow[j];
                    dt.Rows.Add(newrow);
                }
            }
            _adattabla.Rows.Clear();
            string colname = "";
            bool szukkodtab = Azon == "SZCS";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                oldrow = dt.Rows[i];
                newrow = _adattabla.Ujsor();
                for (int j = 0; j < _tablacolumns.Count; j++)
                {
                    Cols egycol = (Cols)_tablacolumns[j];
                    colname = egycol.ColumnName;
                    if (egycol.IsIdentity)
                    {
                        if (oldrow[j].ToString() == "")
                            newrow["PREV_ID"] = 0;
                        else
                            newrow["PREV_ID"] = oldrow[j];
                    }
                    else if (colname == "VERZIO_ID")
                        newrow[j] = verz;
                    else if (colname != "PREV_ID")
                    {
                        string ert = "";
                        newrow[j] = oldrow[j];
                        if(Tablanev=="OSSZEF" || szukkodtab)
                        {
                            Tablainfo tabinfo1 = _osszefinfo.tabinfo1;
                            Tablainfo tabinfo2 = _osszefinfo.tabinfo2;
                            DataView view1 = _osszefinfo.DataView1;
                            DataView view2 = _osszefinfo.DataView2;
                            string[] sorszok = null;
                            switch (colname)
                            {
                                case "VERZIO_ID1":
                                    newrow[j] = tabinfo1.AktVerzioId;
                                    break;
                                case "VERZIO_ID2":
                                    newrow[j] = tabinfo2.AktVerzioId;
                                    break;
                                case "SORSZAM1":
                                    if (tabinfo1.KellVerzio)
                                    {
                                        sorszok = Fak.GetTartal(tabinfo1,tabinfo1.IdentityColumnName, "PREV_ID", oldrow["PREV_ID1"].ToString());
                                        if (sorszok != null)
                                            newrow[j] = sorszok[0];
                                    }
                                    break;
                                case "SORSZAM2":
                                    if (tabinfo1.KellVerzio)
                                    {
                                        sorszok = Fak.GetTartal(tabinfo2, tabinfo2.IdentityColumnName, "PREV_ID", oldrow["PREV_ID2"].ToString());
                                        if (sorszok != null)
                                            newrow[j] = sorszok[0];
                                    }
                                    break;
                                case "RSORSZAM":
                                    sorszok = Fak.GetTartal(tabinfo1,tabinfo1.IdentityColumnName,"KOD",oldrow["KOD"].ToString());
                                    if (sorszok != null)
                                        newrow[j] = sorszok[0];
                                    break;
                                case "PREV_ID1":
                                    if(!szukkodtab)
                                        newrow[j] = oldrow[_osszefinfo.sorszam1col];
                                    else
                                        newrow[j] = oldrow["RSORSZAM"];
                                    break;
                                case "PREV_ID2":
                                    newrow[j] = oldrow[_osszefinfo.sorszam2col];
                                    break;
                            }
                        }
                        else if (egycol.Comboe)
                        {
                            ert = oldrow[j].ToString();
                            if (ert == "")
                                ert = "0";
                            //                            newrow[j] = oldrow[j];
                            if (ert != "0" && ert != "" && egycol.Combo_Info != null)
                            {
                                Tablainfo info = egycol.Combo_Info.Combotag.Tablainfo;
                                if (info.KellVerzio)
                                {
                                    if (info.LastVersionId > _aktverzioid)
                                    {
                                        int jj = egycol.Combo_Info.KiegFileinfo.IndexOf(ert);
                                        string oldszov = egycol.Combo_Info.KiegInfo[jj].ToString();
                                        jj = egycol.Combo_Info.ComboInfo.IndexOf(oldszov);
                                        if (jj != -1)
                                            ert = egycol.Combo_Info.ComboFileinfo[jj].ToString();
                                        else
                                            ert = egycol.Combo_Info.DefFileba;
                                    }
                                }
                                //                               if(info.LastVersionId>
                            }
                            newrow[j] = ert;
                        }

                    }
                }
                ValtozasNaplozas(newrow);
            }
            if (kellvaltozas)
                _fak.KellValtozas = true;
            string adatfajta = Adatfajta;
            Tartalmaktolt();
            string selszov = _azonositok.Selectstring;
            if (selszov == "")
                selszov += " where ";
            else
                selszov += " and ";
            _adattabla.LastSel = selszov + "VERZIO_ID=" + verz.ToString();
            for (int i = 0; i < _adattabla.Rows.Count; i++)
                _adattabla.Rows[i]["MODOSITOTT_M"] = 1;
            if (_verzioterkeparr.Count == 0)
                _modositott = true;
            _verzioterkeparr.Clear();
            for(int i=1;i<=verz;i++)
            //_verzioterkeparr.Add(verz);
                _verzioterkeparr.Add(i);
            if (update)
            {
                _aktverzioid = Convert.ToInt32(verz);
                _fak.UpdateTransaction(new Tablainfo[] { this });
                if (Osszefinfo != null)
                    Osszefinfo.InitKell = true;
            }
            return verz.ToString();
        }
        /// <summary>
        /// toltse be az elozo verziot, ha van
        /// </summary>
        public void ElozoVerzio()
        {
            for (int i = 0; i < _verzioterkeparr.Count; i++)
            {
                if (_verzioterkeparr[i].ToString() == _aktverzioid.ToString())
                {
                    if (i != 0)
                    {
                        i--;
                        string verz = _verzioterkeparr[i].ToString();
                        _aktverzioid = Convert.ToInt32(verz);
                        _adattabla.Select();
                        if (Tablanev == "LEIRO")
                        {
                            Tablainfo adatinfo = TablaTag.Tablainfo;
                            DataRow dr;
                            DataView leiroview = DataView;
                            ColCollection adattablacol = adatinfo.TablaColumns;
                            Cols egycol;
                            for (int j = 0; j < leiroview.Count; j++)
                            {
                                dr = leiroview[j].Row;
                                string adatnev = dr["ADATNEV"].ToString().Trim();
                                egycol = adattablacol[adatnev];
                                if (egycol != null)
                                    egycol.Beallitasok(dr, _fak);
                            }
                        }
                        if (Tablanev == "TARTAL")
                            ViewFrissit();
                        else if (Osszefinfo != null)
                            Osszefinfo.InitKell = true;
                        _fak.AzontipSzerintUpdate(this);
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// toltse be a kovetkezo verziot, ha van
        /// </summary>
        public void KovetkezoVerzio()
        {
            for (int i = 0; i < _verzioterkeparr.Count; i++)
            {
                if (_verzioterkeparr[i].ToString() == _aktverzioid.ToString())
                {
                    if (i != _verzioterkeparr.Count - 1)
                    {
                        i++;
                        string verz = _verzioterkeparr[i].ToString();
                        _aktverzioid = Convert.ToInt32(verz);
                        _adattabla.Select();
                        if (Tablanev == "LEIRO")
                        {
                            Tablainfo adatinfo = TablaTag.Tablainfo;
                            DataRow dr;
                            DataView leiroview = DataView;
                            ColCollection adattablacol = adatinfo.TablaColumns;
                            Cols egycol;
                            for (int j = 0; j < leiroview.Count; j++)
                            {
                                dr = leiroview[j].Row;
                                string adatnev = dr["ADATNEV"].ToString().Trim();
                                egycol = adattablacol[adatnev];
                                if (egycol != null)
                                    egycol.Beallitasok(dr, _fak);
                            }
                        }
                        if (Tablanev == "TARTAL")
                            ViewFrissit();
                        else if (Osszefinfo != null)
                            Osszefinfo.InitKell = true;
                        _fak.AzontipSzerintUpdate(this);
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// a valtozasok naplozasahoz a parametersorbol es a beallitott funkciobol oszzeallitja a szukseges sorokat
        /// </summary>
        /// <param name="dr"></param>
        public void ValtozasNaplozas(DataRow dr)
        {
            if (_fak.KellValtozas && Tablanev != "VALTOZASNAPLO")
            {
                string szint = Szint;
                string funkcio = _fak.ValtoztatasFunkcio;
                DataTable valttabla = NaploTabla;
                if (valttabla == null)
                {
                    int valind = "RUC".IndexOf(szint);
                    valttabla = _fak.NaploTablak[valind];
                }
                foreach (Cols egycol in _tablacolumns)
                {
                    string hason = dr[egycol.ColumnName].ToString().Trim();
                    if (egycol.DataType.ToString().Contains("Date"))
                    {
                        if (hason != "")
                            hason = Convert.ToDateTime(hason).ToShortDateString();
                    }
                    if (funkcio == "ADD" || funkcio != "MODIFY" || egycol.OrigTartalom != hason)
                    {
                        DataRow dr1 = valttabla.NewRow();
                        if (_fak.KezeloId != -1)
                            dr1["KEZELO_ID"] = _fak.KezeloId;
                        dr1["ALKALM"] = _azonositok.Owner;
                        dr1["USEREK"] = _azonositok.User;
                        dr1["FUNKCIO"] = funkcio;
                        if (_leiroe)
                            dr1["SZINT"] = TablaTag.Tablainfo.Szint;
                        else if (Tablanev != "BASE")
                            dr1["SZINT"] = Szint;
                        else
                            dr1["SZINT"] = _tablacolumns["SZINT"].Tartalom;
                        dr1["AZON"] = Azon;
                        dr1["KODTIPUS"] = Kodtipus;
                        dr1["TABLANEV"] = Tablanev;
                        if (Leiroe)
                        {
                            dr1["ADATTABLANEV"] = TablaTag.Tablainfo.Tablanev;
                            dr1["KODTIPUS"] = dr["ADATNEV"].ToString();
                        }
                        else if (Tablanev == "TARTAL")
                        {
                            string tartal = _tablacolumns["KODTIPUS"].Tartalom;
                            if (tartal != "")
                                dr1["KODTIPUS"] = tartal;
                            else
                                dr1["KODTIPUS"] = _tablacolumns["TABLANEV"].Tartalom;
                        }
                        else if (!Leiroe && Fak.Adatszintek.Contains(Szint))
                            dr1["CEG_ID"] = Fak.AktualCegid;
                        dr1["VERZIO_ID"] = AktVerzioId;
                        dr1["MEZONEV"] = egycol.ColumnName;
                        if (funkcio == "ADD" || funkcio == "NEWVERSION" || funkcio == "MODIFY")
                            dr1["NEWVALUE"] = dr[egycol.ColumnName];
                        if (funkcio == "TOROL" || funkcio == "DELETEVERSION" || funkcio == "MODIFY")
                            dr1["OLDVALUE"] = egycol.OrigTartalom;
                        dr1["SORINDEX"] = ViewSorindex;
                        valttabla.Rows.Add(dr1);
                    }
                }
            }
        }
        /// <summary>
        /// Tartalomjegyzek elozo ill. kovetkezo verzio kerese eseten a GridView frissitese
        /// </summary>
        public void ViewFrissit()
        {
            DataRow dr;
            TablainfoTagCollection childtagok = _tablatag.ChildTablainfoTagok;
            TablainfoTag tag;
            Tablainfo tabinfo;
            string azontip;
            string childazontip;
            bool talalt = false;
            if (childtagok.Count > _adattabla.DataView.Count)          // biztosan kell torolni
            {
                for (int i = 0; i < childtagok.Count; i++)
                {
                    talalt = false;
                    childazontip = childtagok[i].Azonositok.Azontip;
                    for (int j = 0; j < _adattabla.DataView.Count; j++)
                    {
                        dr = _adattabla.DataView[j].Row;
                        azontip = dr["AZONTIP"].ToString();
                        if (azontip == childazontip)
                        {
                            talalt = true;
                            break;
                        }
                    }
                    if (!talalt)
                    {
                        childtagok[i].Remove();
                        i--;
                    }
                }
            }
            if (childtagok.Count < _adattabla.DataView.Count)
            {
                for (int i = 0; i < _adattabla.DataView.Count; i++)
                {
                    dr = _adattabla.DataView[i].Row;
                    azontip = dr["AZONTIP"].ToString();
                    tag = childtagok[azontip];
                    if (tag == null)
                    {
                        tabinfo = _fak.Tablainfok.GetByAzontip(azontip);
                        tag = tabinfo.TablaTag;
                        tag.SorIndex = i;
                        tag.Insert();
                    }
                }
            }
        }
    }
}




