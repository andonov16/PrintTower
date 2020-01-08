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
        public List<DeviceProp> DeviceProps { get; private set; }

        static Device()
        {
            Devices = new ObservableCollection<Device>();
            LoadData();
        }
        public Device(string deviceName, string ip)
        {
            DeviceName = deviceName;
            IP = ip;
            DeviceProps = new List<DeviceProp>();
            IsOnline();
            Devices.Add(this);
        }

        public async void IsOnline()
        {
            var result = await new Ping().SendPingAsync(IP);
            Online = result.Status == IPStatus.Success;
            if (Online)
            {
                lastOnline = DateTime.Now;
            }
        }
        public void CheckOIDs()
        {
            IsOnline();
            if (Online)
            {
                try
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
                                DeviceProps[i].PropValue = result.Pdu.VbList[i].Value.ToString();
                        }
                    }
                    else
                    {
                        throw new Exception("No response received from SNMP agent.");
                    }
                    target.Close();
                }
                catch (ArgumentException)
                {
                    //Invalid OID
                }
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
            DeviceProps.AddRange(devProps);
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
        }
    }
}
