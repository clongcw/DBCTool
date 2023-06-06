#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DbcParserLib;
using DbcParserLib.Model;
using Microsoft.Win32;
using PropertyChanged;

#endregion

namespace DBCTool
{
    [AddINotifyPropertyChangedInterface]
    public partial class MainViewModel : ObservableObject
    {
        #region Property

        public string Title { get; set; }
        public string? DBCPath { get; set; }
        public string? NodeName { get; set; }

        public ObservableCollection<Message> Messages { get; set; } = new();
        public ObservableCollection<Signal> Signals { get; set; } = new();

        #endregion

        #region Constructor

        public MainViewModel()
        {
            Title = "DBC解析器";
        }

        #endregion

        #region Commands

        [RelayCommand]
        public void LoadDBC()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open dbc file";
            openFileDialog.FileName = "";
            openFileDialog.Filter = "DBC File|*.dbc";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                LoadDbc(openFileDialog.FileName);
            }
        }

        [RelayCommand]
        public void SaveDBC()
        {
            IEnumerable<Node> Nodes = new ObservableCollection<Node>();
            IEnumerable<Message> Messages2 = new ObservableCollection<Message>();

            IEnumerable<EnvironmentVariable> EnvironmentVariables = new ObservableCollection<EnvironmentVariable>();

            //Dbc dbc=new Dbc(Nodes,Messages2,EnvironmentVariables);
        }

        #endregion

        #region Methods

        public void LoadDbc(string filePath)
        {
            try
            {
                var dbc = Parser.ParseFromPath(filePath);
                DBCPath = filePath;
                PopulateView(dbc);
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public void PopulateView(Dbc dbc)
        {
            // Nodes
            NodeName = "";
            foreach (var node in dbc.Nodes)
            {
                NodeName += node.Name + " ";
            }

            // Messages
            Messages.Clear();
            foreach (var msg in dbc.Messages)
            {
                Messages.Add(new Message()
                {
                    ID = $"0x{msg.ID.ToString("X")}",
                    Name = msg.Name,
                    DLC = msg.DLC,
                    Transmitter = msg.Transmitter,
                    CycleTime = msg.CycleTime,
                });
            }

            // Signals
            Signals.Clear();
            foreach (var msg in dbc.Messages)
            {
                foreach (var sig in msg.Signals)
                {
                    Signals.Add(new Signal()
                    {
                        ID = "0x" + sig.ID.ToString("X"),
                        Name = sig.Name,
                        StartBit = sig.StartBit,
                        Length = sig.Length,
                        ByteOrder = sig.ByteOrder,
                        ValueType = DbcValueType.Signed,
                        InitialValue = sig.InitialValue,
                        Factor = sig.Factor,
                        Offset = sig.Offset,
                        Minimum = sig.Minimum,
                        Maximum = sig.Maximum,
                        Unit = sig.Unit,
                        ValueTable = sig.ValueTable,
                        Comment = sig.Comment
                    });
                }
            }
        }

        #endregion
    }
}