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
    public class PredmetController
    {
        public PredmetDAO _predmet { get; init; }
        private readonly ProfesorDAO _profesor;

        public PredmetController(ProfesorDAO profesorDAO)
        {
            _profesor = profesorDAO;
            _predmet = new PredmetDAO();       
        }

        public List<Predmet> GetAllPredmeti()
        {
            return _predmet.GetAllPredmeti();
        }

        public void Add(Predmet predmet)
        {
            _predmet.AddPredmet(predmet);
        }

        public void Delete(int predmetId)
        {
            _predmet.RemovePredmet(predmetId);
        }

        public void Update(Predmet predmet)
        {
            _predmet.UpdatePredmet(predmet);
        }

        public IEnumerable<Predmet> GetAllPredmeti(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
        {
            return _predmet.GetAllPredmeti(page,pageSize,sortCriteria,sortDirection);
        }

        public void Subscribe(IObserver observer)
        {
            _predmet.PredmetSubject.Subscribe(observer);
        }
    }
}
