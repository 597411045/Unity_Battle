using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public interface PS_TEST_ISubject
{
    void AddObserver(string name, PS_TEST_IObserver ob);
    void RemoveObserver(string name, PS_TEST_IObserver ob);
    void Notify(string name, EventArgs e);

}



