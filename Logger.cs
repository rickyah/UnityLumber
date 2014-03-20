using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Collections;
using Lumber.Scopes;
using Lumber.Extensions;

namespace Lumber
{
    public class RichLogger
    {
        private StringBuilder _printer;
        private bool _isNextPrintANewLine;
        private int _level;
        private const string _defaultTabSeparator = "\t";
        private string _tabSeparator;

        public string TabSeparator
        {
            get
            {
                return _tabSeparator;
            } 
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    value = _defaultTabSeparator;    
                }
                
                _tabSeparator = value;
            }
        }

        public int Level
        {
            get
            { 
                return _level; 
            }
            private set
            {
                _level = value >= 0 ? value : 0;    
            }
        }

        public Color Color
        {
            get; 
            private set;
        }

        public RichLogger()
        {
            TabSeparator = _defaultTabSeparator;
            _printer = new System.Text.StringBuilder();
            Clear();
        }

        public RichLogger SetColor(Color newColor)
        {
            Color = newColor;
            return this;
        }

        public RichLogger SetLevel(int newLevel)
        {
            Level = newLevel;
            return this;
        }

        public TabScope WithLevel(int level)
        {
            return new TabScope(this, level);
        }

        public TabScope AddLevel(int levelToAdd = 1)
        {
            return new TabScope(this, this.Level + levelToAdd);
        }

        public ColorScope WithColor(Color newColor)
        {
            return new ColorScope(this, newColor);
        }

        public RichLogger Print(IEnumerable enumerable)
        {
            int idx = 0;
            foreach (var element in enumerable)
            {
                PrintLine(" [{0}] = {1}", idx++, element.ToString());    
            }
            
            return this;
        }

        public RichLogger Print(string format, params object[] objs)
        {
            return InternalPrint(string.Format(format, objs));
        }

        public RichLogger PrintLine(string format, params object[] objs)
        {
            return Print(format, objs).NewLine();
        }

        public RichLogger Clear()
        {
            _printer.Remove(0, _printer.Length);
        
            return ClearLevel().ClearColor();
        }

        public RichLogger ClearLevel()
        {
            Level = 0;
            return this;
        }

        public RichLogger ClearColor()
        {
            Color = Color.black;
            return this;
        }

        public RichLogger NewLine()
        {
            _printer.AppendLine();
            _isNextPrintANewLine = true;
            return this;
        }

        public override string ToString()
        {
            return _printer.ToString();
        }

        private RichLogger InternalPrint(string msg)
        {
            _printer.AppendFormat("{0}<color={1}>{2}</color>",
                _isNextPrintANewLine ? FormatTabLevel : string.Empty, 
                FormatColorCode, msg);
        
            _isNextPrintANewLine = false;
            return this;
        }

        private string FormatTabLevel
        {
            get
            {
                return string.Concat(Enumerable.Repeat(TabSeparator, Level).ToArray());
            }
        
        }

        private string FormatColorCode
        {
            get
            {
                return this.Color.ToARGB();
            }
        }
    }
}