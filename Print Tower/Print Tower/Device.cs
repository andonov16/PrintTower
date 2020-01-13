using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.NetworkInformation;
using SnmpSharpNet;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace Print_Tower
{
    public class Device
    {
        [JsonRequired]
        private DateTime lastOnline;

        public static ObservableCollection<Device> Devices { get; private set; }
        public string DeviceName { get; private set; }
        public string IP { get; private set; }
        [JsonIgnore]
        public bool Online { get; private set; }
        public ObservableCollection<DeviceProp> DeviceProps { get; private set; }

        static Device()
        {
            Devices = new ObservableCollection<Device>();
            Device.LoadData();
        }
        public Device(string deviceName, string ip)
        {
            DeviceName = deviceName;
            IP = ip;
            DeviceProps = new ObservableCollection<DeviceProp>();
            IsOnline();
            if (Device.Devices == null)
                Device.Devices = new ObservableCollection<Device>();         
            Devices.Add(this);
        }

        public async void IsOnline()
        {
            try
            {
                var result = await new Ping().SendPingAsync(IP);
                Online = result.Status == IPStatus.Success;
                if (Online)
                {
                    lastOnline = DateTime.Now;
                }
            }
            catch (Exception)
            {
                //go on
            }
        }
        public void CheckOIDs()
        {
            IsOnline();
            if (Online)
            {
                OctetString community = new OctetString("public");
                AgentParameters param = new AgentParameters(community);
                IpAddress agent = new IpAddress(IP);
                UdpTarget target = new UdpTarget((IPAddress)agent, 161, 2000, 1);
                Pdu pdu = new Pdu(PduType.Get);
                foreach (var oid in DeviceProps)
                {
                    pdu.VbList.Add(oid.PropOID);
                }
                SnmpPacket result = target.Request(pdu, param);
                if (result != null)
                {
                    if (result.Pdu.ErrorStatus != 0)
                    {
                        throw new ArgumentException("Error in SNMP reply. Error {0} " +
                            result.Pdu.ErrorStatus + "index" +
                            result.Pdu.ErrorIndex);
                    }
                    else
                    {
                        for (int i = 0; i < DeviceProps.Count; i++)
                            try
                            {
                                DeviceProps[i].PropValue = result.Pdu.VbList[i].Value.ToString();
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                    }
                }
                else
                {
                    throw new Exception("No response received from SNMP agent.");
                }
                target.Close();
            }
        }
        public void AddDeviceProperty(string propName, string propOid)
        {
            DeviceProp newProp = new DeviceProp(propName, propOid);
            DeviceProps.Add(newProp);
            IsOnline();
            if (Online)
                CheckOIDs();
        }
        public void AddDeviceProperties(IEnumerable<DeviceProp> devProps)
        {
            foreach (DeviceProp devProp in devProps)
                DeviceProps.Add(devProp);
            IsOnline();
            if (Online)
                CheckOIDs();
        }

        public static void SaveData()
        {
            File.WriteAllText(@"data/devices.json", JsonConvert.SerializeObject(Devices));
        }
        public static void LoadData()
        {
            if (Directory.Exists("data"))
                if (File.Exists("data/devices.json"))
                {
                    string json;
                    json = File.ReadAllText("data/devices.json");
                    Devices = JsonConvert.DeserializeObject<ObservableCollection<Device>>(json);
                }
                else
                    File.Create("data/devices.json").Close();
            else
            {
                Directory.CreateDirectory("data");
                File.Create("data/devices.json").Close();
            }
        }
        public static void Delete(Device dev)
        {
            Devices.Remove(dev);
            //DBConnect.Delete(dev.DeviceName);
        }
    }

    public class DeviceProp
    {
        public string PropName { get; private set; }
        public string PropOID { get; private set; }
        public string PropValue { get; set; }

        public DeviceProp(string propName, string propOID)
        {
            this.PropName = propName;
            this.PropOID = propOID;
            this.PropValue = "No information";
        }
    }
}
