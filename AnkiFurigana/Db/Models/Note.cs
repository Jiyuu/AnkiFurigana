using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnkiFurigana.Db.Models
{
//    CREATE TABLE notes(
//    id integer primary key,   /* 0 */
//    guid text not null,         /* 1 */
//    mid integer not null,      /* 2 */
//    mod integer not null,      /* 3 */
//    usn integer not null,      /* 4 */
//    tags text not null,         /* 5 */
//    flds text not null,         /* 6 */
//    sfld integer not null,      /* 7 */
//    csum integer not null,      /* 8 */
//    flags integer not null,      /* 9 */
//    data text not null          /* 10 */
//    );
    class Note
    {
        public long  id { get; set; }
        public string guid { get; set; }
        public long mid { get; set; }
        public long mod { get; set; }
        public long usn { get; set; }
        public string tags { get; set; }
        public string[] flds { get; set; }
        public long sfld { get; set; }
        public long csum { get; set; }
        public long flags { get; set; }
        public string data { get; set; }
    }
    class NotesConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            // This Converter will perform the conversion to and from Json to the desired type
            builder.Property(e => e.flds).HasConversion(
                v => String.Join('\x1f',v),
                v => v.Split('\x1f',StringSplitOptions.None));
        }
    }
}
