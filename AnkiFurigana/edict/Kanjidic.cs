using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AnkiFurigana.Erdic
{
    public partial class Kanjidic
    {
        public Codepoint[] Codepoints { get; set; }
        public DicNumber[] DicNumbers { get; set; }
        public long? Freq { get; set; }
        public long? Grade { get; set; }
        public long? Jlpt { get; set; }
        public string Literal { get; set; }
        public KanjiMeaning[] Meanings { get; set; }
        public QueryCode[] QueryCodes { get; set; }
        public Radical[] Radicals { get; set; }
        public KanjiReading[] Readings { get; set; }
        public long StrokeCount { get; set; }
        public KanjiVariant Variant { get; set; }
    }

    public partial class Codepoint
    {
        public VarTypeEnum CpType { get; set; }
        public string CpValue { get; set; }
    }

    public partial class DicNumber
    {
        public string DicRef { get; set; }
        public DrType DrType { get; set; }
    }

    public partial class KanjiMeaning
    {
        public string Meaning { get; set; }

        [JsonProperty("m_lang")]
        public MLang? MLang { get; set; }
    }

    public partial class QueryCode
    {
        public string QCode { get; set; }
        public QcType QcType { get; set; }
    }

    public partial class Radical
    {
        [JsonProperty("rad_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public RadType RadType { get; set; }
        [JsonProperty("rad_value")]
        public long RadValue { get; set; }
    }

    public partial class KanjiReading
    {
        //without these attributes the rtype enum doent get processes properly
        [JsonProperty("r_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public RType RType { get; set; }
        public string Reading { get; set; }
    }

    public partial class KanjiVariant
    {
        public VarTypeEnum VarType { get; set; }
        public string Variant { get; set; }
    }

    public enum VarTypeEnum { Deroo, Jis208, Jis212, Jis213, NelsonC, Njecd, Oneill, SH, Ucs };

    public enum DrType { BusyPeople, Crowley, Gakken, HalpernKkld, HalpernNjecd, Heisig, Henshall, Henshall3, JfCards, KanjiInContext, KodanshaCompact, Maniette, Moro, NelsonC, NelsonN, OneillKk, OneillNames, Sakade, ShKk, TuttCards };

    public enum MLang {
        [EnumMember(Value = "es")]
        Es,
        [EnumMember(Value = "fr")]
        Fr,
        [EnumMember(Value = "pt")]
        Pt
    };

    public enum QcType { Deroo, FourCorner, ShDesc, Skip };

    public enum RadType {
        [EnumMember(Value = "classical")]
        Classical,
        [EnumMember(Value = "nelson_c")]
        NelsonC };


    public enum RType {
        [EnumMember(Value = "ja_kun")]
        ja_kun,
        [EnumMember(Value = "ja_on")]
        ja_on,
        [EnumMember(Value = "korean_h")]
        korean_h,
        [EnumMember(Value = "korean_r")]
        korean_r,
        [EnumMember(Value = "pinyin")]
        pinyin
    }
};
