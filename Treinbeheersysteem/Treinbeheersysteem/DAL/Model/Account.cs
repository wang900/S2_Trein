using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Treinbeheersysteem.DAL.Model
{
    public class Account : Entity
    {
        public string Wachtwoord;
        public string Gebruikersnaam { get; set; }
        public bool Actief { get; set; }
        public Rol Rol { get; set; }
        public Persoon Persoon { get; set; }
        public string NormalizedGebruikersnaam { get; set; }
        public string NormalizedEmail { get; set; }
        

        public Account(long id, string gebruikersnaam, Rol rol, Persoon persoon, bool actief) 
        {
            Id = id;
            Gebruikersnaam = gebruikersnaam;
            Actief = actief;
            Rol = rol;
            Persoon = persoon;
        }

        public Account(long id, string gebruikersnaam, Persoon persoon)
        {
            Id = id;
            Gebruikersnaam = gebruikersnaam;
            Persoon = persoon;
        }

        public Account(long id, string gebruikersnaam, string wachtwoord)
        {
            Id = id;
            Gebruikersnaam = gebruikersnaam;
            Wachtwoord = wachtwoord;
        }


        public void ActiefOFInactiefZetten()
        {
            Actief = !Actief;
        }

        public void SetPassword(string wachtwoord)
        {
            Wachtwoord = wachtwoord;
        }
    }
}
