using CLI.DAO;
using CLI.Model;
using CLI.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Controller
{
    public class ProfesorController
    {
        public ProfesorDAO _profesor { get; init; }
        private readonly AdresaDAO _adresa;
        private readonly KatedraDAO _katedra;

        public ProfesorController()
        {
            _profesor = new ProfesorDAO();
            _katedra = new KatedraDAO(_profesor);
            _adresa = new AdresaDAO();
        }

        public void AddAdresa(Adresa adresa)
        { 
            _adresa.AddAdresa(adresa);
        }

        public void UpdateAdresa(Adresa adresa) 
        {
            _adresa.UpdateAdresa(adresa);
        }

        public List<Profesor> GetAllProfessors()
        {
            return _profesor.GetAllProfesors();
        }

        public void Add(Profesor profesor)
        {
            _profesor.AddProfessor(profesor);
        }

        public void Update(Profesor profesor)
        {
            _profesor.UpdateProfesor(profesor);
        }

        public void Delete(int profesorId)
        {
            _profesor.RemoveProfesor(profesorId);
        }

        public Profesor GetProfesorById(int id)
        {
            return _profesor.GetProfesorById(id);
        }

        public Katedra GetKatedraById(int id) 
        {
            return _katedra.GetKatedraById(id);
        }
        public List<Katedra> GetAllKatedra() 
        {
            return _katedra.GetAllKatedra();
        }

        public void UpdateKatedra(Katedra katedra) 
        {
            _katedra.UpdateKatedra(katedra);
        }

        public IEnumerable<Profesor> GetAllProfessors(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
        {
            return _profesor.GetAllProfesors(page,pageSize,sortCriteria,sortDirection);
        }
        public void Subscribe(IObserver observer)
        {
            _profesor.ProfesorSubject.Subscribe(observer);
        }
    }
}
