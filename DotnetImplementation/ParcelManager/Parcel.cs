using System;

namespace ParcelManager
{
    public class Parcel
    {
        public int LengthMm { get; }
        public int HeightMm { get; }
        public int BreadthMm { get; }
        public decimal WeightKg { get; }
        public decimal CostNzd { get; }
        public ParcelType ParcelType { get; }


        public Parcel(int lengthMm, int breadthMm, int heightMm, decimal weightKg)
        {
            ValidateParameters(lengthMm, heightMm, breadthMm, weightKg);
            this.LengthMm = lengthMm;
            this.HeightMm = heightMm;
            this.BreadthMm = breadthMm;
            this.WeightKg = weightKg;
            this.ParcelType = CalculateType();
            this.CostNzd = CalculateCost();
        }

        private void ValidateParameters(int lengthMm, int breadthMm, int heightMm, decimal weightKg)
        {
            if (weightKg <= 0 || weightKg > 25)
            {
                throw new ArgumentException("Package must be between 0 and 25kg");
            }
            if (lengthMm <= 0 || heightMm <= 0 || breadthMm <= 0)
            {
                throw new ArgumentException("dimensions must be greater than 0mm.");
            }
        }

        private ParcelType CalculateType()
        {
            // Assumption:  overall volume is what's important here.  There is no real difference between height/width/depth
            // If needed we could get clever and sort the list of dimensions and calculate the best or worst value for customers depending on what we want.
            // I'm just going to keep simple and use volume for now.
            var volume = this.LengthMm * this.HeightMm * this.BreadthMm;
            if (volume > ParcelHelper.VolumeLargeMm)
            {
                throw new ArgumentException("Package too large.");
            }
            if (volume > ParcelHelper.VolumeMedMm)
            {
                return ParcelType.Large;
            }
            if (volume > ParcelHelper.VolumeMedMm)
            {
                return ParcelType.Large;
            }
            if (volume > ParcelHelper.VolumeSmallMm)
            {
                return ParcelType.Medium;
            }
            return ParcelType.Small;
        }

        private decimal CalculateCost()
        {
            // In real life probably get from external service or database.
            switch (this.ParcelType)
            {
                case ParcelType.Large:
                    {
                        return ParcelHelper.CostLargeNzd;
                    }
                case ParcelType.Medium:
                    {
                        return ParcelHelper.CostMedNzd;
                    }
                case ParcelType.Small:
                    {
                        return ParcelHelper.CostSmallNzd;
                    }
                default:
                    {
                        throw new ArgumentException("Package type not known");
                    }
            }
        }
    }
}