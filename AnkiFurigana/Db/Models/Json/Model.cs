using System;
using System.Collections.Generic;
using System.Text;

namespace AnkiFurigana.Db.Models.Json
{
    public partial class Model
    {
        public string Css { get; set; }
        public long Did { get; set; }
        public Field[] Flds { get; set; }
        public long Id { get; set; }
        public string LatexPost { get; set; }
        public string LatexPre { get; set; }
        public long Mod { get; set; }
        public string Name { get; set; }
        public object Req { get; set; }
        public long Sortf { get; set; }
        public object[] Tags { get; set; }
        public Template[] Tmpls { get; set; }
        public long Type { get; set; }
        public long Usn { get; set; }
        public object[] Vers { get; set; }
    }

    public partial class Field
    {
        public string Font { get; set; }
        public object[] Media { get; set; }
        public string Name { get; set; }
        public long Ord { get; set; }
        public bool Rtl { get; set; }
        public long Size { get; set; }
        public bool Sticky { get; set; }
    }

    public partial class Template
    {
        public string Afmt { get; set; }
        public string Bafmt { get; set; }
        public string Bqfmt { get; set; }
        public object Did { get; set; }
        public string Name { get; set; }
        public long Ord { get; set; }
        public string Qfmt { get; set; }
    }


}
