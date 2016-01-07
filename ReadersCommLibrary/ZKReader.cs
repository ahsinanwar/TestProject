using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReadersCommLibrary
{
    public class ZKReader : IReader
    {
        #region --Variables and Initilizers --

        public zkemkeeper.CZKEMClass czkem;
        int iMachineNumber = 1;
        string ip;
        string rdrName = "";
        int readerid;
        short dutycode;
        bool isConnected;
        int port;
        public string IPAddress
        {
            get
            {
                return ip;
            }
            set
            {
                ip = value;
            }
        }
        public int ReaderID
        {
            get
            {
                return readerid;
            }
            set
            {
                readerid = value;
            }
        }
        public string ReaderName
        {
            get
            {
                return rdrName;
            }
            set
            {
                rdrName = value;
            }
        }
        public int Port
        {
            get
            {
                return port;
            }
            set
            {
                port = value;
            }
        }
        public short DutyCode
        {
            get
            {
                return dutycode;
            }
            set
            {

                dutycode = value;
            }
        }
        public bool IsConnected
        {
            get
            {
                return isConnected;
            }
            set
            {
                isConnected = value;
            }
        }
        event EventHandler onconnectHandler;
        public event EventHandler OnDisconnected;
        #endregion

        public ZKReader()
        {
            czkem = new zkemkeeper.CZKEMClass();
                 }

        #region Connect/ Disconnect/ Enable / Disable--

        public bool Connect(string IPAddress, int Port)
        {
            bool result = false;
            try
            {
                if (czkem.Connect_Net(IPAddress, Port))
                {
                    IsConnected = true;
                    this.Port = Port;
                    this.IPAddress = IPAddress;
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public bool Connect(string IPAddress, int Port,string _ReaderName)
        {
            bool result = false;
            try
            {
                if (czkem.Connect_Net(IPAddress, Port))
                {
                    IsConnected = true;
                    this.Port = Port;
                    this.IPAddress = IPAddress;
                    result = true;
                    ReaderName = _ReaderName;
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public bool Disconnect()
        {
            bool result = false;
            try
            {
                czkem.Disconnect();
                IsConnected = false;
            }
            catch (Exception)
            {
                result = true ;
            }
            return result;
        }

        public event EventHandler OnConnected
        {
            add { this.onconnectHandler += value; }
            remove { this.onconnectHandler -= value; }
        }
        void RaiseOnConnectedEvent(object sender, EventArgs args)
        {
            if (onconnectHandler != null)
            {
                onconnectHandler(sender, args);
            }
        }

        public bool EnableDevice()
        {
            if (czkem.EnableDevice(iMachineNumber, false))
            {
                return true;
            }
            else
                return false;
        }
        public bool DisableDevice()
        {
            if (czkem.EnableDevice(iMachineNumber, true))
            {
                return true;
            }
            else
                return false;
        }

        #endregion

        public bool SetDateTime()
        {
            try
            {
                if (czkem.SetDeviceTime(iMachineNumber))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public List<Poll> DownloadData()
        {
            //int iMachineNumber = 1;
            string sdwEnrollNumber = "";
            int idwTMachineNumber = 0;
            int idwEMachineNumber = 0;
            int idwVerifyMode = 0;
            int idwInOutMode = 0;
            int idwYear = 0;
            int idwMonth = 0;
            int idwDay = 0;
            int idwHour = 0;
            int idwMinute = 0;
            int idwSecond = 0;
            int idwWorkcode = 0;

            int idwErrorCode = 0;
            int iGLCount = 0;
            int iIndex = 0;
            List<Poll> data = new List<Poll>();
            if (IsConnected)
            {
                czkem.EnableDevice(iMachineNumber, false);//disable the device
                if (czkem.ReadGeneralLogData(iMachineNumber))//read all the attendance records to the memory
                {
                    while (czkem.SSR_GetGeneralLogData(iMachineNumber, out sdwEnrollNumber, out idwVerifyMode,
                               out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                    {
                        try
                        {
                            iGLCount++;
                            //data.Items.Add(iGLCount.ToString());
                            //data.Items[iIndex].SubItems.Add(sdwEnrollNumber);//modify by Darcy on Nov.26 2009
                            //data.Items[iIndex].SubItems.Add(idwVerifyMode.ToString());
                            //data.Items[iIndex].SubItems.Add(idwInOutMode.ToString());
                            //data.Items[iIndex].SubItems.Add(idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString());
                            //data.Items[iIndex].SubItems.Add(idwWorkcode.ToString());
                            DateTime entry = new DateTime(idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond);
                            data.Add(new Poll(Int32.Parse(sdwEnrollNumber), entry));
                            iIndex++;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                else
                {
                    czkem.GetLastError(ref idwErrorCode);

                    if (idwErrorCode != 0)
                    {
                        //MessageBox.Show("Reading data from terminal failed,ErrorCode: " + idwErrorCode.ToString(), "Error");
                    }
                    else
                    {
                        //MessageBox.Show("No data from terminal returns!", "Error");
                    }
                }
                czkem.EnableDevice(iMachineNumber, true);//enable the device
               
            }
            if (data.Count > 0)
            {
                ClearAttDataFromDevice(iMachineNumber);
            }
            return data;
        }
        public void ClearAttDataFromDevice(int _iMachineNum)
        {
            try
            {
                int idwErrorCode = 0;
                czkem.EnableDevice(_iMachineNum, false);//disable the device
                if (czkem.ClearGLog(_iMachineNum))
                {
                    czkem.RefreshData(_iMachineNum);//the data in the device should be refreshed
                }
                else
                {
                    czkem.GetLastError(ref idwErrorCode);
                }
                czkem.EnableDevice(iMachineNumber, true);//enable the device
            }
            catch (Exception ex)
            {
            }
        }

        private bool CheckChar(string sdwEnrollNumber)
        {
            if (string.IsNullOrWhiteSpace(sdwEnrollNumber))
                return false;
            foreach (char c in sdwEnrollNumber)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }

            }
            return true;

        }

        #region --Get Data From Device--
        // Get FingerPrint Templates from Device
        public List<EmpFPTemp> GetFPTempFromDevice()
        {
            List<EmpFPTemp> FPDataList = new List<EmpFPTemp>();
             string sdwEnrollNumber = "";
             string sName = "";
             string sPassword = "";
             int iPrivilege = 0;
             bool bEnabled = false;
            int idwFingerIndex;
             string sTmpData = "";
             int iTmpLength = 0;
             int iFlag = 0;
             string FP1="";
            string FP2="";
            string FP3="";
            string FP4="";
            try
            {
                if (IsConnected)
                {
                    czkem.EnableDevice(iMachineNumber, false);
                    czkem.ReadAllUserID(iMachineNumber);//read all the user information to the memory
                    czkem.ReadAllTemplate(iMachineNumber);//read all the users' fingerprint templates to the memory

                    while (czkem.SSR_GetAllUserInfo(iMachineNumber, out sdwEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))
                    {
                        bool checkFPTemp = false;
                        if (CheckChar(sdwEnrollNumber))
                        {
                           //FPID = Convert.ToInt32(sdwEnrollNumber);
                            //FP.Enabled = bEnabled;
                            //string dc = sName;
                            //string sc = sPassword;
                            //int aa = iPrivilege;

                            for (idwFingerIndex = 0; idwFingerIndex < 4; idwFingerIndex++)
                            {
                                if (czkem.GetUserTmpExStr(iMachineNumber,
                                    sdwEnrollNumber,
                                    idwFingerIndex,
                                    out iFlag,
                                    out sTmpData,
                                    out iTmpLength))
                                {
                                    if (idwFingerIndex == 0)
                                    {
                                        FP1 = sTmpData;
                                        checkFPTemp = true;
                                    }
                                    if (idwFingerIndex == 1)
                                        FP2 = sTmpData;
                                    if (idwFingerIndex == 2)
                                        FP3 = sTmpData;
                                    if (idwFingerIndex == 3)
                                        FP4 = sTmpData;
                                }
                            }
                            //check the user has FP or card 
                            if(checkFPTemp == true)
                            FPDataList.Add(new EmpFPTemp(Convert.ToInt32(sdwEnrollNumber), FP1, FP2, FP3, FP4));
                        }
                    }
                    czkem.RefreshData(iMachineNumber);
                }
                else
                {
                    return new List<EmpFPTemp>();
                }
                return FPDataList;
                }
                
            catch(Exception ex)
            {
                return new List<EmpFPTemp>();
            }
            finally
                {
                    czkem.EnableDevice(iMachineNumber, true);
                }
        }
        // Get Face Templates from device
        public List<EmpFPTemp> GetFaceTempFromDevice()
        {
            List<EmpFPTemp> FPDataList = new List<EmpFPTemp>();
            string sUserID = "";
            string sName = "";
            string sPassword = "";
            int iPrivilege = 0;
            bool bEnabled = false;

            int iFaceIndex = 50;//the only possible parameter value
            string sTmpData = "";
            int iLength = 0;
            try
            {
                if (IsConnected)
                {
                    czkem.EnableDevice(iMachineNumber, false);
                    while (czkem.SSR_GetAllUserInfo(iMachineNumber, out sUserID, out sName, out sPassword, out iPrivilege, out bEnabled))//get all the users' information from the memory
                    {
                        if (czkem.GetUserFaceStr(iMachineNumber, sUserID, iFaceIndex, ref sTmpData, ref iLength))//get the face templates from the memory
                        {
                            FPDataList.Add(new EmpFPTemp(Convert.ToInt32(sUserID), sTmpData, iLength.ToString(), "", ""));
                        }
                    }
                    czkem.RefreshData(iMachineNumber);
                }
                else
                {
                    return new List<EmpFPTemp>();
                }
                return FPDataList;
            }

            catch (Exception ex)
            {
                return new List<EmpFPTemp>();
            }
            finally
            {
                czkem.EnableDevice(iMachineNumber, true);
            }
        }
        //Get Card User from device
        public List<EmpCard> GetCardFromDevice(byte RdrTypeID)
        {
            List<EmpCard> CardDataList = new List<EmpCard>();
            int sdwEnrollNumber = 0;
            string sName = "";
            string sPassword = "";
            int iPrivilege = 0;
            bool bEnabled = false;
            string _cardNo = "";
            try
            {
                if (IsConnected)
                {
                    czkem.EnableDevice(iMachineNumber, false);
                    czkem.ReadAllUserID(iMachineNumber);//read all the user information to the memory
                   switch(RdrTypeID)
                   {
                       case 7://Black and White Readers
                    while (czkem.GetAllUserInfo(iMachineNumber, ref sdwEnrollNumber, ref sName, ref sPassword, ref iPrivilege, ref bEnabled))//get user information from memory
                    {
                        czkem.GetStrCardNumber(out _cardNo);//get the card number from the memory
                        
                            //if (CheckChar(sdwEnrollNumber))
                            //{
                               CardDataList.Add(new EmpCard(Convert.ToInt32(sdwEnrollNumber), _cardNo,sName,iPrivilege,bEnabled));
                            //}
                        
                    }
                    break;
                       case 8://IFace
                    string ssdwEnrollNumber = "";
                    while (czkem.SSR_GetAllUserInfo(iMachineNumber, out ssdwEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))//get user information from memory
                    {
                        czkem.GetStrCardNumber(out _cardNo);//get the card number from the memory
                        
                            //if (CheckChar(sdwEnrollNumber))
                            //{
                            CardDataList.Add(new EmpCard(Convert.ToInt32(ssdwEnrollNumber), _cardNo,sName,iPrivilege,bEnabled));
                            //}
                        
                    }
                    break;
                }
                    czkem.RefreshData(iMachineNumber);
                    //czkem.EnableDevice(iMachineNumber, true);
                }
                else
                {
                    return new List<EmpCard>();
                }
                return CardDataList;
            }

            catch (Exception ex)
            {
                return new List<EmpCard>();
            }
            finally
            {
                czkem.EnableDevice(iMachineNumber, true);
            }
        }
        #endregion

        #region --Delete User Data from Device--
        //Delete FP Templates One by One
        public bool DeleteTemplatesFromDevice(int enrollNumber)
        {
            try
            {
                int idwErrorCode = 0;
                if (czkem.SSR_DeleteEnrollData(iMachineNumber, enrollNumber.ToString(), 12))
                {
                    czkem.RefreshData(iMachineNumber);//the data in the device should be refreshed
                    //ErrorList.Add("Deleted Template ,EmpNO:" + enrollNumber);
                    return true;
                }
                
                else
                {
                    czkem.GetLastError(ref idwErrorCode);
                    //ErrorList.Add("Operation failed,ErrorCode=" + idwErrorCode);
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                //czkem.EnableDevice(iMachineNumber, true);
                //Disconnect();
            }
        }
        //Delete Face Template one by one
        public bool DeleteFaceTempFromDevice(int enrollNumber)
        {
            try
            {
                int idwErrorCode = 0;
                //index will be 50 only
                if (czkem.DelUserFace(iMachineNumber, enrollNumber.ToString(), 50))
                {
                    czkem.RefreshData(iMachineNumber);//the data in the device should be refreshed
                    //ErrorList.Add("Deleted Template ,EmpNO:" + enrollNumber);
                    return true;
                }

                else
                {
                    czkem.GetLastError(ref idwErrorCode);
                    //ErrorList.Add("Operation failed,ErrorCode=" + idwErrorCode);
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }
        //Delete Card one by one
        public bool DeleteCardFromDevice(string CardNo, int EmpID, string EmpName, byte RdrTpeID)
        {
            bool check = false;
            int idwErrorCode = 0;
            int sdwEnrollNumber = EmpID;
            string sName = EmpName;
            string sPassword = "";
            int iPrivilege = 0;
            string sCardnumber = CardNo;
            //czkem.EnableDevice(iMachineNumber, false);
            czkem.SetStrCardNumber(sCardnumber);//Before you using function SetUserInfo,set the card number to make sure you can upload it to the device
            switch (RdrTpeID)
            {
                case 7://Black and White
                    if (czkem.SetUserInfo(iMachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, false))//upload the user's information(card number included)
                    {
                        check = true;
                    }
                    else
                    {
                        czkem.GetLastError(ref idwErrorCode);
                    }
                    if (czkem.DeleteEnrollData(iMachineNumber, EmpID, 0, 12))
                    {

                    }
                    break;
                case 8://IFace
                    if (czkem.SSR_SetUserInfo(iMachineNumber, sdwEnrollNumber.ToString(), sName, sPassword, iPrivilege, false))//upload the user's information(card number included)
                    {
                        check = true;
                    }
                    else
                    {
                        czkem.GetLastError(ref idwErrorCode);
                    }
                    if (czkem.SSR_DeleteEnrollData(iMachineNumber, EmpID.ToString(), 0))
                    {

                    }
                    break;
            }
            czkem.RefreshData(iMachineNumber);//the data in the device should be refreshed
            //czkem.EnableDevice(iMachineNumber, true);
            return check;
        }
        #endregion

        #region --Upload User to device one by one--
        //Upload FP Templates one by one
        public bool UploadFPData(EmpFPTemp empfp, string EmpName)
        {
            try
            {
                string sdwEnrollNumber = "";
                string sName = "";
                string sPassword = "";
                int iPrivilege = 0;
                bool bEnabled = true;
                int idwFingerIndex = 0;
                string temstr = "";
                if (empfp.FP2 != null)
                { idwFingerIndex = 1; }
                if (empfp.FP3 != null)
                { idwFingerIndex = 2; }
                if (empfp.FP4 != null)
                { idwFingerIndex = 3; }
                if (IsConnected)
                {
                    sdwEnrollNumber = empfp.FPID.ToString();
                    sName = EmpName;
                    if (czkem.SSR_SetUserInfo(iMachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled))
                    {
                        for (int i = 0; i <= idwFingerIndex; i++)
                        {
                            if (idwFingerIndex == 0)
                            { temstr = empfp.FP1; }
                            if (idwFingerIndex == 1)
                            { temstr = empfp.FP2; }
                            if (idwFingerIndex == 2)
                            { temstr = empfp.FP3; }
                            if (idwFingerIndex == 3)
                            { temstr = empfp.FP4; }
                            try
                            { czkem.SSR_SetUserTmpStr(iMachineNumber, sdwEnrollNumber, 0, temstr); }
                            catch
                            { return false; }
                        }
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        // Upload FaceTemplate one by one
        public bool UploadFaceTempData(EmpFPTemp empfp, string EmpName)
        {
            try
            {
                string sdwEnrollNumber = "";
                string sName = "";
                string sPassword = "";
                int iPrivilege = 0;
                bool bEnabled = true;
                if (IsConnected)
                {
                    sdwEnrollNumber = empfp.FPID.ToString();
                    sName = EmpName;
                    if (czkem.SSR_SetUserInfo(iMachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled))//face templates are part of users' information
                    {
                        czkem.SetUserFaceStr(iMachineNumber, sdwEnrollNumber, 50, empfp.FP1, Convert.ToInt32(empfp.FP2));//upload face templates information to the device
                    }
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        // Upload Card one by one
        public bool UploadCardData(string CardNo, int EmpID, string EmpName, byte RdrTpeID)
        {
            bool check = false;
            int idwErrorCode = 0;
            int sdwEnrollNumber = EmpID;
            string sName = EmpName;
            string sPassword = "";
            int iPrivilege = 0;
            string sCardnumber = CardNo;
            czkem.EnableDevice(iMachineNumber, false);
            czkem.SetStrCardNumber(sCardnumber);//Before you using function SetUserInfo,set the card number to make sure you can upload it to the device
            switch(RdrTpeID)
            {
                case 7://Black and White
            if (czkem.SetUserInfo(iMachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, true))//upload the user's information(card number included)
            {
                check = true;
            }
            else
            {
                czkem.GetLastError(ref idwErrorCode);
            }
            break;
            case 8://IFace
            if (czkem.SSR_SetUserInfo(iMachineNumber, sdwEnrollNumber.ToString(), sName, sPassword, iPrivilege, true))//upload the user's information(card number included)
            {
                check = true;
            }
            else
            {
                czkem.GetLastError(ref idwErrorCode);
            }
            break;
        }
            czkem.RefreshData(iMachineNumber);//the data in the device should be refreshed
            czkem.EnableDevice(iMachineNumber, true);
            return check;
        }
        #endregion

        // Get UserIfo from Device
        public List<string> GetUsersFromDevice()
        {
            List<string> FingerPrintID = new List<string>();
            try
            {
                string sEnrollNumber = "";
                string sName = "";
                string sPassword = "";
                int iPrivilege = 0;
                bool bEnabled = false;

                czkem.EnableDevice(iMachineNumber, false);
                czkem.ReadAllUserID(iMachineNumber);//read all the user information to the memory
                while (czkem.SSR_GetAllUserInfo(iMachineNumber, out sEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))
                {
                    FingerPrintID.Add(sEnrollNumber);
                    Console.WriteLine(sName);
                    Console.WriteLine(sEnrollNumber);
                    
                }
                czkem.EnableDevice(iMachineNumber, true);
                return FingerPrintID;
            }
            catch (Exception)
            {
                return FingerPrintID;
            }
        }

        // Update FP in Batch
        public bool UploadDataBatch(List<EmpUpload> _EmpUploadData)
        {
            int idwErrorCode = 0;

            string sdwEnrollNumber = "";
            string sName = "";
            int idwFingerIndex = 0;
            string sTmpData = "";
            int iPrivilege = 0;
            string sPassword = "";
            string sEnabled = "";
            bool bEnabled = false;
            int iFlag = 1;

            int iUpdateFlag = 1;
            czkem.EnableDevice(iMachineNumber, false);
            if (czkem.BeginBatchUpdate(iMachineNumber, iUpdateFlag))//create memory space for batching data
            {
                string sLastEnrollNumber = "";//the former enrollnumber you have upload(define original value as 0)
                for (int i = 0; i < _EmpUploadData.Count; i++)
                {
                    sdwEnrollNumber = _EmpUploadData[i].FPID;
                    sName = _EmpUploadData[i].Name;
                    idwFingerIndex = _EmpUploadData[i].Index;
                    sTmpData = _EmpUploadData[i].FP1;
                    iPrivilege = _EmpUploadData[i].Privilege;
                    sPassword = _EmpUploadData[i].Password;
                    sEnabled = _EmpUploadData[i].Enable;
                    iFlag = _EmpUploadData[i].Flag;

                    if (sEnabled == "true")
                    {
                        bEnabled = true;
                    }
                    else
                    {
                        bEnabled = false;
                    }
                    if (sdwEnrollNumber != sLastEnrollNumber)//identify whether the user information(except fingerprint templates) has been uploaded
                    {
                        if (czkem.SSR_SetUserInfo(iMachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled))//upload user information to the memory
                        {
                            czkem.SetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, iFlag, sTmpData);//upload templates information to the memory
                        }
                        else
                        {
                            czkem.GetLastError(ref idwErrorCode);
                            czkem.EnableDevice(iMachineNumber, true);
                        }
                    }
                    else//the current fingerprint and the former one belongs the same user,that is ,one user has more than one template
                    {
                        czkem.SetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, iFlag, sTmpData);
                    }
                    sLastEnrollNumber = sdwEnrollNumber;//change the value of iLastEnrollNumber dynamicly
                }
            }
            czkem.BatchUpdate(iMachineNumber);//upload all the information in the memory
            czkem.RefreshData(iMachineNumber);//the data in the device should be refreshed
            czkem.EnableDevice(iMachineNumber, true);
            return false;
        }

        //Register Event
        public void RegisterEvents()
        {
            try
            {
                iMachineNumber = 1;
                if (czkem.RegEvent(iMachineNumber, 65535))//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
                    czkem.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
            }
            catch (Exception ex)
            {

            }

        }
        public void UnRegisterEvents()
        {
            try
            {
                czkem.OnAttTransactionEx -= new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
            }
            catch
            {

            }
        }
        //If your fingerprint(or your card) passes the verification,this event will be triggered
        private void axCZKEM1_OnAttTransactionEx(string sEnrollNumber, int iIsInValid, int iAttState, int iVerifyMethod, int iYear, int iMonth, int iDay, int iHour, int iMinute, int iSecond, int iWorkCode)
        {
            try
            {
                if (this.AttRecordEvent != null)
                {
                    DateTime dt = new DateTime(iYear, iMonth, iDay, iHour, iMinute, iSecond);
                    this.AttRecordEvent(this, new AttEvent(sEnrollNumber, iIsInValid, iAttState, iVerifyMethod, dt));
                }
            }
            catch (Exception ex)
            {
               
            }
        }

        public delegate void MyDelegate(object sender, AttEvent e);
        public event MyDelegate AttRecordEvent;
    }

    public class AttEvent : EventArgs
    {
        private string _EnrollID;
        private int _isValid;
        private int _iAttState;
        private int _iVerifiedMethod;
        private DateTime _AttDateTime;
        public AttEvent(string EnrollID, int isValid,int iAttState,int iVerifiedMethod,DateTime AttDateTime)
        {
            this._EnrollID = EnrollID;
            this._isValid = isValid;
            this._iVerifiedMethod = iVerifiedMethod;
            this._AttDateTime = AttDateTime;
        }
        public string EnrollID
        {
            get { return _EnrollID; }
        }
        public int IsValid
        {
            get { return _isValid; }
        }

        public int AttState
        {
            get { return _iAttState; }
        }
        public int VerifiedMethod
        {
            get { return _iVerifiedMethod; }
        }
        public DateTime AttDateTime
        {
            get { return _AttDateTime; }
        }
    }
}
