namespace DataAccessLayer.Models
{
    //todo 1 к 1 с пользователем
    public class Proxy
    {
        public long Id { get; set; }
        public string ProxyLogin { get; set; }
        public string PropxyPassword { get; set; }
        public string Address { get; set; }
        public string Port { get; set; }

    }
}
