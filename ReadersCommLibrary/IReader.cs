using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadersCommLibrary
{
    public interface IReader
    {
        bool Connect(string IPAddress, int Port);
        bool Disconnect();
        bool EnableDevice();
        bool DisableDevice();
        bool SetDateTime();
        event EventHandler OnConnected;
        event EventHandler OnDisconnected;
        string IPAddress { get; set; }
        int ReaderID { get; set; }
        short DutyCode { get; set; }
        int Port { get; set; }
        bool IsConnected { get; set; }
        List<Poll> DownloadData();
        //Uploading
        bool UploadFPData(EmpFPTemp Empfp, string EmpName);
        bool UploadFaceTempData(EmpFPTemp Empfp, string EmpName);
        bool UploadCardData(string CardNo, int EmpID, string EmpName, byte RdrTpeID);
        //Downloading
        List<EmpFPTemp> GetFPTempFromDevice();
        List<EmpFPTemp> GetFaceTempFromDevice();
        List<EmpCard> GetCardFromDevice(byte RdrID);
        //Delete
        bool DeleteTemplatesFromDevice(int enrollNumber);
        bool DeleteFaceTempFromDevice(int enrollNumber);
        bool DeleteCardFromDevice(string CardNo, int EmpID, string EmpName, byte RdrTpeID);

        bool UploadDataBatch(List<EmpUpload> _EmpUploadData);
        List<string> GetUsersFromDevice();


    }
}
