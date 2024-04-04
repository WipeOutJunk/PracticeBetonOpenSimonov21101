using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PracticeBetonNetV.CustomClasses
{
    class OrderViewModel : INotifyPropertyChanged
    {
        private int _productId;
        private string _productName;
        private int _clientId;
        private string _clientAddress;

        public event PropertyChangedEventHandler PropertyChanged;

        public OrderViewModel()
        {
            Materialtransactions = new HashSet<Materialtransaction>();
            Payments = new HashSet<Payment>();
        }

        public int OrderId { get; set; }
        //public int ClientId { get; set; }
        public int Quantity { get; set; }
        public DateOnly OrderDate { get; set; }
        public DateOnly? DeliveryDate { get; set; }
        public string Status { get; set; } = null!;
       // public string ClientAddress { get; set; }
        public virtual Client Client { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual ICollection<Materialtransaction> Materialtransactions { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }

        public int ProductId
        {
            get => _productId;
            set
            {
                if (_productId != value)
                {
                    _productId = value;
                    ProductName = GetProductNameById(value);
                    OnPropertyChanged(nameof(ProductId));
                    OnPropertyChanged(nameof(ProductName));
                }
            }
        }

        public string ProductName
        {
            get => _productName;
            set
            {
                if (_productName != value)
                {
                    _productName = value;
                    OnPropertyChanged(nameof(ProductName));
                }
            }
        }

        public int ClientId
        {
            get => _clientId;
            set
            {
                if (_clientId != value)
                {
                    _clientId = value;
                    ClientAddress = GetClientAddressById(value);
                    OnPropertyChanged(nameof(ClientId));
                    OnPropertyChanged(nameof(ClientAddress));
                }
            }
        }
        public string ClientAddress
        {
            get => _clientAddress;
            set
            {
                if (_clientAddress != value)
                {
                    _clientAddress = value;
                    OnPropertyChanged(nameof(ClientAddress));
                }
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string GetProductNameById(int productId)
        {
            using (var context = new PracticeBetonContext())
            {
                var product = context.Products.FirstOrDefault(p => p.ProductId == productId);
                return product?.Name;
            }
        }
        private string GetClientAddressById(int clientId)
        {
            using (var context = new PracticeBetonContext())
            {
                var client = context.Clients.FirstOrDefault(p => p.ClientId == clientId);
                return client?.Address;
            }
        }
    }
}
