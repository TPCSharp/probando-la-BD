using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication2
{

class Actor
{
    private string nombre, apellido;

    public string first_name 
    {
        get { return nombre; }
        set { nombre = value; }
        
    }
    
    public string last_name 
    {
        get { return apellido; }
        set { apellido = value; }
    }
}

}
