using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Bluebird.Barcode;
using Microsoft.Win32;
using Microsoft.WindowsCE.Forms;
using System.Windows.Forms;

namespace BarcodeReader.barcode
{
    public class BarcodeReader
    {
        #region [ Delegate ] 2011.11.01

        // KR : Barcode read message을 받으면 data을 받아서 처리할 대리자
        // EN : The representative to get and handle the data when get Barcode read message
        public delegate void GetDataDelegate();

        #endregion [ Delegate ]

        #region [ Variable ] 2011.11.01

        // KR : Barcode 사용을 위한 class 선언
        // EN : Declare class in order to use Barcode 
        private Barcode barcode = null;

        // KR : Message Window class 선언
        // EN : Declare Message Window class
        private MsgHandler handler = null;

        // KR : Barcode read message을 받고 실행할 대리자 선언
        // EN : Declare representative to get and handle Barcode read message
        private GetDataDelegate getdatafunc = null;

        // KR : 단말기에 장착된 barcode 모듈이 1D인지? 2D인지? 확인한다.
        // EN : Verify 1D or 2D barcode module is installed
        private bool bIs2DBarcode = false;

        // KR : Scan enable 상태를 확인한다.
        // EN : Verfiy Scan enable status
        private bool bScanEnabled = true;

        // KR : 현재 단말기가 Scan Enable을 지원하는지 확인한다.
        // EN : Verify whether the device supports Scan enable or not
        private bool bSupportScanEnable = true;

        // KR : Parameter 설정에 사용할 1D barcode type을 추가한다.
        // EN : Add 1D barcode type to use parameter setting
        private string[] arrParam1D = { "UPC-A", "UPC-E", "EAN13", "Code128" };

        // KR : Parameter 설정에 사용할 2D barcode type을 추가한다.
        // EN : Add 2D barcode type to use parameter setting
        private string[] arrParam2D = { "DataMatrix", "QRCode" };

        // KR : Parameter 설정에서 사용할 설정을 추가한다.
        // EN : Add setting to use Parameter setting
        private string[] arrExParam = { "Enable", "Translate Check Digit", "Convert UPC-E to UPC-A" };

        // KR : Param code (Get/SetParameterEx에서 사용)
        // EN : Param code (Use for Get/SetParameterEx)
        private byte byParam = 0x00;

        // KR : extra param code (Get/SetParameterEx에서 사용)
        // EN : extra param code (Use for Get/SetParameterEx)
        private byte byExParam = 0x00;

        // KR : demo page의 index을 설정한다.
        // EN : Set index of demo page
        private const int DEMO_PAGE = 0;

        // this list in Barcode.XXXX you can find barcode type list
        private string[] arrBarcodeType = { "NOTAPPLICABLE", "UPC_A", "UPC_E", "UPC_E1", "EAN8",
                                                    "EAN13", "SUPPLEMENTCODE", "UCC_COUPON_EXTENDED", "CODE39", "CODE93",
                                                    "CODE128", "INTERLEAVED2OF5", "INDUSTRIAL2OF5", "CODABAR", "KOREAN_POST",
                                                    "CODE11", "MSI", "CHINESE_POST", "RSS", "PDF417",
                                                    "ISBT128", "IATA25", "TELEPEN", "MATRIX20F5", "COMPOSITE",
                                                    "DATAMATRIX", "MAXICODE", "AZTECCODE", "MICROPDF", "QRCODE",
                                                    "TRIOPTICCODE", "PLESSEY", "CODE32", "POSICODE", "JAPANESE_POST",
                                                    "AUSTRALIAN_POST", "BRITISH_POST", "CANADIAN_POST", "NETHERLANDS_POST","POSTNET",
                                                    "OCR", "AZTEC_MESA", "CODE49", "CODABLOCK", "PLANET",
                                                    "TLC39", "STRAIGHT20F5", "CODE16K", "DISCRETE20F5", "UK_PLESSEY",
                                                    "AZTEC_RUNES", "USPS4CB", "IDTAG"
                                                  };

        // KR : 프로그램이 시작될 때의 Vitual wedge 상태를 기억하는 변수 선언
        // EN : Declare variable which memorize Virtual Wedge status when program is started
        private bool bPreVirtualWedgeEnabled = false;

