using System;
using System.Runtime.InteropServices;
using GlobalStructures;

namespace DirectShow
{
    internal class DirectShowTools
    {
        public static Guid CLSID_FilterGraph = new Guid("E436EBB3-524F-11CE-9F53-0020AF0BA770");
        public static Guid CLSID_CaptureGraphBuilder2 = new Guid("BF87B6E1-8C27-11d0-B3F0-00AA003761C5");
        public static Guid CLSID_VideoMixingRenderer9 = new Guid("{51B4ABF3-748F-4E3B-A276-C828330E926A}");
        public static Guid CLSID_SystemDeviceEnum = new Guid("62BE5D10-60EB-11D0-BD3B-00A0C911CE86");
        public static Guid CLSID_VideoInputDeviceCategory = new Guid("860BB310-5D01-11D0-BD3B-00A0C911CE86");
        public static Guid CLSID_AudioRendererCategory = new Guid("E0F158E1-CB04-11D0-BD4E-00A0C911CE86");      
        public static Guid PIN_CATEGORY_PREVIEW = new Guid("fb6c4282-0353-11d1-905f-0000c0cc16ba");
        public static Guid MEDIATYPE_Video = new Guid("73646976-0000-0010-8000-00AA00389B71");       
    }

    [ComImport()]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("56a868a9-0ad4-11ce-b03a-0020af0ba770")]
    public interface IGraphBuilder : IFilterGraph
    {
        #region IFilterGraph
        new HRESULTMF AddFilter(IBaseFilter pFilter, string pName);
        new HRESULTMF RemoveFilter(IBaseFilter pFilter);
        new HRESULTMF EnumFilters(out IEnumFilters ppEnum);
        new HRESULTMF FindFilterByName(string pName, out IBaseFilter ppFilter);
        new HRESULTMF ConnectDirect(IPin ppinOut, IPin ppinIn, ref AM_MEDIA_TYPE pmt);
        new HRESULTMF Reconnect(IPin ppin);
        new HRESULTMF Disconnect(IPin ppin);
        new HRESULTMF SetDefaultSyncSource();
        #endregion

        HRESULTMF Connect(IPin ppinOut, IPin ppinIn);
        HRESULTMF Render(IPin ppinOut);
        HRESULTMF RenderFile(string lpcwstrFile, string lpcwstrPlayList);
        HRESULTMF AddSourceFilter(string lpcwstrFileName, string lpcwstrFilterName, out IBaseFilter ppFilter);
        HRESULTMF SetLogFile(IntPtr hFile);
        HRESULTMF Abort();
        HRESULTMF ShouldOperationContinue();
    }

    [ComImport()]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("36b73882-c2c8-11cf-8b46-00805f6cef60")]
    public interface IFilterGraph2 : IGraphBuilder
    {
        #region IGraphBuilder
        #region IFilterGraph
        new HRESULTMF AddFilter(IBaseFilter pFilter, string pName);
        new HRESULTMF RemoveFilter(IBaseFilter pFilter);
        new HRESULTMF EnumFilters(out IEnumFilters ppEnum);
        new HRESULTMF FindFilterByName(string pName, out IBaseFilter ppFilter);
        new HRESULTMF ConnectDirect(IPin ppinOut, IPin ppinIn, ref AM_MEDIA_TYPE pmt);
        new HRESULTMF Reconnect(IPin ppin);
        new HRESULTMF Disconnect(IPin ppin);
        new HRESULTMF SetDefaultSyncSource();
        #endregion

        new HRESULTMF Connect(IPin ppinOut, IPin ppinIn);
        new HRESULTMF Render(IPin ppinOut);
        new HRESULTMF RenderFile(string lpcwstrFile, string lpcwstrPlayList);
        new HRESULTMF AddSourceFilter(string lpcwstrFileName, string lpcwstrFilterName, out IBaseFilter ppFilter);
        new HRESULTMF SetLogFile(IntPtr hFile);
        new HRESULTMF Abort();
        new HRESULTMF ShouldOperationContinue();
        #endregion

        //HRESULT AddSourceFilterForMoniker(IMoniker pMoniker, IBindCtx pCtx, string lpcwstrFilterName, out IBaseFilter ppFilter);
        HRESULTMF AddSourceFilterForMoniker(IMoniker pMoniker, IntPtr pCtx, string lpcwstrFilterName, out IBaseFilter ppFilter);
        HRESULTMF ReconnectEx(IPin ppin, AM_MEDIA_TYPE pmt);
        HRESULTMF RenderEx(IPin pPinOut, uint dwFlags, ref uint pvContext);
    }

    public enum AM_RENSDEREXFLAGS
    {
        AM_RENDEREX_RENDERTOEXISTINGRENDERERS = 0x1
    };

    [ComImport()]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("56a8689f-0ad4-11ce-b03a-0020af0ba770")]
    public interface IFilterGraph
    {
        HRESULTMF AddFilter(IBaseFilter pFilter, string pName);
        HRESULTMF RemoveFilter(IBaseFilter pFilter);
        HRESULTMF EnumFilters(out IEnumFilters ppEnum);
        HRESULTMF FindFilterByName(string pName, out IBaseFilter ppFilter);
        HRESULTMF ConnectDirect(IPin ppinOut, IPin ppinIn, ref AM_MEDIA_TYPE pmt);
        HRESULTMF Reconnect(IPin ppin);
        HRESULTMF Disconnect(IPin ppin);
        HRESULTMF SetDefaultSyncSource();
    }

    [ComImport()]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("56a86893-0ad4-11ce-b03a-0020af0ba770")]
    public interface IEnumFilters
    {
        HRESULTMF Next(uint cFilters, out IBaseFilter ppFilter, out uint pcFetched);
        HRESULTMF Skip(uint cFilters);
        HRESULTMF Reset();
        HRESULTMF Clone(out IEnumFilters ppEnum);
    }

    [ComImport()]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("0000010c-0000-0000-C000-000000000046")]
    public interface IPersist
    {
        HRESULTMF GetClassID(out Guid pClassID);
    }

    [ComImport()]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("56a86899-0ad4-11ce-b03a-0020af0ba770")]
    public interface IMediaFilter : IPersist
    {
        #region IPersist
        new HRESULTMF GetClassID(out Guid pClassID);
        #endregion
        HRESULTMF Stop();
        HRESULTMF Pause();
        HRESULTMF Run(Int64 tStart);
        HRESULTMF GetState(int dwMilliSecsTimeout, out FILTER_STATE State);
        HRESULTMF SetSyncSource(IntPtr pClock);
        HRESULTMF GetSyncSource(out IntPtr pClock);
        //HRESULT SetSyncSource(IReferenceClock pClock);
        //HRESULT GetSyncSource(out IReferenceClock pClock);
    }

    [ComImport()]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("56a86895-0ad4-11ce-b03a-0020af0ba770")]
    public interface IBaseFilter : IMediaFilter
    {
        #region IMediaFilter
        #region IPersist
        new HRESULTMF GetClassID(out Guid pClassID);
        #endregion
        new HRESULTMF Stop();
        new HRESULTMF Pause();
        new HRESULTMF Run(Int64 tStart);
        new HRESULTMF GetState(int dwMilliSecsTimeout, out FILTER_STATE State);
        new HRESULTMF SetSyncSource(IntPtr pClock);
        new HRESULTMF GetSyncSource(out IntPtr pClock);
        #endregion

        HRESULTMF EnumPins(out IEnumPins ppEnum);
        HRESULTMF FindPin(string Id, out IPin ppPin);
        HRESULTMF QueryFilterInfo(out FILTER_INFO pInfo);
        HRESULTMF JoinFilterGraph(IFilterGraph pGraph, string pName);
        HRESULTMF QueryVendorInfo(out string pVendorInfo);
    }

    [ComImport()]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("56a86892-0ad4-11ce-b03a-0020af0ba770")]
    public interface IEnumPins
    {
        HRESULTMF Next(uint cPins, out IPin ppPins, out uint pcFetched);
        HRESULTMF Skip(uint cPins);
        HRESULTMF Reset();
        HRESULTMF Clone(out IEnumPins ppEnum);
    }

    [ComImport()]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("56a86891-0ad4-11ce-b03a-0020af0ba770")]
    public interface IPin
    {
        HRESULTMF Connect(IPin pReceivePin, ref AM_MEDIA_TYPE pmt);
        HRESULTMF ReceiveConnection(IPin pConnector, ref AM_MEDIA_TYPE pmt);
        HRESULTMF Disconnect();
        HRESULTMF ConnectedTo(out IPin pPin);
        HRESULTMF ConnectionMediaType(out AM_MEDIA_TYPE pmt);
        HRESULTMF QueryPinInfo(out PIN_INFO pInfo);
        HRESULTMF QueryDirection(out PIN_DIRECTION pPinDir);
        HRESULTMF QueryId(out string Id);
        HRESULTMF QueryAccept(ref AM_MEDIA_TYPE pmt);
        HRESULTMF EnumMediaTypes(out IEnumMediaTypes ppEnum);
        HRESULTMF QueryInternalConnections(out IPin apPin, ref uint nPin);
        HRESULTMF EndOfStream();
        HRESULTMF BeginFlush();
        HRESULTMF EndFlush();
        HRESULTMF NewSegment(Int64 tStart, Int64 tStop, double dRate);
    }

    [ComImport()]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("89c31040-846b-11ce-97d3-00aa0055595a")]
    public interface IEnumMediaTypes
    {
        HRESULTMF Next(uint cMediaTypes, out AM_MEDIA_TYPE ppMediaTypes, out uint pcFetched);
        HRESULTMF Skip(uint cMediaTypes);
        HRESULTMF Reset();
        HRESULTMF Clone(out IEnumMediaTypes ppEnum);
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct AM_MEDIA_TYPE
    {
        public Guid majortype;
        public Guid subtype;
        public bool bFixedSizeSamples;
        public bool bTemporalCompression;
        public uint lSampleSize;
        public Guid formattype;
        // public pUnk As IUnknown
        public IntPtr pUnk;
        public uint cbFormat;
        public byte pbFormat;
    }

    public enum PIN_DIRECTION : int
    {
        PINDIR_INPUT = 0,
        PINDIR_OUTPUT = (PINDIR_INPUT + 1)
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class PIN_INFO
    {
        public IBaseFilter filter;
        public PIN_DIRECTION dir;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string name;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class FILTER_INFO
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string achName;
        [MarshalAs(UnmanagedType.IUnknown)] // IFilterGraph
        public object pUnk;
    }

    public enum FILTER_STATE
    {
        State_Stopped = 0,
        State_Paused = (State_Stopped + 1),
        State_Running = (State_Paused + 1)
    }

    [ComImport()]
    [Guid("56A868B1-0AD4-11CE-B03A-0020AF0BA770")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IMediaControl
    {
        [PreserveSig]
        HRESULTMF Run();
        [PreserveSig]
        HRESULTMF Pause();
        [PreserveSig]
        HRESULTMF Stop();
        [PreserveSig]
        HRESULTMF GetState(int msTimeout, out int pfs);
        [PreserveSig]
        HRESULTMF RenderFile(string strFilename);
        [PreserveSig]
        HRESULTMF AddSourceFilter(string strFilename, out object ppUnk);
        [PreserveSig]
        HRESULTMF get_FilterCollection(out object ppUnk);
        [PreserveSig]
        HRESULTMF get_RegFilterCollection(out object ppUnk);
        //HRESULT AddSourceFilter(string strFilename, out IDispatch ppUnk);
        //HRESULT get_FilterCollection(out IDispatch ppUnk);
        //HRESULT get_RegFilterCollection(out IDispatch ppUnk);
        [PreserveSig]
        HRESULTMF StopWhenReady();
    }

    [ComImport()]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    [Guid("56a868b4-0ad4-11ce-b03a-0020af0ba770")]
    public interface IVideoWindow
    {
        HRESULTMF put_Caption(string strCaption);
        HRESULTMF get_Caption(out string strCaption);
        HRESULTMF put_WindowStyle(int WindowStyle);
        HRESULTMF get_WindowStyle(out int WindowStyle);
        HRESULTMF put_WindowStyleEx(int WindowStyleEx);
        HRESULTMF get_WindowStyleEx(out int WindowStyleEx);
        HRESULTMF put_AutoShow(int AutoShow);
        HRESULTMF get_AutoShow(out int AutoShow);
        HRESULTMF put_WindowState(int WindowState);
        HRESULTMF get_WindowState(out int WindowState);
        HRESULTMF put_BackgroundPalette(int BackgroundPalette);
        HRESULTMF get_BackgroundPalette(out int pBackgroundPalette);
        [PreserveSig]
        HRESULTMF put_Visible(int Visible);
        HRESULTMF get_Visible(out int pVisible);
        HRESULTMF put_Left(int Left);
        HRESULTMF get_Left(out int pLeft);
        HRESULTMF put_Width(int Width);
        HRESULTMF get_Width(out int pWidth);
        HRESULTMF put_Top(int Top);
        HRESULTMF get_Top(out int pTop);
        HRESULTMF put_Height(int Height);
        HRESULTMF get_Height(out int pHeight);
        [PreserveSig]
        HRESULTMF put_Owner(IntPtr Owner);
        HRESULTMF get_Owner(out IntPtr Owner);
        HRESULTMF put_MessageDrain(IntPtr Drain);
        HRESULTMF get_MessageDrain(out IntPtr Drain);
        HRESULTMF get_BorderColor(out int Color);
        HRESULTMF put_BorderColor(int Color);
        HRESULTMF get_FullScreenMode(out int FullScreenMode);
        HRESULTMF put_FullScreenMode(int FullScreenMode);
        HRESULTMF SetWindowForeground(int Focus);
        HRESULTMF NotifyOwnerMessage(IntPtr hwnd, int uMsg, int wParam, IntPtr lParam);
        [PreserveSig]
        HRESULTMF SetWindowPosition(int Left, int Top, int Width, int Height);
        HRESULTMF GetWindowPosition(out int pLeft, out int pTop, out int pWidth, out int pHeight);
        HRESULTMF GetMinIdealImageSize(out int pWidth, out int pHeight);
        HRESULTMF GetMaxIdealImageSize(out int pWidth, out int pHeight);
        HRESULTMF GetRestorePosition(out int pLeft, out int pTop, out int pWidth, out int pHeight);
        HRESULTMF HideCursor(int HideCursor);
        HRESULTMF IsCursorHidden(out int CursorHidden);
    }

    [ComImport()]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    [Guid("56a868c0-0ad4-11ce-b03a-0020af0ba770")]
    public interface IMediaEventEx : IMediaEvent
    {
        #region IMediaEvent
        new HRESULTMF GetEventHandle(out IntPtr hEvent);
        [PreserveSig]
        new HRESULTMF GetEvent(out int lEventCode, out IntPtr lParam1, out IntPtr lParam2, int msTimeout);
        new HRESULTMF WaitForCompletion(int msTimeout, out int pEvCode);
        new HRESULTMF CancelDefaultHandling(long lEvCode);
        new HRESULTMF RestoreDefaultHandling(int lEvCode);
        new HRESULTMF FreeEventParams(int lEvCode, IntPtr lParam1, IntPtr lParam2);
        #endregion

        HRESULTMF SetNotifyWindow(IntPtr hwnd, int lMsg, IntPtr lInstanceData);
        HRESULTMF SetNotifyFlags(int lNoNotifyFlags);
        HRESULTMF GetNotifyFlags(out int lplNoNotifyFlags);
    }

    [ComImport()]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    [Guid("56a868b6-0ad4-11ce-b03a-0020af0ba770")]
    public interface IMediaEvent
    {
        HRESULTMF GetEventHandle(out IntPtr hEvent);
        [PreserveSig]
        HRESULTMF GetEvent(out int lEventCode, out IntPtr lParam1, out IntPtr lParam2, int msTimeout);
        HRESULTMF WaitForCompletion(int msTimeout, out int pEvCode);
        HRESULTMF CancelDefaultHandling(long lEvCode);
        HRESULTMF RestoreDefaultHandling(int lEvCode);
        HRESULTMF FreeEventParams(int lEvCode, IntPtr lParam1, IntPtr lParam2);
    }

    [ComImport()]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("36b73880-c2c8-11cf-8b46-00805f6cef60")]
    public interface IMediaSeeking
    {
        [PreserveSig]
        HRESULTMF GetCapabilities(out uint pCapabilities);
        [PreserveSig]
        HRESULTMF CheckCapabilities(ref uint pCapabilities);
        [PreserveSig]
        HRESULTMF IsFormatSupported(ref Guid pFormat);
        [PreserveSig]
        HRESULTMF QueryPreferredFormat(out Guid pFormat);
        [PreserveSig]
        HRESULTMF GetTimeFormat(out Guid pFormat);
        [PreserveSig]
        HRESULTMF IsUsingTimeFormat(ref Guid pFormat);
        [PreserveSig]
        HRESULTMF SetTimeFormat(ref Guid pFormat);
        [PreserveSig]
        HRESULTMF GetDuration(out long pDuration);
        [PreserveSig]
        HRESULTMF GetStopPosition(out long pStop);
        [PreserveSig]
        HRESULTMF GetCurrentPosition(out long pCurrent);
        [PreserveSig]
        HRESULTMF ConvertTimeFormat(out long pTarget, ref Guid pTargetFormat, long Source, ref Guid pSourceFormat);
        //HRESULT SetPositions(ref long pCurrent, uint dwCurrentFlags, ref long pStop, uint dwStopFlags);
        [PreserveSig]
        HRESULTMF SetPositions([In, Out, MarshalAs(UnmanagedType.LPStruct)] DsLong pCurrent, [In] AM_SEEKING_SeekingFlags dwCurrentFlags,
            [In, Out, MarshalAs(UnmanagedType.LPStruct)] DsLong pStop, [In] AM_SEEKING_SeekingFlags dwStopFlags);
        [PreserveSig]
        HRESULTMF GetPositions(long pCurrent, long pStop);
        [PreserveSig]
        HRESULTMF GetAvailable(long pEarliest, long pLatest);
        [PreserveSig]
        HRESULTMF SetRate(double dRate);
        [PreserveSig]
        HRESULTMF GetRate(out double pdRate);
        [PreserveSig]
        HRESULTMF GetPreroll(out long pllPreroll);
    }

    // Copied from DirectShowLib

    /// <summary>
    /// DirectShowLib.DsLong is a wrapper class around a <see cref="System.Int64"/> value type.
    /// </summary>
    /// <remarks>
    /// This class is necessary to enable null paramters passing.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public class DsLong
    {
        private long Value;

        /// <summary>
        /// Constructor
        /// Initialize a new instance of DirectShowLib.DsLong with the Value parameter
        /// </summary>
        /// <param name="Value">Value to assign to this new instance</param>
        public DsLong(long Value)
        {
            this.Value = Value;
        }

        /// <summary>
        /// Get a string representation of this DirectShowLib.DsLong Instance.
        /// </summary>
        /// <returns>A string representing this instance</returns>
        public override string ToString()
        {
            return this.Value.ToString();
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        /// <summary>
        /// Define implicit cast between DirectShowLib.DsLong and System.Int64 for languages supporting this feature.
        /// VB.Net doesn't support implicit cast. <see cref="DirectShowLib.DsLong.ToInt64"/> for similar functionality.
        /// <code>
        ///   // Define a new DsLong instance
        ///   DsLong dsL = new DsLong(9876543210);
        ///   // Do implicit cast between DsLong and Int64
        ///   long l = dsL;
        ///
        ///   Console.WriteLine(l.ToString());
        /// </code>
        /// </summary>
        /// <param name="g">DirectShowLib.DsLong to be cast</param>
        /// <returns>A casted System.Int64</returns>
        public static implicit operator long(DsLong l)
        {
            return l.Value;
        }

        /// <summary>
        /// Define implicit cast between System.Int64 and DirectShowLib.DsLong for languages supporting this feature.
        /// VB.Net doesn't support implicit cast. <see cref="DirectShowLib.DsGuid.FromInt64"/> for similar functionality.
        /// <code>
        ///   // Define a new Int64 instance
        ///   long l = 9876543210;
        ///   // Do implicit cast between Int64 and DsLong
        ///   DsLong dsl = l;
        ///
        ///   Console.WriteLine(dsl.ToString());
        /// </code>
        /// </summary>
        /// <param name="g">System.Int64 to be cast</param>
        /// <returns>A casted DirectShowLib.DsLong</returns>
        public static implicit operator DsLong(long l)
        {
            return new DsLong(l);
        }

        /// <summary>
        /// Get the System.Int64 equivalent to this DirectShowLib.DsLong instance.
        /// </summary>
        /// <returns>A System.Int64</returns>
        public long ToInt64()
        {
            return this.Value;
        }

        /// <summary>
        /// Get a new DirectShowLib.DsLong instance for a given System.Int64
        /// </summary>
        /// <param name="g">The System.Int64 to wrap into a DirectShowLib.DsLong</param>
        /// <returns>A new instance of DirectShowLib.DsLong</returns>
        public static DsLong FromInt64(long l)
        {
            return new DsLong(l);
        }
    }

    public enum AM_SEEKING_SeekingFlags
    {
        AM_SEEKING_NoPositioning = 0,
        AM_SEEKING_AbsolutePositioning = 0x1,
        AM_SEEKING_RelativePositioning = 0x2,
        AM_SEEKING_IncrementalPositioning = 0x3,
        AM_SEEKING_PositioningBitsMask = 0x3,
        AM_SEEKING_SeekToKeyFrame = 0x4,
        AM_SEEKING_ReturnTime = 0x8,
        AM_SEEKING_Segment = 0x10,
        AM_SEEKING_NoFlush = 0x20
    }

    public enum AM_SEEKING_SeekingCapabilities
    {
        AM_SEEKING_CanSeekAbsolute = 0x1,
        AM_SEEKING_CanSeekForwards = 0x2,
        AM_SEEKING_CanSeekBackwards = 0x4,
        AM_SEEKING_CanGetCurrentPos = 0x8,
        AM_SEEKING_CanGetStopPos = 0x10,
        AM_SEEKING_CanGetDuration = 0x20,
        AM_SEEKING_CanPlayBackwards = 0x40,
        AM_SEEKING_CanDoSegments = 0x80,
        AM_SEEKING_Source = 0x100
    }

    [ComImport()]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("93E5A4E0-2D50-11d2-ABFA-00A0C9C6E38D")]
    public interface ICaptureGraphBuilder2
    {
        HRESULTMF SetFiltergraph(IGraphBuilder pfg);
        HRESULTMF GetFiltergraph(out IGraphBuilder ppfg);
        HRESULTMF SetOutputFileName(ref Guid pType, string lpstrFile, out IBaseFilter ppf, out IFileSinkFilter ppSink);
        HRESULTMF FindInterface(ref Guid pCategory, ref Guid pType, IBaseFilter pf, ref Guid riid, out IntPtr ppint);
        //HRESULT RenderStream(ref Guid pCategory, ref Guid pType, IUnknown pSource,IBaseFilter pfCompressor,IBaseFilter pfRenderer);
        [PreserveSig]
        HRESULTMF RenderStream(ref Guid pCategory, ref Guid pType, IntPtr pSource, IBaseFilter pfCompressor, IBaseFilter pfRenderer);
        HRESULTMF ControlStream(ref Guid pCategory, ref Guid pType, IBaseFilter pFilter, Int64 pstart, Int64 pstop, ushort wStartCookie, ushort wStopCookie);
        HRESULTMF AllocCapFile(string lpstr, UInt64 dwlSize);
        HRESULTMF CopyCaptureFile(string lpwstrOld, string lpwstrNew, int fAllowEscAbort, IAMCopyCaptureFileProgress pCallback);
        //HRESULT FindPin(IUnknown pSource, PIN_DIRECTION pindir, ref Guid pCategory, ref Guid pType, bool fUnconnected, int num, out IPin ppPin);
        HRESULTMF FindPin(IntPtr pSource, PIN_DIRECTION pindir, ref Guid pCategory, ref Guid pType, bool fUnconnected, int num, out IPin ppPin);
    }

    [ComImport()]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("a2104830-7c70-11cf-8bce-00aa00a3f1a6")]
    public interface IFileSinkFilter
    {
        HRESULTMF SetFileName(string pszFileName, AM_MEDIA_TYPE pmt);
        HRESULTMF GetCurFile(out string ppszFileName, out AM_MEDIA_TYPE pmt);
    }

    [ComImport()]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("670d1d20-a068-11d0-b3f0-00aa003761c5")]
    public interface IAMCopyCaptureFileProgress
    {
        HRESULTMF Progress(int iProgress);
    }

    [ComImport()]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("29840822-5B84-11D0-BD3B-00A0C911CE86")]
    public interface ICreateDevEnum
    {
        HRESULTMF CreateClassEnumerator(ref Guid clsidDeviceClass, out IEnumMoniker ppEnumMoniker, int dwFlags);
    }

    [ComImport]
    [Guid("00000102-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumMoniker
    {
        HRESULTMF Next(uint celt, out IMoniker rgelt, out uint pceltFetched);
        HRESULTMF Skip(uint celt);
        HRESULTMF Reset();
        HRESULTMF Clone(out IEnumMoniker ppenum);
    }

    [ComImport]
    [Guid("0000000f-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMoniker : IPersistStream
    {
        #region IPersist
        new HRESULTMF GetClassID([Out] out Guid pClassID);
        #endregion

        #region IPersistStream
        new HRESULTMF IsDirty();
        new HRESULTMF Load(System.Runtime.InteropServices.ComTypes.IStream pStm);
        new HRESULTMF Save(System.Runtime.InteropServices.ComTypes.IStream pStm, bool fClearDirty);
        new HRESULTMF GetSizeMax(out ULARGE_INTEGER pcbSize);
        #endregion

        //HRESULT BindToObject(System.Runtime.InteropServices.ComTypes.IBindCtx pbc, IMoniker pmkToLeft, ref Guid riidResult, ref IntPtr ppvResult);
        HRESULTMF BindToObject(IntPtr pbc, IMoniker pmkToLeft, ref Guid riidResult, ref IntPtr ppvResult);

        //HRESULT BindToStorage(IBindCtx pbc, IMoniker pmkToLeft, ref Guid riid, out IntPtr ppvObj);
        HRESULTMF BindToStorage(IntPtr pbc, IMoniker pmkToLeft, ref Guid riid, out IntPtr ppvObj);

        //HRESULT Reduce(IBindCtx pbc, int dwReduceHowFar, ref IMoniker ppmkToLeft, out IMoniker ppmkReduced);
        HRESULTMF Reduce(IntPtr pbc, int dwReduceHowFar, ref IMoniker ppmkToLeft, out IMoniker ppmkReduced);

        HRESULTMF ComposeWith(IMoniker pmkRight, bool fOnlyIfNotGeneric, out IMoniker ppmkComposite);

        HRESULTMF Enum(bool fForward, out IEnumMoniker ppenumMoniker);

        HRESULTMF IsEqual(IMoniker pmkOtherMoniker);

        HRESULTMF Hash(out int pdwHash);

        //HRESULT IsRunning(IBindCtx pbc, IMoniker pmkToLeft, IMoniker pmkNewlyRunning);
        HRESULTMF IsRunning(IntPtr pbc, IMoniker pmkToLeft, IMoniker pmkNewlyRunning);

        //HRESULT GetTimeOfLastChange(IBindCtx pbc, IMoniker pmkToLeft, out FILETIME pFileTime);
        HRESULTMF GetTimeOfLastChange(IntPtr pbc, IMoniker pmkToLeft, out System.Runtime.InteropServices.ComTypes.FILETIME pFileTime);

        HRESULTMF Inverse(out IMoniker ppmk);

        HRESULTMF CommonPrefixWith(IMoniker pmkOther, out IMoniker ppmkPrefix);

        HRESULTMF RelativePathTo(IMoniker pmkOther, out IMoniker ppmkRelPath);

        //HRESULT GetDisplayName(IBindCtx pbc, IMoniker pmkToLeft, out LPOLESTR ppszDisplayName);
        HRESULTMF GetDisplayName(IntPtr pbc, IMoniker pmkToLeft, out string ppszDisplayName);

        //HRESULT ParseDisplayName(IBindCtx pbc, IMoniker pmkToLeft, LPOLESTR pszDisplayName, out uint pchEaten, out IMoniker ppmkOut);
        HRESULTMF ParseDisplayName(IntPtr pbc, IMoniker pmkToLeft, string pszDisplayName, out uint pchEaten, out IMoniker ppmkOut);

        HRESULTMF IsSystemMoniker(out int pdwMksys);
    }

    [ComImport]
    [Guid("00000109-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPersistStream : IPersist
    {
        #region IPersist
        new HRESULTMF GetClassID([Out] out Guid pClassID);
        #endregion
        HRESULTMF IsDirty();
        HRESULTMF Load(System.Runtime.InteropServices.ComTypes.IStream pStm);
        HRESULTMF Save(System.Runtime.InteropServices.ComTypes.IStream pStm, bool fClearDirty);
        HRESULTMF GetSizeMax(out ULARGE_INTEGER pcbSize);
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct ULARGE_INTEGER
    {
        [FieldOffset(0)]
        public int LowPart;
        [FieldOffset(4)]
        public int HighPart;
        [FieldOffset(0)]
        public long QuadPart;
    }

    [ComImport]
    [Guid("55272A00-42CB-11CE-8135-00AA004BB851")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPropertyBag
    {
        //HRESULT Read(string pszPropName, ref VARIANT* pVar, IErrorLog pErrorLog);
        //HRESULT Read(string pszPropName, ref IntPtr pVar, IErrorLog pErrorLog);
        HRESULTMF Read(string pszPropName, out PROPVARIANT pVar, IErrorLog pErrorLog);

        //HRESULT Write(string pszPropName, VARIANT* pVar);
        HRESULTMF Write(string pszPropName, IntPtr pVar);
    }

    [ComImport]
    [Guid("3127CA40-446E-11CE-8135-00AA004BB851")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IErrorLog
    {
        //HRESULT AddError(LPCOLESTR pszPropName, System.Runtime.InteropServices.ComTypes.EXCEPINFO pExcepInfo);
        HRESULTMF AddError([In, MarshalAs(UnmanagedType.LPWStr)] string pszPropName, System.Runtime.InteropServices.ComTypes.EXCEPINFO pExcepInfo);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PROPARRAY
    {
        public UInt32 cElems;
        public IntPtr pElems;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct PROPVARIANT
    {
        [FieldOffset(0)]
        public ushort varType;
        [FieldOffset(2)]
        public ushort wReserved1;
        [FieldOffset(4)]
        public ushort wReserved2;
        [FieldOffset(6)]
        public ushort wReserved3;

        [FieldOffset(8)]
        public byte bVal;
        [FieldOffset(8)]
        public sbyte cVal;
        [FieldOffset(8)]
        public ushort uiVal;
        [FieldOffset(8)]
        public short iVal;
        [FieldOffset(8)]
        public UInt32 uintVal;
        [FieldOffset(8)]
        public Int32 intVal;
        [FieldOffset(8)]
        public UInt64 ulVal;
        [FieldOffset(8)]
        public Int64 lVal;
        [FieldOffset(8)]
        public float fltVal;
        [FieldOffset(8)]
        public double dblVal;
        [FieldOffset(8)]
        public short boolVal;
        [FieldOffset(8)]
        public IntPtr pclsidVal; // GUID ID pointer
        [FieldOffset(8)]
        public IntPtr pszVal; // Ansi string pointer
        [FieldOffset(8)]
        public IntPtr pwszVal; // Unicode string pointer
        [FieldOffset(8)]
        public IntPtr punkVal; // punkVal (interface pointer)
        [FieldOffset(8)]
        public PROPARRAY ca;
        [FieldOffset(8)]
        public System.Runtime.InteropServices.ComTypes.FILETIME filetime;
    }

    public enum VARENUM
    {
        VT_EMPTY = 0,
        VT_NULL = 1,
        VT_I2 = 2,
        VT_I4 = 3,
        VT_R4 = 4,
        VT_R8 = 5,
        VT_CY = 6,
        VT_DATE = 7,
        VT_BSTR = 8,
        VT_DISPATCH = 9,
        VT_ERROR = 10,
        VT_BOOL = 11,
        VT_VARIANT = 12,
        VT_UNKNOWN = 13,
        VT_DECIMAL = 14,
        VT_I1 = 16,
        VT_UI1 = 17,
        VT_UI2 = 18,
        VT_UI4 = 19,
        VT_I8 = 20,
        VT_UI8 = 21,
        VT_INT = 22,
        VT_UINT = 23,
        VT_VOID = 24,
        VT_HRESULT = 25,
        VT_PTR = 26,
        VT_SAFEARRAY = 27,
        VT_CARRAY = 28,
        VT_USERDEFINED = 29,
        VT_LPSTR = 30,
        VT_LPWSTR = 31,
        VT_RECORD = 36,
        VT_INT_PTR = 37,
        VT_UINT_PTR = 38,
        VT_FILETIME = 64,
        VT_BLOB = 65,
        VT_STREAM = 66,
        VT_STORAGE = 67,
        VT_STREAMED_OBJECT = 68,
        VT_STORED_OBJECT = 69,
        VT_BLOB_OBJECT = 70,
        VT_CF = 71,
        VT_CLSID = 72,
        VT_VERSIONED_STREAM = 73,
        VT_BSTR_BLOB = 0xfff,
        VT_VECTOR = 0x1000,
        VT_ARRAY = 0x2000,
        VT_BYREF = 0x4000,
        VT_RESERVED = 0x8000,
        VT_ILLEGAL = 0xffff,
        VT_ILLEGALMASKED = 0xfff,
        VT_TYPEMASK = 0xfff
    };


    [ComImport]
    [Guid("5a804648-4f66-4867-9c43-4f5c822cf1b8")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRFilterConfig9
    {
        HRESULTMF SetImageCompositor(IVMRImageCompositor9 lpVMRImgCompositor);
        HRESULTMF SetNumberOfStreams(uint dwMaxStreams);
        HRESULTMF GetNumberOfStreams(out uint pdwMaxStreams);
        HRESULTMF SetRenderingPrefs(uint dwRenderFlags);
        HRESULTMF GetRenderingPrefs(out uint pdwRenderFlags);
        HRESULTMF SetRenderingMode(uint Mode);
        HRESULTMF GetRenderingMode(out uint pMode);
    }

    [ComImport]
    [Guid("4a5c89eb-df51-4654-ac2a-e48e02bbabf6")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRImageCompositor9
    {
        HRESULTMF InitCompositionDevice(IntPtr pD3DDevice);
        HRESULTMF TermCompositionDevice(IntPtr pD3DDevice);
        HRESULTMF SetStreamMediaType(uint dwStrmID, AM_MEDIA_TYPE pmt, bool fTexture);
        //HRESULT CompositeImage(IntPtr pD3DDevice, IDirect3DSurface9* pddsRenderTarget, AM_MEDIA_TYPE pmtRenderTarget, long rtStart,
        //     long rtEnd, uint dwClrBkGnd, VMR9VideoStreamInfo pVideoStreamInfo, uint cStreams);
        HRESULTMF CompositeImage(IntPtr pD3DDevice, IntPtr pddsRenderTarget, AM_MEDIA_TYPE pmtRenderTarget, long rtStart,
            long rtEnd, uint dwClrBkGnd, ref VMR9VideoStreamInfo pVideoStreamInfo, uint cStreams);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VMR9VideoStreamInfo
    {
        //public IDirect3DSurface9* pddsVideoSurface;
        public IntPtr pddsVideoSurface;
        public uint dwWidth;
        public uint dwHeight;
        public uint dwStrmID;
        public float fAlpha;
        VMR9NormalizedRect rNormal;
        long rtStart;
        long rtEnd;
        VMR9_SampleFormat SampleFormat;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VMR9NormalizedRect
    {
        public float left;
        public float top;
        public float right;
        public float bottom;
    }

    public enum VMR9_SampleFormat
    {
        VMR9_SampleReserved = 1,
        VMR9_SampleProgressiveFrame = 2,
        VMR9_SampleFieldInterleavedEvenFirst = 3,
        VMR9_SampleFieldInterleavedOddFirst = 4,
        VMR9_SampleFieldSingleEven = 5,
        VMR9_SampleFieldSingleOdd = 6
    }

    public enum VMR9Mode
    {
        VMR9Mode_Windowed = 0x1,
        VMR9Mode_Windowless = 0x2,
        VMR9Mode_Renderless = 0x4,
        VMR9Mode_Mask = 0x7
    }

    [ComImport]
    [Guid("8f537d09-f85e-4414-b23b-502e54c79927")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRWindowlessControl9
    {
        HRESULTMF GetNativeVideoSize(out int lpWidth, out int lpHeight, out int lpARWidth, out int lpARHeight);
        HRESULTMF GetMinIdealVideoSize(out int lpWidth, out int lpHeight);
        HRESULTMF GetMaxIdealVideoSize(out int lpWidth, out int lpHeight);
        //HRESULT SetVideoPosition(ref RECT lpSRCRect, ref RECT lpDSTRect);
        HRESULTMF SetVideoPosition(IntPtr lpSRCRect, ref RECT lpDSTRect);
        //HRESULT SetVideoPosition(IntPtr lpSRCRect, IntPtr lpDSTRect);
        HRESULTMF GetVideoPosition(out RECT lpSRCRect, out RECT lpDSTRect);
        HRESULTMF GetAspectRatioMode(out uint lpAspectRatioMode);
        HRESULTMF SetAspectRatioMode(uint AspectRatioMode);
        HRESULTMF SetVideoClippingWindow(IntPtr hwnd);
        HRESULTMF RepaintVideo(IntPtr hwnd, IntPtr hdc);
        HRESULTMF DisplayModeChanged();
        //HRESULT GetCurrentImage(out BYTE** lpDib);
        HRESULTMF GetCurrentImage(out IntPtr lpDib);
        HRESULTMF SetBorderColor(uint Clr);
        HRESULTMF GetBorderColor(out uint lpClr);
    }

    //[ComImport]
    //[Guid("ced175e5-1935-4820-81bd-ff6ad00c9108")]
    //[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    //public interface IVMRMixerBitmap9
    //{
    //    HRESULT SetAlphaBitmap(ref VMR9AlphaBitmap pBmpParms);
    //    HRESULT UpdateAlphaBitmapParameters(ref VMR9AlphaBitmap pBmpParms);
    //    HRESULT GetAlphaBitmapParameters(out VMR9AlphaBitmap pBmpParms);           
    //}

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
        Guid("ced175e5-1935-4820-81bd-ff6ad00c9108"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRMixerBitmap9
    {
        [PreserveSig]
        HRESULTMF SetAlphaBitmap([In] ref VMR9AlphaBitmap pBmpParms);

        [PreserveSig]
        HRESULTMF UpdateAlphaBitmapParameters([In] ref VMR9AlphaBitmap pBmpParms);

        [PreserveSig]
        HRESULTMF GetAlphaBitmapParameters([Out] out VMR9AlphaBitmap pBmpParms);
    }

    /// <summary> 
    /// From VMR9AlphaBitmap 
    /// </summary> 
    [StructLayout(LayoutKind.Sequential)]
    public struct VMR9AlphaBitmap
    {
        public uint dwFlags;
        public IntPtr hdc; // HDC 
        public IntPtr pDDS; // IDirect3DSurface9 
        public RECT rSrc;
        public VMR9NormalizedRect rDest;
        public float fAlpha;
        public int clrSrcKey;
        public uint dwFilterMode;
    }

    public enum VMRBITMAP
    {
        VMRBITMAP_DISABLE = 0x00000001,
        VMRBITMAP_HDC = 0x00000002,
        VMRBITMAP_ENTIREDDS = 0x00000004,
        VMRBITMAP_SRCCOLORKEY = 0x00000008,
        VMRBITMAP_SRCRECT = 0x00000010
    }
}
