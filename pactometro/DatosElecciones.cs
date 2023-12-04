using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pactometro
{
    internal class DatosElecciones
    {
        
        private List<Eleccion> elecciones;
        public delegate void AñadirEleccionDelegate(Eleccion eleccion);

        public DatosElecciones()
        {
            generarDatosIniciales();
        }

        public List<Eleccion> ObtenerElecciones()
        {
            return elecciones;
        }


        public void generarDatosIniciales()
        {
            List<Resultado> resultado1 = new List<Resultado>();
            resultado1.Add(new Resultado("PP", 136));
            resultado1.Add(new Resultado("PSOE", 122));
            resultado1.Add(new Resultado("VOX", 33));
            resultado1.Add(new Resultado("SUMAR", 31));
            resultado1.Add(new Resultado("JUNTS", 7));
            resultado1.Add(new Resultado("EH_BILDU", 6));
            resultado1.Add(new Resultado("EAJ_PNV", 5));
            resultado1.Add(new Resultado("BNG", 1));
            resultado1.Add(new Resultado("CCA", 1));
            resultado1.Add(new Resultado("UPN", 1));

            elecciones.Add(new Eleccion(resultado1, "CORTES GENERALES", "ELECCIONES GENERALES", "23/7/2023"));


            List<Resultado> resultado2 = new List<Resultado>();

            resultado2.Add(new Resultado("PSOE", 120));
            resultado2.Add(new Resultado("PP", 89));
            resultado2.Add(new Resultado("VOX", 52));
            resultado2.Add(new Resultado("PODEMOS", 35));
            resultado2.Add(new Resultado("ERC", 13));
            resultado2.Add(new Resultado("CS", 10));
            resultado2.Add(new Resultado("JUNTS", 8));
            resultado2.Add(new Resultado("BNG", 6));
            resultado2.Add(new Resultado("EH_BILDU", 5));
            resultado2.Add(new Resultado("MASPAIS", 3));
            resultado2.Add(new Resultado("CUP_PR", 2));
            resultado2.Add(new Resultado("CCA", 2));
            resultado2.Add(new Resultado("BNG", 1));
            resultado2.Add(new Resultado("OTROS", 4));

            elecciones.Add(new Eleccion(resultado2, "CORTES GENERALES", "ELECCIONES GENERALES", "10/11/2019"));

            List<Resultado> resultado3 = new List<Resultado>();

            resultado3.Add(new Resultado("PP", 31));
            resultado3.Add(new Resultado("PSOE", 28));
            resultado3.Add(new Resultado("VOX", 13));
            resultado3.Add(new Resultado("UPL", 3));
            resultado3.Add(new Resultado("SY", 3));
            resultado3.Add(new Resultado("PODEMOS", 1));
            resultado3.Add(new Resultado("CS", 1));
            resultado3.Add(new Resultado("XAV", 1));


            elecciones.Add(new Eleccion(resultado3, "PARLAMENTO DE CyL", "ELECCIONES AUTONÓMICAS", "14/2/2022"));

            List<Resultado> resultado4 = new List<Resultado>();

            resultado4.Add(new Resultado("PSOE", 35));
            resultado4.Add(new Resultado("PP", 29));
            resultado4.Add(new Resultado("CS", 12));
            resultado4.Add(new Resultado("PODEMOS", 2));
            resultado4.Add(new Resultado("UPL", 1));
            resultado4.Add(new Resultado("XAV", 1));


            elecciones.Add(new Eleccion(resultado4, "PARLAMENTO DE CyL", "ELECCIONES AUTONÓMICAS", "14/2/2022"));

        }
    }
}
