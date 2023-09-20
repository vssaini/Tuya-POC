using WaterDesk.Contracts;

namespace WaterDesk
{
    public partial class Main : Form
    {
        private readonly ITuyaService _tuyaService;

        public Main(ITuyaService tuyaService)
        {
            _tuyaService = tuyaService;
            InitializeComponent();
        }

        public void frmMain_Load(object sender, EventArgs e)
        {

        }
    }
}