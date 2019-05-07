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

using Audimat;
using Audimat.UI;
using Transonic.MIDI;
using Transonic.MIDI.System;

namespace Transonic.VST
{
    public class VSTPlugin
    {
        public VSTPanel panel;
        public AudimatWindow audiwin;

        public Vashti vashti;
        public String filename;

        public int audioInIdx;
        public int audioOutIdx;
        public int midiInDeviceNum;
        public PluginMidiIn midiInUnit;
        public int midiOutIdx;

        //these are supplied by the plugin
        public int id;
        public String _name;
        public String vendor;
        public int version;
        public int numPrograms;
        public int numParams;
        public int numInputs;
        public int numOutputs;
        public int flags;
        public int uniqueID;

        public VSTParam[] parameters;
        public VSTProgram[] programs;
        int curProgramNum;

        public bool hasEditor;
        public int editorWidth;
        public int editorHeight;


        public VSTPlugin(VSTPanel _panel, Vashti _vashti, String _filename)
        {
            panel = _panel;
            audiwin = panel.audiwin;
            vashti = _vashti;
            filename = _filename;

            //-1 == not set yet
            audioInIdx = -1;
            audioOutIdx = -1;
            midiInDeviceNum = -1;
            midiInUnit = null;
            midiOutIdx = -1;
        }

        public bool load()
        {
            id = vashti.loadPlugin(filename);
            bool result = (id != -1);
            if (result)
            {
                PluginInfo pluginfo = new PluginInfo();
                vashti.getPluginInfo(id, ref pluginfo);
                _name = pluginfo.name;
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
                    programs = new VSTProgram[numPrograms];
                    for (int i = 0; i < numPrograms; i++)
                    {
                        String progName = vashti.getPluginProgramName(id, i);
                        programs[i] = new VSTProgram(i, progName);
                    }
                }
                else
                {
                    programs = new VSTProgram[1];
                    programs[0] = new VSTProgram(0, "no programs");
                }
                curProgramNum = 0;
            }
            return result;
        }

        public void unload()
        {
            setMidiIn(-1);                  //disconnect midi inptu listener
            vashti.unloadPlugin(id);
        }

        public String name
        {
            get { return _name; }
        }

        //- settings ----------------------------------------------------------

        public void setAudioIn(int idx)
        {
            if (audioInIdx != idx)
            {
                audioInIdx = idx;
            }
        }

        public void setAudioOut(int idx)
        {
            if (audioOutIdx != idx)
            {
                audioOutIdx = idx;
            }
        }

        public void setMidiIn(int deviceNum)
        {
            if (midiInDeviceNum != deviceNum)
            {
                if (midiInUnit != null)
                {
                    audiwin.disconnectMidiInput(midiInDeviceNum, midiInUnit);
                }
                midiInDeviceNum = deviceNum;
                if (deviceNum != -1)
                {
                    midiInUnit = new PluginMidiIn(this);
                    audiwin.connectMidiInput(deviceNum, midiInUnit);
                }
                else
                {
                    midiInUnit = null;
                }
            }
        }

        public void setMidiOut(int idx)
        {
            if (midiOutIdx != idx)
            {
                midiOutIdx = idx;
            }
        }

        //- backend communication ---------------------------------------------

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

        public void sendMidiMessage(Message msg)
        {
            byte[] bytes = msg.getDataBytes();
            vashti.sendMidiMessage(id, bytes[0], bytes[1], bytes[2]);
        }

        public void sendMidiMessage(byte b1, byte b2, byte b3)
        {
            vashti.sendMidiMessage(id, b1, b2, b3);
        }
    }

    //-----------------------------------------------------------------------------

    //midi input listener unit
    public class PluginMidiIn : SystemUnit
    {
        public VSTPlugin plugin;

        public PluginMidiIn(VSTPlugin _plugin)
            : base(_plugin.name)
        {
            plugin = _plugin;
        }

        public override void receiveMessage(byte[] msg)
        {
            Console.WriteLine(" sending midi message {0} {1} {2}", msg[0], msg[1], msg[2]);
            plugin.sendMidiMessage(msg[0], msg[1], msg[2]);
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

    public class VSTProgram
    {
        public int num;
        public String _name;

        public String name
        {
            get { return _name; }
        }

        public VSTProgram(int _num, String __name)
        {
            num = _num;
            _name = __name;
        }
    }
}

//Console.WriteLine(" plugin " + name + " parameter " + i + " name is " + paramName);
