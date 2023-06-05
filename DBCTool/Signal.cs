﻿using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCTool
{
    [AddINotifyPropertyChangedInterface]
    public class Signal
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public ushort StartBit { get; set; }
        public ushort Length { get; set; }
        public byte ByteOrder { get; set; }
        public byte IsSigned { get; set; }
        public double InitialValue { get; set; }
        public double Factor { get; set; }
        public double Offset { get; set; }
        public double Minimum { get; set; }
        public double Maximum { get; set; }
        public string Unit { get; set; }
        public string ValueTable { get; set; }
        public string Comment { get; set; }
    }
}
