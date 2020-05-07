using JustDialScraper.Common.Base;
using System.Runtime.Serialization;

namespace JustDialScraper.Ui.Models
{
    public class LocationModel: ModelBase, IJustDialRecord
    {
        [DataMember(Name = "value")]
        public string Value { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
