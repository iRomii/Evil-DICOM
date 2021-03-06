﻿using System.IO;
using EvilDICOM.Core.IO.Writing;
using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Interfaces;
using System.Text;

namespace EvilDICOM.Network.PDUs
{
    public class Reject : IPDU
    {
        public byte Reason { get; set; }
        public RejectSource Source { get; set; }
        public RejectResult Result { get; set; }

        public byte[] Write()
        {
            var written = new byte[0];
            using (var stream = new MemoryStream())
            {
                using (var dw = new DICOMBinaryWriter(stream))
                {
                    dw.Write((byte) PDUType.A_ASSOC_REJECT);
                    dw.WriteNullBytes(1); //Reserved Null byte
                    LengthWriter.WriteBigEndian(dw, 4, 4);
                    dw.WriteNullBytes(1); //Reserved Null byte
                    dw.Write((byte) Source);
                    dw.Write(Reason);
                    written = stream.ToArray();
                }
            }
            return written;
        }

        public PDUType Type
        {
            get { return PDUType.A_ASSOC_REJECT; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("A-ASSOCIATION RJ");
            sb.AppendLine("-----------------------------");
            sb.AppendFormat("Result : {0}\n", Result==RejectResult.REJECTED_TRANSIENT?
                "Transient":"Permanent");
            string source = string.Empty;
            switch (this.Source)
            {
                case RejectSource.DICOM_UL_SERVICE_USER: source = "SCU"; break;
                case RejectSource.DICOM_UL_SERVICE_PROVIDER_ACSE: source = "SCP ACSE"; break;
                case RejectSource.DICOM_UL_SERVICE_PROVIDER_PRESENTATION: source = "SCP Presentation"; break;
            }
            sb.AppendFormat("Source : {0}\n", source);

            string reason = string.Empty;
            if (this.Source == RejectSource.DICOM_UL_SERVICE_USER)
            {
                switch ((int)this.Reason)
                {
                    case 1: reason = "No reason given"; break;
                    case 2: reason = "Application context not supported"; break;
                    case 3: reason = "Calling AE Title Not Recognized"; break;
                    case 7: reason = "Called AE Title Not Recognized"; break;
                    default: reason = "Unknown"; break;
                }
            }
            else if (this.Source == RejectSource.DICOM_UL_SERVICE_PROVIDER_ACSE)
            {
                switch ((int)this.Reason)
                {
                    case 1: reason = "No reason given"; break;
                    case 2: reason = "Protocol version not supported"; break;
                }
            }
            else
            {
                switch ((int)this.Reason)
                {
                    case 0: reason = "Reserved"; break;
                    case 1: reason = "Temporary congestion"; break;
                    case 2: reason = "Local limit exceeded"; break;
                    default: reason = "Unknown"; break;
                }
            }
            sb.AppendFormat("Reason : {0}\n", reason);
            return sb.ToString();
        }
    }
}