        // KR : Barcode beep음의 경로를 기억하는 변수
        // EN : Variable which memorize the path of Barcode beep
        private string strBeepPath = "";

        #endregion [ Variable ]

        public BarcodeReader()
        {


            // KR : Barcode data 및 type을 리딩할 대리자에 GetData 함수를 연결시킨다.
            // EN : Connect GetData function to the representative which read Barcode data and type
            getdatafunc = new GetDataDelegate(GetData);

            // KR : Barcode class 생성 및 초기화한다.
            // EN : Create and initialize Barcode class
            barcode = new Barcode();

            // KR : Message handler 생성 및 초기화한다.
            // EN : Create and initialize Message handler
            handler = new MsgHandler(getdatafunc);

            // KR : Decode mode을 선택하는 combo box에서 default로 Trigger mode로 설정한다.
            // EN : Set Trigger mode as default at combo box which select Decode mode

            // KR : BarcodeApp가 동작 중이면 프로그램을 종료 후 계속 진행한다.
            // EN : If BarcodeApp is running, terminate the program then keep processing
            barcode.CloseBarcodeApp();

            // KR : OS version을 통해서 Windows mobile과 Windows CE을 구분한다.
            // EN : Verify between Windows Mobile and Windows CE thru OS version
            System.OperatingSystem osInfo = System.Environment.OSVersion;
            if (osInfo.Version.Minor == 0)
            {
                // Windows CE
                strBeepPath = "\\PocketStore\\data\\barcode.wav";
            }
            else
            {
                // Windows Mobile
                strBeepPath = "\\ProgramStore\\data\\barcode.wav";
            }

            // KR : Barcode module을 확인한다.
            // EN : Verify Barcode module
            GetBarcodeModule();

        }

        public void GetBarcodeModule()
        {
            // KR : HKLM\Drivers\BuiltIn\BarcodeModuleType 에서 Barcode Type을 확인한다.
            // EN : Verify Barcode Type at HKLM\Drivers\BuiltIn\BarcodeModuleType
            RegistryKey reg = Registry.LocalMachine.OpenSubKey(@"\Drivers\BuiltIn\BarcodeModuleType", false);

            if (reg == null)
            {

                // KR : 옛날 이미지일 경우 해당 registry가 없는 경우도 있으므로 확인 메시지 상자를 띄워준다.
                // EN : Pop up a confirmation message box in case OS image is old and no registry on it
               

                // KR : Messagebox에서 어떠한 값을 선택하였는지 확인 후 해당 동작을 수행한다.
                // EN : Check which value is selected at Messagebox then run the operation
      // 1D Barcode
                        bIs2DBarcode = false;
                    

                // KR : registry 확인을 종료한다.
                // EN : Terminate registry verification
                return;
            }

            string strKeyValue = string.Empty;

            // KR : Module Type을 확인한다.
            // EN : Verify Module Type
            strKeyValue = reg.GetValue("Module Type") as string;

            if (strKeyValue.Equals("IT5x00") == true || strKeyValue.Equals("N560x") == true)
            {
                // KR : key value가 IT5x00 이면 2D barcode이다.
                // EN : If key value is IT5x00, it is 2D barcode
                bIs2DBarcode = true;
            }
            else
            {
                // KR : 그 외의 값이면 1D barcode이다.
                // EN : If key value is something else, it is 1D barcode
                bIs2DBarcode = false;
            }

            reg.Close();
        }

        public void ToggleTriger()
        {
            if (barcode.IsOpened() == true)
            {
                bool bResult = false;

                // KR : Trigger의 현재 상태를 확인한다.
                // EN : Check the current status of Trigger
                if (barcode.GetTriggerState() == true)
                {
                    // KR : Trigger가 켜져 있으면 꺼준다.
                    // EN : If Trigger is on, turn Trigger off
                    bResult = barcode.SetTrigger(true);
                }
                else
                {
                    // KR : Trigger가 켜져 있으면 켜준다.
                    // EN : If Trigger is off, turn Trigger on
                    bResult = barcode.SetTrigger(true);
                }

                if (bResult == false)
                {
                    // KR : Trigger 상태 변경에 실패하면 메시지 박스를 띄워준다.
                    // EN : Popup message box if Trigger status change is failed
                    //MessageBox.Show("Set trigger failed");
                }
            }
        }

