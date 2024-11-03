using System.ComponentModel;

namespace Core.Enums
{
    public enum TipoRegiao
    {
        [Description(Regiao.Sudeste)]
        Sudeste = 0,
        [Description(Regiao.Sul)]
        Sul = 1,
        [Description(Regiao.CentroOeste)]
        CentroOeste = 2,
        [Description(Regiao.Norte)]
        Norte = 3,
        [Description(Regiao.Nordeste)]
        Nordeste = 4
    }

    public static class Regiao
    {
        public const string Sudeste = "Sudeste";
        public const string Sul = "Sul";
        public const string CentroOeste = "Centro - Oeste";
        public const string Norte = "Norte";
        public const string Nordeste = "Nordeste";
    }
}