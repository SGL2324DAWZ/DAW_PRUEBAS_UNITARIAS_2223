using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GestionBancariaAppNS;

namespace GestionBancariaTestSGL
{
    [TestClass]
    public class GestionBancariaTest
    {
        [TestMethod]
        [DataRow(1000, 250, 750)]
        [DataRow(1000, 1000, 0)]
        [DataRow(1000, 1, 999)]
        [DataRow(1000, 500, 500)]
        public void ValidarReintegro(double saldoInicial, double reintegro, double saldoEsperado)
        {
            GestionBancariaApp miApp = new GestionBancariaApp(saldoInicial);

            // Método a probar
            miApp.RealizarReintegro(reintegro);

            Assert.AreEqual(saldoEsperado, miApp.ObtenerSaldo(), 0.001, "Se produjo un error al realizar el reintegro, saldo incorrecto.");
        }

        [TestMethod]
        [DataRow(1000, -250)]
        [DataRow(900, -50)]
        [DataRow(10, -2)]
        public void validarReintegroCantidadNoValida(double saldoInicial, double reintegro)
        {
            GestionBancariaApp miApp = new GestionBancariaApp(saldoInicial);

            try
            {
                miApp.RealizarReintegro(reintegro);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                // assert
                StringAssert.Contains(exception.Message, GestionBancariaApp.ERR_CANTIDAD_NO_VALIDA);
                return;
            }
            Assert.Fail("Error. Se debía haber producido una excepción.");
        }

        [TestMethod]
        [DataRow(1000, 250, 1250)]
        [DataRow(1000, 1000, 2000)]
        [DataRow(1000, 1, 1001)]
        [DataRow(1000, 500, 1500)]
        public void ValidarIngreso(double saldoInicial, double ingreso, double saldoEsperado)
        {
            GestionBancariaApp miApp = new GestionBancariaApp(saldoInicial);

            // Método a probar
            miApp.RealizarIngreso(ingreso);

            Assert.AreEqual(saldoEsperado, miApp.ObtenerSaldo(), 0.001, "Se produjo un error al realizar el ingreso, saldo incorrecto.");
        }

        [TestMethod]
        [DataRow(1000, -250)]
        [DataRow(90, -5)]
        [DataRow(810, -27)]
        public void validarIngresoCantidadNoValida(double saldoInicial, double reintegro)
        {
            GestionBancariaApp miApp = new GestionBancariaApp(saldoInicial);

            try
            {
                miApp.RealizarIngreso(reintegro);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                // assert
                StringAssert.Contains(exception.Message, GestionBancariaApp.ERR_CANTIDAD_NO_VALIDA);
                return;
            }
            Assert.Fail("Error. Se debía haber producido una excepción.");
        }

        [TestMethod]
        [DataRow(100)]
        [DataRow(50)]
        [DataRow(2)]
        public void ValidarNumeroPositivo(double numeroPositivo)
        {
            // Método a probar
            bool esPositivo = GestionBancariaApp.EsPositivo(numeroPositivo);

            // Assert
            Assert.IsTrue(esPositivo, "Error al validar número positivo.");
        }

        [TestMethod]
        [DataRow(-100)]
        [DataRow(-50)]
        [DataRow(-2)]
        public void ValidarNumeroNegativo(double numeroNegativo)
        {        
            // Método a probar
            bool esPositivo = GestionBancariaApp.EsPositivo(numeroNegativo);

            // Assert
            Assert.IsFalse(esPositivo, "Error al validar número negativo.");
        }
    }
}
