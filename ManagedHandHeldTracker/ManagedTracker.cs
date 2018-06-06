using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Security.Cryptography;
using System.IO;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace ManagedHandHeldTracker
{
    public enum TiposDevices        // Tipos de dispositivos
    {
        NODEFINIDO,
        DEVICE,
        VIRTUALZONE,
        GPS
    }

    public class ManagedTracker
    {
        string nombre;

        Regex Event_Data = new Regex(@"BADGE:(.*),NAME:(.*),SURNAME:(.*),SSNO:(.*),COMPANY:(.*),HHID:(.*),ACCESSTYPE:(.*),DATETIME:(.*),LATITUDE:(.*),LONGITUDE:(.*),IDIMAGENEMPLEADO:(.*),IDIMAGENACCESO:(.*),READERNAME:(.*)");

        Regex datosAccesoValido = new Regex(@"VALIDO:(.*),(.*),(.*)");
        Regex datosAccesoInvalido = new Regex(@"INVALIDO:(.*)");
        Regex datosAccesoAlarma = new Regex(@"ALARMA:(.*)");

        public ManagedTracker(string v_nombre)
        {
            this.nombre = v_nombre;
        }

        public string getNombre()
        {
            return nombre;
        }

        public void showMessage(string v_mensaje)
        {

        }

        public bool isHandHeldInstalled()
        {
            return Tools.GetInstance().hasHandHeldTranslator;
        }

        public bool isVZoneInstalled()
        {
            return Tools.GetInstance().hasVZoneTranslator;
        }

        public void liveTracking(int deviceID)
        {
            try
            {
                Tools.GetInstance().DoLog("Llama a livetracking con deviceID: " + deviceID.ToString());

//                StaticCustomOptionsManager.cargarDatosLicencia();

                PanelType devType = Tools.GetInstance().getDeviceType(deviceID);
                Tools.GetInstance().DoLog("Pedido de deviceType dio: " + devType.ToString());
                if (devType ==PanelType.NODEFINIDO)
                    MessageBox.Show("Couldn't get information for the device");
                else
                {
                    
                        if (devType == PanelType.DEVICE)
                        {
                            frmLiveTracking ventana = new frmLiveTracking();

                            ventana.DEVICEID = deviceID.ToString();
                            ventana.ORGID = Tools.GetInstance().MainOrgID;

                            ventana.Text = "Live Tracking HandHeld";
                            ventana.ShowDialog();
                            ventana.Dispose();
                        }
                        if (devType == PanelType.VIRTUALZONE)
                        {
                            frmLiveTrackingVG ventana = new frmLiveTrackingVG();

                            frmLiveTrackingVG.DEVICEID = deviceID.ToString();
                            frmLiveTrackingVG.ORGID = Tools.GetInstance().MainOrgID;

                            ventana.Text = "Live tracking Virtual Zone";
                            ventana.ShowDialog();
                            ventana.Dispose();
                        }
                    }
                
            }
            catch (Exception)
            {
               // MessageBox.Show("Communication error. Try again in a few moments. " + ex.Message);
            }
        }

        /// <summary>
        /// Extrae un campo de una expresion regular reconocida
        /// </summary>
        /// <param name="resultMatch"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public string getMatchData(Match resultMatch, int index)
        {
            return resultMatch.Groups[index].Value;
        }




        /// <summary>
        /// getConnStatus: obtener el estado de la conexion del Handheld asociado al v_panelID
        /// FAIL: No conectado
        /// </summary>
        /// <param name="v_PanelID"></param>
        /// <returns></returns>
        public string getConnStatus(int v_PanelID)
        {
            return "YES";
        }

        public void extendedFeatures(int deviceID)
        {

            try
            {
                frmExtendedFeatures ventana = new frmExtendedFeatures();
                ventana.DEVICEID = deviceID.ToString();
                ventana.ORGID = Tools.GetInstance().MainOrgID;

                ventana.ShowDialog();
                ventana.Dispose();
            }
            catch (Exception)
            {

            }

        }


        public void showEvent(int serialNum, int deviceID)
        {

            try
            {
                String isConn = getConnStatus(deviceID);
                if (isConn == "FAIL")
                    MessageBox.Show("Server not available", "Connection timeout");
                else
                {
                    doShowEvent(serialNum, deviceID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Communication error. Try again in a few moments." + ex.Message);
            }

        }

        private void doShowEvent(int serialNum, int deviceID)
        {
            bool conInternet = Tools.GetInstance().hasInternetConnection();  // conInternet: HTML. sinInternet: JPG

            byte[] fotoEmp = null;
            byte[] fotoEv = null;
            string tipoEvento = "";
            string errDesc = "";
            int errCode = -1;
            string datosEvento = WebServiceAPI.GetInstance().GetEventData(deviceID.ToString(), serialNum.ToString(), conInternet.ToString(), out tipoEvento, out fotoEmp, out fotoEv, out errDesc, out errCode);

            if (String.IsNullOrEmpty(datosEvento.Trim()))
            {
                MessageBox.Show("No data available", "Information");
                return;
            }
            
            if (tipoEvento == "VALIDO")
            {
                frmEventinfoValidAccess ventana = new frmEventinfoValidAccess();
                ventana.SERIALNUM = serialNum.ToString();
                ventana.DEVICEID = deviceID.ToString();
                ventana.fotoEmp = fotoEmp;
                ventana.fotoEv = fotoEv;
                ventana.datosEvento = datosEvento;
                ventana.Text = "Valid Access";
                ventana.tipoMapa = conInternet;
                ventana.ShowDialog();
                ventana.Dispose();
                
                return;
            }

            if (tipoEvento == "INVALIDO")
            {
                frmEventInfoInvalidAccess ventana = new frmEventInfoInvalidAccess();
                ventana.SERIALNUM = serialNum.ToString();
                ventana.DEVICEID = deviceID.ToString();
                ventana.datosEvento = datosEvento;
                ventana.Text = "Invalid Access";
                ventana.tipoMapa = conInternet;

                ventana.ShowDialog();
                ventana.Dispose();
              
                return;
            }
            if (tipoEvento == "ALARMA")
            {
                eventInfoTemplate ventana = new eventInfoTemplate();
                ventana.SERIALNUM = serialNum.ToString();
                ventana.DEVICEID = deviceID.ToString();
                ventana.loadedData = datosEvento;

                ventana.Text = "Alarm Access";
                ventana.tipoMapa = conInternet;

                ventana.ShowDialog();
                ventana.Dispose();
                return;
            }
            return;
        }

        public void deviceProperties(int v_deviceID)
        {
            try
            {
                //StaticCustomOptionsManager.cargarDatosLicencia();

                String isConn = getConnStatus(v_deviceID);
                if (isConn == "FAIL")
                    MessageBox.Show("Server not available", "Connection timeout");
                else
                {

                    string deviceName = "";
                    string deviceType = "";
                    string hhMode = "";
                    //string IMEI = "";
                    string speedLimit = "";
                    string GPSupdateTime = "";

                    Tools.GetInstance().cargarPropiedadesDevice(v_deviceID.ToString(), Tools.GetInstance().MainOrgID.ToString(), 
                                                                 ref  deviceName, ref deviceType,  ref hhMode, ref speedLimit, ref GPSupdateTime);

                    if (deviceType == "DEVICE")
                    {
                        frmProperties ventana = new frmProperties();
                        ventana.ORGANIZATIONID = Tools.GetInstance().MainOrgID;     // no toma la de la llamada
                        ventana.DEVICEID = v_deviceID;
                        ventana.deviceName = deviceName;
                        ventana.deviceType = deviceType;
                        ventana.HHMode = int.Parse(hhMode);
                        //ventana.IMEI = IMEI;

                        ventana.SpeedLimit = speedLimit;
                        ventana.GPSUpdateTime = GPSupdateTime;

                        ventana.ShowDialog();
                        ventana.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Operation not supported for this type of device", "Information");
                    }

                    return;
                }
            }
            catch (Exception)
            { }

        }

        /// <summary>
        /// v_deviceID es el PanelID de Lenel
        /// </summary>
        /// <param name="v_deviceID"></param>
        public void defineZone(int v_deviceID)
        {
            try
            {

                PanelType devType = Tools.GetInstance().getDeviceType(v_deviceID);
              
                if (devType== PanelType.VIRTUALZONE)
                {
                    string errDesc = "";
                    int errCode = 0;
                    Dictionary<int, Device> listaPaneles = WebServiceAPI.GetInstance().ObtenerListaPanels(ref errDesc, ref errCode);

                    frmZoneDefinition ventana = new frmZoneDefinition();
                    ventana.ORGANIZATIONID = Tools.GetInstance().MainOrgID;     // no toma la de la llamada
                    ventana.DEVICEID = v_deviceID.ToString();
                    if (listaPaneles.ContainsKey(v_deviceID))
                    {
                        ventana.ZoneName = listaPaneles[v_deviceID].HHID;
                        Tools.GetInstance().DoLog("Zone name=" + ventana.ZoneName);
                    }
                    else
                        Tools.GetInstance().DoLog("v_deviceID=" + v_deviceID + " no encontrado. listapanels tiene " + listaPaneles.Count + " paneles");

                    //                        ventana.Text = "Zone Definition: " + v_deviceID.ToString();
                    ventana.ShowDialog();
                    ventana.Dispose();
                }
                else
                {
                    MessageBox.Show("Operation not supported for this type of device", "Information");
                }

            }
            catch (Exception)
            { }
        }
    }
}
