using CitasApp.Domain.Interfaces;

namespace CitasApp.Infrastructure.Calculo
{
    public class CalculadoraBasica : ICalculadora
    {
        public double Sumar(double a, double b) => a + b;
        public double Restar(double a, double b) => a - b;
        public double Multiplicar(double a, double b) => a * b;

        public double Dividir(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException("No se puede dividir entre cero.");
            return a / b;
        }
    }
}