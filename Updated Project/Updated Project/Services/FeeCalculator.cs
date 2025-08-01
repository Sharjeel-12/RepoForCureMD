using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace PatientVisitManager.Services
{
    public class FeeCalculator
    {
        private readonly Dictionary<string, int> _fees;

        public FeeCalculator(string configPath = null)
        {
            configPath ??= Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Config\fees.json"));
            if (!File.Exists(configPath)) throw new FileNotFoundException($"Missing config: {configPath}");
            _fees = JsonConvert.DeserializeObject<Dictionary<string, int>>(File.ReadAllText(configPath));
        }

        public int GetFee(string type) => _fees.TryGetValue(type, out int f) ? f : 0;
    }
}
