using DbcParserLib.Model;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCTool
{
    [AddINotifyPropertyChangedInterface]
    public class Message
    {
        public string ID { get; set; }
        public bool IsExtID { get; set; }
        public string Name { get; set; }
        public ushort DLC { get; set; }
        public string Transmitter { get; set; }
        public string Comment { get; set; }
        public int CycleTime { get; set; }

        public List<Signal> Signals = new List<Signal>();
        public IDictionary<string, CustomProperty> CustomProperties = new Dictionary<string, CustomProperty>();

        
    }
}
