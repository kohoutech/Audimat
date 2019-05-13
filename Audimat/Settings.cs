/* ----------------------------------------------------------------------------
Audimat : an audio plugin host
Copyright (C) 2005-2019  George E Greaney

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
----------------------------------------------------------------------------*/

//simple YAML-like syntax
//no error checking yet

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Audimat
{
    public class Settings
    {
        String filename;
        SettingsStem root;

        public Settings(String _filename)
        {
            root = null;
            filename = _filename;
            string[] lines = null;
            try
            {
                lines = File.ReadAllLines(filename);
            }
            catch (Exception e)
            {
                return;
            }

            parseRoot(lines);
        }

        char[] wspace = new char[] { ' ' };

        public void parseRoot(string[] lines)
        {
            int lineNum = 0;
            root = parseSubtree(lines, ref lineNum);
        }

        public SettingsStem parseSubtree(string[] lines, ref int lineNum)
        {
            SettingsStem curStem = new SettingsStem();
            int indentLevel = -1;
            while (lineNum < lines.Length)
            {
                String line = lines[lineNum++].TrimEnd(wspace);
                if (line.Length == 0 || line[0] == '#')                 //skip blank lines & comments
                {
                    continue;
                }

                int indent = 0;
                while ((indent < line.Length) && (line[indent] == ' ')) indent++;   //get line indent
                if (indentLevel == -1) indentLevel = indent;                        //if first line of subtree, get indent level

                if (indent < indentLevel)
                {
                    lineNum--;              //this line is not in subgroup so back up one line
                    return curStem;
                }
                else
                {
                    line = line.TrimStart(wspace);                              //we have the indent count, remove the leading spaces
                    int colonpos = line.IndexOf(':');
                    String name = line.Substring(0, colonpos).Trim();
                    if (colonpos + 1 != line.Length)                                //nnn : xxx
                    {
                        String val = line.Substring(colonpos + 1).Trim();
                        curStem.children.Add(name, new SettingsLeaf(val));
                    }
                    else
                    {
                        SettingsStem substem = parseSubtree(lines, ref lineNum);
                        curStem.children.Add(name, substem);
                    }
                }
            }
            return curStem;
        }

        //- getting values ----------------------------------------------------

        public String findLeafValue(String path, SettingsStem subtree)
        {
            String result = null;
            int dotpos = path.IndexOf('.');
            if (dotpos != -1)
            {
                String name = path.Substring(0, dotpos);
                String subpath = path.Substring(dotpos + 1);
                result = findLeafValue(subpath, (SettingsStem)subtree.children[name]);
            }
            else
            {
                result = ((SettingsLeaf)subtree.children[path]).value;
            }
            return result;
        }

        public int getIntValue(String path, int defval)
        {
            int result = defval;
            if (root != null)
            {
                String intstr = findLeafValue(path, root);
                try
                {
                    result = Int32.Parse(intstr);
                }
                catch (Exception e)
                {
                }
            }
            return result;
        }

        //- setting values ----------------------------------------------------

        public void setLeafValue(String path, SettingsStem subtree, String val)
        {
            int dotpos = path.IndexOf('.');
            if (dotpos != -1)
            {
                String name = path.Substring(0, dotpos);
                String subpath = path.Substring(dotpos + 1);
                setLeafValue(subpath, (SettingsStem)subtree.children[name], val);
            }
            else
            {
                ((SettingsLeaf)subtree.children[path]).value = val;
            }
        }

        public void setIntValue(String path, int val)
        {
            String intstr = val.ToString();
            if (root == null)
            {
                root = new SettingsStem();
            }
            setLeafValue(path, root, intstr);
        }

        public void saveToFile()
        {
            List<String> lines = new List<string>();
            storeSubTree(lines, root, "");
        }

        private void storeSubTree(List<string> lines, SettingsNode root, String indent)
        {
            
        }
    }

    //-------------------------------------------------------------------------

    //base class
    public class SettingsNode
    {
    }

    public class SettingsStem : SettingsNode
    {
        public Dictionary<string, SettingsNode> children;

        public SettingsStem()
        {
            children = new Dictionary<string, SettingsNode>();
        }
    }

    public class SettingsLeaf : SettingsNode
    {
        public String value;

        public SettingsLeaf(String val)
        {
            value = val;
        }
    }
}
