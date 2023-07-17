using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace ProyectoGenerico.Helper
{
    public class IpAddressHelper
    {
        public static string HostName()
        {
            string result = "";
            try
            {
                result = Dns.GetHostName(); //Retrive the Name Of HOST
            }
            catch (Exception ex)
            {
                LogHelper.GetInstance().PrintError(ex);
            }
            return result;
        }

        public static string IpAddress()
        {
            string myIP = "";
            try
            {
                IPHostEntry host = Dns.GetHostEntry(HostName());
                myIP = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToString();
            }
            catch (Exception ex)
            {
                LogHelper.GetInstance().PrintError(ex);
            }
            return myIP;
        }

        public static string HostNameIpAddressToString()
        {
            return "- Server Name:" + IpAddressHelper.HostName() + " Ip:" + "(" + IpAddressHelper.IpAddress() + ")";
        }
    }
}