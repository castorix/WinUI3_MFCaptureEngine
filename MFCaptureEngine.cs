using System;
using System.Reflection;
using System.Reflection.Metadata;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using GlobalStructures;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Media;
using Windows.Devices.Enumeration.Pnp;
using Windows.Media.Core;

namespace MFCaptureEngine
{
    internal class MFCaptureEngineTools
    {
        [DllImport("Mfplat.dll", SetLastError = true)]
        public static extern HRESULT MFStartup(uint Version, uint dwFlags = 0);

        public const int MF_SDK_VERSION = 0x0002;
        public const int MF_API_VERSION = 0x0070; // This value is unused in the Win7 release and left at its Vista release value
        public const int MF_VERSION = (MF_SDK_VERSION << 16 | MF_API_VERSION);

        [DllImport("Mfplat.dll", SetLastError = true)]
        public static extern HRESULT MFShutdown();

        public static readonly Guid CLSID_MFCaptureEngine = new Guid(0xefce38d3, 0x8914, 0x4674, 0xa7, 0xdf, 0xae, 0x1b, 0x3d, 0x65, 0x4b, 0x8a);
        public static readonly Guid CLSID_MFCaptureEngineClassFactory = new Guid(0xefce38d3, 0x8914, 0x4674, 0xa7, 0xdf, 0xae, 0x1b, 0x3d, 0x65, 0x4b, 0x8a);
        public static readonly Guid MFSampleExtension_DeviceReferenceSystemTime = new Guid(0x6523775a, 0xba2d, 0x405f, 0xb2, 0xc5, 0x01, 0xff, 0x88, 0xe2, 0xe8, 0xf6);
        public static readonly Guid MF_CAPTURE_ENGINE_INITIALIZED = new Guid(0x219992bc, 0xcf92, 0x4531, 0xa1, 0xae, 0x96, 0xe1, 0xe8, 0x86, 0xc8, 0xf1);
        public static readonly Guid MF_CAPTURE_ENGINE_PREVIEW_STARTED = new Guid(0xa416df21, 0xf9d3, 0x4a74, 0x99, 0x1b, 0xb8, 0x17, 0x29, 0x89, 0x52, 0xc4);
        public static readonly Guid MF_CAPTURE_ENGINE_PREVIEW_STOPPED = new Guid(0x13d5143c, 0x1edd, 0x4e50, 0xa2, 0xef, 0x35, 0x0a, 0x47, 0x67, 0x80, 0x60);
        public static readonly Guid MF_CAPTURE_ENGINE_RECORD_STARTED = new Guid(0xac2b027b, 0xddf9, 0x48a0, 0x89, 0xbe, 0x38, 0xab, 0x35, 0xef, 0x45, 0xc0);
        public static readonly Guid MF_CAPTURE_ENGINE_RECORD_STOPPED = new Guid(0x55e5200a, 0xf98f, 0x4c0d, 0xa9, 0xec, 0x9e, 0xb2, 0x5e, 0xd3, 0xd7, 0x73);
        public static readonly Guid MF_CAPTURE_ENGINE_PHOTO_TAKEN = new Guid(0x3c50c445, 0x7304, 0x48eb, 0x86, 0x5d, 0xbb, 0xa1, 0x9b, 0xa3, 0xaf, 0x5c);
        public static readonly Guid MF_CAPTURE_SOURCE_CURRENT_DEVICE_MEDIA_TYPE_SET = new Guid(0xe7e75e4c, 0x039c, 0x4410, 0x81, 0x5b, 0x87, 0x41, 0x30, 0x7b, 0x63, 0xaa);
        public static readonly Guid MF_CAPTURE_ENGINE_ERROR = new Guid(0x46b89fc6, 0x33cc, 0x4399, 0x9d, 0xad, 0x78, 0x4d, 0xe7, 0x7d, 0x58, 0x7c);
        public static readonly Guid MF_CAPTURE_ENGINE_EFFECT_ADDED = new Guid(0xaa8dc7b5, 0xa048, 0x4e13, 0x8e, 0xbe, 0xf2, 0x3c, 0x46, 0xc8, 0x30, 0xc1);
        public static readonly Guid MF_CAPTURE_ENGINE_EFFECT_REMOVED = new Guid(0xc6e8db07, 0xfb09, 0x4a48, 0x89, 0xc6, 0xbf, 0x92, 0xa0, 0x42, 0x22, 0xc9);
        public static readonly Guid MF_CAPTURE_ENGINE_ALL_EFFECTS_REMOVED = new Guid(0xfded7521, 0x8ed8, 0x431a, 0xa9, 0x6b, 0xf3, 0xe2, 0x56, 0x5e, 0x98, 0x1c);
        public static readonly Guid MF_CAPTURE_SINK_PREPARED = new Guid(0x7BFCE257, 0x12B1, 0x4409, 0x8C, 0x34, 0xD4, 0x45, 0xDA, 0xAB, 0x75, 0x78);
        public static readonly Guid MF_CAPTURE_ENGINE_OUTPUT_MEDIA_TYPE_SET = new Guid(0xcaaad994, 0x83ec, 0x45e9, 0xa3, 0x0a, 0x1f, 0x20, 0xaa, 0xdb, 0x98, 0x31);
        public static readonly Guid MF_CAPTURE_ENGINE_CAMERA_STREAM_BLOCKED = new Guid(0xA4209417, 0x8D39, 0x46F3, 0xB7, 0x59, 0x59, 0x12, 0x52, 0x8F, 0x42, 0x07);
        public static readonly Guid MF_CAPTURE_ENGINE_CAMERA_STREAM_UNBLOCKED = new Guid(0x9BE9EEF0, 0xCDAF, 0x4717, 0x85, 0x64, 0x83, 0x4A, 0xAE, 0x66, 0x41, 0x5C);
        public static readonly Guid MF_CAPTURE_ENGINE_D3D_MANAGER = new Guid(0x76e25e7b, 0xd595, 0x4283, 0x96, 0x2c, 0xc5, 0x94, 0xaf, 0xd7, 0x8d, 0xdf);
        public static readonly Guid MF_CAPTURE_ENGINE_RECORD_SINK_VIDEO_MAX_UNPROCESSED_SAMPLES = new Guid(0xb467f705, 0x7913, 0x4894, 0x9d, 0x42, 0xa2, 0x15, 0xfe, 0xa2, 0x3d, 0xa9);
        public static readonly Guid MF_CAPTURE_ENGINE_RECORD_SINK_AUDIO_MAX_UNPROCESSED_SAMPLES = new Guid(0x1cddb141, 0xa7f4, 0x4d58, 0x98, 0x96, 0x4d, 0x15, 0xa5, 0x3c, 0x4e, 0xfe);
        public static readonly Guid MF_CAPTURE_ENGINE_RECORD_SINK_VIDEO_MAX_PROCESSED_SAMPLES = new Guid(0xe7b4a49e, 0x382c, 0x4aef, 0xa9, 0x46, 0xae, 0xd5, 0x49, 0xb, 0x71, 0x11);
        public static readonly Guid MF_CAPTURE_ENGINE_RECORD_SINK_AUDIO_MAX_PROCESSED_SAMPLES = new Guid(0x9896e12a, 0xf707, 0x4500, 0xb6, 0xbd, 0xdb, 0x8e, 0xb8, 0x10, 0xb5, 0xf);
        public static readonly Guid MF_CAPTURE_ENGINE_USE_AUDIO_DEVICE_ONLY = new Guid(0x1c8077da, 0x8466, 0x4dc4, 0x8b, 0x8e, 0x27, 0x6b, 0x3f, 0x85, 0x92, 0x3b);
        public static readonly Guid MF_CAPTURE_ENGINE_USE_VIDEO_DEVICE_ONLY = new Guid(0x7e025171, 0xcf32, 0x4f2e, 0x8f, 0x19, 0x41, 0x5, 0x77, 0xb7, 0x3a, 0x66);
        public static readonly Guid MF_CAPTURE_ENGINE_DISABLE_HARDWARE_TRANSFORMS = new Guid(0xb7c42a6b, 0x3207, 0x4495, 0xb4, 0xe7, 0x81, 0xf9, 0xc3, 0x5d, 0x59, 0x91);
        public static readonly Guid MF_CAPTURE_ENGINE_DISABLE_DXVA = new Guid(0xf9818862, 0x179d, 0x433f, 0xa3, 0x2f, 0x74, 0xcb, 0xcf, 0x74, 0x46, 0x6d);
        public static readonly Guid MF_CAPTURE_ENGINE_MEDIASOURCE_CONFIG = new Guid(0xbc6989d2, 0x0fc1, 0x46e1, 0xa7, 0x4f, 0xef, 0xd3, 0x6b, 0xc7, 0x88, 0xde);
        public static readonly Guid MF_CAPTURE_ENGINE_DECODER_MFT_FIELDOFUSE_UNLOCK_Attribute = new Guid(0x2b8ad2e8, 0x7acb, 0x4321, 0xa6, 0x06, 0x32, 0x5c, 0x42, 0x49, 0xf4, 0xfc);
        public static readonly Guid MF_CAPTURE_ENGINE_ENCODER_MFT_FIELDOFUSE_UNLOCK_Attribute = new Guid(0x54c63a00, 0x78d5, 0x422f, 0xaa, 0x3e, 0x5e, 0x99, 0xac, 0x64, 0x92, 0x69);
        public static readonly Guid MF_CAPTURE_ENGINE_ENABLE_CAMERA_STREAMSTATE_NOTIFICATION = new Guid(0x4C808E9D, 0xAAED, 0x4713, 0x90, 0xFB, 0xCB, 0x24, 0x06, 0x4A, 0xB8, 0xDA);
        public static readonly Guid MF_CAPTURE_ENGINE_MEDIA_CATEGORY = new Guid(0x8e3f5bd5, 0xdbbf, 0x42f0, 0x85, 0x42, 0xd0, 0x7a, 0x39, 0x71, 0x76, 0x2a);
        public static readonly Guid MF_CAPTURE_ENGINE_AUDIO_PROCESSING = new Guid(0x10f1be5e, 0x7e11, 0x410b, 0x97, 0x3d, 0xf4, 0xb6, 0x10, 0x90, 0x0, 0xfe);
        public static readonly Guid MF_CAPTURE_ENGINE_EVENT_GENERATOR_GUID = new Guid(0xabfa8ad5, 0xfc6d, 0x4911, 0x87, 0xe0, 0x96, 0x19, 0x45, 0xf8, 0xf7, 0xce);
        public static readonly Guid MF_CAPTURE_ENGINE_EVENT_STREAM_INDEX = new Guid(0x82697f44, 0xb1cf, 0x42eb, 0x97, 0x53, 0xf8, 0x6d, 0x64, 0x9c, 0x88, 0x65);
        public static readonly Guid MF_CAPTURE_ENGINE_SELECTEDCAMERAPROFILE = new Guid(0x03160B7E, 0x1C6F, 0x4DB2, 0xAD, 0x56, 0xA7, 0xC4, 0x30, 0xF8, 0x23, 0x92);
        public static readonly Guid MF_CAPTURE_ENGINE_SELECTEDCAMERAPROFILE_INDEX = new Guid(0x3CE88613, 0x2214, 0x46C3, 0xB4, 0x17, 0x82, 0xF8, 0xA3, 0x13, 0xC9, 0xC3);

