using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using NppPluginNET;
using KDragTool.Dialogs;

namespace KDragTool
{
    class Main
    {
        #region " Fields "
        internal const string PluginName = "KDragTool";
        static string firstFile = null;
        static bool someSetting = false;
        static Bitmap tbBmp = Properties.Resources.star;
        static Bitmap tbBmp_tbTab = Properties.Resources.star_bmp;
        #endregion

        #region " StartUp/CleanUp "
        internal static void CommandMenuInit()
        {
            StringBuilder sbIniFilePath = new StringBuilder(Win32.MAX_PATH);
            Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETPLUGINSCONFIGDIR, Win32.MAX_PATH, sbIniFilePath);
            firstFile = sbIniFilePath.ToString();
            if (!Directory.Exists(firstFile)) Directory.CreateDirectory(firstFile);
            firstFile = Path.Combine(firstFile, PluginName + ".ini");
            someSetting = (Win32.GetPrivateProfileInt("SomeSection", "SomeKey", 0, firstFile) != 0);

            PluginBase.SetCommand(0, "Unordered diff", UnorderedDiff, new ShortcutKey(false, false, false, Keys.None));
        }
        internal static void SetToolBarIcon()
        {
            toolbarIcons tbIcons = new toolbarIcons();
            tbIcons.hToolbarBmp = tbBmp.GetHbitmap();
            IntPtr pTbIcons = Marshal.AllocHGlobal(Marshal.SizeOf(tbIcons));
            Marshal.StructureToPtr(tbIcons, pTbIcons, false);
            Marshal.FreeHGlobal(pTbIcons);
        }
        #endregion

        #region " Menu functions "
        internal static void UnorderedDiff()
        {
            StringBuilder currentFilePath = new StringBuilder(Win32.MAX_PATH);
            Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETFULLCURRENTPATH, Win32.MAX_PATH, currentFilePath);
            firstFile = currentFilePath.ToString();
            var dlg = new DiffDialog(firstFile);
            dlg.Show();
        }
        #endregion
    }
}