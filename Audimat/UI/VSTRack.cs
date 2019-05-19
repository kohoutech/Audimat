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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

using Audimat.Graph;
using Transonic.VST;
using Transonic.MIDI.System;

namespace Audimat.UI
{
    public class VSTRack : UserControl
    {
        public AudimatWindow auditwin;
        public VSTHost host;
        public MidiSystem midiDevices;

        private VScrollBar scrollbar;
        private Panel panelSpace;

        public List<VSTPanel> panels;

        public VSTRig currentRig;
        public Patch currentPatch;

        public bool isRunning;
        public bool hasChanged;

        //cons
        public VSTRack(AudimatWindow _auditwin, VSTHost _host)
        {
            auditwin = _auditwin;
            host = _host;

            midiDevices = new MidiSystem();

            scrollbar = new VScrollBar();
            scrollbar.Minimum = 0;
            scrollbar.Dock = DockStyle.Right;
            scrollbar.ValueChanged += new EventHandler(scrollbar_ValueChanged);

            panelSpace = new Panel();

            panelSpace.BackColor = Color.Black;
            panelSpace.Location = new Point(0, 0);
            panelSpace.Size = new Size(0, 0);

            this.Controls.Add(scrollbar);
            this.Controls.Add(panelSpace);

            this.Size = new Size(VSTPanel.PANELWIDTH + scrollbar.Width, VSTPanel.PANELHEIGHT);        //initial size
            this.BackColor = Color.Black;

            panels = new List<VSTPanel>();

            currentRig = new VSTRig();
            currentPatch = null;

            isRunning = false;
            hasChanged = false;
        }

        void scrollbar_ValueChanged(object sender, EventArgs e)
        {
            panelSpace.Location = new Point(0, -scrollbar.Value);
        }

        //compensate for the fact that if the scrollbar max = 50, the greatest value will be 50 - THUMBWIDTH (value determined experimentally)
        const int THUMBWIDTH = 9;

