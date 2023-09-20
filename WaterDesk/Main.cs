using WaterDesk.Contracts;

namespace WaterDesk
{
    public partial class Main : Form
    {
        private readonly IWaterDeskService _wdService;
        private BindingSource _deviceSource;

        public Main(IWaterDeskService wdService)
        {
            //_deviceSource = new BindingSource();

            _wdService = wdService;
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // Show loader on gvDevices
            tsLblStatus.Text = "Loading devices...";
            bgWorker.RunWorkerAsync();
        }

        private void bgWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var devices = _wdService.GetDevicesAsync().Result;
            _deviceSource = new BindingSource { DataSource = devices, AllowNew = false };
        }

        private void bgWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            _deviceSource.ResetBindings(false);
            gvDevices.DataSource = _deviceSource;

            tsLblStatus.Text = "Ready";
        }
    }
}