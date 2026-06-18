using CitasApp.Domain.Interfaces;

namespace CitasApp.Application.Services
{
    public class CalculadoraService
    {
        private readonly ICalculadora _calculadora;

        public CalculadoraService(ICalculadora calculadora)
        {
            _calculadora = calculadora;
        }

        public double Sumar(double a, double b) => _calculadora.Sumar(a, b);
        public double Restar(double a, double b) => _calculadora.Restar(a, b);
        public double Multiplicar(double a, double b) => _calculadora.Multiplicar(a, b);
        public double Dividir(double a, double b) => _calculadora.Dividir(a, b);
    }
}