using System.Windows.Forms;
using System.Net;
using System;

namespace CRM.CHATSERVER
{
    public partial class frmCHATSERVER : Form
    {
        private delegate void AtualizaStatusCallback(string strMensagem);
        private readonly string IP = Properties.Settings.Default.IP;
        private readonly string PORTA = Properties.Settings.Default.PORTA;

        private CHATSERVER mainServidor = new CHATSERVER(null, 0);

        public frmCHATSERVER()
        {
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
            InitializeComponent();

            txtIP.Text = IP;
            textBox1.Text = PORTA;
            btnAtender_Click(null, null);
        }

        private void btnAtender_Click(object sender, System.EventArgs e)
        {
            if (txtIP.Text == string.Empty)
            {
                _ = MessageBox.Show("Informe o endereço IP.");
                _ = txtIP.Focus();
                return;
            }

            try
            {
                // Analisa o endereço IP do servidor informado no textbox
                IPAddress enderecoIP = IPAddress.Parse(txtIP.Text);

                // Cria uma nova instância do objeto CRM.CHATSERVER
                mainServidor = new CHATSERVER(enderecoIP, int.Parse(textBox1.Text));

                // Vincula o tratamento de evento StatusChanged a mainServer_StatusChanged
                CHATSERVER.StatusChanged += new StatusChangedEventHandler(mainServidor_StatusChanged);

                // Inicia o atendimento das conexões
                mainServidor.IniciaAtendimento(true);

                // Mostra que nos iniciamos o atendimento para conexões
                txtLog.AppendText("Monitoramento iniciado...\r\n");
                btnAtender.Enabled = false;
                btnParar.Enabled = true;
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show("Erro de conexão : " + ex.Message);
            }
        }

        public void mainServidor_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            // Chama o método que atualiza o formulário
            _ = Invoke(new AtualizaStatusCallback(AtualizaStatus), new object[] { e.EventMessage });
        }

        private void AtualizaStatus(string strMensagem)
        {
            // Atualiza o logo com mensagens
            txtLog.AppendText(strMensagem + "\r\n");
        }

        private void btnParar_Click(object sender, EventArgs e)
        {
            txtLog.AppendText("Monitoramento parado...\r\n");
            btnAtender.Enabled = true;
            btnParar.Enabled = false;
        }

        public void OnApplicationExit(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
