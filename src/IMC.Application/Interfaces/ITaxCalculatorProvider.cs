namespace IMC.Application.Interfaces {
    public interface ITaxCalculatorProvider {
        ITaxCalculator GetTaxCalculator(string id);
    }
}
