using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateResult
{

    public readonly int NextStateId;
    public readonly object ResultData; /*Aqu� me he tomado la licencia de que el par�metro de entrada sea un object  
                                        * y hacer un casting dentro del State, pero en lugar de esto podr�amos
                                       registrar los datos de salida de un estado en un diccionario y que fuera el estado concreto
                                        el que consultara la informaci�n que necesite. Esta t�cnica se utiliza para IA.*/

    public StateResult(int sigStateId, object resultData = null)
    {
        NextStateId = sigStateId;
        ResultData = resultData;
    }
}
