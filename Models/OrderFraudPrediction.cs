using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IntexII.Models;

public class OrderFraudPrediction
{
    public Orders Orders { get; set; }
    public string Prediction {get; set; }
}
