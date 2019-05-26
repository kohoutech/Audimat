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

using Audimat.UI;
using Origami.Serial;

namespace Audimat
{
    public class Settings
    {
        public static String VERSION = "1.3.2";

        public int rackHeight;
        public int rackPosX;
        public int rackPosY;
        public int keyWndPosX;
        public int keyWndPosY;

        public Settings()
        {
            SerialData data = new SerialData("Audimat.cfg");

            string version = data.getStringValue("version", VERSION);
            rackHeight = data.getIntValue("global-settings.rack-window-height", VSTPanel.PANELHEIGHT);
            rackPosX = data.getIntValue("global-settings.rack-window-pos.x", 100);
            rackPosY = data.getIntValue("global-settings.rack-window-pos.y", 100);
            keyWndPosX = data.getIntValue("global-settings.keyboard-window-pos.x", 200);
            keyWndPosY = data.getIntValue("global-settings.keyboard-window-pos.y", 200);
        }

        public void save()
        {
            SerialData data = new SerialData("Audimat.cfg");

            data.setStringValue("version", VERSION);
            data.setIntValue("global-settings.rack-window-height", rackHeight);
            data.setIntValue("global-settings.rack-window-pos.x", rackPosX);
            data.setIntValue("global-settings.rack-window-pos.y", rackPosY);
            data.setIntValue("global-settings.keyboard-window-pos.x", keyWndPosX);
            data.setIntValue("global-settings.keyboard-window-pos.y", keyWndPosY);

            data.saveToFile();
        }
    }
}
