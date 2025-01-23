using Newtonsoft.Json.Linq;

namespace WpfRDLC01
{
    public class DataModel
    {
        public int seq { get; set; }
        public string subject { get; set; }
        public JArray content { get; set; }
    }
}