        //called when the race space or panel space changes
        void updateScrollBar()
        {
            scrollbar.Maximum = ((this.Height) < panelSpace.Height) ? (panelSpace.Height - this.Height + THUMBWIDTH) : 0;
            if ((scrollbar.Maximum > THUMBWIDTH) && (scrollbar.Maximum - scrollbar.Value < THUMBWIDTH))
            {
                scrollbar.Value = scrollbar.Maximum - THUMBWIDTH;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            updateScrollBar();
        }

        public void shutdown()
        {
            host.stopEngine();
            currentRig.shutDown();
            removeAllPanels();
            midiDevices.shutdown();     //close midi devices
        }

        //- host management ----------------------------------------------------------

        public void startEngine()
        {
            host.startEngine();
            isRunning = true;
        }

        public void stopEngine()
        {
            host.stopEngine();
            isRunning = false;
        }

        //- rigging -------------------------------------------------------------------

        public bool loadRig(string rigPath)
        {
            VSTRig newRig = VSTRig.loadFromFile(rigPath);
            if (newRig != null)
            {
                //remove prev rig
                currentRig.shutDown();
                removeAllPanels();

                //install new rig
                currentRig = newRig;
                foreach (Module mod in currentRig.modules)      //add panels to rack
                {
                    VSTPanel panel = addPanel(mod.path, false);
                    if (panel != null)
                    {
                        mod.panel = panel;
                        panel.setMidiIn(mod.midiIn);
                    }
                }
                auditwin.rackProgramsChanged(null);

                hasChanged = false;
                return true;
            }
            return false;
        }

        public void clearRig()
        {
            currentRig.shutDown();
            removeAllPanels();

            currentRig = new VSTRig();
            hasChanged = false;
        }

        public void saveRig(string rigPath)
        {
            currentRig.saveToFile(rigPath);
            hasChanged = false;
        }

        //- patching ----------------------------------------------------------

        public void setCurrentPatch(int p)
        {
            currentPatch = currentRig.patches[p];
            foreach (Module mod in currentRig.modules)
            {
                int patchNum = (currentPatch.modules.ContainsKey(mod) ? currentPatch.modules[mod] : 0);
                mod.panel.cbxProgList.SelectedIndex = patchNum;
            }
        }

        public void clearPatch()
        {
            currentPatch = null;
            auditwin.rackProgramsChanged(null);
            foreach (Module mod in currentRig.modules)
            {
                mod.panel.cbxProgList.SelectedIndex = 0;
            }
        }

        public void updatePatch()
        {
            foreach (Module mod in currentRig.modules)
            {
                int patchNum = mod.panel.cbxProgList.SelectedIndex;
                if (currentPatch.modules.ContainsKey(mod))
                {
                    currentPatch.modules[mod] = patchNum;
                }
            }
        }

        public void addNewPatch(String patchName)
        {
            Patch newPatch = new Patch(patchName);
            foreach (Module mod in currentRig.modules)
            {
                int patchNum = mod.panel.cbxProgList.SelectedIndex;
                newPatch.AddModule(mod, patchNum);
            }
            currentRig.AddPatch(newPatch);
            currentPatch = newPatch;
            auditwin.rackProgramsChanged(currentPatch.name);
        }

        //- panel management ----------------------------------------------------------

        public VSTPanel addPanel(String plugPath, bool updateRig)
        {
            VSTPanel panel = new VSTPanel(this, panels.Count);
            bool result = panel.loadPlugin(plugPath);
            if (!result) return null;

            panels.Add(panel);
            panelSpace.Size = new Size(VSTPanel.PANELWIDTH, VSTPanel.PANELHEIGHT * panels.Count);
            panel.Location = new Point(0, VSTPanel.PANELHEIGHT * (panels.Count - 1));
            panelSpace.Controls.Add(panel);
            updateScrollBar();

            auditwin.rackModulesChanged();         //broadcast rack change
            if (updateRig)
            {
                Module mod = new Module(plugPath, "no output", "no input");
                mod.panel = panel;
                currentRig.AddModule(mod);
            }

            return panel;
        }

        public void removePanel(VSTPanel panel)
        {
            //remove panel from rack, shutting down plugin is handled by panel
            panelSpace.Controls.Remove(panel);
            panels.Remove(panel);
            panelSpace.Size = new Size(VSTPanel.PANELWIDTH, VSTPanel.PANELHEIGHT * panels.Count);
            for (int i = 0; i < panels.Count; i++)
            {
                panels[i].plugNum = i;
                panels[i].Location = new Point(0, VSTPanel.PANELHEIGHT * i);
            }
            updateScrollBar();
            Invalidate();

            panel.unloadPlugin();           //disconnect panel & shut down plugin
            auditwin.rackModulesChanged();         //broadcast rack change
        }

        public void removeAllPanels()
        {
            foreach (VSTPanel panel in panels)            //remove panels from rack
            {
                panelSpace.Controls.Remove(panel);
                panel.unloadPlugin();
            }
            panels.Clear();
            panelSpace.Size = new Size(0, 0);
            updateScrollBar();
            Invalidate();

            //broadcast rack change
            auditwin.rackProgramsChanged(null);
            auditwin.rackModulesChanged();
        }

        public List<VSTPlugin> getPluginList()
        {
            return host.plugins;
        }

        //- midi i/o connections ----------------------------------------------

        public void connectMidiInput(InputDevice indev, PanelMidiIn pluginMidiIn)
        {
            //InputDevice indev = midiDevices.inputDevices[idx];
            try
            {
                indev.open();
                indev.connectUnit(pluginMidiIn);
                indev.start();
            }
            catch
            {
                //Console.WriteLine("error connecting midi input");
            }
        }

        public void disconnectMidiInput(InputDevice indev, PanelMidiIn pluginMidiIn)
        {
            //InputDevice indev = midiDevices.inputDevices[idx];
            indev.disconnectUnit(pluginMidiIn);
        }

        public void connectMidiOutput(int idx, PanelMidiOut pluginMidiOut)
        {
        }

        public void disconnectMidiOutput(int idx, PanelMidiOut pluginMidiOut)
        {
        }

        //- painting ------------------------------------------------------------------

        public const int RAILWIDTH = 20;
        public const int SCREWHOLE = RAILWIDTH / 2;
        public const int SCREWOFS = (RAILWIDTH - SCREWHOLE) / 2;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            //rails in empty rack space
            Rectangle leftRail = new Rectangle(0, panelSpace.Bottom, RAILWIDTH, this.Height - panelSpace.Bottom);
            Rectangle rightRail = new Rectangle(VSTPanel.PANELWIDTH - RAILWIDTH, panelSpace.Bottom, RAILWIDTH, this.Height - panelSpace.Bottom);

            g.FillRectangle(Brushes.DarkGray, leftRail);
            g.FillRectangle(Brushes.DarkGray, rightRail);

            //screw holes on rails
            int unitCount = (this.Height + VSTPanel.PANELHEIGHT - 1) / VSTPanel.PANELHEIGHT;
            int rightOfs = VSTPanel.PANELWIDTH - SCREWHOLE - SCREWOFS;
            int bottomofs = VSTPanel.PANELHEIGHT - (SCREWHOLE * 2);
            for (int i = 0; i < unitCount; i++)
            {
                int rackofs = i * VSTPanel.PANELHEIGHT + panelSpace.Bottom;

                g.FillEllipse(Brushes.Black, SCREWOFS, rackofs + SCREWHOLE, SCREWHOLE, SCREWHOLE);
                g.FillEllipse(Brushes.Black, SCREWOFS, rackofs + bottomofs, SCREWHOLE, SCREWHOLE);
                g.FillEllipse(Brushes.Black, rightOfs, rackofs + SCREWHOLE, SCREWHOLE, SCREWHOLE);
                g.FillEllipse(Brushes.Black, rightOfs, rackofs + bottomofs, SCREWHOLE, SCREWHOLE);
            }
        }
    }
}

//Console.WriteLine(" there's no sun in the shadow of the wizard");