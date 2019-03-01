﻿namespace TradeUnionCommittee.BLL.DTO
{
    public class HobbyEmployeesDTO
    {
        public string HashId { get; set; }
        public string HashIdEmployee { get; set; }
        public string HashIdHobby { get; set; }
        public string NameHobby { get; set; }
        public uint RowVersion { get; set; }
    }

    public class HobbyChildrensDTO
    {
        public string HashId { get; set; }
        public string HashIdChildren { get; set; }
        public string HashIdHobby { get; set; }
        public string NameHobby { get; set; }
        public uint RowVersion { get; set; }
    }

    public class HobbyGrandChildrensDTO
    {
        public string HashId { get; set; }
        public string HashIdGrandChildren { get; set; }
        public string HashIdHobby { get; set; }
        public string NameHobby { get; set; }
        public uint RowVersion { get; set; }
    }
}