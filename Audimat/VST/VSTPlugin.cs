/* ----------------------------------------------------------------------------
Transonic VST Library
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Audimat.UI;

namespace Transonic.VST
{
    public class VSTPlugin
    {
        public Vashti vashti;
        public String filename;

        //these are supplied by the plugin
        public int id;
        public String name;
        public String vendor;
        public int version; 
        public int numPrograms;
        public int numParams;
        public int numInputs;
        public int numOutputs;
        public int flags;
        public int uniqueID;

        public bool hasEditor;
        public int editorWidth;
        public int editorHeight;

        public VSTParam[] parameters;
        public String[] programs;
        int curProgramNum;

        public VSTPlugin(Vashti _vashti, String _filename)
        {
            vashti = _vashti;
            filename = _filename;        
        }

        public bool load()
        {
            id = vashti.loadPlugin(filename);
            bool result = (id != -1);
            if (result)
            {
                PluginInfo pluginfo = new PluginInfo();
                vashti.getPluginInfo(id, ref pluginfo);
                name = pluginfo.name;
                vendor = pluginfo.vendor;
                version = pluginfo.version;
                numPrograms = pluginfo.numPrograms;
                numParams = pluginfo.numParameters;
                numInputs = pluginfo.numInputs;
                numOutputs = pluginfo.numOutputs;
                flags = pluginfo.flags;
                uniqueID = pluginfo.uniqueID;
                editorWidth = pluginfo.editorWidth;
                editorHeight = pluginfo.editorHeight;

                parameters = new VSTParam[numParams];
                for (int i = 0; i < numParams; i++)
                {
                    String paramName = vashti.getPluginParamName(id, i);
                    float paramVal = vashti.getPluginParamValue(id, i);
                    parameters[i] = new VSTParam(i, paramName, paramVal);                    
                }

                if (numPrograms > 0)
                {
                    programs = new String[numPrograms];
                    for (int i = 0; i < numPrograms; i++)
                    {
                        String progName = vashti.getPluginProgramName(id, i);
                        programs[i] = progName;
                    }
                }
                else
                {
                    programs = new String[]{"no programs"};
                }
                curProgramNum = 0;
            }
            return result;
        }

        public void unload()
        {
            vashti.unloadPlugin(id);
        }

        public void setParamValue(int paramNum, float paramVal) 
        {
            parameters[paramNum].value = paramVal;
            vashti.setPluginParamValue(id, paramNum, paramVal);
        }

        public void setProgram(int progNum)
        {
            curProgramNum = progNum;
            vashti.setPluginProgram(id, progNum);
        }

        public void openEditorWindow(IntPtr editorWindow)
        {
            vashti.openEditorWindow(id, editorWindow);
        }

        public void closeEditorWindow()
        {
            vashti.closeEditorWindow(id);
        }
    }

//-----------------------------------------------------------------------------

    [StructLayout(LayoutKind.Sequential)]
    public struct PluginInfo
    {
        public string name;
        public string vendor;
        public int version;
        public int numPrograms;
        public int numParameters;
        public int numInputs;
        public int numOutputs;
        public int flags;
        public int uniqueID;
        public int editorWidth;
        public int editorHeight;
    }

    public class VSTParam
    {
        public int num;
        public String name;
        public float value;

        public VSTParam(int _num, String _name, float _val)
        {
            num = _num;
            name = _name;
            value = _val;
        }
    }

    //public class VSTProgram
    //{
    //    public int num;
    //    public String name;

    //    public VSTProgram(int _num, String _name)
    //    {
    //        num = _num;
    //        name = _name;
    //    }
    //}
}

//Console.WriteLine(" plugin " + name + " parameter " + i + " name is " + paramName);
