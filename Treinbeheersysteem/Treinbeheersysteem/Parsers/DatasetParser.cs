using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;

namespace Treinbeheersysteem.Parsers
{
    public class DataSetParser
    {
        public static Perron DataSetToPerron(DataSet set, int rowIndex)
        {
            return new Perron()
            {
                Id = (long)set.Tables[0].Rows[rowIndex][0],
                Naam = set.Tables[0].Rows[rowIndex][1].ToString(),
                StationId = (long)set.Tables[0].Rows[rowIndex][2],
                Actief = (bool)set.Tables[0].Rows[rowIndex][3],
            };
        }

        public static Station DataSetToStation(DataSet set, int rowIndex)
        {
            return new Station()
            {
                Id = (long)set.Tables[0].Rows[rowIndex][0],
                Naam = set.Tables[0].Rows[rowIndex][1].ToString(),
                Actief = (bool)set.Tables[0].Rows[rowIndex][2],
            };
        }

        public static Ticket DataSetToTicket(DataSet set, int rowIndex)
        {
            return new Ticket()
            {
                Id = (long)set.Tables[0].Rows[rowIndex][0],
                Klasse = (Klasse)set.Tables[0].Rows[rowIndex][1],
                BeginStation = new Station() { Id = (long)set.Tables[0].Rows[rowIndex][2] },
                EindStation = new Station() { Id = (long)set.Tables[0].Rows[rowIndex][3] },
                Persoon = new Persoon() { Id = (long)set.Tables[0].Rows[rowIndex][4] },
                Datum = (DateTime)set.Tables[0].Rows[rowIndex][5]
            };
        }

        public static Traject DataSetToTraject(DataSet set, int rowIndex)
        {
            return new Traject()
            {
                Id = (long)set.Tables[0].Rows[rowIndex][0],
                Naam = set.Tables[0].Rows[rowIndex][1].ToString(),
                Actief = (bool)set.Tables[0].Rows[rowIndex][2],
            };
        }

        public static Trein DataSetToTrein(DataSet set, int rowIndex)
        {
            return new Trein()
            {
                Id = (long)set.Tables[0].Rows[rowIndex][0],
                Naam = set.Tables[0].Rows[rowIndex][1].ToString(),
                MaxSnelheid = (int)set.Tables[0].Rows[rowIndex][2],
                Actief = (bool)set.Tables[0].Rows[rowIndex][3],
            };
        }

        public static Verbinding DataSetToVerbinding(DataSet set, int rowIndex)
        {
            return new Verbinding()
            {
                Id = (long)set.Tables[0].Rows[rowIndex][0],
                Naam = set.Tables[0].Rows[rowIndex][1].ToString(),
                Lengte = (int)set.Tables[0].Rows[rowIndex][2],
                Actief = (bool)set.Tables[0].Rows[rowIndex][3],
            };
        }

        public static Wagon DataSetToWagon(DataSet set, int rowIndex)
        {
            return new Wagon()
            {
                Id = (long)set.Tables[0].Rows[rowIndex][0],
                Naam = set.Tables[0].Rows[rowIndex][1].ToString(),
                Stoelaantal_Klasse1 = (int)set.Tables[0].Rows[rowIndex][2],
                Stoelaantal_Klasse2 = (int)set.Tables[0].Rows[rowIndex][3],
                Actief = (bool)set.Tables[0].Rows[rowIndex][4],
            };
        }
    }
}
