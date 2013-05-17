using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace AsbaBank.Domain.Models
{
    [DataContract]
    public class Address
    {
        protected Address()
        {
            //here for the deserializer
        }

        public Address(string streetNumber, string street, string city, string postalCode)
        {
            ValidateInput("street number", streetNumber);
            ValidateInput("street", street);
            ValidateInput("postal code", postalCode);
            ValidateInput("city", city);
            StreetNumber = streetNumber;
            Street = street;
            City = city;
            PostalCode = postalCode;
        }

        [Key]
        [DataMember]
        public int Id { get; protected set; }

        [DataMember]
        public string StreetNumber { get; protected set; }

        [DataMember]
        public string Street { get; protected set; }

        [DataMember]
        public string PostalCode { get; protected set; }

        [DataMember]
        public string City { get; protected set; }

        public override string ToString()
        {
            return HasBeenCaptured() ?  string.Concat(StreetNumber, " ", Street, " ", City, " ", PostalCode) : "Address not yet captured";
        }

        public bool HasBeenCaptured()
        {
            return
                City != "Not yet captured" &&
                PostalCode != "Not yet captured" &&
                Street != "Not yet captured" &&
                StreetNumber != "Not yet captured";
        }

        public static Address NullAddress()
        {
            return new Address
                {
                    City = "Not yet captured",
                    PostalCode = "Not yet captured",
                    Street = "Not yet captured",
                    StreetNumber = "Not yet captured",
                };
        }

        private void ValidateInput(string parameterName, string value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(String.Format("Please provide a valid {0}.", parameterName));
            }
        }
    }
}