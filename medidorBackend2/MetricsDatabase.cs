namespace medidorBackend2
{
    public class MetricsDatabase
    {
        public string dataPath;
        public ILogger logger;

        protected DateTime lastDate;
        protected StreamWriter fileW;

        public string getStorageDirectory(DateTime date)
        {
            return Path.Combine(dataPath, date.Year.ToString(), date.Month.ToString(), date.Minute.ToString());
        }

        public string getMeasurementsPath(DateTime date)
        {
            return Path.Combine(getStorageDirectory(date), "measurements.csv");
        }

        public void saveMeasurement(string value)
        {
            float f_value;
            if (float.TryParse(value, out f_value))
            {
                fileW.WriteLine(value);
            } else
            {
                logger.LogError("Measurement ["+ value +"] is invalid.");
            }
            
        }

        public MetricsDatabase(ILogger logger, string dataPath)
        {
            this.logger = logger;
            this.dataPath = dataPath;
            this.lastDate = DateTime.UtcNow;

            var directory = getStorageDirectory(lastDate);
            var measurements_path = getMeasurementsPath(lastDate);

            Directory.CreateDirectory(directory);
            this.fileW = File.AppendText(measurements_path);

            logger.LogInformation("MetricsDatabase opened at: " + measurements_path);

            Task.Run(() =>
            {
                while (true)
                {
                    var newDate = DateTime.UtcNow;
                    if (newDate.Minute != lastDate.Minute || newDate.Month != lastDate.Month || newDate.Year != lastDate.Year)
                    {
                        logger.LogInformation("A new Minute begins!");
                        fileW.Close();

                        var directory = getStorageDirectory(newDate);
                        var measurements_path = Path.Combine(directory, "measurements.csv");

                        logger.LogInformation("MetricsDatabase change storage file to: " + measurements_path);

                        Directory.CreateDirectory(directory);
                        fileW = File.AppendText(measurements_path);

                        lastDate = newDate;
                    }
                    Thread.Sleep(1000);
                }
            });
        }
    }
}