        public void Open()
        {
            // KR : Barcode가 open 되었는지 확인
            // EN : If Barcode is opened, no operation
            if (barcode.IsOpened() == true)
            {
                // KR : Barcode가 open 되어있으면 아무런 동작도 하지 않는다.
                // EN : If Barcode is opened, no operation
                return;
            }

            // Barcode device open
            if (barcode.Open(true) == false)
            {
                // KR : Barcode open에 실패하면 메시지 박스를 띄워준다.
                // EN : Pop up the message box if barcode is failed to open
                //MessageBox.Show("Barcode open failed");
                return;
            }
            
            // KR : Barcode data을 리딩 하였을 때 Window message을 전달할 경로를 설정한다.
            // EN : Set delivery path for Windows message when read Barcode data
            if (barcode.SetClientHandle(handler.Hwnd) == false)
            {
                // KR : SetClienthandle에 실패하였기 때문에 close 해준다.
                // EN : Close barcode because SetCilentHandle was failed.
                barcode.Close();
                //MessageBox.Show("Set client handle failed");
                return;
            }

            // KR : 원래 Virtual wedge가 상태를 기억한다.
            // EN : Memorize original Virtual wedge status
            bPreVirtualWedgeEnabled = barcode.GetVirtualWedge();

            // KR : Barcode data을 현재 프로그램에서 받기 위해서 virtual wedge을 false로 하여야 한다.
            // EN : In order to receive barcode data from the current program, virtual wedge must be false.
            if (bPreVirtualWedgeEnabled == true && barcode.SetVirtualWedge(false) != 0)
            {
                //MessageBox.Show("Set virtual wedge false failed");
            }

            // KR : Scan enable 상태로 변경한다.
            // EN : Change to Scan enable status
            bScanEnabled = true;
            try
            {
                if (barcode.ScanEnable(bScanEnabled) == false)
                {
                    // KR : Open에는 성공하였기 때문에 다시 close 해준다.
                    // EN : Re-close due to Open is successful
                    barcode.Close();
                    //MessageBox.Show("Scan enable failed");
                }

                bSupportScanEnable = true;
            }
            catch(Exception e)
            {
                Logger.Logger.Log(e);
                bSupportScanEnable = false;
            
                // KR : 현재 단말기에서 지원하지 않는 API 임을 출력한다.
                // EN : Output the result that this API is not supported for current device
                //MessageBox.Show("Not support API");
            }

            // KR : Scan enable checkbox을 check하고 Disable 시킨다.
            // EN : Check Scan enable checkbox then Disable


            // KR : 모든 기능을 활성화 시켜서 사용 가능하도록 한다.
            // EN : To enablee all function and make it available to use


            // KR : Trigger 상태를 확인하는 Timer을 활성화 시킨다.
            // EN : Enable Timer to check Trigger status


            // KR : parameter menu을 초기화 시켜준다.
            // EN : Initialize parameter menu

        }

