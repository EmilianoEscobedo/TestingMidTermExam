using NUnit.Framework;
using System;

namespace TestColaPrioridad
{
    public class ColaPrioridadTest
    {
        private ColaPrioridad _cola;

        [SetUp]
        public void Setup()
        {
            _cola = new ColaPrioridad();
        }

        [Test]
        public void IsEmpty_ColaVacia_RetornaTrue()
        {
            Assert.That(_cola.IsEmpty(), Is.True);
        }

        [Test]
        public void IsEmpty_ColaConElementos_RetornaFalse()
        {
            _cola.Encolar("elemento", 1);
            Assert.That(_cola.IsEmpty(), Is.False);
        }

        [Test]
        public void Encolar_ElementoConPrioridadCero_RetornaElementoConMayorPrioridad()
        {
            _cola.Encolar("elemento", 0);
            Assert.That(_cola.getMayorPrioridad(), Is.EqualTo("elemento"));
        }

        [Test]
        public void Encolar_ElementoConPrioridadNegativa_TienePrioridadMasAltaQuePositiva()
        {
            _cola.Encolar("alta", -1);
            _cola.Encolar("baja", 5);
            Assert.That(_cola.getMayorPrioridad(), Is.EqualTo("alta"));
        }

        [Test]
        public void Encolar_ElementoVacio_SeGuardaCorrectamente()
        {
            _cola.Encolar("", 1);
            Assert.That(_cola.getMayorPrioridad(), Is.EqualTo(""));
        }

        [Test]
        public void Encolar_ElementoNull_SeGuardaCorrectamente()
        {
            _cola.Encolar(null, 1);
            Assert.That(_cola.getMayorPrioridad(), Is.Null);
        }

        [Test]
        public void getMayorPrioridad_VariosElementos_RetornaElDeMenorValorPrioridad()
        {
            _cola.Encolar("bajo", 5);
            _cola.Encolar("alto", 1);
            _cola.Encolar("medio", 3);
            Assert.That(_cola.getMayorPrioridad(), Is.EqualTo("alto"));
        }

        [Test]
        public void getMayorPrioridad_ElementosConMismaPrioridad_RetornaPrimeroIngresado()
        {
            _cola.Encolar("primero", 2);
            _cola.Encolar("segundo", 2);
            _cola.Encolar("tercero", 2);
            Assert.That(_cola.getMayorPrioridad(), Is.EqualTo("primero"));
        }

        [Test]
        public void Desencolar_UnElemento_RetornaElementoYColaQuedaVacia()
        {
            _cola.Encolar("solo", 1);
            string resultado = _cola.Desencolar();
            Assert.That(resultado, Is.EqualTo("solo"));
            Assert.That(_cola.IsEmpty(), Is.True);
        }

        [Test]
        public void Desencolar_VariosElementos_RetornaElDeMayorPrioridadYLoElimina()
        {
            _cola.Encolar("bajo", 5);
            _cola.Encolar("alto", 1);
            _cola.Encolar("medio", 3);
            
            string resultado = _cola.Desencolar();
            Assert.That(resultado, Is.EqualTo("alto"));
            Assert.That(_cola.getMayorPrioridad(), Is.EqualTo("medio"));
        }

        [Test]
        public void Desencolar_CuatroElementosOrdenadosPorPrioridad_RetornaEnOrdenCorrectoDePrioridad()
        {
            _cola.Encolar("ultimo", 10);
            _cola.Encolar("primero", 1);
            _cola.Encolar("segundo", 2);
            _cola.Encolar("tercero", 3);
            
            Assert.That(_cola.Desencolar(), Is.EqualTo("primero"));
            Assert.That(_cola.Desencolar(), Is.EqualTo("segundo"));
            Assert.That(_cola.Desencolar(), Is.EqualTo("tercero"));
            Assert.That(_cola.Desencolar(), Is.EqualTo("ultimo"));
            Assert.That(_cola.IsEmpty(), Is.True);
        }

        [Test]
        public void OperacionesMixtas_EncolarYDesencolarAlternados_MantienePrioridadCorrecta()
        {
            _cola.Encolar("primero", 3);
            _cola.Encolar("segundo", 1);
            
            Assert.That(_cola.Desencolar(), Is.EqualTo("segundo"));
            
            _cola.Encolar("tercero", 2);
            Assert.That(_cola.getMayorPrioridad(), Is.EqualTo("tercero"));
            
            _cola.Encolar("cuarto", 1);
            Assert.That(_cola.getMayorPrioridad(), Is.EqualTo("cuarto"));
        }

        [Test]
        public void Encolar_PrioridadMaxInt_TieneMenorPrioridadQueValorNormal()
        {
            _cola.Encolar("max", int.MaxValue);
            _cola.Encolar("normal", 5);
            Assert.That(_cola.getMayorPrioridad(), Is.EqualTo("normal"));
        }

        [Test]
        public void Encolar_PrioridadMinInt_TieneMayorPrioridadQueValorNormal()
        {
            _cola.Encolar("min", int.MinValue);
            _cola.Encolar("normal", 5);
            Assert.That(_cola.getMayorPrioridad(), Is.EqualTo("min"));
        }

        [Test]
        public void getMayorPrioridad_LlamadaMultiple_NoModificaEstadoDeLaCola()
        {
            _cola.Encolar("permanente", 1);
            string primera = _cola.getMayorPrioridad();
            string segunda = _cola.getMayorPrioridad();
            
            Assert.That(primera, Is.EqualTo("permanente"));
            Assert.That(segunda, Is.EqualTo("permanente"));
            Assert.That(_cola.IsEmpty(), Is.False);
        }

        [Test]
        public void Desencolar_CuatroElementosMismaPrioridad_RespetaOrdenFIFO()
        {
            _cola.Encolar("a", 1);
            _cola.Encolar("b", 1);
            _cola.Encolar("c", 1);
            _cola.Encolar("d", 1);
            
            Assert.That(_cola.Desencolar(), Is.EqualTo("a"));
            Assert.That(_cola.Desencolar(), Is.EqualTo("b"));
            Assert.That(_cola.Desencolar(), Is.EqualTo("c"));
            Assert.That(_cola.Desencolar(), Is.EqualTo("d"));
        }

        [Test]
        public void getMayorPrioridad_DespuesDeMultiplesDesencolar_ActualizaPrioridadCorrectamente()
        {
            _cola.Encolar("a", 1);
            _cola.Encolar("b", 2);
            _cola.Encolar("c", 3);
            
            _cola.Desencolar();
            Assert.That(_cola.getMayorPrioridad(), Is.EqualTo("b"));
            
            _cola.Desencolar();
            Assert.That(_cola.getMayorPrioridad(), Is.EqualTo("c"));
        }
    }
}