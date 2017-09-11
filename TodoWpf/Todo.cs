using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoWpf
{
    class Todo : BindableBase

    {
        private Guid _id;
        private string _name;
        private bool _done;

        public Guid Id { get { return _id; } set { SetProperty(ref _id, value); } }
        public string Name{ get { return _name; } set { SetProperty(ref _name, value); } }
        public bool Done { get { return _done; } set { SetProperty(ref _done, value); } }

        public Todo()
        {
            Id = Guid.NewGuid();
        }
    }
}