        public void GetData()
        {
            // KR : Barcode Type을 반환받을 변수
            // EN : Varialbe to get return Barcode Type
            byte byType = new byte();
            // KR : Buffer의 크기를 저장
            // EN : Save Buffer size
            int nBufSize = 0;
            // KR : Reading된 data의 길이를 반환받을 변수
            // EN : Variable to get return read the length of data
            int nReadSize = 0;
            // KR : Listbox에 추가할 문자열
            // EN : String wish to add on Listbox
            string strOutput = "";


            // KR : Rawdata 출력인지 아닌지를 확인한다.
            // EN : Verify whether it is raw data output or not
            
            
                // KR : Barcode data을 받아올 변수를 선언 및 초기화한다.
                // EN : Declare and initialize the variable which is ogint be be received barcode data
                StringBuilder strBuf = new StringBuilder(1024);

                // KR : Buffer의 크기가 얼마인지 저장한다.
                // EN : Save Buffer size
                nBufSize = strBuf.Capacity;

                // KR : Barcode data와 type을 확인한다.
                // EN : Verify Barcode data and type
                if (barcode.GetDecodeDataNType(strBuf, ref byType, nBufSize, ref nReadSize) == true)
                {
                    // KR : 정상적으로 data을 가져오면 Beep음을 출력한다.
                    // EN : output beep sound if data is successfully received.


                    if (BarcodeReadType.BarcodeType.Equals(BarcodeType.READ_REGULAR_USER_TICKET))
                    {
                        System.Media.SystemSounds.Beep.Play();
                        try
                        {
                            ChargeRegularUserController.CheckOut(strBuf.ToString());
                        }
                        catch (Exception e)
                        {
                            Logger.Logger.Log(e);
                            //MessageBox.Show(e.Message);
                        }
                    }
                    else if (BarcodeReadType.BarcodeType.Equals(BarcodeType.READ_SUBSCRIBER))
                    {
                        System.Media.SystemSounds.Beep.Play();
                        try
                        {
                            ChargeSubscriberController.CheckIn(strBuf.ToString());
                        }
                        catch (Exception e)
                        {
                            Logger.Logger.Log(e);
                            //MessageBox.Show(e.Message);
                        }
                    }
                    else if (BarcodeReadType.BarcodeType.Equals(BarcodeType.READ_SUBSCRIBER_TICKET))
                    {
                        System.Media.SystemSounds.Beep.Play();
                        try
                        {
                            ChargeSubscriberController.CheckOut(strBuf.ToString());
                        }
                        catch (Exception e)
                        {
                            Logger.Logger.Log(e);
                            //MessageBox.Show(e.Message);
                        }
                    }
                    else
                    {

                    }
                    BarcodeReadType.BarcodeType = BarcodeType.NONE;
                    Close();
                }
            
        }

        public string GetBarcodeType(byte byType)
        {
            // KR : Barcode type 이 0x10 ~ 0x44 이면 바코드 type 배열의 문자열을 반환한다.
            // EN : Return Barcode type arrangement string if Barcode type is 0x10 ~ 0x44
            if ((byType >= 0x10) && (byType <= 0x44))
            {
                return arrBarcodeType[byType - 0x10];
            }
            else
            {
                return arrBarcodeType[0];
            }
        }

        public void Close()
        {
            if (barcode != null)
            {
                // KR : barcode가 null 이 아닐 경우
                // EN : In case barcode is not null
                if (barcode.IsOpened() == true)
                {
                    // KR : barcode 가 open 상태일 경우 barcode을 close해 준다.
                    // EN : Close barcode if barcode is open
                    barcode.ReleaseClientHandle();
                    barcode.Close();
                }

                barcode = null;
            }
        }

        public void txtParamValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // KR : text box에 숫자만 입력할 수 있도록 한다.
            // EN : Only allow to inout number at text box
            if (false == char.IsNumber(e.KeyChar) && e.KeyChar != 0x08)
            {
                e.Handled = true;
            }
        }



        #region [ Inner class ] 2011.11.01

        private class MsgHandler : MessageWindow
        {
            public const int WM_USER = 0x0400;
            public const int WM_SCANTRIGGER = WM_USER + 702;

            // KR : Barcode 리딩 시 실행할 대리자 생성 및 초기화
            // EN : Create and initialize the representative when Barcode reading
            private GetDataDelegate getdata = null;

            public MsgHandler(GetDataDelegate func)
            {
                // KR : GetData 대리자에 인자로 넘어온 값을 대입한다.
                // EN : Substitute passed value as factor at GetData representative
                getdata = func;
            }

            // It is receive window message and get barcode data
            protected override void WndProc(ref Message msg)
            {
                switch (msg.Msg)
                {
                    // KR : Barcode을 읽었다는 메시지가 전달되면 지정된 함수를 실행한다.
                    // EN : Operate assigned function if barcode read success message is transferred
                    case WM_SCANTRIGGER:
                        if (getdata != null)
                        {
                            getdata();
                        }
                        break;
                }
            }
        }

        #endregion [ Inner class ]
    }
    
}
