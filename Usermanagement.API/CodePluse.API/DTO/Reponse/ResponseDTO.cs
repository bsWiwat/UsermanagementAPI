using System;
using static CodePluse.API.DTO.Reponse.ResponseDTO;

namespace CodePluse.API.DTO.Reponse
{
	public class ResponseDTO
	{
        public class Status
        {
            public string Code { get; set; }

            public string Description { get; set; }
        }

        public class Data
        {
            public bool Result { get; set; }

            public string Message { get; set; }
        }

        public Status status { get; set; }

        public Data data { get; set; }

        //public List<RoleDTO> Role { get; set; }

        //public List<PermissionDTO> Permission { get; set; }
    }
}

