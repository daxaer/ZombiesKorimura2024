using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateResult
{

    public readonly int NextStateId;
    public readonly object ResultData; /*Aquí me he tomado la licencia de que el parámetro de entrada sea un object  
                                        * y hacer un casting dentro del State, pero en lugar de esto podríamos
                                       registrar los datos de salida de un estado en un diccionario y que fuera el estado concreto
                                        el que consultara la información que necesite. Esta técnica se utiliza para IA.*/

    public StateResult(int sigStateId, object resultData = null)
    {
        NextStateId = sigStateId;
        ResultData = resultData;
    }
}
