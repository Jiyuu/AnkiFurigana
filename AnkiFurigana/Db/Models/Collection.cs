using AnkiFurigana.Db.Models.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnkiFurigana.Db.Models
{
    //    CREATE TABLE col(
    //    id integer primary key,
    //    crt integer not null,
    //    mod integer not null,
    //    scm integer not null,
    //    ver integer not null,
    //    dty integer not null,
    //    usn integer not null,
    //    ls integer not null,
    //    conf text not null,
    //    models text not null,
    //    decks text not null,
    //    dconf text not null,
    //    tags text not null
    //)

    [Table("col")]
    class Collection
    {
        public long id { get; set; }
        public long crt { get; set; }
        public long mod { get; set; }
        public long scm { get; set; }
        public long ver { get; set; }
        public long dty { get; set; }
        public long usn { get; set; }
        public long ls { get; set; }
        public string conf { get; set; }

        public IDictionary<string, Model> models { get; set; }
        public string decks { get; set; }
        public string dconf { get; set; }
        public string tags { get; set; }



        
    }

    class CollectionConfiguration : IEntityTypeConfiguration<Collection>
    {
        public void Configure(EntityTypeBuilder<Collection> builder)
        {
            // This Converter will perform the conversion to and from Json to the desired type
            builder.Property(e => e.models).HasConversion(
                v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                v => JsonConvert.DeserializeObject<IDictionary<string, Model>>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }
    }
}
