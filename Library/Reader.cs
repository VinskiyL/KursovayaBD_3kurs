using System;

namespace Library
{
    public class Reader
    {
        public int id_;
        public string surname_;
        public string name_;
        public string patronymic_;
        public DateTime birthday_;
        public string education_;
        public string profession_;
        public string institut;
        public string city_;
        public string street_;
        public int house_;
        public string building;
        public int flat_;
        public string phone_;
        public int series;
        public int number;
        public string issued;
        public DateTime date;
        public DateTime consists;
        public DateTime re;

        public Reader(int id_, string surname_, string name_, string patronymic_, DateTime birthday_, string education_, string profession_, string institut, string city_, string street_, int house_, string building, int flat_, string phone_, int series, int number, string issued, DateTime date, DateTime consists, DateTime re)
        {
            this.id_ = id_;
            this.surname_ = surname_;
            this.name_ = name_;
            this.patronymic_ = patronymic_;
            this.birthday_ = birthday_;
            this.education_ = education_;
            this.profession_ = profession_;
            this.institut = institut;
            this.city_ = city_;
            this.street_ = street_;
            this.house_ = house_;
            this.building = building;
            this.flat_ = flat_;
            this.phone_ = phone_;
            this.series = series;
            this.number = number;
            this.issued = issued;
            this.date = date;
            this.consists = consists;
            this.re = re;
        }
    }
}
