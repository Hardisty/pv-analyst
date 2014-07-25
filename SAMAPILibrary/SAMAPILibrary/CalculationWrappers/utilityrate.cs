﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;
using SAMAPILibrary.DataObjects.FinancialModels;
using SAMAPILibrary.DataObjects.OutputData;

namespace SAMAPILibrary.CalculationWrappers
{
    public class utilityrate
    {
        public utilityrate()
        {
        }

        public UtilityRateOutput run(AnnualOutputOutput aoo)
        {
            Data data = new Data();
            Module module = new Module("utilityrate");

            UtilityParams p = new UtilityParams(aoo);
            p.setDataParameters(data);

            if (module.Exec(data))
            {
                return new UtilityRateOutput(data);
            }
            else
            {
                int idx = 0;
                String msg;
                int type;
                float time;
                while (module.Log(idx, out msg, out type, out time))
                {
                    String stype = "NOTICE";
                    if (type == API.WARNING) stype = "WARNING";
                    else if (type == API.ERROR) stype = "ERROR";
                    Console.WriteLine("[ " + stype + " at time:" + time + " ]: " + msg + "\n");
                    idx++;
                }
                Console.WriteLine("utilityrate failed\n");

                return null;
            }
        }
    }
}
