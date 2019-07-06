using AnkiFurigana.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AnkiFurigana
{
    class Program
    {
        static HashSet<string> hiregana = new HashSet<string>() { "あ", "い", "う", "え", "お", "か", "っ",
            "き", "く", "け", "こ", "が", "ぎ", "ぐ",
            "げ", "ご", "さ", "し", "す", "せ", "そ",
            "ざ", "じ", "ず", "ぜ", "ぞ", "た", "ち",
            "つ", "て", "と", "だ", "ぢ", "づ", "で",
            "ど", "な", "に", "ぬ", "ね", "の", "は",
            "ひ", "ふ", "へ", "ほ", "ば", "び", "ぶ",
            "べ", "ぼ", "ぱ", "ぴ", "ぷ", "ぺ", "ぽ",
            "ま", "み", "む", "め", "も", "や", "ゆ",
            "よ", "ら", "り", "る", "れ", "ろ", "わ",
            "ゐ", "を", "ゑ", "ん", "ゃ", "ゅ", "ょ" };
        static async Task Main(string[] args)
        {
            var erdicData = JsonConvert.DeserializeObject<Erdic.Kanjidic[]>(await File.ReadAllTextAsync("kanjidic2.json"));
            var jlptn2345 = erdicData.Where(k => k.Jlpt.HasValue && k.Jlpt.Value > 1).ToDictionary(k => k.Literal);
            var jlptn1 = erdicData.Where(k => !k.Jlpt.HasValue || k.Jlpt.Value <= 1).ToDictionary(k => k.Literal);

            //var test = jlptn2345["色"];

            //var test = jlptn2345.Where(k => k.ToCharArray().Length > 1).ToArray();
            //var test2 = jlptn1.Where(k => k.ToCharArray().Length > 1).ToArray();
            string dbLocation = @"C:\Users\Dror\Documents\Anki\Japanese Vocabulary_ JLPT\collection.anki2";
            var dbc = new AnkiContext(new DbContextOptionsBuilder<AnkiContext>()
                .UseSqlite($"Data Source={dbLocation}").Options);

            var simulated_update = new List<(string, string)>();
            var mixed = new List<(string, string)>();

            var models = (await dbc.Collection.FirstAsync()).models;
            List<string> n1Kanji;
            foreach (var note in dbc.Notes)
            {
                var model = models[note.mid.ToString()];
                var fieldDictionary = model.Flds.ToDictionary(v => v.Name,v=>v.Ord);
                //var mappedFields = note.flds.Select((field, index) => (field, index)).ToDictionary(v => fieldDictionary[v.index].Name, v => v.field);
                if (note.flds[fieldDictionary["Kanji"]] != String.Empty && note.flds[fieldDictionary["furigana"]] == String.Empty)
                {
                    if (!StringInfo.GetTextElementEnumerator(note.flds[fieldDictionary["Kanji"]]).ToEnumerable().Any(k => jlptn2345.ContainsKey(k)))//we dont have any n2-5 kanji here, so safe to 
                    {
                        note.flds[fieldDictionary["furigana"]] = note.flds[fieldDictionary["Reading"]];
                        dbc.Notes.Update(note);
                        //var update = (note.flds[fieldDictionary["Kanji"]], note.flds[fieldDictionary["Reading"]]);
                        //simulated_update.Add(update);
                    }
                    else if ((n1Kanji = StringInfo.GetTextElementEnumerator(note.flds[fieldDictionary["Kanji"]]).ToEnumerable().Where(k => jlptn1.ContainsKey(k)).Distinct().ToList()).Count>0) //mixed, what to do?
                    {
                        var sb = new StringBuilder();

                        foreach (var kanji in n1Kanji)
                        {
                            sb.Append($"{kanji}[{jlptn1[kanji].Readings.Where(r => r.RType == Erdic.RType.ja_kun || r.RType == Erdic.RType.ja_on).Select(r => r.Reading).StringJoin(",")}],\t");
                        }
                        note.flds[fieldDictionary["furigana"]] = sb.ToString().TrimEnd(',','\t');
                        dbc.Notes.Update(note);

                    }
                }


            }
            await dbc.SaveChangesAsync();
        }

        static string removehiregana(string str)
        {
            return StringInfo.GetTextElementEnumerator(str).ToEnumerable().Where(chr => !hiregana.Contains(chr)).StringJoin();
        }
    }



    public static class TEST
    {

        public static IEnumerable<string> ToEnumerable(this TextElementEnumerator enumerator)
        {
            while (enumerator.MoveNext())
            {
                yield return enumerator.GetTextElement();
            }
        }

        public static string StringJoin(this IEnumerable<string> strings, string seperator = "")
        {
            return String.Join(seperator, strings);
        }
    }
}
