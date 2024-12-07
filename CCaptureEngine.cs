using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using GlobalStructures;
using static GlobalStructures.GlobalTools;
using DXGI;
using static DXGI.DXGITools;
using MFCaptureEngine;
using static MFCaptureEngine.MFCaptureEngineTools;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml.Controls;
using Global;
using static Global.MFError;
using System.Diagnostics;
using System.Reflection.Metadata;
using Microsoft.UI.Xaml.Media;
using Windows.Devices.Sms;

// Reference : https://github.com/microsoft/Windows-classic-samples/tree/main/Samples/CaptureEngineVideoCapture

namespace WinUI3_MFCaptureEngine
{
    internal class CCaptureEngine : IMFCaptureEngineOnEventCallback, IMFCaptureEngineOnSampleCallback, IDisposable
    {
        [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        public const int WM_APP = 0x8000;
        public const int WM_APP_CAPTURE_EVENT = WM_APP + 1;

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr CreateEvent(IntPtr lpEventAttributes, bool bManualReset, bool bInitialState, string lpName);

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool SetEvent(IntPtr hEvent);

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool ResetEvent(IntPtr hEvent);

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool CloseHandle(IntPtr hObject);

        public const uint INFINITE = 0xFFFFFFFF;

        public uint m_nWidth = 0;
        public uint m_nHeight = 0;
        public string m_sFriendlyName = null;
        Microsoft.UI.Xaml.Media.Imaging.WriteableBitmap m_WriteableBitmapCapture = null;

        private IMFCaptureEngine m_pCaptureEngine = null;
        private IMFCaptureEngineClassFactory m_pCaptureEngineClassFactory = null;
        private IMFAttributes m_pAttributes = null;
        private IntPtr m_pD3D11DevicePtr = IntPtr.Zero;
        private IntPtr m_pD3D11DeviceContextPtr = IntPtr.Zero;
        private IMFDXGIDeviceManager m_pDXGIDeviceManager = null;

        private IMFCapturePreviewSink m_pCapturePreviewSink = null;
        private IMFCaptureRecordSink m_pCaptureRecordSink = null;
        private IMFCaptureSource m_pCaptureSource = null;
        private IMFTransform m_pTransform = null;
        //private IPropertyStore m_pPropertyStore = null;

        private MainWindow _mw;
        private IntPtr m_hWnd;
        private IntPtr m_hEvent = IntPtr.Zero;

        public bool m_bPreview = false;
        public bool m_bRecording = false;
        public bool m_bTakingPhoto = false;
        //private string m_sImageFileName = null;

        public CCaptureEngine(MainWindow mw)
        {
            _mw = mw;
            m_hWnd = WinRT.Interop.WindowNative.GetWindowHandle(_mw);           
        }

        public void WaitForResult()
        {
            WaitForSingleObject(m_hEvent, INFINITE);
        }

        public void ResetEvent()
        {
            ResetEvent(m_hEvent);
        }

        // Random crash  :
        // Unhandled exception at 0x77CD8AA0 (ntdll.dll) in WinUI3_MFCaptureEngine.exe: Indirect call guard check detected invalid control transfer.
        // The Common Language Runtime cannot stop at this exception.Common causes include: incorrect COM interop marshalling and memory corruption.To investigate further use native-only debugging.
        // Exception thrown at 0x77CD896B (ntdll.dll) in WinUI3_MFCaptureEngine.exe: 0xC0000005: Access violation reading location 0x00D30000.

        public GlobalStructures.HRESULTMF Initialize(IntPtr hWnd, IntPtr pAudioSource, IntPtr pVideoSource)
        {
            //HRESULT hr = MFStartup(MF_VERSION);
            GlobalStructures.HRESULTMF hr = GlobalStructures.HRESULTMF.S_OK;
            if (hr == GlobalStructures.HRESULTMF.S_OK)
            {
                m_hEvent = CreateEvent(nint.Zero, false, false, null);
                m_pCaptureEngineClassFactory = (IMFCaptureEngineClassFactory)Activator.CreateInstance(Type.GetTypeFromCLSID(CLSID_MFCaptureEngineClassFactory));
                if (m_pCaptureEngineClassFactory != null)
                {
                    hr = MFCreateAttributes(out m_pAttributes, 1);
                    if (hr == GlobalStructures.HRESULTMF.S_OK)
                    {
                        hr = CreateDeviceContext();
                        if (hr == GlobalStructures.HRESULTMF.S_OK)
                        {
                            uint nResetToken;
                            hr = MFCreateDXGIDeviceManager(out nResetToken, out m_pDXGIDeviceManager);
                            if (hr == GlobalStructures.HRESULTMF.S_OK)
                            {
                                hr = m_pDXGIDeviceManager.ResetDevice(m_pD3D11DevicePtr, nResetToken);
                                IntPtr pUnknownDeviceManager = Marshal.GetComInterfaceForObject(m_pDXGIDeviceManager, typeof(IMFDXGIDeviceManager));
                                hr = m_pAttributes.SetUnknown(MF_CAPTURE_ENGINE_D3D_MANAGER, pUnknownDeviceManager);
                                Marshal.Release(pUnknownDeviceManager);

                                hr = m_pCaptureEngineClassFactory.CreateInstance(CLSID_MFCaptureEngine, typeof(IMFCaptureEngine).GUID, out m_pCaptureEngine);
                                if (hr == GlobalStructures.HRESULTMF.S_OK)
                                {
                                    hr = m_pCaptureEngine.Initialize(this, m_pAttributes, pAudioSource, pVideoSource);
                                    if (hr == GlobalStructures.HRESULTMF.S_OK)
                                    {
                                        //var CLSID_CColorControlDmo = new Guid("798059f0-89ca-4160-b325-aeb48efe4f9a");
                                        //m_pTransform = (IMFTransform)Activator.CreateInstance(Type.GetTypeFromCLSID(CLSID_CColorControlDmo));
                                        //m_pPropertyStore = (IPropertyStore)m_pTransform;                                        
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return hr;
        }

        public GlobalStructures.HRESULTMF GetDeviceData()
        {
            GlobalStructures.HRESULTMF hr = GlobalStructures.HRESULTMF.S_OK;
            IMFAttributes pAttributes = null;
            hr = MFCreateAttributes(out pAttributes, 1);
            IMFActivate[] pActivateArray = null;
            pAttributes.SetGUID(MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE, MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_GUID);
            uint nElements = 0;
            hr = MFEnumDeviceSources(pAttributes, out pActivateArray, out nElements);
            if (hr == GlobalStructures.HRESULTMF.S_OK)
            {
                if (nElements > 0)
                {
                    StringBuilder sbValue = new StringBuilder(260); ;
                    hr = pActivateArray[0].GetAllocatedString(MF_DEVSOURCE_ATTRIBUTE_FRIENDLY_NAME, out sbValue, out uint nLenght);
                    m_sFriendlyName = sbValue.ToString();
                    
                    hr = m_pCaptureEngine.GetSource(out m_pCaptureSource);
                    if (hr == GlobalStructures.HRESULTMF.S_OK)
                    {
                        IMFMediaType pMediaType = null;
                        hr = m_pCaptureSource.GetCurrentDeviceMediaType((uint)MF_CAPTURE_ENGINE_MEDIA_TYPE.MF_CAPTURE_ENGINE_PREFERRED_SOURCE_STREAM_FOR_VIDEO_PREVIEW, out pMediaType);
                        if (hr == GlobalStructures.HRESULTMF.S_OK)
                        {
                            hr = MFGetAttributeSize(pMediaType, MF_MT_FRAME_SIZE, out m_nWidth, out m_nHeight);
                            if (hr == GlobalStructures.HRESULTMF.S_OK)
                            {
                                PostMessage(m_hWnd, WM_APP_CAPTURE_EVENT, nint.Zero, (IntPtr)1);
                                int nSize = (int)(m_nWidth * 4 * m_nHeight);
                                m_pBytesArray = new byte[nSize];
                            }
                            SafeRelease(ref pMediaType);
                        }
                    }
                    _mw.Title = "Device : " + m_sFriendlyName + " (" + m_nWidth.ToString() + " * " + m_nHeight.ToString() + ")";                   
                }
                for (int n = 0; n < nElements; n++)
                {
                    SafeRelease(ref pActivateArray[n]);
                }            
            }
            SafeRelease(ref pAttributes);
            return hr;
        }
        
        public GlobalStructures.HRESULTMF CreateDeviceContext()
        {
            GlobalStructures.HRESULTMF hr = GlobalStructures.HRESULTMF.S_OK;
            uint creationFlags = (uint)D3D11_CREATE_DEVICE_FLAG.D3D11_CREATE_DEVICE_BGRA_SUPPORT;

            // Needs "Enable native code Debugging"
#if DEBUG
            creationFlags |= (uint)D3D11_CREATE_DEVICE_FLAG.D3D11_CREATE_DEVICE_DEBUG;
#endif

            int[] aD3D_FEATURE_LEVEL = new int[] { (int)D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_11_1, (int)D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_11_0,
                (int)D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_10_1, (int)D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_10_0, (int)D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_9_3,
                (int)D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_9_2, (int)D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_9_1};

            D3D_FEATURE_LEVEL featureLevel;
            hr = D3D11CreateDevice(null,    // specify null to use the default adapter
                D3D_DRIVER_TYPE.D3D_DRIVER_TYPE_HARDWARE,
                IntPtr.Zero,
                creationFlags,      // optionally set debug and Direct2D compatibility flags
                aD3D_FEATURE_LEVEL, // list of feature levels this app can support
                                    //(uint)Marshal.SizeOf(aD3D_FEATURE_LEVEL),   // number of possible feature levels
                (uint)aD3D_FEATURE_LEVEL.Length, // number of possible feature levels
                D3D11_SDK_VERSION,
                out m_pD3D11DevicePtr,    // returns the Direct3D device created
                out featureLevel,         // returns feature level of device created            
                out m_pD3D11DeviceContextPtr // returns the device immediate context
            );
            if (hr == GlobalStructures.HRESULTMF.S_OK)
            {
                ID3D10Multithread pMultithread = Marshal.GetObjectForIUnknown(m_pD3D11DevicePtr) as ID3D10Multithread;
                bool bRet = pMultithread.SetMultithreadProtected(true);
                SafeRelease(ref pMultithread);
            }
            return hr;
        }   

        public GlobalStructures.HRESULTMF StartPreview()
        {
            GlobalStructures.HRESULTMF hr = GlobalStructures.HRESULTMF.S_OK;
            if (m_pCaptureEngine != null)
            {
                if (m_pCapturePreviewSink == null)
                {
                    IMFCaptureSink pCaptureSink = null;
                    hr = m_pCaptureEngine.GetSink(MF_CAPTURE_ENGINE_SINK_TYPE.MF_CAPTURE_ENGINE_SINK_TYPE_PREVIEW, out pCaptureSink);
                    if (hr == GlobalStructures.HRESULTMF.S_OK)
                    {
                        m_pCapturePreviewSink = (IMFCapturePreviewSink)pCaptureSink;
                        if (hr == GlobalStructures.HRESULTMF.S_OK)
                        {
                            if (m_pCaptureSource == null)
                                hr = m_pCaptureEngine.GetSource(out m_pCaptureSource);
                            if (hr == GlobalStructures.HRESULTMF.S_OK)
                            {
                                IMFMediaType pMediaType = null;
                                hr = m_pCaptureSource.GetCurrentDeviceMediaType((uint)MF_CAPTURE_ENGINE_MEDIA_TYPE.MF_CAPTURE_ENGINE_PREFERRED_SOURCE_STREAM_FOR_VIDEO_PREVIEW, out pMediaType);
                                if (hr == GlobalStructures.HRESULTMF.S_OK)
                                {
                                    IMFMediaType pMediaType2 = null;
                                    hr = CloneVideoMediaType(pMediaType, MFVideoFormat_RGB32, out pMediaType2);
                                    if (hr == GlobalStructures.HRESULTMF.S_OK)
                                    {
                                        hr = pMediaType2.SetUINT32(MF_MT_ALL_SAMPLES_INDEPENDENT, 1);
                                        if (hr == GlobalStructures.HRESULTMF.S_OK)
                                        {
                                            hr = m_pCapturePreviewSink.AddStream((uint)MF_CAPTURE_ENGINE_MEDIA_TYPE.MF_CAPTURE_ENGINE_PREFERRED_SOURCE_STREAM_FOR_VIDEO_PREVIEW, pMediaType2, null, out uint nSinkStreamIndex);
                                            if (hr == GlobalStructures.HRESULTMF.S_OK)
                                            {
                                                hr = m_pCapturePreviewSink.SetSampleCallback(nSinkStreamIndex, this);
                                                //hr = m_pCapturePreviewSink.SetRenderHandle(m_hWnd);                                               
                                            }
                                        }
                                        SafeRelease(ref pMediaType2);
                                    }
                                    SafeRelease(ref pMediaType);
                                }
                                //SafeRelease(ref pCaptureSource);
                            }
                            //SafeRelease(ref pCaptureSink);
                        }
                    }
                }
                if (hr == GlobalStructures.HRESULTMF.S_OK)
                {
                    if (!m_bPreview)
                    {
                        try
                        {
                            ResetEvent();
                            hr = m_pCaptureEngine.StartPreview();
                            WaitForResult();
                        }
                        catch (NotImplementedException niex)
                        {

                        }
                        catch (InvalidOperationException ioex)
                        {

                        }                                             
                        catch (Exception ex)
                        {

                        }                                               
                    }
                }
            }
            return hr;
        }

        public GlobalStructures.HRESULTMF StopPreview()
        {
            GlobalStructures.HRESULTMF hr = GlobalStructures.HRESULTMF.S_OK;
            if (m_pCaptureEngine != null)
            {
                if (m_bPreview)
                {
                    try
                    {
                        ResetEvent();
                        hr = m_pCaptureEngine.StopPreview();
                        WaitForResult();
                    }
                    catch (NotImplementedException niex)
                    {

                    }
                    catch (InvalidOperationException e)
                    {

                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return hr;
        }

        public GlobalStructures.HRESULTMF StartRecord(string sFileName, Guid guidVideoEncoding, Guid guidAudioEncoding)
        {
            GlobalStructures.HRESULTMF hr = GlobalStructures.HRESULTMF.S_OK;
            if (m_pCaptureEngine != null && !m_bRecording)
            {
                if (m_pCaptureRecordSink == null)
                {
                    IMFCaptureSink pCaptureSink = null;
                    hr = m_pCaptureEngine.GetSink(MF_CAPTURE_ENGINE_SINK_TYPE.MF_CAPTURE_ENGINE_SINK_TYPE_RECORD, out pCaptureSink);
                    m_pCaptureRecordSink = (IMFCaptureRecordSink)pCaptureSink;
                }
                if (hr == GlobalStructures.HRESULTMF.S_OK)
                {                   
                    if (m_pCaptureSource == null)
                        hr = m_pCaptureEngine.GetSource(out m_pCaptureSource);
                    if (hr == GlobalStructures.HRESULTMF.S_OK)
                    {
                        hr = m_pCaptureRecordSink.RemoveAllStreams();
                        if (hr == GlobalStructures.HRESULTMF.S_OK)
                        {
                            hr = m_pCaptureRecordSink.SetOutputFileName(sFileName);
                            if (hr == GlobalStructures.HRESULTMF.S_OK)
                            {
                                if (guidVideoEncoding != Guid.Empty)
                                {
                                    uint dwSinkStreamIndex = 0; ;
                                    hr = ConfigureVideoEncoding(m_pCaptureSource, m_pCaptureRecordSink, guidVideoEncoding, out dwSinkStreamIndex);
                                    if (hr == GlobalStructures.HRESULTMF.S_OK)
                                    {
                                        if (guidAudioEncoding != Guid.Empty)
                                        {
                                            hr = ConfigureAudioEncoding(m_pCaptureSource, m_pCaptureRecordSink, guidAudioEncoding);
                                            if (hr == GlobalStructures.HRESULTMF.S_OK)
                                            {
                                                //hr = m_pCaptureRecordSink.SetSampleCallback(dwSinkStreamIndex, this);

                                                if (0 == 1)
                                                {
                                                    object pServiceUnknown = null;
                                                    hr = m_pCaptureSource.GetService(Guid.Empty, typeof(IMFSourceReader).GUID, out pServiceUnknown);
                                                    IMFSourceReader pSourceReader = (IMFSourceReader)pServiceUnknown;
                                                    SafeRelease(ref pSourceReader);

                                                    IntPtr pPtr = nint.Zero;
                                                    //IMFSimpleAudioVolume pVol = null;
                                                    Guid guid = typeof(IMFSimpleAudioVolume).GUID;
                                                    //Guid guid = typeof(IMFAudioStreamVolume).GUID;

                                                    IntPtr pUnknown = Marshal.GetComInterfaceForObject(m_pCaptureSource, typeof(IMFCaptureSource));
                                                    Guid g = MR_CAPTURE_POLICY_VOLUME_SERVICE;
                                                    hr = MFGetService(pUnknown, ref g, ref guid, out pPtr);
                                                    Marshal.Release(pUnknown);

                                                    // : 'L’objet ne prend pas en charge le service spécifié. (0xC00D36BA)'
                                                    //hr = pSourceReader.GetServiceForStream((uint)MF_SOURCE_READER.MF_SOURCE_READER_FIRST_AUDIO_STREAM, MR_CAPTURE_POLICY_VOLUME_SERVICE, ref guid, out pPtr);

                                                    //IMFGetService pService = null;
                                                    //pService = (IMFGetService)pSourceReader;

                                                    //IMFStreamSink p = (IMFStreamSink)pCaptureRecordSink;
                                                    //IMFMediaSink p = (IMFMediaSink)pCaptureRecordSink;
                                                    //IntPtr pServicePtr = IntPtr.Zero;
                                                    //hr = pCaptureRecordSink.GetService(dwSinkStreamIndex, Guid.Empty, typeof(IMFSinkWriter).GUID, out pServicePtr);
                                                    ////IMFGetService pService = null;
                                                    //pService = (IMFGetService)pCaptureSource;
                                                }
                                                hr = m_pCaptureEngine.StartRecord();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //SafeRelease(ref pCaptureSink);
                } 
            }
            return hr;
        }

        public GlobalStructures.HRESULTMF StopRecord()
        {
            GlobalStructures.HRESULTMF hr = GlobalStructures.HRESULTMF.S_OK;
            if (m_pCaptureEngine != null)
            {
                hr = m_pCaptureEngine.StopRecord(true, false);
            }
            return hr;
        }

        GlobalStructures.HRESULTMF ConfigureVideoEncoding(IMFCaptureSource pSource, IMFCaptureRecordSink pRecord, Guid guidEncodingType, out uint dwSinkStreamIndex)
        {
            IMFMediaType pMediaType = null;
            IMFMediaType pMediaType2 = null;
            Guid guidSubType = Guid.Empty;
            dwSinkStreamIndex = 0;

            // Configure the video format for the recording sink.
            GlobalStructures.HRESULTMF hr = pSource.GetCurrentDeviceMediaType((uint)MF_CAPTURE_ENGINE_MEDIA_TYPE.MF_CAPTURE_ENGINE_PREFERRED_SOURCE_STREAM_FOR_VIDEO_RECORD, out pMediaType);
            if (hr == GlobalStructures.HRESULTMF.S_OK)
            {
                hr = CloneVideoMediaType(pMediaType, guidEncodingType, out pMediaType2);
                if (hr == GlobalStructures.HRESULTMF.S_OK)
                {
                    hr = pMediaType.GetGUID(MF_MT_SUBTYPE, out guidSubType);
                    if (hr == GlobalStructures.HRESULTMF.S_OK)
                    {
                        if (guidSubType == MFVideoFormat_H264_ES || guidSubType == MFVideoFormat_H264)
                        {
                            //When the webcam supports H264_ES or H264, we just bypass the stream. The output from Capture engine shall be the same as the native type supported by the webcam
                            hr = pMediaType2.SetGUID(MF_MT_SUBTYPE, MFVideoFormat_H264);
                        }
                        else
                        {
                            uint uiEncodingBitrate;
                            hr = GetEncodingBitrate(pMediaType2, out uiEncodingBitrate);
                            if (hr == GlobalStructures.HRESULTMF.S_OK)
                            {
                                hr = pMediaType2.SetUINT32(MF_MT_AVG_BITRATE, uiEncodingBitrate);
                            }
                        }
                        // Connect the video stream to the recording sink.                      
                        hr = pRecord.AddStream((uint)MF_CAPTURE_ENGINE_MEDIA_TYPE.MF_CAPTURE_ENGINE_PREFERRED_SOURCE_STREAM_FOR_VIDEO_RECORD, pMediaType2, null, out dwSinkStreamIndex);
                    }
                    SafeRelease(ref pMediaType2);
                }
                SafeRelease(ref pMediaType);
            }
            return hr;
        }

        GlobalStructures.HRESULTMF GetEncodingBitrate(IMFMediaType pMediaType, out uint uiEncodingBitrate)
        {
            uint uiWidth;
            uint uiHeight;
            float uiBitrate;
            uint uiFrameRateNum = 0;
            uint uiFrameRateDenom = 0;
            GlobalStructures.HRESULTMF hr = MFGetAttributeSize(pMediaType, MF_MT_FRAME_SIZE, out uiWidth, out uiHeight);
            if (hr == GlobalStructures.HRESULTMF.S_OK)
            {
                hr = MFGetAttributeRatio(pMediaType, MF_MT_FRAME_RATE, out uiFrameRateNum, out uiFrameRateDenom);
            }
            uiBitrate = uiWidth / 3.0f * uiHeight * uiFrameRateNum / uiFrameRateDenom;
            uiEncodingBitrate = (uint)uiBitrate;
            return hr;
        }

        GlobalStructures.HRESULTMF ConfigureAudioEncoding(IMFCaptureSource pSource, IMFCaptureRecordSink pRecord, Guid guidEncodingType)
        {
            IMFCollection pAvailableTypes = null;
            IMFMediaType pMediaType = null;
            IMFAttributes pAttributes = null;

            // Configure the audio format for the recording sink.

            GlobalStructures.HRESULTMF hr = MFCreateAttributes(out pAttributes, 1);
            if (hr == GlobalStructures.HRESULTMF.S_OK)
            {
                // Enumerate low latency media types
                hr = pAttributes.SetUINT32(MF_LOW_LATENCY, 1);
                // Get a list of encoded output formats that are supported by the encoder.
                hr = MFTranscodeGetAudioOutputAvailableTypes(ref guidEncodingType, (uint)(MFT_ENUM_FLAG.MFT_ENUM_FLAG_ALL | MFT_ENUM_FLAG.MFT_ENUM_FLAG_SORTANDFILTER),
                    pAttributes, out pAvailableTypes);
                if (hr == GlobalStructures.HRESULTMF.S_OK)
                {
                    // Pick the first format from the list.
                    //hr = GetCollectionObject(pAvailableTypes, 0, out pMediaType);
                    IntPtr pUnk = nint.Zero;
                    //hr = pAvailableTypes.GetElement(31, out pUnk);
                    hr = pAvailableTypes.GetElement(0, out pUnk);

                    if (0 == 1)
                    {
                        hr = pAvailableTypes.GetElementCount(out uint nCount);
                        for (uint i = 0; i < nCount; i++)
                        {
                            IntPtr pUnknown = nint.Zero;
                            hr = pAvailableTypes.GetElement(i, out pUnknown);
                            if (hr == GlobalStructures.HRESULTMF.S_OK)
                            {
                                pMediaType = Marshal.GetObjectForIUnknown(pUnknown) as IMFMediaType;
                                uint nAVG_BITRATE = 0;
                                pMediaType.GetUINT32(MF_MT_AVG_BITRATE, out nAVG_BITRATE);
                                Guid guid = Guid.Empty;
                                // MFAudioFormat_AAC = new Guid("00001610-0000-0010-8000-00aa00389b71");
                                pMediaType.GetGUID(MF_MT_SUBTYPE, out guid);
                                uint nChannels = 0;
                                pMediaType.GetUINT32(MF_MT_AUDIO_NUM_CHANNELS, out nChannels);
                                uint nBITS_PER_SAMPLE = 0;
                                pMediaType.GetUINT32(MF_MT_AUDIO_BITS_PER_SAMPLE, out nBITS_PER_SAMPLE);
                                uint nSAMPLES_PER_SECOND = 0;
                                pMediaType.GetUINT32(MF_MT_AUDIO_SAMPLES_PER_SECOND, out nSAMPLES_PER_SECOND);
                                uint nAVG_BYTES_PER_SECOND = 0;
                                pMediaType.GetUINT32(MF_MT_AUDIO_AVG_BYTES_PER_SECOND, out nAVG_BYTES_PER_SECOND);
                                uint nBLOCK_ALIGNMENT = 0;
                                pMediaType.GetUINT32(MF_MT_AUDIO_BLOCK_ALIGNMENT, out nBLOCK_ALIGNMENT);                                

                                uint nWMADRC_PEAKREF = 0;
                                pMediaType.GetUINT32(MF_MT_AUDIO_WMADRC_PEAKREF, out nWMADRC_PEAKREF);
                                uint nWMADRC_PEAKTARGET = 0;
                                pMediaType.GetUINT32(MF_MT_AUDIO_WMADRC_PEAKTARGET, out nWMADRC_PEAKTARGET);
                                uint nWMADRC_AVGREF = 0;
                                pMediaType.GetUINT32(MF_MT_AUDIO_WMADRC_AVGREF, out nWMADRC_AVGREF);
                                uint nWMADRC_AVGTARGET = 0;
                                pMediaType.GetUINT32(MF_MT_AUDIO_WMADRC_AVGTARGET, out nWMADRC_AVGTARGET);

                                Debug.WriteLine("Media Type {0}", i);
                                Debug.WriteLine("\tMF_MT_SUBTYPE: {0}", guid);
                                Debug.WriteLine("\tMF_MT_AVG_BITRATE: {0}", nAVG_BITRATE);
                                Debug.WriteLine("\tMF_MT_AUDIO_NUM_CHANNELS: {0}", nChannels);
                                Debug.WriteLine("\tMF_MT_AUDIO_BITS_PER_SAMPLE: {0}", nBITS_PER_SAMPLE);
                                Debug.WriteLine("\tMF_MT_AUDIO_SAMPLES_PER_SECOND: {0}", nSAMPLES_PER_SECOND);
                                Debug.WriteLine("\tMF_MT_AUDIO_AVG_BYTES_PER_SECOND: {0}", nAVG_BYTES_PER_SECOND);
                                Debug.WriteLine("\tMF_MT_AUDIO_BLOCK_ALIGNMENT: {0}", nBLOCK_ALIGNMENT);
                                Debug.WriteLine("\tMF_MT_AUDIO_WMADRC_PEAKREF: {0}", nWMADRC_PEAKREF);
                                Debug.WriteLine("\tMF_MT_AUDIO_WMADRC_PEAKTARGET: {0}", nWMADRC_PEAKTARGET);
                                Debug.WriteLine("\tMF_MT_AUDIO_WMADRC_AVGREF: {0}", nWMADRC_AVGREF);
                                Debug.WriteLine("\tMF_MT_AUDIO_WMADRC_AVGTARGET: {0}", nWMADRC_AVGTARGET);

                                SafeRelease(ref pMediaType);
                            }
                        }
                    }

                    if (hr == GlobalStructures.HRESULTMF.S_OK)
                    {
                        pMediaType = Marshal.GetObjectForIUnknown(pUnk) as IMFMediaType;

                        // Connect the audio stream to the recording sink.
                        uint dwSinkStreamIndex;
                        hr = pRecord.AddStream((uint)MF_CAPTURE_ENGINE_MEDIA_TYPE.MF_CAPTURE_ENGINE_PREFERRED_SOURCE_STREAM_FOR_AUDIO, pMediaType, null, out dwSinkStreamIndex);
                        if (hr == (GlobalStructures.HRESULTMF)MF_E_INVALIDSTREAMNUMBER)
                        {
                            //If an audio device is not present, allow video only recording
                            hr = GlobalStructures.HRESULTMF.S_OK;
                        }
                        SafeRelease(ref pMediaType);
                        Marshal.Release(pUnk);
                    }
                    SafeRelease(ref pAvailableTypes);
                }
                SafeRelease(ref pAttributes);
            }
            return hr;
        }

        public GlobalStructures.HRESULTMF TakePhoto(string pszFileName, Guid guidImage, ref IMFByteStream byteStream)
        {
            IMFCaptureSink pSink = null;
            IMFCapturePhotoSink pPhoto = null;
            IMFCaptureSource pSource = null;
            IMFMediaType pMediaType = null;
            IMFMediaType pMediaType2 = null;
            bool bHasPhotoStream = true;
            GlobalStructures.HRESULTMF hr = GlobalStructures.HRESULTMF.S_OK;

            if (!m_bTakingPhoto)
            {
                // Get a pointer to the photo sink.
                hr = m_pCaptureEngine.GetSink(MF_CAPTURE_ENGINE_SINK_TYPE.MF_CAPTURE_ENGINE_SINK_TYPE_PHOTO, out pSink);
                if (hr == GlobalStructures.HRESULTMF.S_OK)
                {
                    pPhoto = (IMFCapturePhotoSink)pSink;
                    hr = m_pCaptureEngine.GetSource(out pSource);
                    if (hr == GlobalStructures.HRESULTMF.S_OK)
                    {
                        hr = pSource.GetCurrentDeviceMediaType((uint)MF_CAPTURE_ENGINE_MEDIA_TYPE.MF_CAPTURE_ENGINE_PREFERRED_SOURCE_STREAM_FOR_PHOTO, out pMediaType);
                        if (hr == GlobalStructures.HRESULTMF.S_OK)
                        {
                            //Configure the photo format
                            hr = CreatePhotoMediaType(pMediaType, out pMediaType2, guidImage);
                            if (hr == GlobalStructures.HRESULTMF.S_OK)
                            {
                                hr = pPhoto.RemoveAllStreams();
                                if (hr == GlobalStructures.HRESULTMF.S_OK)
                                {
                                    uint dwSinkStreamIndex;
                                    // Try to connect the first still image stream to the photo sink
                                    if (bHasPhotoStream)
                                    {
                                        hr = pPhoto.AddStream((uint)MF_CAPTURE_ENGINE_MEDIA_TYPE.MF_CAPTURE_ENGINE_PREFERRED_SOURCE_STREAM_FOR_PHOTO, pMediaType2, null, out dwSinkStreamIndex);
                                        if (hr == GlobalStructures.HRESULTMF.S_OK)
                                        {
                                            if (pszFileName != null)
                                                hr = pPhoto.SetOutputFileName(pszFileName);
                                            else
                                                hr = pPhoto.SetOutputByteStream(byteStream);

                                            //m_sImageFileName = pszFileName;
                                            //hr = pPhoto.SetSampleCallback(this);

                                            if (hr == GlobalStructures.HRESULTMF.S_OK)
                                            {
                                                try
                                                {
                                                    m_bTakingPhoto = true;
                                                    ResetEvent();
                                                    hr = m_pCaptureEngine.TakePhoto();
                                                    WaitForResult();
                                                }
                                                catch (NotImplementedException niex)
                                                {

                                                }
                                                catch (InvalidOperationException ioex)
                                                {

                                                }
                                                catch (Exception ex)
                                                {

                                                }
                                            }
                                        }
                                    }
                                }
                                SafeRelease(ref pMediaType2);
                            }
                            SafeRelease(ref pMediaType);
                        }
                        SafeRelease(ref pSource);
                    }
                    SafeRelease(ref pSink);
                }
            }
            return hr;
        }

        GlobalStructures.HRESULTMF CreatePhotoMediaType(IMFMediaType pSrcMediaType, out IMFMediaType ppPhotoMediaType, Guid guidImage)
        {
            ppPhotoMediaType = null;
            
            //uint uiFrameRateNumerator = 30;
            //uint uiFrameRateDenominator = 1;

            IMFMediaType pPhotoMediaType = null;

            GlobalStructures.HRESULTMF hr = MFCreateMediaType(out pPhotoMediaType);
            if (hr == GlobalStructures.HRESULTMF.S_OK)
            {
                hr = pPhotoMediaType.SetGUID(MF_MT_MAJOR_TYPE, MFMediaType_Image);
                if (hr == GlobalStructures.HRESULTMF.S_OK)
                {
                    hr = pPhotoMediaType.SetGUID(MF_MT_SUBTYPE, guidImage);
                    if (hr == GlobalStructures.HRESULTMF.S_OK)
                    {
                        hr = CopyAttribute(pSrcMediaType, pPhotoMediaType, MF_MT_FRAME_SIZE);
                        ppPhotoMediaType = pPhotoMediaType;
                    }
                }
                //SafeRelease(ref pPhotoMediaType);
            }      
            return hr;
        }

        public void SetCaptureEvent()
        {
            SetEvent(m_hEvent);
        }

        GlobalStructures.HRESULTMF IMFCaptureEngineOnEventCallback.OnEvent(IMFMediaEvent pEvent)
        {
            GlobalStructures.HRESULTMF hr = GlobalStructures.HRESULTMF.S_OK;
            if (pEvent != null)
            {
                try
                {
                    Guid guidType;
                    hr = pEvent.GetExtendedType(out guidType);
                    if (guidType == MF_CAPTURE_ENGINE_INITIALIZED)
                    {
                        //hr = GetDeviceData();
                    }
                    else if (guidType == MF_CAPTURE_ENGINE_PREVIEW_STARTED)
                    {
                        m_bPreview = true;
                    }
                    else if (guidType == MF_CAPTURE_ENGINE_PREVIEW_STOPPED)
                    {
                        m_bPreview = false;
                    }
                    else if (guidType == MF_CAPTURE_ENGINE_RECORD_STARTED)
                    {
                        m_bRecording = true;
                    }
                    else if (guidType == MF_CAPTURE_ENGINE_RECORD_STOPPED)
                    {
                        m_bRecording = false;
                    }
                    else if (guidType == MF_CAPTURE_ENGINE_PHOTO_TAKEN)
                    {
                        m_bTakingPhoto = false;
                    }
                    else if (guidType == MF_CAPTURE_ENGINE_ALL_EFFECTS_REMOVED)
                    {                      
                    }
                    else if (guidType == MF_CAPTURE_ENGINE_EFFECT_ADDED)
                    {                      
                    }

                    SetEvent(m_hEvent);

                    IntPtr pEventPtr = Marshal.GetIUnknownForObject(pEvent);
                    PostMessage(m_hWnd, WM_APP_CAPTURE_EVENT, pEventPtr, IntPtr.Zero);
                    Marshal.Release(pEventPtr);                  

                    //Marshal.ReleaseComObject(pEvent);
                }
                catch (NotImplementedException niex)
                {

                }
                catch (InvalidOperationException ioex)
                {

                }
                catch (Exception ex)
                {

                }
            }
            return hr;
        }
        
        void SetBitmap()
        {
            // To avoid RPC_E_WRONG_THREAD
            bool isQueued = _mw.DispatcherQueue.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal, async () =>
            {
                if (m_WriteableBitmapCapture != null)
                    m_WriteableBitmapCapture.Invalidate();
                m_WriteableBitmapCapture = new Microsoft.UI.Xaml.Media.Imaging.WriteableBitmap((int)m_nWidth, (int)m_nHeight);
                await m_WriteableBitmapCapture.PixelBuffer.AsStream().WriteAsync(m_pBytesArray, 0, m_pBytesArray.Length);
                _mw.img1.Source = m_WriteableBitmapCapture;
            });
        }

        byte[] m_pBytesArray;
        void IMFCaptureEngineOnSampleCallback.OnSample(IMFSample pSample)
        {
            GlobalStructures.HRESULTMF hr = GlobalStructures.HRESULTMF.S_OK;
            if (pSample != null)
            {
                IMFMediaBuffer pMediaBuffer = null;
                hr = pSample.GetBufferByIndex(0, out pMediaBuffer);
                if (hr == GlobalStructures.HRESULTMF.S_OK)
                {
                    if (m_bPreview)
                    {
                        if (!m_bTakingPhoto)
                        {
                            IntPtr pData = nint.Zero;
                            hr = pMediaBuffer.Lock(out pData, out int nMaxLenght, out int nCurrentLenght);
                            if (hr == GlobalStructures.HRESULTMF.S_OK)
                            {
                                Marshal.Copy(pData, m_pBytesArray, 0, nCurrentLenght);
                                hr = pMediaBuffer.Unlock();
                                SetBitmap();
                            }                            
                        }
                    }
                    else if (m_bTakingPhoto)
                    {
                        IntPtr pData = nint.Zero;
                        hr = pMediaBuffer.Lock(out pData, out int nMaxLenght, out int nCurrentLenght);
                        if (hr == GlobalStructures.HRESULTMF.S_OK)
                        {
                            //byte[] pBytesArray = new byte[nCurrentLenght];
                            //Marshal.Copy(pData, pBytesArray, 0, nCurrentLenght);
                            //using var bw = new System.IO.BinaryWriter(System.IO.File.OpenWrite("E:\\takingphoto.bmp"));
                            //bw.Write(pBytesArray);

                            hr = pMediaBuffer.Unlock();
                        }
                    }
                    SafeRelease(ref pMediaBuffer);
                }             
                SafeRelease(ref pSample);
            }     
        }

        // MF_E_CAPTURE_SINK_MIRROR_ERROR
        public MFError.HRESULTMF SetMirrorState(bool bMirror)
        {  
            MFError.HRESULTMF hr = MFError.HRESULTMF.E_FAIL;
            if (this.m_pCapturePreviewSink != null)
              hr = (MFError.HRESULTMF)this.m_pCapturePreviewSink.SetMirrorState(bMirror);
            return hr;
        }

        public GlobalStructures.HRESULTMF SetRotation(bool bRecord, uint dwStreamIndex, uint dwRotationValue)
        {
            GlobalStructures.HRESULTMF hr = GlobalStructures.HRESULTMF.E_FAIL;
            if (!bRecord)
            {
                if (this.m_pCapturePreviewSink != null)
                    hr = this.m_pCapturePreviewSink.SetRotation(dwStreamIndex, dwRotationValue);
            }
            else
            {
                if (this.m_pCaptureRecordSink != null)
                    hr = this.m_pCaptureRecordSink.SetRotation(dwStreamIndex, dwRotationValue);
            }
            return hr;
        }

        private bool bDisposedValue = false;

        protected virtual void Dispose(bool bDisposing)
        {
            if (!bDisposedValue)
            {
                if (bDisposing)
                {
                    // TODO: dispose managed state (managed objects)
                }
                Clean();
                bDisposedValue = true;
            }
        }

        // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~CCaptureEngine()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(bDisposing: false);
        }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(bDisposing: true);
            //GC.SuppressFinalize(this);
        }

        private void Clean()
        {
            if (m_pCaptureEngine != null && m_bRecording)
                this.StopRecord();
            if (m_pCaptureEngine != null && m_bPreview)
                this.StopPreview();          
            SafeRelease(ref m_pTransform); 
            SafeRelease(ref m_pCaptureRecordSink);
            SafeRelease(ref m_pCapturePreviewSink);
            SafeRelease(ref m_pCaptureSource);          
            SafeRelease(ref m_pCaptureEngine);
            SafeRelease(ref m_pDXGIDeviceManager);
            if (m_pD3D11DevicePtr != IntPtr.Zero)
                Marshal.Release(m_pD3D11DevicePtr);
            if (m_pD3D11DeviceContextPtr != IntPtr.Zero)
                Marshal.Release(m_pD3D11DeviceContextPtr);
            SafeRelease(ref m_pAttributes);
            SafeRelease(ref m_pCaptureEngineClassFactory);
            CloseHandle(m_hEvent);
            //MFShutdown();
        }
    }
}
