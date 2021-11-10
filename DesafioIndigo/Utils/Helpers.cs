using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioIndigo
{
    public static class Helpers
    {
        public static string UfToEstado(this string value)
        {
            switch (value)
            {
                case "AC":
                    return "Acre";
                case "AL":
                    return "Alagoas";
                case "AM":
                    return "Amazonas";
                case "AP":
                    return "Amapá";
                case "BA":
                    return "Bahia";
                case "CE":
                    return "Ceará";
                case "DF":
                    return "Distrto Federal";
                case "ES":
                    return "Espírito Santo";
                case "GO":
                    return "Goiás";
                case "MA":
                    return "Maranhão";
                case "MT":
                    return "Mato Grosso";
                case "MS":
                    return "Mato Grosso do Sul";
                case "MG":
                    return "Minas Gerais";
                case "PA":
                    return "Pará";
                case "PB":
                    return "Paraíba";
                case "PR":
                    return "Paraná";
                case "PE":
                    return "Pernambuco";
                case "PI":
                    return "Piauí";
                case "RJ":
                    return "Rio de Janeiro";
                case "RN":
                    return "Rio Grande do Norte";
                case "RS":
                    return "Rio Grande do Sul";
                case "RO":
                    return "Rondônia";
                case "RR":
                    return "Roraima";
                case "SC":
                    return "Santa Catarina";
                case "SP":
                    return "São Paulo";
                case "SE":
                    return "Sergipe";
                case "TO":
                    return "Tocantis";

                default:
                    return "Brasil";
            }
        }
    }
}
