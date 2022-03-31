using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using OpenCvSharp;

namespace CameraDetector
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {

        IntPtr deviceHandle;
        public const uint WM_CAP = 0x400;
        public const uint WM_CAP_DRIVER_CONNECT = 0x40a;
        public const uint WM_CAP_DRIVER_DISCONNECT = 0x40b;
        public const uint WM_CAP_EDIT_COPY = 0x41e;
        public const uint WM_CAP_SET_PREVIEW = 0x432;
        public const uint WM_CAP_SET_OVERLAY = 0x433;
        public const uint WM_CAP_SET_PREVIEWRATE = 0x434;
        public const uint WM_CAP_SET_SCALE = 0x435;
        public const uint WS_CHILD = 0x40000000; 
        public const uint WS_VISIBLE = 0x10000000;

        [DllImport("avicap32.dll")]
        public extern static IntPtr capGetDriverDescription(ushort index, StringBuilder name, int nameCapacity, StringBuilder description, int descriptionCapacity);
        [DllImport("avicap32.dll")]
        public extern static IntPtr capCreateCaptureWindow(string title, uint style, int x, int y, int width, int height, IntPtr window, int id);
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public void Attach() {
            deviceHandle = capCreateCaptureWindow(string.Empty, WS_VISIBLE | WS_CHILD, 50, 50, 350, 350, new WindowInteropHelper(this).Handle, 0);
            IntPtr secondDeviceHandle = capCreateCaptureWindow(string.Empty, WS_VISIBLE | WS_CHILD, 750, 50, 350, 350, new WindowInteropHelper(this).Handle, 0);
            IntPtr thirdDeviceHandle = capCreateCaptureWindow(string.Empty, WS_VISIBLE | WS_CHILD, 50, 500, 350, 350, new WindowInteropHelper(this).Handle, 0);
            IntPtr fourthDeviceHandle = capCreateCaptureWindow(string.Empty, WS_VISIBLE | WS_CHILD, 750, 500, 350, 350, new WindowInteropHelper(this).Handle, 0);
            if (SendMessage(deviceHandle, WM_CAP_DRIVER_CONNECT, (IntPtr)0, (IntPtr)0).ToInt32() > 0) {
                SendMessage(deviceHandle, WM_CAP_SET_SCALE, (IntPtr)(-1), (IntPtr)0);
                SendMessage(deviceHandle, WM_CAP_SET_PREVIEWRATE, (IntPtr)0x42, (IntPtr)0);
                SendMessage(deviceHandle, WM_CAP_SET_PREVIEW, (IntPtr)(-1), (IntPtr)0);
                SetWindowPos(deviceHandle, new IntPtr(0), 50, 50, 350, 350, 6);
            }
            if (SendMessage(secondDeviceHandle, WM_CAP_DRIVER_CONNECT, (IntPtr)0, (IntPtr)0).ToInt32() > 0)
            {
                SendMessage(secondDeviceHandle, WM_CAP_SET_SCALE, (IntPtr)(-1), (IntPtr)0);
                SendMessage(secondDeviceHandle, WM_CAP_SET_PREVIEWRATE, (IntPtr)0x42, (IntPtr)0);
                SendMessage(secondDeviceHandle, WM_CAP_SET_PREVIEW, (IntPtr)(-1), (IntPtr)0);
                SetWindowPos(secondDeviceHandle, new IntPtr(0), 500, 50, 350, 350, 6);
            }
            if (SendMessage(thirdDeviceHandle, WM_CAP_DRIVER_CONNECT, (IntPtr)0, (IntPtr)0).ToInt32() > 0)
            {
                SendMessage(thirdDeviceHandle, WM_CAP_SET_SCALE, (IntPtr)(-1), (IntPtr)0);
                SendMessage(thirdDeviceHandle, WM_CAP_SET_PREVIEWRATE, (IntPtr)0x42, (IntPtr)0);
                SendMessage(thirdDeviceHandle, WM_CAP_SET_PREVIEW, (IntPtr)(-1), (IntPtr)0);
                SetWindowPos(thirdDeviceHandle, new IntPtr(0), 50, 500, 350, 350, 6);
            }
            if (SendMessage(fourthDeviceHandle, WM_CAP_DRIVER_CONNECT, (IntPtr)0, (IntPtr)0).ToInt32() > 0)
            {
                SendMessage(fourthDeviceHandle, WM_CAP_SET_SCALE, (IntPtr)(-1), (IntPtr)0);
                SendMessage(fourthDeviceHandle, WM_CAP_SET_PREVIEWRATE, (IntPtr)0x42, (IntPtr)0);
                SendMessage(fourthDeviceHandle, WM_CAP_SET_PREVIEW, (IntPtr)(-1), (IntPtr)0);
                SetWindowPos(fourthDeviceHandle, new IntPtr(0), 500, 500, 350, 350, 6);
            }
        }


        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Attach();
        }
    }
}
