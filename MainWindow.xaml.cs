// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

using GlobalStructures;
using static GlobalStructures.GlobalTools;
using System.Runtime.InteropServices;
using MFCaptureEngine;
using static MFCaptureEngine.MFCaptureEngineTools;
using System.Threading.Tasks;
using DXGI;
using Microsoft.UI.Xaml.Media.Imaging;
using System.ComponentModel;
//using Windows.Storage.Streams;
//using System.Reflection.Metadata;
using System.Text;
using DirectShow;
using Global;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUI3_MFCaptureEngine
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public delegate int SUBCLASSPROC(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, IntPtr uIdSubclass, uint dwRefData);

        [DllImport("Comctl32.dll", SetLastError = true)]
        public static extern bool SetWindowSubclass(IntPtr hWnd, SUBCLASSPROC pfnSubclass, uint uIdSubclass, uint dwRefData);

        [DllImport("Comctl32.dll", SetLastError = true)]
        public static extern int DefSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("Shlwapi.dll", SetLastError = true)]
        public static extern System.Runtime.InteropServices.ComTypes.IStream SHCreateMemStream(IntPtr pInit, uint cbInit);

        [DllImport("Ole32.dll", SetLastError = true)]
        public static extern HRESULTMF CreateStreamOnHGlobal(IntPtr hGlobal, bool fDeleteOnRelease, out System.Runtime.InteropServices.ComTypes.IStream ppstm);

        public const int GMEM_FIXED = 0x0000;
        public const int GMEM_ZEROINIT = 0x0040;

        [DllImport("Mfplat.dll", SetLastError = true, PreserveSig = true)]
        public static extern HRESULTMF MFCreateMFByteStreamOnStream(System.Runtime.InteropServices.ComTypes.IStream pStream, out IMFByteStream ppByteStream);

        [DllImport("Mfplat.dll", SetLastError = true, PreserveSig = true)]
        public static extern HRESULTMF MFCreateMFByteStreamOnStreamEx([MarshalAs(UnmanagedType.IUnknown)] object punkStream /* IRandomAccessStream */, out IMFByteStream ppByteStream);

        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern IntPtr GlobalAlloc(uint uFlags, int dwBytes);

        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern IntPtr GlobalFree(IntPtr hMem);

        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern IntPtr LocalAlloc(uint uFlags, uint uBytes);

        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern IntPtr LocalFree(IntPtr hMem);

        [DllImport("msvcrt.dll", EntryPoint = "memcpy", SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CopyMemory(byte[] dest, IntPtr src, uint count);

        [DllImport("msvcrt.dll", EntryPoint = "memcpy", SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CopyMemory(IntPtr dest, IntPtr src, uint count);

        [DllImport("Winmm.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int mciSendString(string lpszCommand, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpszReturnString, int cchReturn, IntPtr hwndCallback);


        private IntPtr hWndMain = IntPtr.Zero;
        private Microsoft.UI.Windowing.AppWindow _apw;

        CCaptureEngine captureEngine = null;
        private SUBCLASSPROC SubClassDelegate;

        System.Collections.ObjectModel.ObservableCollection<string> rotate = new System.Collections.ObjectModel.ObservableCollection<string>();

        string m_sClickFileName = "";
        //string m_sAliasName = "";

        object g_FilterGraph = null;
        IMediaControl g_pMC = null;
        IMediaEventEx g_pME = null;
        IGraphBuilder g_pGraph = null;
        IMediaSeeking g_pMS = null;

        public MainWindow()
        {
            this.InitializeComponent();
           
            Application.Current.Resources["ButtonBackgroundPointerOver"] = new SolidColorBrush(Microsoft.UI.Colors.LightSteelBlue);
            Application.Current.Resources["ButtonBackgroundPressed"] = new SolidColorBrush(Microsoft.UI.Colors.RoyalBlue);

            hWndMain = WinRT.Interop.WindowNative.GetWindowHandle(this);
            Microsoft.UI.WindowId myWndId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWndMain);
            _apw = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(myWndId);

            _apw.Resize(new Windows.Graphics.SizeInt32(1100, 800));
            _apw.Move(new Windows.Graphics.PointInt32(400, 200));

            SubClassDelegate = new SUBCLASSPROC(WindowSubClass);
            bool bRet = SetWindowSubclass(hWndMain, SubClassDelegate, 0, 0);

            rotate.Add("0");
            rotate.Add("90");
            rotate.Add("180");
            rotate.Add("270");
            cmbRotate.SelectedIndex = 0;
           
            m_sClickFileName = @"Assets\Camera_Click.mp3"; 
            Guid CLSID_FilterGraph = new Guid("E436EBB3-524F-11CE-9F53-0020AF0BA770");
            g_FilterGraph = Activator.CreateInstance(Type.GetTypeFromCLSID(CLSID_FilterGraph));
            g_pGraph = (IGraphBuilder)g_FilterGraph;
            g_pMC = (IMediaControl)g_FilterGraph;
            g_pME = (IMediaEventEx)g_FilterGraph;
            g_pMS = (IMediaSeeking)g_FilterGraph;
            g_pGraph.RenderFile(m_sClickFileName, null);

            this.Closed += MainWindow_Closed;

            captureEngine = new CCaptureEngine(this);
            HRESULTMF hr = captureEngine.Initialize(hWndMain, IntPtr.Zero, IntPtr.Zero);
        }

        private void btn_PreviewVideo_Click(object sender, RoutedEventArgs e)
        {
            HRESULTMF hr = HRESULTMF.S_OK;
            if (captureEngine != null)
            {
                if (!captureEngine.m_bPreview)
                {                   
                    hr = captureEngine.StartPreview();                              
                }
                else
                {
                    hr = captureEngine.StopPreview();                   
                }
            }
        }

        private async void btn_RecordVideo_Click(object sender, RoutedEventArgs e)
        {
            if (tbFileVideo.Text == "")
            {
                Windows.UI.Popups.MessageDialog md = new Windows.UI.Popups.MessageDialog("File name to record the video must be filled", "Information");
                WinRT.Interop.InitializeWithWindow.Initialize(md, hWndMain);
                _ = await md.ShowAsync();
            }           
            else
            {
                RecordVideo();
            }
        }

        private async void RecordVideo()
        {
            HRESULTMF hr = HRESULTMF.S_OK;
            if (captureEngine != null)
            {
                if (!captureEngine.m_bRecording)
                {
                    Guid guidVideoEncoding = Guid.Empty;
                    Guid guidAudioEncoding = Guid.Empty;
                    try
                    {
                        using (System.IO.File.Create(tbFileVideo.Text)) { };
                        var file = await Windows.Storage.StorageFile.GetFileFromPathAsync(tbFileVideo.Text);
                        switch (file.FileType)
                        {
                            case ".mp4":
                                guidVideoEncoding = MFVideoFormat_H264;
                                guidAudioEncoding = MFAudioFormat_AAC;
                                break;
                            case ".wmv":
                                guidVideoEncoding = MFVideoFormat_WMV3;
                                guidAudioEncoding = MFAudioFormat_WMAudioV9;
                                break;
                        }
                        hr = captureEngine.StartRecord(tbFileVideo.Text, guidVideoEncoding, guidAudioEncoding);
                        if (hr == HRESULTMF.S_OK)
                        {
                            fi_RecordVideo.Glyph = "\u23F9";
                            ttip_RecordVideo.Content = "Stop recording video";
                        }
                    }
                    catch (Exception e)
                    {
                        Windows.UI.Popups.MessageDialog md = new Windows.UI.Popups.MessageDialog("Cannot save file " + tbFileVideo.Text + "\r\n" + "Exception : " + e.Message, "Information");
                        WinRT.Interop.InitializeWithWindow.Initialize(md, hWndMain);
                        _ = await md.ShowAsync();
                    }
                }
                else
                {
                    hr = captureEngine.StopRecord();
                    // https://emojipedia.org/movie-camera/
                    // https://www.unicode.org/emoji/charts/full-emoji-list.html
                    if (hr == HRESULTMF.S_OK)
                    {
                        fi_RecordVideo.Glyph = "\U0001F3A5";
                        ttip_RecordVideo.Content = "Start recording video";
                    }
                }
            }
        }

        private async void btnBrowseVideo_Click(object sender, RoutedEventArgs e)
        {
            bool bRet = await RecordVideoDialogPicker();
        }

        private async Task<bool> RecordVideoDialogPicker()
        {
            try
            {
                var fsp = new Windows.Storage.Pickers.FileSavePicker();
                WinRT.Interop.InitializeWithWindow.Initialize(fsp, hWndMain);
                fsp.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
                fsp.SuggestedFileName = "New Video";

                fsp.FileTypeChoices.Add("MP4 (H.264/AAC) (*.mp4)", new List<string>() { ".mp4" });
                fsp.FileTypeChoices.Add("Windows Media Video (*.wmv)", new List<string>() { ".wmv" });

                Windows.Storage.StorageFile file = await fsp.PickSaveFileAsync();
                if (file != null)
                {
                    tbFileVideo.Text = file.Path;
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                Windows.UI.Popups.MessageDialog md = new Windows.UI.Popups.MessageDialog("Cannot save file " + tbFileImage.Text + "\r\n" + "Exception : " + e.Message, "Information");
                WinRT.Interop.InitializeWithWindow.Initialize(md, hWndMain);
                _ = await md.ShowAsync();
                return false;
            }
        }

        byte[] m_pBytesArray;

        private async void CopyImage()
        {
            uint nStride = captureEngine.m_nWidth * 4;
            int nSize = (int)(nStride * captureEngine.m_nHeight);

            System.Runtime.InteropServices.ComTypes.IStream pStream = SHCreateMemStream(IntPtr.Zero, 0);
            if (pStream != null)
            {
                IMFByteStream pMFByteStream = null;
                HRESULTMF hr = MFCreateMFByteStreamOnStream(pStream, out pMFByteStream);
                if (hr == HRESULTMF.S_OK)
                {
                    hr = captureEngine.TakePhoto(null, GUID_ContainerFormatBmp, ref pMFByteStream);
                    if (hr == HRESULTMF.S_OK)
                    {
                        IntPtr pData = Marshal.AllocHGlobal(nSize);
                        pMFByteStream.Read(pData, (uint)nSize, out uint nRead);
                        IntPtr pData2 = Marshal.AllocHGlobal(nSize);
                        for (IntPtr pBytesDest = pData2, pBytesSrc = pData + nSize; pBytesSrc.ToInt64() > pData.ToInt64(); pBytesDest += (int)nStride, pBytesSrc -= (int)nStride)
                            CopyMemory(pBytesDest, (IntPtr)(pBytesSrc - (int)nStride), nStride);
                        Marshal.Copy(pData2, m_pBytesArray, 0, nSize);

                        Microsoft.UI.Xaml.Media.Imaging.WriteableBitmap wb = new Microsoft.UI.Xaml.Media.Imaging.WriteableBitmap((int)captureEngine.m_nWidth, (int)captureEngine.m_nHeight);
                        await wb.PixelBuffer.AsStream().WriteAsync(m_pBytesArray, 0, m_pBytesArray.Length);
                        img1.Source = wb;

                        Marshal.FreeHGlobal(pData2);
                        Marshal.FreeHGlobal(pData);
                    }
                    Marshal.ReleaseComObject(pMFByteStream);
                }
                Marshal.ReleaseComObject(pStream);
            }
        }

        private async void btn_SaveImage_Click(object sender, RoutedEventArgs e)
        {  
            if (tbFileImage.Text == "")
            {
                Windows.UI.Popups.MessageDialog md = new Windows.UI.Popups.MessageDialog("File name to save a frame must be filled", "Information");
                WinRT.Interop.InitializeWithWindow.Initialize(md, hWndMain);
                _ = await md.ShowAsync();
            }
            else
            {
                SaveImage();
            }
        }

        private async void SaveImage()
        {
            if (captureEngine != null)
            {
                //if (!captureEngine.m_bRecording)
                {
                    Guid guidCodec = Guid.Empty;
                    try
                    {
                        using (System.IO.File.Create(tbFileImage.Text)) { };
                        var file = await Windows.Storage.StorageFile.GetFileFromPathAsync(tbFileImage.Text);
                        switch (file.FileType)
                        {
                            case ".jpg":                                
                                guidCodec = GUID_ContainerFormatJpeg;
                                break;
                            case ".png":
                                guidCodec = GUID_ContainerFormatPng;
                                break;
                            case ".gif":
                                guidCodec = GUID_ContainerFormatGif;
                                break;
                            case ".bmp":
                                guidCodec = GUID_ContainerFormatBmp;
                                break;
                            case ".tif":
                                guidCodec = GUID_ContainerFormatTiff;
                                break;
                        }
                        IMFByteStream pMFByteStream = null;
                        HRESULTMF hr = captureEngine.TakePhoto(tbFileImage.Text, guidCodec, ref pMFByteStream);
                        if (hr == HRESULTMF.S_OK)
                        {
                            hr = g_pMS.SetPositions(DsLong.FromInt64(0), AM_SEEKING_SeekingFlags.AM_SEEKING_AbsolutePositioning, null, AM_SEEKING_SeekingFlags.AM_SEEKING_NoPositioning);
                            hr = g_pMC.Run(); 
                        }
                    }
                    catch (Exception e)
                    {
                        Windows.UI.Popups.MessageDialog md = new Windows.UI.Popups.MessageDialog("Cannot save file " + tbFileImage.Text + "\r\n" + "Exception : " + e.Message, "Information");
                        WinRT.Interop.InitializeWithWindow.Initialize(md, hWndMain);
                        _ = await md.ShowAsync();
                    }
                }
                //else
                //{
                //    captureEngine.StopRecord();
                //    // https://emojipedia.org/movie-camera/
                //    // https://www.unicode.org/emoji/charts/full-emoji-list.html
                //    fi_RecordVideo.Glyph = "\U0001F3A5";
                //    ttip_RecordVideo.Content = "Start recording video";
                //}
            }
        }

        private async void btnBrowseImage_Click(object sender, RoutedEventArgs e)
        {
            bool bRet = await SaveImageDialogPicker();
        }

        private async Task<bool> SaveImageDialogPicker()
        {
            var fsp = new Windows.Storage.Pickers.FileSavePicker();
            WinRT.Interop.InitializeWithWindow.Initialize(fsp, hWndMain);
            fsp.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            fsp.SuggestedFileName = "New Image";

            fsp.FileTypeChoices.Add("JPG Joint Photographic Experts Group (*.jpg)", new List<string>() { ".jpg" });
            fsp.FileTypeChoices.Add("PNG Portable Network Graphics (*.png)", new List<string>() { ".png" });
            fsp.FileTypeChoices.Add("GIF Graphics Interchange Format (*.gif)", new List<string>() { ".gif" });
            fsp.FileTypeChoices.Add("BMP Windows Bitmap (*.bmp)", new List<string>() { ".bmp" });
            fsp.FileTypeChoices.Add("TIF Tagged Image File Format (*.tif)", new List<string>() { ".tif" });

            Windows.Storage.StorageFile file = await fsp.PickSaveFileAsync();
            if (file != null)
            {
                tbFileImage.Text = file.Path;
                return true;
            }
            else
                return false;
        }

        private void tsMirror_Toggled(object sender, RoutedEventArgs e)
        {
            if (captureEngine != null)
            {
                ToggleSwitch ts = sender as ToggleSwitch;
                MFError.HRESULTMF hr = captureEngine.SetMirrorState(ts.IsOn);
                if (hr != MFError.HRESULTMF.S_OK)
                {
                    ts.IsOn = false;
                }
            }
        }

        private void cmbRotate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (captureEngine != null)
            {
                if (e.AddedItems.Count > 0)
                {             
                    var item = e.AddedItems[0];
                    if (item != null)
                    {
                        captureEngine.SetRotation(false,  0, (uint)Convert.ToInt32(item.ToString()));
                    }
                }
            }
        }

        private int WindowSubClass(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, IntPtr uIdSubclass, uint dwRefData)
        {
            switch (uMsg)
            {
                case CCaptureEngine.WM_APP_CAPTURE_EVENT:
                    {
                        if (wParam != IntPtr.Zero)
                        {
                            IMFMediaEvent pMediaEvent = Marshal.GetObjectForIUnknown(wParam) as IMFMediaEvent;
                            if (pMediaEvent != null)
                            {
                                HRESULTMF hrStatus;
                                HRESULTMF hr = pMediaEvent.GetStatus(out hrStatus);
                                MediaEventType nType = 0;
                                hr = pMediaEvent.GetType(out nType);
                                Guid guidType;
                                hr = pMediaEvent.GetExtendedType(out guidType);
                                if (guidType == MF_CAPTURE_ENGINE_INITIALIZED)
                                {
                                    hr = captureEngine.GetDeviceData();
                                    //SafeRelease(ref pMediaEvent);
                                    //Console.Beep(1000, 10);
                                    //if (captureEngine != null)
                                    //{
                                    //    System.Threading.Thread.Sleep(200);
                                    //    hr = captureEngine.StartPreview();
                                    //}
                                }
                                else if (guidType == MF_CAPTURE_ENGINE_PREVIEW_STARTED)
                                {
                                    var item = cmbRotate.SelectedItem;
                                    if (item != null)
                                    {
                                        captureEngine.SetRotation(false, 0, (uint)Convert.ToInt32(item.ToString()));
                                    }
                                    btn_PreviewVideo.Background = new SolidColorBrush(Microsoft.UI.Colors.LightGreen);
                                }
                                else if (guidType == MF_CAPTURE_ENGINE_PREVIEW_STOPPED)
                                {
                                    //img1.Source = new BitmapImage(new Uri("ms-appx:///Assets/webcam.jpg"));
                                    System.Threading.Thread.Sleep(200);
                                    CopyImage();
                                    btn_PreviewVideo.Background = new SolidColorBrush(Microsoft.UI.Colors.MidnightBlue);
                                }
                                else if (guidType == MF_CAPTURE_ENGINE_RECORD_STARTED)
                                {
                                    var item = cmbRotate.SelectedItem;
                                    if (item != null)
                                    {
                                        captureEngine.SetRotation(true, 0, (uint)Convert.ToInt32(item.ToString()));
                                        btnBrowseVideo.IsEnabled = false;
                                        tbFileVideo.IsEnabled = false;
                                    }
                                }
                                else if (guidType == MF_CAPTURE_ENGINE_RECORD_STOPPED)
                                {
                                    btnBrowseVideo.IsEnabled = true;
                                    tbFileVideo.IsEnabled = true;
                                }
                                else if (guidType == MF_CAPTURE_ENGINE_PHOTO_TAKEN)
                                {

                                }
                                //captureEngine.SetCaptureEvent();
                                SafeRelease(ref pMediaEvent);
                            }
                        }
                        else
                        {
                            if (lParam == (IntPtr)1)
                            {                                
                                int nSize = (int)(captureEngine.m_nWidth * 4 * captureEngine.m_nHeight);
                                m_pBytesArray = new byte[nSize];
                                //CopyImage();                    
                            }
                        }
                    }
                    break;
            }
            return DefSubclassProc(hWnd, uMsg, wParam, lParam);
        }

        void Clean()
        {
            SafeRelease(ref g_FilterGraph);
            if (captureEngine != null)
            	((IDisposable)captureEngine).Dispose();
        }

        private void MainWindow_Closed(object sender, WindowEventArgs args)
        {
            Clean();
        }
    }   
}
