using WaterDesk.Contracts;
using WaterDesk.Models.Enums;

namespace WaterDesk
{
    public partial class Main : Form
    {
        private readonly IWaterDeskService _wdService;
        private BindingSource _deviceSource;

        private FormAction _formAction;
        private string _deviceId;

        public Main(IWaterDeskService wdService)
        {
            _formAction = FormAction.FetchDevices;

            _wdService = wdService;
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            FetchDevices();
        }

        private void FetchDevices()
        {
            gvDevices.ResetBindings();

            _formAction = FormAction.FetchDevices;
            tsLblStatus.Text = "Please wait! Fetching devices via Tuya API...";
            bgWorker.RunWorkerAsync();
        }

        private void bgWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            switch (_formAction)
            {
                case FormAction.FetchDevices:
                    var devices = _wdService.GetDevicesAsync().Result;
                    _deviceSource = new BindingSource { DataSource = devices, AllowNew = false };
                    break;

                case FormAction.ToggleDeviceSwitch:
                    _deviceId = Convert.ToString(e.Argument);
                    e.Result = ToggleDeviceSwitch(_deviceId);
                    break;
            }
        }

        private bool ToggleDeviceSwitch(string deviceId)
        {
            return _wdService.ToggleDeviceSwitchAsync(deviceId).Result;
        }

        private void bgWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            switch (_formAction)
            {
                case FormAction.FetchDevices:
                    _deviceSource.ResetBindings(false);
                    gvDevices.DataSource = _deviceSource;

                    tsLblStatus.Text = _deviceSource.Count > 0 ? $"{_deviceSource.Count} devices found." : "No device found.";
                    AddSwitchButtonToDataGridView();
                    break;

                case FormAction.ToggleDeviceSwitch:
                    gvDevices.Enabled = true;

                    var result = Convert.ToBoolean(e.Result);
                    if (result)
                    {
                        tsLblStatus.Text = $"Toggled switch for device id {_deviceId} successfully!";
                        MessageBox.Show("Toggle device switch successfully!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        tsLblStatus.Text = $"Failed to toggle switch for device id {_deviceId}.";
                        MessageBox.Show("Failed to toggle device switch", "Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    FetchDevices();

                    break;
            }
        }

        private void AddSwitchButtonToDataGridView()
        {
            gvDevices.Columns["IsOnline"]!.Visible = false;

            if (gvDevices.Columns["dgvBtnColSwitch"] != null)
                return;

            var dgvBtnCol = new DataGridViewButtonColumn
            {
                HeaderText = "Device Status",
                Name = "dgvBtnColSwitch",
                UseColumnTextForButtonValue = false,
                Width = 100
            };

            var colIndex = gvDevices.ColumnCount;
            gvDevices.Columns.Insert(colIndex, dgvBtnCol);
        }

        private void gvDevices_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || gvDevices.Columns["dgvBtnColSwitch"] == null)
                return;

            if (e.ColumnIndex == gvDevices.Columns["dgvBtnColSwitch"].Index)
            {
                // Get the value of the column that influences the button text
                var isOnline = gvDevices.Rows[e.RowIndex].Cells["IsOnline"].Value;

                e.Value = Convert.ToString(isOnline) == "True" ? "On" : "Off";
            }
        }

        private void gvDevices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvDevices.Columns["dgvBtnColSwitch"] == null) return;
            if (e.ColumnIndex != gvDevices.Columns["dgvBtnColSwitch"].Index) return;

            var row = gvDevices.Rows[e.RowIndex];
            _deviceId = Convert.ToString(row.Cells["DeviceId"].Value);
            var deviceName = Convert.ToString(row.Cells["Name"].Value);

            _formAction = FormAction.ToggleDeviceSwitch;
            gvDevices.Enabled = false;

            tsLblStatus.Text = $"Toggling switch for device '{deviceName}'";
            bgWorker.RunWorkerAsync(_deviceId);
        }
    }
}