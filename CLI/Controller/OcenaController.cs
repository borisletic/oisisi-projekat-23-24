using CLI.DAO;
using CLI.Model;
using CLI.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Controller
{
    public class OcenaController
    {
        private readonly StudentDAO _student;
        private readonly PredmetDAO _predmet;
        private readonly OcenaDAO _ocena;

        public OcenaController(StudentDAO studentDAO,PredmetDAO predmetDAO)
        {
            _student = studentDAO;
            _predmet = predmetDAO;
            _ocena = new OcenaDAO(studentDAO,predmetDAO);
        }

        public void Add(Ocena ocena)
        {
            _ocena.AddOcena(ocena);
        }

        public void Update(Ocena ocena)
        {
            _ocena.UpdateOcena(ocena);
        }

        public void Delete(int studentId,int predmetId)
        {
            _ocena.RemoveOcena(studentId,predmetId);
        }


        public void Subscribe(IObserver observer)
        {
            _ocena.OcenaSubject.Subscribe(observer);
        }
    }
}