        [DllImport("Mfplat.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern HRESULT MFCreateAttributes(out IMFAttributes ppMFAttributes, uint cInitialSize);

        [DllImport("Mfplat.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern HRESULT MFCreateMediaType(out IMFMediaType ppMFType);

        public static readonly Guid MF_MT_MAJOR_TYPE = new Guid("48eba18e-f8c9-4687-bf11-0a74c9f96a8f");
        public static readonly Guid MF_MT_SUBTYPE = new Guid("f7e34c9a-42e8-4714-b74b-cb29d72c35e5");
        public static readonly Guid MF_MT_INTERLACE_MODE = new Guid("E2724BB8-E676-4806-B4B2-A8D6EFB44CCD");
        public static readonly Guid MF_MT_FRAME_SIZE = new Guid("1652C33D-D6B2-4012-B834-72030849A37D");
        public static readonly Guid MF_MT_FRAME_RATE = new Guid("C459A2E8-3D2C-4E44-B132-FEE5156C7BB0");
        public static readonly Guid MF_MT_PIXEL_ASPECT_RATIO = new Guid("C6376A1E-8D0A-4027-BE45-6D9A0AD39BB6");
        public static readonly Guid MF_MT_DEFAULT_STRIDE = new Guid("644b4e48-1e02-4516-b0eb-c01ca9d49ac6");


        // {3e23d450-2c75-4d25-a00e-b91670d12327}   MF_MT_YUV_MATRIX                {UINT32 (oneof MFVideoTransferMatrix)}
        public static readonly Guid MF_MT_YUV_MATRIX = new Guid(0x3e23d450, 0x2c75, 0x4d25, 0xa0, 0x0e, 0xb9, 0x16, 0x70, 0xd1, 0x23, 0x27);

        // {53a0529c-890b-4216-8bf9-599367ad6d20}   MF_MT_VIDEO_LIGHTING            {UINT32 (oneof MFVideoLighting)}
        public static readonly Guid MF_MT_VIDEO_LIGHTING = new Guid(0x53a0529c, 0x890b, 0x4216, 0x8b, 0xf9, 0x59, 0x93, 0x67, 0xad, 0x6d, 0x20);

        // {c21b8ee5-b956-4071-8daf-325edf5cab11}   MF_MT_VIDEO_NOMINAL_RANGE       {UINT32 (oneof MFNominalRange)}
        public static readonly Guid MF_MT_VIDEO_NOMINAL_RANGE = new Guid(0xc21b8ee5, 0xb956, 0x4071, 0x8d, 0xaf, 0x32, 0x5e, 0xdf, 0x5c, 0xab, 0x11);

        // {66758743-7e5f-400d-980a-aa8596c85696}   MF_MT_GEOMETRIC_APERTURE        {BLOB (MFVideoArea)}
        public static readonly Guid MF_MT_GEOMETRIC_APERTURE = new Guid(0x66758743, 0x7e5f, 0x400d, 0x98, 0x0a, 0xaa, 0x85, 0x96, 0xc8, 0x56, 0x96);

        // {d7388766-18fe-48c6-a177-ee894867c8c4}   MF_MT_MINIMUM_DISPLAY_APERTURE  {BLOB (MFVideoArea)}
        public static readonly Guid MF_MT_MINIMUM_DISPLAY_APERTURE = new Guid(0xd7388766, 0x18fe, 0x48c6, 0xa1, 0x77, 0xee, 0x89, 0x48, 0x67, 0xc8, 0xc4);

        // {79614dde-9187-48fb-b8c7-4d52689de649}   MF_MT_PAN_SCAN_APERTURE         {BLOB (MFVideoArea)}
        public static readonly Guid MF_MT_PAN_SCAN_APERTURE = new Guid(0x79614dde, 0x9187, 0x48fb, 0xb8, 0xc7, 0x4d, 0x52, 0x68, 0x9d, 0xe6, 0x49);

        // {4b7f6bc3-8b13-40b2-a993-abf630b8204e}   MF_MT_PAN_SCAN_ENABLED          {UINT32 (BOOL)}
        public static readonly Guid MF_MT_PAN_SCAN_ENABLED = new Guid(0x4b7f6bc3, 0x8b13, 0x40b2, 0xa9, 0x93, 0xab, 0xf6, 0x30, 0xb8, 0x20, 0x4e);

        // {20332624-fb0d-4d9e-bd0d-cbf6786c102e}   MF_MT_AVG_BITRATE               {UINT32}
        public static readonly Guid MF_MT_AVG_BITRATE = new Guid(0x20332624, 0xfb0d, 0x4d9e, 0xbd, 0x0d, 0xcb, 0xf6, 0x78, 0x6c, 0x10, 0x2e);

        // {799cabd6-3508-4db4-a3c7-569cd533deb1}   MF_MT_AVG_BIT_ERROR_RATE        {UINT32}
        public static readonly Guid MF_MT_AVG_BIT_ERROR_RATE = new Guid(0x799cabd6, 0x3508, 0x4db4, 0xa3, 0xc7, 0x56, 0x9c, 0xd5, 0x33, 0xde, 0xb1);

        // {c16eb52b-73a1-476f-8d62-839d6a020652}   MF_MT_MAX_KEYFRAME_SPACING      {UINT32}
        public static readonly Guid MF_MT_MAX_KEYFRAME_SPACING = new Guid(0xc16eb52b, 0x73a1, 0x476f, 0x8d, 0x62, 0x83, 0x9d, 0x6a, 0x02, 0x06, 0x52);

        // {b6bc765f-4c3b-40a4-bd51-2535b66fe09d}   MF_MT_USER_DATA                 {BLOB}
        public static readonly Guid MF_MT_USER_DATA = new Guid(0xb6bc765f, 0x4c3b, 0x40a4, 0xbd, 0x51, 0x25, 0x35, 0xb6, 0x6f, 0xe0, 0x9d);

        // {a505d3ac-f930-436e-8ede-93a509ce23b2} MF_MT_OUTPUT_BUFFER_NUM {UINT32}
        public static readonly Guid MF_MT_OUTPUT_BUFFER_NUM = new Guid(0xa505d3ac, 0xf930, 0x436e, 0x8e, 0xde, 0x93, 0xa5, 0x09, 0xce, 0x23, 0xb2);

        /// {0xbb12d222,0x2bdb,0x425e,0x91,0xec,0x23,0x08,0xe1,0x89,0xa5,0x8f}   MF_MT_REALTIME_CONTENT UINT32 (0 or 1)
        public static readonly Guid MF_MT_REALTIME_CONTENT = new Guid(0xbb12d222, 0x2bdb, 0x425e, 0x91, 0xec, 0x23, 0x08, 0xe1, 0x89, 0xa5, 0x8f);

        //
        // AUDIO data
        //

        // {37e48bf5-645e-4c5b-89de-ada9e29b696a}   MF_MT_AUDIO_NUM_CHANNELS            {UINT32}
        public static readonly Guid MF_MT_AUDIO_NUM_CHANNELS = new Guid(0x37e48bf5, 0x645e, 0x4c5b, 0x89, 0xde, 0xad, 0xa9, 0xe2, 0x9b, 0x69, 0x6a);

        // {5faeeae7-0290-4c31-9e8a-c534f68d9dba}   MF_MT_AUDIO_SAMPLES_PER_SECOND      {UINT32}
        public static readonly Guid MF_MT_AUDIO_SAMPLES_PER_SECOND = new Guid(0x5faeeae7, 0x0290, 0x4c31, 0x9e, 0x8a, 0xc5, 0x34, 0xf6, 0x8d, 0x9d, 0xba);

        // {fb3b724a-cfb5-4319-aefe-6e42b2406132}   MF_MT_AUDIO_FLOAT_SAMPLES_PER_SECOND {double}
        public static readonly Guid MF_MT_AUDIO_FLOAT_SAMPLES_PER_SECOND = new Guid(0xfb3b724a, 0xcfb5, 0x4319, 0xae, 0xfe, 0x6e, 0x42, 0xb2, 0x40, 0x61, 0x32);

        // {1aab75c8-cfef-451c-ab95-ac034b8e1731}   MF_MT_AUDIO_AVG_BYTES_PER_SECOND    {UINT32}
        public static readonly Guid MF_MT_AUDIO_AVG_BYTES_PER_SECOND = new Guid(0x1aab75c8, 0xcfef, 0x451c, 0xab, 0x95, 0xac, 0x03, 0x4b, 0x8e, 0x17, 0x31);

        // {322de230-9eeb-43bd-ab7a-ff412251541d}   MF_MT_AUDIO_BLOCK_ALIGNMENT         {UINT32}
        public static readonly Guid MF_MT_AUDIO_BLOCK_ALIGNMENT = new Guid(0x322de230, 0x9eeb, 0x43bd, 0xab, 0x7a, 0xff, 0x41, 0x22, 0x51, 0x54, 0x1d);

        // {f2deb57f-40fa-4764-aa33-ed4f2d1ff669}   MF_MT_AUDIO_BITS_PER_SAMPLE         {UINT32}
        public static readonly Guid MF_MT_AUDIO_BITS_PER_SAMPLE = new Guid(0xf2deb57f, 0x40fa, 0x4764, 0xaa, 0x33, 0xed, 0x4f, 0x2d, 0x1f, 0xf6, 0x69);

        // {d9bf8d6a-9530-4b7c-9ddf-ff6fd58bbd06}   MF_MT_AUDIO_VALID_BITS_PER_SAMPLE   {UINT32}
        public static readonly Guid MF_MT_AUDIO_VALID_BITS_PER_SAMPLE = new Guid(0xd9bf8d6a, 0x9530, 0x4b7c, 0x9d, 0xdf, 0xff, 0x6f, 0xd5, 0x8b, 0xbd, 0x06);

        // {aab15aac-e13a-4995-9222-501ea15c6877}   MF_MT_AUDIO_SAMPLES_PER_BLOCK       {UINT32}
        public static readonly Guid MF_MT_AUDIO_SAMPLES_PER_BLOCK = new Guid(0xaab15aac, 0xe13a, 0x4995, 0x92, 0x22, 0x50, 0x1e, 0xa1, 0x5c, 0x68, 0x77);

        // {55fb5765-644a-4caf-8479-938983bb1588}`  MF_MT_AUDIO_CHANNEL_MASK            {UINT32}
        public static readonly Guid MF_MT_AUDIO_CHANNEL_MASK = new Guid(0x55fb5765, 0x644a, 0x4caf, 0x84, 0x79, 0x93, 0x89, 0x83, 0xbb, 0x15, 0x88);

        //
        // MF_MT_AUDIO_FOLDDOWN_MATRIX stores folddown structure from multichannel to stereo
        //

        [StructLayout(LayoutKind.Sequential)]
        public struct MFFOLDDOWN_MATRIX
        {
            public uint cbSize;
            public uint cSrcChannels; // number of source channels
            public uint cDstChannels; // number of destination channels
            public uint dwChannelMask; // mask
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public int[] Coeff;
        }
        
        // {9d62927c-36be-4cf2-b5c4-a3926e3e8711}`  MF_MT_AUDIO_FOLDDOWN_MATRIX         {BLOB, MFFOLDDOWN_MATRIX}
           public static readonly Guid MF_MT_AUDIO_FOLDDOWN_MATRIX = new Guid(0x9d62927c, 0x36be, 0x4cf2, 0xb5, 0xc4, 0xa3, 0x92, 0x6e, 0x3e, 0x87, 0x11);

        // {0x9d62927d-36be-4cf2-b5c4-a3926e3e8711}`  MF_MT_AUDIO_WMADRC_PEAKREF         {UINT32}
        public static readonly Guid MF_MT_AUDIO_WMADRC_PEAKREF = new Guid(0x9d62927d, 0x36be, 0x4cf2, 0xb5, 0xc4, 0xa3, 0x92, 0x6e, 0x3e, 0x87, 0x11);

        // {0x9d62927e-36be-4cf2-b5c4-a3926e3e8711}`  MF_MT_AUDIO_WMADRC_PEAKTARGET        {UINT32}
        public static readonly Guid MF_MT_AUDIO_WMADRC_PEAKTARGET = new Guid(0x9d62927e, 0x36be, 0x4cf2, 0xb5, 0xc4, 0xa3, 0x92, 0x6e, 0x3e, 0x87, 0x11);


        // {0x9d62927f-36be-4cf2-b5c4-a3926e3e8711}`  MF_MT_AUDIO_WMADRC_AVGREF         {UINT32}
        public static readonly Guid MF_MT_AUDIO_WMADRC_AVGREF = new Guid(0x9d62927f, 0x36be, 0x4cf2, 0xb5, 0xc4, 0xa3, 0x92, 0x6e, 0x3e, 0x87, 0x11);

        // {0x9d629280-36be-4cf2-b5c4-a3926e3e8711}`  MF_MT_AUDIO_WMADRC_AVGTARGET      {UINT32}
        public static readonly Guid MF_MT_AUDIO_WMADRC_AVGTARGET = new Guid(0x9d629280, 0x36be, 0x4cf2, 0xb5, 0xc4, 0xa3, 0x92, 0x6e, 0x3e, 0x87, 0x11);

        //
        // MF_MT_AUDIO_PREFER_WAVEFORMATEX tells the converter to prefer a plain WAVEFORMATEX rather than
        // a WAVEFORMATEXTENSIBLE when converting to a legacy type. It is set by the WAVEFORMATEX->IMFMediaType
        // conversion routines when the original format block is a non-extensible WAVEFORMATEX.
        //
        // This preference can be overridden and does not guarantee that the type can be correctly expressed
        // by a non-extensible type.
        //
        // {a901aaba-e037-458a-bdf6-545be2074042}   MF_MT_AUDIO_PREFER_WAVEFORMATEX     {UINT32 (BOOL)}
        public static readonly Guid MF_MT_AUDIO_PREFER_WAVEFORMATEX = new Guid(0xa901aaba, 0xe037, 0x458a, 0xbd, 0xf6, 0x54, 0x5b, 0xe2, 0x07, 0x40, 0x42);

        //
        // AUDIO - AAC extra data
        //

        // {BFBABE79-7434-4d1c-94F0-72A3B9E17188} MF_MT_AAC_PAYLOAD_TYPE       {UINT32}
        public static readonly Guid MF_MT_AAC_PAYLOAD_TYPE = new Guid(0xbfbabe79, 0x7434, 0x4d1c, 0x94, 0xf0, 0x72, 0xa3, 0xb9, 0xe1, 0x71, 0x88);

        // {7632F0E6-9538-4d61-ACDA-EA29C8C14456} MF_MT_AAC_AUDIO_PROFILE_LEVEL_INDICATION       {UINT32}
        public static readonly Guid MF_MT_AAC_AUDIO_PROFILE_LEVEL_INDICATION = new Guid(0x7632f0e6, 0x9538, 0x4d61, 0xac, 0xda, 0xea, 0x29, 0xc8, 0xc1, 0x44, 0x56);

        //
        // AUDIO - FLAC extra data
        //

        // {8B81ADAE-4B5A-4D40-8022-F38D09CA3C5C} MF_MT_AUDIO_FLAC_MAX_BLOCK_SIZE       {UINT32}
        public static readonly Guid MF_MT_AUDIO_FLAC_MAX_BLOCK_SIZE = new Guid(0x8b81adae, 0x4b5a, 0x4d40, 0x80, 0x22, 0xf3, 0x8d, 0x9, 0xca, 0x3c, 0x5c);

        //
        // AUDIO - Spatial Audio Sample extra data
        //

        // {DCFBA24A-2609-4240-A721-3FAEA76A4DF9} MF_MT_SPATIAL_AUDIO_MAX_DYNAMIC_OBJECTS     {UINT32}
        public static readonly Guid MF_MT_SPATIAL_AUDIO_MAX_DYNAMIC_OBJECTS = new Guid(0xdcfba24a, 0x2609, 0x4240, 0xa7, 0x21, 0x3f, 0xae, 0xa7, 0x6a, 0x4d, 0xf9);

        // {2AB71BC0-6223-4BA7-AD64-7B94B47AE792} MF_MT_SPATIAL_AUDIO_OBJECT_METADATA_FORMAT_ID     {GUID}
        public static readonly Guid MF_MT_SPATIAL_AUDIO_OBJECT_METADATA_FORMAT_ID = new Guid(0x2ab71bc0, 0x6223, 0x4ba7, 0xad, 0x64, 0x7b, 0x94, 0xb4, 0x7a, 0xe7, 0x92);

        // {094BA8BE-D723-489F-92FA-766777B34726} MF_MT_SPATIAL_AUDIO_OBJECT_METADATA_LENGTH  {UINT32}
        public static readonly Guid MF_MT_SPATIAL_AUDIO_OBJECT_METADATA_LENGTH = new Guid(0x94ba8be, 0xd723, 0x489f, 0x92, 0xfa, 0x76, 0x67, 0x77, 0xb3, 0x47, 0x26);

        // {11AA80B4-E0DA-47C6-8060-96C1259AE50D} MF_MT_SPATIAL_AUDIO_MAX_METADATA_ITEMS {UINT32}
        public static readonly Guid MF_MT_SPATIAL_AUDIO_MAX_METADATA_ITEMS = new Guid(0x11aa80b4, 0xe0da, 0x47c6, 0x80, 0x60, 0x96, 0xc1, 0x25, 0x9a, 0xe5, 0xd);

        // {83E96EC9-1184-417E-8254-9F269158FC06} MF_MT_SPATIAL_AUDIO_MIN_METADATA_ITEM_OFFSET_SPACING {UINT32}
        public static readonly Guid MF_MT_SPATIAL_AUDIO_MIN_METADATA_ITEM_OFFSET_SPACING = new Guid(0x83e96ec9, 0x1184, 0x417e, 0x82, 0x54, 0x9f, 0x26, 0x91, 0x58, 0xfc, 0x6);

        // {6842F6E7-D43E-4EBB-9C9C-C96F41784863} MF_MT_SPATIAL_AUDIO_DATA_PRESENT {UINT32 (BOOL)}
        public static readonly Guid MF_MT_SPATIAL_AUDIO_DATA_PRESENT = new Guid(0x6842f6e7, 0xd43e, 0x4ebb, 0x9c, 0x9c, 0xc9, 0x6f, 0x41, 0x78, 0x48, 0x63);



        public static readonly Guid MF_MT_ALL_SAMPLES_INDEPENDENT = new Guid(0xc9173739, 0x5e56, 0x461c, 0xb7, 0x13, 0x46, 0xfb, 0x99, 0x5c, 0xb9, 0x5f);
        public static readonly Guid MF_MT_FIXED_SIZE_SAMPLES = new Guid(0xb8ebefaf, 0xb718, 0x4e04, 0xb0, 0xa9, 0x11, 0x67, 0x75, 0xe3, 0x32, 0x1b);
        public static readonly Guid MF_MT_COMPRESSED = new Guid(0x3afd0cee, 0x18f2, 0x4ba5, 0xa1, 0x10, 0x8b, 0xea, 0x50, 0x2e, 0x1f, 0x92);

        public static readonly Guid MFMediaType_Default = new Guid(0x81A412E6, 0x8103, 0x4B06, 0x85, 0x7F, 0x18, 0x62, 0x78, 0x10, 0x24, 0xAC);
        public static readonly Guid MFMediaType_Audio = new Guid(0x73647561, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xAA, 0x00, 0x38, 0x9B, 0x71);
        public static readonly Guid MFMediaType_Video = new Guid(0x73646976, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xAA, 0x00, 0x38, 0x9B, 0x71);
        public static readonly Guid MFMediaType_Protected = new Guid(0x7b4b6fe6, 0x9d04, 0x4494, 0xbe, 0x14, 0x7e, 0x0b, 0xd0, 0x76, 0xc8, 0xe4);
        public static readonly Guid MFMediaType_SAMI = new Guid(0xe69669a0, 0x3dcd, 0x40cb, 0x9e, 0x2e, 0x37, 0x08, 0x38, 0x7c, 0x06, 0x16);
        public static readonly Guid MFMediaType_Script = new Guid(0x72178C22, 0xE45B, 0x11D5, 0xBC, 0x2A, 0x00, 0xB0, 0xD0, 0xF3, 0xF4, 0xAB);
        public static readonly Guid MFMediaType_Image = new Guid(0x72178C23, 0xE45B, 0x11D5, 0xBC, 0x2A, 0x00, 0xB0, 0xD0, 0xF3, 0xF4, 0xAB);
        public static readonly Guid MFMediaType_HTML = new Guid(0x72178C24, 0xE45B, 0x11D5, 0xBC, 0x2A, 0x00, 0xB0, 0xD0, 0xF3, 0xF4, 0xAB);
        public static readonly Guid MFMediaType_Binary = new Guid(0x72178C25, 0xE45B, 0x11D5, 0xBC, 0x2A, 0x00, 0xB0, 0xD0, 0xF3, 0xF4, 0xAB);
        public static readonly Guid MFMediaType_FileTransfer = new Guid(0x72178C26, 0xE45B, 0x11D5, 0xBC, 0x2A, 0x00, 0xB0, 0xD0, 0xF3, 0xF4, 0xAB);
        public static readonly Guid MFMediaType_Stream = new Guid(0xe436eb83, 0x524f, 0x11ce, 0x9f, 0x53, 0x00, 0x20, 0xaf, 0x0b, 0xa7, 0x70);
        public static readonly Guid MFMediaType_MultiplexedFrames = new Guid(0x6ea542b0, 0x281f, 0x4231, 0xa4, 0x64, 0xfe, 0x2f, 0x50, 0x22, 0x50, 0x1c);
        public static readonly Guid MFMediaType_Subtitle = new Guid(0xa6d13581, 0xed50, 0x4e65, 0xae, 0x08, 0x26, 0x06, 0x55, 0x76, 0xaa, 0xcc);
        public static readonly Guid MFMediaType_Perception = new Guid(0x597ff6f9, 0x6ea2, 0x4670, 0x85, 0xb4, 0xea, 0x84, 0x7, 0x3f, 0xe9, 0x40);

        public static readonly Guid MFVideoFormat_WMV3 = new Guid("33564D57-0000-0010-8000-00AA00389B71");
        public static readonly Guid MFVideoFormat_MP4V = new Guid("5634504D-0000-0010-8000-00AA00389B71");
        public static readonly Guid MFVideoFormat_RGB32 = new Guid("00000016-0000-0010-8000-00AA00389B71");
        public static readonly Guid MFVideoFormat_H264 = new Guid("34363248-0000-0010-8000-00AA00389B71");
        public static readonly Guid MFVideoFormat_H264_ES = new Guid("3F40F4F0-5622-4FF8-B6D8-A17A584BEE5E");
        public static readonly Guid MFVideoFormat_H265 = new Guid("35363248-0000-0010-8000-00aa00389b71");

        public static readonly Guid MFAudioFormat_AAC = new Guid("00001610-0000-0010-8000-00aa00389b71");
        public static readonly Guid MFAudioFormat_ADTS = new Guid("00001600-0000-0010-8000-00aa00389b71");
        public static readonly Guid MFAudioFormat_Dolby_AC3_SPDIF = new Guid("00000092-0000-0010-8000-00aa00389b71");
        public static readonly Guid MFAudioFormat_DRM = new Guid("00000009-0000-0010-8000-00aa00389b71");
        public static readonly Guid MFAudioFormat_DTS = new Guid("00000008-0000-0010-8000-00aa00389b71");
        public static readonly Guid MFAudioFormat_Float = new Guid("00000003-0000-0010-8000-00aa00389b71");
        public static readonly Guid MFAudioFormat_MP3 = new Guid("00000055-0000-0010-8000-00aa00389b71");
        public static readonly Guid MFAudioFormat_MPEG = new Guid("00000050-0000-0010-8000-00aa00389b71");
        public static readonly Guid MFAudioFormat_MSP1 = new Guid("0000000a-0000-0010-8000-00aa00389b71");
        public static readonly Guid MFAudioFormat_PCM = new Guid("00000001-0000-0010-8000-00aa00389b71");
        public static readonly Guid MFAudioFormat_WMASPDIF = new Guid("00000164-0000-0010-8000-00aa00389b71");
        public static readonly Guid MFAudioFormat_WMAudio_Lossless = new Guid("00000163-0000-0010-8000-00aa00389b71");
        public static readonly Guid MFAudioFormat_WMAudioV8 = new Guid("00000161-0000-0010-8000-00aa00389b71");
        public static readonly Guid MFAudioFormat_WMAudioV9 = new Guid("00000162-0000-0010-8000-00aa00389b71");

        public static HRESULT CopyAttribute(IMFAttributes pSrc, IMFAttributes pDest, Guid key)
        {
            PROPVARIANT var = new PROPVARIANT();
            HRESULT hr = pSrc.GetItem(ref key, ref var);
            if (hr == HRESULT.S_OK)
            {
                hr = pDest.SetItem(ref key, ref var);
            }
            return hr;
        }

        public static HRESULT CloneVideoMediaType(IMFMediaType pSrcMediaType, Guid guidSubType, out IMFMediaType ppNewMediaType)
        {
            IMFMediaType pNewMediaType = null;
            HRESULT hr = MFCreateMediaType(out pNewMediaType);
            if (hr == HRESULT.S_OK)
            {
                hr = pNewMediaType.SetGUID(MF_MT_MAJOR_TYPE, MFMediaType_Video);
                hr = pNewMediaType.SetGUID(MF_MT_SUBTYPE, guidSubType);
                hr = CopyAttribute(pSrcMediaType, pNewMediaType, MF_MT_FRAME_SIZE);
                hr = CopyAttribute(pSrcMediaType, pNewMediaType, MF_MT_FRAME_RATE);
                hr = CopyAttribute(pSrcMediaType, pNewMediaType, MF_MT_PIXEL_ASPECT_RATIO);
                hr = CopyAttribute(pSrcMediaType, pNewMediaType, MF_MT_INTERLACE_MODE);
            }
            ppNewMediaType = pNewMediaType;
            // SafeRelease(ref pNewMediaType);
            return hr;
        }

        public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE = new Guid(0xc60ac5fe, 0x252a, 0x478f, 0xa0, 0xef, 0xbc, 0x8f, 0xa5, 0xf7, 0xca, 0xd3);
        public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_HW_SOURCE = new Guid(0xde7046ba, 0x54d6, 0x4487, 0xa2, 0xa4, 0xec, 0x7c, 0xd, 0x1b, 0xd1, 0x63);
        public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_FRIENDLY_NAME = new Guid(0x60d0e559, 0x52f8, 0x4fa2, 0xbb, 0xce, 0xac, 0xdb, 0x34, 0xa8, 0xec, 0x1);
        public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_MEDIA_TYPE = new Guid(0x56a819ca, 0xc78, 0x4de4, 0xa0, 0xa7, 0x3d, 0xda, 0xba, 0xf, 0x24, 0xd4);
        public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_CATEGORY = new Guid(0x77f0ae69, 0xc3bd, 0x4509, 0x94, 0x1d, 0x46, 0x7e, 0x4d, 0x24, 0x89, 0x9e);
        public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_SYMBOLIC_LINK = new Guid(0x58f0aad8, 0x22bf, 0x4f8a, 0xbb, 0x3d, 0xd2, 0xc4, 0x97, 0x8c, 0x6e, 0x2f);
        public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_SYMBOLIC_LINK = new Guid(0x98d24b5e, 0x5930, 0x4614, 0xb5, 0xa1, 0xf6, 0x0, 0xf9, 0x35, 0x5a, 0x78);
        public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_MAX_BUFFERS = new Guid(0x7dd9b730, 0x4f2d, 0x41d5, 0x8f, 0x95, 0xc, 0xc9, 0xa9, 0x12, 0xba, 0x26);
        public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_ENDPOINT_ID = new Guid(0x30da9258, 0xfeb9, 0x47a7, 0xa4, 0x53, 0x76, 0x3a, 0x7a, 0x8e, 0x1c, 0x5f);
        public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_ROLE = new Guid(0xbc9d118e, 0x8c67, 0x4a18, 0x85, 0xd4, 0x12, 0xd3, 0x0, 0x40, 0x5, 0x52);
        public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_PROVIDER_DEVICE_ID = new Guid(0x36689d42, 0xa06c, 0x40ae, 0x84, 0xcf, 0xf5, 0xa0, 0x34, 0x6, 0x7c, 0xc4);
        public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_XADDRESS = new Guid(0xbca0be52, 0xc327, 0x44c7, 0x9b, 0x7d, 0x7f, 0xa8, 0xd9, 0xb5, 0xbc, 0xda);
        public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_STREAM_URL = new Guid(0x9d7b40d2, 0x3617, 0x4043, 0x93, 0xe3, 0x8d, 0x6d, 0xa9, 0xbb, 0x34, 0x92);
        public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_USERNAME = new Guid(0x5d01add, 0x949f, 0x46eb, 0xbc, 0x8e, 0x8b, 0xd, 0x2b, 0x32, 0xd7, 0x9d);
        public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_PASSWORD = new Guid(0xa0fd7e16, 0x42d9, 0x49df, 0x84, 0xc0, 0xe8, 0x2c, 0x5e, 0xab, 0x88, 0x74);
        public static readonly Guid CLSID_FrameServerNetworkCameraSource = new Guid(0x7a213aa7, 0x866f, 0x414a, 0x8c, 0x1a, 0x27, 0x5c, 0x72, 0x83, 0xa3, 0x95);
        public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_GUID = new Guid(0x14dd9a1c, 0x7cff, 0x41be, 0xb1, 0xb9, 0xba, 0x1a, 0xc6, 0xec, 0xb5, 0x71);
        public static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_GUID = new Guid(0x8ac3587a, 0x4ae7, 0x42d8, 0x99, 0xe0, 0x0a, 0x60, 0x13, 0xee, 0xf9, 0x0f);
        public static readonly Guid MF_DEVICESTREAM_IMAGE_STREAM = new Guid(0xa7ffb865, 0xe7b2, 0x43b0, 0x9f, 0x6f, 0x9a, 0xf2, 0xa0, 0xe5, 0xf, 0xc0);
        public static readonly Guid MF_DEVICESTREAM_INDEPENDENT_IMAGE_STREAM = new Guid(0x3eeec7e, 0xd605, 0x4576, 0x8b, 0x29, 0x65, 0x80, 0xb4, 0x90, 0xd7, 0xd3);
        public static readonly Guid MF_DEVICESTREAM_STREAM_ID = new Guid(0x11bd5120, 0xd124, 0x446b, 0x88, 0xe6, 0x17, 0x6, 0x2, 0x57, 0xff, 0xf9);
        public static readonly Guid MF_DEVICESTREAM_STREAM_CATEGORY = new Guid(0x2939e7b8, 0xa62e, 0x4579, 0xb6, 0x74, 0xd4, 0x7, 0x3d, 0xfa, 0xbb, 0xba);
        public static readonly Guid MF_DEVICESTREAM_FRAMESERVER_SHARED = new Guid(0x1CB378E9, 0xB279, 0x41D4, 0xAF, 0x97, 0x34, 0xA2, 0x43, 0xE6, 0x83, 0x20);
        public static readonly Guid MF_DEVICESTREAM_TRANSFORM_STREAM_ID = new Guid(0xe63937b7, 0xdaaf, 0x4d49, 0x81, 0x5f, 0xd8, 0x26, 0xf8, 0xad, 0x31, 0xe7);
        public static readonly Guid MF_DEVICESTREAM_EXTENSION_PLUGIN_CLSID = new Guid(0x048e6558, 0x60c4, 0x4173, 0xbd, 0x5b, 0x6a, 0x3c, 0xa2, 0x89, 0x6a, 0xee);
        public static readonly Guid MF_DEVICEMFT_EXTENSION_PLUGIN_CLSID = new Guid(0x844dbae, 0x34fa, 0x48a0, 0xa7, 0x83, 0x8e, 0x69, 0x6f, 0xb1, 0xc9, 0xa8);
        public static readonly Guid MF_DEVICESTREAM_EXTENSION_PLUGIN_CONNECTION_POINT = new Guid(0x37f9375c, 0xe664, 0x4ea4, 0xaa, 0xe4, 0xcb, 0x6d, 0x1d, 0xac, 0xa1, 0xf4);
        public static readonly Guid MF_DEVICESTREAM_TAKEPHOTO_TRIGGER = new Guid(0x1d180e34, 0x538c, 0x4fbb, 0xa7, 0x5a, 0x85, 0x9a, 0xf7, 0xd2, 0x61, 0xa6);
        public static readonly Guid MF_DEVICESTREAM_MAX_FRAME_BUFFERS = new Guid(0x1684cebe, 0x3175, 0x4985, 0x88, 0x2c, 0x0e, 0xfd, 0x3e, 0x8a, 0xc1, 0x1e);
        public static readonly Guid MF_DEVICEMFT_CONNECTED_FILTER_KSCONTROL = new Guid(0x6a2c4fa6, 0xd179, 0x41cd, 0x95, 0x23, 0x82, 0x23, 0x71, 0xea, 0x40, 0xe5);
        public static readonly Guid MF_DEVICEMFT_CONNECTED_PIN_KSCONTROL = new Guid(0xe63310f7, 0xb244, 0x4ef8, 0x9a, 0x7d, 0x24, 0xc7, 0x4e, 0x32, 0xeb, 0xd0);
        public static readonly Guid MF_DEVICE_THERMAL_STATE_CHANGED = new Guid(0x70ccd0af, 0xfc9f, 0x4deb, 0xa8, 0x75, 0x9f, 0xec, 0xd1, 0x6c, 0x5b, 0xd4);

        // {D2E7558C-DC1F-403f-9A72-D28BB1EB3B5E}   MF_MT_FRAME_RATE_RANGE_MIN      {UINT64 (HI32(Numerator),LO32(Denominator))}
        public static readonly Guid MF_MT_FRAME_RATE_RANGE_MIN = new Guid(0xd2e7558c, 0xdc1f, 0x403f, 0x9a, 0x72, 0xd2, 0x8b, 0xb1, 0xeb, 0x3b, 0x5e);

        // {E3371D41-B4CF-4a05-BD4E-20B88BB2C4D6}   MF_MT_FRAME_RATE_RANGE_MAX      {UINT64 (HI32(Numerator),LO32(Denominator))}
        public static readonly Guid MF_MT_FRAME_RATE_RANGE_MAX = new Guid(0xe3371d41, 0xb4cf, 0x4a05, 0xbd, 0x4e, 0x20, 0xb8, 0x8b, 0xb2, 0xc4, 0xd6);

        // {9C27891A-ED7A-40e1-88E8-B22727A024EE}   MF_LOW_LATENCY                  {UINT32 (BOOL)}
        // Same GUID as CODECAPI_AVLowLatencyMode  
        public static readonly Guid MF_LOW_LATENCY = new Guid(0x9c27891a, 0xed7a, 0x40e1, 0x88, 0xe8, 0xb2, 0x27, 0x27, 0xa0, 0x24, 0xee);

        // {E3F2E203-D445-4B8C-9211-AE390D3BA017}  {UINT32} Maximum macroblocks per second that can be handled by MFT
        public static readonly Guid MF_VIDEO_MAX_MB_PER_SEC = new Guid(0xe3f2e203, 0xd445, 0x4b8c, 0x92, 0x11, 0xae, 0x39, 0xd, 0x3b, 0xa0, 0x17);

        // {7086E16C-49C5-4201-882A-8538F38CF13A} {UINT32 (BOOL)} Enables(0, default)/disables(1) the DXVA decode status queries in decoders. When disabled decoder won't provide MFSampleExtension_FrameCorruption
        public static readonly Guid MF_DISABLE_FRAME_CORRUPTION_INFO = new Guid(0x7086e16c, 0x49c5, 0x4201, 0x88, 0x2a, 0x85, 0x38, 0xf3, 0x8c, 0xf1, 0x3a);

        public static readonly Guid GUID_ContainerFormatBmp = new Guid(0x0af1d87e, 0xfcfe, 0x4188, 0xbd, 0xeb, 0xa7, 0x90, 0x64, 0x71, 0xcb, 0xe3);
        public static readonly Guid GUID_ContainerFormatIco = new Guid(0xa3a860c4, 0x338f, 0x4c17, 0x91, 0x9a, 0xfb, 0xa4, 0xb5, 0x62, 0x8f, 0x21);
        public static readonly Guid GUID_ContainerFormatGif = new Guid(0x1f8a5601, 0x7d4d, 0x4cbd, 0x9c, 0x82, 0x1b, 0xc8, 0xd4, 0xee, 0xb9, 0xa5);
        public static readonly Guid GUID_ContainerFormatJpeg = new Guid(0x19e4a5aa, 0x5662, 0x4fc5, 0xa0, 0xc0, 0x17, 0x58, 0x02, 0x8e, 0x10, 0x57);
        public static readonly Guid GUID_ContainerFormatPng = new Guid(0x1b7cfaf4, 0x713f, 0x473c, 0xbb, 0xcd, 0x61, 0x37, 0x42, 0x5f, 0xae, 0xaf);
        public static readonly Guid GUID_ContainerFormatTiff = new Guid(0x163bcc30, 0xe2e9, 0x4f0b, 0x96, 0x1d, 0xa3, 0xe9, 0xfd, 0xb7, 0x88, 0xa3);
        public static readonly Guid GUID_ContainerFormatWmp = new Guid(0x57a37caa, 0x367a, 0x4540, 0x91, 0x6b, 0xf1, 0x83, 0xc5, 0x09, 0x3a, 0x4b);


        [DllImport("Mf.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern HRESULT MFEnumDeviceSources(IMFAttributes pAttributes, out IMFActivate[] pppSourceActivate, out uint pcSourceActivate);

        public static HRESULT MFGetAttributeSize(IMFAttributes pAttributes, Guid guidKey, out uint punWidth, out uint punHeight)
        {
            return MFGetAttribute2UINT32asUINT64(pAttributes, ref guidKey, out punWidth, out punHeight);
        }

        public static HRESULT MFGetAttribute2UINT32asUINT64(IMFAttributes pAttributes, ref Guid guidKey, out uint punHigh32, out uint punLow32)
        {
            ulong unPacked = 0;
            HRESULT hr = HRESULT.S_OK;

            punHigh32 = 0;
            punLow32 = 0;
            hr = pAttributes.GetUINT64(ref guidKey, out unPacked);
            if (hr == HRESULT.S_OK)
            {
                Unpack2UINT32AsUINT64(unPacked, out punHigh32, out punLow32);
            }
            return hr;
        }

        public static void Unpack2UINT32AsUINT64(ulong unPacked, out uint punHigh, out uint punLow)
        {
            punHigh = HI32(unPacked);
            punLow = LO32(unPacked);
        }

        public static uint HI32(ulong unPacked)
        {
            return (uint)(unPacked >> 32);
        }

        public static uint LO32(ulong unPacked)
        {
            return (uint)unPacked;
        }

        public static HRESULT MFGetAttributeRatio(IMFAttributes pAttributes, Guid guidKey, out uint punNumerator, out uint punDenominator)
        {
            return MFGetAttribute2UINT32asUINT64(pAttributes, ref guidKey, out punNumerator, out punDenominator);
        }

        [DllImport("Mfreadwrite.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern HRESULT MFCreateSourceReaderFromMediaSource(IMFMediaSource pMediaSource, IMFAttributes pAttributes, out IMFSourceReader ppSourceReader);

        [DllImport("Mf.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern HRESULT MFTranscodeGetAudioOutputAvailableTypes(ref Guid guidSubType, uint dwMFTFlags, IMFAttributes pCodecConfig, out IMFCollection ppAvailableTypes);

        public static readonly Guid MR_POLICY_VOLUME_SERVICE = new Guid(0x1abaa2ac, 0x9d3b, 0x47c6, 0xab, 0x48, 0xc5, 0x95, 0x6, 0xde, 0x78, 0x4d);
        public static readonly Guid MR_CAPTURE_POLICY_VOLUME_SERVICE = new Guid(0x24030acd, 0x107a, 0x4265, 0x97, 0x5c, 0x41, 0x4e, 0x33, 0xe6, 0x5f, 0x2a);

        [DllImport("Mf.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern HRESULT MFGetService(IntPtr punkObject, ref Guid guidService, ref Guid riid, out IntPtr ppvObject);

        public static readonly PROPERTYKEY MFPKEY_COLOR_BRIGHTNESS = new PROPERTYKEY(new Guid(0x174fb0ec, 0x2695, 0x476c, 0x88, 0xaa, 0xd2, 0xb4, 0x1c, 0xe7, 0x5e, 0x67), 0x01);
        public static readonly PROPERTYKEY MFPKEY_COLOR_CONTRAST = new PROPERTYKEY(new Guid(0x174fb0ec, 0x2695, 0x476c, 0x88, 0xaa, 0xd2, 0xb4, 0x1c, 0xe7, 0x5e, 0x67), 0x02);
        public static readonly PROPERTYKEY MFPKEY_COLOR_HUE = new PROPERTYKEY(new Guid(0x174fb0ec, 0x2695, 0x476c, 0x88, 0xaa, 0xd2, 0xb4, 0x1c, 0xe7, 0x5e, 0x67), 0x03);
        public static readonly PROPERTYKEY MFPKEY_COLOR_SATURATION = new PROPERTYKEY(new Guid(0x174fb0ec, 0x2695, 0x476c, 0x88, 0xaa, 0xd2, 0xb4, 0x1c, 0xe7, 0x5e, 0x67), 0x04);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MFVideoNormalizedRect
    {
        public float left;
        public float top;
        public float right;
        public float bottom;
        public MFVideoNormalizedRect(float Left, float Top, float Right, float Bottom)
        {
            left = Left;
            top = Top;
            right = Right;
            bottom = Bottom;
        }
    }

    public enum MF_CAPTURE_ENGINE_DEVICE_TYPE
    {
        MF_CAPTURE_ENGINE_DEVICE_TYPE_AUDIO = 0,
        MF_CAPTURE_ENGINE_DEVICE_TYPE_VIDEO = 0x1
    }

    public enum MF_CAPTURE_ENGINE_SINK_TYPE
    {
        MF_CAPTURE_ENGINE_SINK_TYPE_RECORD = 0,
        MF_CAPTURE_ENGINE_SINK_TYPE_PREVIEW = 0x1,
        MF_CAPTURE_ENGINE_SINK_TYPE_PHOTO = 0x2
    }

    public enum MF_CAPTURE_ENGINE_MEDIA_TYPE : uint
    {
        MF_CAPTURE_ENGINE_PREFERRED_SOURCE_STREAM_FOR_VIDEO_PREVIEW = 0xfffffffa,
        MF_CAPTURE_ENGINE_PREFERRED_SOURCE_STREAM_FOR_VIDEO_RECORD = 0xfffffff9,
        MF_CAPTURE_ENGINE_PREFERRED_SOURCE_STREAM_FOR_PHOTO = 0xfffffff8,
        MF_CAPTURE_ENGINE_PREFERRED_SOURCE_STREAM_FOR_AUDIO = 0xfffffff7,
        MF_CAPTURE_ENGINE_MEDIASOURCE = 0xffffffff
    };

    public enum MF_CAPTURE_ENGINE_STREAM_CATEGORY
    {
        MF_CAPTURE_ENGINE_STREAM_CATEGORY_VIDEO_PREVIEW = 0,
        MF_CAPTURE_ENGINE_STREAM_CATEGORY_VIDEO_CAPTURE = 0x1,
        MF_CAPTURE_ENGINE_STREAM_CATEGORY_PHOTO_INDEPENDENT = 0x2,
        MF_CAPTURE_ENGINE_STREAM_CATEGORY_PHOTO_DEPENDENT = 0x3,
        MF_CAPTURE_ENGINE_STREAM_CATEGORY_AUDIO = 0x4,
        MF_CAPTURE_ENGINE_STREAM_CATEGORY_UNSUPPORTED = 0x5
    }

    public enum MF_CAPTURE_ENGINE_MEDIA_CATEGORY_TYPE
    {
        MF_CAPTURE_ENGINE_MEDIA_CATEGORY_TYPE_OTHER = 0,
        MF_CAPTURE_ENGINE_MEDIA_CATEGORY_TYPE_COMMUNICATIONS = 1,
        MF_CAPTURE_ENGINE_MEDIA_CATEGORY_TYPE_MEDIA = 2,
        MF_CAPTURE_ENGINE_MEDIA_CATEGORY_TYPE_GAMECHAT = 3,
        MF_CAPTURE_ENGINE_MEDIA_CATEGORY_TYPE_SPEECH = 4
    }

    public enum MF_CAPTURE_ENGINE_AUDIO_PROCESSING_MODE
    {
        MF_CAPTURE_ENGINE_AUDIO_PROCESSING_DEFAULT = 0,
        MF_CAPTURE_ENGINE_AUDIO_PROCESSING_RAW = 1
    }

    [ComImport]
    [Guid("aeda51c0-9025-4983-9012-de597b88b089")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFCaptureEngineOnEventCallback
    {
        [PreserveSig]
        HRESULT OnEvent(IMFMediaEvent pEvent);
    }

    [ComImport]
    [Guid("52150b82-ab39-4467-980f-e48bf0822ecd")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFCaptureEngineOnSampleCallback
    {
        [PreserveSig]
        void OnSample(IMFSample pSample);
        //HRESULT OnSample(IMFSample pSample);
    }

    [ComImport]
    [Guid("e37ceed7-340f-4514-9f4d-9c2ae026100b")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFCaptureEngineOnSampleCallback2 : IMFCaptureEngineOnSampleCallback
    {
#region IMFCaptureEngineOnSampleCallback
        [PreserveSig]
        new HRESULT OnSample(IMFSample pSample);
#endregion

        [PreserveSig]
        HRESULT OnSynchronizedEvent(IMFMediaEvent pEvent);
    }

    [ComImport]
    [Guid("72d6135b-35e9-412c-b926-fd5265f2a885")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFCaptureSink
    {
        [PreserveSig]
        HRESULT GetOutputMediaType(uint dwSinkStreamIndex, out IMFMediaType ppMediaType);
        [PreserveSig]
        HRESULT GetService(uint dwSinkStreamIndex, [In, MarshalAs(UnmanagedType.LPStruct)] Guid rguidService, [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppUnknown);
        [PreserveSig]
        HRESULT AddStream(uint dwSourceStreamIndex, IMFMediaType pMediaType, IMFAttributes pAttributes, out uint pdwSinkStreamIndex);
        [PreserveSig]
        HRESULT Prepare();
        [PreserveSig]
        HRESULT RemoveAllStreams();
    }

    [ComImport]
    [Guid("f9e4219e-6197-4b5e-b888-bee310ab2c59")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFCaptureSink2 : IMFCaptureSink
    {
        #region IMFCaptureSink
        [PreserveSig]
        new HRESULT GetOutputMediaType(uint dwSinkStreamIndex, out IMFMediaType ppMediaType);
        [PreserveSig]
        new HRESULT GetService(uint dwSinkStreamIndex, [In, MarshalAs(UnmanagedType.LPStruct)] Guid rguidService, [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppUnknown);
        [PreserveSig]
        new HRESULT AddStream(uint dwSourceStreamIndex, IMFMediaType pMediaType, IMFAttributes pAttributes, out uint pdwSinkStreamIndex);
        [PreserveSig]
        new HRESULT Prepare();
        [PreserveSig]
        new HRESULT RemoveAllStreams();
        #endregion
        [PreserveSig]
        HRESULT SetOutputMediaType(uint dwStreamIndex, IMFMediaType pMediaType, IMFAttributes pEncodingAttributes);
    }

    [ComImport]
    [Guid("3323b55a-f92a-4fe2-8edc-e9bfc0634d77")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFCaptureRecordSink : IMFCaptureSink
    {
        #region IMFCaptureSink
        [PreserveSig]
        new HRESULT GetOutputMediaType(uint dwSinkStreamIndex, out IMFMediaType ppMediaType);
        [PreserveSig]
        new HRESULT GetService(uint dwSinkStreamIndex, [In, MarshalAs(UnmanagedType.LPStruct)] Guid rguidService, [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppUnknown);
        [PreserveSig]
        new HRESULT AddStream(uint dwSourceStreamIndex, IMFMediaType pMediaType, IMFAttributes pAttributes, out uint pdwSinkStreamIndex);
        [PreserveSig]
        new HRESULT Prepare();
        [PreserveSig]
        new HRESULT RemoveAllStreams();
        #endregion

        [PreserveSig]
        HRESULT SetOutputByteStream(IMFByteStream pByteStream, ref Guid guidContainerType);
        [PreserveSig]
        HRESULT SetOutputFileName(string fileName);
        [PreserveSig]
        HRESULT SetSampleCallback(uint dwStreamSinkIndex, IMFCaptureEngineOnSampleCallback pCallback);
        [PreserveSig]
        HRESULT SetCustomSink(IMFMediaSink pMediaSink);
        [PreserveSig]
        HRESULT GetRotation(uint dwStreamIndex, out uint pdwRotationValue);
        [PreserveSig]
        HRESULT SetRotation(uint dwStreamIndex, uint dwRotationValue);
    }

    [ComImport]
    [Guid("77346cfd-5b49-4d73-ace0-5b52a859f2e0")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFCapturePreviewSink : IMFCaptureSink
    {
        #region IMFCaptureSink
        [PreserveSig]
        new HRESULT GetOutputMediaType(uint dwSinkStreamIndex, out IMFMediaType ppMediaType);
        [PreserveSig]
        new HRESULT GetService(uint dwSinkStreamIndex, [In, MarshalAs(UnmanagedType.LPStruct)] Guid rguidService, [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppUnknown);
        [PreserveSig]
        new HRESULT AddStream(uint dwSourceStreamIndex, IMFMediaType pMediaType, IMFAttributes pAttributes, out uint pdwSinkStreamIndex);
        [PreserveSig]
        new HRESULT Prepare();
        [PreserveSig]
        new HRESULT RemoveAllStreams();
        #endregion

        [PreserveSig]
        HRESULT SetRenderHandle(IntPtr handle);
        [PreserveSig]
        HRESULT SetRenderSurface(IntPtr pSurface);
        [PreserveSig]
        HRESULT UpdateVideo(ref MFVideoNormalizedRect pSrc, ref RECT pDst, uint pBorderClr);
        [PreserveSig]
        HRESULT SetSampleCallback(uint dwStreamSinkIndex, IMFCaptureEngineOnSampleCallback pCallback);
        [PreserveSig]
        HRESULT GetMirrorState(out bool pfMirrorState);
        [PreserveSig]
        HRESULT SetMirrorState(bool fMirrorState);
        [PreserveSig]
        HRESULT GetRotation(uint dwStreamIndex, out uint pdwRotationValue);
        [PreserveSig]
        HRESULT SetRotation(uint dwStreamIndex, uint dwRotationValue);
        [PreserveSig]
        HRESULT SetCustomSink(IMFMediaSink pMediaSink);
    }

    [ComImport]
    [Guid("d2d43cc8-48bb-4aa7-95db-10c06977e777")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFCapturePhotoSink : IMFCaptureSink
    {
        #region IMFCaptureSink
        [PreserveSig]
        new HRESULT GetOutputMediaType(uint dwSinkStreamIndex, out IMFMediaType ppMediaType);
        [PreserveSig]
        new HRESULT GetService(uint dwSinkStreamIndex, [In, MarshalAs(UnmanagedType.LPStruct)] Guid rguidService, [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppUnknown);
        [PreserveSig]
        new HRESULT AddStream(uint dwSourceStreamIndex, IMFMediaType pMediaType, IMFAttributes pAttributes, out uint pdwSinkStreamIndex);
        [PreserveSig]
        new HRESULT Prepare();
        [PreserveSig]
        new HRESULT RemoveAllStreams();
        #endregion

        [PreserveSig]
        HRESULT SetOutputFileName(string fileName);
        [PreserveSig]
        HRESULT SetSampleCallback(IMFCaptureEngineOnSampleCallback pCallback);
        [PreserveSig]
        HRESULT SetOutputByteStream(IMFByteStream pByteStream);
    }

    [ComImport]
    [Guid("439a42a8-0d2c-4505-be83-f79b2a05d5c4")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFCaptureSource
    {
        [PreserveSig]
        HRESULT GetCaptureDeviceSource(MF_CAPTURE_ENGINE_DEVICE_TYPE mfCaptureEngineDeviceType, out IMFMediaSource ppMediaSource);
        [PreserveSig]
        HRESULT GetCaptureDeviceActivate(MF_CAPTURE_ENGINE_DEVICE_TYPE mfCaptureEngineDeviceType, out IMFActivate ppActivate);
        [PreserveSig]
        HRESULT GetService([MarshalAs(UnmanagedType.LPStruct)] Guid rguidService, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppUnknown);
        [PreserveSig]
        HRESULT AddEffect(uint dwSourceStreamIndex, [MarshalAs(UnmanagedType.IUnknown)] object pUnknown);
        [PreserveSig]     
        HRESULT RemoveEffect(uint dwSourceStreamIndex, [MarshalAs(UnmanagedType.IUnknown)] object pUnknown);
        [PreserveSig]
        HRESULT RemoveAllEffects(uint dwSourceStreamIndex);
        [PreserveSig]
        HRESULT GetAvailableDeviceMediaType(uint dwSourceStreamIndex, uint dwMediaTypeIndex, out IMFMediaType ppMediaType);
        [PreserveSig]
        HRESULT SetCurrentDeviceMediaType(uint dwSourceStreamIndex, IMFMediaType pMediaType);
        [PreserveSig]
        HRESULT GetCurrentDeviceMediaType(uint dwSourceStreamIndex, out IMFMediaType ppMediaType);
        [PreserveSig]
        HRESULT GetDeviceStreamCount(out uint pdwStreamCount);
        [PreserveSig]
        HRESULT GetDeviceStreamCategory(uint dwSourceStreamIndex, out MF_CAPTURE_ENGINE_STREAM_CATEGORY pStreamCategory);
        [PreserveSig]
        HRESULT GetMirrorState(uint dwStreamIndex, out bool pfMirrorState);
        [PreserveSig]
        HRESULT SetMirrorState(uint dwStreamIndex, bool fMirrorState);
        [PreserveSig]
        HRESULT GetStreamIndexFromFriendlyName(uint uifriendlyName, out uint pdwActualStreamIndex);
    }

    [ComImport]
    [Guid("a6bba433-176b-48b2-b375-53aa03473207")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFCaptureEngine
    {
        [PreserveSig]
        HRESULT Initialize(IMFCaptureEngineOnEventCallback pEventCallback, IMFAttributes pAttributes, IntPtr pAudioSource, IntPtr pVideoSource);
        [PreserveSig]
        HRESULT StartPreview();
        [PreserveSig]
        HRESULT StopPreview();
        [PreserveSig]
        HRESULT StartRecord();
        [PreserveSig]
        HRESULT StopRecord(bool bFinalize, bool bFlushUnprocessedSamples);
        [PreserveSig]
        HRESULT TakePhoto();
        [PreserveSig]
        HRESULT GetSink(MF_CAPTURE_ENGINE_SINK_TYPE mfCaptureEngineSinkType, out IMFCaptureSink ppSink);
        [PreserveSig]
        HRESULT GetSource(out IMFCaptureSource ppSource);
    }

    [ComImport]
    [Guid("8f02d140-56fc-4302-a705-3a97c78be779")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFCaptureEngineClassFactory
    {
        [PreserveSig]
        HRESULT CreateInstance(ref Guid clsid, ref Guid riid, out IMFCaptureEngine ppvObject);
    }

    [ComImport]
    [Guid("2cd2d921-c447-44a7-a13c-4adabfc247e3")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFAttributes
    {
        [PreserveSig]
        HRESULT GetItem(ref Guid guidKey, ref PROPVARIANT pValue);
        [PreserveSig]
        HRESULT GetItemType(ref Guid guidKey, out MF_ATTRIBUTE_TYPE pType);
        [PreserveSig]
        HRESULT CompareItem(ref Guid guidKey, ref PROPVARIANT Value, out bool pbResult);
        [PreserveSig]
        HRESULT Compare(IMFAttributes pTheirs, MF_ATTRIBUTES_MATCH_TYPE MatchType, out bool pbResult);
        [PreserveSig]
        HRESULT GetUINT32(ref Guid guidKey, out uint punValue);
        [PreserveSig]
        HRESULT GetUINT64(ref Guid guidKey, out ulong punValue);
        [PreserveSig]
        HRESULT GetDouble(ref Guid guidKey, out double pfValue);
        [PreserveSig]
        HRESULT GetGUID(ref Guid guidKey, out Guid pguidValue);
        [PreserveSig]
        HRESULT GetStringLength(ref Guid guidKey, out uint pcchLength);
        [PreserveSig]
        HRESULT GetString(ref Guid guidKey, [Out, MarshalAs(UnmanagedType.LPWStr)] out StringBuilder pwszValue, uint cchBufSize, ref uint pcchLength);
        [PreserveSig]
        HRESULT GetAllocatedString(ref Guid guidKey, [Out, MarshalAs(UnmanagedType.LPWStr)] out StringBuilder ppwszValue, out uint pcchLength);
        [PreserveSig]
        HRESULT GetBlobSize(ref Guid guidKey, out uint pcbBlobSize);
        [PreserveSig]
        HRESULT GetBlob(ref Guid guidKey, out IntPtr pBuf, uint cbBufSize, ref uint pcbBlobSize);
        [PreserveSig]
        HRESULT GetAllocatedBlob(ref Guid guidKey, out IntPtr ppBuf, out uint pcbSize);
        [PreserveSig]
        HRESULT GetUnknown(ref Guid guidKey, ref Guid riid, out IntPtr ppv);
        [PreserveSig]
        HRESULT SetItem(ref Guid guidKey, ref PROPVARIANT Value);
        [PreserveSig]
        HRESULT DeleteItem(ref Guid guidKey);
        [PreserveSig]
        HRESULT DeleteAllItems();
        [PreserveSig]
        HRESULT SetUINT32(ref Guid guidKey, uint unValue);
        [PreserveSig]
        HRESULT SetUINT64(ref Guid guidKey, ulong unValue);
        [PreserveSig]
        HRESULT SetDouble(ref Guid guidKey, double fValue);
        [PreserveSig]
        HRESULT SetGUID(ref Guid guidKey, ref Guid guidValue);
        [PreserveSig]
        HRESULT SetString(ref Guid guidKey, string wszValue);
        [PreserveSig]
        HRESULT SetBlob(ref Guid guidKey, char pBuf, uint cbBufSize);
        [PreserveSig]
        HRESULT SetUnknown(ref Guid guidKey, [MarshalAs(UnmanagedType.IUnknown)] object pUnknown);
        [PreserveSig]
        HRESULT LockStore();
        [PreserveSig]
        HRESULT UnlockStore();
        [PreserveSig]
        HRESULT GetCount(out uint pcItems);
        [PreserveSig]
        HRESULT GetItemByIndex(uint unIndex, out Guid pguidKey, ref PROPVARIANT pValue);
        [PreserveSig]
        HRESULT CopyAllItems(IMFAttributes pDest = null);
    }

    public enum MF_ATTRIBUTE_TYPE
    {
        MF_ATTRIBUTE_UINT32 = VARENUM.VT_UI4,
        MF_ATTRIBUTE_UINT64 = VARENUM.VT_UI8,
        MF_ATTRIBUTE_DOUBLE = VARENUM.VT_R8,
        MF_ATTRIBUTE_GUID = VARENUM.VT_CLSID,
        MF_ATTRIBUTE_STRING = VARENUM.VT_LPWSTR,
        MF_ATTRIBUTE_BLOB = (VARENUM.VT_VECTOR | VARENUM.VT_UI1),
        MF_ATTRIBUTE_IUNKNOWN = VARENUM.VT_UNKNOWN
    }

    public enum MF_ATTRIBUTES_MATCH_TYPE
    {
        MF_ATTRIBUTES_MATCH_OUR_ITEMS = 0,
        MF_ATTRIBUTES_MATCH_THEIR_ITEMS = 1,
        MF_ATTRIBUTES_MATCH_ALL_ITEMS = 2,
        MF_ATTRIBUTES_MATCH_INTERSECTION = 3,
        MF_ATTRIBUTES_MATCH_SMALLER = 4
    }

    [ComImport, Guid("DF598932-F10C-4E39-BBA2-C308F101DAA3"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFMediaEvent : IMFAttributes
    {
        #region IMFAttributes
        [PreserveSig]
        new HRESULT GetItem(ref Guid guidKey, ref PROPVARIANT pValue);
        [PreserveSig]
        new HRESULT GetItemType(ref Guid guidKey, out MF_ATTRIBUTE_TYPE pType);
        [PreserveSig]
        new HRESULT CompareItem(ref Guid guidKey, ref PROPVARIANT Value, out bool pbResult);
        [PreserveSig]
        new HRESULT Compare(IMFAttributes pTheirs, MF_ATTRIBUTES_MATCH_TYPE MatchType, out bool pbResult);
        [PreserveSig]
        new HRESULT GetUINT32(ref Guid guidKey, out uint punValue);
        [PreserveSig]
        new HRESULT GetUINT64(ref Guid guidKey, out ulong punValue);
        [PreserveSig]
        new HRESULT GetDouble(ref Guid guidKey, out double pfValue);
        [PreserveSig]
        new HRESULT GetGUID(ref Guid guidKey, out Guid pguidValue);
        [PreserveSig]
        new HRESULT GetStringLength(ref Guid guidKey, out uint pcchLength);
        [PreserveSig]
        new HRESULT GetString(ref Guid guidKey, [Out, MarshalAs(UnmanagedType.LPWStr)] out StringBuilder pwszValue, uint cchBufSize, ref uint pcchLength);
        [PreserveSig]
        new HRESULT GetAllocatedString(ref Guid guidKey, [Out, MarshalAs(UnmanagedType.LPWStr)] out StringBuilder ppwszValue, out uint pcchLength);
        [PreserveSig]
        new HRESULT GetBlobSize(ref Guid guidKey, out uint pcbBlobSize);
        [PreserveSig]
        new HRESULT GetBlob(ref Guid guidKey, out IntPtr pBuf, uint cbBufSize, ref uint pcbBlobSize);
        [PreserveSig]
        new HRESULT GetAllocatedBlob(ref Guid guidKey, out IntPtr ppBuf, out uint pcbSize);
        [PreserveSig]
        new HRESULT GetUnknown(ref Guid guidKey, ref Guid riid, out IntPtr ppv);
        [PreserveSig]
        new HRESULT SetItem(ref Guid guidKey, ref PROPVARIANT Value);
        [PreserveSig]
        new HRESULT DeleteItem(ref Guid guidKey);
        [PreserveSig]
        new HRESULT DeleteAllItems();
        [PreserveSig]
        new HRESULT SetUINT32(ref Guid guidKey, uint unValue);
        [PreserveSig]
        new HRESULT SetUINT64(ref Guid guidKey, ulong unValue);
        [PreserveSig]
        new HRESULT SetDouble(ref Guid guidKey, double fValue);
        [PreserveSig]
        new HRESULT SetGUID(ref Guid guidKey, ref Guid guidValue);
        [PreserveSig]
        new HRESULT SetString(ref Guid guidKey, string wszValue);
        [PreserveSig]
        new HRESULT SetBlob(ref Guid guidKey, char pBuf, uint cbBufSize);
        [PreserveSig]
        new HRESULT SetUnknown(ref Guid guidKey, [MarshalAs(UnmanagedType.IUnknown)] object pUnknown);
        [PreserveSig]
        new HRESULT LockStore();
        [PreserveSig]
        new HRESULT UnlockStore();
        [PreserveSig]
        new HRESULT GetCount(out uint pcItems);
        [PreserveSig]
        new HRESULT GetItemByIndex(uint unIndex, out Guid pguidKey, ref PROPVARIANT pValue);
        [PreserveSig]
        new HRESULT CopyAllItems(IMFAttributes pDest = null);
        #endregion

        [PreserveSig]
        HRESULT GetType(out MediaEventType pmet);
        [PreserveSig]
        HRESULT GetExtendedType(out Guid pguidExtendedType);
        [PreserveSig]
        HRESULT GetStatus(out HRESULT phrStatus);
        [PreserveSig]
        HRESULT GetValue(out PROPVARIANT pvValue);
    };

    public enum MediaEventType
    {
        MEUnknown = 0,
        MEError = 1,
        MEExtendedType = 2,
        MENonFatalError = 3,
        MEGenericV1Anchor = MENonFatalError,
        MESessionUnknown = 100,
        MESessionTopologySet = 101,
        MESessionTopologiesCleared = 102,
        MESessionStarted = 103,
        MESessionPaused = 104,
        MESessionStopped = 105,
        MESessionClosed = 106,
        MESessionEnded = 107,
        MESessionRateChanged = 108,
        MESessionScrubSampleComplete = 109,
        MESessionCapabilitiesChanged = 110,
        MESessionTopologyStatus = 111,
        MESessionNotifyPresentationTime = 112,
        MENewPresentation = 113,
        MELicenseAcquisitionStart = 114,
        MELicenseAcquisitionCompleted = 115,
        MEIndividualizationStart = 116,
        MEIndividualizationCompleted = 117,
        MEEnablerProgress = 118,
        MEEnablerCompleted = 119,
        MEPolicyError = 120,
        MEPolicyReport = 121,
        MEBufferingStarted = 122,
        MEBufferingStopped = 123,
        MEConnectStart = 124,
        MEConnectEnd = 125,
        MEReconnectStart = 126,
        MEReconnectEnd = 127,
        MERendererEvent = 128,
        MESessionStreamSinkFormatChanged = 129,
        MESessionV1Anchor = MESessionStreamSinkFormatChanged,
        MESourceUnknown = 200,
        MESourceStarted = 201,
        MEStreamStarted = 202,
        MESourceSeeked = 203,
        MEStreamSeeked = 204,
        MENewStream = 205,
        MEUpdatedStream = 206,
        MESourceStopped = 207,
        MEStreamStopped = 208,
        MESourcePaused = 209,
        MEStreamPaused = 210,
        MEEndOfPresentation = 211,
        MEEndOfStream = 212,
        MEMediaSample = 213,
        MEStreamTick = 214,
        MEStreamThinMode = 215,
        MEStreamFormatChanged = 216,
        MESourceRateChanged = 217,
        MEEndOfPresentationSegment = 218,
        MESourceCharacteristicsChanged = 219,
        MESourceRateChangeRequested = 220,
        MESourceMetadataChanged = 221,
        MESequencerSourceTopologyUpdated = 222,
        MESourceV1Anchor = MESequencerSourceTopologyUpdated,
        MESinkUnknown = 300,
        MEStreamSinkStarted = 301,
        MEStreamSinkStopped = 302,
        MEStreamSinkPaused = 303,
        MEStreamSinkRateChanged = 304,
        MEStreamSinkRequestSample = 305,
        MEStreamSinkMarker = 306,
        MEStreamSinkPrerolled = 307,
        MEStreamSinkScrubSampleComplete = 308,
        MEStreamSinkFormatChanged = 309,
        MEStreamSinkDeviceChanged = 310,
        MEQualityNotify = 311,
        MESinkInvalidated = 312,
        MEAudioSessionNameChanged = 313,
        MEAudioSessionVolumeChanged = 314,
        MEAudioSessionDeviceRemoved = 315,
        MEAudioSessionServerShutdown = 316,
        MEAudioSessionGroupingParamChanged = 317,
        MEAudioSessionIconChanged = 318,
        MEAudioSessionFormatChanged = 319,
        MEAudioSessionDisconnected = 320,
        MEAudioSessionExclusiveModeOverride = 321,
        MESinkV1Anchor = MEAudioSessionExclusiveModeOverride,
        MECaptureAudioSessionVolumeChanged = 322,
        MECaptureAudioSessionDeviceRemoved = 323,
        MECaptureAudioSessionFormatChanged = 324,
        MECaptureAudioSessionDisconnected = 325,
        MECaptureAudioSessionExclusiveModeOverride = 326,
        MECaptureAudioSessionServerShutdown = 327,
        MESinkV2Anchor = MECaptureAudioSessionServerShutdown,
        METrustUnknown = 400,
        MEPolicyChanged = 401,
        MEContentProtectionMessage = 402,
        MEPolicySet = 403,
        METrustV1Anchor = MEPolicySet,
        MEWMDRMLicenseBackupCompleted = 500,
        MEWMDRMLicenseBackupProgress = 501,
        MEWMDRMLicenseRestoreCompleted = 502,
        MEWMDRMLicenseRestoreProgress = 503,
        MEWMDRMLicenseAcquisitionCompleted = 506,
        MEWMDRMIndividualizationCompleted = 508,
        MEWMDRMIndividualizationProgress = 513,
        MEWMDRMProximityCompleted = 514,
        MEWMDRMLicenseStoreCleaned = 515,
        MEWMDRMRevocationDownloadCompleted = 516,
        MEWMDRMV1Anchor = MEWMDRMRevocationDownloadCompleted,
        METransformUnknown = 600,
        METransformNeedInput = (METransformUnknown + 1),
        METransformHaveOutput = (METransformNeedInput + 1),
        METransformDrainComplete = (METransformHaveOutput + 1),
        METransformMarker = (METransformDrainComplete + 1),
        METransformInputStreamStateChanged = (METransformMarker + 1),
        MEByteStreamCharacteristicsChanged = 700,
        MEVideoCaptureDeviceRemoved = 800,
        MEVideoCaptureDevicePreempted = 801,
        MEStreamSinkFormatInvalidated = 802,
        MEEncodingParameters = 803,
        MEContentProtectionMetadata = 900,
        MEDeviceThermalStateChanged = 950,
        MEReservedMax = 10000
    }

    [ComImport]
    [Guid("c40a00f2-b93a-4d80-ae8c-5a1c634f58e4")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFSample : IMFAttributes
    {
        #region IMFAttributes
        [PreserveSig]
        new HRESULT GetItem(ref Guid guidKey, ref PROPVARIANT pValue);
        [PreserveSig]
        new HRESULT GetItemType(ref Guid guidKey, out MF_ATTRIBUTE_TYPE pType);
        [PreserveSig]
        new HRESULT CompareItem(ref Guid guidKey, ref PROPVARIANT Value, out bool pbResult);
        [PreserveSig]
        new HRESULT Compare(IMFAttributes pTheirs, MF_ATTRIBUTES_MATCH_TYPE MatchType, out bool pbResult);
        [PreserveSig]
        new HRESULT GetUINT32(ref Guid guidKey, out uint punValue);
        [PreserveSig]
        new HRESULT GetUINT64(ref Guid guidKey, out ulong punValue);
        [PreserveSig]
        new HRESULT GetDouble(ref Guid guidKey, out double pfValue);
        [PreserveSig]
        new HRESULT GetGUID(ref Guid guidKey, out Guid pguidValue);
        [PreserveSig]
        new HRESULT GetStringLength(ref Guid guidKey, out uint pcchLength);
        [PreserveSig]
        new HRESULT GetString(ref Guid guidKey, [Out, MarshalAs(UnmanagedType.LPWStr)] out StringBuilder pwszValue, uint cchBufSize, ref uint pcchLength);
        [PreserveSig]
        new HRESULT GetAllocatedString(ref Guid guidKey, [Out, MarshalAs(UnmanagedType.LPWStr)] out StringBuilder ppwszValue, out uint pcchLength);
        [PreserveSig]
        new HRESULT GetBlobSize(ref Guid guidKey, out uint pcbBlobSize);
        [PreserveSig]
        new HRESULT GetBlob(ref Guid guidKey, out IntPtr pBuf, uint cbBufSize, ref uint pcbBlobSize);
        [PreserveSig]
        new HRESULT GetAllocatedBlob(ref Guid guidKey, out IntPtr ppBuf, out uint pcbSize);
        [PreserveSig]
        new HRESULT GetUnknown(ref Guid guidKey, ref Guid riid, out IntPtr ppv);
        [PreserveSig]
        new HRESULT SetItem(ref Guid guidKey, ref PROPVARIANT Value);
        [PreserveSig]
        new HRESULT DeleteItem(ref Guid guidKey);
        [PreserveSig]
        new HRESULT DeleteAllItems();
        [PreserveSig]
        new HRESULT SetUINT32(ref Guid guidKey, uint unValue);
        [PreserveSig]
        new HRESULT SetUINT64(ref Guid guidKey, ulong unValue);
        [PreserveSig]
        new HRESULT SetDouble(ref Guid guidKey, double fValue);
        [PreserveSig]
        new HRESULT SetGUID(ref Guid guidKey, ref Guid guidValue);
        [PreserveSig]
        new HRESULT SetString(ref Guid guidKey, string wszValue);
        [PreserveSig]
        new HRESULT SetBlob(ref Guid guidKey, char pBuf, uint cbBufSize);
        [PreserveSig]
        new HRESULT SetUnknown(ref Guid guidKey, [MarshalAs(UnmanagedType.IUnknown)] object pUnknown);
        [PreserveSig]
        new HRESULT LockStore();
        [PreserveSig]
        new HRESULT UnlockStore();
        [PreserveSig]
        new HRESULT GetCount(out uint pcItems);
        [PreserveSig]
        new HRESULT GetItemByIndex(uint unIndex, out Guid pguidKey, ref PROPVARIANT pValue);
        [PreserveSig]
        new HRESULT CopyAllItems(IMFAttributes pDest = null);
        #endregion

        [PreserveSig]
        HRESULT GetSampleFlags(out int pdwSampleFlags);
        [PreserveSig]
        HRESULT SetSampleFlags(int dwSampleFlags);
        [PreserveSig]
        HRESULT GetSampleTime(out long phnsSampleTime);
        [PreserveSig]
        HRESULT SetSampleTime(long hnsSampleTime);
        [PreserveSig]
        HRESULT GetSampleDuration(out long phnsSampleDuration);
        HRESULT SetSampleDuration(long hnsSampleDuration);
        [PreserveSig]
        HRESULT GetBufferCount(out int pdwBufferCount);
        [PreserveSig]
        HRESULT GetBufferByIndex(int dwIndex, out IMFMediaBuffer ppBuffer);
        [PreserveSig]
        HRESULT ConvertToContiguousBuffer(out IMFMediaBuffer ppBuffer);
        [PreserveSig]
        HRESULT AddBuffer(IMFMediaBuffer pBuffer);
        [PreserveSig]
        HRESULT RemoveBufferByIndex(int dwIndex);
        [PreserveSig]
        HRESULT RemoveAllBuffers();
        [PreserveSig]
        HRESULT GetTotalLength(out int pcbTotalLength);
        [PreserveSig]
        HRESULT CopyToBuffer(IMFMediaBuffer pBuffer);
    }

    [Guid("045FA593-8799-42b8-BC8D-8968C6453507")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFMediaBuffer
    {
        [PreserveSig]
        HRESULT Lock(out IntPtr ppbBuffer, out int pcbMaxLength, out int pcbCurrentLength);
        [PreserveSig]
        HRESULT Unlock();
        [PreserveSig]
        HRESULT GetCurrentLength(out int pcbCurrentLength);
        [PreserveSig]
        HRESULT SetCurrentLength(int cbCurrentLength);
        [PreserveSig]
        HRESULT GetMaxLength(out int pcbMaxLength);
    }

    [ComImport]
    [Guid("44ae0fa8-ea31-4109-8d2e-4cae4997c555")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFMediaType : IMFAttributes
    {
        #region IMFAttributes
        [PreserveSig]
        new HRESULT GetItem(ref Guid guidKey, ref PROPVARIANT pValue);
        [PreserveSig]
        new HRESULT GetItemType(ref Guid guidKey, out MF_ATTRIBUTE_TYPE pType);
        [PreserveSig]
        new HRESULT CompareItem(ref Guid guidKey, ref PROPVARIANT Value, out bool pbResult);
        [PreserveSig]
        new HRESULT Compare(IMFAttributes pTheirs, MF_ATTRIBUTES_MATCH_TYPE MatchType, out bool pbResult);
        [PreserveSig]
        new HRESULT GetUINT32(ref Guid guidKey, out uint punValue);
        [PreserveSig]
        new HRESULT GetUINT64(ref Guid guidKey, out ulong punValue);
        [PreserveSig]
        new HRESULT GetDouble(ref Guid guidKey, out double pfValue);
        [PreserveSig]
        new HRESULT GetGUID(ref Guid guidKey, out Guid pguidValue);
        [PreserveSig]
        new HRESULT GetStringLength(ref Guid guidKey, out uint pcchLength);
        [PreserveSig]
        new HRESULT GetString(ref Guid guidKey, [Out, MarshalAs(UnmanagedType.LPWStr)] out StringBuilder pwszValue, uint cchBufSize, ref uint pcchLength);
        [PreserveSig]
        new HRESULT GetAllocatedString(ref Guid guidKey, [Out, MarshalAs(UnmanagedType.LPWStr)] out StringBuilder ppwszValue, out uint pcchLength);
        [PreserveSig]
        new HRESULT GetBlobSize(ref Guid guidKey, out uint pcbBlobSize);
        [PreserveSig]
        new HRESULT GetBlob(ref Guid guidKey, out IntPtr pBuf, uint cbBufSize, ref uint pcbBlobSize);
        [PreserveSig]
        new HRESULT GetAllocatedBlob(ref Guid guidKey, out IntPtr ppBuf, out uint pcbSize);
        [PreserveSig]
        new HRESULT GetUnknown(ref Guid guidKey, ref Guid riid, out IntPtr ppv);
        [PreserveSig]
        new HRESULT SetItem(ref Guid guidKey, ref PROPVARIANT Value);
        [PreserveSig]
        new HRESULT DeleteItem(ref Guid guidKey);
        [PreserveSig]
        new HRESULT DeleteAllItems();
        [PreserveSig]
        new HRESULT SetUINT32(ref Guid guidKey, uint unValue);
        [PreserveSig]
        new HRESULT SetUINT64(ref Guid guidKey, ulong unValue);
        [PreserveSig]
        new HRESULT SetDouble(ref Guid guidKey, double fValue);
        [PreserveSig]
        new HRESULT SetGUID(ref Guid guidKey, ref Guid guidValue);
        [PreserveSig]
        new HRESULT SetString(ref Guid guidKey, string wszValue);
        [PreserveSig]
        new HRESULT SetBlob(ref Guid guidKey, char pBuf, uint cbBufSize);
        [PreserveSig]
        new HRESULT SetUnknown(ref Guid guidKey, [MarshalAs(UnmanagedType.IUnknown)] object pUnknown);
        [PreserveSig]
        new HRESULT LockStore();
        [PreserveSig]
        new HRESULT UnlockStore();
        [PreserveSig]
        new HRESULT GetCount(out uint pcItems);
        [PreserveSig]
        new HRESULT GetItemByIndex(uint unIndex, out Guid pguidKey, ref PROPVARIANT pValue);
        [PreserveSig]
        new HRESULT CopyAllItems(IMFAttributes pDest = null);
        #endregion

        [PreserveSig]
        HRESULT GetMajorType(out Guid pguidMajorType);
        [PreserveSig]
        HRESULT IsCompressedFormat(out bool pfCompressed);
        [PreserveSig]
        HRESULT IsEqual(IMFMediaType pIMediaType, out int pdwFlags);
        [PreserveSig]
        HRESULT GetRepresentation(ref Guid guidRepresentation, out IntPtr ppvRepresentation);
        [PreserveSig]
        HRESULT FreeRepresentation(ref Guid guidRepresentation, IntPtr pvRepresentation);
    }

    [ComImport]
    [Guid("ad4c1b00-4bf7-422f-9175-756693d9130d")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFByteStream
    {
        [PreserveSig]
        HRESULT GetCapabilities(out int pdwCapabilities);
        [PreserveSig]
        HRESULT GetLength(out ulong pqwLength);
        [PreserveSig]
        HRESULT SetLength(ulong qwLength);
        [PreserveSig]
        HRESULT GetCurrentPosition(out ulong pqwPosition);
        [PreserveSig]
        HRESULT SetCurrentPosition(ulong qwPosition);
        [PreserveSig]
        HRESULT IsEndOfStream(out bool pfEndOfStream);
        [PreserveSig]
        HRESULT Read(IntPtr pb, uint cb, out uint pcbRead);
        [PreserveSig]
        HRESULT BeginRead(IntPtr pb, uint cb, IMFAsyncCallback pCallback, IntPtr punkState);
        [PreserveSig]
        HRESULT EndRead(IMFAsyncResult pResult, out uint pcbRead);
        [PreserveSig]
        HRESULT Write(IntPtr pb, uint cb, out uint pcbWritten);
        [PreserveSig]
        HRESULT BeginWrite(IntPtr pb, uint cb, IMFAsyncCallback pCallback, IntPtr punkState);
        [PreserveSig]
        HRESULT EndWrite(IMFAsyncResult pResult, out uint pcbWritten);
        [PreserveSig]
        HRESULT Seek(MFBYTESTREAM_SEEK_ORIGIN SeekOrigin, long llSeekOffset, int dwSeekFlags, out ulong pqwCurrentPosition);
        [PreserveSig]
        HRESULT Flush();
        [PreserveSig]
        HRESULT Close();
    }

    public enum MFBYTESTREAM_SEEK_ORIGIN
    {
        msoBegin = 0,
        msoCurrent = (msoBegin + 1)
    }

    [ComImport]
    [Guid("a27003cf-2354-4f2a-8d6a-ab7cff15437e")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFAsyncCallback
    {
        [PreserveSig]
        HRESULT GetParameters(out int pdwFlags, out int pdwQueue);
        [PreserveSig]
        HRESULT Invoke(IMFAsyncResult pAsyncResult);
    }

    [ComImport]
    [Guid("ac6b7889-0740-4d51-8619-905994a55cc6")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFAsyncResult
    {
        [PreserveSig]
        HRESULT GetState(out IntPtr ppunkState);
        [PreserveSig]
        HRESULT GetStatus();
        [PreserveSig]
        HRESULT SetStatus(HRESULT hrStatus);
        [PreserveSig]
        HRESULT GetObject(out IntPtr ppObject);
        //IUnknown* GetStateNoAddRef();
        [PreserveSig]
        IntPtr GetStateNoAddRef();
    }

    [ComImport]
    [Guid("6ef2a660-47c0-4666-b13d-cbb717f2fa2c")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFMediaSink
    {
        [PreserveSig]
        HRESULT GetCharacteristics(out uint pdwCharacteristics);
        [PreserveSig]
        HRESULT AddStreamSink(uint dwStreamSinkIdentifier, IMFMediaType pMediaType, out IMFStreamSink ppStreamSink);
        [PreserveSig]
        HRESULT RemoveStreamSink(uint dwStreamSinkIdentifier);
        [PreserveSig]
        HRESULT GetStreamSinkCount(out uint pcStreamSinkCount);
        [PreserveSig]
        HRESULT GetStreamSinkByIndex(uint dwIndex, out IMFStreamSink ppStreamSink);
        [PreserveSig]
        HRESULT GetStreamSinkById(uint dwStreamSinkIdentifier, out IMFStreamSink ppStreamSink);
        [PreserveSig]
        HRESULT SetPresentationClock(IMFPresentationClock pPresentationClock);
        [PreserveSig]
        HRESULT GetPresentationClock(out IMFPresentationClock ppPresentationClock);
        [PreserveSig]
        HRESULT Shutdown();
    }

    [ComImport, Guid("2CD0BD52-BCD5-4B89-B62C-EADC0C031E7D"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFMediaEventGenerator
    {
        [PreserveSig]
        HRESULT GetEvent(int dwFlags, out IMFMediaEvent ppEvent);
        [PreserveSig]
        HRESULT BeginGetEvent(IMFAsyncCallback pCallback, IntPtr punkState);
        [PreserveSig]
        HRESULT EndGetEvent(IMFAsyncResult pResult, out IMFMediaEvent ppEvent);
        [PreserveSig]
        HRESULT QueueEvent(int met, ref Guid guidExtendedType, HRESULT hrStatus, PROPVARIANT pvValue);
    };

    [ComImport, Guid("0A97B3CF-8E7C-4a3d-8F8C-0C843DC247FB"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFStreamSink : IMFMediaEventGenerator
    {
        #region IMFMediaEventGenerator
        [PreserveSig]
        new HRESULT GetEvent(int dwFlags, out IMFMediaEvent ppEvent);
        [PreserveSig]
        new HRESULT BeginGetEvent(IMFAsyncCallback pCallback, IntPtr punkState);
        [PreserveSig]
        new HRESULT EndGetEvent(IMFAsyncResult pResult, out IMFMediaEvent ppEvent);
        [PreserveSig]
        new HRESULT QueueEvent(int met, ref Guid guidExtendedType, HRESULT hrStatus, PROPVARIANT pvValue);
        #endregion

        [PreserveSig]
        HRESULT GetMediaSink(out IMFMediaSink ppMediaSink);
        [PreserveSig]
        HRESULT GetIdentifier(out uint pdwIdentifier);
        [PreserveSig]
        HRESULT GetMediaTypeHandler(out IMFMediaTypeHandler ppHandler);
        [PreserveSig]
        HRESULT ProcessSample(IMFSample pSample);
        [PreserveSig]
        HRESULT PlaceMarker(MFSTREAMSINK_MARKER_TYPE eMarkerType, PROPVARIANT pvarMarkerValue, PROPVARIANT pvarContextValue);
        [PreserveSig]
        HRESULT Flush();
    }

    [ComImport, Guid("e93dcf6c-4b07-4e1e-8123-aa16ed6eadf5"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFMediaTypeHandler
    {
        [PreserveSig]
        HRESULT IsMediaTypeSupported(IMFMediaType pMediaType, out IMFMediaType ppMediaType);
        [PreserveSig]
        HRESULT GetMediaTypeCount(out int pdwTypeCount);
        [PreserveSig]
        HRESULT GetMediaTypeByIndex(int dwIndex, out IMFMediaType ppType);
        [PreserveSig]
        HRESULT SetCurrentMediaType(IMFMediaType pMediaType);
        [PreserveSig]
        HRESULT GetCurrentMediaType(out IMFMediaType ppMediaType);
        [PreserveSig]
        HRESULT GetMajorType(out Guid pguidMajorType);
    };

    public enum MFSTREAMSINK_MARKER_TYPE
    {
        MFSTREAMSINK_MARKER_DEFAULT = 0,
        MFSTREAMSINK_MARKER_ENDOFSEGMENT = (MFSTREAMSINK_MARKER_DEFAULT + 1),
        MFSTREAMSINK_MARKER_TICK = (MFSTREAMSINK_MARKER_ENDOFSEGMENT + 1),
        MFSTREAMSINK_MARKER_EVENT = (MFSTREAMSINK_MARKER_TICK + 1)
    }

    [ComImport]
    [Guid("2eb1e945-18b8-4139-9b1a-d5d584818530")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFClock
    {
        [PreserveSig]
        HRESULT GetClockCharacteristics(out uint pdwCharacteristics);
        [PreserveSig]
        HRESULT GetCorrelatedTime(uint dwReserved, out long pllClockTime, out long phnsSystemTime);
        [PreserveSig]
        HRESULT GetContinuityKey(out uint pdwContinuityKey);
        [PreserveSig]
        HRESULT GetState(uint dwReserved, out MFCLOCK_STATE peClockState);
        [PreserveSig]
        HRESULT GetProperties(out MFCLOCK_PROPERTIES pClockProperties);
    }

    public enum MFCLOCK_STATE
    {
        MFCLOCK_STATE_INVALID = 0,
        MFCLOCK_STATE_RUNNING = (MFCLOCK_STATE_INVALID + 1),
        MFCLOCK_STATE_STOPPED = (MFCLOCK_STATE_RUNNING + 1),
        MFCLOCK_STATE_PAUSED = (MFCLOCK_STATE_STOPPED + 1)
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MFCLOCK_PROPERTIES
    {
        public UInt64 qwCorrelationRate;
        public Guid guidClockId;
        public uint dwClockFlags;
        public UInt64 qwClockFrequency;
        public uint dwClockTolerance;
        public uint dwClockJitter;
    }

    [ComImport]
    [Guid("868CE85C-8EA9-4f55-AB82-B009A910A805")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFPresentationClock : IMFClock
    {
        #region IMFClock
        [PreserveSig]
        new HRESULT GetClockCharacteristics(out uint pdwCharacteristics);
        [PreserveSig]
        new HRESULT GetCorrelatedTime(uint dwReserved, out long pllClockTime, out long phnsSystemTime);
        [PreserveSig]
        new HRESULT GetContinuityKey(out uint pdwContinuityKey);
        [PreserveSig]
        new HRESULT GetState(uint dwReserved, out MFCLOCK_STATE peClockState);
        [PreserveSig]
        new HRESULT GetProperties(out MFCLOCK_PROPERTIES pClockProperties);
        #endregion

        [PreserveSig]
        HRESULT SetTimeSource(IMFPresentationTimeSource pTimeSource);
        [PreserveSig]
        HRESULT GetTimeSource(out IMFPresentationTimeSource ppTimeSource);
        [PreserveSig]
        HRESULT GetTime(out long phnsClockTime);
        [PreserveSig]
        HRESULT AddClockStateSink(IMFClockStateSink pStateSink);
        [PreserveSig]
        HRESULT RemoveClockStateSink(IMFClockStateSink pStateSink);
        [PreserveSig]
        HRESULT Start(long llClockStartOffset);
        [PreserveSig]
        HRESULT Stop();
        [PreserveSig]
        HRESULT Pause();
    }


    [ComImport]
    [Guid("7FF12CCE-F76F-41c2-863B-1666C8E5E139")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFPresentationTimeSource : IMFClock
    {
        #region IMFClock
        [PreserveSig]
        new HRESULT GetClockCharacteristics(out uint pdwCharacteristics);
        [PreserveSig]
        new HRESULT GetCorrelatedTime(uint dwReserved, out long pllClockTime, out long phnsSystemTime);
        [PreserveSig]
        new HRESULT GetContinuityKey(out uint pdwContinuityKey);
        [PreserveSig]
        new HRESULT GetState(uint dwReserved, out MFCLOCK_STATE peClockState);
        [PreserveSig]
        new HRESULT GetProperties(out MFCLOCK_PROPERTIES pClockProperties);
        #endregion

        [PreserveSig]
        HRESULT GetUnderlyingClock(out IMFClock ppClock);
    }

    [ComImport]
    [Guid("F6696E82-74F7-4f3d-A178-8A5E09C3659F")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFClockStateSink
    {
        [PreserveSig]
        HRESULT OnClockStart(long hnsSystemTime, long llClockStartOffset);
        [PreserveSig]
        HRESULT OnClockStop(long hnsSystemTime);
        [PreserveSig]
        HRESULT OnClockPause(long hnsSystemTime);
        [PreserveSig]
        HRESULT OnClockRestart(long hnsSystemTime);
        [PreserveSig]
        HRESULT OnClockSetRate(long hnsSystemTime, float flRate);
    }

    [ComImport, Guid("279a808d-aec7-40c8-9c6b-a6b492c78a66"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFMediaSource : IMFMediaEventGenerator
    {
        #region IMFMediaEventGenerator
        [PreserveSig]
        new HRESULT GetEvent(int dwFlags, out IMFMediaEvent ppEvent);
        [PreserveSig]
        new HRESULT BeginGetEvent(IMFAsyncCallback pCallback, IntPtr punkState);
        [PreserveSig]
        new HRESULT EndGetEvent(IMFAsyncResult pResult, out IMFMediaEvent ppEvent);
        [PreserveSig]
        new HRESULT QueueEvent(int met, ref Guid guidExtendedType, HRESULT hrStatus, PROPVARIANT pvValue);
        #endregion

        [PreserveSig]
        HRESULT GetCharacteristics(out int pdwCharacteristics);
        [PreserveSig]
        HRESULT CreatePresentationDescriptor(out IMFPresentationDescriptor ppPresentationDescriptor);
        [PreserveSig]
        HRESULT Start(IMFPresentationDescriptor pPresentationDescriptor, ref Guid pguidTimeFormat, PROPVARIANT pvarStartPosition);
        [PreserveSig]
        HRESULT Stop();
        [PreserveSig]
        HRESULT Pause();
        [PreserveSig]
        HRESULT Shutdown();
    };

    [ComImport, Guid("03cb2711-24d7-4db6-a17f-f3a7a479a536"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFPresentationDescriptor : IMFAttributes
    {
        #region IMFAttributes
        [PreserveSig]
        new HRESULT GetItem(ref Guid guidKey, ref PROPVARIANT pValue);
        [PreserveSig]
        new HRESULT GetItemType(ref Guid guidKey, out MF_ATTRIBUTE_TYPE pType);
        [PreserveSig]
        new HRESULT CompareItem(ref Guid guidKey, ref PROPVARIANT Value, out bool pbResult);
        [PreserveSig]
        new HRESULT Compare(IMFAttributes pTheirs, MF_ATTRIBUTES_MATCH_TYPE MatchType, out bool pbResult);
        [PreserveSig]
        new HRESULT GetUINT32(ref Guid guidKey, out uint punValue);
        [PreserveSig]
        new HRESULT GetUINT64(ref Guid guidKey, out ulong punValue);
        [PreserveSig]
        new HRESULT GetDouble(ref Guid guidKey, out double pfValue);
        [PreserveSig]
        new HRESULT GetGUID(ref Guid guidKey, out Guid pguidValue);
        [PreserveSig]
        new HRESULT GetStringLength(ref Guid guidKey, out uint pcchLength);
        [PreserveSig]
        new HRESULT GetString(ref Guid guidKey, [Out, MarshalAs(UnmanagedType.LPWStr)] out StringBuilder pwszValue, uint cchBufSize, ref uint pcchLength);
        [PreserveSig]
        new HRESULT GetAllocatedString(ref Guid guidKey, [Out, MarshalAs(UnmanagedType.LPWStr)] out StringBuilder ppwszValue, out uint pcchLength);
        [PreserveSig]
        new HRESULT GetBlobSize(ref Guid guidKey, out uint pcbBlobSize);
        [PreserveSig]
        new HRESULT GetBlob(ref Guid guidKey, out IntPtr pBuf, uint cbBufSize, ref uint pcbBlobSize);
        [PreserveSig]
        new HRESULT GetAllocatedBlob(ref Guid guidKey, out IntPtr ppBuf, out uint pcbSize);
        [PreserveSig]
        new HRESULT GetUnknown(ref Guid guidKey, ref Guid riid, out IntPtr ppv);
        [PreserveSig]
        new HRESULT SetItem(ref Guid guidKey, ref PROPVARIANT Value);
        [PreserveSig]
        new HRESULT DeleteItem(ref Guid guidKey);
        [PreserveSig]
        new HRESULT DeleteAllItems();
        [PreserveSig]
        new HRESULT SetUINT32(ref Guid guidKey, uint unValue);
        [PreserveSig]
        new HRESULT SetUINT64(ref Guid guidKey, ulong unValue);
        [PreserveSig]
        new HRESULT SetDouble(ref Guid guidKey, double fValue);
        [PreserveSig]
        new HRESULT SetGUID(ref Guid guidKey, ref Guid guidValue);
        [PreserveSig]
        new HRESULT SetString(ref Guid guidKey, string wszValue);
        [PreserveSig]
        new HRESULT SetBlob(ref Guid guidKey, char pBuf, uint cbBufSize);
        [PreserveSig]
        new HRESULT SetUnknown(ref Guid guidKey, [MarshalAs(UnmanagedType.IUnknown)] object pUnknown);
        [PreserveSig]
        new HRESULT LockStore();
        [PreserveSig]
        new HRESULT UnlockStore();
        [PreserveSig]
        new HRESULT GetCount(out uint pcItems);
        [PreserveSig]
        new HRESULT GetItemByIndex(uint unIndex, out Guid pguidKey, ref PROPVARIANT pValue);
        [PreserveSig]
        new HRESULT CopyAllItems(IMFAttributes pDest = null);
        #endregion

        [PreserveSig]
        HRESULT GetStreamDescriptorCount(out int pdwDescriptorCount);
        [PreserveSig]
        HRESULT GetStreamDescriptorByIndex(int dwIndex, out bool pfSelected, out IMFStreamDescriptor ppDescriptor);
        [PreserveSig]
        HRESULT SelectStream(int dwDescriptorIndex);
        [PreserveSig]
        HRESULT DeselectStream(int dwDescriptorIndex);
        [PreserveSig]
        HRESULT Clone(out IMFPresentationDescriptor ppPresentationDescriptor);
    };

    [ComImport, Guid("56c03d9c-9dbb-45f5-ab4b-d80f47c05938"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFStreamDescriptor : IMFAttributes
    {
        #region IMFAttributes
        [PreserveSig]
        new HRESULT GetItem(ref Guid guidKey, ref PROPVARIANT pValue);
        [PreserveSig]
        new HRESULT GetItemType(ref Guid guidKey, out MF_ATTRIBUTE_TYPE pType);
        [PreserveSig]
        new HRESULT CompareItem(ref Guid guidKey, ref PROPVARIANT Value, out bool pbResult);
        [PreserveSig]
        new HRESULT Compare(IMFAttributes pTheirs, MF_ATTRIBUTES_MATCH_TYPE MatchType, out bool pbResult);
        [PreserveSig]
        new HRESULT GetUINT32(ref Guid guidKey, out uint punValue);
        [PreserveSig]
        new HRESULT GetUINT64(ref Guid guidKey, out ulong punValue);
        [PreserveSig]
        new HRESULT GetDouble(ref Guid guidKey, out double pfValue);
        [PreserveSig]
        new HRESULT GetGUID(ref Guid guidKey, out Guid pguidValue);
        [PreserveSig]
        new HRESULT GetStringLength(ref Guid guidKey, out uint pcchLength);
        [PreserveSig]
        new HRESULT GetString(ref Guid guidKey, [Out, MarshalAs(UnmanagedType.LPWStr)] out StringBuilder pwszValue, uint cchBufSize, ref uint pcchLength);
        [PreserveSig]
        new HRESULT GetAllocatedString(ref Guid guidKey, [Out, MarshalAs(UnmanagedType.LPWStr)] out StringBuilder ppwszValue, out uint pcchLength);
        [PreserveSig]
        new HRESULT GetBlobSize(ref Guid guidKey, out uint pcbBlobSize);
        [PreserveSig]
        new HRESULT GetBlob(ref Guid guidKey, out IntPtr pBuf, uint cbBufSize, ref uint pcbBlobSize);
        [PreserveSig]
        new HRESULT GetAllocatedBlob(ref Guid guidKey, out IntPtr ppBuf, out uint pcbSize);
        [PreserveSig]
        new HRESULT GetUnknown(ref Guid guidKey, ref Guid riid, out IntPtr ppv);
        [PreserveSig]
        new HRESULT SetItem(ref Guid guidKey, ref PROPVARIANT Value);
        [PreserveSig]
        new HRESULT DeleteItem(ref Guid guidKey);
        [PreserveSig]
        new HRESULT DeleteAllItems();
        [PreserveSig]
        new HRESULT SetUINT32(ref Guid guidKey, uint unValue);
        [PreserveSig]
        new HRESULT SetUINT64(ref Guid guidKey, ulong unValue);
        [PreserveSig]
        new HRESULT SetDouble(ref Guid guidKey, double fValue);
        [PreserveSig]
        new HRESULT SetGUID(ref Guid guidKey, ref Guid guidValue);
        [PreserveSig]
        new HRESULT SetString(ref Guid guidKey, string wszValue);
        [PreserveSig]
        new HRESULT SetBlob(ref Guid guidKey, char pBuf, uint cbBufSize);
        [PreserveSig]
        new HRESULT SetUnknown(ref Guid guidKey, [MarshalAs(UnmanagedType.IUnknown)] object pUnknown);   
        [PreserveSig]
        new HRESULT LockStore();
        [PreserveSig]
        new HRESULT UnlockStore();
        [PreserveSig]
        new HRESULT GetCount(out uint pcItems);
        [PreserveSig]
        new HRESULT GetItemByIndex(uint unIndex, out Guid pguidKey, ref PROPVARIANT pValue);
        [PreserveSig]
        new HRESULT CopyAllItems(IMFAttributes pDest = null);
        #endregion

        [PreserveSig]
        HRESULT GetStreamIdentifier(out int pdwStreamIdentifier);
        [PreserveSig]
        HRESULT GetMediaTypeHandler(out IMFMediaTypeHandler ppMediaTypeHandler);
    };

    [ComImport, Guid("7FEE9E9A-4A89-47a6-899C-B6A53A70FB67"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFActivate : IMFAttributes
    {
        #region IMFAttributes
        [PreserveSig]
        new HRESULT GetItem(ref Guid guidKey, ref PROPVARIANT pValue);
        [PreserveSig]
        new HRESULT GetItemType(ref Guid guidKey, out MF_ATTRIBUTE_TYPE pType);
        [PreserveSig]
        new HRESULT CompareItem(ref Guid guidKey, ref PROPVARIANT Value, out bool pbResult);
        [PreserveSig]
        new HRESULT Compare(IMFAttributes pTheirs, MF_ATTRIBUTES_MATCH_TYPE MatchType, out bool pbResult);
        [PreserveSig]
        new HRESULT GetUINT32(ref Guid guidKey, out uint punValue);
        [PreserveSig]
        new HRESULT GetUINT64(ref Guid guidKey, out ulong punValue);
        [PreserveSig]
        new HRESULT GetDouble(ref Guid guidKey, out double pfValue);
        [PreserveSig]
        new HRESULT GetGUID(ref Guid guidKey, out Guid pguidValue);
        [PreserveSig]
        new HRESULT GetStringLength(ref Guid guidKey, out uint pcchLength);
        [PreserveSig]
        new HRESULT GetString(ref Guid guidKey, [Out, MarshalAs(UnmanagedType.LPWStr)] out StringBuilder pwszValue, uint cchBufSize, ref uint pcchLength);
        [PreserveSig]
        new HRESULT GetAllocatedString(ref Guid guidKey, [Out, MarshalAs(UnmanagedType.LPWStr)] out StringBuilder ppwszValue, out uint pcchLength);
        [PreserveSig]
        new HRESULT GetBlobSize(ref Guid guidKey, out uint pcbBlobSize);
        [PreserveSig]
        new HRESULT GetBlob(ref Guid guidKey, out IntPtr pBuf, uint cbBufSize, ref uint pcbBlobSize);
        [PreserveSig]
        new HRESULT GetAllocatedBlob(ref Guid guidKey, out IntPtr ppBuf, out uint pcbSize);
        [PreserveSig]
        new HRESULT GetUnknown(ref Guid guidKey, ref Guid riid, out IntPtr ppv);
        [PreserveSig]
        new HRESULT SetItem(ref Guid guidKey, ref PROPVARIANT Value);
        [PreserveSig]
        new HRESULT DeleteItem(ref Guid guidKey);
        [PreserveSig]
        new HRESULT DeleteAllItems();
        [PreserveSig]
        new HRESULT SetUINT32(ref Guid guidKey, uint unValue);
        [PreserveSig]
        new HRESULT SetUINT64(ref Guid guidKey, ulong unValue);
        [PreserveSig]
        new HRESULT SetDouble(ref Guid guidKey, double fValue);
        [PreserveSig]
        new HRESULT SetGUID(ref Guid guidKey, ref Guid guidValue);
        [PreserveSig]
        new HRESULT SetString(ref Guid guidKey, string wszValue);
        [PreserveSig]
        new HRESULT SetBlob(ref Guid guidKey, char pBuf, uint cbBufSize);
        [PreserveSig]
        new HRESULT SetUnknown(ref Guid guidKey, [MarshalAs(UnmanagedType.IUnknown)] object pUnknown);
        [PreserveSig]
        new HRESULT LockStore();
        [PreserveSig]
        new HRESULT UnlockStore();
        [PreserveSig]
        new HRESULT GetCount(out uint pcItems);
        [PreserveSig]
        new HRESULT GetItemByIndex(uint unIndex, out Guid pguidKey, ref PROPVARIANT pValue);
        [PreserveSig]
        new HRESULT CopyAllItems(IMFAttributes pDest = null);
        #endregion

        [PreserveSig]
        HRESULT ActivateObject(ref Guid riid, out IntPtr ppv);
        [PreserveSig]
        HRESULT ShutdownObject();
        [PreserveSig]
        HRESULT DetachObject();
    }

    [ComImport]
    [Guid("7DC9D5F9-9ED9-44ec-9BBF-0600BB589FBB")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMF2DBuffer
    {
        [PreserveSig]
        HRESULT Lock2D(out IntPtr ppbScanline0, out uint plPitch);
        [PreserveSig]
        HRESULT Unlock2D();
        [PreserveSig]
        HRESULT GetScanline0AndPitch(out IntPtr pbScanline0, out uint plPitch);
        [PreserveSig]
        HRESULT IsContiguousFormat(out bool pfIsContiguous);
        [PreserveSig]
        HRESULT GetContiguousLength(out uint pcbLength);
        [PreserveSig]
        HRESULT ContiguousCopyTo(out IntPtr pbDestBuffer, uint cbDestBuffer);
        [PreserveSig]
        HRESULT ContiguousCopyFrom(IntPtr pbSrcBuffer, uint cbSrcBuffer);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MFOffset
    {
        short fract;
        short value;
    }
 

    [StructLayout(LayoutKind.Sequential)]
    public struct MFVideoArea
    {
        MFOffset OffsetX;
        MFOffset OffsetY;
        SIZE Area;
    }

    [ComImport]
    [Guid("70ae66f2-c809-4e4f-8915-bdcb406b7993")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFSourceReader
    {
        [PreserveSig]
        HRESULT GetStreamSelection(uint dwStreamIndex, out bool pfSelected);
        [PreserveSig]
        HRESULT SetStreamSelection(uint dwStreamIndex, bool fSelected);
        [PreserveSig]
        HRESULT GetNativeMediaType(uint dwStreamIndex, uint dwMediaTypeIndex, out IMFMediaType ppMediaType);
        [PreserveSig]
        HRESULT GetCurrentMediaType(uint dwStreamIndex, out IMFMediaType ppMediaType);
        [PreserveSig]
        HRESULT SetCurrentMediaType(uint dwStreamIndex, ref uint pdwReserved, IMFMediaType pMediaType);
        [PreserveSig]
        HRESULT SetCurrentPosition(ref Guid guidTimeFormat, ref PROPVARIANT varPosition);
        [PreserveSig]
        HRESULT ReadSample(uint dwStreamIndex, uint dwControlFlags, out uint pdwActualStreamIndex, out uint pdwStreamFlags, out long pllTimestamp, out IMFSample ppSample);
        [PreserveSig]
        HRESULT Flush(uint dwStreamIndex);
        [PreserveSig]
        HRESULT GetServiceForStream(uint dwStreamIndex, ref Guid guidService, ref Guid riid, out IntPtr ppvObject);
        [PreserveSig]
        HRESULT GetPresentationAttribute(uint dwStreamIndex, ref Guid guidAttribute, out PROPVARIANT pvarAttribute);
    }

    public enum MF_SOURCE_READER : uint
    {
        MF_SOURCE_READER_INVALID_STREAM_INDEX = 0xffffffff,
        MF_SOURCE_READER_ALL_STREAMS = 0xfffffffe,
        MF_SOURCE_READER_ANY_STREAM = 0xfffffffe,
        MF_SOURCE_READER_FIRST_AUDIO_STREAM = 0xfffffffd,
        MF_SOURCE_READER_FIRST_VIDEO_STREAM = 0xfffffffc,
        MF_SOURCE_READER_MEDIASOURCE = 0xffffffff
    };

    [ComImport]
    [Guid("7b981cf0-560e-4116-9875-b099895f23d7")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFSourceReaderEx : IMFSourceReader
    {
        #region IMFSourceReader
        [PreserveSig]
        new HRESULT GetStreamSelection(uint dwStreamIndex, out bool pfSelected);
        [PreserveSig]
        new HRESULT SetStreamSelection(uint dwStreamIndex, bool fSelected);
        [PreserveSig]
        new HRESULT GetNativeMediaType(uint dwStreamIndex, uint dwMediaTypeIndex, out IMFMediaType ppMediaType);
        [PreserveSig]
        new HRESULT GetCurrentMediaType(uint dwStreamIndex, out IMFMediaType ppMediaType);
        [PreserveSig]
        new HRESULT SetCurrentMediaType(uint dwStreamIndex, ref uint pdwReserved, IMFMediaType pMediaType);
        [PreserveSig]
        new HRESULT SetCurrentPosition(ref Guid guidTimeFormat, ref PROPVARIANT varPosition);
        [PreserveSig]
        new HRESULT ReadSample(uint dwStreamIndex, uint dwControlFlags, out uint pdwActualStreamIndex, out uint pdwStreamFlags, out long pllTimestamp, out IMFSample ppSample);
        [PreserveSig]
        new HRESULT Flush(uint dwStreamIndex);
        [PreserveSig]
        new HRESULT GetServiceForStream(uint dwStreamIndex, ref Guid guidService, ref Guid riid, out IntPtr ppvObject);
        [PreserveSig]
        new HRESULT GetPresentationAttribute(uint dwStreamIndex, ref Guid guidAttribute, out PROPVARIANT pvarAttribute);
        #endregion

        [PreserveSig]
        HRESULT SetNativeMediaType(uint dwStreamIndex, IMFMediaType pMediaType, out uint pdwStreamFlags);
        [PreserveSig]
        HRESULT AddTransformForStream(uint dwStreamIndex, IntPtr pTransformOrActivate);
        [PreserveSig]
        HRESULT RemoveAllTransformsForStream(uint dwStreamIndex);
        [PreserveSig]
        HRESULT GetTransformForStream(uint dwStreamIndex, uint dwTransformIndex, out Guid pGuidCategory, out IMFTransform ppTransform);
    }

    [ComImport]
    [Guid("5BC8A76B-869A-46a3-9B03-FA218A66AEBE")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFCollection
    {
        [PreserveSig]
        HRESULT GetElementCount(out uint pcElements);
        [PreserveSig]
        HRESULT GetElement(uint dwElementIndex, out IntPtr ppUnkElement);
        [PreserveSig]
        HRESULT AddElement(IntPtr pUnkElement);
        [PreserveSig]
        HRESULT RemoveElement(uint dwElementIndex, out IntPtr ppUnkElement);
        [PreserveSig]
        HRESULT InsertElementAt(uint dwIndex, [MarshalAs(UnmanagedType.IUnknown)] object pUnknown);
        [PreserveSig]
        HRESULT RemoveAllElements();
    }

    public enum MFT_ENUM_FLAG
    {
        MFT_ENUM_FLAG_SYNCMFT = 0x00000001, // Enumerates V1 MFTs. This is default.
        MFT_ENUM_FLAG_ASYNCMFT = 0x00000002, // Enumerates only software async MFTs also known as V2 MFTs
        MFT_ENUM_FLAG_HARDWARE = 0x00000004, // Enumerates V2 hardware async MFTs
        MFT_ENUM_FLAG_FIELDOFUSE = 0x00000008, // Enumerates MFTs that require unlocking
        MFT_ENUM_FLAG_LOCALMFT = 0x00000010, // Enumerates Locally (in-process) registered MFTs
        MFT_ENUM_FLAG_TRANSCODE_ONLY = 0x00000020, // Enumerates decoder MFTs used by transcode only    
        MFT_ENUM_FLAG_SORTANDFILTER = 0x00000040, // Apply system local, do not use and preferred sorting and filtering
        MFT_ENUM_FLAG_SORTANDFILTER_APPROVED_ONLY = 0x000000C0, // Similar to MFT_ENUM_FLAG_SORTANDFILTER, but apply a local policy of: MF_PLUGIN_CONTROL_POLICY_USE_APPROVED_PLUGINS
        MFT_ENUM_FLAG_SORTANDFILTER_WEB_ONLY = 0x00000140, // Similar to MFT_ENUM_FLAG_SORTANDFILTER, but apply a local policy of: MF_PLUGIN_CONTROL_POLICY_USE_WEB_PLUGINS
        MFT_ENUM_FLAG_SORTANDFILTER_WEB_ONLY_EDGEMODE = 0x00000240, // Similar to MFT_ENUM_FLAG_SORTANDFILTER, but apply a local policy of: MF_PLUGIN_CONTROL_POLICY_USE_WEB_PLUGINS_EDGEMODE
        MFT_ENUM_FLAG_UNTRUSTED_STOREMFT = 0x00000400, // Enumerates all untrusted store MFTs downloaded from the store
        MFT_ENUM_FLAG_ALL = 0x0000003F, // Enumerates all MFTs including SW and HW MFTs and applies filtering
    };

    [ComImport]
    [Guid("fa993888-4383-415a-a930-dd472a8cf6f7")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFGetService
    {
        [PreserveSig]
        HRESULT GetService(ref Guid guidService, ref Guid riid, out IntPtr ppvObject);
    }

    [ComImport]
    [Guid("3137f1cd-fe5e-4805-a5d8-fb477448cb3d")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFSinkWriter
    {
        [PreserveSig]
        HRESULT AddStream(IMFMediaType pTargetMediaType, out int pdwStreamIndex);
        [PreserveSig]
        HRESULT SetInputMediaType(int dwStreamIndex, IMFMediaType pInputMediaType, IMFAttributes pEncodingParameters = null);
        [PreserveSig]
        HRESULT BeginWriting();
        [PreserveSig]
        HRESULT WriteSample(int dwStreamIndex, IMFSample pSample);
        [PreserveSig]
        HRESULT SendStreamTick(int dwStreamIndex, long llTimestamp);
        [PreserveSig]
        HRESULT PlaceMarker(int dwStreamIndex, IntPtr pvContext);
        [PreserveSig]
        HRESULT NotifyEndOfSegment(int dwStreamIndex);
        [PreserveSig]
        HRESULT Flush(int dwStreamIndex);
        [PreserveSig]
        HRESULT Finalize();
        [PreserveSig]
        HRESULT GetServiceForStream(int dwStreamIndex, ref Guid guidService, ref Guid riid, out IntPtr ppvObject);
        [PreserveSig]
        HRESULT GetStatistics(int dwStreamIndex, out MF_SINK_WRITER_STATISTICS pStats);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MF_SINK_WRITER_STATISTICS
    {
        int cb;
        long llLastTimestampReceived;
        long llLastTimestampEncoded;
        long llLastTimestampProcessed;
        long llLastStreamTickReceived;
        long llLastSinkSampleRequest;
        ulong qwNumSamplesReceived;
        ulong qwNumSamplesEncoded;
        ulong qwNumSamplesProcessed;
        ulong qwNumStreamTicksReceived;
        int dwByteCountQueued;
        ulong qwByteCountProcessed;
        int dwNumOutstandingSinkSampleRequests;
        int dwAverageSampleRateReceived;
        int dwAverageSampleRateEncoded;
        int dwAverageSampleRateProcessed;
    }

    [ComImport]
    [Guid("588d72ab-5Bc1-496a-8714-b70617141b25")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFSinkWriterEx: IMFSinkWriter
    {
        #region IMFSinkWriter
        [PreserveSig]
        new HRESULT AddStream(IMFMediaType pTargetMediaType, out int pdwStreamIndex);
        [PreserveSig]
        new HRESULT SetInputMediaType(int dwStreamIndex, IMFMediaType pInputMediaType, IMFAttributes pEncodingParameters = null);
        [PreserveSig]
        new HRESULT BeginWriting();
        [PreserveSig]
        new HRESULT WriteSample(int dwStreamIndex, IMFSample pSample);
        [PreserveSig]
        new HRESULT SendStreamTick(int dwStreamIndex, long llTimestamp);
        [PreserveSig]
        new HRESULT PlaceMarker(int dwStreamIndex, IntPtr pvContext);
        [PreserveSig]
        new HRESULT NotifyEndOfSegment(int dwStreamIndex);
        [PreserveSig]
        new HRESULT Flush(int dwStreamIndex);
        [PreserveSig]
        new HRESULT Finalize();
        [PreserveSig]
        new HRESULT GetServiceForStream(int dwStreamIndex, ref Guid guidService, ref Guid riid, out IntPtr ppvObject);
        [PreserveSig]
        new HRESULT GetStatistics(int dwStreamIndex, out MF_SINK_WRITER_STATISTICS pStats);
        #endregion

        [PreserveSig]
        HRESULT GetTransformForStream(uint dwStreamIndex, uint dwTransformIndex, out Guid pGuidCategory, out IMFTransform ppTransform);
    }

    [ComImport]
    [Guid("089EDF13-CF71-4338-8D13-9E569DBDC319")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFSimpleAudioVolume
    {
        [PreserveSig]
        HRESULT SetMasterVolume(float fLevel);
        [PreserveSig]
        HRESULT GetMasterVolume(out float pfLevel);
        [PreserveSig]
        HRESULT SetMute(bool bMute);
        [PreserveSig]
        HRESULT GetMute(out bool pbMute);
    }

    [ComImport]
    [Guid("76B1BBDB-4EC8-4f36-B106-70A9316DF593")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFAudioStreamVolume
    {
        [PreserveSig]
        HRESULT GetChannelCount(out uint pdwCount);
        [PreserveSig]
        HRESULT SetChannelVolume(uint dwIndex, float fLevel);
        [PreserveSig]
        HRESULT GetChannelVolume(uint dwIndex, out float pfLevel);
        [PreserveSig]
        HRESULT SetAllVolumes(uint dwCount, float pfVolumes);
        [PreserveSig]
        HRESULT GetAllVolumes(uint dwCount, out float pfVolumes);
    }

    [ComImport]
    [Guid("bf94c121-5b05-4e6f-8000-ba598961414d")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFTransform
    {
        [PreserveSig]
        HRESULT GetStreamLimits(out uint pdwInputMinimum, out uint pdwInputMaximum, out uint pdwOutputMinimum, out uint pdwOutputMaximum);
        [PreserveSig]
        HRESULT GetStreamCount(out uint pcInputStreams, out uint pcOutputStreams);
        [PreserveSig]
        HRESULT GetStreamIDs(uint dwInputIDArraySize, out uint pdwInputIDs, uint dwOutputIDArraySize, out uint pdwOutputIDs);
        [PreserveSig]
        HRESULT GetInputStreamInfo(uint dwInputStreamID, out MFT_INPUT_STREAM_INFO pStreamInfo);
        [PreserveSig]
        HRESULT GetOutputStreamInfo(uint dwOutputStreamID, out MFT_OUTPUT_STREAM_INFO pStreamInfo);
        [PreserveSig]
        HRESULT GetAttributes(out IMFAttributes pAttributes);
        [PreserveSig]
        HRESULT GetInputStreamAttributes(uint dwInputStreamID, out IMFAttributes pAttributes);
        [PreserveSig]
        HRESULT GetOutputStreamAttributes(uint dwOutputStreamID, out IMFAttributes pAttributes);
        [PreserveSig]
        HRESULT DeleteInputStream(uint dwStreamID);
        [PreserveSig]
        HRESULT AddInputStreams(uint cStreams, uint adwStreamIDs);
        [PreserveSig]
        HRESULT GetInputAvailableType(uint dwInputStreamID, uint dwTypeIndex, out IMFMediaType ppType);
        [PreserveSig]
        HRESULT GetOutputAvailableType(uint dwOutputStreamID, uint dwTypeIndex, out IMFMediaType ppType);
        [PreserveSig]
        HRESULT SetInputType(uint dwInputStreamID, IMFMediaType pType, uint dwFlags);
        [PreserveSig]
        HRESULT SetOutputType(uint dwOutputStreamID, IMFMediaType pType, uint dwFlags);
        [PreserveSig]
        HRESULT GetInputCurrentType(uint dwInputStreamID, out IMFMediaType ppType);
        [PreserveSig]
        HRESULT GetOutputCurrentType(uint dwOutputStreamID, out IMFMediaType ppType);
        [PreserveSig]
        HRESULT GetInputStatus(uint dwInputStreamID, out uint pdwFlags);
        [PreserveSig]
        HRESULT GetOutputStatus(out uint pdwFlags);
        [PreserveSig]
        HRESULT SetOutputBounds(long hnsLowerBound, long hnsUpperBound);
        [PreserveSig]
        HRESULT ProcessEvent(uint dwInputStreamID, IMFMediaEvent pEvent);
        [PreserveSig]
        HRESULT ProcessMessage(MFT_MESSAGE_TYPE eMessage, IntPtr ulParam);
        [PreserveSig]
        HRESULT ProcessInput(uint dwInputStreamID, IMFSample pSample, uint dwFlags);
        [PreserveSig]
        HRESULT ProcessOutput(uint dwFlags, uint cOutputBufferCount, ref MFT_OUTPUT_DATA_BUFFER pOutputSamples, out uint pdwStatus);
    }

    public enum MFT_INPUT_STREAM_INFO_FLAGS
    {
        MFT_INPUT_STREAM_WHOLE_SAMPLES = 0x1,
        MFT_INPUT_STREAM_SINGLE_SAMPLE_PER_BUFFER = 0x2,
        MFT_INPUT_STREAM_FIXED_SAMPLE_SIZE = 0x4,
        MFT_INPUT_STREAM_HOLDS_BUFFERS = 0x8,
        MFT_INPUT_STREAM_DOES_NOT_ADDREF = 0x100,
        MFT_INPUT_STREAM_REMOVABLE = 0x200,
        MFT_INPUT_STREAM_OPTIONAL = 0x400,
        MFT_INPUT_STREAM_PROCESSES_IN_PLACE = 0x800
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct MFT_INPUT_STREAM_INFO
    {
        public long hnsMaxLatency;
        public uint dwFlags;
        public uint cbSize;
        public uint cbMaxLookahead;
        public uint cbAlignment;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MFT_OUTPUT_STREAM_INFO
    {
        public uint dwFlags;
        public uint cbSize;
        public uint cbAlignment;
    }

    public enum MFT_MESSAGE_TYPE
    {
        MFT_MESSAGE_COMMAND_FLUSH = 0,
        MFT_MESSAGE_COMMAND_DRAIN = 0x1,
        MFT_MESSAGE_SET_D3D_MANAGER = 0x2,
        MFT_MESSAGE_DROP_SAMPLES = 0x3,
        MFT_MESSAGE_COMMAND_TICK = 0x4,
        MFT_MESSAGE_NOTIFY_BEGIN_STREAMING = 0x10000000,
        MFT_MESSAGE_NOTIFY_END_STREAMING = 0x10000001,
        MFT_MESSAGE_NOTIFY_END_OF_STREAM = 0x10000002,
        MFT_MESSAGE_NOTIFY_START_OF_STREAM = 0x10000003,
        MFT_MESSAGE_NOTIFY_RELEASE_RESOURCES = 0x10000004,
        MFT_MESSAGE_NOTIFY_REACQUIRE_RESOURCES = 0x10000005,
        MFT_MESSAGE_NOTIFY_EVENT = 0x10000006,
        MFT_MESSAGE_COMMAND_SET_OUTPUT_STREAM_STATE = 0x10000007,
        MFT_MESSAGE_COMMAND_FLUSH_OUTPUT_STREAM = 0x10000008,
        MFT_MESSAGE_COMMAND_MARKER = 0x20000000
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MFT_OUTPUT_DATA_BUFFER
    {
        public uint dwStreamID;
        public IMFSample pSample;
        public uint dwStatus;
        public IMFCollection pEvents;
    }

    [ComImport]
    [Guid("A3F675D5-6119-4f7f-A100-1D8B280F0EFB")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFVideoProcessorControl
    {
        [PreserveSig]
        HRESULT SetBorderColor(ref MFARGB pBorderColor);
        [PreserveSig]
        HRESULT SetSourceRectangle(ref RECT pSrcRect);
        [PreserveSig]
        HRESULT SetDestinationRectangle(ref RECT pDstRect);
        [PreserveSig]
        HRESULT SetMirror(MF_VIDEO_PROCESSOR_MIRROR eMirror);
        [PreserveSig]
        HRESULT SetRotation(MF_VIDEO_PROCESSOR_ROTATION eRotation);
        [PreserveSig]
        HRESULT SetConstrictionSize(Windows.Foundation.Size pConstrictionSize);
    }

    public enum MF_VIDEO_PROCESSOR_MIRROR
    {
        MIRROR_NONE = 0,
        MIRROR_HORIZONTAL = 1,
        MIRROR_VERTICAL = 2
    }

    public enum MF_VIDEO_PROCESSOR_ROTATION
    {
        ROTATION_NONE = 0,
        ROTATION_NORMAL = 1
    }

    [ComImport]
    [Guid("BDE633D3-E1DC-4a7f-A693-BBAE399C4A20")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFVideoProcessorControl2 : IMFVideoProcessorControl
    {
        #region IMFVideoProcessorControl
        [PreserveSig]
        new HRESULT SetBorderColor(ref MFARGB pBorderColor);
        [PreserveSig]
        new HRESULT SetSourceRectangle(ref RECT pSrcRect);
        [PreserveSig]
        new HRESULT SetDestinationRectangle(ref RECT pDstRect);
        [PreserveSig]
        new HRESULT SetMirror(MF_VIDEO_PROCESSOR_MIRROR eMirror);
        [PreserveSig]
        new HRESULT SetRotation(MF_VIDEO_PROCESSOR_ROTATION eRotation);
        [PreserveSig]
        new HRESULT SetConstrictionSize(Windows.Foundation.Size pConstrictionSize);
        #endregion

        [PreserveSig]
        HRESULT SetRotationOverride(MFVideoRotationFormat uiRotation);
        [PreserveSig]
        HRESULT EnableHardwareEffects(bool fEnabled);
        [PreserveSig]
        HRESULT GetSupportedHardwareEffects(out uint puiSupport);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MFARGB
    {
        public byte rgbBlue;
        public byte rgbGreen;
        public byte rgbRed;
        public byte rgbAlpha;
        public MFARGB(byte RgbBlue, byte RgbGreen, byte RgbRed, byte RgbAlpha)
        {
            rgbBlue = RgbBlue;
            rgbGreen = RgbGreen;
            rgbRed = RgbRed;
            rgbAlpha = RgbAlpha;
        }
    }

    [ComImport]
    [Guid("2424B3F2-EB23-40f1-91AA-74BDDEEA0883")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFVideoProcessorControl3 : IMFVideoProcessorControl2
    {
        #region IMFVideoProcessorControl2
        #region IMFVideoProcessorControl
        [PreserveSig]
        new HRESULT SetBorderColor(ref MFARGB pBorderColor);
        [PreserveSig]
        new HRESULT SetSourceRectangle(ref RECT pSrcRect);
        [PreserveSig]
        new HRESULT SetDestinationRectangle(ref RECT pDstRect);
        [PreserveSig]
        new HRESULT SetMirror(MF_VIDEO_PROCESSOR_MIRROR eMirror);
        [PreserveSig]
        new HRESULT SetRotation(MF_VIDEO_PROCESSOR_ROTATION eRotation);
        [PreserveSig]
        new HRESULT SetConstrictionSize(Windows.Foundation.Size pConstrictionSize);
        #endregion

        new HRESULT SetRotationOverride(MFVideoRotationFormat uiRotation);
        [PreserveSig]
        new HRESULT EnableHardwareEffects(bool fEnabled);
        [PreserveSig]
        new HRESULT GetSupportedHardwareEffects(out uint puiSupport);
        #endregion

        [PreserveSig]
        HRESULT GetNaturalOutputType(out IMFMediaType ppType);
        [PreserveSig]
        HRESULT EnableSphericalVideoProcessing(bool fEnable, MFVideoSphericalFormat eFormat, MFVideoSphericalProjectionMode eProjectionMode);
        [PreserveSig]
        HRESULT SetSphericalVideoProperties(float X, float Y, float Z, float W, float fieldOfView);
        [PreserveSig]
        HRESULT SetOutputDevice(IntPtr pOutputDevice);
    }

    public enum MFVideoSphericalFormat
    {
        MFVideoSphericalFormat_Unsupported = 0,
        MFVideoSphericalFormat_Equirectangular = 1,
        MFVideoSphericalFormat_CubeMap = 2,
        MFVideoSphericalFormat_3DMesh = 3
    }

    public enum MFVideoSphericalProjectionMode
    {
        MFVideoSphericalProjectionMode_Spherical = 0,
        MFVideoSphericalProjectionMode_Flat = (MFVideoSphericalProjectionMode_Spherical + 1)
    }

    public enum MFVideoRotationFormat
    {
        MFVideoRotationFormat_0 = 0,
        MFVideoRotationFormat_90 = 90,
        MFVideoRotationFormat_180 = 180,
        MFVideoRotationFormat_270 = 270,
    }

    [ComImport]
    [Guid("6AB0000C-FECE-4d1f-A2AC-A9573530656E")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFVideoProcessor
    {
        [PreserveSig]
        HRESULT GetAvailableVideoProcessorModes(ref uint lpdwNumProcessingModes, out Guid pVideoProcessingModes);
    }

}
