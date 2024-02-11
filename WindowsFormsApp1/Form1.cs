using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionBancariaAppNS
{
    public partial class GestionBancariaApp : Form
    {
        private double saldo;
        public const String ERR_CANTIDAD_NO_VALIDA = "Cantidad no válida";
        public const String ERR_SALDO_INSUFICIENTE = "Saldo insuficiente";

        public GestionBancariaApp(double saldo = 0)
        {
            InitializeComponent();
            if (saldo > 0)
                this.saldo = saldo;
            else
                this.saldo = 0;
            txtSaldo.Text = ObtenerSaldo().ToString();
            txtCantidad.Text = "0";
        }

        public double ObtenerSaldo() { return saldo; }

        public int RealizarReintegro(double cantidad) 
        {
            if (!EsPositivo(cantidad))
                //return ERR_CANTIDAD_NO_VALIDA;
                throw new ArgumentOutOfRangeException(ERR_CANTIDAD_NO_VALIDA);


            if (saldo < cantidad)
                //return ERR_SALDO_INSUFICIENTE;
                throw new ArgumentOutOfRangeException(ERR_SALDO_INSUFICIENTE);

            saldo -= cantidad;  // en lugar de sumar la forma correcta es restar
            return 0;
        }

        public int RealizarIngreso(double cantidad) {
            if (!EsPositivo(cantidad))
                //return ERR_CANTIDAD_NO_VALIDA;
                throw new ArgumentOutOfRangeException(ERR_CANTIDAD_NO_VALIDA);

            saldo += cantidad;  // en lugar de restar la forma correcta es sumar
            return 0;
        }
        public static bool EsPositivo(double numero)
        {
            // Método que valida si el número es positivo
            return numero > 0;
        }

        private void btOperar_Click(object sender, EventArgs e)
        {
            double cantidad = Convert.ToDouble(txtCantidad.Text);
            if (rbReintegro.Checked)
            {
                try
                {
                    RealizarReintegro(cantidad);
                    MessageBox.Show("Transacción realizada.");
                }
                catch (Exception error)
                {
                    if (error.Message.Contains(ERR_SALDO_INSUFICIENTE.ToString()))
                        MessageBox.Show("No se ha podido realizar la operación (¿Saldo insuficiente ?)");
                    else
                    if (error.Message.Contains(ERR_CANTIDAD_NO_VALIDA.ToString()))
                       MessageBox.Show("Cantidad no válida, sólo se admiten cantidades positivas.");
                }
            }
            else
            {
                try
                {
                    RealizarIngreso(cantidad);
                    MessageBox.Show("Transacción realizada.");
                }
                catch (Exception error)
                {
                    if (error.Message.Contains(ERR_CANTIDAD_NO_VALIDA.ToString()))
                        MessageBox.Show("Cantidad no válida, sólo se admiten cantidades positivas.");
                }
            }
            txtSaldo.Text = ObtenerSaldo().ToString();
        }
    }
}
