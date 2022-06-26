using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InTheHand.Net.Bluetooth;
namespace BluetoothTestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load();
        }

        public async void Load()
        {

            BluetoothDevicePicker picker = new BluetoothDevicePicker();
            picker.RequireAuthentication = true;

            var selectedDevice = await picker.PickSingleDeviceAsync();
          
            InTheHand.Net.Sockets.BluetoothClient client = new InTheHand.Net.Sockets.BluetoothClient();
            client.Authenticate = true;
            await client.ConnectAsync(selectedDevice.DeviceAddress, BluetoothService.SerialPort);
            var devices = client.DiscoverDevices();
            var pairedDevices = client.PairedDevices;
            
            var stream = client.GetStream();
            var connected = BluetoothSecurity.PairRequest(selectedDevice.DeviceAddress, "1234");
            
            MessageBox.Show(this, $"Name: {selectedDevice.DeviceName}, " +
                $"Address: {selectedDevice.DeviceAddress}, " +
                $"Connected: {selectedDevice.Connected}, " +
                $"Class: {selectedDevice.ClassOfDevice}" +
                $"Is Connected: { connected }"
                );

            string open = "X";
            string close = "x";
            byte[] openArr = System.Text.Encoding.ASCII.GetBytes(open);
            byte[] closeArr = System.Text.Encoding.ASCII.GetBytes(close);

            stream.Write(openArr, 0, openArr.Length);
            
            Task.Delay(1000);

            stream.Write(closeArr, 0, closeArr.Length);
            if (connected)
            {
                
            }
            if (selectedDevice.Connected)
            {
                Debug.WriteLine("Connected !");
            }
        }
    }
}
